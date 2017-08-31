using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace VoodooPOS
{
    public class InventoryItem_depricated
    {
        int id = -1;
        string name = "";
        string description = "";
        string upc = "";
        double price = 0;
        double wholesalePrice = 0;
        double salePrice = 0;
        string manufacturer = "";
        string model = "";
        string size = "";
        string color = "";
        int quantity = 0;
        string picturePath = "";
        string status = "";
        bool onSale = false;
        string source = "";
        int categoryID = -1;

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

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public string UPC
        {
            get { return upc; }
            set { upc = value; }
        }

        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        public double WholesalePrice
        {
            get { return wholesalePrice; }
            set { wholesalePrice = value; }
        }

        public double SalePrice
        {
            get { return salePrice; }
            set { salePrice = value; }
        }

        public string Manufacturer
        {
            get { return manufacturer; }
            set { manufacturer = value; }
        }

        public string Model
        {
            get { return model; }
            set { model = value; }
        }

        public string Size
        {
            get { return size; }
            set { size = value; }
        }

        public string Color
        {
            get { return color; }
            set { color = value; }
        }

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public string PicturePath
        {
            get { return picturePath; }
            set { picturePath = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public bool OnSale
        {
            get { return onSale; }
            set { onSale = value; }
        }

        public string Source
        {
            get { return source; }
            set { source = value; }
        }

        public int CategoryID
        {
            get { return categoryID; }
            set { categoryID = value; }
        }
    }
}
