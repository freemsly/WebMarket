﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.SqlClient;
using System.IO;
using MarcEtlContracts;
using MarcETL.AWS;
using MarcETL.Common;
using MarcETL.Model;


namespace MarcETL.NA
{
    public class Processor : MarcProcess<Marc>
    {
        private List<Marc> _filesList;
        private string Environment = "NA";
        
        public override List<Marc> Extract()
        {
            var filePath = MarcConfiguration.GetSourceDirectory() + "\\" + Environment;
            string[] dirs = Helper.GetMarcExtensionFiles(filePath);
            Console.WriteLine("The number of files having extension .mrc is {0}.", dirs.Length);
            _filesList = new List<Marc>();
            foreach (string file in dirs)
            {
                Console.WriteLine(file);
                
                var marc = new Marc();
                marc.FileName = Helper.ExtractFileName(file);
                marc.ProductNumber = Helper.ExtractProductNumber(marc.FileName);
                marc.FileLocation = MarcConfiguration.GetSourceDirectory() + "\\"+ Environment;
                
                _filesList.Add(marc);
            }
            Console.WriteLine("extracted marc files for NA");
            return _filesList;
        }

        public override void Transfer(List<Marc> filesList)
        {
            //move the files to new location
            Console.WriteLine("moved marc files for NA");
            var i = 0;
            foreach (var item in filesList)
            {
                try
                {
                    Console.WriteLine("Transfer of product - " + item.Isbn);
                    
                    S3MarcManager.UploadFile(item, CompositeBucketName(item));
                    item.IsFileUploaded = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                Console.WriteLine("No of files transferred - " + i++);

            }
        }


        public override void UpdateState()
        {
            foreach (var item in _filesList)
            {
                if (item.IsFileUploaded)
                {
                    //update the database that marc is uploaded
                    Console.WriteLine("updated HasMARC for NA");

                    using (var cn = new SqlConnection(MarcConfiguration.GetMarcSqlConnection()))
                    {
                        using (
                            var sqlCommand =
                                new SqlCommand(
                                    //"IF NOT EXISTS(Select 1 from ProductMARC where ProductNumber = @ProdNumber) " +
                                    "IF NOT EXISTS(Select 1 from Marc where ProductNumber = @ProdNumber and Environment =@environment) " +
                                    "BEGIN " +
                                    "INSERT INTO Marc(ProductNumber,HasMARC,MARCFileName,MARCType,UpdatedAt,Environment) VALUES (@ProdNumber,1,@filename,'F',GetDate(),@environment) " +
                                    "END ", cn))
                        {
                            cn.Open();
                            sqlCommand.Parameters.AddWithValue("@ProdNumber", item.ProductNumber);
                            sqlCommand.Parameters.AddWithValue("@filename", item.FileName);
                            sqlCommand.Parameters.AddWithValue("@environment", Environment);
                            sqlCommand.ExecuteNonQuery();
                            cn.Close();
                        }
                    }
                }
            }
        }


        public override void CleanUp()
        {

            foreach (var item in _filesList)
            {
                if (item.IsFileUploaded)
                {
                    // gp to the source directory with the file name and search it
                    //move the file to destination directory
                    var sourceFile = System.IO.Path.Combine(MarcConfiguration.GetSourceDirectory() + "\\"+ Environment, item.FileName);
                    var destFile = System.IO.Path.Combine(MarcConfiguration.GetDestinationDirectory() + "\\"+ Environment, item.FileName);
                    if (File.Exists(destFile))
                    {
                        File.Delete(destFile);                        
                    }
                    File.Move(sourceFile, destFile);

                }
            }
        }

        private  string CompositeBucketName(Marc marc)
        {
            return S3Configs.MarcBucketName + "/"+ Environment + "/" + marc.ProductNumber;
        }

    }
}
