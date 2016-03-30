using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace SmartPresenter.Data.Common.Local
{
    public class LocalDB
    {
        #region Properties

        private static string _connectionString { get; set; }

        #endregion

        #region Constructor

        static LocalDB()
        {
            //_connectionString = ConfigurationManager.ConnectionStrings["LocalDB"].ToString();
        }

        #endregion

        #region Methods

        public void OpenConnection()
        {
            _connectionString = @"Data Source = E:\Projects\SmartPresenter\Code\Database\SmartPresenterLocalDB.db";
            SQLiteConnection connection = new SQLiteConnection(_connectionString);
            connection.Open();
        }

        #endregion
    }
}
