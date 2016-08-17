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
using AutoMapper;
using OMRManagement.DAL;
using OMRManagement.Entities;

namespace OMRReader.BL
{
    public class TemplateBL
    {
        public static List<Template> GetProjectTemplates(int projectID)
        {

            List<TemplateMaster> templateMasterList = TemplateDAL.GetProjectTemplates(projectID);
            Mapper.CreateMap<TemplateMaster, Template>();
            List<Template> templateList = Mapper.Map<List<TemplateMaster>, List<Template>>(templateMasterList);
            return templateList;
        }
        public static List<Template> GetFrontPageTemplates(int projectID)
        {

            List<TemplateMaster> templateMasterList = TemplateDAL.GetFrontPageTemplates(projectID);
            Mapper.CreateMap<TemplateMaster, Template>();
            List<Template> templateList = Mapper.Map<List<TemplateMaster>, List<Template>>(templateMasterList);
            return templateList;
        }
        public static Template GetTemplateByID(int templateID)
        {
            TemplateMaster templateMaster = TemplateDAL.GetTemplateByID(templateID);
            Mapper.CreateMap<TemplateMaster, Template>();
            Template template = Mapper.Map<TemplateMaster, Template>(templateMaster);
            return template;
        }
        //public static Template GetTemplateByName(string templateCode,string templateName)
        //{
        //    TemplateMaster templateMaster = TemplateDAL.GetTemplateByName(templateCode,templateName);
        //    Mapper.CreateMap<TemplateMaster, Template>();
        //    Template template = Mapper.Map<TemplateMaster, Template>(templateMaster);
        //    return template;
        //}

        public static Template GetBackTemplateByFrontTemplateID(int templateID)
        {
            TemplateMaster templateMaster = TemplateDAL.GetBackTemplateByFrontTemplateID(templateID);
            Mapper.CreateMap<TemplateMaster, Template>();
            Template template = Mapper.Map<TemplateMaster, Template>(templateMaster);
            return template;
        }

        public static List<TemplateField> GetTemplateFields(int templateID)
        {

            List<TemplateField> templateMasterList = FieldsDAL.GetTemplateFields(templateID);
            //Mapper.CreateMap<TemplateField, Template>();
            //List<Template> templateList = Mapper.Map<List<TemplateField>, List<Template>>(templateMasterList);
            return templateMasterList;
        }
    }
}
