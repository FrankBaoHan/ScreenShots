using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenShoot
{
    public delegate void ArrangeEvent(Object sender, ArrangeEventArgs args);

    public class ArrangeEventArgs : EventArgs
    {
        public ArrangeEventArgs(StringBuilder sbContent)
        {
            this.sbContent = sbContent;
        }

        public StringBuilder sbContent { get; set; }
    }
}
