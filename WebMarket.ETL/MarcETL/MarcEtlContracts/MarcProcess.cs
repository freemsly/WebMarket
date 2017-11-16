using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace MarcEtlContracts
{
    [InheritedExport(typeof(IMarcProcess))]
    public abstract class MarcProcess<T> : IMarcProcess
    {
        public abstract List<T> Extract();
        public abstract void Transfer(List<T> filesList);
        public abstract void UpdateState();

        public abstract void CleanUp();

        public void Process()
        {
            var fileList = Extract();
            Transfer(fileList);
            UpdateState();
            CleanUp();
        }
    }
}
