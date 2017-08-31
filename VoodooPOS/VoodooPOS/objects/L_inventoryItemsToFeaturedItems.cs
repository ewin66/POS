using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace VoodooPOS
{
    public class L_inventoryItemsToFeaturedItems
    {
        int id = -1;
        int inventoryItemID = -1;
        DateTime expirationDate;
        bool active = false;
        DateTime dateCreated;

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

        public DateTime ExpirationDate
        {
            get { return expirationDate; }
            set { expirationDate = value; }
        }

        public bool Active
        {
            get { return active; }
            set { active = value; }
        }
    }
}
