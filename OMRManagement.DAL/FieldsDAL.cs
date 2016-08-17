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
 public   class FieldsDAL
    {
     public static List<TemplateField> GetTemplateFields(int templateID)
     {
         List<TemplateField> fieldList = null;
         Connection conn = new Connection();
         using (conn.SqlConn = new SqlConnection(conn.ConnectionString))
         {
             conn.OpenConnection();
             try
             {
                 using (conn.SqlCmd = new SqlCommand("GetTemplateFields", conn.SqlConn))
                 {

                     conn.SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                     conn.SqlCmd.Parameters.AddWithValue("@TemplateID", templateID);
                     conn.SqlDr = conn.SqlCmd.ExecuteReader();
                     if (conn.SqlDr.HasRows)
                     {
                         fieldList = new List<TemplateField>();
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
                             //field.TemplateFieldType = Utility.ConvertToString(conn.SqlDr, "FieldType", true);
                             field.TrackRowNo = Utility.ConvertToInt(conn.SqlDr, "StartRowNo", true);
                             field.BubbleWidth = Utility.ConvertToInt(conn.SqlDr, "BubbleWidth", true);
                             field.BubbleHeight = Utility.ConvertToInt(conn.SqlDr, "BubbleHeight", true);
                             field.BubbleRowGap = Utility.ConvertToInt(conn.SqlDr, "BubbleRowGap", true);
                             field.BubbleColumnGap = Utility.ConvertToInt(conn.SqlDr, "BubbleColumnGap", true);
                             field.AnnotationHeight = Utility.ConvertToInt(conn.SqlDr, "AnnotationHeight", true);
                             field.AnnotationWidth = Utility.ConvertToInt(conn.SqlDr, "AnnotationWidth", true);
                             // template.DriveID = Utility.ConvertToBoolean(conn.SqlDr, "TemplateImage", true);
                             fieldList.Add(field);
                         }
                     }
                 }
                 conn.SqlConn.Close();

             }
             catch (SqlException ex)
             {

             }

         }

         return fieldList;

     }
     public static bool DeleteFieldID(int fieldID)
     {
         Connection conn = new Connection();
         using (conn.SqlConn = new SqlConnection(conn.ConnectionString))
         {
             conn.OpenConnection();
             try
             {
                 using (conn.SqlCmd = new SqlCommand("DeleteFieldID", conn.SqlConn))
                 {

                     conn.SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                     conn.SqlCmd.Parameters.AddWithValue("@FieldID", fieldID);
                     conn.SqlCmd.ExecuteNonQuery();
                 }
             }
             catch
             {
             }
             return true;
         }

     }
    }
}
