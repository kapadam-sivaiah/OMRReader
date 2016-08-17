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
   public class JobComposite
    {
       public Job Job { get; set; }
       public List<ReadTask> TaskList { get; set; }
       public List<TemplateMaster> TemplateList { get; set; }
       public List<TemplateField> TemplateFieldList { get; set; }

       public JobComposite()
       {
           this.Job = new Job();
           this.TaskList = new List<ReadTask>();
           this.TemplateList = new List<TemplateMaster>();
           this.TemplateFieldList = new List<TemplateField>();
       }
   }

  //public class JobTemplateComposite
  //     {
  //         public Job Job  {get;set;}

  //         public JobTemplateComposite(JobComposite jobComposite)
  //         {
  //             Job = new Job();
  //             Job = jobComposite.Job;                
  //             Job.TaskList = jobComposite.TaskList;

  //             Job.TaskList.ForEach( t =>
  //                 {                    
  //                     t.MatchingTemplate = jobComposite.TemplateList .Where ( temp => temp.TemplateID == t.MatchingTemplate.TemplateID).Select( temp => temp).FirstOrDefault();

  //                     if (t.MatchingTemplate.FieldList.Count == 0)
  //                     {
  //                         List<TemplateField> templateFieldList = new List<TemplateField>();
  //                         templateFieldList = jobComposite.TemplateFieldList.Where(f => f.Template.TemplateID == t.MatchingTemplate.TemplateID).ToList();
  //                         t.MatchingTemplate.FieldList.AddRange(templateFieldList);
  //                     }
  //                  }
  //                 );
  //         }
  //     }
        
}
