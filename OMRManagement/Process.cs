using OMRManagement.Entities;
using OMRReader.BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OMRManagement
{
    public partial class OMRProcess : Form
    {
        List<ProjectDetails> projectList = null;
        public OMRProcess()
        {
            InitializeComponent();
        }

        private void buttonShow_Click(object sender, EventArgs e)
        {
            OMRTemplateManager templateManager = new OMRTemplateManager();
            int projectID = Convert.ToInt32(comboBoxProjects.SelectedValue);
            templateManager.tab = 2;
            templateManager.CurrentProjectName = comboBoxProjects.Text;
           // templateManager.projectID = projectID;
            //templateManager.Show();
            this.Hide();

            templateManager.ShowDialog(projectList.Where(p => p.ProjectID == projectID).Select(p => p).FirstOrDefault());
          

        }

        private void OMRProcess_Load(object sender, EventArgs e)
        {
            projectList = ProjectBL.GetProjects();
            if (projectList != null)
            {
                comboBoxProjects.DisplayMember = "ProjectName";
                comboBoxProjects.ValueMember = "ProjectID";
                comboBoxProjects.DataSource = projectList;
            }
            else
            {
                MessageBox.Show("No projects found");
            }
        }

        private void OMRProcess_FormClosed(object sender, FormClosedEventArgs e)
        {
            Home h = new Home();
            h.Show();
            
        }

        private void comboBoxProjects_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                buttonShow_Click(sender, null);
            }
        }
    }
}
