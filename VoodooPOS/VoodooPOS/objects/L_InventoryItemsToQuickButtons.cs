using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace VoodooPOS
{
    public class L_InventoryItemsToQuickButtons
    {
        int id = -1;
        int inventoryItemID = -1;

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
    }
}
