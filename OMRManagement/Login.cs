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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void buttonShow_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;
            string password = txtPassword.Text;
            bool user=false;
            user = LoginBL.CheckUser(userName, password);
            if (user == true)
            {
                OMRProcess omrProecess = new OMRProcess();
                omrProecess.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalied UserName/Password");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            Home home = new Home();
            home.Visible = true;
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                buttonShow_Click(sender, null);
            }
        }
    }
}
