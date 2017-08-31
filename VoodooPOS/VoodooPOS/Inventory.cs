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
    public partial class InventoryWindow : Form
    {
        XmlData xmlData;
        string ddSearchString = "";
        DataTable dtInventoryItems;
        Common common;
        InventoryItemEdit editItemWindow;
        
        public modes mode = modes.Inventory;

        public enum modes
        {
            Inventory, Checkout, Reports, Edit, Locked
        }

        public InventoryWindow()
        {
            InitializeComponent();

            xmlData = new XmlData(Application.StartupPath);

            common = new Common(Application.StartupPath);

            displayInventory();
            populateInventoryDD();

            //set background image
            if (File.Exists(Application.StartupPath + Properties.Settings.Default.BackgroundImagePath))
                this.BackgroundImage = Image.FromFile(Application.StartupPath + Properties.Settings.Default.BackgroundImagePath);
        }

        private void displayInventory()
        {
            //Invoke((MethodInvoker)delegate
            //{
                try
                {
                    dgvItemsToDisplay.Rows.Clear();

                    Voodoo.Objects.InventoryItem item = null;
                    DataTable dtInventory = xmlData.Select("*", "name asc", XmlData.Tables.InventoryItems);

                    if (dtInventory != null)
                    {
                        foreach (DataRow dr in dtInventory.Rows)
                        {
                            double price = 0;
                            int quantity = 0;
                            int n = dgvItemsToDisplay.Rows.Add();

                            dgvItemsToDisplay.Rows[n].Cells["id"].Value = dr["id"].ToString();
                            dgvItemsToDisplay.Rows[n].Cells["UPC"].Value = dr["upc"].ToString();
                            dgvItemsToDisplay.Rows[n].Cells["quantity"].Value = dr["quantity"].ToString();
                            dgvItemsToDisplay.Rows[n].Cells["items"].Value = dr["name"].ToString();

                            double.TryParse(dr["price"].ToString(), out price);
                            int.TryParse(dr["quantity"].ToString(), out quantity);

                            dgvItemsToDisplay.Rows[n].Cells["price"].Value = price.ToString("C");

                            dgvItemsToDisplay.Rows[n].Cells["subtotal"].Value = (price * quantity).ToString("C");

                            //featuredItem
                            DataTable dtFeaturedItemIds = xmlData.Select("InventoryItemID = "+ dr["id"], "", "data\\" + XmlData.Tables.L_InventoryItemsToFeaturedItems.ToString());

                            if (dtFeaturedItemIds != null)
                                dgvItemsToDisplay.Rows[n].Cells["featuredItem"].Value = true;
                            else
                                dgvItemsToDisplay.Rows[n].Cells["featuredItem"].Value = false;

                            //on sale Item
                            DataTable dtOnSaleItemIds = xmlData.Select("InventoryItemID = " + dr["id"], "", "data\\" + XmlData.Tables.L_InventoryItemsToSalesCalendar.ToString());

                            if (dtOnSaleItemIds != null)
                                dgvItemsToDisplay.Rows[n].Cells["onsale"].Value = true;
                            else
                                dgvItemsToDisplay.Rows[n].Cells["onsale"].Value = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Common.WriteToFile(ex);
                }
            //});
        }

        private void dgvItemsToDisplay_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (dgvItemsToDisplay.SelectedCells.Count > 0 && dgvItemsToDisplay.Rows[dgvItemsToDisplay.SelectedCells[0].RowIndex].Cells["items"].Value != null)
                {
                    Voodoo.Objects.InventoryItem itemToEdit = common.FindItemInInventory(int.Parse(dgvItemsToDisplay.Rows[dgvItemsToDisplay.SelectedCells[0].RowIndex].Cells["id"].Value.ToString()));

                    editItemWindow = new InventoryItemEdit(itemToEdit);
                    editItemWindow.inventoryUpdated += new InventoryItemEdit.InventoryUpdated(getInventory);
                    editItemWindow.ShowDialog();

                    populateTransactionDates();

                           
                }
            }
            catch (Exception ex)
            {
                Common.WriteToFile(ex);
            }
        }

        private void populateTransactionDates()
        {
            DateTime tempDate;

            ddTransactionDates.Items.Clear();

            if (!File.Exists(Application.StartupPath + "\\data\\logs"))
                Directory.CreateDirectory(Application.StartupPath + "\\data\\logs");

            foreach (string name in Directory.GetFiles(Application.StartupPath + "\\data\\logs"))
            {
                if (name.Contains("\\Transactions_"))
                {
                    if (DateTime.TryParse(name.Replace("Transactions_", "").Replace(Application.StartupPath + "\\data\\logs\\", "").Replace(".xml", "").Trim(), out tempDate))
                    {
                        ddTransactionDates.Items.Add(tempDate.ToShortDateString());

                        //if (tempDate.ToShortDateString() == DateTime.Now.ToShortDateString())
                        //    ddTransactionDates.SelectedItem = tempDate.ToShortDateString();
                    }
                }
            }

            ddTransactionDates.Items.Insert(0, "Choose Date To View");
            //ddTransactionDates.SelectedIndex = 0;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ddSearchString = txtSearchBox.Text.Trim();

            if (ddSearchString.Trim().Length > 0)
            {
                ddInventory.Items.Clear();

                if (ddSearchString.Length > txtSearchBox.Text.Length + 1)
                    ddSearchString = txtSearchBox.Text;

                Voodoo.Objects.InventoryItem inventoryItemToAdd = new Voodoo.Objects.InventoryItem();

                foreach (DataRow dr in dtInventoryItems.Rows)
                {
                    if (dr["Name"].ToString().ToLower().ToString().StartsWith(ddSearchString, true, System.Globalization.CultureInfo.InvariantCulture)
                            || dr["Name"].ToString().ToLower().ToString().Contains(" " + ddSearchString.ToLower()))
                    {
                        ddInventory.Items.Add(common.FindItemInInventory(int.Parse(dr["id"].ToString())));
                    }
                }

                Voodoo.Objects.InventoryItem item = new Voodoo.Objects.InventoryItem();
                item.Name = "Select a FILTERED Item";
                item.UPC = "";
                item.ID = -1;

                ddInventory.Items.Insert(0, item);
                ddInventory.SelectedIndex = 0;
            }
            else
                populateInventoryDD();
        }

        private void populateInventoryDD()
        {
            txtSearchBox.Text = "";

            ddInventory.Text = "";

            ddInventory.Items.Clear();

            Voodoo.Objects.InventoryItem item = null;
            //dtInventoryItems = xmlData.Select("*", "name asc", XmlData.Tables.InventoryItems);
            double salePrice = 0;
            double price = 0;
            bool onSale = false;

            if (dtInventoryItems == null)
                getInventory();

            if (dtInventoryItems != null)
            {
                foreach (DataRow dr in dtInventoryItems.Rows)
                {
                    //item = (Voodoo.Objects.InventoryItem)dr;

                    item = new Voodoo.Objects.InventoryItem();
                    item.Name = dr["name"].ToString();
                    item.UPC = dr["UPC"].ToString();
                    item.ID = int.Parse(dr["id"].ToString());

                    double.TryParse(dr["price"].ToString(), out price);
                    item.Price = price;

                    bool.TryParse(dr["onSale"].ToString(), out onSale);
                    item.OnSale = onSale;

                    double.TryParse(dr["salePrice"].ToString(), out salePrice);
                    item.SalePrice = salePrice;

                    ddInventory.Items.Add(item);
                }
            }

            item = new Voodoo.Objects.InventoryItem();
            item.Name = "Select/Scan an Item";
            item.UPC = "";
            item.ID = -1;

            ddInventory.Items.Insert(0, item);
            ddInventory.SelectedIndex = 0;
        }

        private void getInventory()
        {
            //dtInventoryItems = xmlData.Select("active=true", "name asc", XmlData.Tables.InventoryItems);
            dtInventoryItems = xmlData.Select("*", "name asc", XmlData.Tables.InventoryItems);
        }

        private void reportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mode = modes.Reports;

            this.Close();
        }

        private void checkoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }
    }
}
