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
    public partial class InventoryItemEdit : Form
    {
        string upc = "";
        Voodoo.Objects.InventoryItem newItem;
        Common common;
        XmlData xmlData;

        public delegate void InventoryUpdated();
        public event InventoryUpdated inventoryUpdated;

        public string UPC
        {
            set
            {
                upc = value;

                var X = this.Handle;

                Invoke((MethodInvoker)delegate
                {
                    try
                    {
                        txtUpc.Text = upc;
                    }
                    catch (Exception ex)
                    {
                        Common.WriteToFile(ex);
                    }
                });
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
                txtPrice.Text = newItem.Price.ToString("N");
                txtWholesalePrice.Text = newItem.WholesalePrice.ToString("N");
                txtQuantity.Text = newItem.Quantity.ToString();
                txtSize.Text = newItem.Size;
                txtPicturePath.Text = newItem.PicturePath;

                if(newItem.PicturePath.Trim().Length > 0)
                    pictureBox1.BackgroundImage = new Bitmap(newItem.PicturePath);

                chbOnSale.Checked = newItem.OnSale;
                chbDisplayOnWeb.Checked = newItem.DisplayOnWeb;
                chbActive.Checked = newItem.Active;
                txtSalePrice.Text = newItem.SalePrice.ToString("N");

                txtLabelName.Text = newItem.Name;
                txtLabelPrice.Text = newItem.Price.ToString("N");

                DataTable dtFeatured = xmlData.Select("inventoryItemID = " + newItem.ID, "", "data\\" + XmlData.Tables.L_InventoryItemsToFeaturedItems.ToString());

                if (dtFeatured != null)
                {
                    chbFeatured.Checked = (bool)dtFeatured.Rows[0]["active"];
                }
                else
                {
                    chbFeatured.Checked = false;
                }

                //quick buttons
                DataTable dtQuickButtons = xmlData.Select("InventoryItemID = " + newItem.ID, "", "data\\" + XmlData.Tables.L_InventoryItemsToQuickButtons.ToString());
                if (dtQuickButtons != null)
                    cbQuickButton.Checked = true;
                else
                    cbQuickButton.Checked = false;

                displayItemCategories(newItem.ID);
            }
        }

        public InventoryItemEdit(Voodoo.Objects.InventoryItem itemToEdit)
        {
            InitializeComponent();

            common = new Common(Application.StartupPath);
            common.inventoryUpdated += new Common.InventoryUpdated(common_inventoryUpdated);

            xmlData = new XmlData(Application.StartupPath);

            NewItem = itemToEdit;

            populateCategories();

            //set background image
            if (File.Exists(Application.StartupPath + Properties.Settings.Default.BackgroundImagePath))
                this.BackgroundImage = Image.FromFile(Application.StartupPath + Properties.Settings.Default.BackgroundImagePath);
        }

        private void populateCategories()
        {
            DataTable dtCategories = xmlData.Select("*", "category asc", XmlData.Tables.Categories);

            ddCategories.DisplayMember = "category";
            ddCategories.ValueMember = "id";

            ddCategories.DataSource = dtCategories;
        }

        void common_inventoryUpdated()
        {
            inventoryUpdated();
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            saveCurrentItem();

            this.Close();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();

            fdlg.Title = "Find Your Picture";
            fdlg.InitialDirectory = Application.StartupPath +"";
            fdlg.Filter = "All files (*.*)|*.*|All files (*.*)|*.*";
            fdlg.FilterIndex = 2;
            fdlg.RestoreDirectory = true;

            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                txtPicturePath.Text = fdlg.FileName;

                pictureBox1.BackgroundImage = new Bitmap(txtPicturePath.Text.Trim());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrintLabel_Click(object sender, EventArgs e)
        {
            saveCurrentItem();

            Voodoo.Objects.InventoryItem itemToPrint = new Voodoo.Objects.InventoryItem();
            itemToPrint = newItem;

            int numLabels = 0;
            double price = 0;

            int.TryParse(txtPrintQuantity.Text.Trim(), out numLabels);
            itemToPrint.Quantity = numLabels;

            itemToPrint.Name = txtLabelName.Text.Trim();

            double.TryParse(txtLabelPrice.Text.Trim(),out price);
            itemToPrint.Price = price;

            Printer printer = new Printer(Application.StartupPath);
            printer.PrintLabel(itemToPrint);
        }

        private void saveCurrentItem()
        {
            int quantity = 0;
            double price = 0;
            double wholesalePrice = 0;
            double salePrice = 0;
            //CashRegister register = (CashRegister)this.ParentForm;

            newItem.UPC = txtUpc.Text;
            newItem.Name = txtName.Text;
            newItem.Description = txtDescription.Text;
            newItem.Manufacturer = txtManufacturer.Text;
            newItem.Color = txtColor.Text;
            newItem.Model = txtModel.Text;

            double.TryParse(txtPrice.Text.Trim(), out price);
            newItem.Price = price;

            double.TryParse(txtWholesalePrice.Text.Trim(), out wholesalePrice);
            newItem.WholesalePrice = wholesalePrice;

            double.TryParse(txtSalePrice.Text.Trim(), out salePrice);
            newItem.SalePrice = salePrice;

            newItem.OnSale = chbOnSale.Checked;
            newItem.Active = chbActive.Checked;
            newItem.DisplayOnWeb = chbDisplayOnWeb.Checked;

            int.TryParse(txtQuantity.Text.Trim(), out quantity);
            newItem.Quantity = quantity;

            newItem.Size = txtSize.Text;

            newItem.PicturePath = txtPicturePath.Text.Trim();

            if (newItem.PicturePath.Length > 0 && !newItem.PicturePath.ToLower().Contains(Application.StartupPath.ToLower() + "\\data\\inventoryimages"))//copy picture to application path
            {
                System.IO.FileInfo picturePath = new System.IO.FileInfo(txtPicturePath.Text.Trim());

                newItem.PicturePath = Application.StartupPath + "\\data\\inventoryImages\\" + picturePath.Name;

                if (!System.IO.File.Exists(newItem.PicturePath))
                    System.IO.File.Copy(picturePath.FullName, newItem.PicturePath);
            }

            common.UpdateItemInInventory(newItem);

            //featured
            DataTable dtFeatured = xmlData.Select("InventoryItemID = " + newItem.ID, "", "data\\" + XmlData.Tables.L_InventoryItemsToFeaturedItems.ToString());

            if (dtFeatured != null)
            {
                if (chbFeatured.Checked)
                {
                    //update the existing record
                    L_inventoryItemsToFeaturedItems featuredItem = new L_inventoryItemsToFeaturedItems();
                    featuredItem.Active = chbFeatured.Checked;
                    featuredItem.ExpirationDate = (DateTime)dtFeatured.Rows[0]["ExpirationDate"];
                    featuredItem.ID = newItem.ID;
                    featuredItem.InventoryItemID = (int)dtFeatured.Rows[0]["InventoryItemID"];

                    xmlData.Update(featuredItem, XmlData.Tables.L_InventoryItemsToFeaturedItems);
                }
                else
                    xmlData.Delete("InventoryItemID = '" + newItem.ID + "'", XmlData.Tables.L_InventoryItemsToFeaturedItems);
            }
            else if (chbFeatured.Checked)
            {
                //create new record
                L_inventoryItemsToFeaturedItems featuredItem = new L_inventoryItemsToFeaturedItems();
                featuredItem.Active = chbFeatured.Checked;
                featuredItem.InventoryItemID = newItem.ID;

                xmlData.Insert(featuredItem, "data\\" + XmlData.Tables.L_InventoryItemsToFeaturedItems.ToString());
            }

            //on sale
            DataTable dtSalesInfo = xmlData.Select("InventoryItemID = " + newItem.ID, "", "data\\" + XmlData.Tables.L_InventoryItemsToSalesCalendar.ToString());

            if (dtSalesInfo != null)
            {
                if (chbOnSale.Checked)
                {
                    //update the existing record
                    L_inventoryItemsToSalesCalendar saleItem = new L_inventoryItemsToSalesCalendar();
                    saleItem.StartDate = dateTimePickerStartDate.Value;
                    saleItem.EndDate = dateTimePickerEndDate.Value;
                    saleItem.ID = newItem.ID;
                    saleItem.InventoryItemID = (int)dtSalesInfo.Rows[0]["InventoryItemID"];

                    xmlData.Update(saleItem, XmlData.Tables.L_InventoryItemsToFeaturedItems);
                }
                else //the item used to be on sale and now it isn't - delete the record
                {
                    xmlData.Delete("InventoryItemID = '"+ newItem.ID +"'", XmlData.Tables.L_InventoryItemsToSalesCalendar);
                }
            }
            else if (chbOnSale.Checked)//the item is on sale - create a new record
            {
                //create new record
                L_inventoryItemsToSalesCalendar saleItem = new L_inventoryItemsToSalesCalendar();
                saleItem.StartDate = dateTimePickerStartDate.Value;
                saleItem.EndDate = dateTimePickerEndDate.Value;
                saleItem.InventoryItemID = newItem.ID;

                xmlData.Insert(saleItem, "data\\" + XmlData.Tables.L_InventoryItemsToSalesCalendar.ToString());
            }

            //quick buttons
            DataTable dtQuickButtons = xmlData.Select("InventoryItemID = " + newItem.ID, "", "data\\" + XmlData.Tables.L_InventoryItemsToQuickButtons.ToString());

            if (dtQuickButtons != null)
            {
                if (cbQuickButton.Checked)
                {
                    //update the existing record
                    L_InventoryItemsToQuickButtons quickButtonItem = new L_InventoryItemsToQuickButtons();
                    quickButtonItem.ID = newItem.ID;
                    quickButtonItem.InventoryItemID = (int)dtSalesInfo.Rows[0]["InventoryItemID"];

                    xmlData.Update(quickButtonItem, XmlData.Tables.L_InventoryItemsToQuickButtons);
                }
                else //delete record
                    xmlData.Delete("InventoryItemID = '" + newItem.ID + "'", XmlData.Tables.L_InventoryItemsToQuickButtons);
            }
            else if (cbQuickButton.Checked)
            {
                //create new record
                L_InventoryItemsToQuickButtons quickButtonItem = new L_InventoryItemsToQuickButtons();
                quickButtonItem.InventoryItemID = newItem.ID;

                xmlData.Insert(quickButtonItem, "data\\" + XmlData.Tables.L_InventoryItemsToQuickButtons.ToString());
            }
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

        private void addCategoryToItem(int itemID, int categoryID)
        {
            //categories
            DataTable dtcategories = xmlData.Select("categoryID = " + categoryID + " and inventoryItemID = " + itemID, "", "data\\" + XmlData.Tables.L_inventoryItemsToCategories.ToString());

            if (dtcategories == null) //these values are not in the db
            {
                //create new record
                VoodooPOS.objects.L_inventoryItemsToCategories categoryForItem = new VoodooPOS.objects.L_inventoryItemsToCategories();
                categoryForItem.CategoryID = (int)ddCategories.SelectedValue;
                categoryForItem.InventoryItemID = itemID;

                xmlData.Insert(categoryForItem, "data\\" + XmlData.Tables.L_inventoryItemsToCategories.ToString());
            }
        }

        private void RemoveCategoryFromItem(int itemID, int categoryID)
        {
            xmlData.Delete("categoryID = " + categoryID + " and inventoryItemID = " + itemID, XmlData.Tables.L_inventoryItemsToCategories);
        }

        private void lnkAddToItem_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            addCategoryToItem(newItem.ID, (int)ddCategories.SelectedValue);

            displayItemCategories(newItem.ID);
        }

        /// <summary>
        /// Remove selected category from item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void llRemoveCategory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (lbCategories.SelectedItem != null)
            {
                RemoveCategoryFromItem(newItem.ID, (int)lbCategories.SelectedValue);

                displayItemCategories(newItem.ID);
            }
            else
                MessageBox.Show("You must select something to remove");
        }

        private void chbOnSale_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePickerStartDate.Enabled = chbOnSale.Checked;
            dateTimePickerEndDate.Enabled = chbOnSale.Checked;
            txtSalePrice.Enabled = chbOnSale.Checked;
        }

        /// <summary>
        /// add a "quick" button for this item to the check out screen of the cash register
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbQuickButton_CheckedChanged(object sender, EventArgs e)
        {
            
        }
    }
}
