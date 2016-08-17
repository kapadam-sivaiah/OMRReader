/*******************************************************
* Copyright (C) 2016-2020 Manipal Technologies Pvt. Ltd.
* 
 * This file is part of the project OSR and has been exclusively created for internal use of
* Manipal Technologies Pvt. Ltd. or licensed use of clients of Manipal Technologies Pvt. Ltd.
* Under no circumstances, can this file/project could be used individually or as part of
* OSR application can be copied and/or distributed without the express
* permission of Manipal Technologies Pvt. Ltd.
*******************************************************/

using OMRManagement.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMRManagement.DAL
{
    public class SheetValuesDAL
    {
        public static Sheet Add(Sheet sheet)
        {

            Connection conn = new Connection();
            using (conn.SqlConn = new SqlConnection(conn.ConnectionString))
            {
                conn.OpenConnection();
                try
                {
                    using (conn.SqlCmd = new SqlCommand("SaveSheetValues", conn.SqlConn))
                    {

                        conn.SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                        conn.SqlCmd.Parameters.AddWithValue("@TaskID", sheet.TaskID);
                        conn.SqlCmd.Parameters.AddWithValue("@SheetName", sheet.SheetName);
                        conn.SqlCmd.Parameters.AddWithValue("@SheetValues", sheet.SheetValues);
                        conn.SqlCmd.Parameters.AddWithValue("@ImagePath", sheet.ImagePath);
                        conn.SqlCmd.Parameters.AddWithValue("@HasError", sheet.HasError);
                        conn.SqlCmd.Parameters.AddWithValue("@ExceptionMessage", sheet.ExceptionMessage);
                        conn.SqlCmd.ExecuteNonQuery();

                    }
                    conn.SqlConn.Close();
                }
                catch (SqlException ex)
                {
                    sheet.ExceptionMessage = ex.Message;
                }
            }

            return sheet;

        }

        public static bool AddFileStatus(string FileStatusXML)
        {
            
            Connection conn = new Connection();
            using (conn.SqlConn = new SqlConnection(conn.ConnectionString))
            {
                conn.OpenConnection();
                try
                {
                    using (conn.SqlCmd = new SqlCommand("SaveFileStatus", conn.SqlConn))
                    {

                        //conn.SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                        ////conn.SqlCmd.Parameters.AddWithValue("@TaskID", status.TaskID);
                        ////conn.SqlCmd.Parameters.AddWithValue("@FileName", status.FileName);
                        ////conn.SqlCmd.Parameters.AddWithValue("@FilePath", status.FilePath);
                        ////conn.SqlCmd.Parameters.AddWithValue("@Status", status.Status);
                       
                        ////conn.SqlCmd.Parameters.AddWithValue("@ErrorMessage", status.ErrorMessage);
                        ////conn.SqlCmd.Parameters.AddWithValue("@RunType", status.RunType.ToString());
                        ////conn.SqlCmd.Parameters.AddWithValue("@IsDelete", status.IsDelete);
                        //conn.SqlCmd.ExecuteNonQuery();
                        conn.SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                        conn.SqlCmd.Parameters.AddWithValue("@FileStatusXML", FileStatusXML);
                        conn.SqlCmd.ExecuteNonQuery();

                    }
                    conn.SqlConn.Close();
                }
                catch (SqlException ex)
                {
                  //  status.ExceptionMessage = ex.Message;
                    return false;
                }
            }

            return true;

        }
        public static DataSet GetSheetValues(int ProjectID)
        {

            Connection conn = new Connection();
            DataSet ds = new DataSet();
            using (conn.SqlConn = new SqlConnection(conn.ConnectionString))
            {
                conn.OpenConnection();
                try
                {
                    using (conn.SqlCmd = new SqlCommand("GetSheetValues", conn.SqlConn))
                    {
                        conn.SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                        conn.SqlCmd.Parameters.AddWithValue("@ProjectID", ProjectID);
                        conn.SqlAdap = new SqlDataAdapter(conn.SqlCmd);
                        conn.SqlAdap.Fill(ds);
                        conn.SqlConn.Close();
                    }
                }
                catch (SqlException ex)
                {
                    //sheet.ExceptionMessage = ex.Message;
                }


            }
            return ds;
        }



        public static List<string> GetReProcessValues(int JobID)
        {

            Connection conn = new Connection();
            List<string> ReProcessValues = new List<string>();
            using (conn.SqlConn = new SqlConnection(conn.ConnectionString))
            {
                conn.OpenConnection();
                try
                {
                    using (conn.SqlCmd = new SqlCommand("GetReProcessValues", conn.SqlConn))
                    {
                        conn.SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                        conn.SqlCmd.Parameters.AddWithValue("@JobID", JobID);
                        conn.SqlDr = conn.SqlCmd.ExecuteReader();
                         conn.SqlDr.NextResult();
                         while (conn.SqlDr.Read())
                         {
                             ReProcessValues.Add(Utility.ConvertToString(conn.SqlDr, "ImagePath", true));
                         }
                    }
                }
                catch (SqlException ex)
                {
                    //sheet.ExceptionMessage = ex.Message;
                }


            }
            return ReProcessValues;
        }
        public static List<string> GetRecoveryValues(int JobID)
        {

            Connection conn = new Connection();
            List<string> recoveryValues = new List<string>();
            using (conn.SqlConn = new SqlConnection(conn.ConnectionString))
            {
                conn.OpenConnection();
                try
                {
                    using (conn.SqlCmd = new SqlCommand("GetRecoverValues", conn.SqlConn))
                    {
                        conn.SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                        conn.SqlCmd.Parameters.AddWithValue("@JobID", JobID);
                        conn.SqlDr = conn.SqlCmd.ExecuteReader();                        
                        while (conn.SqlDr.Read())
                        {
                            recoveryValues.Add(Utility.ConvertToString(conn.SqlDr, "ImagePath", true));
                        }
                    }
                }
                catch (SqlException ex)
                {
                    //sheet.ExceptionMessage = ex.Message;
                }


            }
            return recoveryValues;
        }
     
        public static List<string> GetErrorValues(int JobID)
        {

            Connection conn = new Connection();
            List<string> ErrorValues = new List<string>();
            using (conn.SqlConn = new SqlConnection(conn.ConnectionString))
            {
                conn.OpenConnection();
                try
                {
                    using (conn.SqlCmd = new SqlCommand("GetErrorValues", conn.SqlConn))
                    {
                        conn.SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                        conn.SqlCmd.Parameters.AddWithValue("@JobID", JobID);
                        conn.SqlDr = conn.SqlCmd.ExecuteReader();
                        conn.SqlDr.NextResult();
                        while (conn.SqlDr.Read())
                        {
                            ErrorValues.Add(Utility.ConvertToString(conn.SqlDr, "ImagePath", true));
                        }
                        
                    }
                }
                catch (SqlException ex)
                {
                    //sheet.ExceptionMessage = ex.Message;
                }


            }
            return ErrorValues;
        }

        public static void UpdateIsDelete(int JobID)
        {

            Connection conn = new Connection();
           // List<string> ErrorValues = new List<string>();
            using (conn.SqlConn = new SqlConnection(conn.ConnectionString))
            {
                conn.OpenConnection();
                try
                {
                    using (conn.SqlCmd = new SqlCommand("[UpdateIsDelete]", conn.SqlConn))
                    {
                        conn.SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                        conn.SqlCmd.Parameters.AddWithValue("@JobID", JobID);
                        conn.SqlCmd.ExecuteNonQuery();

                    }
                }
                catch (SqlException ex)
                {
                    //sheet.ExceptionMessage = ex.Message;
                }


            }
           // return ErrorValues;
        }
        public static void UpdateTaskEndDate(int taskID)
        {

            Connection conn = new Connection();
            // List<string> ErrorValues = new List<string>();
            using (conn.SqlConn = new SqlConnection(conn.ConnectionString))
            {
                conn.OpenConnection();
                try
                {
                    using (conn.SqlCmd = new SqlCommand("[UpdateTaskEndDate]", conn.SqlConn))
                    {
                        conn.SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                        conn.SqlCmd.Parameters.AddWithValue("@TaskID", taskID);
                        conn.SqlCmd.ExecuteNonQuery();

                    }
                }
                catch (SqlException ex)
                {
                    //sheet.ExceptionMessage = ex.Message;
                }


            }
        }
             public static void UpdateTaskStarDate(int taskID)
        {

            Connection conn = new Connection();
            // List<string> ErrorValues = new List<string>();
            using (conn.SqlConn = new SqlConnection(conn.ConnectionString))
            {
                conn.OpenConnection();
                try
                {
                    using (conn.SqlCmd = new SqlCommand("[UpdateTaskStartDate]", conn.SqlConn))
                    {
                        conn.SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                        conn.SqlCmd.Parameters.AddWithValue("@TaskID", taskID);
                        conn.SqlCmd.ExecuteNonQuery();

                    }
                }
                catch (SqlException ex)
                {
                    //sheet.ExceptionMessage = ex.Message;
                }


            }
            // return ErrorValues;
        }
    }
}
