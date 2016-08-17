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
using OMRReader.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OMRConsoleReader
{
    class Program
    {

        //[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        //private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        //[DllImport("kernel32.dll", ExactSpelling = true)]
        //private static extern IntPtr GetConsoleWindow();
        //private static IntPtr ThisConsole = GetConsoleWindow();
        //private const int HIDE = 0;
        //private const int MAXIMIZE = 3;
        //private const int MINIMIZE = 6;
        //private const int RESTORE = 9;
        

       public static int JobID = 0;
       public string ProcessType = string.Empty;
        static void Main(string[] args)
        {

           // Console.Clear();
         //   Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
           // ShowWindow(ThisConsole, MAXIMIZE);
         //   Console.WriteLine("Please Wait i am starting process....");
           // Console.BackgroundColor = ConsoleColor.White;
            if (args.Length > 0)
            {
                JobID = Convert.ToInt32( args[0].ToString());
               
              JobDetails jobDetails = JobBL.GetJobByID(JobID);
            
              switch (jobDetails.Type)
              {
                  case RunType.Normal:
                      jobDetails.Run();
                      jobDetails.UpdateJobEndDate(JobID);
                      //jobDetails.
                      break;
                  case RunType.Recovery:
                      jobDetails.Recover();
                      jobDetails.UpdateJobEndDate(JobID);
                      break;
                  case RunType.ForceOverwrite:
                      jobDetails.ForceRun();
                      jobDetails.UpdateJobEndDate(JobID);
                      break;
                  case RunType.ErrorOverwrite:
                      jobDetails.ErrorsRun();
                      jobDetails.UpdateJobEndDate(JobID);
                      break;
                  case RunType.UnMatched:
                      //jobDetails.
                      //jobDetails.UpdateJobEndDate(JobID);
                      break;
              }


                //Console.Read();
            }
            else
            {
                //Console.WriteLine("Please supply Job ID ..............................");              
               /// Console.Read();
            }
        }
    }
}
