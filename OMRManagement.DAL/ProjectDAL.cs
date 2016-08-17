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
    public class ProjectDAL
    {
        public static ProjectDetails Add(ProjectDetails project)
        {

            Connection conn = new Connection();
            using (conn.SqlConn = new SqlConnection(conn.ConnectionString))
            {
                conn.OpenConnection();
                try
                {
                    using (conn.SqlCmd = new SqlCommand("SaveProject", conn.SqlConn))
                    {

                        conn.SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                        conn.SqlCmd.Parameters.AddWithValue("@ProjectName", project.ProjectName);
                        conn.SqlCmd.Parameters.AddWithValue("@ProjectCode", project.ProjectCode);

                        SqlParameter outPutParameter = new SqlParameter();
                        outPutParameter.ParameterName = "@ProjectID";
                        outPutParameter.SqlDbType = System.Data.SqlDbType.Int;
                        outPutParameter.Direction = System.Data.ParameterDirection.Output;
                        conn.SqlCmd.Parameters.Add(outPutParameter);
                        conn.SqlCmd.ExecuteNonQuery();

                        if (outPutParameter.Value != null)
                        {
                            project.ProjectID = Convert.ToInt32(outPutParameter.Value.ToString());
                        }

                    }
                    conn.SqlConn.Close();
   
                }
                catch (SqlException ex)
                {
                    project.ErrorMessge = ex.Message;
                }
            }

            return project;
        }
       public static List<ProjectDetails> GetProjects()
        {
            List<ProjectDetails> projectList = null;
            Connection conn = new Connection();
            using (conn.SqlConn = new SqlConnection(conn.ConnectionString))
            {
                conn.OpenConnection();
                try
                {
                    using (conn.SqlCmd = new SqlCommand("GetProject", conn.SqlConn))
                    {

                        conn.SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                       
                        conn.SqlDr = conn.SqlCmd.ExecuteReader();
                        if (conn.SqlDr.HasRows)
                        {
                            projectList = new List<ProjectDetails>();
                            while (conn.SqlDr.Read())
                            {
                                ProjectDetails project = new ProjectDetails();
                                project.ProjectID = Utility.ConvertToInt(conn.SqlDr, "ProjectID", true);
                                project.ProjectName = Utility.ConvertToString(conn.SqlDr, "ProjectName", true);
                                project.ProjectCode = Utility.ConvertToString(conn.SqlDr, "ProjectCode", true);
                                projectList.Add(project);
                            }
                        }
                    }
                    conn.SqlConn.Close();
   
                }
                catch (SqlException ex)
                {
                    
                }

            }

            return projectList;
 
        }
       public static List<ProjectDetails> GetProjectsByID(int projectID)
       {
           List<ProjectDetails> projectList = null;
           Connection conn = new Connection();
           using (conn.SqlConn = new SqlConnection(conn.ConnectionString))
           {
               conn.OpenConnection();
               try
               {
                   using (conn.SqlCmd = new SqlCommand("GetProjectByID", conn.SqlConn))
                   {

                       conn.SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                       conn.SqlCmd.Parameters.AddWithValue("@ProjectID", projectID);
                       conn.SqlDr = conn.SqlCmd.ExecuteReader();
                       if (conn.SqlDr.HasRows)
                       {
                           projectList = new List<ProjectDetails>();
                           while (conn.SqlDr.Read())
                           {
                               ProjectDetails project = new ProjectDetails();
                               project.ProjectID = Utility.ConvertToInt(conn.SqlDr, "ProjectID", true);
                               project.ProjectName = Utility.ConvertToString(conn.SqlDr, "ProjectName", true);
                               project.ProjectCode = Utility.ConvertToString(conn.SqlDr, "ProjectCode", true);
                               projectList.Add(project);
                           }
                       }
                   }
                   conn.SqlConn.Close();

               }
               catch (SqlException ex)
               {

               }

           }

           return projectList;

       }

    }
}
