using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace VoodooPOS
{
    public partial class Login : Form
    {
        public string UserName = "";
        public string Password = "";
        public Voodoo.Objects.Employee Employee;

        XmlData xmlData;
        DataTable dtEmployee;

        public Login()
        {
            InitializeComponent();
            
            xmlData = new XmlData(Application.StartupPath);

            //set background image
            if (File.Exists(Application.StartupPath + Properties.Settings.Default.BackgroundImagePath))
                this.BackgroundImage = Image.FromFile(Application.StartupPath + Properties.Settings.Default.BackgroundImagePath);

            txtUserName.Select();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text.Trim().Length == 0)
                MessageBox.Show("User Name is required");
            else if (txtPassword.Text.Trim().Length == 0)
                MessageBox.Show("Password is required");
            else
            {
                UserName = txtUserName.Text.Trim();
                Password = txtPassword.Text.Trim();

                dtEmployee = xmlData.Select("username = '"+ UserName +"' and password = '"+ Password +"'", "", XmlData.Tables.Employees);

                if (dtEmployee != null)
                {
                    Employee = new Voodoo.Objects.Employee();
                    Employee.FirstName = dtEmployee.Rows[0]["firstName"].ToString();
                    Employee.LastName = dtEmployee.Rows[0]["lastName"].ToString();
                    Employee.EmailAddress = dtEmployee.Rows[0]["emailAddress"].ToString();
                    Employee.PhoneNumber = dtEmployee.Rows[0]["phoneNumber"].ToString();
                    Employee.UserName = dtEmployee.Rows[0]["username"].ToString();
                    Employee.Password = dtEmployee.Rows[0]["password"].ToString();
                    Employee.SecurityLevel = int.Parse(dtEmployee.Rows[0]["securityLevel"].ToString());
                    Employee.DateCreated = DateTime.Parse(dtEmployee.Rows[0]["dateCreated"].ToString());

                    this.DialogResult = DialogResult.OK;
                }
                else
                    MessageBox.Show("Invalid login");

                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void Login_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //enter key
                btnLogin.PerformClick();
        }
    }
}
