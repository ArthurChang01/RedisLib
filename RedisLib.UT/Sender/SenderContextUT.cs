using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using RedisLib.Core;
using RedisLib.Sender.Constants;
using RedisLib.Sender.Context;
using RedisLib.Sender.Models;
using RedisLib.Sender.SenderStates.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace RedisLib.UT.Sender
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class SenderContextUT
    {
        private SenderContext<object> _ctx = null;
        private List<ReceiverRecord> rcRecords = new List<ReceiverRecord>();

        [TestFixtureSetUp]
        public void Initial()
        {
            _ctx = new SenderContext<object>();
            _ctx.ReceiverTable.Receivers = rcRecords;

            _ctx.MsgConnection = Substitute.For<IRediser>();
            _ctx.DataConnection = Substitute.For<IRediser>();
        }

        [TearDown]
        public void EachTime()
        {
            rcRecords.Clear();
            this._ctx.ReceiverTable.Receivers = rcRecords;
        }

        [TestFixtureTearDown]
        public void Finished()
        {
            if (this._ctx.ReceiverTable.Amount > 0)
                this._ctx.ReceiverTable.Receivers.Clear();

            this._ctx.ReceiverTable.Receivers = null;
            this._ctx.MsgConnection = null;
            this._ctx.DataConnection = null;
            this._ctx = null;
        }

        #region InitialState
        [Test]
        public void Should_ChangeStateToInitialState_When_ExecuteInitial()
        {
            //Arrange
            string expect = "InitialState",
                      actual = string.Empty;

            //Act
            this._ctx.Initial();
            ISenderState currentState = this._ctx.CurrentState;
            actual = currentState.StateName;

            //Assert
            actual.Should().NotBeEmpty().And.Be(expect);
        }

        [Test]
        public void Should_HasOneReceiverRecord_When_OneReceiverHasRegistered()
        {
            //Arrange
            string receiverId = Guid.NewGuid().ToString();
            ReceiverRecord expect = new ReceiverRecord { ReceiverId = receiverId, ReceiverNodeId = 0, UnReplyCounter = 0 },
                                        actual = null;

            _ctx.DataConnection.KeyExist(KeyName.ReceiverRegistry).Returns<bool>(true);
            _ctx.DataConnection.GetHashTable<int>(KeyName.ReceiverRegistry)
                .Returns<IDictionary<string, int>>(new Dictionary<string, int> {
                    { receiverId, 0}
                });

            _ctx.DataConnection.KeyExist(KeyName.ReceiverReply).Returns<bool>(true);
            _ctx.DataConnection.GetHashTable<int>(KeyName.ReceiverReply)
                .Returns<IDictionary<string, int>>(new Dictionary<string, int> {
                    { "0",0 }
                });

            //Act
            this._ctx.Initial();
            actual = this._ctx.ReceiverTable.Receivers.ElementAt(0);

            //Assert
            actual.Should().NotBeNull().And.Be(expect);
        }

        [Test]
        public void Should_HasOneReceiverRecord_When_OneReceiverHasRegistered_ButNotYetInitialReplyInfor()
        {
            //Arrange
            string receiverId = Guid.NewGuid().ToString();
            ReceiverRecord expect = new ReceiverRecord { ReceiverId = receiverId, ReceiverNodeId = 0, UnReplyCounter = 0 },
                                        actual = null;

            _ctx.DataConnection.KeyExist(KeyName.ReceiverRegistry).Returns<bool>(true);
            _ctx.DataConnection.GetHashTable<int>(KeyName.ReceiverRegistry)
                .Returns<IDictionary<string, int>>(new Dictionary<string, int> {
                    {  receiverId, 0 }
                });

            _ctx.DataConnection.KeyExist(KeyName.ReceiverReply).Returns<bool>(false);

            //Act
            this._ctx.Initial();
            actual = this._ctx.ReceiverTable.Receivers.ElementAt(0);

            //Assert
            actual.Should().NotBeNull().And.Be(expect);
        }
        #endregion

        #region PrepareState
        [Test]
        public void Should_ChangeStateToProcessState_When_ExecuteSend()
        {
            //Arrange
            string expect = "ProcessState",
                      actual = string.Empty;
            object dataValue = new object();

            //Act
            this._ctx.Initial();
            this._ctx.Send(enLogType.BO, dataValue);
            ISenderState currentState = this._ctx.CurrentState;
            actual = currentState.StateName;

            actual.Should().NotBeEmpty().And.Be(expect);
        }

        [Test]
        public void Should_DataKeyStartWithBO0_When_LogTypeIsBO_And_ThereIsOnlyOneReceiver()
        {
            //Arrange
            string expect = "{BO/0}",
                      actual = string.Empty;
            rcRecords.Add(new ReceiverRecord { ReceiverId = Guid.NewGuid().ToString(), ReceiverNodeId = 0, UnReplyCounter = 0 });

            //Act
            this._ctx.Send(enLogType.BO, new object());
            actual = this._ctx.DataKey.Split(':')[0];

            //Assert
            actual.Should().NotBeEmpty().And.Be(expect);
        }

        [Test]
        public void Should_DataKeyStartWithBO0_When_LogTypeIsBO_And_ThereIsNoReceiver()
        {
            //Arrange
            string expect = "{BO/0}",
                      actual = string.Empty;

            //Act
            this._ctx.Initial();
            this._ctx.Send(enLogType.BO, new object());
            actual = this._ctx.DataKey.Split(':')[0];

            //Assert
            actual.Should().NotBeEmpty().And.Be(expect);
        }

        [Test]
        public void Should_DataKeyStartWithBO3_When_LogTypeIsBO_And_CandidateIdIs1_And_ThereAre3Receivers_But_NodeIdSerialNumberIs013()
        {
            //Arrange
            string expect = "{BO/3}",
                      actual = string.Empty;
            rcRecords.Add(new ReceiverRecord { ReceiverId = Guid.NewGuid().ToString(), ReceiverNodeId = 0, UnReplyCounter = 0 });
            rcRecords.Add(new ReceiverRecord { ReceiverId = Guid.NewGuid().ToString(), ReceiverNodeId = 1, UnReplyCounter = 0 });
            rcRecords.Add(new ReceiverRecord { ReceiverId = Guid.NewGuid().ToString(), ReceiverNodeId = 3, UnReplyCounter = 0 });
            this._ctx.ReceiverTable.Receivers = rcRecords;
            this._ctx.ReceiverTable.CandidateInfo = new Dictionary<enLogType, int> { { enLogType.API,-1},{ enLogType.BO,1},{ enLogType.System,-1 } };

            //Act
            this._ctx.Send(enLogType.BO, new object());
            actual = this._ctx.DataKey.Split(':')[0];

            //Assert
            actual.Should().NotBeEmpty().And.Be(expect);
        }
        #endregion

        #region ProcessState
        [Test]
        public void Should_SaveMustBeExecuted_And_SetHashTablePlusMustBeExecuted_When_ThereIsOneReceiver()
        {
            //Arrange
            object dataValue = new object();
            string receiverId = Guid.NewGuid().ToString();

            rcRecords.Add(new ReceiverRecord { ReceiverId = receiverId, ReceiverNodeId = 0, UnReplyCounter = 0 });
            this._ctx.ReceiverTable.CandidateInfo = new Dictionary<enLogType, int> { { enLogType.API, -1 }, { enLogType.BO, 1 }, { enLogType.System, -1 } };

            //Act
            this._ctx.Send(enLogType.BO, dataValue);

            //Assert
            this._ctx.DataConnection.Received().Save(Arg.Is<string>(x => x.Split(':')[0].Equals("{BO/0}")), dataValue);
            this._ctx.DataConnection.Received().SetHashTable_Plus(KeyName.ReceiverReply, "0", 1);
        }

        [Test]
        public void Should_SaveMustBeExecuted_And_SetHashTablePlusMustBeExecuted_But_PublishMessageDontBeExecuted_When_ThereIsNoReceiver()
        {
            //Arrange
            object dataValue = new object();

            this._ctx.ReceiverTable.Receivers = rcRecords;
            this._ctx.ReceiverTable.CandidateInfo = new Dictionary<enLogType, int> { { enLogType.API, -1 }, { enLogType.BO, 0 }, { enLogType.System, -1 } };

            //Act
            this._ctx.Initial();
            this._ctx.Send(enLogType.BO, dataValue);

            //Assert
            this._ctx.DataConnection.Received().Save(Arg.Is<string>(x => x.Split(':')[0].Equals("{BO/0}")), dataValue);
            this._ctx.DataConnection.Received().SetHashTable_Plus(KeyName.ReceiverReply, "0", 1);
            this._ctx.DataConnection.DidNotReceive().PublishMessage(Arg.Any<string>(), Arg.Any<string>());
        }
        #endregion
    }
}
