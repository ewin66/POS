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
    public partial class checkout_cashAndCredit : Form
    {
        Common common;
        double total = 0;
        double cashTendered = 0;
        double change = 0;
        ArrayList arCart = new ArrayList();

        public checkout_cashAndCredit(ArrayList Cart, double Total)
        {
            InitializeComponent();

            common = new Common(Application.StartupPath);

            total = Total;
            arCart = Cart; ;

            lblTotal.Text = total.ToString("C2");

            txtCashTendered.Focus();
        }

        private void btnSplit_Click(object sender, EventArgs e)
        {
            double.TryParse(txtCashTendered.Text, out cashTendered);

            change = total - cashTendered;

            if (change > 4.99)
            {
                btnSplit.Enabled = false;

                lblCCAmount.Text = change.ToString("N2");

                txtCashTendered.ReadOnly = true;

                btnCheckout.Enabled = true;
            }
            else
                MessageBox.Show("There is a $5 minimum on credit card transactions");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (btnCancel.Text == "Done")
            {
                this.DialogResult = DialogResult.OK;

                this.Close();
            }
            else
            {
                if (txtCashTendered.ReadOnly)
                {
                    btnSplit.Enabled = true;
                    lblCCAmount.Text = "";
                    txtCashTendered.ReadOnly = false;
                    btnCheckout.Enabled = false;
                }
                else
                {
                    this.DialogResult = DialogResult.Cancel;

                    this.Close();
                }
            }
        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            double.TryParse(txtCashTendered.Text, out cashTendered);

            change = cashTendered - total;

            //if (cartTotal > 4.99)
            //{
                //open drawer
                common.OpenDrawer();

                ArrayList cartToSave = arCart;

                Voodoo.Objects.InventoryItem newCart = new Voodoo.Objects.InventoryItem();
                newCart.Name = "New Cart_"+ ((CashRegister)this.Owner).Employee.ID.ToString() +"_"+ DateTime.Now.ToShortTimeString();
                newCart.Description = "Cash for " + cashTendered.ToString("N2") + " and Credit Transaction for "+ change.ToString("N2");
                newCart.Price = total;

                cartToSave.Insert(0, newCart);

                common.RecordTransaction(cartToSave);

                //updateGiftCertificates();

                cartToSave.Remove(newCart);

                btnCancel.Text = "Done";

                ////clear cart
                //if (MessageBox.Show("Would you like to clear the cart?", "Clear Cart?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                //{
                //    arCart.Clear();
                //    //displayCart();
                //}

                //populateTransactionDates();
            //}
            //else
            //    MessageBox.Show("There is a $5 minimum on credit card transactions");
        }
    }
}
