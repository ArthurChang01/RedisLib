using RedisLib.Logger;
using System;
using System.Configuration;

namespace Redis.Sender
{
    class Program
    {
        static void Main(string[] args)
        {
            string conString = ConfigurationManager.ConnectionStrings["redis"].ConnectionString;

            RedisLogger logger = new RedisLogger(conString);

            while (true)
            {
                Console.WriteLine("請輸入文字:");
                var input = Console.ReadLine();
                logger.SaveLog(string.Format(@"{0}{1}", "{apilog}", input), input);
            }

            Console.ReadKey();
        }
    }
}
