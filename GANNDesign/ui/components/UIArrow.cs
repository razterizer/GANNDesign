using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GANNDesign.ui.components
{
    class UIArrow
    {
        int m_head_hwidth;
        int m_head_length;
        int m_tail_hwidth;
        Point[] m_arrow;

        public UIArrow(int head_half_width, int head_length, int tail_half_width)
        {
            m_head_hwidth = head_half_width;
            m_head_length = head_length;
            m_tail_hwidth = tail_half_width;
            m_arrow = new Point[7];
        }

        public void Draw(Graphics g, Rectangle from_box, Rectangle to_box)
        {
            int x0 = from_box.Left + from_box.Width / 2;
            m_arrow[0] = new Point(x0 - m_tail_hwidth, from_box.Bottom);
            m_arrow[1] = new Point(x0 - m_tail_hwidth, to_box.Top - m_head_length);
            m_arrow[2] = new Point(x0 - m_head_hwidth, to_box.Top - m_head_length);
            m_arrow[3] = new Point(x0, to_box.Top - 1);
            m_arrow[4] = new Point(x0 + m_head_hwidth, to_box.Top - m_head_length);
            m_arrow[5] = new Point(x0 + m_tail_hwidth, to_box.Top - m_head_length);
            m_arrow[6] = new Point(x0 + m_tail_hwidth, from_box.Bottom);

            g.FillPolygon(Brushes.White, m_arrow);
            g.DrawPolygon(Pens.Black, m_arrow);
        }
    }
}
