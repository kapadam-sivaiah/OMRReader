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
  public  class ReadTask
    {
       
            public Job Job { get; set; }

            public int TaskID { get; set; }

            public TemplateMaster MatchingTemplate { get; set; }

            public string FolderPath { get; set; }

            public int TotalFileCount { get; set; }

            public int SuccessfulFileCount { get; set; }

            public DateTime TaskStartDate { get; set; }

            public DateTime TaskEndDate { get; set; }
            public List<string> UnMatchedFileList { get; set; }

            public ReadTask()
            {
                this.MatchingTemplate = new TemplateMaster();
                this.Job = new Job();
            }

    }
}
