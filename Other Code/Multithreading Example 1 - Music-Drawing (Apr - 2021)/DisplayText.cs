using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MulProAssignment1
{
    public class DisplayText
    {
        int posX;
        int posY;

        Panel displayPanel;

        Random rand;

        public DisplayText(Random rand, ref Panel displayPanel)
        {
            this.rand = rand;
            this.displayPanel = displayPanel;
        }

        public void UpdateTextPosition()
        {
            Thread.Sleep(2000);

            posX = rand.Next(0, displayPanel.Width - 100);
            posY = rand.Next(0, displayPanel.Height - 30);

            displayPanel.Invalidate();
            PaintOnDisplayPanel();

            UpdateTextPosition();
        }

        public void PaintOnDisplayPanel()
        {
            if (displayPanel.IsDisposed)
            {
                Thread.CurrentThread.Abort();
                return;
            }

            Graphics g = displayPanel.CreateGraphics();
            SolidBrush brush = new SolidBrush(Color.Black);

            g.DrawString("Display Thread", new Font("Arial", 11), brush, posX, posY);
        }
    }
}
