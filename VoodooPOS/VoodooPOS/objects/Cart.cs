using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace VoodooPOS.objects
{
    class Cart
    {
        int id = 0;
        string name = "";
        ArrayList cart = new ArrayList();
        DateTime createdDate = DateTime.Now;

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

        public ArrayList CartToSave
        {
            get { return cart; }
            set { cart = value; }
        }

        public DateTime CreatedDate
        {
            get { return createdDate; }
        }
    }
}
