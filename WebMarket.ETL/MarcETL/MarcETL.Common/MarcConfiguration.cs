using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarcETL.Common
{
    public static class MarcConfiguration
    {
        public static string SourceDirectory = @"C:\Source";
        public static string DestinationDirectory = @"C:\Destination";

        public static string GetSourceDirectory()
        {
            var sourceDirectory = ConfigurationManager.AppSettings["sourcedirectory"];
            if (!string.IsNullOrEmpty(sourceDirectory))
            {
                SourceDirectory = sourceDirectory;
            }
            return SourceDirectory;
        }

        public static string GetDestinationDirectory()
        {
            var sourceDirectory = ConfigurationManager.AppSettings["destinationdirectory"];
            if (!string.IsNullOrEmpty(sourceDirectory))
            {
                SourceDirectory = sourceDirectory;
            }
            return SourceDirectory;
        }

        public static string GetMarcSqlConnection()
        {
            var sqlConnection = ConfigurationManager.ConnectionStrings["marc"].ConnectionString;
            return sqlConnection;
        }
    }
}
