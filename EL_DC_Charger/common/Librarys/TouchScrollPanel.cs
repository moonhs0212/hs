using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp_Bunifu
{
    public class TouchScrollPanel
    {
        private Point mouseDownPoint;
        private Panel parentPanel;

        public TouchScrollPanel(Panel panel)
        {
            parentPanel = panel;
            AssignEvent(parentPanel);
        }

        private void AssignEvent(Control control)
        {
            control.MouseDown += MouseDown;
            control.MouseMove += MouseMove;
            foreach (Control child in control.Controls)
            {
                AssignEvent(child);
            }
        }

        private void MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            Point pointDiff = new Point(Cursor.Position.X + mouseDownPoint.X, (Cursor.Position.Y - mouseDownPoint.Y)*2);

            if ((mouseDownPoint.X == Cursor.Position.X) && (mouseDownPoint.Y == Cursor.Position.Y))
                return;

            Point currAutoS = parentPanel.AutoScrollPosition;
            parentPanel.AutoScrollPosition = new Point(Math.Abs(currAutoS.X) - pointDiff.X, Math.Abs(currAutoS.Y) - pointDiff.Y);
            mouseDownPoint = Cursor.Position;
        }

        private void MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                this.mouseDownPoint = Cursor.Position;
        }
    }
}
