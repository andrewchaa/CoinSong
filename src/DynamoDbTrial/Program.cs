using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
using Newtonsoft.Json;

namespace DynamoDbTrial
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Hello World!");

            SaveO().Wait();

        }

        private static async Task SaveO()
        {
            var credentials = new BasicAWSCredentials("", "");
            var dynamoDb = new AmazonDynamoDBClient(credentials, RegionEndpoint.EUWest2);

            var response = await dynamoDb.PutItemAsync(
                tableName: "HistoricalRates",
                item: new Dictionary<string, AttributeValue>
                {
                    {"Currency", new AttributeValue {S = "BTC"}},
                    {"Date", new AttributeValue {S = DateTime.Today.ToString("yyyy-MM-dd")}},
                    {"Rates", new AttributeValue {S = JsonConvert.SerializeObject(new { Test = 1})}}
                });
        }
    }
}
