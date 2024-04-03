using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GANNDesign.ui.components
{
    class UIButtonAngularSpring : UIButton
    {
        Point m_central_point;
        Point[] m_limbs;
        PointF[] m_spring_coil;
        
        public UIButtonAngularSpring(Rectangle bounds)
            : base(bounds)
        {
            m_central_point = new Point(40, 10);
            m_central_point.Offset(bounds.Location);

            m_limbs = new Point[3];
            m_limbs[0] = new Point(25, 25);
            m_limbs[1] = new Point(40, 10);
            m_limbs[2] = new Point(55, 25);
            for (int i = 0; i < m_limbs.Length; i++)
                m_limbs[i].Offset(bounds.Location);

            m_spring_coil = new PointF[630]; // 360 + 270 deg.
            float radius_expansion_factor = 3.0f;
            float start_angle = (float)(Math.PI / 4.0);
            float start_radius = 3.0f;
            for (int i = 0; i < 630; i++)
            {
                float t = (float)i / 360.0f;
                m_spring_coil[i] = new PointF(
                    m_central_point.X + (start_radius + radius_expansion_factor * t) * (float)Math.Cos(-2.0 * Math.PI * t + start_angle),
                    m_central_point.Y + (start_radius + radius_expansion_factor * t) * (float)Math.Sin(-2.0 * Math.PI * t + start_angle));
            }
        }

        public override void Draw(Graphics g)
        {
            base.Draw(g);


            g.DrawLines(Pens.Black, m_limbs);

            //g.FillEllipse(base.BackgroundBrush, m_central_point.X - 3, m_central_point.Y - 3, 6, 6);
            //g.DrawEllipse(Pens.Black, m_central_point.X - 3, m_central_point.Y - 3, 6, 6);

            g.DrawLines(Pens.Black, m_spring_coil);

            g.FillEllipse(Brushes.Black, m_limbs[0].X - 2, m_limbs[0].Y - 2, 4, 4);
            g.FillEllipse(Brushes.Black, m_limbs[1].X - 2, m_limbs[1].Y - 2, 4, 4);
            g.FillEllipse(Brushes.Black, m_limbs[2].X - 2, m_limbs[2].Y - 2, 4, 4);
        }

        public override ButtonType Type
        {
            get
            {
                return ButtonType.AngularSpring;
            }
        }
    }
}
