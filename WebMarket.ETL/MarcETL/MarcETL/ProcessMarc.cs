using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MarcEtlContracts;

namespace MarcETL
{
    public class ProcessMarc
    {
        [ImportMany]
        public IEnumerable<IMarcProcess> MarcProcesses;
        public void Run()
        {
            var folderPath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + "\\" + "processors";
            DirectoryCatalog dc = new DirectoryCatalog(folderPath);
            AggregateCatalog catalog = new AggregateCatalog(dc);
            CompositionContainer container = new CompositionContainer(catalog);

            container.ComposeParts(this);

            foreach (var marcProcess in MarcProcesses)
            {
                marcProcess.Process();
            }
        }

    }
}
