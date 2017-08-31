using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoodooPOS.objects
{
    class Categories
    {
        int id = -1;
        string category = "";

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public string Category
        {
            get { return category; }
            set { category = value; }
        }
    }
}
