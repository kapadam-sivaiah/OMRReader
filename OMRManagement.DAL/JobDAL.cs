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
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMRManagement.Entities;
namespace OMRManagement.DAL
{
  public class JobDAL
    {
      public static Job  Add(string jobXML)
      {
          Job job = new Job();
          Connection conn = new Connection();
          using (conn.SqlConn = new SqlConnection(conn.ConnectionString))
          {
              conn.OpenConnection();            
              try
              {
                  using (conn.SqlCmd = new SqlCommand("SaveJob", conn.SqlConn))
                  {
                     

                      conn.SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                      conn.SqlCmd.Parameters.AddWithValue("@JobXML", jobXML);

                      SqlParameter jobID = new SqlParameter("@JobID", System.Data.SqlDbType.Int,5) { Direction = System.Data.ParameterDirection.Output };
                      conn.SqlCmd.Parameters.Add(jobID);
                      SqlParameter errorMessage = new SqlParameter("@ErrorMessage", System.Data.SqlDbType.NVarChar,500) { Direction = System.Data.ParameterDirection.Output };
                      conn.SqlCmd.Parameters.Add(errorMessage);                   


                      conn.SqlCmd.ExecuteNonQuery();
                      if (jobID.Value != null)
                      {
                          job.JobID = Convert.ToInt32(jobID.Value.ToString());
                      }
                      if (errorMessage.Value != null)
                      {
                          job.ErrorMessge = errorMessage.Value.ToString();
                      }

                  }
                  conn.SqlConn.Close();
              }
              catch (SqlException ex)
              {
                  job.ErrorMessge = ex.Message;
              }
          }
          return job;
      }
      public static Job GetJobByID(int jobID)
      {
          //JobComposite jobComposite = null;
          Job job = null;
          Connection conn = new Connection();
          using (conn.SqlConn = new SqlConnection(conn.ConnectionString))
          {
              conn.OpenConnection();
              try
              {
                  using (conn.SqlCmd = new SqlCommand("GetJobByID", conn.SqlConn))
                  {

                      conn.SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                      conn.SqlCmd.Parameters.AddWithValue("@JobID", jobID);
                      conn.SqlDr = conn.SqlCmd.ExecuteReader();
                      if (conn.SqlDr.HasRows)
                      {
                          //Jobs
                          job = new Job();
                          while (conn.SqlDr.Read())
                          {
                              job.JobID = Utility.ConvertToInt(conn.SqlDr, "JobID", true);
                              job.JobStartDate = Utility.ConvertToDateTime(conn.SqlDr, "JobStartDate", true);
                              job.JobEndDate = Utility.ConvertToDateTime(conn.SqlDr, "JobStartDate", true);
                              job.JobStatus = (Status)Enum.Parse(typeof(Status), Utility.ConvertToString(conn.SqlDr, "JobStatus", true));
                              job.Type = (RunType)Enum.Parse(typeof(RunType), Utility.ConvertToString(conn.SqlDr, "RunType", true));
                              job.BasePath = Utility.ConvertToString(conn.SqlDr, "BasePath", true);
                              job.Threshold = Utility.ConvertToInt(conn.SqlDr, "Threshold", true);
                              job.Project.ProjectID = Utility.ConvertToInt(conn.SqlDr, "ProjectID", true);                             

                          }
                          //Tasks
                          conn.SqlDr.NextResult();
                          while (conn.SqlDr.Read())
                          {
                              ReadTask task = new ReadTask();
                              task.FolderPath = Utility.ConvertToString(conn.SqlDr, "FolderPath", true);
                              task.TaskID = Utility.ConvertToInt(conn.SqlDr, "TaskID", true);
                              task.TaskStartDate = Utility.ConvertToDateTime(conn.SqlDr, "StartDate", true);
                              task.TotalFileCount = Utility.ConvertToInt(conn.SqlDr, "TotalFileCount", true);
                              task.TaskEndDate = Utility.ConvertToDateTime(conn.SqlDr, "EndDate", true);
                              task.MatchingTemplate.TemplateID = Utility.ConvertToInt(conn.SqlDr, "TemplateID", true);
                              task.Job.JobID = Utility.ConvertToInt(conn.SqlDr, "JobID", true);
                              job.TaskList.Add(task);
                          }

                      }
                  }
                  conn.SqlConn.Close();


              }
              catch (SqlException ex)
              {

              }
             
          }
          return job;
      }

      public static Job Update(string jobXML)
      {
          Job job = new Job();
          Connection conn = new Connection();
          using (conn.SqlConn = new SqlConnection(conn.ConnectionString))
          {
              conn.OpenConnection();
              try
              {
                  using (conn.SqlCmd = new SqlCommand("UpdateJob", conn.SqlConn))
                  {

                      conn.SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                      conn.SqlCmd.Parameters.AddWithValue("@UpdateJobXML", jobXML);
                      conn.SqlCmd.ExecuteNonQuery();
                  }
                  conn.SqlConn.Close();
              }
              catch (SqlException ex)
              {
                  //return false;
              }
          }
          return job;
      }
      public static void UpdateJobEndDate(int JobID)
      {
         
          Connection conn = new Connection();
          using (conn.SqlConn = new SqlConnection(conn.ConnectionString))
          {
              conn.OpenConnection();
              try
              {
                  using (conn.SqlCmd = new SqlCommand("[UpdateJobEndDate]", conn.SqlConn))
                  {

                      conn.SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                      conn.SqlCmd.Parameters.AddWithValue("@JobID", JobID);
                      conn.SqlCmd.ExecuteNonQuery();
                  }
                  conn.SqlConn.Close();
              }
              catch (SqlException ex)
              {
                  //return false;
              }
          }
         
      }
      public static int AddTask(int JobID,int templateID,int totalfileCount,RunType runType)
      {
          int taskID=0;
          Connection conn = new Connection();

          using (conn.SqlConn = new SqlConnection(conn.ConnectionString))
          {
              conn.OpenConnection();
              try
              {
                  using (conn.SqlCmd = new SqlCommand("[SaveTask]", conn.SqlConn))
                  {

                      conn.SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                      conn.SqlCmd.Parameters.AddWithValue("@JobID", JobID);
                      conn.SqlCmd.Parameters.AddWithValue("@TemplateID", templateID);
                      conn.SqlCmd.Parameters.AddWithValue("@totalfileCount", totalfileCount);
                      conn.SqlCmd.Parameters.AddWithValue("@RunType", runType);

                      SqlParameter outPutParameter = new SqlParameter();
                      outPutParameter.ParameterName = "@TaskID";
                      outPutParameter.SqlDbType = System.Data.SqlDbType.Int;
                      outPutParameter.Direction = System.Data.ParameterDirection.Output;
                      conn.SqlCmd.Parameters.Add(outPutParameter);
                      conn.SqlCmd.ExecuteNonQuery();

                      if (outPutParameter.Value != null)
                      {
                          taskID= Convert.ToInt32(outPutParameter.Value.ToString());
                      }
                      
                  }
                  conn.SqlConn.Close();
              }
              catch (SqlException ex)
              {
                  //return false;
              }
             
          }
          return taskID;
      }
      
    }
}
