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
    public partial class MiscItem : Form
    {
        Voodoo.Objects.InventoryItem newItem;

        public Voodoo.Objects.InventoryItem NewItem
        {
            get { return newItem; }
        }

        public MiscItem()
        {
            InitializeComponent();

            txtQuantity.Text = "1";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int quantity = 0;
            double price = 0;
            int id = -1;

            newItem = new Voodoo.Objects.InventoryItem();

            while (id < 500)
                id = DateTime.Now.Millisecond;

            newItem.ID = id;
            newItem.UPC = txtUpc.Text;
            newItem.Name = txtName.Text;
            newItem.Description = txtDescription.Text;
            newItem.Manufacturer = txtManufacturer.Text;
            newItem.Color = txtColor.Text;
            newItem.Model = txtModel.Text;

            double.TryParse(txtPrice.Text.Trim(), out price);
            newItem.Price = price;

            int.TryParse(txtQuantity.Text.Trim(), out quantity);
            newItem.Quantity = quantity;

            newItem.Size = txtSize.Text;

            this.DialogResult = DialogResult.OK;

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;

            this.Close();
        }
    }
}
