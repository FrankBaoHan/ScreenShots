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
    public partial class FormCutter : Form
    {
        private ScreenShotModel screenShotModel;
        private int index;

        private bool isCutStart = false, isFinish = false;
        private Point startPoint;

        private Rectangle cutRectangle;

        public ArrangeEvent arrangeDoneEvent;

        public FormCutter(int index, ScreenShotModel screenShotModel)
        {
            InitializeComponent();

            this.screenShotModel = screenShotModel;
            this.index = index;
        }

        private void FormCutter_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button.Equals(MouseButtons.Left))
            {
                if (!isCutStart)
                {
                    isCutStart = true;

                    startPoint = e.Location;
                }
            }

            if (e.Button.Equals(MouseButtons.Right))
            {
                foreach (Form cutForm in screenShotModel.cutForms)
                {
                    cutForm.Close();
                }
            }
        }

        private void FormCutter_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button.Equals(MouseButtons.Left))
            {
                if (isCutStart)
                {
                    isCutStart = false;
                    isFinish = true;
                }
            }   
        }

        private void FormCutter_MouseMove(object sender, MouseEventArgs e)
        {
            if (isCutStart)
            {
                Bitmap copyBmp = (Bitmap)screenShotModel.bitMaps[index].Clone();

                int width = Math.Abs(e.X - startPoint.X);
                int height = Math.Abs(e.Y - startPoint.Y);

                Point tempStartPoint = new Point(startPoint.X, startPoint.Y);

                if (e.X < startPoint.X)
                {
                    tempStartPoint.X = e.X;
                }

                if (e.Y < startPoint.Y)
                {
                    tempStartPoint.Y = e.Y;
                }

                Graphics graphics = Graphics.FromImage(copyBmp);
                Pen pen = new Pen(Color.GhostWhite, 1.5f);            

                cutRectangle = new Rectangle(tempStartPoint, new Size(width, height));

                graphics.DrawRectangle(pen, cutRectangle);

                graphics.Dispose();
                pen.Dispose();

                Graphics graphicsCache = this.CreateGraphics();
                graphicsCache.DrawImage(copyBmp, new Point(0, 0));

                graphicsCache.Dispose();
                copyBmp.Dispose();
            }
        }

        private void FormCutter_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && isFinish)
            {
                if (cutRectangle.Width != 0 && cutRectangle.Height != 0)
                {
                    Bitmap catchedBmp = new Bitmap(cutRectangle.Width, cutRectangle.Height);

                    Graphics graphics = Graphics.FromImage(catchedBmp);

                    graphics.DrawImage(
                        screenShotModel.bitMaps[index],
                        new Rectangle(0, 0, cutRectangle.Width, cutRectangle.Height),
                        cutRectangle,
                        GraphicsUnit.Pixel);

                    //Clipboard.SetImage(catchedBmp);
                    StringBuilder sbContent = RecognitionHelper.recognize(catchedBmp);
                    sbContent = CodeArrangeHelper.arrangeByGLAck(sbContent);

                    if (arrangeDoneEvent != null)
                    {
                        arrangeDoneEvent.Invoke(this, new ArrangeEventArgs(sbContent));
                    }

                    graphics.Dispose();
                    isFinish = false;

                    catchedBmp.Dispose();
                }

                foreach(Form cutForm in screenShotModel.cutForms)
                {
                    cutForm.Close();
                }

                screenShotModel.cutForms.Clear();
            }
        }
    }
}
