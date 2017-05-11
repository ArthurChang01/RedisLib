using System.Diagnostics.CodeAnalysis;

namespace RedisLib.Receiver.Constants
{
    [ExcludeFromCodeCoverage]
    public static class ChannelName
    {
        public static string ReceiveReply = "ReceiveReply_{0}";
        public static string Sync_Message = "Sync_Message";
    }
}
