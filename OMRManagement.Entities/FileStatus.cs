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
    public class FileStatus
    {
        public int FileID { get; set; }
        public int TaskID { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public int Status { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsDelete { get; set; }
        public RunType RunType { get; set; }
        public string ExceptionMessage { get; set; }

        public FileStatus()
        {
        }

        public FileStatus(int taskID, string fileName, string filePath, int status, string errorMessage, bool isDelete, RunType runtype)
        {
            this.TaskID = taskID;
            this.FileName = fileName;
            this.FilePath = filePath;
            this.Status = status;
            this.ErrorMessage = errorMessage;
            this.IsDelete = IsDelete;
            this.RunType = runtype;
        }


    }
}
