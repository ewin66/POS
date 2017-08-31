using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoodooPOS
{
    class GiftCertificateActivity
    {
        int id = 0;
        int giftCertificateID = 0;
        double beginningBalance = 0;
        double endingBalance = 0;
        string activity = "";
        DateTime dateCreated = DateTime.Now;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public int GiftCertificateID
        {
            get { return giftCertificateID; }
            set { giftCertificateID = value; }
        }

        public string Activity
        {
            get { return activity; }
            set { activity = value; }
        }

        public double BeginningBalance
        {
            get { return beginningBalance; }
            set { beginningBalance = value; }
        }

        public double EndingBalance
        {
            get { return endingBalance; }
            set { endingBalance = value; }
        }

        public DateTime DateCreated
        {
            get { return dateCreated; }
        }
    }
}
