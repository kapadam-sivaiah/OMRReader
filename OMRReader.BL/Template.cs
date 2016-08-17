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
using OMRManagement.DAL;
using OMRManagement.Entities;
namespace OMRReader.BL
{
    public class Template
    {

        public ProjectDetails Project { get; set; }
       // public TemplateField Fields { get; set; }
        public int TemplateID { get; set; }
        public string TemplateCode { get; set; }
        public string TemplateName { get; set; }
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

        public Template()
        {
            this.Project = new ProjectDetails();
            this.FieldList = new List<TemplateField>();
            this.FrontBackTemplateMapperList = new List<FrontBackPageMapper>();
        }
        public List<TemplateField> Field()
        {
            this.FieldList = new List<TemplateField>();
            return FieldList;
        }
        public bool Add()
        {
           // this.TemplateImage = Utility.GetImageInBase64(this.TemplateFileName);
            string templateXML = Utility.CreateXML(this);
            templateXML = templateXML.Replace("<Project>", string.Empty).Replace("</Project>", string.Empty);
            return TemplateDAL.Add(templateXML);
        }

        public bool Delete(int fieldID)
        {
            return FieldsDAL.DeleteFieldID(fieldID);
            //return true;
        }

        public bool Update()
        {
            //this.TemplateImage = Utility.GetImageInBase64(this.TemplateFileName);
            string templateXML = Utility.CreateXML(this);
            templateXML = templateXML.Replace("<Project>", string.Empty).Replace("</Project>", string.Empty);
            return TemplateDAL.Update(templateXML);

           // return true;
        }

        public static bool Match(string folderPath)
        {
            // write code to match template to db here
            ///////
            return true;
        }

     

    }
}
