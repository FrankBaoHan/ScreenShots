using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenShoot
{
    public class ScreenShotModel
    {
        public List<Bitmap> bitMaps = new List<Bitmap>();
        public List<Form> cutForms = new List<Form>();

        private FormMain formMain;

        public ScreenShotModel(FormMain formMain)
        {
            this.formMain = formMain;

            Screen[] screens = Screen.AllScreens;

            foreach (Screen screen in screens)
            {
                bitMaps.Add(new Bitmap(screen.Bounds.Width, screen.Bounds.Height));
            }
        }

        public void showAllScreens()
        {
            Screen[] screens = Screen.AllScreens;

            int index = 0;
            foreach (Screen screen in screens)
            {
                Graphics g = Graphics.FromImage(bitMaps[index]);

                g.CopyFromScreen(
                    screen.Bounds.Location,
                    new Point(0, 0),
                    new Size(screen.Bounds.Width,
                    screen.Bounds.Height));

                FormCutter formCutter = new FormCutter(index, this);
                formCutter.BackgroundImage = bitMaps[index];

                formCutter.Location = screen.WorkingArea.Location;

                formCutter.Show();

                cutForms.Add(formCutter);

                formCutter.arrangeDoneEvent += formMain.arrangeContentReceiveEvent;

                index++;
            }
        }
    }
}
