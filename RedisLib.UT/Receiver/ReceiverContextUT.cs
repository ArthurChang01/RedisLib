using NSubstitute;
using NUnit.Framework;
using RedisLib.Core;
using RedisLib.Receiver.Context;

namespace RedisLib.UT.Receiver
{
    [TestFixture]
    public class ReceiverContextUT
    {
        private ReceiverContext<object> _ctx = null;

        [TestFixtureSetUp]
        public void Initial()
        {

            this._ctx.MsgConnection = Substitute.For<IRediser>();
            this._ctx.DataConnection = Substitute.For<IRediser>();
        }

        [TearDown]
        public void EachTime()
        {
        }

        [TestFixtureTearDown]
        public void Finished()
        {
            this._ctx.MsgConnection = null;
            this._ctx.DataConnection = null;
            this._ctx = null;
        }

        #region InitialState
        [Test]
        public void Should_ChangeStateToInitialState_When_ExecuteInitial()
        {
            //Arrange

            //Act

            //Assert
        }
        #endregion

        #region RegistryState
        #endregion

        #region PrepareState
        #endregion

        #region FetchDataState
        #endregion

        #region ProcessState
        #endregion

        #region FinishState
        #endregion
    }
}
