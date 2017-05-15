using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using RedisLib.Core;
using RedisLib.Receiver.Constants;
using RedisLib.Receiver.Context;
using RedisLib.Receiver.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace RedisLib.UT.Receiver
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class ReceiverContextUT
    {
        private ReceiverContext<object> _ctx = null;

        [TestFixtureSetUp]
        public void Initial()
        {
            this._ctx = new ReceiverContext<object>();
            this._ctx.MsgConnection = Substitute.For<IRediser>();
            this._ctx.DataConnection = Substitute.For<IRediser>();
        }

        [TestFixtureTearDown]
        public void Finished()
        {
            this._ctx.MsgConnection = null;
            this._ctx.DataConnection = null;
            this._ctx = null;
        }

        [Test]
        public void Should_NodeIdMustBe0_When_ThereIsNoReceiverRegistried()
        {
            //Arrange
            int expect = 0, actual = -1;
            this._ctx.DataConnection.KeyExist(KeyName.ReceiverRegistry).Returns(false);
            this._ctx.DataConnection.Fetch<object>(Arg.Any<string>()).Returns<object>(null);

            //Act
            this._ctx.Run();
            actual = this._ctx.NodeId;

            //Assert
            actual.Should().Be(expect);
            this._ctx.CurrentState.StateName.Should().Be("PrepareState");
        }

        [Test]
        public void Should_NodeIdMustBe1_When_ThereIs3ReceiverRegistried_And_ReceiverNo2HasHigherDebt()
        {
            //Arrange
            int expect = 1, actual = -1;
            IDictionary<string, int> receivers = 
                new Dictionary<string, int> {
                    { Guid.NewGuid().ToString(), 0}, { Guid.NewGuid().ToString(), 1}, { Guid.NewGuid().ToString(), 2}
                };
            IDictionary<string, int> debtForNode =
                new Dictionary<string, int> {
                    { "0",1}, { "1",22}, { "2",19}
                };
            this._ctx.DataConnection.KeyExist(KeyName.ReceiverRegistry).Returns(true);
            this._ctx.DataConnection.GetHashTable<int>(KeyName.ReceiverRegistry).Returns(receivers);
            this._ctx.DataConnection.GetHashTable<int>(KeyName.ReceiverReply).Returns(debtForNode);
            this._ctx.DataConnection.Fetch<object>(Arg.Any<string>()).Returns<object>(null);

            //Act
            this._ctx.Initial();
            this._ctx.Run();
            actual = this._ctx.NodeId;

            //Assert
            actual.Should().Be(expect);
            this._ctx.CurrentState.StateName.Should().Be("PrepareState");
        }

        [Test]
        public void Should_TopElementMustBeAPI_When_ExecuteRecordsIsEmpty() {
            //Arrange
            enLogType expect = enLogType.API, actual;
            this._ctx.DataConnection.Fetch<object>(Arg.Any<string>()).Returns<object>(null);

            //Act
            this._ctx.Run();
            actual = this._ctx.ExecutedRecords.Dequeue();

            //Assert
            actual.Should().Be(expect);
        }

        [Test]
        public void Should_TopElementMustBeBO_When_AddNewOne_And_ThereAreAPIAndBOInExecutedRecords() {
            //Arrange
            enLogType expect = enLogType.BO, actual;
            this._ctx.ExecutedRecords.Enqueue(enLogType.API);
            this._ctx.ExecutedRecords.Enqueue(enLogType.BO);
            this._ctx.DataConnection.Fetch<object>(Arg.Any<string>()).Returns<object>(null);

            //Act
            this._ctx.Run();
            actual = this._ctx.ExecutedRecords.Dequeue();

            //Assert
            actual.Should().Be(expect);
        }

        [Test]
        public void Should_KeyPatternMustBeStartedWithAPI0_When_ThereIsNoExecutedRecords_And_NodeIdIs0() {
            //Arrange
            string expect = "{API/0}";
            this._ctx.DataConnection.Fetch<object>(Arg.Any<string>()).Returns<object>(null);

            //Act
            this._ctx.Run();

            //Assert
            this._ctx.DataConnection.Received().Fetch<object>(Arg.Is<string>(x => x.Split(':')[0].Equals(expect)));
        }

        [Test]
        public void Should_ThereAreSomethingInDataObjs_When_FetchSomething() {
            //Arrange
            object something = new object();
            List<object> expect = new List<object> { something },
                                  actual = null;
            this._ctx.DataConnection.Fetch<object>(Arg.Any<string>()).Returns<object>(expect);

            //Act
            this._ctx.Run();
            actual = this._ctx.DataObjs;

            //Assert
            actual.Should().BeSameAs(actual);
        }

    }
}
