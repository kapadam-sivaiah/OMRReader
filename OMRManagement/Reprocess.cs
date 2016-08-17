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

    public partial class Reprocess : Form
    {
    

        public Reprocess()
        {
            InitializeComponent();
        }

        private void Reprocess_Load(object sender, EventArgs e)
        {
        }

        private void btnRecover_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Form f = System.Windows.Forms.Application.OpenForms["OMRTemplateManager"];
            ((OMRTemplateManager)f).ProcessType = 1;
            this.Close();
        }

        private void buttonReprocess_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Form f = System.Windows.Forms.Application.OpenForms["OMRTemplateManager"];
            ((OMRTemplateManager)f).ProcessType = 2;
            this.Close();
        }

        private void btnError_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Form f = System.Windows.Forms.Application.OpenForms["OMRTemplateManager"];
            ((OMRTemplateManager)f).ProcessType = 3;
            this.Close();
        }

        private void btnRecover_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

       
    }
}
