using System.Diagnostics.CodeAnalysis;

namespace RedisLib.Sender.Constants
{
    [ExcludeFromCodeCoverage]
    public static class KeyName
    {
        public static string ReceiverRegistry = "ReceiverRegistry";

        public static string ReceiverReply = "ReceiverReply";

        public static string KeyBuffer = "KeyBuffer";
    }
}
