using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoodooPOS
{
    public class GiftCertificate
    {
        public enum GiftCertificateStatus
        {
            Active, Deactivated, NotActivated
        }

        int id = 0;
        string name = "";
        double amount = 0;
        double originalAmount = 0;
        double amountToApply = 0;
        string status = GiftCertificateStatus.NotActivated.ToString();
        string upc = "";
        DateTime dateCreated = DateTime.Now;
        string displayName = "";

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string DisplayName
        {
            get { return displayName; }
            set { displayName = value; }
        }

        public double Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        public double AmountToApply
        {
            get { return amountToApply; }
            set { amountToApply = value; }
        }

        public double OriginalAmount
        {
            get { return originalAmount; }
            set { originalAmount = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public string UPC
        {
            get { return upc; }
            set { upc = value; }
        }

        public DateTime DateCreated
        {
            get { return dateCreated; }
            set { dateCreated = value; }
        }
    }
}
