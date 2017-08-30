using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.S3.Model;
using MarcETL.Model;

namespace MarcETL.AWS
{
    public static class S3MarcManager
    {
        public static bool UploadFile(Marc marc, string bucketName)
        {
            S3Client client = new S3Client(bucketName);
            client.UploadFile(marc.FileLocation+"/"+ marc.FileName, marc.FileName);
            return true;
        }

        
    }
}
