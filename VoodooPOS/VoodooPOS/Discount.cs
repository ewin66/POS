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
    public partial class Discount : Form
    {
        Voodoo.Objects.InventoryItem currentItem;

        public Voodoo.Objects.InventoryItem CurrentItem
        {
            set { setCurrentItem(value); }
            get { return currentItem; }
        }

        public Discount(Voodoo.Objects.InventoryItem item)
        {
            InitializeComponent();

            setCurrentItem(item);
        }

        private void setCurrentItem(Voodoo.Objects.InventoryItem item)
        {
            lblName.Text = item.Name;
            lblDescription.Text = item.Description;
            lblPrice.Text = "$" + item.Price.ToString("N2");
            txtQuantity.Text = item.Quantity.ToString();

            if (System.IO.File.Exists(item.PicturePath))
                pictureBox1.BackgroundImage = new Bitmap(item.PicturePath);
            else
                pictureBox1.BackgroundImage = new Bitmap(Application.StartupPath + "\\data\\inventoryImages\\imageNotAvailable.jpg");
            
            txtDiscountPrice.Text = item.Price.ToString();

            txtUpc.Text = item.UPC;

            currentItem = item;
        }

        private void btnApplyDiscount_Click(object sender, EventArgs e)
        {
            double tempPrice = currentItem.Price;
            int tempQuantity = currentItem.Quantity;
            double.TryParse(txtDiscountPrice.Text.Trim(),out tempPrice);
            currentItem.Price = tempPrice;
            int.TryParse(txtQuantity.Text.Trim(),out tempQuantity);
            currentItem.Quantity = tempQuantity;

            this.Close();
        }
    }
}
