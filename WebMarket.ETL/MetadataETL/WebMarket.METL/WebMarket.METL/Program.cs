using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.Extensions.Configuration;
using Nest;
using WebMarket.Contracts;
using WebMarket.ETL;
using WebMarket.ETL.extractor;
using WebMarket.Model;
using WebMarket.Server.elasticsearch;

namespace WebMarket.METL
{
    class Program
    {

        

        static void Main(string[] args)
        {
            try
            {
                if (args == null || !args.Any()) return;
                switch (args[0].ToLower())
                {
                    case "onemart":
                        ExecuteOneMart();
                        break;
                    case "ownershipprofile":
                        ExecuteOneclickOwnershipProfile();
                        break;
                    default:
                        ExecuteOneMart();
                        break;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
            Console.ReadLine();
        }

        

        private static void ExecuteOneMart()
        {
            //PushNotification("{'text' : '" + ConfigConstants.Environment.ToUpper() + " METL started " + DateTime.Now + "'}");
            Stopwatch timerStopwatch = new Stopwatch();
            timerStopwatch.Start();
            IProcessingStrategy<ProcessItem<MediaTitle>> strategy = new OneMartProcessingStrategy()
            {
                DataSource = new OneMartDataServer()
            };

            if (strategy.Initialize())
            {
                strategy.ExecuteStrategy();
                strategy.Cleanup();
            }
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine(@"ExecuteOneMart completed in {0} mins", timerStopwatch.Elapsed.Minutes);
            Console.WriteLine(Environment.NewLine);


            ExecuteMagazineImport();
            ExecuteOrderHistory();
            //PushNotification("{'text' : '" + Constants.Environment.ToUpper() + " METL finished running successfully at " + DateTime.Now + " '}");
        }

        private static void PushNotification(string message)
        {
            //if (!eXtensibleConfig.Zone.Equals("production", StringComparison.OrdinalIgnoreCase)) return;
            //PushNotificationToSlack(message);
        }

        private static void PushNotificationToSlack(string message)
        {
            using (WebClient client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json;charset=UTF-8";
                client.Encoding = Encoding.UTF8;
                var reply = client.UploadString("https://hooks.slack.com/services/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",
                    message);
                Console.WriteLine(reply);
            }
        }


        private static void ExecuteMagazineImport()
        {
            Stopwatch timerStopwatch = new Stopwatch();
            timerStopwatch.Start();
            MagazineExtractor.BulkInsert();
            Console.WriteLine(@"MagazineExtractor completed in {0} sec", timerStopwatch.Elapsed.Seconds);
            Console.WriteLine(Environment.NewLine);
        }

        private static void ExecuteOrderHistory()
        {
            Stopwatch timerStopwatch = new Stopwatch();
            timerStopwatch.Start();
            OrderHistoryExtractor.BulkInsert();
            Console.WriteLine(@"ExecutePurchaseOrder completed in {0} sec", timerStopwatch.Elapsed.Seconds);
            Console.WriteLine(Environment.NewLine);
        }

        private static void ExecuteOneclickOwnershipProfile()
        {
            Stopwatch timerStopwatch = new Stopwatch();
            timerStopwatch.Start();
            OneclickTransactionsExtractor.ProfileOwnership();
            Console.WriteLine(@"ExecuteOneclickOwnershipProfile completed in {0} sec", timerStopwatch.Elapsed.Seconds);
            Console.WriteLine(Environment.NewLine);
        }
    }
}
