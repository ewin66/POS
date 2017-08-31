using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoodooPOS
{
    class FlowPanel : System.Windows.Forms.FlowLayoutPanel
    {
        public FlowPanel()
            : base()
        {
            this.DoubleBuffered = true;
        }
    }
}
