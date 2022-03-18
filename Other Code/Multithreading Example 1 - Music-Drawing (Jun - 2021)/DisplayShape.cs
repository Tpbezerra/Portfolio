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
    public class DisplayShape
    {
        int triangleX = 25;
        int triangleY = 25;

        int triangleXIncrease = 3;
        int triangleYIncrease = 3;

        float topAngle = -90;

        Panel drawPanel;

        public DisplayShape(ref Panel drawPanel)
        {
            this.drawPanel = drawPanel;
        }

        public void UpdateTrianglePosition()
        {
            triangleX += triangleXIncrease;
            triangleY += triangleYIncrease;
            topAngle += 1;

            if ((triangleX <= 25 && triangleXIncrease < 0) || (triangleX >= drawPanel.Width - 25 && triangleXIncrease > 0))
                triangleXIncrease *= -1;

            if ((triangleY <= 25 && triangleYIncrease < 0) || (triangleY >= drawPanel.Height - 25 && triangleYIncrease > 0))
                triangleYIncrease *= -1;

            drawPanel.Invalidate();
            PaintOnDrawPanel();

            Thread.Sleep(33);

            UpdateTrianglePosition();
        }

        public void PaintOnDrawPanel()
        {
            if (drawPanel.IsDisposed)
            {
                Thread.CurrentThread.Abort();
                return;
            }

            Graphics g = drawPanel.CreateGraphics();
            Pen pen = new Pen(Color.Black);

            Point[] trianglePoints = new Point[4];
            trianglePoints[0] = new Point(triangleX + GetPositionOffsetFromAngle(topAngle).X, triangleY + GetPositionOffsetFromAngle(topAngle).Y);
            trianglePoints[1] = new Point(triangleX + GetPositionOffsetFromAngle(topAngle + 120).X, triangleY + GetPositionOffsetFromAngle(topAngle + 120).Y);
            trianglePoints[2] = new Point(triangleX + GetPositionOffsetFromAngle(topAngle + 240).X, triangleY + GetPositionOffsetFromAngle(topAngle + 240).Y);
            trianglePoints[3] = new Point(triangleX + GetPositionOffsetFromAngle(topAngle).X, triangleY + GetPositionOffsetFromAngle(topAngle).Y);

            g.DrawLines(pen, trianglePoints);
        }

        Point GetPositionOffsetFromAngle(float angle)
        {
            float xOffset = (float)Math.Cos((double)angle * (Math.PI / 180d)) * 25;
            float yOffset = (float)Math.Sin((double)angle * (Math.PI / 180d)) * 25;
            return new Point((int)xOffset, (int)yOffset);
        }
    }
}
