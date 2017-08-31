using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;

namespace VoodooPOS
{
    public partial class NewInventoryItem : Form
    {
        string upc = "";
        Voodoo.Objects.InventoryItem newItem;
        Common common;
        XmlData xmlData;

        public string UPC
        {
            set 
            { 
                upc = value;
                txtUpc.Text = upc;
            }
        }

        public Voodoo.Objects.InventoryItem NewItem
        {
            set
            {
                newItem = value;

                txtUpc.Text = newItem.UPC;
                txtName.Text = newItem.Name;
                txtDescription.Text = newItem.Description;
                txtManufacturer.Text = newItem.Manufacturer;
                txtColor.Text = newItem.Color;
                txtModel.Text = newItem.Model;
                txtPrice.Text = newItem.Price.ToString();
                txtQuantity.Text = newItem.Quantity.ToString();
                txtSize.Text = newItem.Size;
                txtPicturePath.Text = newItem.PicturePath;

                if(System.IO.File.Exists(newItem.PicturePath))
                    pictureBox1.BackgroundImage = new Bitmap(newItem.PicturePath);
            }
        }

        public NewInventoryItem()
        {
            InitializeComponent();

            common = new Common(Application.StartupPath);
            common.inventoryUpdated += new Common.InventoryUpdated(common_inventoryUpdated);

            xmlData = new XmlData(Application.StartupPath);

            populateStatus();
            populateCategories();

            //set background image
            if (File.Exists(Application.StartupPath + Properties.Settings.Default.BackgroundImagePath))
                this.BackgroundImage = Image.FromFile(Application.StartupPath + Properties.Settings.Default.BackgroundImagePath);
        }

        void common_inventoryUpdated()
        {
            
        }

        private void populateStatus()
        {
            ddStatus.Items.Add("In Stock");
            ddStatus.Items.Add("Backordered");
            ddStatus.Items.Add("Shipped");

            ddStatus.SelectedIndex = 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int quantity = 0;
                double price = 0;
                double wholesalePrice = 0;
                double salePrice = 0;
                //CashRegister register = (CashRegister)this.ParentForm;

                newItem = new Voodoo.Objects.InventoryItem();
                newItem.ID = 999999;
                newItem.UPC = txtUpc.Text;
                newItem.Name = txtName.Text;
                newItem.Description = txtDescription.Text;
                newItem.Manufacturer = txtManufacturer.Text;
                newItem.Color = txtColor.Text;
                newItem.Model = txtModel.Text;
                newItem.Status = ddStatus.SelectedText;
                newItem.OnSale = chbOnSale.Checked;
                newItem.Active = chbActive.Checked;
                newItem.DisplayOnWeb = chbDisplayOnWeb.Checked;

                double.TryParse(txtPrice.Text.Trim(), out price);
                newItem.Price = price;

                double.TryParse(txtSalePrice.Text.Trim(), out salePrice);
                newItem.SalePrice = salePrice;

                double.TryParse(txtWholesalePrice.Text.Trim(), out wholesalePrice);
                newItem.WholesalePrice = wholesalePrice;

                int.TryParse(txtQuantity.Text.Trim(), out quantity);
                newItem.Quantity = quantity;

                newItem.Size = txtSize.Text;

                if (System.IO.File.Exists(txtPicturePath.Text.Trim()))
                {
                    System.IO.FileInfo picturePath = new System.IO.FileInfo(txtPicturePath.Text.Trim());

                    string newPicturePath = Application.StartupPath + "\\data\\inventoryImages\\" + picturePath.Name;

                    if (!System.IO.File.Exists(newPicturePath))
                    {
                        newItem.PicturePath = txtPicturePath.Text.Trim();

                        if (newItem.PicturePath.Length > 0 && !newItem.PicturePath.ToLower().Contains(Application.StartupPath.ToLower() + "\\data\\inventoryimages"))//copy picture to application path
                        {
                            newItem.PicturePath = Application.StartupPath + "\\data\\inventoryImages\\" + picturePath.Name;

                            if(!System.IO.File.Exists(newItem.PicturePath))
                                System.IO.File.Copy(picturePath.FullName, newItem.PicturePath);
                        }
                    }
                }

                newItem.ID = common.AddItemToInventory(newItem);

                foreach (DataRowView dr in lbCategories.Items)
                {
                    int categoryID = -1;

                    categoryID = int.Parse(dr["id"].ToString());

                    //categories
                    DataTable dtcategories = xmlData.Select("categoryID = " + categoryID + " and inventoryItemID = " + newItem.ID, "", "data\\" + XmlData.Tables.L_inventoryItemsToCategories.ToString());

                    if (dtcategories == null) //these values are not in the db
                    {
                        //create new record
                        VoodooPOS.objects.L_inventoryItemsToCategories categoryForItem = new VoodooPOS.objects.L_inventoryItemsToCategories();
                        categoryForItem.CategoryID = categoryID;
                        categoryForItem.InventoryItemID = newItem.ID;

                        xmlData.Insert(categoryForItem, "data\\" + XmlData.Tables.L_inventoryItemsToCategories.ToString());
                    }
                }

                this.DialogResult = DialogResult.OK;

                this.Close();
            }
            catch (Exception ex)
            {
                Common.WriteToFile(ex);
                //MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();

            fdlg.Title = "Find Your Picture";
            fdlg.InitialDirectory = @"c:\";
            fdlg.Filter = "All files (*.*)|*.*|All files (*.*)|*.*";
            fdlg.FilterIndex = 2;
            fdlg.RestoreDirectory = true;

            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                txtPicturePath.Text = fdlg.FileName;

                pictureBox1.Load(txtPicturePath.Text.Trim());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;

            this.Close();
        }

        private void lnkNew_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (ddCategories.SelectedIndex == -1)
            {
                VoodooPOS.objects.Categories category = new VoodooPOS.objects.Categories();
                category.Category = ddCategories.Text;
                
                xmlData.Insert(category, "data\\" + XmlData.Tables.Categories.ToString());

                populateCategories();
            }
        }

        private void lnkAddToItem_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            addCategoryToItem((int)ddCategories.SelectedValue);

            //displayItemCategories(newItem.ID);

        }

        private void addCategoryToItem(int categoryID)
        {
            ArrayList selectedCategories = new ArrayList();

            if (lbCategories.Items.Count > 0)
            {
                foreach (DataRowView dr in lbCategories.Items)
                {
                    selectedCategories.Add(dr);
                }
            }

            DataTable dtCategory = xmlData.Select("ID = " + categoryID, "category asc", XmlData.Tables.Categories);

            //if (dtTemp != null)
            //{
            foreach (DataRowView dr in selectedCategories)
                    dtCategory.Rows.Add(dr.Row.ItemArray);
            //}

            lbCategories.DataSource = dtCategory;

            //categories
            //DataTable dtcategories = xmlData.Select("categoryID = " + categoryID + " and inventoryItemID = " + itemID, "", "data\\" + XmlData.Tables.L_inventoryItemsToCategories.ToString());

            //if (dtcategories == null) //these values are not in the db
            //{
            //    //create new record
            //    VoodooPOS.objects.L_inventoryItemsToCategories categoryForItem = new VoodooPOS.objects.L_inventoryItemsToCategories();
            //    categoryForItem.CategoryID = (int)ddCategories.SelectedValue;
            //    categoryForItem.InventoryItemID = itemID;

            //    xmlData.Insert(categoryForItem, "data\\" + XmlData.Tables.L_inventoryItemsToCategories.ToString());
            //}
        }

        private void displayItemCategories(int itemID)
        {
            DataTable dtcategoryIds = xmlData.Select("inventoryItemID = " + itemID.ToString(), "", "data\\" + XmlData.Tables.L_inventoryItemsToCategories.ToString());

            if (dtcategoryIds != null)
            {
                string categoryIDs = "";

                foreach (DataRow dr in dtcategoryIds.Rows)
                {
                    if (categoryIDs.Length > 0)
                        categoryIDs += ",";

                    categoryIDs += dr["categoryID"].ToString();
                }

                DataTable dtProducts = xmlData.Select("id in (" + categoryIDs + ")", "category asc", "data\\" + XmlData.Tables.Categories.ToString());

                lbCategories.DisplayMember = "category";
                lbCategories.ValueMember = "id";
                lbCategories.DataSource = dtProducts;
            }
        }

        private void populateCategories()
        {
            DataTable dtCategories = xmlData.Select("*", "category asc", XmlData.Tables.Categories);

            ddCategories.DisplayMember = "category";
            ddCategories.ValueMember = "id";

            ddCategories.DataSource = dtCategories;
        }
    }
}
