using System.Diagnostics.CodeAnalysis;

namespace RedisLib.Sender.Constants
{
    [ExcludeFromCodeCoverage]
    public static class ChannelName
    {
        public static string ReceiveReply = "ReceiveReply_{0}";

        public static string ReceiverRegistry = "ReceiverRegistry";
    }
}
