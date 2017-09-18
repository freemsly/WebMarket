﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarcEtlContracts;
using MarcETL.AWS;
using MarcETL.Common;
using MarcETL.Model;

namespace MarcETL.AU
{
    public class Processor : MarcProcess<Marc>
    {
        private List<Marc> _filesList;

        public override List<Marc> Extract()
        {
            string[] dirs = Helper.GetMarcExtensionFiles(MarcConfiguration.GetSourceDirectory());
            Console.WriteLine("The number of files having extension .mrc is {0}.", dirs.Length);
            _filesList = new List<Marc>();
            foreach (string file in dirs)
            {
                Console.WriteLine(file);

                var marc = new Marc();
                marc.FileName = Helper.ExtractFileName(file);
                marc.ProductNumber = Helper.ExtractProductNumber(marc.FileName);
                marc.FileLocation = MarcConfiguration.GetSourceDirectory();

                _filesList.Add(marc);
            }
            Console.WriteLine("extracted marc files for AU");
            return _filesList;
        }

        public override void Transfer(List<Marc> filesList)
        {
            //move the files to new location
            Console.WriteLine("moved marc files for AU");

            foreach (var item in filesList)
            {
                //if (File.Exists(MarcConfiguration.GetDestinationDirectory()))
                //{
                //    File.Delete(MarcConfiguration.GetDestinationDirectory());
                //}
                //var sourceFile = System.IO.Path.Combine(MarcConfiguration.GetSourceDirectory(), item.FileName);
                //var destFile = System.IO.Path.Combine(MarcConfiguration.GetDestinationDirectory(), item.FileName);
                //File.Move(sourceFile, destFile);
                //item.IsFileUploaded = true;
                S3MarcManager.UploadFile(item, CompositeBucketName(item));
                item.IsFileUploaded = true;



            }
        }


        public override void UpdateState()
        {
            foreach (var item in _filesList)
            {
                if (item.IsFileUploaded)
                {
                    //update the database that marc is uploaded
                    Console.WriteLine("updated HasMARC for AU");

                    using (var cn = new SqlConnection(MarcConfiguration.GetMarcSqlConnection()))
                    {
                        using (
                            var sqlCommand =
                                new SqlCommand(
                                    //"IF NOT EXISTS(Select 1 from ProductMARC where ProductNumber = @ProdNumber) " +
                                    "IF NOT EXISTS(Select 1 from MarcETL where ProductNumber = @ProdNumber)"+
                                    "BEGIN " +
                                    "INSERT INTO ProductMarc(ProductNumber,HasMARC,MARCFileName,MARCType,UpdatedAt) VALUES (@ProdNumber,1,@filename,'F',GetDate()) " +
                                    "END ", cn))
                        {
                            cn.Open();
                            sqlCommand.Parameters.AddWithValue("@ProdNumber", item.ProductNumber);
                            sqlCommand.Parameters.AddWithValue("@filename", item.FileName);
                            sqlCommand.ExecuteNonQuery();
                            cn.Close();
                        }
                    }
                }
            }
        }

        private static string CompositeBucketName(Marc marc)
        {
            return S3Configs.MarcBucketName + "/AU/" + marc.ProductNumber;
        }

    }
}