// <copyright company="Recorded Books Inc" file="MetlManager.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>

namespace WebMarket.Contracts
{
    using System.Diagnostics;
    using WebMarket.Common;

    public sealed class MetlManager
    {

        #region local fields
        private Stopwatch timer = null;
        private MetlTypeOption metlType;



        #endregion

        #region constructors

        public MetlManager(MetlTypeOption option)
        {
            metlType = option;
        }

        #endregion


        public static void Execute()
        {
            
            //MetlManager mgr = new MetlManager(MetlTypeOption.Full);
            //mgr.ExecuteLocal();
        }

        private void ExecuteLocal()
        {
            timer.Start();

            timer.Stop();
        }


    }
}
