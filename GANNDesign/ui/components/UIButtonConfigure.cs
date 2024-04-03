using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using MathLib.utils;

namespace GANNDesign.ui.components
{
    class UIButtonConfigure : UIButton
    {
        PointF[] m_gear;
        Point m_axis_point;

        public UIButtonConfigure(Rectangle bounds)
            : base(bounds)
        {
            m_gear = new PointF[320];
            const float r_i = 10.0f;
            const float r_o = 12.0f;
            const float ang_step = 20.0f;// * (float)Math.PI / 180.0f;
            for (int i = 0; i < m_gear.Length; i++)
            {
                float t = (float)i / (float)m_gear.Length;
                if ((int)(t * 360.0f / ang_step) % 2 == 0)
                    m_gear[i] = new PointF(
                        40.0f + r_i * (float)Math.Cos(2.0 * Math.PI * t),
                        15.0f + r_i * (float)Math.Sin(2.0 * Math.PI * t));
                else
                    m_gear[i] = new PointF(
                        40.0f + r_o * (float)Math.Cos(2.0 * Math.PI * t),
                        15.0f + r_o * (float)Math.Sin(2.0 * Math.PI * t));
            }
            for (int i = 0; i < m_gear.Length; i++)
                m_gear[i] = UtilsPointF.Plus(m_gear[i], bounds.Location);

            m_axis_point = new Point(40, 15);
            m_axis_point.Offset(bounds.Location);
        }

        public override void Draw(Graphics g)
        {
            base.Draw(g);

            g.DrawPolygon(Pens.Black, m_gear);
            g.DrawEllipse(Pens.Black, m_axis_point.X - 5, m_axis_point.Y - 5, 10, 10);
        }

        public override ButtonType Type
        {
            get
            {
                return ButtonType.Configure;
            }
        }
    }
}
