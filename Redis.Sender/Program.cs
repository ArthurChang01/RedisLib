using Redis.Sender.Context;
using Redis.Sender.SenderStates.Models;
using System;

namespace Redis.Sender
{
    class Program
    {
        static void Main(string[] args)
        {
            SenderContext ctx = new SenderContext();
            ctx.Initial();

            while (true) {
                Console.WriteLine("請輸入值:");
                string value = Console.ReadLine();

                ctx.Send(enLogType.API, value);
            }

            Console.ReadKey();
        }
    }
}
