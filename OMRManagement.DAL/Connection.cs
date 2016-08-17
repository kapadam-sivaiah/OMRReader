/*******************************************************
* Copyright (C) 2016-2020 Manipal Technologies Pvt. Ltd.
* 
 * This file is part of the project OSR and has been exclusively created for internal use of
* Manipal Technologies Pvt. Ltd. or licensed use of clients of Manipal Technologies Pvt. Ltd.
* Under no circumstances, can this file/project could be used individually or as part of
* OSR application can be copied and/or distributed without the express
* permission of Manipal Technologies Pvt. Ltd.
*******************************************************/

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMRManagement.DAL
{
  public  class Connection
    {
        public SqlConnection SqlConn { get; set; }

        public SqlDataAdapter SqlAdap { get; set; }

        public SqlCommand SqlCmd { get; set; }

        public SqlDataReader SqlDr { get; set; }

        public string ConnectionString { get; set; }

        public Connection()
        {

            this.ConnectionString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        }

        public Connection(string connString)
        {
            this.ConnectionString = connString;
        }

        public void OpenConnection()
        {
            if (this.SqlConn != null)
            {
                this.SqlConn.Open();
            }
        }

        public void CloseConnection()
        {
            if (this.SqlConn != null && this.SqlConn.State == ConnectionState.Open)
            {
                this.SqlConn.Close();
            }
        }

    }
}
