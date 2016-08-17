using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMRManagement.Entities
{

 public   class UnmatchedList
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
        public UnmatchedList(int taskID)
        {
            
        }
 
    }
}
