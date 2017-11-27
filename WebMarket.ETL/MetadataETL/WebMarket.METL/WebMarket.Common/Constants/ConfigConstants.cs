// <copyright company="Recorded Books LLC" file="ConfigConstants.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace WebMarket.Common
{

    internal static class ConfigConstants
    {
        public const string ImageUrlKey = "api.url.images";
        public const string Protocol = "api.api.protocol";
        public const string WebDomain = "api.web.domain";
        public const string MarcLink = "marclink";
        public const string MarcFileLocation = "marcfilelocation";
        public const string MarcOutputFilePath = "marcApiServer";
        public const string Environment = "environment";

        public const string MARCUploadsrc = "marcUploadsrc";
        
        public const string MARCUploaddest1 = "marcUploaddest1";
        public const string MARCUploaddest2 = "marcUploaddest2";


        public static class Defaults
        {
            public const string ImageUrl = "http://images.oneclickdigital.com/";
            public const int TrustedAppExpiryInHours = 4;
            public const string Protocol = "https";
            public const string WebDomain = "com";
            public const string MarcLink = "http://www.recordedbooks.com/cfc/cartapi.cfc?method=GenerateMarcFile&DSN=rbent&prodIdList=";
            public const string MarcFileLocation = "~/Marc_Files/";
            public const string Environment = "na";
            public const string MarcOutputFilePath = @"\\192.168.100.83\d$\API\na\MARC_Files";

            public const string MARCUploadsrc = @"\\192.168.100.81\d$\pre_marc_files";
           
            public const string MARCUploaddest1 = @"\\192.168.100.81\d$\marcfile";
            public const string MARCUploaddest2 = @"\\192.168.100.83\d$\marcfile";
        }
    }

}
