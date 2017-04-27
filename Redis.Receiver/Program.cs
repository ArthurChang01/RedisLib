using RedisLib.Receiver.Context;
using System;

namespace Redis.Receiver
{
    class Program
    {
        static void Main(string[] args)
        {
            ReceiverContext ctx = new ReceiverContext();

            ctx.Run();

            Console.ReadKey();
        }
    }
}
