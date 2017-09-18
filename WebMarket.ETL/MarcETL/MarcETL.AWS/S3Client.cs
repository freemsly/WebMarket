using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon.S3.IO;

namespace MarcETL.AWS
{
    public class S3Client
    {
        public string BucketName { get; set; }
        public  IAmazonS3 Client;
        public S3Client(string bucketName)
        {
            BucketName = bucketName;
            Client = new AmazonS3Client(S3Configs.AccessKey,S3Configs.SecretKey, RegionEndpoint.USEast1);
        }

        public bool UploadFile(string sourcePath, string fileName)
        {

            var fileExists = new Amazon.S3.IO.S3FileInfo(Client, BucketName, fileName);
            
            var b = false;
            try
            {
                if (fileExists.Exists)
                {
                    // Delete existing file and re- upload
                    Client.DeleteObject(new Amazon.S3.Model.DeleteObjectRequest() { BucketName = BucketName, Key = fileName });                    
                }
                
                var utility = new TransferUtility(Client);
                var request = new TransferUtilityUploadRequest
                {
                    BucketName = BucketName,
                    Key = fileName,
                    FilePath = sourcePath
                };

                utility.Upload(request);
                b = true;
            }
            catch (AmazonS3Exception ex)
            {
                Console.WriteLine("Caught Exception: " + ex.Message);
                Console.WriteLine("Response Status Code: " + ex.StatusCode);
                Console.WriteLine("Error Code: " + ex.ErrorCode);
                Console.WriteLine("Error Type: " + ex.ErrorType);
                Console.WriteLine("Request ID: " + ex.RequestId);
            }
            return b;
        }
        
    }
}
