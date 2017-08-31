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
    public partial class LabelTemplate : Form
    {
        int selectedRowIndex = 1;
        int selectedColumnIndex = 1;
        int numColumns = 4;
        int numRows = 20;
        XmlData xmlData;

        public int SelectedRowIndex
        {
            get { return selectedRowIndex; }
        }

        public int SelectedColumnIndex
        {
            get { return selectedColumnIndex; }
        }

        public int NumberOfColumns
        {
            get { return numColumns; }
        }

        public LabelTemplate()
        {
            InitializeComponent();

            xmlData = new XmlData(Application.StartupPath);

            createButtons();

            ddColumns.SelectedItem = numColumns.ToString();

            txtSelectedRow.Text = selectedRowIndex.ToString();
            txtSelectedColumn.Text = selectedColumnIndex.ToString();
        }

        private void checkRowHeight()
        {
            int rowHeight = 25;

            rowHeight = dgvLabels.Height / dgvLabels.Rows.Count;

            foreach (DataGridViewRow row in dgvLabels.Rows)
                row.Height = rowHeight;
        }

        private void createButtons()
        {
            //set numRows
            switch (numColumns)
            {
                case 3:
                    numRows = 10;
                    break;
                case 4:
                    numRows = 20;
                    break;
            }

            dgvLabels.Rows.Clear();
            dgvLabels.Columns.Clear();

            DataGridViewColumn newColumn;
            DataGridViewCell cellTemplate = new DataGridViewTextBoxCell();

            for (int y = 0; y < numColumns; y++)
            {
                newColumn = new DataGridViewColumn(cellTemplate);

                dgvLabels.Columns.Add(newColumn);
            }

            for (int x = 0; x < numRows; x++)
            {
                dgvLabels.Rows.Add();

                for (int y = 0; y < numColumns; y++)
                    dgvLabels.Rows[x].Cells[y].Value = (x + 1).ToString() + " - " + (y + 1).ToString();
            }

            dgvLabels.CellClick += new DataGridViewCellEventHandler(dgvLabels_CellContentClick);

            checkRowHeight();
        }

        private void dgvLabels_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedColumnIndex = e.ColumnIndex;
            selectedRowIndex = e.RowIndex;

            if(chkPrintOnSelect.Checked)
                this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            selectedRowIndex = -1;
            selectedColumnIndex = -1;

            this.DialogResult = DialogResult.Cancel;

            this.Close();
        }

        /// <summary>
        /// Update layout to match what was asked for
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (int.TryParse(ddColumns.SelectedItem.ToString(), out numColumns))
                createButtons();
            else
                MessageBox.Show("invalid selection");    
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            selectedRowIndex = 1;
            selectedColumnIndex = 1;

            txtSelectedColumn.Text = selectedColumnIndex.ToString();
            txtSelectedRow.Text = selectedRowIndex.ToString();
        }
    }
}
