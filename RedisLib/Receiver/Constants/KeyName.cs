using System.Diagnostics.CodeAnalysis;

namespace RedisLib.Receiver.Constants
{
    [ExcludeFromCodeCoverage]
    public static class KeyName
    {
        public static string KeyBuffer = "KeyBuffer";
        public static string ReceiverRegistry = "ReceiverRegistry";
        public static string ReceiverReply = "ReceiverReply";
    }
}
