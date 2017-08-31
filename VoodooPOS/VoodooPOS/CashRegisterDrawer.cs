using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace VoodooPOS
{
    public partial class CashRegisterDrawer : Form
    {
        XmlData xmlData = new XmlData(Application.StartupPath);
        VoodooPOS.objects.CashRegisterDrawer cashDrawer;

        public CashRegisterDrawer()
        {
            InitializeComponent();

            DataTable dtDrawer = null;
            DataTable dtDrawerHistory = new DataTable();
            DataTable dtTemp = null;

            try
            {
                dtDrawer = xmlData.Select("*", "", XmlData.Tables.CashRegisterDrawer);
            }
            catch (Exception ex)
            {
                Common.WriteToFile(ex);
            }

            if (dtDrawer != null)
            {
                cashDrawer = (VoodooPOS.objects.CashRegisterDrawer)Common.CreateObjects.FromDataRow(dtDrawer.Rows[0], new VoodooPOS.objects.CashRegisterDrawer());

                txtCurrentAmount.Text = cashDrawer.Amount.ToString("N");
            }

            try
            {
                foreach (string file in Directory.GetFiles(Application.StartupPath + "\\data\\logs", "CashDrawerHistory_*"))
                {
                    FileInfo fi = new FileInfo(file);

                    dtTemp = xmlData.Select("*", "DateCreated asc", "Data\\logs\\" + fi.Name.Replace(".xml",""), "Data\\"+ XmlData.Tables.CashRegisterDrawer.ToString());

                    if (dtTemp != null && dtTemp.Rows.Count > 0)
                    {
                        if (dtDrawerHistory.Rows.Count == 0)
                            dtDrawerHistory = dtTemp.Copy();
                        else
                        {
                            foreach (DataRow dr in dtTemp.Rows)
                                dtDrawerHistory.Rows.Add(dr.ItemArray);
                        }
                    }
                }

                dtDrawerHistory.DefaultView.Sort = "DateCreated desc";

                dgvDrawer.DataSource = dtDrawerHistory.DefaultView;
            }
            catch (Exception ex)
            {
                Common.WriteToFile(ex);
            }
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            if (btnChange.Text == "Change")
            {
                txtCurrentAmount.ReadOnly = false;
                txtCurrentAmount.BackColor = Color.White;
                btnSave.Enabled = true;
                btnChange.Text = "Cancel";

                txtCurrentAmount.Focus();
            }
            else
            {
                btnChange.Text = "Change";
                txtCurrentAmount.ReadOnly = true;
                txtCurrentAmount.BackColor = Color.LightGray;
                btnSave.Enabled = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cashDrawer == null)
            {
                cashDrawer = new VoodooPOS.objects.CashRegisterDrawer();
                cashDrawer.Amount = double.Parse(txtCurrentAmount.Text);
                cashDrawer.Description = "Cart File didn't exist.  Cart Created";

                CashDrawerChangeReason reasonForm = new CashDrawerChangeReason();
                reasonForm.ShowDialog();

                cashDrawer.ReasonForChange = reasonForm.ReasonForChange;

                xmlData.Insert(cashDrawer, XmlData.Tables.CashRegisterDrawer);
            }
            else
            {
                double origAmount = cashDrawer.Amount;

                cashDrawer.Amount = double.Parse(txtCurrentAmount.Text);

                CashDrawerChangeReason reasonForm = new CashDrawerChangeReason();
                reasonForm.ShowDialog();

                cashDrawer.ReasonForChange = reasonForm.ReasonForChange;

                xmlData.Update(cashDrawer, XmlData.Tables.CashRegisterDrawer);

                cashDrawer.Description = "Drawer amount changed manually from "+ origAmount.ToString("C") +" to "+ cashDrawer.Amount.ToString("C");
            }

            Common common = new Common(Application.StartupPath);

            common.WriteToLog("CashDrawerHistory", cashDrawer);

            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (cashDrawer == null)
            {
                MessageBox.Show("You must have a total for your drawer before you can remove from your drawer");
            }
            else
            {
                double amountToRemove = 0;

                if (double.TryParse(txtRemoveMoney.Text, out amountToRemove))
                {
                    if (amountToRemove < cashDrawer.Amount)
                    {
                        CashDrawerChangeReason reasonForm = new CashDrawerChangeReason();
                        reasonForm.ShowDialog();

                        cashDrawer.ReasonForChange = reasonForm.ReasonForChange;

                        cashDrawer.Amount = cashDrawer.Amount - amountToRemove;
                        xmlData.Update(cashDrawer, XmlData.Tables.CashRegisterDrawer);

                        cashDrawer.Description = amountToRemove.ToString("C") +" Removed from drawer";
                    }
                    else
                        MessageBox.Show("You can not remove more than what is in the drawer");
                }
                else
                    MessageBox.Show("You must enter a valid amount to remove");
            }

            Common common = new Common(Application.StartupPath);

            common.WriteToLog("CashDrawerHistory", cashDrawer);

            this.Close();
        }
    }
}
