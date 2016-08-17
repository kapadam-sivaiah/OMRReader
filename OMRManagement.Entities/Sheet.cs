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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMRManagement.Entities
{
   public class Sheet
    {       
        public int SheetID { get; set; }
        public string SheetName { get; set; }
        public string ImagePath { get; set; }
        public string SheetValues { get; set; }
        public int TaskID { get; set; }
        public bool HasError { get; set; }
        public string ExceptionMessage { get; set; }
        public Sheet(string sheetName, string imagePath, string sheetValues, int taskID, string exceptionMessage,bool hasError )
        {
            this.SheetName = sheetName;
            this.ImagePath = imagePath;
            this.SheetValues = sheetValues;
            this.TaskID = taskID;
            this.HasError = hasError;
            this.ExceptionMessage = exceptionMessage;
        }
    }
}
