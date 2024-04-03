using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GANNDesign.ui.components
{
    class UICheckBox
    {
        Rectangle m_box;
        Point[] m_check_line_1;
        Point[] m_check_line_2;
        Point m_mouse_down;

        public UICheckBox(Point location, int size)
        {
            m_box = new Rectangle(location, new Size(size, size));
            
            m_check_line_1 = new Point[2];
            m_check_line_1[0] = location;
            m_check_line_1[1] = new Point(location.X + size, location.Y + size);

            m_check_line_2 = new Point[2];
            m_check_line_2[0] = new Point(location.X, location.Y + size);
            m_check_line_2[1] = new Point(location.X + size, location.Y);

            this.Checked = false;
            this.Visible = true;
            this.Enabled = true;
        }

        public void Draw(Graphics g)
        {
            if (!this.Visible)
                return;

            Pen pen = new Pen(this.Enabled ? Color.Black : Color.Gray);
            g.DrawRectangle(pen, m_box);
            if (this.Checked)
            {
                g.DrawLine(pen, m_check_line_1[0], m_check_line_1[1]);
                g.DrawLine(pen, m_check_line_2[0], m_check_line_2[1]);
            }
        }

        public void MouseDown(Point p)
        {
            m_mouse_down = p;
        }

        public void MouseUp(Point p)
        {
            if (!this.Visible || !this.Enabled)
                return;

            if (m_box.Contains(m_mouse_down) &&
                m_box.Contains(p))
            {
                this.Checked = !this.Checked;
            }
        }

        public bool MouseMove(Point p)
        {
            return m_box.Contains(p);
        }

        public bool Checked
        {
            get;
            set;
        }

        public bool Visible
        {
            get;
            set;
        }

        public bool Enabled
        {
            get;
            set;
        }
    }
}
