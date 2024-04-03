using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GANNDesign.ui.components
{
    class UIButtonRemove : UIButton
    {
        Point[] m_cross_line_1;
        Point[] m_cross_line_2;

        public UIButtonRemove(Rectangle bounds)
            : base(bounds)
        {
            int half_size = 8;

            m_cross_line_1 = new Point[2];
            m_cross_line_1[0] = new Point(40 - half_size, 15 - half_size);
            m_cross_line_1[1] = new Point(40 + half_size, 15 + half_size);
            for (int i = 0; i < m_cross_line_1.Length; i++)
                m_cross_line_1[i].Offset(bounds.Location);

            m_cross_line_2 = new Point[2];
            m_cross_line_2[0] = new Point(40 - half_size, 15 + half_size);
            m_cross_line_2[1] = new Point(40 + half_size, 15 - half_size);
            for (int i = 0; i < m_cross_line_2.Length; i++)
                m_cross_line_2[i].Offset(bounds.Location);
        }

        public override void Draw(Graphics g)
        {
            base.Draw(g);

            g.DrawLine(Pens.Black, m_cross_line_1[0], m_cross_line_1[1]);
            g.DrawLine(Pens.Black, m_cross_line_2[0], m_cross_line_2[1]);
        }

        public override ButtonType Type
        {
            get
            {
                return ButtonType.Remove;
            }
        }
    }
}
