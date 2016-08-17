using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OMRManagement.Entities;
using OMRReader.BL;

namespace OMRManagement
{
    public partial class Project : Form
    {
        List<ProjectDetails> projectList =null;
        public Project()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonShow_Click(object sender, EventArgs e)
        {
            OMRTemplateManager templateManager = new OMRTemplateManager();
            //int projectID = Convert.ToInt32(comboBoxProjects.SelectedValue);
           ProjectDetails Project = new ProjectDetails();
            Project.ProjectName = txtProjectName.Text.Trim();
            Project.ProjectCode = txtProjectCode.Text.Trim();
            if (!string.IsNullOrEmpty(Project.ProjectName) && !string.IsNullOrEmpty(Project.ProjectCode))
            {
                Project = ProjectBL.Add(Project);

                txtProjectName.Clear();
                txtProjectCode.Clear();
                templateManager.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Project Name/ Project Code Blanks Allowed");
            }
           

        }

        private void Project_Load(object sender, EventArgs e)
        {
            projectList = ProjectBL.GetProjects();
            if (projectList != null)
            {
                comboBoxProjects.DisplayMember = "ProjectName";
                comboBoxProjects.ValueMember = "ProjectID";
                comboBoxProjects.DataSource = projectList;
            }
            //else
            //{
            //    MessageBox.Show("No projects found");
            //}
        }

        private void btnProjectReset_Click(object sender, EventArgs e)
        {
            txtProjectCode.Clear();
            txtProjectName.Clear();
        }

        private void txtProjectCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                buttonShow_Click(sender, null);
            }
        }
    }
}
