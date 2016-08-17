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
    public class Job
    {

        public int JobID { get; set; }

        public ProjectDetails Project { get; set; }
     
        public DateTime JobStartDate { get; set; }

        public DateTime JobEndDate { get; set; }

        public Status JobStatus { get; set; }

        public string BasePath { get; set; }

        public RunType Type { get; set; }

        public string ErrorMessge { get; set; }

        public int Threshold { get; set; }

       public List<ReadTask> TaskList { get; set; }


            public Job()
            {
               this.TaskList=new List<ReadTask>();
                this.Project = new ProjectDetails();
            }
    }
}
