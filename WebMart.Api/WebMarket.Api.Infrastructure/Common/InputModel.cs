// <copyright company="Recorded Books LLC" file="InputModel.cs">
// Copyright © 2013 All Right Reserved
// </copyright>

namespace WebMarket.Api.Infrastructure.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// 
    /// </summary>
    public abstract class InputModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int ErrorCode { get; set; }

        #region ErrorMessages (List<string>)

        private List<string> _ErrorMessages = new List<string>();

    
        /// <summary>
        /// 
        /// </summary>
        public List<string> ErrorMessages
        {
            get { return _ErrorMessages; }
            set
            {
                if (_ErrorMessages != value)
                {
                    _ErrorMessages = value;
                }
            }
        }

        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract bool IsValid();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ComposeErrorMessage()
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;
            foreach (var item in ErrorMessages)
            {
                if (i > 0)
                {
                    sb.Append("; ");
                }
                i++;
                sb.Append(item);
            }
            return sb.ToString();
        }
    }

}
