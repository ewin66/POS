using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace VoodooPOS.objects
{
    public class L_inventoryItemsToCategories
    {
        int id = -1;
        int inventoryItemID = -1;
        int categoryID = -1;
        DateTime dateCreated = DateTime.Now;

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

        public int CategoryID
        {
            get { return categoryID; }
            set { categoryID = value; }
        }
    }
}
