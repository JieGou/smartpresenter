using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPresenter.Data.Common.Cloud.Sync
{
    public static class CloudDB
    {
        #region Properties

        public static string ConnectionString {get;set;}

        #endregion

        #region Constructor

        static CloudDB()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["CloudDB"].ToString();
        }

        #endregion

        #region Methods

        #endregion
    }
}
