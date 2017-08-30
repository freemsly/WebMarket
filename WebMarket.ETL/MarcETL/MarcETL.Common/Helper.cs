using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarcETL.Common
{
    public static class Helper
    {
        public static string ExtractProductNumber(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("FileName cannot be empty");
            }
            return fileName.Substring(0, fileName.Length - 4);
        }

        public static string ExtractFileName(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException("FilePath cannot be empty");
            }
            var fileName = System.IO.Path.GetFileName(filePath);
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("FileName is empty");
            }
            return fileName;
        }

        public static string[] GetMarcExtensionFiles(string filePath)
        {
            return Directory.GetFiles(filePath, "*.mrc");
        }
    }
}
