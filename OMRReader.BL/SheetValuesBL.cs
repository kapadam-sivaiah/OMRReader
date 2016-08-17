/*******************************************************
* Copyright (C) 2016-2020 Manipal Technologies Pvt. Ltd.
* 
 * This file is part of the project OSR and has been exclusively created for internal use of
* Manipal Technologies Pvt. Ltd. or licensed use of clients of Manipal Technologies Pvt. Ltd.
* Under no circumstances, can this file/project could be used individually or as part of
* OSR application can be copied and/or distributed without the express
* permission of Manipal Technologies Pvt. Ltd.
*******************************************************/

using OMRManagement.DAL;
using OMRManagement.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMRReader.BL
{
  public  class SheetValuesBL
    {
      public static Sheet Add(Sheet sheet)
      {
          return SheetValuesDAL.Add(sheet);

      }

      public static bool AddFileStatus(List<FileStatus> fileStatus)
      {


          string fileStatusXML = Utility.CreateXML(fileStatus);
          fileStatusXML = fileStatusXML.Replace("<ArrayOfFileStatus>", string.Empty).Replace("</ArrayOfFileStatus>", string.Empty);
         
          return SheetValuesDAL.AddFileStatus(fileStatusXML); 
          
         
      }
      public static DataSet GetSheetValues(int projectID)
      {


          return SheetValuesDAL.GetSheetValues(projectID);


      }
      public void UpdateIsDelete(int JoID)
      {


           SheetValuesDAL.UpdateIsDelete(JoID);


      }
      public void UpdateTaskEndtDate(int TaskID)
      {


          SheetValuesDAL.UpdateIsDelete(TaskID);


      }

    }
}
