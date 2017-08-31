using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Drawing.Printing;
using System.IO;
using Gma.UserActivityMonitor;

namespace VoodooPOS
{
    public partial class CashRegister : Form
    {
        ArrayList arSavedCarts = new ArrayList();
        ArrayList arCart = new ArrayList();
        Voodoo.Objects.InventoryItem currentItem;
        Voodoo.Objects.InventoryItem clickedItem;
        XmlData xmlData;
        Common common;
        double taxRate = 8.55;
        Modes mode = Modes.Locked;
        SerialPort cashDrawerPort = new SerialPort();
        double cartTotal = 0;
        double cartSubTotal = 0;
        double discount = 0;
        string ddSearchString = "";
        DataTable dtCategories;
        DataTable dtInventoryItems;
        InventoryItemEdit editItemWindow;
        Reports reportWindow;
        ApplicationProperties applicationPropertiesWindow;
        InventoryWindow inventoryWindow;
        bool stoppedTyping = false;
        int keystrokeTimerCounter = 0;
        double minimumCreditCharge = 0;

        public Voodoo.Objects.Employee Employee;
        
        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(System.Windows.Forms.Keys vKey); // Keys enumeration
        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(System.Int32 vKey);
        private StringBuilder keyBuffer;
        int labelCounter = 0;
        int maxLabels = 4;
        int labelRow = -1;
        int labelColumn = -1;
        DataSet dsLayout;

        #region toggles
        public static bool ControlKey
        {
            get { return Convert.ToBoolean(GetAsyncKeyState(Keys.ControlKey) & 0x8000); }
        } // ControlKey
        public static bool ShiftKey
        {
            get { return Convert.ToBoolean(GetAsyncKeyState(Keys.ShiftKey) & 0x8000); }
        } // ShiftKey
        public static bool CapsLock
        {
            get { return Convert.ToBoolean(GetAsyncKeyState(Keys.CapsLock) & 0x8000); }
        } // CapsLock
        public static bool AltKey
        {
            get { return Convert.ToBoolean(GetAsyncKeyState(Keys.Menu) & 0x8000); }
        } // AltKey

        private bool tglAlt = false;
        private bool tglControl = false;
        private bool tglCapslock = false;
        #endregion

        public enum Modes
        {
            Inventory, Checkout, Reports, Edit, Locked, Admin
        }

        public CashRegister()
        {
            InitializeComponent();

#if Release
            if (!VoodooPOS.Common.Activation.isActivated())
                this.Close();
#endif

            try
            {
                keyBuffer = new StringBuilder();

                xmlData = new XmlData(Application.StartupPath);

                common = new Common(Application.StartupPath);
                common.inventoryUpdated += new Common.InventoryUpdated(getInventory);

                DataTable dtEmployee = xmlData.Select("*", "", XmlData.Tables.Employees);
                dtEmployee.WriteXmlSchema("EmployeesSchema.xsd");

                //set tax rate
                if (Properties.Settings.Default.TaxRate != null)
                    taxRate = Properties.Settings.Default.TaxRate;

                //minimumCreditCharge
                if (Properties.Settings.Default.MinimumCreditCharge != null)
                    minimumCreditCharge = Properties.Settings.Default.MinimumCreditCharge;
                
                //set background image
                if (File.Exists(Application.StartupPath + Properties.Settings.Default.BackgroundImagePath))
                {
                    pnlBackgroundImage.BackgroundImage = Image.FromFile(Application.StartupPath + Properties.Settings.Default.BackgroundImagePath);
                    pnlUI.BackgroundImage = pnlBackgroundImage.BackgroundImage;
                }

                pnlBackgroundImage.Visible = true;

                pbLogo.Parent = pnlBackgroundImage;

                //set logo image
                if (File.Exists(Application.StartupPath + Properties.Settings.Default.LogoImagePath))
                {
                    pbLogo.Image = Image.FromFile(Application.StartupPath + Properties.Settings.Default.LogoImagePath);
                    pbLogo.Visible = true;
                }
                else
                    pbLogo.Visible = false;

                //loadTemplate("Default");

                //getInventory();

                //populateTransactionDates();

                //populateTransactionTimesDD();

                

                clockTimer_Tick(null, null);

                //get upc code from scanner gun
                HookManager.KeyPress += HookManager_KeyPress;

                setMode(mode);

                //start with displaying the featured items
                displayFeaturedItems();

                //uses webcam to read barcodes
                //BarcodeScanner scanner = new BarcodeScanner();
                //scanner.FoundUPC += new BarcodeScanner.foundUPC(FoundUpc);
                //scanner.Show();
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

        private void HookManager_KeyPress(object sender, KeyPressEventArgs e)
        {
            int tempKeyChar = -1;

            //only add integers to the search box
            if (mode == Modes.Checkout && int.TryParse(e.KeyChar.ToString(), out tempKeyChar))
                txtSearchBox.Text += e.KeyChar.ToString();
            if (mode == Modes.Inventory && int.TryParse(e.KeyChar.ToString(), out tempKeyChar))
                txtSearchBox.Text += e.KeyChar.ToString();
        }

        public bool FoundUpc(string upc)
        {
            bool updateRecord = false;
            bool foundUpc = false;

            switch (mode)
            {
                case Modes.Checkout:
                    //check inventory for item
                    DataTable retTable = xmlData.Select("upc = '" + upc + "' or upc = '0"+ upc +"'", "Name asc", XmlData.Tables.InventoryItems);
                    Voodoo.Objects.InventoryItem item = null;

                    if (retTable == null || retTable.Rows.Count == 0) //check for just the first 10 digits to see if we get a match from before the bug fix
                    {
                        retTable = xmlData.Select("upc LIKE '" + upc.Substring(0, 9) + "*' or upc LIKE '0" + upc.Substring(0, 9) + "*'", "Name asc", XmlData.Tables.InventoryItems);

                        if (retTable != null && retTable.Rows.Count > 0)//ask to fix upc
                        {
                            if (retTable.Rows.Count == 1)
                            {
                                if (MessageBox.Show("This upc code needs updated.  Do you want to update the upc code?", "Update UPC?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    updateRecord = true;
                                }
                                else//don't update record
                                {
                                    updateRecord = false;
                                }
                            }
                            else
                                MessageBox.Show("There are multiple matches");
                        }
                    }

                    if (retTable != null && retTable.Rows.Count > 0)//add to cart
                    {
                        common.PlaySound();

                        foundUpc = true;
                        double price = 0;
                        double.TryParse(retTable.Rows[0]["Price"].ToString(), out price);

                        int quantity = 1;

                        item = new Voodoo.Objects.InventoryItem();
                        item.ID = int.Parse(retTable.Rows[0]["ID"].ToString());
                        item.Name = retTable.Rows[0]["name"].ToString();
                        item.Description = retTable.Rows[0]["Description"].ToString();
                        item.Manufacturer = retTable.Rows[0]["Manufacturer"].ToString();
                        item.Model = retTable.Rows[0]["Model"].ToString();
                        item.Price = price;
                        item.Color = retTable.Rows[0]["Color"].ToString();
                        item.Size = retTable.Rows[0]["Size"].ToString();
                        item.PicturePath = retTable.Rows[0]["PicturePath"].ToString();
                        item.UPC = retTable.Rows[0]["UPC"].ToString();
                        item.Quantity = quantity;

                        AddItem(item);

                        populateInventoryDD();

                        if (updateRecord)
                        {
                            item.UPC = upc;
                            common.UpdateItemInInventory(item);
                        }
                    }
                    else //check other datasources for the upc - like the gift certificates
                    {
                        retTable = xmlData.Select("upc = '" + upc + "'", "Name asc", XmlData.Tables.GiftCertificates);

                        if (retTable != null && retTable.Rows.Count > 0)
                        {
                            common.PlaySound();

                            foundUpc = true;
                            double amount = 0;
                            DateTime dtNow = DateTime.Now;

                            double.TryParse(retTable.Rows[0]["amount"].ToString(), out amount);
                            DateTime.TryParse(retTable.Rows[0]["dateCreated"].ToString(), out dtNow);

                            GiftCertificate giftCertificate = new GiftCertificate();
                            giftCertificate.Name = retTable.Rows[0]["name"].ToString();
                            giftCertificate.Amount = amount;
                            giftCertificate.DateCreated = dtNow;
                            giftCertificate.Status = retTable.Rows[0]["status"].ToString();
                            giftCertificate.UPC = retTable.Rows[0]["upc"].ToString();

                            //display gift certificate
                            GiftCertificateFormRedeem giftCertificateForm = new GiftCertificateFormRedeem(giftCertificate);
                            giftCertificateForm.ShowDialog();
                        }
                    }
                    break;
                case Modes.Inventory:
                    common.PlaySound();

                    Voodoo.Objects.InventoryItem scannedItem = common.FindItemInInventory(upc);

                    if (scannedItem == null)
                    {
                        NewInventoryItem newItem = new NewInventoryItem();
                        newItem.UPC = upc;

                        if (newItem.ShowDialog() == DialogResult.OK)
                        {
                            getInventory();

                            populateInventoryDD();
                        }

                        txtSearchBox.Text = "";
                        //txtSearchBox.Focus();
                    }
                    else
                    {
                        //Invoke((MethodInvoker)delegate
                        //{

                        foundUpc = true;

                            if (editItemWindow == null)
                            {
                                editItemWindow = new InventoryItemEdit(scannedItem);
                                editItemWindow.inventoryUpdated += new InventoryItemEdit.InventoryUpdated(getInventory);
                                editItemWindow.ShowDialog();
                                editItemWindow = null;
                            }
                            else
                                editItemWindow.UPC = upc;

                            //txtSearchBox.Focus();
                        //});
                    }

                    break;
                case Modes.Reports:
                    common.PlaySound();

                    foundUpc = true;
                     
                    if (reportWindow != null)
                        reportWindow.UPC = upc;
                    break;
            }

            return foundUpc;
        }

        private void setMode(Modes newMode)
        {
            mode = newMode;

            if (menuStrip1.Items.Count > 0)
            {
                foreach (ToolStripMenuItem menuItem in ((ToolStripMenuItem)menuStrip1.Items[0]).DropDownItems)
                {
                    if (menuItem.Name == "checkoutToolStripMenuItem" && newMode == Modes.Checkout)
                    {
                        menuItem.Checked = true;

                        this.BackColor = Color.DodgerBlue;

                        btnNew.Visible = false;
                        grpGridDisplay.Text = "Total Order:";

                        getInventory();

                        displayCart();

                        //populateInventoryDD();

                        populateCategories();

                        populateQuickButtons();

                        displayFeaturedItems();

                        //focusTimer.Enabled = true;
                        //focusTimer.Start();
                    }
                    else if (menuItem.Name == "inventoryToolStripMenuItem" && newMode == Modes.Inventory)
                    {
                        bool changeMode = true;

                        menuItem.Checked = true;

                        this.BackColor = Color.LightCyan;

                        btnNew.Visible = true;

                        //check cart and make sure it's empty
                        if (arCart.Count > 0)
                        {
                            DialogResult result = MessageBox.Show("Do you want to empty the cart and start taking Inventory?", "Start taking Inventory?", MessageBoxButtons.YesNo);

                            if (result == DialogResult.Yes)
                                changeMode = true;
                            else
                                changeMode = false;
                        }

                        if (changeMode)
                        {
                            //grpGridDisplay.Text = "Inventory:";
                            arCart.Clear();
                            //displayInventory();
                            //displayItem(new Voodoo.Objects.InventoryItem());

                            inventoryWindow = new InventoryWindow();
                            inventoryWindow.WindowState = FormWindowState.Maximized;
                            inventoryWindow.ShowDialog(this);

                            if (inventoryWindow.DialogResult == DialogResult.OK) //check mode
                            {
                                switch(inventoryWindow.mode)
                                {
                                    case InventoryWindow.modes.Checkout:
                                        setMode(Modes.Checkout);
                                        break;
                                    case InventoryWindow.modes.Edit:
                                        setMode(Modes.Edit);
                                        break;
                                    case InventoryWindow.modes.Locked:
                                        setMode(Modes.Locked);
                                        break;
                                    case InventoryWindow.modes.Reports:
                                        setMode(Modes.Reports);
                                        break;
                                    default:
                                        setMode(Modes.Checkout);
                                        break;
                                }
                            }
                            else
                                setMode(Modes.Checkout);

                            //focusTimer.Stop();
                            //focusTimer.Enabled = false;
                        }
                    }
                    else if (menuItem.Name == "reportsToolStripMenuItem" && newMode == Modes.Reports)
                    {
                        menuItem.Checked = true;

                        //this.BackColor = Color.LawnGreen;

                        btnNew.Visible = false;

                        grpGridDisplay.Text = "Reports:";
                        arCart.Clear();
                        //displayItem(new Voodoo.Objects.InventoryItem());

                        reportWindow = new Reports();
                        reportWindow.WindowState = FormWindowState.Maximized;
                        reportWindow.ShowDialog(this);

                        switch (reportWindow.mode)
                        {
                            case Reports.modes.Checkout:
                                setMode(Modes.Checkout);
                                break;
                            case Reports.modes.Inventory:
                                setMode(Modes.Inventory);
                                break;
                            default:
                                setMode(Modes.Checkout);
                                break;
                        }

                        focusTimer.Stop();
                        focusTimer.Enabled = false;
                    }
                    else if (menuItem.Name == "editToolStripMenuItem" && newMode == Modes.Edit)
                    {
                        //bool changeMode = true;

                        menuItem.Checked = true;

                        this.BackColor = Color.LightSkyBlue;

                        btnNew.Visible = true;

                        //check cart and make sure it's empty
                        //if (arCart.Count > 0)
                        //{
                        //    DialogResult result = MessageBox.Show("Do you want to empty the cart and start taking Inventory?", "Start taking Inventory?", MessageBoxButtons.YesNo);

                        //    if (result == DialogResult.Yes)
                        //        changeMode = true;
                        //    else
                        //        changeMode = false;
                        //}

                        //if (changeMode)
                        //{
                        //    grpGridDisplay.Text = "Inventory:";
                        //    arCart.Clear();
                        //    displayInventory();
                        //    displayItem(new Voodoo.Objects.InventoryItem());

                        //    focusTimer.Stop();
                        //    focusTimer.Enabled = false;
                        //}

                        //apply the right click "edit" menu

                    }
                    else if (menuItem.Name == "adminToolStripMenuItem" && mode == Modes.Admin)
                    {
                        menuItem.Checked = true;

                        btnNew.Visible = false;

                        grpGridDisplay.Text = "Application Settings:";
                        arCart.Clear();
                        //displayItem(new Voodoo.Objects.InventoryItem());

                        applicationPropertiesWindow = new ApplicationProperties();
                        applicationPropertiesWindow.WindowState = FormWindowState.Maximized;
                        applicationPropertiesWindow.ShowDialog(this);

                        switch (applicationPropertiesWindow.mode)
                        {
                            case VoodooPOS.ApplicationProperties.modes.Checkout:
                                setMode(Modes.Checkout);
                                break;
                            case VoodooPOS.ApplicationProperties.modes.Inventory:
                                setMode(Modes.Inventory);
                                break;
                            default:
                                setMode(Modes.Checkout);
                                break;
                        }

                        focusTimer.Stop();
                        focusTimer.Enabled = false;
                    }
                    else
                    {
                        menuItem.Checked = false;
                    }
                }
            }
            //txtSearchBox.Focus();
        }

        /// <summary>
        /// Fill dropdown with dates of available transactions
        /// </summary>
        private void populateTransactionTimesDD()
        {
            if (ddTransactionDates.SelectedItem != null && ddTransactionDates.SelectedIndex > 0)
            {
                ddTransactionTimes.Items.Clear();

                Voodoo.Objects.InventoryItem item = null;
                DataTable dtInventory = xmlData.Select("*", "datecreated asc", "data\\logs\\Transactions_" + ddTransactionDates.SelectedItem.ToString().Replace("/", "-"), "data\\InventoryItems");
                DateTime tempDate;
                double price = 0;

                if (dtInventory != null)
                {
                    foreach (DataRow dr in dtInventory.Rows)
                    {
                        if (dr["name"].ToString().StartsWith("New Cart_"))
                        {
                            if (DateTime.TryParse(dr["DateCreated"].ToString(), out tempDate))
                            {
                                double.TryParse(dr["price"].ToString(), out price);

                                //ddTransactionTimes.Items.Add(tempDate.ToShortDateString() + "-" + tempDate.ToShortTimeString() + " " + price.ToString("C"));
                                ddTransactionTimes.Items.Add(tempDate.ToShortTimeString() + " " + price.ToString("C"));
                            }
                        }
                    }
                }

                ddTransactionTimes.Items.Insert(0, "Choose Transaction To View");
                ddTransactionTimes.SelectedIndex = 0;
            }
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

            ddInventory.Items.Insert(0,item);
            ddInventory.SelectedIndex = 0;
        }

        private void displayInventory()
        {
            Invoke((MethodInvoker)delegate
            {
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
                        }
                    }
                }
                catch (Exception ex)
                {
                    Common.WriteToFile(ex);
                }                
            });
        }

        public void AddItem(Voodoo.Objects.InventoryItem itemToAdd)
        {
            try
            {
                Voodoo.Objects.InventoryItem existingItem;

                switch (mode)
                {
                    case Modes.Checkout:
                        displayItem(itemToAdd);

                        //Invoke((MethodInvoker)delegate
                        //{
                            existingItem = common.FindItemInCart(itemToAdd, arCart);

                            if (existingItem != null)
                            {
                                txtQuantity.Text = existingItem.Quantity.ToString();

                                btnUpdate.Text = "Update Cart";
                            }
                            else
                                btnUpdate.Text = "Add To Cart";

                            addItemToCart(currentItem);

                            displayCart();
                        //});

                        break;
                    case Modes.Inventory:
                        
                        //look upc up in inventory and see if we have a match
                        NewInventoryItem newItemForm = new NewInventoryItem();
                        newItemForm.NewItem = itemToAdd;
                        newItemForm.ShowDialog();

                        displayInventory();

                        break;
                    case Modes.Reports:

                        break;
                }
                
            }
            catch (Exception ex)
            {
                Common.WriteToFile(ex);
            }
        }

        private void displayCart()
        {
            dgvItemsToDisplay.Rows.Clear();
            discount = 0;

            foreach (object item in arCart)
            {
                if (item is Voodoo.Objects.InventoryItem)
                {
                    Voodoo.Objects.InventoryItem itemInCart = (Voodoo.Objects.InventoryItem)item;

                    if (itemInCart.Quantity > 0)
                    {
                        int n = dgvItemsToDisplay.Rows.Add();

                        dgvItemsToDisplay.Rows[n].Cells["id"].Value = itemInCart.ID;
                        dgvItemsToDisplay.Rows[n].Cells["UPC"].Value = itemInCart.UPC;
                        dgvItemsToDisplay.Rows[n].Cells["Quantity"].Value = itemInCart.Quantity;
                        dgvItemsToDisplay.Rows[n].Cells["items"].Value = itemInCart.Name;

                        if (itemInCart.OnSale && itemInCart.SalePrice != 0)
                        {
                            dgvItemsToDisplay.Rows[n].Cells["price"].Value = itemInCart.SalePrice.ToString("C");
                            dgvItemsToDisplay.Rows[n].Cells["price"].Style.ForeColor = Color.Red;

                            dgvItemsToDisplay.Rows[n].Cells["subtotal"].Value = (itemInCart.SalePrice * itemInCart.Quantity).ToString("C");
                            dgvItemsToDisplay.Rows[n].Cells["subtotal"].Style.ForeColor = Color.Red;
                        }
                        else
                        {
                            dgvItemsToDisplay.Rows[n].Cells["price"].Value = itemInCart.Price.ToString("C");

                            //                        dgvItemsToDisplay.Rows[n].Cells["subtotal"].Value = (item.Price * item.Quantity - discount).ToString("C");
                            dgvItemsToDisplay.Rows[n].Cells["subtotal"].Value = (itemInCart.Price * itemInCart.Quantity).ToString("C");
                        }
                    }
                }
                else if (item is GiftCertificate)
                {
                    GiftCertificate certificateInCart = (GiftCertificate)item;

                    int n = dgvItemsToDisplay.Rows.Add();

                    dgvItemsToDisplay.Rows[n].Cells["id"].Value = certificateInCart.ID;
                    dgvItemsToDisplay.Rows[n].Cells["UPC"].Value = certificateInCart.UPC;
                    dgvItemsToDisplay.Rows[n].Cells["quantity"].Value = 1;
                    dgvItemsToDisplay.Rows[n].Cells["items"].Value = certificateInCart.DisplayName;

                    dgvItemsToDisplay.Rows[n].Cells["price"].Value = (certificateInCart.AmountToApply * -1).ToString("C");

                    dgvItemsToDisplay.Rows[n].Cells["subtotal"].Value = (certificateInCart.AmountToApply * -1).ToString("C");

                    if (certificateInCart.AmountToApply > 0)
                    {
                        dgvItemsToDisplay.Rows[n].Cells["price"].Style.ForeColor = Color.Red;
                        dgvItemsToDisplay.Rows[n].Cells["subtotal"].Style.ForeColor = Color.Red;
                    }
                }
            }

            updateTotal();
        }

        private void addItemToCart(object itemToAdd)
        {
            bool foundItem = false;

            foreach (object item in arCart)
            {
                if (itemToAdd is Voodoo.Objects.InventoryItem && item is Voodoo.Objects.InventoryItem)
                {
                    Voodoo.Objects.InventoryItem newItem = (Voodoo.Objects.InventoryItem)itemToAdd;
                    Voodoo.Objects.InventoryItem itemInCart = (Voodoo.Objects.InventoryItem)item;

                    if (newItem.ID == itemInCart.ID)//this is our item
                    {
                        ((Voodoo.Objects.InventoryItem)item).Quantity += newItem.Quantity;

                        foundItem = true;

                        break;
                    }
                }
                else if (itemToAdd is GiftCertificate && item is GiftCertificate)
                {
                    GiftCertificate certificateToAdd = (GiftCertificate)itemToAdd;
                    GiftCertificate certificateInCart = (GiftCertificate)item;

                    if (certificateToAdd.UPC == certificateInCart.UPC)//this is our item
                    {
                        //item.Quantity += certificateToAdd.Quantity;

                        foundItem = true;

                        break;
                    }
                }
            }

            if (!foundItem)
                arCart.Add(itemToAdd);

            updateTotal();
        }

        //private void addItemToCart(GiftCertificate certificateToAdd)
        //{
        //    bool foundItem = false;

        //    foreach (GiftCertificate item in arCart)
        //    {
        //        //if (item.UPC == itemToAdd.UPC)//this is our item
        //        if (item.UPC == certificateToAdd.UPC)//this is our item
        //        {
        //            //item.Quantity += certificateToAdd.Quantity;

        //            foundItem = true;

        //            break;
        //        }
        //    }

        //    if (!foundItem)
        //        arCart.Add(certificateToAdd);

        //    updateTotal();
        //}

        private void displayFeaturedItems()
        {
            DataTable dtFeaturedItemIds = xmlData.Select("*", "", "data\\" + XmlData.Tables.L_InventoryItemsToFeaturedItems.ToString());

            if (dtFeaturedItemIds != null)
            {
                string featuredItemIds = "";

                lblSelectedCategory.Text = "Featured Items";

                foreach (DataRow dr in dtFeaturedItemIds.Rows)
                {
                    if (featuredItemIds.Length > 0)
                        featuredItemIds += ",";

                    featuredItemIds += dr["inventoryItemID"].ToString();
                }

                DataTable dtProducts = xmlData.Select("id in (" + featuredItemIds + ")", "name asc", "data\\" + XmlData.Tables.InventoryItems.ToString());

                if (dtProducts != null)
                {
                    flowLayoutPanelProducts.Visible = false;
                    flowLayoutPanelProducts.Controls.Clear();

                    foreach (DataRow dr in dtProducts.Rows)
                    {
                        GelButton newButton = new GelButton();
                        newButton.Text = dr["name"].ToString();
                        newButton.Tag = dr["id"].ToString();
                        newButton.Width = 120;
                        newButton.Height = 75;
                        newButton.Click += new EventHandler(btnItem_Click);

                        flowLayoutPanelProducts.Controls.Add(newButton);
                    }

                    flowLayoutPanelProducts.Visible = true;
                }
            }
        }

        private void displayItem(Voodoo.Objects.InventoryItem itemToDisplay)
        {
            Invoke((MethodInvoker)delegate
                {
                    if (this.Handle != null)
                    {
                        lblName.Text = itemToDisplay.Name;
                        lblDescription.Text = itemToDisplay.Description;
                        lblPrice.Text = "$" + itemToDisplay.Price.ToString("N2");

                        if (System.IO.File.Exists(itemToDisplay.PicturePath))
                            pictureBox1.BackgroundImage = new Bitmap(itemToDisplay.PicturePath);
                        else
                            pictureBox1.BackgroundImage = new Bitmap(Application.StartupPath + "\\data\\inventoryImages\\imageNotAvailable.jpg");

                        lblUpc.Text = itemToDisplay.UPC;
                        txtQuantity.Text = itemToDisplay.Quantity.ToString();

                        if (itemToDisplay.OnSale)
                        {
                            txtSalePrice.Text = itemToDisplay.SalePrice.ToString();

                            gbCurrentItem.BackColor = Color.LightBlue;
                        }
                        else
                        {
                            txtSalePrice.Text = "0";

                            gbCurrentItem.BackColor = Color.Transparent;
                        }

                        //if(itemToDisplay.ID > 0)
                        //    lblQuantity.Text = common.FindItemInInventory(itemToDisplay.ID).Quantity.ToString();

                        lblQuantity.Text = itemToDisplay.Quantity.ToString();

                        //select the currentItem
                        foreach (Voodoo.Objects.InventoryItem item in ddInventory.Items)
                        {
                            if (item.ID == itemToDisplay.ID)
                            {
                                ddInventory.Text = item.Name;
                            }
                        }

                        currentItem = itemToDisplay;

                        //txtQuantity.Focus();
                    }
                });
        }

        private void displayItem(GiftCertificate itemToDisplay)
        {
            Invoke((MethodInvoker)delegate
            {
                if (this.Handle != null)
                {
                    lblName.Text = itemToDisplay.Name;
                    //lblDescription.Text = itemToDisplay.Description;
                    lblPrice.Text = "$" + itemToDisplay.AmountToApply.ToString("N2");

                    //if (System.IO.File.Exists(itemToDisplay.PicturePath))
                    //    pictureBox1.BackgroundImage = new Bitmap(itemToDisplay.PicturePath);
                    //else
                    //    pictureBox1.BackgroundImage = new Bitmap(Application.StartupPath + "\\data\\inventoryImages\\imageNotAvailable.jpg");

                    lblUpc.Text = itemToDisplay.UPC;
                    txtQuantity.Text = "1";

                    
                    txtSalePrice.Text = "0";

                    gbCurrentItem.BackColor = Color.Transparent;

                    //currentItem = itemToDisplay;
                }
            });
        }

        private void updateTotal()
        {
            //double subtotal = 0;
            double tax = 0;
            double taxableSubTotal = 0;
            discount = 0;
            cartSubTotal = 0;

            //update total
            foreach (object cartObj in arCart)
            {
                int quantity = 1;
                double price = 0;

                if (cartObj is Voodoo.Objects.InventoryItem)
                {
                    Voodoo.Objects.InventoryItem item = (Voodoo.Objects.InventoryItem)cartObj;
                    DataTable dtSalesInfo = xmlData.Select("InventoryItemID = " + item.ID, "", "data\\" + XmlData.Tables.L_InventoryItemsToSalesCalendar.ToString());

                    if (dtSalesInfo != null)
                    {
                        double.TryParse(dtSalesInfo.Rows[0]["salePrice"].ToString(),out price);
                        discount += (item.Price - price) * item.Quantity;
                    }
                    else
                    {
                        price = item.Price;

                        if (item.Price < 0)
                            discount += item.Price * item.Quantity;
                    }

                    quantity = item.Quantity;

                    price = quantity * price;

                    if (!item.Name.StartsWith("Gift Certificate"))
                        taxableSubTotal += price;

                    cartSubTotal += price;
                }

                if (cartObj is GiftCertificate)
                {
                    GiftCertificate certificate = (GiftCertificate)cartObj;

                    cartSubTotal += certificate.AmountToApply * -1;

                    cartSubTotal = Math.Round(cartSubTotal, 2, MidpointRounding.ToEven);

                    discount += certificate.AmountToApply;
                }
            }

            //foreach (GiftCertificate certificate in arCart)
            //    subtotal += certificate.AmountToApply * -1;

            //tax = taxableSubTotal * (taxRate / 100);

            //tax = Math.Round(tax, 2, MidpointRounding.ToEven);

            tax = SCTech.Common.SalesTax.GetTax(taxableSubTotal);

            lblSubtotal.Text = string.Format("{0:N2}", cartSubTotal.ToString("N"));
            lblTax.Text = string.Format("{0:N2}", tax.ToString("N"));
            lblTotal.Text = string.Format("{0:N2}", (cartSubTotal + tax).ToString("N"));

            cartTotal = cartSubTotal + tax;

            lblDiscount.Text = string.Format("{0:N2}", discount);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ddInventory.SelectedItem != null)
            {
                int quantity = 0;
                int.TryParse(txtQuantity.Text.Trim(), out quantity);

                Voodoo.Objects.InventoryItem updatedItem = (Voodoo.Objects.InventoryItem)ddInventory.SelectedItem;
                updatedItem.Quantity = quantity;

                common.UpdateItemInCart(updatedItem, arCart);

                displayCart();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Voodoo.Objects.InventoryItem foundItem = common.FindItemInInventory(lblUpc.Text.Trim());

            if (foundItem != null)
                AddItem(foundItem);
            //addItemToCart(foundItem);
            //else
            //{
                //item wasn't found in inventory

            //}
        }

        /// <summary>
        /// apply a discount to current item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDiscount_Click(object sender, EventArgs e)
        {
            Voodoo.Objects.InventoryItem itemToDiscount = common.FindItemInCart(currentItem, arCart);

            if (itemToDiscount.Quantity == 0)
                itemToDiscount.Quantity = 1;

            Discount discount = new Discount(itemToDiscount);
            discount.ShowDialog();

            if (discount.CurrentItem != null)
            {
                common.UpdateItemInCart(discount.CurrentItem, arCart);

                displayCart();
            }
        }

        private void dgvOrder_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvItemsToDisplay.SelectedCells.Count > 0 && dgvItemsToDisplay.Rows[dgvItemsToDisplay.SelectedCells[0].RowIndex].Cells["items"].Value != null)
                {
                    switch (mode)
                    {
                        case Modes.Checkout:
                            object itemToDisplay = common.FindItemInCart(int.Parse(dgvItemsToDisplay.Rows[dgvItemsToDisplay.SelectedCells[0].RowIndex].Cells["id"].Value.ToString()), arCart);

                            if (itemToDisplay is Voodoo.Objects.InventoryItem)
                            {
                                btnUpdate.Text = "Update Cart";

                                if (itemToDisplay != null)
                                {
                                    populateInventoryDD();

                                    displayItem((Voodoo.Objects.InventoryItem)itemToDisplay);
                                }                                
                            }
                            else
                                displayItem(new Voodoo.Objects.InventoryItem());
                            break;
                        case Modes.Inventory:
                            //Voodoo.Objects.InventoryItem itemToEdit = common.FindItemInInventory(int.Parse(dgvItemsToDisplay.Rows[dgvItemsToDisplay.SelectedCells[0].RowIndex].Cells["id"].Value.ToString()));

                            //InventoryItemEdit editItem = new InventoryItemEdit(itemToEdit);
                            //editItem.ShowDialog();

                            //displayInventory();

                            //populateTransactionDates();

                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Common.WriteToFile(ex);
            }
        }

        private void checkoutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            setMode(Modes.Checkout);
        }

        private void inventoryToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            setMode(Modes.Inventory);
        }

        private void reportsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            setMode(Modes.Reports);
        }

        //put form in edit mode
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setMode(Modes.Edit);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            NewInventoryItem newItemForm = new NewInventoryItem();
            newItemForm.ShowDialog();

            if (newItemForm.DialogResult == DialogResult.OK)
            {
                displayInventory();

                populateTransactionDates();
            }
        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            common.OpenDrawer();
        }

        private void btnGiftCertificate_Click(object sender, EventArgs e)
        {
            GiftCertificateFormCreate giftCertificateForm = new GiftCertificateFormCreate();
            DialogResult result = giftCertificateForm.ShowDialog();
            GiftCertificate giftCertificate = new GiftCertificate();

            switch (result)
            {
                case DialogResult.OK://apply gift certificate
                    
                    //giftCertificate.DisplayName = "Gift Certificate";

                    //if (giftCertificateForm.CurrentGiftCertificate.Name.Trim().Length > 0)
                    //    giftCertificate.DisplayName += " for " + giftCertificateForm.CurrentGiftCertificate.Name;

                    //giftCertificate.Amount = giftCertificateForm.CurrentGiftCertificate.Amount;
                    ////giftCertificate.SalePrice = giftCertificateForm.Amount * -1;
                    //giftCertificate.Quantity = 1;
                    //giftCertificate.ID = giftCertificateForm.CurrentGiftCertificate.ID;

                    addItemToCart(giftCertificateForm.CurrentGiftCertificate);

                    displayCart();
                    break;
                case DialogResult.Retry:
                    GiftCertificateFormRedeem giftCertificateFormRedeem = new GiftCertificateFormRedeem();
                    result = giftCertificateFormRedeem.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        //giftCertificate.Name = "Gift Certificate";

                        //if (giftCertificateFormRedeem.CurrentGiftCertificate.Name.Trim().Length > 0)
                        //    giftCertificate.Name += " for " + giftCertificateFormRedeem.CurrentGiftCertificate.Name;

                        //giftCertificate.Price = giftCertificateFormRedeem.AmountToApply;
                        //giftCertificate.SalePrice = giftCertificateFormRedeem.AmountToApply * -1;
                        //giftCertificate.OnSale = true;
                        //giftCertificate.Quantity = 1;
                        //giftCertificate.ID = giftCertificateFormRedeem.CurrentGiftCertificate.ID;

                        //addItemToCart(giftCertificate);

                        addItemToCart(giftCertificateFormRedeem.CurrentGiftCertificate);

                        displayCart();
                    }
                    break;
                default:

                    break;
            }
        }

        private void btnOpenDrawer_Click(object sender, EventArgs e)
        {
            common.OpenDrawer();
        }

        /// <summary>
        /// removes the current item from the cart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemove_Click(object sender, EventArgs e)
        {
            Voodoo.Objects.InventoryItem itemInCart = common.FindItemInCart(currentItem, arCart);

            itemInCart.Quantity = itemInCart.Quantity - currentItem.Quantity;

            common.UpdateItemInCart(itemInCart, arCart);

            displayCart();

            displayItem(itemInCart);
        }

        private void keystrokeTimer1_Tick(object sender, EventArgs e)
        {
            keystrokeTimerCounter++;

            if (keystrokeTimerCounter > 0)
            {
                stoppedTyping = true;
                keystrokeTimer1.Enabled = false;

                double upc = -1;

                if (double.TryParse(txtSearchBox.Text, out upc) && txtSearchBox.Text.Trim().Length >= 10)//this is our upc - do a search
                    FoundUpc(txtSearchBox.Text.Trim());
            }

            foreach (System.Int32 i in Enum.GetValues(typeof(Keys)))
            {
                if (GetAsyncKeyState(i) == -32767)
                {
                    //Console.WriteLine(i.ToString()); // Outputs the pressed key code [Debugging purposes]
                    //EventLog.WriteEntry("keystroke", i.ToString());

                    switch (mode)
                    {
                        case Modes.Checkout:
                            //reset focusTimer
                            focusTimer.Stop();
                            focusTimer.Enabled = false;

                            //focusTimer.Enabled = true;
                            //focusTimer.Start();
                            break;
                    }

                    if (ControlKey)
                    {
                        if (!tglControl)
                        {
                            tglControl = true;
                            //keyBuffer.Append("<Ctrl=On>");
                        }
                    }
                    else
                    {
                        if (tglControl)
                        {
                            tglControl = false;
                            //keyBuffer.Append("<Ctrl=Off>");
                        }
                    }

                    if (AltKey)
                    {
                        if (!tglAlt)
                        {
                            tglAlt = true;
                            //keyBuffer.Append("<Alt=On>");
                        }
                    }
                    else
                    {
                        if (tglAlt)
                        {
                            tglAlt = false;
                            //keyBuffer.Append("<Alt=Off>");
                        }
                    }

                    if (CapsLock)
                    {
                        if (!tglCapslock)
                        {
                            tglCapslock = true;
                            //keyBuffer.Append("<CapsLock=On>");
                        }
                    }
                    else
                    {
                        if (tglCapslock)
                        {
                            tglCapslock = false;
                            //keyBuffer.Append("<CapsLock=Off>");
                        }
                    }

                    //if (Enum.GetName(typeof(Keys), i) == "LButton")
                    //    keyBuffer.Append(Environment.NewLine + "<LMouse>" + Environment.NewLine);
                    //else if (Enum.GetName(typeof(Keys), i) == "RButton")
                    //    keyBuffer.Append(Environment.NewLine + "<RMouse>" + Environment.NewLine);
                    //else if (Enum.GetName(typeof(Keys), i) == "Back")
                    //    keyBuffer.Append(Environment.NewLine + "<Backspace>" + Environment.NewLine);
                    if (Enum.GetName(typeof(Keys), i) == "Space")
                        keyBuffer.Append("<Space>");
                    //keyBuffer.Append(" ");
                    else if (Enum.GetName(typeof(Keys), i) == "Return" || Enum.GetName(typeof(Keys), i) == "Enter")
                        keyBuffer.Append(Environment.NewLine + "<Enter>" + Environment.NewLine);
                    else if (Enum.GetName(typeof(Keys), i) == "ControlKey")
                        continue;
                    else if (Enum.GetName(typeof(Keys), i) == "LControlKey")
                        continue;
                    else if (Enum.GetName(typeof(Keys), i) == "RControlKey")
                        continue;
                    else if (Enum.GetName(typeof(Keys), i) == "LControlKey")
                        continue;
                    else if (Enum.GetName(typeof(Keys), i) == "ShiftKey")
                        continue;
                    else if (Enum.GetName(typeof(Keys), i) == "LShiftKey")
                        continue;
                    else if (Enum.GetName(typeof(Keys), i) == "RShiftKey")
                        continue;
                    //else if (Enum.GetName(typeof(Keys), i) == "Delete")
                    //    keyBuffer.Append(Environment.NewLine + "<Del>" + Environment.NewLine);
                    //else if (Enum.GetName(typeof(Keys), i) == "Insert")
                    //    keyBuffer.Append(Environment.NewLine + "<Ins>" + Environment.NewLine);
                    //else if (Enum.GetName(typeof(Keys), i) == "Home")
                    //    keyBuffer.Append(Environment.NewLine + "<Home>" + Environment.NewLine);
                    //else if (Enum.GetName(typeof(Keys), i) == "End")
                    //    keyBuffer.Append(Environment.NewLine + "<End>" + Environment.NewLine);
                    else if (Enum.GetName(typeof(Keys), i) == "Tab")
                        keyBuffer.Append("<Tab>");
                    else if (Enum.GetName(typeof(Keys), i) == "Prior")
                        keyBuffer.Append("<Page Up>");
                    else if (Enum.GetName(typeof(Keys), i) == "PageDown")
                        keyBuffer.Append("<Page Down>");
                    //else if (Enum.GetName(typeof(Keys), i) == "LWin" || Enum.GetName(typeof(Keys), i) == "RWin")
                    //    keyBuffer.Append(Environment.NewLine + "<Win>" + Environment.NewLine);

                    if (i == 76 && tglControl && tglAlt)//display the password dialog - CTRL-ALT L    L=76
                    {
                        //passwordUI = new Password(password);

                        //passwordUI.Focus();
                        //passwordUI.BringToFront();

                        //DialogResult result = passwordUI.ShowDialog();

                        #region self closing messagebox

                        //                            Library code snippets
                        //Close a message box automatically
                        //Comments (4) PDF 5 interested By James Crowley, published on 14 Jul 2001  James first started this website when learning Visual Basic back in 1999 whilst studying his GCSEs. The site grew steadily over the years while being run as a hobby - to a regular monthly audience ... Many programs display message boxes that automatically close after a period of time, including printer errors and closing outlook. In VB, this is more complicated than it should be, but still possible. Here's how.

                        //'Module Code
                        //Private Declare Function KillTimer Lib "user32" (ByVal hWnd As Long, ByVal nIDEvent As Long) As Long
                        //Private Declare Function FindWindow Lib "user32" Alias "FindWindowA" (ByVal lpClassName As String, ByVal lpWindowName As String) As Long
                        //Private Declare Function SetForegroundWindow Lib "user32" (ByVal hWnd As Long) As Long
                        //'// Message we receive telling us to close the message box
                        //Public Const NV_CLOSEMSGBOX As Long = &H5000&
                        //Public Sub TimerProc(ByVal hWnd As Long, ByVal uMsg As Long, ByVal idEvent As Long, ByVal dwTime As Long)
                        //    '// this is a callback function.  This means that windows "calls back" to this function
                        //    '// when it's time for the timer event to fire
                        //    '// first thing we do is kill the timer so that no other timer events will fire
                        //    KillTimer hWnd, idEvent
                        //    '// select the type of manipulation that we want to perform
                        //    Select Case idEvent
                        //    Case NV_CLOSEMSGBOX '// we want to close this messagebox after 4 seconds
                        //        Dim hMessageBox As Long
                        //        '// find the messagebox window
                        //        '// change the text to whatever the title of the message box is
                        //        hMessageBox = FindWindow("#32770", "Self Closing Message Box")
                        //        '// if we found it make sure it has the keyboard focus and then send it an enter to dismiss it
                        //        If hMessageBox Then
                        //            Call SetForegroundWindow(hMessageBox)
                        //            '// this will result in the default option being chosen
                        //            SendKeys "{enter}"
                        //        End If
                        //    End Select
                        //End Sub


                        //'Form Code
                        //Private Declare Function SetTimer Lib "user32" (ByVal hWnd As Long, ByVal nIDEvent As Long, ByVal uElapse As Long, ByVal lpTimerFunc As Long) As Long

                        //Private Sub cmdShowMsg_Click()
                        //    '// this shows a messagebox that will be dismissed after 4 seconds

                        //    '// set the callback timer and pass our application defined ID (NV_CLOSEMSGBOX)
                        //    '// set the time for 4 seconds (4000 microseconds)
                        //    SetTimer hWnd, NV_CLOSEMSGBOX, 4000, AddressOf TimerProc

                        //    '// call the messagebox function
                        //    If MsgBox("Watch this message box close itself after four seconds. The printer is out of paper. Retry or Cancel? (Example)", vbRetryCancel + vbDefaultButton1, "Self Closing Message Box") = vbRetry Then
                        //        MsgBox "Retry!"
                        //    Else
                        //        MsgBox "Cancel"
                        //    End If

                        //End Sub 
                        #endregion

                        //Thread passwordThread = new Thread(passwordUI.Show);
                        //passwordThread.IsBackground = false;
                        //passwordThread.Start();

                        //passwordThread.Join();
                    }

                    /* ********************************************** *
                     * Detect key based off ShiftKey Toggle
                     * ********************************************** */
                    if (ShiftKey)
                    {
                        if (i >= 65 && i <= 122)
                        {
                            keyBuffer.Append((char)i);
                        }
                        else if (i.ToString() == "49")
                            keyBuffer.Append("!");
                        else if (i.ToString() == "50")
                            keyBuffer.Append("@");
                        else if (i.ToString() == "51")
                            keyBuffer.Append("#");
                        else if (i.ToString() == "52")
                            keyBuffer.Append("$");
                        else if (i.ToString() == "53")
                            keyBuffer.Append("%");
                        else if (i.ToString() == "54")
                            keyBuffer.Append("^");
                        else if (i.ToString() == "55")
                            keyBuffer.Append("&");
                        else if (i.ToString() == "56")
                            keyBuffer.Append("*");
                        else if (i.ToString() == "57")
                            keyBuffer.Append("(");
                        else if (i.ToString() == "48")
                            keyBuffer.Append(")");
                        else if (i.ToString() == "192")
                            keyBuffer.Append("~");
                        else if (i.ToString() == "189")
                            keyBuffer.Append("_");
                        else if (i.ToString() == "187")
                            keyBuffer.Append("+");
                        else if (i.ToString() == "219")
                            keyBuffer.Append("{");
                        else if (i.ToString() == "221")
                            keyBuffer.Append("}");
                        else if (i.ToString() == "220")
                            keyBuffer.Append("|");
                        else if (i.ToString() == "186")
                            keyBuffer.Append(":");
                        else if (i.ToString() == "222")
                            keyBuffer.Append("\"");
                        else if (i.ToString() == "188")
                            keyBuffer.Append("<");
                        else if (i.ToString() == "190")
                            keyBuffer.Append(">");
                        else if (i.ToString() == "191")
                            keyBuffer.Append("?");
                    }
                    else
                    {

                        if (i >= 65 && i <= 122)
                        {
                            keyBuffer.Append((char)(i + 32));
                        }
                        else if (i.ToString() == "49")
                            keyBuffer.Append("1");
                        else if (i.ToString() == "50")
                            keyBuffer.Append("2");
                        else if (i.ToString() == "51")
                            keyBuffer.Append("3");
                        else if (i.ToString() == "52")
                            keyBuffer.Append("4");
                        else if (i.ToString() == "53")
                            keyBuffer.Append("5");
                        else if (i.ToString() == "54")
                            keyBuffer.Append("6");
                        else if (i.ToString() == "55")
                            keyBuffer.Append("7");
                        else if (i.ToString() == "56")
                            keyBuffer.Append("8");
                        else if (i.ToString() == "57")
                            keyBuffer.Append("9");
                        else if (i.ToString() == "48")
                            keyBuffer.Append("0");
                        else if (i.ToString() == "189")
                            keyBuffer.Append("-");
                        else if (i.ToString() == "187")
                            keyBuffer.Append("=");
                        else if (i.ToString() == "92")
                            keyBuffer.Append("`");
                        else if (i.ToString() == "219")
                            keyBuffer.Append("[");
                        else if (i.ToString() == "221")
                            keyBuffer.Append("]");
                        else if (i.ToString() == "220")
                            keyBuffer.Append("\\");
                        else if (i.ToString() == "186")
                            keyBuffer.Append(");");
                        else if (i.ToString() == "222")
                            keyBuffer.Append("'");
                        else if (i.ToString() == "188")
                            keyBuffer.Append(",");
                        else if (i.ToString() == "190")
                            keyBuffer.Append(".");
                        else if (i.ToString() == "191")
                            keyBuffer.Append("/");
                    }

                    switch (keyBuffer.ToString())
                    {
                        case "\r\n<Enter>\r\n":
                            //int quantity = 0;
                            //int.TryParse(txtQuantity.Text.Trim(), out quantity);

                            //if (quantity == 0)
                            //    quantity = 1;

                            //Voodoo.Objects.InventoryItem updatedItem = currentItem;
                            //updatedItem.Quantity = quantity;

                            //common.UpdateItemInCart(updatedItem, arCart);

                            if (mode == Modes.Checkout && currentItem != null)
                            {
                                btnUpdate_Click(null, null);

                                displayCart();

                                keyBuffer.Remove(0, keyBuffer.Length); // reset
                            }
                            break;
                        case "<Backspace>":
                        case "<Del>":
                            //switch (mode)
                            //{
                            //    case Modes.Checkout:
                            //        Voodoo.Objects.InventoryItem itemInCart = common.FindItemInCart(currentItem, arCart);

                            //        itemInCart.Quantity = itemInCart.Quantity - currentItem.Quantity;

                            //        common.UpdateItemInCart(itemInCart, arCart);

                            //        displayCart();

                            //        displayItem(itemInCart);

                            //        keyBuffer.Remove(0, keyBuffer.Length); // reset

                            //        break;
                            //}
                            //break;
                        case "<Space>":
                            keyBuffer.Remove(0, keyBuffer.Length); // reset
                            //common.OpenDrawer();
                            break;
                    }

                    keyBuffer.Remove(0, keyBuffer.Length); // reset
                }
            }
        }

        /// <summary>
        /// Add misc item to order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMisc_Click(object sender, EventArgs e)
        {
            MiscItem miscItem = new MiscItem();
            miscItem.ShowDialog();

            if (miscItem.DialogResult == DialogResult.OK)
            {
                common.UpdateItemInCart(miscItem.NewItem, arCart);

                displayCart();
            }
        }

        private void btnTestIt_Click(object sender, EventArgs e)
        {
            //check inventory for item
            DataTable retTable = xmlData.Select("id = '1'", "Name asc", XmlData.Tables.InventoryItems);

            if (retTable != null && retTable.Rows.Count > 0)//display item information or add to cash register
            {
                double price = 0;
                double.TryParse(retTable.Rows[0]["Price"].ToString(), out price);

                int quantity = 1;
                
                Voodoo.Objects.InventoryItem item = new Voodoo.Objects.InventoryItem();
                item.Name = retTable.Rows[0]["name"].ToString();
                item.Description = retTable.Rows[0]["Description"].ToString();
                item.Manufacturer = retTable.Rows[0]["Manufacturer"].ToString();
                item.Model = retTable.Rows[0]["Model"].ToString();
                item.Price = price;
                item.Color = retTable.Rows[0]["Color"].ToString();
                item.Size = retTable.Rows[0]["Size"].ToString();
                item.PicturePath = retTable.Rows[0]["PicturePath"].ToString();
                item.UPC = retTable.Rows[0]["UPC"].ToString();
                item.Quantity = quantity;

                common.WriteToLog("testing", item);
            }
        }

        /// <summary>
        /// they are paying with cash
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCash_Click(object sender, EventArgs e)
        {
            checkout_cash checkout = new checkout_cash(arCart, cartTotal, double.Parse(lblTax.Text));
            DialogResult result = checkout.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                updateGiftCertificates();

                //clear cart
                if (MessageBox.Show("Would you like to clear the cart?", "Clear Cart?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    arCart.Clear();
                    displayCart();
                }
            }

            populateTransactionDates();

            txtSearchBox.Text = "";
            //txtSearchBox.Focus();
        }

        /// <summary>
        /// they are paying with credit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCredit_Click(object sender, EventArgs e)
        {
            if (cartTotal >= minimumCreditCharge)
            {
                //open drawer
                common.OpenDrawer();

                ArrayList cartToSave = arCart;

                Voodoo.Objects.InventoryItem newCart = new Voodoo.Objects.InventoryItem();
                newCart.Name = "New Cart_"+ Employee.ID.ToString() +"_" + DateTime.Now.ToShortTimeString();
                newCart.Description = "Credit Transaction";
                newCart.Price = cartTotal;

                cartToSave.Insert(0, newCart);

                common.RecordTransaction(cartToSave);

                updateGiftCertificates();

                cartToSave.Remove(newCart);

                //clear cart
                if (MessageBox.Show("Would you like to clear the cart?", "Clear Cart?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    arCart.Clear();
                    displayCart();
                }

                populateTransactionDates();

                txtSearchBox.Text = "";
                //txtSearchBox.Focus();
            }
            else
                MessageBox.Show("There is a $"+ minimumCreditCharge +" minimum on credit card transactions");
        }

        private void ddInventory_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetterOrDigit(e.KeyChar))
            {
                ddSearchStringTimer.Enabled = true;
                ddSearchStringTimer.Start();

                ddSearchString += e.KeyChar;

                if (ddSearchString.Trim().Length > 1)
                {
                    foreach (Voodoo.Objects.InventoryItem item in ddInventory.Items)
                    {
                        if (item.Name.ToLower().ToString().StartsWith(ddSearchString, true, System.Globalization.CultureInfo.InvariantCulture)
                                || item.Name.ToLower().ToString().Contains(" " + ddSearchString.ToLower()))
                        {
                            ddInventory.SelectedItem = item;

                            //ddSearchString = "";

                            break;
                        }

                        //if (item.Name.ToLower().Contains(e.KeyChar.ToString().ToLower(), true, System.Globalization.CultureInfo.InvariantCulture))
                        //{
                        //    ddInventory.SelectedItem = item;

                        //    break;
                        //}
                    }
                }

                //foreach (DataGridViewRow dgvRow in dgvItemsToDisplay.Rows)
                //{
                //    if (dgvRow.Cells["name"].FormattedValue.ToString().StartsWith(e.KeyChar.ToString(), true, CultureInfo.InvariantCulture)
                //            || dgvRow.Cells["name"].FormattedValue.ToString().Contains(" " + e.KeyChar.ToString(), true, CultureInfo.InvariantCulture))
                //    {
                //        dgvRow.Selected = true;
                //        break;
                //    }
                //}
            }

        }

        private void ddSearchStringTimer_Tick(object sender, EventArgs e)
        {
            ddSearchString = "";

            ddSearchStringTimer.Stop();
            ddSearchStringTimer.Enabled = false;
        }

        private void btnReceipt_Click(object sender, EventArgs e)
        {
            Printer printer = new Printer(Application.StartupPath);
            printer.PrintReceipt(chbGiftReceipt.Checked, arCart);            
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //clear cart
            if (MessageBox.Show("Would you like to clear the cart?", "Clear Cart?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //if (MessageBox.Show("Are you sure you would like to clear the cart?", "Clear Cart?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                //{
                //    if (MessageBox.Show("Are you really sure you would like to clear the cart?", "Clear Cart?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                //    {
                //        if (MessageBox.Show("Would you rather click the Credit or Cash buttons?", "Clear Cart?", MessageBoxButtons.YesNo) == DialogResult.No)
                //        {
                //            if (MessageBox.Show("So you really would like to clear the cart?", "Clear Cart?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                //            {
                                arCart.Clear();
                                displayCart();
                //            }
                //        }
                //    }
                //}
            }

        }

        private void txtQuantity_Enter(object sender, EventArgs e)
        {
            txtQuantity.SelectAll();
        }

        private void cOM1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            common.CashDrawerPortName = "COM1";
        }

        private void cOM2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            common.CashDrawerPortName = "COM2";
        }

        private void ddInventory_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //if (ddInventory.SelectedIndex > 0)
            //{
            //    Voodoo.Objects.InventoryItem selectedItem = (Voodoo.Objects.InventoryItem)ddInventory.SelectedItem;

            //    if (selectedItem.ID.ToString().Trim().Length > 0)
            //    {
            //        selectedItem = common.FindItemInInventory(selectedItem.ID);
            //        selectedItem.Quantity = 1;

            //        Voodoo.Objects.InventoryItem existingItem = common.FindItemInCart(selectedItem, arCart);

            //        if (existingItem != null)
            //            selectedItem.Quantity = (selectedItem.Quantity + existingItem.Quantity);

            //        if (txtSalePrice.Text.Trim().Length > 0 && txtSalePrice.Text.Trim() != "0")
            //        {
            //            double salePrice = 0;
            //            double.TryParse(txtSalePrice.Text.Trim(), out salePrice);

            //            selectedItem.Price = salePrice;
            //        }

            //        displayItem(selectedItem);

            //        txtQuantity.Focus();
            //    }
            //}
            //else
            //    if (this.Handle != null)
            //        displayItem(new Voodoo.Objects.InventoryItem());
        }

        private void ddInventory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddInventory.SelectedIndex > 0 || (ddInventory.SelectedItem is Voodoo.Objects.InventoryItem && ((Voodoo.Objects.InventoryItem)ddInventory.SelectedItem).ID > -1))
            {
                Voodoo.Objects.InventoryItem selectedItem = (Voodoo.Objects.InventoryItem)ddInventory.SelectedItem;

                switch (mode)
                {
                    case Modes.Checkout:
                        btnUpdate.Text = "Add To Cart";

                        if (selectedItem.ID > -1)
                        {
                            selectedItem = common.FindItemInInventory(selectedItem.ID);
                            selectedItem.Quantity = 1;

                            Voodoo.Objects.InventoryItem existingItem = common.FindItemInCart(selectedItem, arCart);

                            if (existingItem != null)
                            {
                                //selectedItem.Quantity = (selectedItem.Quantity + existingItem.Quantity);
                                selectedItem.Quantity = existingItem.Quantity;

                                btnUpdate.Text = "Update Cart";
                            }

                            //if (txtSalePrice.Text.Trim().Length > 0 && txtSalePrice.Text.Trim() != "0")
                            //{
                            //    double salePrice = 0;
                            //    double.TryParse(txtSalePrice.Text.Trim(), out salePrice);

                            //    selectedItem.Price = salePrice;
                            //}

                            displayItem(selectedItem);

                            txtQuantity.Focus();
                        }
                        break;
                    case Modes.Inventory:
                        //display the edit window for it
                        editItemWindow = new InventoryItemEdit(selectedItem);
                        editItemWindow.inventoryUpdated += new InventoryItemEdit.InventoryUpdated(getInventory);
                        editItemWindow.ShowDialog();

                        break;
                }                
            }
            else
                if (this.Handle != null)
                    displayItem(new Voodoo.Objects.InventoryItem());
        }
        
        private void ddTransactionTimes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddTransactionDates.SelectedItem != null && ddTransactionTimes.SelectedIndex > 0)
            {
                string selectedText = ddTransactionTimes.SelectedItem.ToString();
                string selectedDateAndTime = selectedText.Substring(0, selectedText.IndexOf(" $")).Replace("-"," ");

                DateTime selectedDateTime = DateTime.Now;
                DateTime.TryParse(selectedDateAndTime, out selectedDateTime);

                selectedText = selectedText.Substring(0, selectedText.IndexOf("-"));
                selectedText = selectedText.Replace("/", "-");

                //display selected cart
                Voodoo.Objects.InventoryItem item = null;
                DataTable dtInventory = xmlData.Select("*", "datecreated asc", "data\\logs\\Transactions_" + selectedText, "data\\InventoryItems");
                DateTime tempDate;
                bool currentCart = false;

                if (arCart.Count > 0)
                {
                    DialogResult result = MessageBox.Show("Do you want to add the existing cart to the selected cart?", "Clear Existing Cart", MessageBoxButtons.YesNo);

                    if (result == DialogResult.No)
                        arCart.Clear();
                }

                if (dtInventory != null)
                {
                    foreach (DataRow dr in dtInventory.Rows)
                    {
                        DateTime.TryParse(dr["datecreated"].ToString(), out tempDate);
                        if ((tempDate.ToShortDateString() + "-" + tempDate.ToShortTimeString()) == selectedDateTime.ToShortDateString() + "-" + selectedDateTime.ToShortTimeString() || currentCart)
                        {
                            currentCart = true;

                            if (dr["name"].ToString().StartsWith("New Cart_") && (tempDate.ToShortDateString() + "-" + tempDate.ToShortTimeString()) != selectedDateTime.ToShortDateString() + "-" + selectedDateTime.ToShortTimeString())
                            {
                                currentCart = false;

                                break;
                            }

                            double price = 0;
                            int quantity = 0;
                            int id = 0;

                            Voodoo.Objects.InventoryItem itemInCart = new Voodoo.Objects.InventoryItem();
                            itemInCart.Name = dr["name"].ToString();

                            double.TryParse(dr["price"].ToString(), out price);
                            itemInCart.Price = price;

                            int.TryParse(dr["quantity"].ToString(), out quantity);
                            itemInCart.Quantity = quantity;

                            int.TryParse(dr["id"].ToString(), out id);
                            itemInCart.ID = id;

                            common.UpdateItemInCart(itemInCart, arCart);

                            displayCart();
                        }
                    }
                }
            }
        }

        private void ddTransactionDates_SelectedIndexChanged(object sender, EventArgs e)
        {
            populateTransactionTimesDD();
        }

        private void btnPrintGiftCertificate_Click(object sender, EventArgs e)
        {
            //GiftCertificateFormRedeem giftCertificate = new GiftCertificateFormRedeem();
            //giftCertificate.Mode = GiftCertificateFormRedeem.mode.Create;
            //giftCertificate.ShowDialog();

            //Printer printer = new Printer(Application.StartupPath);
            //printer.PrintGiftCertificate();
        }

        /// <summary>
        /// save the current cart so it can be used later
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveCart_Click(object sender, EventArgs e)
        {
            VoodooPOS.objects.Cart currentCart = new VoodooPOS.objects.Cart();
            currentCart.CartToSave = (ArrayList)arCart.Clone();
            currentCart.Name = "Testing "+ DateTime.Now.ToShortTimeString();

            arSavedCarts.Add(currentCart);

            arCart.Clear();

            displayCart();

            ddSavedCarts.Items.Add(currentCart);
        }

        private void ddSavedCarts_SelectedIndexChanged(object sender, EventArgs e)
        {
            VoodooPOS.objects.Cart savedCart = (VoodooPOS.objects.Cart)ddSavedCarts.SelectedItem;

            arCart = savedCart.CartToSave;

            displayCart();
        }

        private void dgvItemsToDisplay_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (dgvItemsToDisplay.SelectedCells.Count > 0 && dgvItemsToDisplay.Rows[dgvItemsToDisplay.SelectedCells[0].RowIndex].Cells["items"].Value != null)
                {
                    switch (mode)
                    {
                        case Modes.Checkout:
                            //Voodoo.Objects.InventoryItem itemToDisplay = common.FindItemInCart(int.Parse(dgvItemsToDisplay.Rows[dgvItemsToDisplay.SelectedCells[0].RowIndex].Cells["id"].Value.ToString()), arCart);

                            //displayItem(itemToDisplay);
                            break;
                        case Modes.Inventory:
                            Voodoo.Objects.InventoryItem itemToEdit = common.FindItemInInventory(int.Parse(dgvItemsToDisplay.Rows[dgvItemsToDisplay.SelectedCells[0].RowIndex].Cells["id"].Value.ToString()));

                            editItemWindow = new InventoryItemEdit(itemToEdit);
                            editItemWindow.inventoryUpdated +=new InventoryItemEdit.InventoryUpdated(getInventory);
                            editItemWindow.ShowDialog();

                            populateTransactionDates();

                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Common.WriteToFile(ex);
            }
        }

        private void txtSearchBox_KeyUp(object sender, KeyEventArgs e)
        {
            ////if (Char.IsLetterOrDigit(e.KeyChar))
            ////{
            ////ddSearchStringTimer.Enabled = true;
            ////ddSearchStringTimer.Start();

            ////ddSearchString += e.KeyChar;

            //ddSearchString = txtSearchBox.Text.Trim();

            ////if (e.KeyChar.ToString() == "\b")
            ////{
            ////    if (ddSearchString.Length >= 2)
            ////        ddSearchString = ddSearchString.Substring(0, ddSearchString.Length - 2);
            ////    else
            ////        ddSearchString = "";
            ////}

            //if (ddSearchString.Trim().Length > 1)
            //{
            //    ddInventory.Items.Clear();

            //    if (ddSearchString.Length > txtSearchBox.Text.Length + 1)
            //        ddSearchString = txtSearchBox.Text;

            //    Voodoo.Objects.InventoryItem inventoryItemToAdd = new Voodoo.Objects.InventoryItem();

            //    foreach (DataRow dr in dtInventoryItems.Rows)
            //    {
            //        if (dr["Name"].ToString().ToLower().ToString().StartsWith(ddSearchString, true, System.Globalization.CultureInfo.InvariantCulture)
            //                || dr["Name"].ToString().ToLower().ToString().Contains(" " + ddSearchString.ToLower()))
            //        {
            //            ddInventory.Items.Add(common.FindItemInInventory(int.Parse(dr["id"].ToString())));
            //        }
            //    }

            //    Voodoo.Objects.InventoryItem item = new Voodoo.Objects.InventoryItem();
            //    item.Name = "Select a FILTERED Item";
            //    item.UPC = "";
            //    item.ID = -1;

            //    ddInventory.Items.Insert(0, item);
            //    //ddInventory.SelectedIndex = 0;
            //}
            //else if(ddSearchString.Trim().Length == 0)
            //    populateInventoryDD();
        }

        private void updateGiftCertificates()
        {
            //activate any gift certificates in the cart or use a gift certificate
            foreach (object cartItem in arCart)
            {
                if (cartItem is GiftCertificate)
                {
                    GiftCertificate certificateInCart = (GiftCertificate)cartItem;

                    if (certificateInCart.Status == GiftCertificate.GiftCertificateStatus.NotActivated.ToString())
                    {
                        certificateInCart.Status = GiftCertificate.GiftCertificateStatus.Active.ToString();

                        xmlData.Update(certificateInCart, XmlData.Tables.GiftCertificates);
                    }
                    else //use the certificate
                    {
                        certificateInCart.Amount = certificateInCart.Amount - certificateInCart.AmountToApply;

                        xmlData.Update(certificateInCart, XmlData.Tables.GiftCertificates);

                        //save giftCertificate activity
                        GiftCertificateActivity giftCertificateActivity = new GiftCertificateActivity();
                        giftCertificateActivity.Activity = "Gift certificate created";
                        giftCertificateActivity.GiftCertificateID = certificateInCart.ID;
                        giftCertificateActivity.BeginningBalance = certificateInCart.Amount + certificateInCart.AmountToApply;
                        giftCertificateActivity.EndingBalance = certificateInCart.Amount;

                        xmlData.Insert(giftCertificateActivity, XmlData.Tables.GiftCertificateActivity);
                    }
                }
            }
        }

        private void getInventory()
        {
            dtInventoryItems = xmlData.Select("active=true", "name asc", XmlData.Tables.InventoryItems);
        }

        private void getCategories()
        {
            dtCategories = xmlData.Select("*", "category asc", XmlData.Tables.Categories);
        }

        private string getCategoryName(int categoryID)
        {
            string categoryName = "";

            DataTable dtCategoryNames = xmlData.Select("ID = "+ categoryID, "", XmlData.Tables.Categories);

            if (dtCategoryNames != null)
                categoryName = dtCategoryNames.Rows[0]["category"].ToString();

            return categoryName;
        }

        private void populateQuickButtons()
        {
            DataTable dtQuickButtonIds = xmlData.Select("*", "", "data\\" + XmlData.Tables.L_InventoryItemsToQuickButtons.ToString());

            if (dtQuickButtonIds != null)
            {
                string quickButtonIds = "";

                foreach (DataRow dr in dtQuickButtonIds.Rows)
                {
                    if (quickButtonIds.Length > 0)
                        quickButtonIds += ",";

                    quickButtonIds += dr["inventoryItemID"].ToString();
                }

                DataTable dtProducts = xmlData.Select("id in (" + quickButtonIds + ")", "name asc", "data\\" + XmlData.Tables.InventoryItems.ToString());

                if (dtProducts != null)
                {
                    flowLayoutPanelQuickButtons.Visible = false;
                    flowLayoutPanelQuickButtons.Controls.Clear();

                    foreach (DataRow dr in dtProducts.Rows)
                    {
                        GelButton newQuickButton = new GelButton();
                        newQuickButton.Text = dr["name"].ToString();
                        newQuickButton.Tag = dr["id"].ToString();
                        newQuickButton.Width = 120;
                        newQuickButton.Height = 75;
                        newQuickButton.Click += new EventHandler(btnQuickButton_Click);

                        flowLayoutPanelQuickButtons.Controls.Add(newQuickButton);
                    }

                    flowLayoutPanelQuickButtons.Visible = true;
                }
            }
        }

        private void populateCategories()
        {
            if (dtCategories == null)
                getCategories();

            flowLayoutPanelCategories.Visible = false;
            flowLayoutPanelCategories.Controls.Clear();

            //iterate the categories and display
            foreach (DataRow dr in dtCategories.Rows)
            {
                GelButton newButton = new GelButton();
                newButton.Text = dr["category"].ToString();
                newButton.Tag = dr["ID"].ToString();
                newButton.Width = 120;
                newButton.Height = 75;
                newButton.Click += new EventHandler(btnCategory_Click);

                flowLayoutPanelCategories.Controls.Add(newButton);
            }

            flowLayoutPanelCategories.Visible = true;
        }

        /// <summary>
        /// display the inventory for chosen category
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCategory_Click(object sender, EventArgs e)
        {
            displaySelectedCategoryItems(((GelButton)sender).Tag.ToString());
        }

        private void btnQuickButton_Click(object sender, EventArgs e)
        {
            //add selected item to cart if in checkout mode
            if (mode == Modes.Checkout)
            {
                Voodoo.Objects.InventoryItem itemToAdd = new Voodoo.Objects.InventoryItem();
                itemToAdd = common.FindItemInInventory(int.Parse(((GelButton)sender).Tag.ToString()));

                Voodoo.Objects.InventoryItem existingItem;

                existingItem = common.FindItemInCart(itemToAdd, arCart);

                if (existingItem != null)
                {
                    itemToAdd.Quantity = existingItem.Quantity + 1;
                    common.UpdateItemInCart(itemToAdd, arCart);
                }
                else
                {
                    itemToAdd.Quantity = 1;

                    addItemToCart(itemToAdd);
                }

                currentItem = itemToAdd;

                displayCart();
            }
        }

        private void displaySelectedCategoryItems(string selectedCategory)
        {
            flowLayoutPanelProducts.Controls.Clear();

            DataTable dtSelectedCategoryItems = getSelectedCategoryItems(selectedCategory);
            if (dtSelectedCategoryItems != null && dtSelectedCategoryItems.Rows.Count > 0)
            {
                lblSelectedCategory.Text = getCategoryName(int.Parse(selectedCategory));

                foreach (DataRow dr in dtSelectedCategoryItems.Rows)
                {
                    GelButton newButton = new GelButton();
                    newButton.Text = dr["name"].ToString();
                    newButton.Tag = dr["ID"].ToString();
                    newButton.Width = 200;
                    newButton.Height = 100;
                    newButton.Click += new EventHandler(btnItem_Click);
                    newButton.DoubleClick += new EventHandler(btnItem_DoubleClick);

                    flowLayoutPanelProducts.Controls.Add(newButton);
                }
            }
            else //no results
            {
                GelButton newButton = new GelButton();
                newButton.Text = "No Results";
                newButton.Tag = "-1";
                newButton.Width = 200;
                newButton.Height = 100;

                flowLayoutPanelProducts.Controls.Add(newButton);
            }
        }

        /// <summary>
        /// show more item info
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnItem_DoubleClick(object sender, EventArgs e)
        {
            Voodoo.Objects.InventoryItem itemToView = new Voodoo.Objects.InventoryItem();
            itemToView = common.FindItemInInventory(int.Parse(((GelButton)sender).Tag.ToString()));

            InventoryItemInfo frmItemInfo = new InventoryItemInfo(itemToView);

            frmItemInfo.ShowDialog(this);
        }

        private void btnItem_Click(object sender, EventArgs e)
        {
            //add selected item to cart if in checkout mode
            if (mode == Modes.Checkout)
            {
                Voodoo.Objects.InventoryItem itemToAdd = new Voodoo.Objects.InventoryItem();
                itemToAdd = common.FindItemInInventory(int.Parse(((GelButton)sender).Tag.ToString()));

                Voodoo.Objects.InventoryItem existingItem;

                existingItem = common.FindItemInCart(itemToAdd, arCart);

                if (existingItem != null)
                {
                    itemToAdd.Quantity = existingItem.Quantity + 1;
                    common.UpdateItemInCart(itemToAdd, arCart);
                }
                else
                {
                    itemToAdd.Quantity = 1;

                    addItemToCart(itemToAdd);
                }

                currentItem = itemToAdd;

                displayCart();
            }
        }

        private DataTable getSelectedCategoryItems(string selectedCategoryID)
        {
            DataTable dtSelectedCategoryInventoryIDs;

            dtSelectedCategoryInventoryIDs = xmlData.Select("categoryID = '" + selectedCategoryID + "'", "", XmlData.Tables.L_inventoryItemsToCategories);

            DataTable dtSelectedCategoryItems = null;

            if (dtSelectedCategoryInventoryIDs != null)
            {
                string inventoryIDs = "";

                foreach (DataRow dr in dtSelectedCategoryInventoryIDs.Rows)
                {
                    if (inventoryIDs.Length > 0)
                        inventoryIDs += ",";

                    inventoryIDs += dr["inventoryItemID"].ToString();
                }

                dtSelectedCategoryItems = xmlData.Select("id in (" + inventoryIDs + ")", "name asc", XmlData.Tables.InventoryItems);
            }

            return dtSelectedCategoryItems;
        }

        private void btnCancelSearch_Click(object sender, EventArgs e)
        {
            txtSearchBox.Text = "";

            populateInventoryDD();

            //txtSearchBox.Focus();
        }

        /// <summary>
        /// display the properties of the cash register drawer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CashRegisterDrawer cashDrawer = new CashRegisterDrawer();
            cashDrawer.ShowDialog();
        }

        private void bChange_Click(object sender, EventArgs e)
        {
            //open drawer
            common.OpenDrawer();

            DataTable dtDrawer = null;

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
                VoodooPOS.objects.CashRegisterDrawer cashDrawer = (VoodooPOS.objects.CashRegisterDrawer)Common.CreateObjects.FromDataRow(dtDrawer.Rows[0], new VoodooPOS.objects.CashRegisterDrawer());

                cashDrawer.Description = "Drawer opened to give change";

                xmlData.Update(cashDrawer, XmlData.Tables.CashRegisterDrawer);

                common.WriteToLog("CashDrawerHistory", cashDrawer); 
            }                       
        }

        /// <summary>
        /// make sure focus is on txtSearchBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void focusTimer_Tick(object sender, EventArgs e)
        {
            txtSearchBox.Focus();
        }

        private void txtSearchBox_TextChanged(object sender, EventArgs e)
        {
            double upc = -1;

            keystrokeTimerCounter = 0;
            stoppedTyping = false;

            if (!keystrokeTimer1.Enabled)
            {
                stoppedTyping = false;
                keystrokeTimer1.Enabled = true;
                keystrokeTimer1.Start();
            }

            //if(stoppedTyping)
            //    if (double.TryParse(txtSearchBox.Text, out upc) && txtSearchBox.Text.Trim().Length >= 10)//this is our upc - do a search
            //        FoundUpc(txtSearchBox.Text.Trim());
        }

        private void ddInventory_DropDown(object sender, EventArgs e)
        {
            focusTimer.Stop();
            focusTimer.Enabled = false;
        }

        private void ddInventory_DropDownClosed(object sender, EventArgs e)
        {
            //focusTimer.Enabled = true;
            //focusTimer.Start();
        }

        private void btnHailNasty_Click(object sender, EventArgs e)
        {          
            if (cartTotal > 0)
            {
                Voodoo.Objects.InventoryItem hailNastyDiscount = new Voodoo.Objects.InventoryItem();
                hailNastyDiscount.Name = "HAIL Nasty Discount";
                hailNastyDiscount.Price = (cartSubTotal * .1) * -1;
                hailNastyDiscount.OnSale = true;
                hailNastyDiscount.SalePrice = (cartSubTotal * .1) * -1;
                hailNastyDiscount.Quantity = 1;
                hailNastyDiscount.ID = 10101010;

                Voodoo.Objects.InventoryItem itemInCart = common.FindItemInCart(hailNastyDiscount, arCart);

                if (itemInCart != null)
                {
                    itemInCart.Quantity = 0;
                    common.UpdateItemInCart(itemInCart, arCart);
                    displayCart();

                    hailNastyDiscount.Price = (cartSubTotal * .1) * -1;
                    hailNastyDiscount.SalePrice = (cartSubTotal * .1) * -1;
                }

                common.UpdateItemInCart(hailNastyDiscount, arCart);

                displayCart();
            }
        }

        /// <summary>
        /// Refresh inventory data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void refreshDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SCTech.Common.XmlData data = new SCTech.Common.XmlData(Application.StartupPath);
            data.ApplyNewSchema(SCTech.Common.XmlData.Tables.InventoryItems);
        }

        private void btnCashAndCredit_Click(object sender, EventArgs e)
        {
            checkout_cashAndCredit checkout = new checkout_cashAndCredit(arCart, cartTotal);
            DialogResult result = checkout.ShowDialog();

            if (result == DialogResult.OK)
            {
                updateGiftCertificates();

                //clear cart
                if (MessageBox.Show("Would you like to clear the cart?", "Clear Cart?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    arCart.Clear();
                    displayCart();
                }
            }

            populateTransactionDates();
        }

        /// <summary>
        /// search for the text in the xml file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click_1(object sender, EventArgs e)
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

        private void covenienceStoreToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        //save the forms current button layout
        private void saveCurrentLayoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ControlFactory.CopyCtrl2ClipBoard(this.comboBox1);

            saveTemplate("LiquorStore");
        }

        private void loadTemplate(string TemplateName)
        {
            Voodoo.Objects.Template currentTemplate = ObjectXMLSerializer<Voodoo.Objects.Template>.Load("data\\Template_"+ TemplateName +".xml" ,SerializedFormat.Binary);

            foreach (Voodoo.Objects.TemplateControl control in currentTemplate.TemplateControls)
            {
                string newControlName = control.PartialName;
                System.Windows.Forms.Control newControl;

                newControl = pnlUI.Controls[control.PropertyList["Name"].ToString()];
                
                //iterate properties and apply to newControl
                PropertyDescriptorCollection newControlProperties = TypeDescriptor.GetProperties(newControl);

                foreach (PropertyDescriptor myProperty in newControlProperties)
                {
                    try
                    {
                        if (myProperty.PropertyType.IsSerializable)
                        {
                            newControlProperties[myProperty.Name].SetValue(newControl, control.PropertyList[myProperty.Name]);
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Trace.WriteLine(ex.Message);
                        //do nothing, just continue
                    }
                }

                //dgvItemsToDisplay needs to follow the location of grpGridDisplay
                //grpGridDisplay.Controls.Add(displayGrid);
            }
        }

        private void saveTemplate(string TemplateName)
        {
            ObjectXMLSerializer<Voodoo.Objects.Template>.Save(new Voodoo.Objects.Template(TemplateName, pnlUI), "data\\Template_" + TemplateName + ".xml", SerializedFormat.Binary);
        }

        private void liquorStoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadTemplate("LiquorStore");
        }

        bool isDragging = false;
        Point ptOffset;
        private void button_MouseDown(object sender, MouseEventArgs e)
        {
            if (mode == Modes.Edit && e.Button == MouseButtons.Left)
            {
                Control clickedControl = ((Control)sender);
                isDragging = true;
                Point ptStartPosition = clickedControl.PointToScreen(new Point(e.X, e.Y));

                ptOffset = new Point();
                ptOffset.X = clickedControl.Location.X - ptStartPosition.X;
                ptOffset.Y = clickedControl.Location.Y - ptStartPosition.Y;
            }
            else
            {
                isDragging = false;
            }
        }

        private void button_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Control clickedControl = ((Control)sender);
                Point newPoint = clickedControl.PointToScreen(new Point(e.X, e.Y));
                newPoint.Offset(ptOffset);
                clickedControl.Location = newPoint;
            }
        }

        private void button_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void gbCurrentItem_MouseDown(object sender, EventArgs e)
        {

        }

        private void clockTimer_Tick(object sender, EventArgs e)
        {
            lblClock.Text = DateTime.Now.ToString("h:mm tt");
        }

        /// <summary>
        /// login or check what mode we are in and display appropriate info
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblEmployee_Click(object sender, EventArgs e)
        {
            if (lblEmployee.Text == "Login")
            {
                Login frmLogin = new Login();
                DialogResult dr = new DialogResult ();

                dr = frmLogin.ShowDialog();

                if (dr == DialogResult.OK) //user is logging in - check credentials
                {
                    Employee = frmLogin.Employee;
                    lblEmployee.Text = Employee.FirstName;

                    setMode(Modes.Checkout);
                    
                    pnlUI.Visible = true;
                    pnlBackgroundImage.Visible = false;
                    pbLogo.Visible = false;

                    //check security levels and display the correct info
                    switch (Employee.SecurityLevel)
                    {
                        case 2://employee
                            menuStrip1.Visible = false;
                            break;
                        case 4://Manager
                            menuStrip1.Visible = true;
                            break;
                        case 9: //Admin
                            menuStrip1.Visible = true;
                            break;
                        case 10: //Master Account
                            menuStrip1.Visible = true;
                            break;
                    }
                }

            }
            else //logout
            {
                Employee = null;

                mode = Modes.Locked;

                //update UI
                pnlUI.Visible = false;
                menuStrip1.Visible = false;
                pbLogo.Visible = true;
                lblEmployee.Text = "Login";
                pnlBackgroundImage.Visible = true;
            }
        }

        private void flowLayoutPanel_MouseEnter(object sender, EventArgs e)
        {
            FlowLayoutPanel pnlCategories = ((FlowLayoutPanel)sender);
            pnlCategories.Focus();
        }

        /// <summary>
        /// select the entire row when a cell is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvItemsToDisplay_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (mode == Modes.Checkout)
            {
                dgvItemsToDisplay.Rows[e.RowIndex].Selected = true;

                clickedItem = common.FindItemInCart(int.Parse(dgvItemsToDisplay.Rows[e.RowIndex].Cells["ID"].Value.ToString()), arCart);
            }
        }

        private void foodVendorToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dgvItemsToDisplay_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 46)//delete button
            {
                Voodoo.Objects.InventoryItem itemInCart = common.FindItemInCart(clickedItem, arCart);

                if (itemInCart != null)
                {
                    itemInCart.Quantity = 0;

                    common.UpdateItemInCart(itemInCart, arCart);

                    displayCart();
                }
            }
        }

        private void pnlBackgroundImage_Click(object sender, EventArgs e)
        {
            if (mode == Modes.Locked) //popup login screen
            {
                lblEmployee_Click(null, null);
            }
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setMode(Modes.Admin);
        }
    }
}

