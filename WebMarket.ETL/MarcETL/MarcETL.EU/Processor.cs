using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarcEtlContracts;

namespace MarcETL.EU
{
    public class Processor : MarcProcess<string>
    {
        public override List<string> Extract()
        {
            //get the files from FTP location
            Console.WriteLine("extracted marc files for EU");
            return new List<string>();
        }

        public override void Transfer(List<string> filesList)
        { 
            //move the files to new location
            Console.WriteLine("moved marc files for EU"); throw new NotImplementedException();
        }
        

        public override void UpdateState()
        {
            //update the database that marc is uploaded
            Console.WriteLine("updated ismarc for EU");
        }

        
    }
}
