using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VoodooPOS
{
    public partial class EmployeeEdit : Form
    {
        public Voodoo.Objects.Employee Employee;
        XmlData xmlData;
        int employeeID = -1;

        public EmployeeEdit()
        {
            InitializeComponent();

            xmlData = new XmlData(Application.StartupPath);
        }

        public EmployeeEdit(DataGridViewRow Employee)
        {
            InitializeComponent();

            employeeID = int.Parse(Employee.Cells["ID"].Value.ToString());

            populateSecurityLevels();

            populateForm(Employee);

        }

        private void populateSecurityLevels()
        {
            DataTable dtSecurityLevels = xmlData.Select("*", "SecurityLevel asc", XmlData.Tables._SecurityLevels);

            foreach (DataRow dr in dtSecurityLevels.Rows)
            {
                ddSecurityLevels.Items.Add(new VoodooPOS.objects.SecurityLevelClass(dr["SecurityLevel"].ToString() +"-"+ dr["Name"].ToString(),int.Parse(dr["SecurityLevel"].ToString())));
            }
        }

        private void populateForm(DataGridViewRow drEmployee)
        {
            Employee = new Voodoo.Objects.Employee();
            Employee.FirstName = drEmployee.Cells["firstName"].Value.ToString();
            Employee.LastName = drEmployee.Cells["lastName"].Value.ToString();
            Employee.EmailAddress = drEmployee.Cells["emailAddress"].Value.ToString();
            Employee.PhoneNumber = drEmployee.Cells["phoneNumber"].Value.ToString();
            Employee.UserName = drEmployee.Cells["username"].Value.ToString();
            Employee.Password = drEmployee.Cells["password"].Value.ToString();
            Employee.SecurityLevel = int.Parse(drEmployee.Cells["securityLevel"].Value.ToString());
            Employee.DateCreated = DateTime.Parse(drEmployee.Cells["dateCreated"].Value.ToString());

            txtFirstName.Text = Employee.FirstName;
            txtLastName.Text = Employee.LastName;
            txtEmail.Text = Employee.EmailAddress;
            txtPhone.Text = Employee.PhoneNumber;
            txtUsername.Text = Employee.UserName;
            txtPassword.Text = Employee.Password;
            ddSecurityLevels.SelectedValue = Employee.SecurityLevel;
            txtDateCreated.Text = Employee.DateCreated.ToShortDateString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Employee = new Voodoo.Objects.Employee();
            Employee.ID = employeeID;
            Employee.FirstName = txtFirstName.Text;
            Employee.LastName = txtLastName.Text;
            Employee.EmailAddress = txtEmail.Text;
            Employee.PhoneNumber = txtPhone.Text;
            Employee.UserName = txtUsername.Text;
            Employee.Password = txtPassword.Text;
            Employee.SecurityLevel = int.Parse(ddSecurityLevels.SelectedValue.ToString());
            Employee.DateCreated = DateTime.Parse(txtDateCreated.Text);

            xmlData.Update(Employee, XmlData.Tables.Employees);

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
