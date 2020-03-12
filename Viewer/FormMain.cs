using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenShoot
{
    public partial class FormMain : Form
    {
        private ScreenShotModel screenShotModel;

        public FormMain()
        {
            InitializeComponent();
            screenShotModel = new ScreenShotModel(this);
        }

        private void btnShoot_Click(object sender, EventArgs e)
        {
            screenShotModel.showAllScreens();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(richTextBox.Text);
        }

        private void richTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button.Equals(MouseButtons.Right))
            {
                contextMenuStrip.Show(this, e.Location);
            }
        }

        private void toolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.Clear();
        }

        public void arrangeContentReceiveEvent(Object sender,ArrangeEventArgs args)
        {
            //非创建线程调用UI,使用委托操作;
            if (this.InvokeRequired)
            {
                try
                {
                    Invoke(new Action<Object, ArrangeEventArgs>(arrangeContentReceiveEvent), sender, args);
                }
                catch (System.Exception)
                {
                    //disable form destroy exception
                }
                return;
            }

            this.richTextBox.AppendText(args.sbContent.ToString());
        }
    }
}
