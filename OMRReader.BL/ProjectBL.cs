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
    public class ProjectBL
    {

        public static ProjectDetails Add(ProjectDetails project)
        {
            return ProjectDAL.Add(project);
           
        }

        public static List<ProjectDetails> GetProjects()
        {
          return  ProjectDAL.GetProjects();
        }
        public static List<ProjectDetails> GetProjectByID(int projectID)
        {
            return ProjectDAL.GetProjectsByID(projectID);
        }
       
    }
}
