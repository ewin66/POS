using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Globalization;

namespace VoodooPOS
{
    public partial class Reports : Form
    {
        Common common = new Common(Application.StartupPath);
        XmlData xmlData = new XmlData(Application.StartupPath);

        public enum TransactionType
        {
            All, Cash, Credit
        }

        public modes mode = modes.Reports;

        public enum modes
        {
            Inventory, Checkout, Reports, Edit, Locked
        }

        public string UPC
        {
            set 
            {
                foreach (DataGridViewCell selectedCell in dgvReport.SelectedCells)
                {
                    if (dgvReport.Columns[selectedCell.ColumnIndex].Name.ToLower() == "upc")
                        selectedCell.Value = value;
                }
            }
        }

        public Reports()
        {
            InitializeComponent();

            DataTable dtInventoryItems = xmlData.Select("*", "name asc", XmlData.Tables.InventoryItems);
            Voodoo.Objects.InventoryItem item;

            if (dtInventoryItems != null)
            {
                double price = 0;
                bool onSale = false;
                double salePrice = 0;

                foreach (DataRow dr in dtInventoryItems.Rows)
                {
                    //item = (InventoryItem)dr;

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

                    ddInventoryItems.Items.Add(item);
                }
            }

            item = new Voodoo.Objects.InventoryItem();
            item.Name = "Select an Item to view";
            item.UPC = "";
            item.ID = -1;

            ddInventoryItems.Items.Insert(0, item);
            ddInventoryItems.SelectedIndex = 0;

            //dtInventoryItems.DefaultView.Sort = "name asc";

            //dgvReport.DataSource = dtInventoryItems;

            //ddInventoryItems.DataSource = dtInventoryItems;

            //start with displaying the end of day report
            endOfDayToolStripMenuItem_Click(null, null);

            common.inventoryUpdated += new Common.InventoryUpdated(common_inventoryUpdated);

            //set background image
            //if (File.Exists(Application.StartupPath + Properties.Settings.Default.BackgroundImagePath))
            //    this.BackgroundImage = Image.FromFile(Application.StartupPath + Properties.Settings.Default.BackgroundImagePath);
        }

        void common_inventoryUpdated()
        {
            
        }

        private void populateAllTransactions()
        {
            DataTable dtTempTable = new DataTable();

            if (!File.Exists(Application.StartupPath + "\\data\\logs"))
                Directory.CreateDirectory(Application.StartupPath + "\\data\\logs");

            foreach (string name in Directory.GetFiles(Application.StartupPath + "\\data\\logs"))
            {
                if (name.Contains("\\Transactions_"))
                {
                    //ddTransactionDates.Items.Add(name.Replace("Transactions_", "").Replace(Application.StartupPath + "\\data\\logs\\", "").Replace(".xml", "").Trim());
                    DataTable dtTransactions = xmlData.Select("*", "datecreated asc", "data\\logs\\Transactions_" + name.Substring(name.LastIndexOf("_") + 1).ToLower().Replace(".xml",""), "InventoryItems");

                    if (dtTransactions != null)
                        dtTempTable.Merge(dtTransactions);
                }
            }

            if (dtTempTable != null)
                dgvReport.DataSource = dtTempTable;
        }

        private void populateTransactionDates()
        {
            ddTransactionDates.Items.Clear();

            if (!File.Exists(Application.StartupPath + "\\data\\logs"))
                Directory.CreateDirectory(Application.StartupPath + "\\data\\logs");

            foreach (string name in Directory.GetFiles(Application.StartupPath + "\\data\\logs"))
            {
                if (name.Contains("\\Transactions_"))
                    ddTransactionDates.Items.Add(name.Replace("Transactions_", "").Replace(Application.StartupPath + "\\data\\logs\\", "").Replace(".xml", "").Trim());
            }

            ddTransactionDates.Items.Insert(0, "Choose Date To View");
            ddTransactionDates.SelectedIndex = 0;
        }

        /// <summary>
        /// Fill dropdown with dates of available transactions
        /// </summary>
        private void populateTransactionTimes()
        {
            if (ddTransactionDates.SelectedItem != null && ddTransactionDates.SelectedIndex > 0)
            {
                dgvReport.DataSource = null;

                Voodoo.Objects.InventoryItem item = null;
                DataTable dtInventory = xmlData.Select("*", "datecreated asc", "data\\logs\\Transactions_" + ddTransactionDates.SelectedItem, "data\\InventoryItems");
                DateTime tempDate;
                double price = 0;

                if (dtInventory != null)
                {
                    pnlNoResults.Visible = false;
                    dgvReport.DataSource = dtInventory;

                    getTotal();

                    populateGeneralInfo(dtInventory);
                }
                else
                    pnlNoResults.Visible = true;
            }
        }

        /// <summary>
        /// populate the general info for the report
        /// </summary>
        private void populateGeneralInfo(DataTable dtResults)
        {
            double cashTransactionTotal = 0;
            int cashTransactionCount = 0;
            double creditTransactionTotal = 0;
            int creditTransactionCount = 0;
            double registerTotal = 0;
            int registerTotalTransactionCount = 0;
            int voidTrans = 0;
            int voidTransactionCount = 0;
            int noSaleTrans = 0;
            int noSaleTransactionCount = 0;
            int totalTrans = 0;
            double taxableAmount = 0;
            double subTotal = 0;
            double tax = 0;
            double total = 0;
            double totalTemp = 0;
            double discountPercent = 0;
            int discountPercentTransactionCount = 0;
            double discountAmount = 0;
            int discountAmountTransactionCount = 0;
            double refunds = 0;
            int refundsTransactionCount = 0;
            double voidedTrans = 0;

            double priceTemp = 0;
            double taxTemp = 0;

            //iterate rows and fill in info
            foreach (DataRow dr in dtResults.Rows)
            {
                if (dr["Name"].ToString().StartsWith("New Cart_"))//this is a new transaction
                {
                    if (dr["Description"].ToString() == "Cash Transaction")//this is a cash transaction
                    {
                        if (double.TryParse(dr["Price"].ToString(), out priceTemp))
                            cashTransactionTotal += priceTemp;

                        cashTransactionCount += 1;
                    }
                    else //this is a credit transaction
                    {
                        if (double.TryParse(dr["Price"].ToString(), out priceTemp))
                            creditTransactionTotal += priceTemp;

                        creditTransactionCount += 1;
                    }

                    if (double.TryParse(dr["Price"].ToString(), out priceTemp))
                        registerTotal += priceTemp;

                    registerTotalTransactionCount += 1;

                    voidTrans = 0;
                    voidTransactionCount = 0;
                    noSaleTrans = 0;
                    noSaleTransactionCount = 0;

                    totalTrans += 1;

                    if (double.TryParse(dr["Price"].ToString(), out priceTemp) && double.TryParse(dr["Tax"].ToString(), out taxTemp))
                        taxableAmount += priceTemp - taxTemp;

                    if (double.TryParse(dr["Price"].ToString(), out priceTemp) && double.TryParse(dr["Tax"].ToString(), out taxTemp))
                        subTotal += priceTemp - taxTemp;

                    if (double.TryParse(dr["Tax"].ToString(), out taxTemp))
                        tax += taxTemp;

                    if (double.TryParse(dr["Price"].ToString(), out priceTemp))
                        total += priceTemp;

                    discountPercent = 0;
                    discountPercentTransactionCount = 0;
                    discountAmount = 0;
                    discountAmountTransactionCount = 0;
                    refunds = 0;
                    refundsTransactionCount = 0;
                    voidedTrans = 0;
                }
            }

            lblCashCount.Text = cashTransactionCount.ToString();
            lblCashTotal.Text = cashTransactionTotal.ToString("C", CultureInfo.CurrentCulture);
            lblVisaMastercardCount.Text = creditTransactionCount.ToString();
            lblVisaMastercardTotal.Text = creditTransactionTotal.ToString("C", CultureInfo.CurrentCulture);
            lblRegisterTotalCount.Text = registerTotalTransactionCount.ToString();
            lblRegisterTotalTotal.Text = registerTotal.ToString("C", CultureInfo.CurrentCulture);
            lblVoidTransactionsCount.Text = voidTransactionCount.ToString();
            lblVoidTransactionsTotal.Text = voidedTrans.ToString("C", CultureInfo.CurrentCulture);
            lblNoSaleCount.Text = noSaleTransactionCount.ToString();
            lblTotalTransactionsCount.Text = totalTrans.ToString();
            lblTaxable1Total.Text = taxableAmount.ToString("C", CultureInfo.CurrentCulture);
            lblTaxable2Total.Text = (0).ToString("C", CultureInfo.CurrentCulture);//this needs to come from another register
            lblSubtotalTotal.Text = subTotal.ToString("C", CultureInfo.CurrentCulture);
            lblTax1Total.Text = tax.ToString("C", CultureInfo.CurrentCulture);
            lblTax2Total.Text = (0).ToString("C", CultureInfo.CurrentCulture);//this needs to come from another register
            lblTotalTaxTotal.Text = (tax + 0 + subTotal).ToString("C", CultureInfo.CurrentCulture);//this needs to come from another register
            lblPercentDiscountCount.Text = discountPercentTransactionCount.ToString();
            lblPercentDiscountTotal.Text = discountPercent.ToString("C", CultureInfo.CurrentCulture);
            lblMoneyDiscountCount.Text = discountAmountTransactionCount.ToString();
            lblMoneyDiscountTotal.Text = discountAmount.ToString("C", CultureInfo.CurrentCulture);
            lblRefundsCount.Text = refundsTransactionCount.ToString();
            lblRefundsTotal.Text = refunds.ToString("C", CultureInfo.CurrentCulture);
            lblVoidTransactionsCount.Text = voidTransactionCount.ToString();
            lblVoidTransactionsTotal.Text = voidedTrans.ToString("C", CultureInfo.CurrentCulture);
        }

        private void dgvReport_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            common.UpdateItemInInventory(dgvReport.Rows[e.RowIndex]);
        }

        /// <summary>
        /// Display a report of the activity for the day with the option to change days
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void endOfDayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem selectedItem = (ToolStripMenuItem)sender;

            dgvReport.DataSource = common.GetTransactions(DateTime.Now);

            populateTransactionDates();

            lblReportTitle.Text = "End of Day Report";
        }

        /// <summary>
        /// display the transactions for the day selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ddTransactionDates_SelectedIndexChanged(object sender, EventArgs e)
        {
            populateTransactionTimes();
        }

        /// <summary>
        /// display all existing transactions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void overallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            populateAllTransactions();
        }

        private void getTotal()
        {
            double total = 0;
            double tempTotal = 0;
            bool onSale = false;

            //add totals
            foreach (DataGridViewRow dr in dgvReport.Rows)
            {
                if (dr.Cells["name"].Value != null && !dr.Cells["name"].Value.ToString().StartsWith("New Cart_"))
                {
                    if (dr.Cells["OnSale"].Value != null && bool.TryParse(dr.Cells["OnSale"].Value.ToString(), out onSale))
                    {
                        if (dr.Cells["salePrice"].Value != null && double.TryParse(dr.Cells["salePrice"].Value.ToString(), out tempTotal))
                            total += tempTotal;
                    }
                    else
                        if (dr.Cells["salePrice"].Value != null && double.TryParse(dr.Cells["price"].Value.ToString(), out tempTotal))
                            total += tempTotal;
                }
            }

            //lblTotal.Text = total.ToString("C");
        }

        private double getTotal(DataTable dtTotalToGet)
        {
            double total = 0;
            double tempTotal = 0;
            bool onSale = false;

            foreach (DataRow dr in dtTotalToGet.Rows)
            {
                if (dr["name"] != null && !dr["name"].ToString().StartsWith("New Cart_"))
                {
                    tempTotal = 0;

                    if (dr["OnSale"] != null && bool.TryParse(dr["OnSale"].ToString(), out onSale))
                    {
                        if (dr["salePrice"] != null && double.TryParse(dr["salePrice"].ToString(), out tempTotal))
                            total += tempTotal;
                    }
                    
                    if(tempTotal == 0)
                        if (dr["price"] != null && double.TryParse(dr["price"].ToString(), out tempTotal))
                            total += tempTotal;
                }
            }

            return total;
        }

        private double getTotal(DataTable dtTotalToGet, TransactionType cashOrCredit)
        {
            double total = 0;
            double tempTotal = 0;
            bool onSale = false;
            TransactionType currentTransactionType = TransactionType.All;

            foreach (DataRow dr in dtTotalToGet.Rows)
            {
                if (dr["name"] != null && !dr["name"].ToString().StartsWith("New Cart_"))
                {
                    if (currentTransactionType == cashOrCredit)
                    {
                        tempTotal = 0;

                        if (dr["OnSale"] != null && bool.TryParse(dr["OnSale"].ToString(), out onSale))
                        {
                            if (dr["salePrice"] != null && double.TryParse(dr["salePrice"].ToString(), out tempTotal))
                                total += tempTotal;
                        }
                        
                        if(tempTotal == 0)
                            if (dr["price"] != null && double.TryParse(dr["price"].ToString(), out tempTotal))
                                total += tempTotal;
                    }
                }
                else if (dr["name"].ToString().StartsWith("New Cart_"))
                {
                    if (dr["description"].ToString() == "Cash Transaction")
                        currentTransactionType = TransactionType.Cash;
                    else
                        currentTransactionType = TransactionType.Credit;
                }
            }

            return total;
        }

        private void getAllTransactions()
        {
            getAllTransactions(TransactionType.All);
        }

        private void getAllTransactions(TransactionType transactionType)
        {
            DataTable dtTempTable = new DataTable();
            DataTable dtMonthlyTransactions = new DataTable();

            if (!File.Exists(Application.StartupPath + "\\data\\logs"))
                Directory.CreateDirectory(Application.StartupPath + "\\data\\logs");

            foreach (string name in Directory.GetFiles(Application.StartupPath + "\\data\\logs"))
            {
                if (name.Contains("\\Transactions_"))
                {
                    DataTable dtTransactions = xmlData.Select("*", "datecreated asc", "logs\\Transactions_" + name.Substring(name.LastIndexOf("_") + 1).ToLower().Replace(".xml", ""), "InventoryItems");

                    if(dtMonthlyTransactions.Columns.Count == 0)
                        dtMonthlyTransactions = dtTransactions.Clone();

                    if (dtTransactions != null)
                    {
                        Voodoo.Objects.InventoryItem currentTransactions = new Voodoo.Objects.InventoryItem();

                        switch (transactionType)
                        {
                            case TransactionType.Cash:
                            case TransactionType.Credit:
                                currentTransactions.Price = getTotal(dtTransactions,transactionType);
                                break;
                            default:
                                currentTransactions.Price = getTotal(dtTransactions);
                                break;
                        }

                        currentTransactions.Name = name.Replace(Application.StartupPath + "\\data\\logs\\Transactions_", "");                        

                        DataRow drNew = dtMonthlyTransactions.NewRow();

                        Type type = currentTransactions.GetType();
                        var properties = type.GetProperties();

                        foreach (PropertyInfo property in properties)
                            drNew[property.Name] = property.GetValue(currentTransactions, null).ToString();

                        drNew["DateCreated"] = DateTime.Now;

                        dtMonthlyTransactions.Rows.Add(drNew);
                    }
                }
            }

            dgvReport.DataSource = dtMonthlyTransactions;
        }

        private void monthlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            getAllTransactions();
        }

        private void rb_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rbSender = (RadioButton)sender;

            switch (rbSender.Name)
            {
                case "rbAll":
                    getAllTransactions();
                    break;
                case "rbCash":
                    getAllTransactions(TransactionType.Cash);
                    break;
                case "rbCredit":
                    getAllTransactions(TransactionType.Credit);
                    break;
            }
        }

        private void giftCertificatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dtGiftCertificates = new DataTable();
            dtGiftCertificates = xmlData.Select("*", "dateCreated desc", XmlData.Tables.GiftCertificates.ToString());

            //if(dtGiftCertificates != null && dtGiftCertificates.Rows.Count > 0)
                dgvReport.DataSource = dtGiftCertificates;
        }

        private void saleItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //on sale
            DataTable dtSaleItems = xmlData.Select("*", "", "data\\" + XmlData.Tables.L_InventoryItemsToSalesCalendar.ToString());

            if (dtSaleItems != null)
            {
                string itemIDs = "";

                foreach (DataRow dr in dtSaleItems.Rows)
                {
                    if (itemIDs.Length > 0)
                        itemIDs += ",";

                    itemIDs += dr["inventoryItemID"].ToString();
                }

                DataTable dtProducts = xmlData.Select("id in (" + itemIDs + ")", "category asc", "data\\" + XmlData.Tables.InventoryItems.ToString());

                dgvReport.DataSource = dtProducts;

                populateTransactionDates();

                lblReportTitle.Text = "On Sale Item Report";
            }
        }

        private void ddInventoryItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            //display transaction history for selected item
            DataTable dtTransactions = null;
            DataTable dtTemp = null;

            Voodoo.Objects.InventoryItem selectedItem = (Voodoo.Objects.InventoryItem)ddInventoryItems.SelectedItem;

            if (selectedItem.ID > -1)
            {
                //look through all transactions for the one selected
                foreach (string file in Directory.GetFiles(Application.StartupPath + "\\data\\logs", "Transactions_*"))
                {
                    FileInfo fi = new FileInfo(file);

                    dtTemp = xmlData.Select("id = " + selectedItem.ID.ToString(), "", "data\\logs\\" + fi.Name.Replace(".xml", ""), "data\\InventoryItems");

                    if (dtTemp != null && dtTemp.Rows.Count > 0)
                    {
                        if (dtTransactions == null || dtTransactions.Rows.Count == 0)
                            dtTransactions = dtTemp.Copy();
                        else
                        {
                            foreach (DataRow dr in dtTemp.Rows)
                                dtTransactions.Rows.Add(dr.ItemArray);
                        }
                    }
                }

                if (dtTransactions != null)
                    dtTransactions.DefaultView.Sort = "DateCreated desc";

                dgvReport.DataSource = dtTransactions;
            }
        }

        private void checkoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mode = modes.Checkout;
            this.Close();
        }

        private void inventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mode = modes.Inventory;
            this.Close();
        }

        private void featuredItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //featured
            DataTable dtFeatured = xmlData.Select("*", "", "data\\" + XmlData.Tables.L_InventoryItemsToFeaturedItems.ToString());

            if (dtFeatured != null)
            {
                string itemIDs = "";

                foreach (DataRow dr in dtFeatured.Rows)
                {
                    if (itemIDs.Length > 0)
                        itemIDs += ",";

                    itemIDs += dr["inventoryItemID"].ToString();
                }

                DataTable dtProducts = xmlData.Select("id in (" + itemIDs + ")", "category asc", "data\\" + XmlData.Tables.InventoryItems.ToString());

                dgvReport.DataSource = dtProducts;

                populateTransactionDates();

                lblReportTitle.Text = "Featured Item Report";
            }
        }

        private void quickButtonsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //quick buttons
            DataTable dtQuickButtons = xmlData.Select("*", "", "data\\" + XmlData.Tables.L_InventoryItemsToQuickButtons.ToString());

            if (dtQuickButtons != null)
            {
                string itemIDs = "";

                foreach (DataRow dr in dtQuickButtons.Rows)
                {
                    if (itemIDs.Length > 0)
                        itemIDs += ",";

                    itemIDs += dr["inventoryItemID"].ToString();
                }

                DataTable dtProducts = xmlData.Select("id in (" + itemIDs + ")", "category asc", "data\\" + XmlData.Tables.InventoryItems.ToString());

                dgvReport.DataSource = dtProducts;

                populateTransactionDates();

                lblReportTitle.Text = "Quick Button Item Report";
            }
        }
    }
}
