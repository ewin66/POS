using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace VoodooPOS
{
    public partial class checkout_cash : Form
    {
        Common common;
        double total = 0;
        double cashTendered = 0;
        double change = 0;
        double tax = 0;
        ArrayList arCart = new ArrayList();

        public checkout_cash(ArrayList Cart, double Total, double Tax)
        {
            InitializeComponent();

            common = new Common(Application.StartupPath);

            tax = Tax;
            total = Total;
            arCart = Cart; ;

            lblTotal.Text = total.ToString("C2");

            txtCashTendered.Focus();
        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            double.TryParse(txtCashTendered.Text, out cashTendered);

            change = cashTendered - total;
            
            lblChange.Text = change.ToString("N2");

            if (change >= 0)
            {
                common.OpenDrawer();

                btnCheckout.Enabled = false;
                btnCancel.Text = "Done";

                ArrayList cartToSave = arCart;

                Voodoo.Objects.InventoryItem newCart = new Voodoo.Objects.InventoryItem();
                newCart.Name = "New Cart_" + ((CashRegister)this.Owner).Employee.ID.ToString() + "_" + DateTime.Now.ToShortTimeString();
                newCart.Description = "Cash Transaction";
                newCart.Tax = tax;
                newCart.Price = total;

                cartToSave.Insert(0, newCart);

                common.RecordTransaction(cartToSave);

                cartToSave.Remove(newCart);
            }
            else
                MessageBox.Show("There is still a balance of " + change.ToString());
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if(btnCancel.Text == "Done")
                this.DialogResult = DialogResult.OK;
            else
                this.DialogResult = DialogResult.Cancel;

            this.Close();
        }
    }
}
