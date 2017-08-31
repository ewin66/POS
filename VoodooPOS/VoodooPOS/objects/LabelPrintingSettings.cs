using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoodooPOS.objects
{
    class LabelPrintingSettings
    {
        int id = -1;
        int startingRow = -1;
        int startingColumn = -1;
        int numColumns = -1;
        int numRows = -1;
        DateTime dateCreated = DateTime.MinValue;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public int StartingRowIndex
        {
            get { return startingRow; }
            set { startingRow = value; }
        }

        public int StartingColumnIndex
        {
            get { return startingColumn; }
            set { startingColumn = value; }
        }

        public int NumColumns
        {
            get { return numColumns; }
            set { numColumns = value; }
        }

        public DateTime DateCreated
        {
            get { return dateCreated; }
            set { dateCreated = value; }
        }
    }
}
