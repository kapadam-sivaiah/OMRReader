/*******************************************************
* Copyright (C) 2016-2020 Manipal Technologies Pvt. Ltd.
* 
 * This file is part of the project OSR and has been exclusively created for internal use of
* Manipal Technologies Pvt. Ltd. or licensed use of clients of Manipal Technologies Pvt. Ltd.
* Under no circumstances, can this file/project could be used individually or as part of
* OSR application can be copied and/or distributed without the express
* permission of Manipal Technologies Pvt. Ltd.
*******************************************************/

using OMRManagement.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMRReader.BL
{
    public class TaskDetails
    {
        public JobDetails Job { get; set; }
        
        public int TaskID { get; set; }

        public Template MatchingTemplate { get; set; }

        public Template MatchingBackPageTemplate { get; set; }

        public string FolderPath { get; set; }

        public int TotalFileCount { get; set; }

        public int SuccessfulFileCount { get; set; }

        public DateTime TaskStartDate { get; set; }

        public DateTime TaskEndDate { get; set; }

        public List<string> UnMatchedFileList { get; set; }

        public RunType RunType { get; set; }

        public List<FileStatus> FileStatusList { get; set; }

        public TaskDetails()
        {
            this.MatchingTemplate = new Template();
            this.Job = new JobDetails();
            //this.Job=new 
            this.UnMatchedFileList = new List<string>();
            this.FileStatusList=new List<FileStatus>();
        }



        public static List<Task> FileCountWiseTaskList { get; set; }

        public void Run(int threshold, RunType runType, List<string> jobUnMatchedList)
        {
            FileCountWiseTaskList = new List<Task>();

            List<string> fileList = Directory.GetFiles(FolderPath).ToList();
           
            int taskCount = 1;
            if (fileList.Count() > 500)
            {
                taskCount = (fileList.Count() / 500) + 1;
            }            
            for (int i = 0; i < taskCount; i++)
            {
                List<string> smallFileList = i < (fileList.Count() % 500) ? fileList.Skip(i * 500).Take(500).ToList()
                                           : fileList.Skip(i * 500).Take((fileList.Count() - (i - 1) * 500)).ToList();

                FileCountWiseTaskList.Add(Task.Factory.StartNew(() =>
                {
                    ProcessOMR(threshold, runType, smallFileList);
                }));
               
               
            }
            Task.WaitAll(FileCountWiseTaskList.ToArray());                       
            jobUnMatchedList.AddRange(this.UnMatchedFileList);
            //SheetValuesBL.
            //insert track issue file status
            //this.FileStatusList 
            SheetValuesBL.AddFileStatus(FileStatusList);
        }


        public List<string> UnmatchedRun(int threshold, RunType runType, List<string> jobUnmatchedList)
        {
            ProcessOMR(threshold, runType, jobUnmatchedList);
            return this.UnMatchedFileList;
        }

        public void ProcessOMR(int threshold, RunType runType, List<string> fileList)
        {
            int i = 0;
            string frontOMR = string.Empty, backOMR = string.Empty, frontBC = string.Empty, backBC = string.Empty;
            string OMRString = string.Empty, barcodeDetails = string.Empty;
            string frontFileName = string.Empty; string errorMessage = string.Empty;
            FileStatus status; int fileStatus = 0;
            MatchingTemplate = TemplateBL.GetTemplateByID(this.MatchingTemplate.TemplateID);
            bool hasError = false;
            bool hasFrontError = false;
            if (MatchingTemplate.IsDuplex)
            {
                MatchingBackPageTemplate = TemplateBL.GetBackTemplateByFrontTemplateID(this.MatchingTemplate.TemplateID);

                fileList.ForEach(file =>
                {
                    if (File.Exists(file) && !file.Contains("Thumbs."))
                    {
                        if (i % 2 == 0) // FRONT PAGE
                        {
                            try
                            {
                                frontOMR = string.Empty;
                                frontBC = string.Empty;
                                backBC = string.Empty;
                                backOMR = string.Empty;
                                OMRString = string.Empty;
                                barcodeDetails = string.Empty;
                                frontFileName = string.Empty;
                                frontFileName = file;
                                ReadOMR(MatchingTemplate, file, threshold, runType, out frontOMR, out frontBC, out hasFrontError);
                            }
                            catch (Exception ex)
                            {
                                this.UnMatchedFileList.Add(file);
                                status = new FileStatus(TaskID, System.IO.Path.GetFileName(file), file, 0, ex.ToString(), false, runType);
                                //status = SheetValuesBL.AddFileStatus(status);
                                this.FileStatusList.Add(status);
                            }
                        }
                        else //BACK PAGE
                        {
                            try
                            {
                                ReadOMR(MatchingBackPageTemplate, file, threshold, runType, out backOMR, out backBC, out hasError);
                            }
                            catch (Exception ex)
                            {
                                this.UnMatchedFileList.Add(file);
                                status = new FileStatus(TaskID, System.IO.Path.GetFileName(file), file, 0, ex.ToString(), false, runType);
                               // status = SheetValuesBL.AddFileStatus(status);
                                this.FileStatusList.Add(status);
                            }

                            if (!string.IsNullOrEmpty(frontBC))
                            {
                                barcodeDetails = frontBC;
                            }
                            if (!string.IsNullOrEmpty(backBC))
                            {
                                barcodeDetails = barcodeDetails + ";" + backBC;
                            }

                            if (!string.IsNullOrEmpty(frontOMR) & !string.IsNullOrEmpty(backOMR))
                            {
                                OMRString = frontOMR + ";" + backOMR;
                                if (hasError || hasFrontError)
                                {
                                    hasError = true;
                                }
                                Sheet sheet = new Sheet(barcodeDetails, frontFileName + ";" + file, OMRString, TaskID, string.Empty,hasError);
                                sheet = SheetValuesBL.Add(sheet);
                                errorMessage = string.Empty;
                                fileStatus = 1;
                            }
                            else
                            {
                                errorMessage = string.IsNullOrEmpty(frontOMR) ? "Front page OMR error" : "Back page OMR error";
                                fileStatus = 0;
                            }

                            //status = new FileStatus(TaskID, System.IO.Path.GetFileName(frontFileName), frontFileName, fileStatus, errorMessage, false, runType);
                            //status = SheetValuesBL.AddFileStatus(status);
                            //status = new FileStatus(TaskID, System.IO.Path.GetFileName(file), file, fileStatus, errorMessage, false, runType);
                            //status = SheetValuesBL.AddFileStatus(status);
                        }
                    }
                    i += 1;
                });
            }
            else
            {
                fileList.ForEach(file =>
                {
                    if (File.Exists(file) && !file.Contains("Thumbs."))
                    {

                        try
                        {
                            OMRString = string.Empty;
                            barcodeDetails = string.Empty;
                            ReadOMR(MatchingTemplate, file, threshold, runType, out OMRString, out barcodeDetails,out hasError);
                            if (!string.IsNullOrEmpty(OMRString))
                            {
                                Sheet sheet = new Sheet(barcodeDetails, file, OMRString, TaskID, string.Empty, hasError);
                                sheet = SheetValuesBL.Add(sheet);
                            }
                            //else
                            //{
                            //    //  this.UnMatchedFileList.Add(file);
                            //    // this.UnMatchedFileList.Add(file);
                            //    status = new FileStatus(TaskID, System.IO.Path.GetFileName(file), file, 0, "Track issue", false, runType);
                            //    // status = SheetValuesBL.AddFileStatus(status);
                            //    this.FileStatusList.Add(status);
                            //}
                        }
                        catch (Exception ex)
                        {
                            this.UnMatchedFileList.Add(file);
                           // this.UnMatchedFileList.Add(file);
                            status = new FileStatus(TaskID, System.IO.Path.GetFileName(file), file, 0, ex.ToString(), false, runType);
                            ///status = SheetValuesBL.AddFileStatus(status);
                           this.FileStatusList.Add(status);
                        }
                    }
                    i += 1;
                });
            }
        }


        public void ReadOMR(Template template, string file, int threshold, RunType runType, out string OMRString, out string barcodeDetails, out bool hasError)
        {
            hasError = false;
            barcodeDetails = BarcodeReader.ReadBarcodeDetails(file);
            BitmapConvertion bitmapConversion = new BitmapConvertion();
            Bitmap bitmapResult = bitmapConversion.Prewit(file);
            //bitmapResult.Save("e:\\4.bmp");
            int templateDiffLeft = 0, templateDiffTop = 0;
          //  int 
            TrackReader.ReadTrack(template.TrackLeft, template.TrackTop, bitmapResult, 0, 0, out templateDiffLeft, out templateDiffTop);
           // templateDiffLeft =templateDiffLeft- (template.TrackLeft);
            ArrayList trackArray = new ArrayList();
            trackArray.AddRange(template.TrackIndex.Split(','));
            
                if (TemplateReader.IsMatch(bitmapResult, trackArray, template.TrackCount, templateDiffLeft, templateDiffTop, template.TrackTop, template.TrackLeft))
                {
                    //READER
                    OMRString = TemplateReader.GetSheetValues(bitmapResult, template, templateDiffTop, templateDiffLeft, trackArray, threshold, out hasError);

                    //FileStatus status = new FileStatus(TaskID, System.IO.Path.GetFileName(file), file, 1, string.Empty, false, runType);
                    //status = SheetValuesBL.AddFileStatus(status);
                }
                else
                {
                    OMRString = string.Empty;
                    this.UnMatchedFileList.Add(file);
                    FileStatus status = new FileStatus(TaskID, System.IO.Path.GetFileName(file), file, 0, "Track issue", false, runType);
                    // status = SheetValuesBL.AddFileStatus(status);
                    this.FileStatusList.Add(status);
                    // insert FileStatus
                }
            //}
            // else
            //    {
            //        OMRString = string.Empty;
            //        this.UnMatchedFileList.Add(file);
            //        FileStatus status = new FileStatus(TaskID, System.IO.Path.GetFileName(file), file, 0, "Track issue", false, runType);
            //        // status = SheetValuesBL.AddFileStatus(status);
            //        this.FileStatusList.Add(status);
            //        // insert FileStatus
            //    }
        }

       
        public void Stop()
        {

        }

        public void Recover(int threshold, RunType runType, List<string> processedFiles)
        {
            List<string> fileList = Directory.GetFiles(FolderPath).ToList();
            //Split if duplex
            List<string> formattedRecoveryFileList = new List<string>();
            processedFiles.ForEach(f =>
            {
                if (f.ToString().Contains(';'))
                {
                    string[] files = f.Split(';');
                    if (!string.IsNullOrEmpty(files[0].ToString()))
                    {
                        formattedRecoveryFileList.Add(files[0].ToString());
                    }
                    if (!string.IsNullOrEmpty(files[1].ToString()))
                    {
                        formattedRecoveryFileList.Add(files[1].ToString());
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(f))
                    {
                        formattedRecoveryFileList.Add(f);
                    }
                }
            });

            ProcessOMR(threshold, runType, fileList.Except(formattedRecoveryFileList).ToList());
            SheetValuesBL.AddFileStatus(FileStatusList);
        }

        public void ForceRun()
        {

        }
    }

    //public class ExceptionTask
    //{

    //}


}
