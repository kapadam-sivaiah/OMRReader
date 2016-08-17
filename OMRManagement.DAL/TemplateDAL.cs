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
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMRManagement.Entities;

namespace OMRManagement.DAL
{
    public class TemplateDAL
    {
        public static bool Add(string templateXML)
        {

            Connection conn = new Connection();
            using (conn.SqlConn = new SqlConnection(conn.ConnectionString))
            {
                conn.OpenConnection();
                try
                {
                    using (conn.SqlCmd = new SqlCommand("SaveTemplate", conn.SqlConn))
                    {

                        conn.SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                        conn.SqlCmd.Parameters.AddWithValue("@TemplaTeXML", templateXML);
                        conn.SqlCmd.ExecuteNonQuery();
                    }
                    conn.SqlConn.Close();
                }
                catch (SqlException ex)
                {
                    return false;
                }
            }


            return true;
        }


        public static List<TemplateMaster> GetProjectTemplates(int projectID)
        {
            List<TemplateMaster> templateList = null;
            Connection conn = new Connection();
            using (conn.SqlConn = new SqlConnection(conn.ConnectionString))
            {
                conn.OpenConnection();
                try
                {
                    using (conn.SqlCmd = new SqlCommand("GetTemplate", conn.SqlConn))
                    {

                        conn.SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                        conn.SqlCmd.Parameters.AddWithValue("@ProjectID", projectID);
                        conn.SqlDr = conn.SqlCmd.ExecuteReader();
                        if (conn.SqlDr.HasRows)
                        {
                            templateList = new List<TemplateMaster>();
                            while (conn.SqlDr.Read())
                            {
                                TemplateMaster template = new TemplateMaster();
                                template.TemplateID = Utility.ConvertToInt(conn.SqlDr, "TemplateID", true);
                                template.TemplateName = Utility.ConvertToString(conn.SqlDr, "TemplateName", true);
                                template.TemplateCode = Utility.ConvertToString(conn.SqlDr, "TemplateCode", true);
                                template.TemplateFileName = Utility.ConvertToString(conn.SqlDr, "TemplateFileName", true);
                                template.TemplateFilePath = Utility.ConvertToString(conn.SqlDr, "TemplateFilePath", true);
                                template.TrackCount = Utility.ConvertToInt(conn.SqlDr, "TrackCount", true);
                                template.TrackIndex = Utility.ConvertToString(conn.SqlDr, "TemplateIndex", true);
                                template.TrackLeft = Utility.ConvertToInt(conn.SqlDr, "TemplateLeft", true);
                                template.TrackTop = Utility.ConvertToInt(conn.SqlDr, "TemplateTop", true);
                                template.TemplateImage = Utility.ConvertToString(conn.SqlDr, "TemplateImage", true);
                                template.IsDuplex = Utility.ConvertToBoolean(conn.SqlDr, "IsDuplex", true);
                                template.AllowedErrorCharCount = Utility.ConvertToInt(conn.SqlDr, "AllowedErrorCharCount", true);
                                template.Page = (PageType)Utility.ConvertToInt(conn.SqlDr, "PageType", true);
                                // template.DriveID = Utility.ConvertToBoolean(conn.SqlDr, "TemplateImage", true);
                                templateList.Add(template);
                            }
                        }
                    }
                    conn.SqlConn.Close();

                }
                catch (SqlException ex)
                {

                }

            }

            return templateList;

        }


        public static TemplateMaster GetTemplateByID(int templateID)
        {
            TemplateMaster template = null;
            Connection conn = new Connection();
            using (conn.SqlConn = new SqlConnection(conn.ConnectionString))
            {
                conn.OpenConnection();
                try
                {
                    using (conn.SqlCmd = new SqlCommand("GetTemplateByID", conn.SqlConn))
                    {

                        conn.SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                        conn.SqlCmd.Parameters.AddWithValue("@TemplateID", templateID);
                        conn.SqlDr = conn.SqlCmd.ExecuteReader();
                        if (conn.SqlDr.HasRows)
                        {
                            template = new TemplateMaster();
                            while (conn.SqlDr.Read())
                            {
                                template.TemplateID = Utility.ConvertToInt(conn.SqlDr, "TemplateID", true);
                                template.TemplateName = Utility.ConvertToString(conn.SqlDr, "TemplateName", true);
                                template.TemplateCode = Utility.ConvertToString(conn.SqlDr, "TemplateCode", true);
                                template.TemplateFileName = Utility.ConvertToString(conn.SqlDr, "TemplateFileName", true);
                                template.TemplateFilePath = Utility.ConvertToString(conn.SqlDr, "TemplateFilePath", true);
                                template.TrackCount = Utility.ConvertToInt(conn.SqlDr, "TrackCount", true);
                                template.TrackIndex = Utility.ConvertToString(conn.SqlDr, "TemplateIndex", true);
                                template.TrackLeft = Utility.ConvertToInt(conn.SqlDr, "TemplateLeft", true);
                                template.TrackTop = Utility.ConvertToInt(conn.SqlDr, "TemplateTop", true);
                                template.TemplateImage = Utility.ConvertToString(conn.SqlDr, "TemplateImage", true);
                                template.IsDuplex = Utility.ConvertToBoolean(conn.SqlDr, "IsDuplex", true);
                                template.AllowedErrorCharCount = Utility.ConvertToInt(conn.SqlDr, "AllowedErrorCharCount", true);
                                template.Page = (PageType)Utility.ConvertToInt(conn.SqlDr, "PageType", true);
                                // template.DriveID = Utility.ConvertToBoolean(conn.SqlDr, "TemplateImage", true);

                            }

                            conn.SqlDr.NextResult();
                            while (conn.SqlDr.Read())
                            {
                                TemplateField field = new TemplateField();
                                field.FieldID = Utility.ConvertToInt(conn.SqlDr, "FieldID", true);
                                field.FieldName = Utility.ConvertToString(conn.SqlDr, "FieldName", true);
                                field.FieldIndex = Utility.ConvertToString(conn.SqlDr, "FieldIndex", true);
                                // field.FieldRowNo = Utility.ConvertToInt(conn.SqlDr, "FieldRowNo", true);
                                field.FieldTopPosition = Utility.ConvertToInt(conn.SqlDr, "FieldTop", true);
                                field.FieldLeftPosition = Utility.ConvertToInt(conn.SqlDr, "FieldLeft", true);
                                field.FieldColumnNo = Utility.ConvertToInt(conn.SqlDr, "NoColumns", true);
                                field.FieldRowNo = Utility.ConvertToInt(conn.SqlDr, "NoRows", true);
                                field.StartIndex = Utility.ConvertToInt(conn.SqlDr, "StartIndex", true);
                                field.TemplateFieldType = (FieldType)Enum.Parse(typeof(FieldType), Utility.ConvertToString(conn.SqlDr, "FieldType", true));
                                field.TrackRowNo = Utility.ConvertToInt(conn.SqlDr, "StartRowNo", true);
                                field.BubbleWidth = Utility.ConvertToInt(conn.SqlDr, "BubbleWidth", true);
                                field.BubbleHeight = Utility.ConvertToInt(conn.SqlDr, "BubbleHeight", true);
                                field.BubbleRowGap = Utility.ConvertToInt(conn.SqlDr, "BubbleRowGap", true);
                                field.BubbleColumnGap = Utility.ConvertToInt(conn.SqlDr, "BubbleColumnGap", true);
                                field.AnnotationHeight = Utility.ConvertToInt(conn.SqlDr, "AnnotationHeight", true);
                                field.AnnotationWidth = Utility.ConvertToInt(conn.SqlDr, "AnnotationWidth", true);
                                template.FieldList.Add(field);
                            }


                            conn.SqlDr.NextResult();
                            while (conn.SqlDr.Read())
                            {
                                FrontBackPageMapper mapper = new FrontBackPageMapper();
                                mapper.FrontPageTemplateID = Utility.ConvertToInt(conn.SqlDr, "FrontTemplateID", true);
                                mapper.BackPageTemplateID = Utility.ConvertToInt(conn.SqlDr, "BackTemplateID", true);
                                template.FrontBackTemplateMapperList.Add(mapper);                            }


                        }
                    }
                    conn.SqlConn.Close();


                }
                catch (SqlException ex)
                {

                }
                return template;
            }
        }
       


        public static TemplateMaster GetBackTemplateByFrontTemplateID(int templateID)
        {
            TemplateMaster template = null;
            Connection conn = new Connection();
            using (conn.SqlConn = new SqlConnection(conn.ConnectionString))
            {
                conn.OpenConnection();
                try
                {
                    using (conn.SqlCmd = new SqlCommand("[GetBackTemplateByID]", conn.SqlConn))
                    {

                        conn.SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                        conn.SqlCmd.Parameters.AddWithValue("@TemplateID", templateID);
                        conn.SqlDr = conn.SqlCmd.ExecuteReader();
                        if (conn.SqlDr.HasRows)
                        {
                            template = new TemplateMaster();
                            while (conn.SqlDr.Read())
                            {
                                template.TemplateID = Utility.ConvertToInt(conn.SqlDr, "TemplateID", true);
                                template.TemplateName = Utility.ConvertToString(conn.SqlDr, "TemplateName", true);
                                template.TemplateCode = Utility.ConvertToString(conn.SqlDr, "TemplateCode", true);
                                template.TemplateFileName = Utility.ConvertToString(conn.SqlDr, "TemplateFileName", true);
                                template.TemplateFilePath = Utility.ConvertToString(conn.SqlDr, "TemplateFilePath", true);
                                template.TrackCount = Utility.ConvertToInt(conn.SqlDr, "TrackCount", true);
                                template.TrackIndex = Utility.ConvertToString(conn.SqlDr, "TemplateIndex", true);
                                template.TrackLeft = Utility.ConvertToInt(conn.SqlDr, "TemplateLeft", true);
                                template.TrackTop = Utility.ConvertToInt(conn.SqlDr, "TemplateTop", true);
                                template.TemplateImage = Utility.ConvertToString(conn.SqlDr, "TemplateImage", true);
                                template.IsDuplex = Utility.ConvertToBoolean(conn.SqlDr, "IsDuplex", true);
                                template.AllowedErrorCharCount = Utility.ConvertToInt(conn.SqlDr, "AllowedErrorCharCount", true);
                                template.Page = (PageType)Utility.ConvertToInt(conn.SqlDr, "PageType", true);
                                // template.DriveID = Utility.ConvertToBoolean(conn.SqlDr, "TemplateImage", true);

                            }

                            conn.SqlDr.NextResult();
                            while (conn.SqlDr.Read())
                            {
                                TemplateField field = new TemplateField();
                                field.FieldID = Utility.ConvertToInt(conn.SqlDr, "FieldID", true);
                                field.FieldName = Utility.ConvertToString(conn.SqlDr, "FieldName", true);
                                field.FieldIndex = Utility.ConvertToString(conn.SqlDr, "FieldIndex", true);
                                // field.FieldRowNo = Utility.ConvertToInt(conn.SqlDr, "FieldRowNo", true);
                                field.FieldTopPosition = Utility.ConvertToInt(conn.SqlDr, "FieldTop", true);
                                field.FieldLeftPosition = Utility.ConvertToInt(conn.SqlDr, "FieldLeft", true);
                                field.FieldColumnNo = Utility.ConvertToInt(conn.SqlDr, "NoColumns", true);
                                field.FieldRowNo = Utility.ConvertToInt(conn.SqlDr, "NoRows", true);
                                field.StartIndex = Utility.ConvertToInt(conn.SqlDr, "StartIndex", true);
                                field.TemplateFieldType = (FieldType)Enum.Parse(typeof(FieldType), Utility.ConvertToString(conn.SqlDr, "FieldType", true));
                                field.TrackRowNo = Utility.ConvertToInt(conn.SqlDr, "StartRowNo", true);
                                field.BubbleWidth = Utility.ConvertToInt(conn.SqlDr, "BubbleWidth", true);
                                field.BubbleHeight = Utility.ConvertToInt(conn.SqlDr, "BubbleHeight", true);
                                field.BubbleRowGap = Utility.ConvertToInt(conn.SqlDr, "BubbleRowGap", true);
                                field.BubbleColumnGap = Utility.ConvertToInt(conn.SqlDr, "BubbleColumnGap", true);
                                field.AnnotationHeight = Utility.ConvertToInt(conn.SqlDr, "AnnotationHeight", true);
                                field.AnnotationWidth = Utility.ConvertToInt(conn.SqlDr, "AnnotationWidth", true);
                                template.FieldList.Add(field);
                            }

                                                     


                        }
                    }
                    conn.SqlConn.Close();


                }
                catch (SqlException ex)
                {

                }
                return template;
            }
        }


        public static List<TemplateMaster> GetFrontPageTemplates(int projectID)
        {
            List<TemplateMaster> templateList = null;
            Connection conn = new Connection();
            using (conn.SqlConn = new SqlConnection(conn.ConnectionString))
            {
                conn.OpenConnection();
                try
                {
                    using (conn.SqlCmd = new SqlCommand("[GetFrontPageTemplate]", conn.SqlConn))
                    {

                        conn.SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                        conn.SqlCmd.Parameters.AddWithValue("@ProjectID", projectID);
                        conn.SqlDr = conn.SqlCmd.ExecuteReader();
                        if (conn.SqlDr.HasRows)
                        {
                            templateList = new List<TemplateMaster>();
                            while (conn.SqlDr.Read())
                            {
                                TemplateMaster template = new TemplateMaster();
                                template.TemplateID = Utility.ConvertToInt(conn.SqlDr, "TemplateID", true);
                                template.TemplateName = Utility.ConvertToString(conn.SqlDr, "TemplateName", true);
                                templateList.Add(template);
                            }
                        }
                    }
                    conn.SqlConn.Close();

                }
                catch (SqlException ex)
                {

                }

            }

            return templateList;



        }



        public static bool Update(string templateXML)
        {
            Connection conn = new Connection();
            using (conn.SqlConn = new SqlConnection(conn.ConnectionString))
            {
                conn.OpenConnection();
                try
                {
                    using (conn.SqlCmd = new SqlCommand("UpdateTemplate", conn.SqlConn))
                    {

                        conn.SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                        conn.SqlCmd.Parameters.AddWithValue("@TemplaTeXML", templateXML);
                        conn.SqlCmd.ExecuteNonQuery();
                    }
                    conn.SqlConn.Close();
                }
                catch (SqlException ex)
                {
                    return false;
                }
            }


            return true;
        }
    }
}
