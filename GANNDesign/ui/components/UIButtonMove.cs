using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GANNDesign.ui.components
{
    class UIButtonMove : UIButton
    {
        Rectangle m_node;
        Point[] m_E_arrow;
        Point[] m_N_arrow;
        Point[] m_W_arrow;
        Point[] m_S_arrow;

        public UIButtonMove(Rectangle bounds)
            : base(bounds)
        {
            m_node = new Rectangle(new Point(40 - 2, 15 - 2), new Size(4, 4));
            m_node.Offset(bounds.Location);

            m_E_arrow = new Point[7];
            m_E_arrow[0] = new Point(50, 17);
            m_E_arrow[1] = new Point(60, 17);
            m_E_arrow[2] = new Point(60, 20);
            m_E_arrow[3] = new Point(65, 15);
            m_E_arrow[4] = new Point(60, 10);
            m_E_arrow[5] = new Point(60, 13);
            m_E_arrow[6] = new Point(50, 13);
            for (int i = 0; i < m_E_arrow.Length; i++)
                m_E_arrow[i].Offset(bounds.Location);

            m_N_arrow = new Point[7];
            m_N_arrow[0] = new Point(42, 10);
            m_N_arrow[1] = new Point(42, 7);
            m_N_arrow[2] = new Point(45, 7);
            m_N_arrow[3] = new Point(40, 2);
            m_N_arrow[4] = new Point(35, 7);
            m_N_arrow[5] = new Point(38, 7);
            m_N_arrow[6] = new Point(38, 10);
            for (int i = 0; i < m_N_arrow.Length; i++)
                m_N_arrow[i].Offset(bounds.Location);

            m_W_arrow = new Point[7];
            m_W_arrow[0] = new Point(30, 17);
            m_W_arrow[1] = new Point(20, 17);
            m_W_arrow[2] = new Point(20, 20);
            m_W_arrow[3] = new Point(15, 15);
            m_W_arrow[4] = new Point(20, 10);
            m_W_arrow[5] = new Point(20, 13);
            m_W_arrow[6] = new Point(30, 13);
            for (int i = 0; i < m_W_arrow.Length; i++)
                m_W_arrow[i].Offset(bounds.Location);

            m_S_arrow = new Point[7];
            m_S_arrow[0] = new Point(42, 20);
            m_S_arrow[1] = new Point(42, 23);
            m_S_arrow[2] = new Point(45, 23);
            m_S_arrow[3] = new Point(40, 28);
            m_S_arrow[4] = new Point(35, 23);
            m_S_arrow[5] = new Point(38, 23);
            m_S_arrow[6] = new Point(38, 20);
            for (int i = 0; i < m_S_arrow.Length; i++)
                m_S_arrow[i].Offset(bounds.Location);
        }

        public override void Draw(Graphics g)
        {
            base.Draw(g);

            g.FillEllipse(Brushes.Black, m_node);

            g.DrawPolygon(Pens.Black, m_E_arrow);
            g.DrawPolygon(Pens.Black, m_N_arrow);
            g.DrawPolygon(Pens.Black, m_W_arrow);
            g.DrawPolygon(Pens.Black, m_S_arrow);
        }

        public override ButtonType Type
        {
            get
            {
                return ButtonType.Move;
            }
        }
    }
}
