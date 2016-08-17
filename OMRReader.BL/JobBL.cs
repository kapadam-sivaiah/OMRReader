/*******************************************************
* Copyright (C) 2016-2020 Manipal Technologies Pvt. Ltd.
* 
 * This file is part of the project OSR and has been exclusively created for internal use of
* Manipal Technologies Pvt. Ltd. or licensed use of clients of Manipal Technologies Pvt. Ltd.
* Under no circumstances, can this file/project could be used individually or as part of
* OSR application can be copied and/or distributed without the express
* permission of Manipal Technologies Pvt. Ltd.
*******************************************************/

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMRManagement.DAL;
using OMRManagement.Entities;
namespace OMRReader.BL
{
    public class JobBL
    {
        public static JobDetails GetJobByID(int jobID)
        {
            //JobComposite  jobComposite  = JobDAL.GetJobByID(jobID);
            //JobTemplateComposite jobTemplate = new JobTemplateComposite(jobComposite);
            //Mapper.CreateMap<List<ReadTask>,List<TaskDetails>>();
            // Mapper.CreateMap<TemplateMaster,Template>();        
            //Mapper.CreateMap<Job, JobDetails>()
            //JobDetails jobDetails = Mapper.Map<Job, JobDetails>(jobTemplate.Job);          
            //return jobDetails;

            Job job = JobDAL.GetJobByID(jobID);
            ////Mapper.CreateMap<List<ReadTask>, List<TaskDetails>>();
            //Mapper.CreateMap<Job, JobDetails>();
            //JobDetails jobDetails = Mapper.Map<Job, JobDetails>(job);

            JobDetails jobDetails = new JobDetails();
            jobDetails.BasePath = job.BasePath;
            jobDetails.Threshold = job.Threshold;
            jobDetails.JobID = job.JobID;
            jobDetails.Type = job.Type;
            jobDetails.JobStatus = job.JobStatus;
            jobDetails.Project.ProjectID = job.Project.ProjectID;
             job.TaskList.ForEach(t =>
             {
                 TaskDetails task = new TaskDetails();

                 task.TotalFileCount = t.TotalFileCount;
                 task.FolderPath = t.FolderPath;
                 task.TaskEndDate = t.TaskEndDate;
                 task.SuccessfulFileCount = t.SuccessfulFileCount;
                 task.TaskStartDate = t.TaskStartDate;
                 task.TaskID = t.TaskID;
                 task.MatchingTemplate.TemplateID = t.MatchingTemplate.TemplateID;
               
                 jobDetails.TaskList.Add(task);
             }
             );


           // jobDetails.TaskList = Mapper.Map<List<ReadTask>, List<TaskDetails>>(job.TaskList);

            return jobDetails; 
                      
        }
       
    }
}
