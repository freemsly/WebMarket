using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MarcETL.AWS
{
    public static class S3Configs
    {
        public static readonly string AccessKey = ConfigurationManager.AppSettings["accesskey"] ?? "";
        public static readonly string SecretKey = ConfigurationManager.AppSettings["secretkey"] ?? "";
        public static readonly string MarcBucketName = ConfigurationManager.AppSettings["s3marcbucketname"] ?? "";
    }
}
