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
//using Inlite.ClearImageNet;
//using Inlite.ClearImageNet;

namespace OMRReader.BL
{
    public class JobDetails
    {
        public int JobID { get; set; }


        public ProjectDetails Project { get; set; }
        public Status JobStatus { get; set; }
        public RunType Type { get; set; }
        public DateTime JobStartDate { get; set; }
        public DateTime JobEndDate { get; set; }
        public string BasePath { get; set; }
        public int Threshold { get; set; }
        public List<string> UnMatchedList { get; set; }
        public List<TaskDetails> TaskList { get; set; }
        public string ErrorMessge { get; set; }
      //  public static ImageEditor ImageRepair = new ImageEditor();
       // public static ImageIO imgIO = new ImageIO();
        public JobDetails()
        {
            this.Project = new ProjectDetails();
            this.TaskList = new List<TaskDetails>();
           // this.ImageRepair = new ImageEditor();
        }
        public Job UpdateJob()
        {
            string jobXML = Utility.CreateXML(this);
            jobXML = jobXML.Replace("<Project>", string.Empty).Replace("</Project>", string.Empty).Replace("<MatchingTemplate>", string.Empty).Replace("</MatchingTemplate>", string.Empty);
            return  JobDAL.Update(jobXML);
        }
        public Job ValidateAndInsert()
        {
            string jobXML = Utility.CreateXML(this);
            jobXML = jobXML.Replace("<Project>", string.Empty).Replace("</Project>", string.Empty).Replace("<MatchingTemplate>", string.Empty).Replace("</MatchingTemplate>", string.Empty);
            return JobDAL.Add(jobXML);
        }
        public void UpdateJobEndDate(int JobID)
        {

            JobDAL.UpdateJobEndDate(JobID);
        }
        public static List<Task> FolderWiseTaskList { get; set; }

        public void Run()
        {
            UnMatchedList=new List<string>();
           
            if (FolderWiseTaskList == null)
            {
                FolderWiseTaskList = new List<Task>();
            }

            this.TaskList.ForEach(
                t =>
                {
                    SheetValuesDAL.UpdateTaskStarDate(t.TaskID);
                    FolderWiseTaskList.Add(Task.Factory.StartNew(() =>
                    {
                        t.Run(Threshold, this.Type, UnMatchedList);
                       // System.Threading.Thread.Sleep(5000);
                    }));
                    SheetValuesDAL.UpdateTaskEndDate(t.TaskID);
                }
                );
                    

            Task.WaitAll(FolderWiseTaskList.ToArray());
            if (UnMatchedList != null&&UnMatchedList.Count>0)
            {
                RunUnmatchedList(Project.ProjectID, UnMatchedList);
            }
            


        }
       
        public void Stop()
        {

        }
        public void RunUnmatchedList(int projectID,List<string> unmatchedList)
        {

            List<Template> templateList = TemplateBL.GetProjectTemplates(projectID);
            if (templateList != null)
            {
                templateList.ForEach(t =>
                {
                    List<string> taskUnmatchedList = new List<string>();
                    //create task 
                    int taskID = JobDAL.AddTask(JobID, t.TemplateID, unmatchedList.Count, RunType.UnMatched);
                    TaskDetails task = new TaskDetails();
                    task.TaskID = taskID;
                    task.MatchingTemplate = t;
                    task.Job.JobID = this.JobID;
                    task.RunType = RunType.UnMatched;
                    //task.TotalFileCount = 
                    taskUnmatchedList = task.UnmatchedRun(Threshold,RunType.UnMatched,unmatchedList);

                    unmatchedList = taskUnmatchedList;
                });
            }

            

        }
        public void Recover()
        {

            List<string> processedFiles = SheetValuesDAL.GetRecoveryValues(JobID);

            if (FolderWiseTaskList == null)
            {
                FolderWiseTaskList = new List<Task>();
            }

            this.TaskList.ForEach(
                t =>
                {
                    SheetValuesDAL.UpdateTaskStarDate(t.TaskID);
                    FolderWiseTaskList.Add(Task.Factory.StartNew(() =>
                    {
                        t.Recover(Threshold, this.Type, processedFiles);
                        // System.Threading.Thread.Sleep(5000);
                    }));
                    SheetValuesDAL.UpdateTaskEndDate(t.TaskID);
                }
                );


            Task.WaitAll(FolderWiseTaskList.ToArray());

        }

        public void ForceRun()
        {
            //update isDeleet to all the sheet values based on JOBID
            SheetValuesDAL.UpdateIsDelete(JobID);
            if (FolderWiseTaskList == null)
            {
                FolderWiseTaskList = new List<Task>();
            }

            this.TaskList.ForEach(
                t =>
                {
                    SheetValuesDAL.UpdateTaskStarDate(t.TaskID);
                    FolderWiseTaskList.Add(Task.Factory.StartNew(() =>
                    {
                        t.Run(Threshold, this.Type,UnMatchedList);
                        // System.Threading.Thread.Sleep(5000);
                    }));
                    SheetValuesDAL.UpdateTaskEndDate(t.TaskID);
                }
                );


            Task.WaitAll(FolderWiseTaskList.ToArray());



        }
        public void ErrorsRun()
        {
            List<string> processedFiles = SheetValuesDAL.GetErrorValues(JobID);

            if (FolderWiseTaskList == null)
            {
                FolderWiseTaskList = new List<Task>();
            }

            this.TaskList.ForEach(
                t =>
                {
                    SheetValuesDAL.UpdateTaskStarDate(t.TaskID);
                    FolderWiseTaskList.Add(Task.Factory.StartNew(() =>
                    {
                        t.Recover(Threshold, this.Type, processedFiles);
                        // System.Threading.Thread.Sleep(5000);
                    }));
                    SheetValuesDAL.UpdateTaskEndDate(t.TaskID);
                }
                );


            Task.WaitAll(FolderWiseTaskList.ToArray());
        }
        public void ExceptionMatch()
        {

        }
    }
}
