using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarcETL
{
    class Program
    {
        static void Main(string[] args)
        {
            ProcessMarc processMarc = new ProcessMarc();
            processMarc.Run();
        }
    }
}
