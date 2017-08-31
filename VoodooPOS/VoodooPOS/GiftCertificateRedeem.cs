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
    public partial class GiftCertificateFormRedeem : Form
    {
        double amountToApply = 0;
        XmlData xmlData = new XmlData(Application.StartupPath);
        GiftCertificate currentGiftCertificate = new GiftCertificate();

        public double AmountToApply
        {
            get { return amountToApply; }
        }

        public GiftCertificate CurrentGiftCertificate
        {
            get { return currentGiftCertificate; }
        }

        public GiftCertificateFormRedeem(GiftCertificate giftCertificate)
        {
            populateGiftCertificateInfo(currentGiftCertificate);

            startUp();

            if (giftCertificate.Amount > 0)
                this.BackColor = Color.LightGreen;
            else
                this.BackColor = Color.Red;
        }

        public GiftCertificateFormRedeem()
        {
            startUp();
        }

        private void startUp()
        {
            InitializeComponent();

            this.DialogResult = DialogResult.Cancel;

            populateGiftCertificates();

            //string giftCertificateStatus = "";

            //if (currentGiftCertificate.ID > 0)
            //{
                
                //switch (giftCertificateStatus)
                //{
                //    case "Active":
                //        break;
                //    case "Deactivated":
                //        break;
                //    case "Not Activated":
                //        break;
                //    default:

                //        break;
                //}

                //if(currentGiftCertificate.Status =
            //}
        }

        private void populateGiftCertificateInfo(GiftCertificate giftCertificate)
        {
            currentGiftCertificate = giftCertificate;

            txtName.Text = giftCertificate.Name;
            txtAmount.Text = giftCertificate.Amount.ToString("N");
            txtUpc.Text = giftCertificate.UPC;
            lblStatus.Text = giftCertificate.Status;

            switch (giftCertificate.Status)
            {
                case "Active":
                    this.BackColor = Color.LightGreen;
                    btnActivate.Text = "Deactivate";
                    btnApply.Enabled = true;
                    break;
                case "NotActivated":
                    btnActivate.Text = "Activate";
                    btnApply.Enabled = false;
                    this.BackColor = Color.LightBlue;
                    break;
                case "Deactivated":
                default:
                    this.BackColor = Color.LightSalmon;
                    btnActivate.Text = "Activate";
                    btnApply.Enabled = false;
                    break;
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(txtAmountToApply.Text.Trim(), out amountToApply))
                MessageBox.Show("You need to enter a valid amount");
            else if (amountToApply > currentGiftCertificate.Amount)
                MessageBox.Show("You need to enter an amount to apply that is less than the amount on the certificate");
            else
            {
                currentGiftCertificate.AmountToApply = amountToApply;

                //record the activity for this gift certificate
                //GiftCertificateActivity giftCertificateActivity = new GiftCertificateActivity();
                //giftCertificateActivity.Activity = "Gift certificate applied";
                //giftCertificateActivity.GiftCertificateID = currentGiftCertificate.ID;
                //giftCertificateActivity.BeginningBalance = currentGiftCertificate.Amount;
                //giftCertificateActivity.EndingBalance = currentGiftCertificate.Amount - amountToApply;

                //xmlData.Insert(giftCertificateActivity, XmlData.Tables.GiftCertificateActivity);

                this.DialogResult = DialogResult.OK;

                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            amountToApply = 0;

            this.DialogResult = DialogResult.Cancel;

            this.Close();
        }

        private void btnActivate_Click(object sender, EventArgs e)
        {
            //activate certificate
            currentGiftCertificate.Status = GiftCertificate.GiftCertificateStatus.Active.ToString();

            xmlData.Update(currentGiftCertificate, XmlData.Tables.GiftCertificates);

            //record the activity for this gift certificate
            GiftCertificateActivity giftCertificateActivity = new GiftCertificateActivity();
            giftCertificateActivity.Activity = "Gift certificate activated";
            giftCertificateActivity.GiftCertificateID = currentGiftCertificate.ID;
            giftCertificateActivity.BeginningBalance = currentGiftCertificate.Amount;
            giftCertificateActivity.EndingBalance = currentGiftCertificate.Amount;

            xmlData.Insert(giftCertificateActivity, XmlData.Tables.GiftCertificateActivity);

            //this.Close();
        }

        /// <summary>
        /// display the history for this gift certificate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInfo_Click(object sender, EventArgs e)
        {

        }

        private void populateGiftCertificates()
        {
            ddGiftCertificates.Items.Clear();

            DataTable dtGiftCertificates = xmlData.Select("*", "datecreated asc", XmlData.Tables.GiftCertificates);
            DateTime tempDate;
            double price = 0;

            if (dtGiftCertificates != null)
            {
                foreach (DataRow dr in dtGiftCertificates.Rows)
                {
                    if (DateTime.TryParse(dr["DateCreated"].ToString(), out tempDate))
                    {
                        double.TryParse(dr["amount"].ToString(), out price);

                        GiftCertificate newGiftCertificate = new GiftCertificate();
                        newGiftCertificate = (GiftCertificate)Common.CreateObjects.FromDataRow(dr, newGiftCertificate);
                        //newGiftCertificate.ID = int.Parse(dr["id"].ToString());
                        //newGiftCertificate.Name = dr["name"].ToString();
                        //newGiftCertificate.Amount = price;
                        //newGiftCertificate.DateCreated = DateTime.Parse(dr["dateCreated"].ToString());

                        //if (dr["displayName"] != null && dr["displayName"].ToString().Length > 0)
                        //    newGiftCertificate.DisplayName = dr["displayName"].ToString();
                        //else
                        //    newGiftCertificate.DisplayName = dr["name"].ToString();

                        //ddGiftCertificates.Items.Add(tempDate.ToShortTimeString() + " " + price.ToString("C"));
                        ddGiftCertificates.Items.Add(newGiftCertificate);

                    }
                }
            }

            ddGiftCertificates.Items.Insert(0, "Choose Gift Certificate To View");
            ddGiftCertificates.SelectedIndex = 0;
        }

        private void ddGiftCertificates_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddGiftCertificates.SelectedItem is GiftCertificate)
            {
                currentGiftCertificate = (GiftCertificate)ddGiftCertificates.SelectedItem;

                populateGiftCertificateInfo(currentGiftCertificate);
            }
        }
    }
}
