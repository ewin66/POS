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
    public partial class GiftCertificateFormCreate : Form
    {
        int amount = 0;
        mode currentMode = mode.Create;
        XmlData xmlData = new XmlData(Application.StartupPath);
        GiftCertificate currentGiftCertificate = new GiftCertificate();

        public enum mode
        {
            Create, Redeem
        }

        public mode Mode
        {
            set 
            { 
                currentMode = value;

                switch (currentMode)
                {
                    case mode.Create:
                        btnCreate.Enabled = true;
                        txtName.Focus();
                        break;
                    case mode.Redeem:
                        btnCreate.Enabled = false;
                        break;
                }
            }
        }

        public int Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        public GiftCertificate CurrentGiftCertificate
        {
            set { currentGiftCertificate = value; }
            get { return currentGiftCertificate; }
        }

        public GiftCertificateFormCreate()
        {
            InitializeComponent();

            this.DialogResult = DialogResult.Cancel;

            switch (currentMode)
            {
                case mode.Create:
                    txtName.Focus();
                    break;
                case mode.Redeem:
                    txtAmount.Focus();
                    break;
            }
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            amount = 0;

            this.DialogResult = DialogResult.Cancel;

            this.Close();
        }

        /// <summary>
        /// Create a gift certificate using the given information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreate_Click(object sender, EventArgs e)
        {
            createGiftCertificate();
        }

        private void createGiftCertificate()
        {
            if (txtAmount.Text.Trim().Length > 0)
            {
                int giftCertificateID = -1;
                double amount = -1;
                double.TryParse(txtAmount.Text.Trim(), out amount);

                currentGiftCertificate.Name = txtName.Text.Trim();
                currentGiftCertificate.Amount = amount;
                currentGiftCertificate.OriginalAmount = amount;
                currentGiftCertificate.Status = GiftCertificate.GiftCertificateStatus.NotActivated.ToString();
                currentGiftCertificate.DisplayName = currentGiftCertificate.Name + " " + currentGiftCertificate.DateCreated.ToShortDateString() + "  " + currentGiftCertificate.Amount.ToString("C");

                //save certificate info
                giftCertificateID = xmlData.Insert(currentGiftCertificate, XmlData.Tables.GiftCertificates);

                currentGiftCertificate.ID = giftCertificateID;
                currentGiftCertificate.AmountToApply = amount * -1;

                //save giftCertificate activity
                GiftCertificateActivity giftCertificateActivity = new GiftCertificateActivity();
                giftCertificateActivity.Activity = "Gift certificate created";
                giftCertificateActivity.GiftCertificateID = giftCertificateID;
                giftCertificateActivity.BeginningBalance = 0;
                giftCertificateActivity.EndingBalance = amount;

                xmlData.Insert(giftCertificateActivity, XmlData.Tables.GiftCertificateActivity);

                //create barcode
                Ean13 ean13 = new Ean13();
                ean13.CountryCode = "";// 
                ean13.ManufacturerCode = String.Format("{0:MMddyyyy}", currentGiftCertificate.DateCreated);
                ean13.ManufacturerCode += String.Format("{0:HHHmm}", currentGiftCertificate.DateCreated);
                ean13.ProductCode = currentGiftCertificate.ID.ToString();// the gift certificate ID;

                //if (txtChecksumDigit.Text.Length > 0)
                //    ean13.ChecksumDigit = txtChecksumDigit.Text;

                ean13.Scale = (float)Convert.ToDecimal(1);

                System.Drawing.Bitmap bmpBarcode = ean13.CreateBitmap();

                currentGiftCertificate.UPC = ean13.CountryCode + ean13.ManufacturerCode + ean13.ProductCode + ean13.ChecksumDigit;

                //print gift certificate
                Printer printer = new Printer(Application.StartupPath);

                if (currentGiftCertificate.Name.Length > 0)
                    printer.PrintGiftCertificate(currentGiftCertificate.Name, currentGiftCertificate.Amount, currentGiftCertificate.ID, bmpBarcode);
                else
                    printer.PrintGiftCertificate(currentGiftCertificate.Amount, currentGiftCertificate.ID, bmpBarcode);

                this.DialogResult = DialogResult.OK;

                this.Close();
            }
            else
                MessageBox.Show("Amount is required");
        }

        private void btnRedeem_Click(object sender, EventArgs e)
        {
            this.Close();

            this.DialogResult = DialogResult.Retry;
        }
    }
}
