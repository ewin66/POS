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
    public partial class InventoryItemInfo : Form
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
                        lblUPC.Text = upc;
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

                lblUPC.Text = newItem.UPC;
                lblName.Text = newItem.Name;
                lblDescription.Text = newItem.Description;
                lblManufacturer.Text = newItem.Manufacturer;
                lblModel.Text = newItem.Model;
                lblSize.Text = newItem.Size;

                if(newItem.PicturePath.Trim().Length > 0)
                    pictureBox1.BackgroundImage = new Bitmap(newItem.PicturePath);

                chbOnSale.Checked = newItem.OnSale;
                chbDisplayOnWeb.Checked = newItem.DisplayOnWeb;

                lblPrice.Text = newItem.Price.ToString("N");

                DataTable dtFeatured = xmlData.Select("inventoryItemID = " + newItem.ID, "", "data\\" + XmlData.Tables.L_InventoryItemsToFeaturedItems.ToString());

                if (dtFeatured != null)
                {
                    chbFeatured.Checked = (bool)dtFeatured.Rows[0]["active"];
                }
                else
                {
                    chbFeatured.Checked = false;
                }
            }
        }

        public InventoryItemInfo(Voodoo.Objects.InventoryItem itemToDisplay)
        {
            InitializeComponent();

            xmlData = new XmlData(Application.StartupPath);

            NewItem = itemToDisplay;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
