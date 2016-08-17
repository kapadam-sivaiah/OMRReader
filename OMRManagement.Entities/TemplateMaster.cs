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
  public  class TemplateMaster
    {
        public ProjectDetails Project { get; set; }
        public int TemplateID { get; set; }
        public string TemplateCode { get; set; }
        public string TemplateName { get; set; }
//TemplateFilePath
        public int TrackTop { get; set; }
        public int TrackLeft { get; set; }
        public int TrackCount { get; set; }
        public string TemplateImage { get; set; }
        public string TemplateFileName { get; set; }
        public string TemplateFilePath { get; set; }
        public int AllowedErrorCharCount { get; set; }
        public bool IsActive { get; set; }
        public string TrackIndex { get; set; }
        public List<TemplateField> FieldList { get; set; }
        public bool IsDuplex { get; set; }
        public PageType Page { get; set; }
        public List<FrontBackPageMapper> FrontBackTemplateMapperList { get; set; }
        public TemplateMaster()
        {
            this.FieldList = new List<TemplateField>();
            this.FrontBackTemplateMapperList = new List<FrontBackPageMapper>();
        }

    }
}
