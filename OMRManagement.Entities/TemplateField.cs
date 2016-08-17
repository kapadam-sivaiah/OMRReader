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
   public class TemplateField
    {
        //public TemplateMaster Te { get; set; }
        public int FieldID { get; set; }
        public string FieldName { get; set; }
        public int FieldTopPosition { get; set; }
        public int FieldLeftPosition { get; set; }
        public int FieldRowNo { get; set; }
        public string FieldIndex { get; set; }
        public FieldType TemplateFieldType { get; set; }
        public int StartIndex { get; set; }
        public int TrackRowNo { get; set; }
        public int FieldColumnNo { get; set; }
        public int BubbleWidth { get; set; }
        public int BubbleHeight { get; set; }
        public int BubbleRowGap { get; set; }
        public int BubbleColumnGap { get; set; }
        public int AnnotationWidth { get; set; }
        public int AnnotationHeight { get; set; }
       
        public TemplateMaster Template { get; set; }
        public TemplateField()
        {
            this.Template = new TemplateMaster();
        }
    }
}
