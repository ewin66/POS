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
    public partial class Employees : Form
    {
        XmlData xmlData;
        Common common;
        

        public Employees()
        {
            InitializeComponent();

            xmlData = new XmlData(Application.StartupPath);

            populateGrid();

        }

        private void populateGrid()
        {
            DataTable dtEmployees = xmlData.Select("*", "", XmlData.Tables.Employees);

            if (dtEmployees != null)
            {
                dgvEmployees.DataSource = dtEmployees;
            }
            else //no results
            {

            }
        }

        private void dgvEmployees_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (dgvEmployees.SelectedCells.Count > 0 && dgvEmployees.Rows[dgvEmployees.SelectedCells[0].RowIndex].Cells["items"].Value != null)
                {
                    Voodoo.Objects.InventoryItem itemToEdit = common.FindItemInInventory(int.Parse(dgvEmployees.Rows[dgvEmployees.SelectedCells[0].RowIndex].Cells["id"].Value.ToString()));

                    EmployeeEdit employeeEditWindow = new EmployeeEdit(dgvEmployees.Rows[dgvEmployees.SelectedCells[0].RowIndex]);
                    employeeEditWindow.ShowDialog(this);


                }
            }
            catch (Exception ex)
            {
                Common.WriteToFile(ex);
            }
        }
    }
}
