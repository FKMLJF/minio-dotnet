using Microsoft.Extensions.Configuration;
using Minio;
using Minio.Exceptions;
using System;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
   
        static void Main(string[] args)
        {
            IConfiguration Config = new ConfigurationBuilder()
                 .AddJsonFile("appSettings.json")
                 .Build();

 

           var endpoint = Config.GetSection("S3:Endpoint").Value;
            var accessKey = Config.GetSection("S3:Accesskey").Value;
            var secretKey = Config.GetSection("S3:Secretkey").Value;
            try
            {
                var minio = new MinioClient(endpoint, accessKey, secretKey).WithSSL();
                Run(minio).Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hiba: " + ex.Message);
            }
            Console.ReadLine();
        }

        public async static Task Run(MinioClient minio)
        {
            try
            {
               
                var list = await minio.ListBucketsAsync();
                foreach (var bucket in list.Buckets)
                {

                //var listO = await minio.Li
                
               
                    Console.WriteLine($"Buket name: {bucket.Name}  Created at: {bucket.CreationDateDateTime}");
                }
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine($"[Bucket]  Exception: {e}");
            }
        }
    }
}
