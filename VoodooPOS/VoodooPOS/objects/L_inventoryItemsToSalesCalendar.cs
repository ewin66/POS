using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace VoodooPOS
{
    public class L_inventoryItemsToSalesCalendar
    {
        int id = -1;
        int inventoryItemID = -1;
        DateTime startDate;
        DateTime endDate;
        double salePrice;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public int InventoryItemID
        {
            get { return inventoryItemID; }
            set { inventoryItemID = value; }
        }

        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }

        public double SalePrice
        {
            get { return salePrice; }
            set { salePrice = value; }
        }
    }
}
