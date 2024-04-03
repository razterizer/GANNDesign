using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GANNDesign.ui.components
{
    class UIButtonLinearSpring : UIButton
    {
        PointF[] m_spring_coil;
        Point[] m_damper_dash;
        Point[] m_damper_pot;
        int m_x_coil_end;
        Point[] m_connect_left;
        Point[] m_connect_right;

        public UIButtonLinearSpring(Rectangle bounds)
            : base(bounds)
        {
            m_spring_coil = new PointF[1000];
            float x_radius = 2.0f;
            float y_radius = 4.0f;
            float x_drift = 4.0f;
            int num_turns = 10;
            for (int i = 0; i < 1000; i++)
            {
                float t = (float)i / 100.0f;
                m_spring_coil[i] = new PointF(
                    bounds.X + 20.0f + x_radius * (float)Math.Cos(2.0 * Math.PI * t) + t * x_drift,
                    bounds.Y + 15.0f + y_radius * (float)Math.Sin(2.0 * Math.PI * t));
                    //bounds.Y + 22.0f + y_radius * (float)Math.Sin(2.0 * Math.PI * t));
            }

            m_damper_dash = new Point[6];
            m_damper_dash[0] = new Point(20, 22);
            m_damper_dash[1] = new Point(18, 22);
            m_damper_dash[2] = new Point(18, 8);
            m_damper_dash[3] = new Point(38, 8);
            m_damper_dash[4] = new Point(38, 6);
            m_damper_dash[5] = new Point(38, 10);
            for (int i = 0; i < m_damper_dash.Length; i++)
                m_damper_dash[i].Offset(bounds.Location);

            m_damper_pot = new Point[9];
            m_x_coil_end = 20 + num_turns * (int)x_drift + (int)x_radius;
            m_damper_pot[0] = new Point(m_x_coil_end, 22);
            m_damper_pot[1] = new Point(4 + m_x_coil_end, 22);
            m_damper_pot[2] = new Point(4 + m_x_coil_end, 8);
            m_damper_pot[3] = new Point(40, 8);
            m_damper_pot[4] = new Point(40, 4);
            m_damper_pot[5] = new Point(36, 4);
            m_damper_pot[6] = new Point(40, 4);
            m_damper_pot[7] = new Point(40, 12);
            m_damper_pot[8] = new Point(36, 12);
            for (int i = 0; i < m_damper_pot.Length; i++)
                m_damper_pot[i].Offset(bounds.Location);

            m_connect_left = new Point[2];
            //m_connect_left[0] = new Point(18, 15);
            m_connect_left[0] = new Point(20, 15);
            m_connect_left[1] = new Point(6, 15);
            for (int i = 0; i < m_connect_left.Length; i++)
                m_connect_left[i].Offset(bounds.Location);

            m_connect_right = new Point[2];
            //m_connect_right[0] = new Point(4 + m_x_coil_end, 15);
            m_connect_right[0] = new Point(m_x_coil_end, 15);
            m_connect_right[1] = new Point(74, 15);
            for (int i = 0; i < m_connect_right.Length; i++)
                m_connect_right[i].Offset(bounds.Location);
        }

        public override void Draw(Graphics g)
        {
            base.Draw(g);

            g.DrawLines(Pens.Black, m_spring_coil);
            //g.DrawLines(Pens.Black, m_damper_dash);
            //g.DrawLines(Pens.Black, m_damper_pot);

            g.DrawLines(Pens.Black, m_connect_left);
            g.FillEllipse(Brushes.Black, m_connect_left[1].X - 2, m_connect_left[1].Y - 2, 4, 4);
            g.DrawLines(Pens.Black, m_connect_right);
            g.FillEllipse(Brushes.Black, m_connect_right[1].X - 2, m_connect_right[1].Y - 2, 4, 4);
        }

        public override ButtonType Type
        {
            get
            {
                return ButtonType.LinearSpring;
            }
        }
    }
}
