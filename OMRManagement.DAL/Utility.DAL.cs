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
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace OMRManagement.DAL
{
    public class Utility
    {
        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        public static DataTable ConvertToDatatable<T>(List<T> data)
        {
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    table.Columns.Add(prop.Name, prop.PropertyType.GetGenericArguments()[0]);
                else
                    table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }
        public static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }
        


        public static int ConvertToInt(IDataReader dr, string column, bool assignZeroAsDefault, int defaultValue = 0)
        {
            return Convert.ToInt32(dr[column] != DBNull.Value &&
                !String.IsNullOrEmpty(Convert.ToString(dr[column])) ? Convert.ToString(dr[column]) : (assignZeroAsDefault ? "0" : defaultValue.ToString()));
        }

        public static string ConvertToString(IDataReader dr, string column, bool assignEmptyAsDefault, string defaultValue = "")
        {
            return Convert.ToString(dr[column] != DBNull.Value &&
                !String.IsNullOrEmpty(Convert.ToString(dr[column])) ? Convert.ToString(dr[column]) : (assignEmptyAsDefault ? string.Empty : defaultValue));
        }

        public static decimal ConvertToDecimal(IDataReader dr, string column, bool assignZeroAsDefault, decimal defaultValue = 0)
        {
            return Convert.ToDecimal(dr[column] != DBNull.Value &&
                !String.IsNullOrEmpty(Convert.ToString(dr[column])) ? Convert.ToString(dr[column]) : (assignZeroAsDefault ? "0" : defaultValue.ToString()));
        }
        public static Guid ConverToGuid(IDataReader dr, string column, bool assignEmptyAsDefault, string defaultValue = "")
        {
            return Guid.Parse(dr[column] != DBNull.Value &&
                !string.IsNullOrEmpty(Convert.ToString(dr[column])) ? Convert.ToString(dr[column]) : (assignEmptyAsDefault ? string.Empty : defaultValue));
        }
        public static bool ConvertToBoolean(IDataReader dr, string column, bool assignFalseAsDefault, bool defaultValue = false)
        {
            return Convert.ToBoolean(dr[column] != DBNull.Value &&
                !String.IsNullOrEmpty(Convert.ToString(dr[column])) ? Convert.ToString(dr[column]) : (assignFalseAsDefault ? "false" : defaultValue.ToString()));
        }

        public static DateTime ConvertToDateTime(IDataReader dr, string column, bool assignMinValueAsDefault, DateTime defaultValue = default(DateTime))
        {
            return Convert.ToDateTime(dr[column] != DBNull.Value &&
                !String.IsNullOrEmpty(Convert.ToString(dr[column])) ? Convert.ToString(dr[column]) : (assignMinValueAsDefault ? DateTime.MinValue.ToString("dd/MM/yyyy") : defaultValue.ToString("dd/MM/yyyy")));
        }
        public static TimeSpan ConverToTimeSpan(IDataReader dr, string column, bool assignMinValueAsDefault, TimeSpan defaultValue = default(TimeSpan))
        {
            return TimeSpan.Parse(dr[column] != DBNull.Value &&
                           !String.IsNullOrEmpty(Convert.ToString(dr[column])) ? Convert.ToString(dr[column]) : (assignMinValueAsDefault ? TimeSpan.MinValue.ToString() : defaultValue.ToString()));
        }
        public static bool ConvertToBooleanFromInt(IDataReader dr, string column, bool assignFalseAsDefault, int defaultValue = 0)
        {
            return Convert.ToBoolean(dr[column] != DBNull.Value &&
                !String.IsNullOrEmpty(Convert.ToString(dr[column])) ? Convert.ToInt32(dr[column]) : (assignFalseAsDefault ? 0 : defaultValue));
        }
    }
}
