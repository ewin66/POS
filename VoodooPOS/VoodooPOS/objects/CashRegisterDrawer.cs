using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoodooPOS.objects
{
    public class CashRegisterDrawer
    {
        int id = 0;
        double amount = 0;
        string description = "";
        string reasonForChange = "";
        DateTime dateCreated = DateTime.Now;
        
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public double Amount
        {
            get { return amount; }
            set {  amount = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public string ReasonForChange
        {
            get { return reasonForChange; }
            set { reasonForChange = value; }
        }

        public DateTime DateCreated
        {
            get { return dateCreated; }
            set { dateCreated = value; }
        }
    }
}
