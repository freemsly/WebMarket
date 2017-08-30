using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace MarcETL.Model
{
    public class Marc
    {
        public string Isbn { get; set; }

        public string ProductNumber { get; set; }
        public string FileName { get; set; }
        public byte[] File { get; set; }

        public string FileLocation { get; set; }
        
        public bool IsFileUploaded { get; set; }
        
        public override string ToString()
        {
            return this.Isbn + "-" + FileName;
        }


    }
}
