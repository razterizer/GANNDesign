using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GANNDesign.ui.components
{
    class UIButton
    {
        Rectangle m_bounds;
        public enum ButtonType { None, LinearSpring, AngularSpring, Move, Remove, Configure };

        public UIButton(Rectangle bounds)
        {
            m_bounds = bounds;
            this.Pressed = false;
        }

        public virtual void Draw(Graphics g)
        {
            g.FillRectangle(BackgroundBrush, m_bounds);
            g.DrawRectangle(Pens.Black, m_bounds);
        }

        public bool Pressed
        {
            get;
            set;
        }

        public Rectangle Bounds
        {
            get { return m_bounds; }
        }

        protected Brush BackgroundBrush
        {
            get
            {
                if (this.Pressed)
                    return Brushes.Khaki;
                else
                    return Brushes.Cornsilk;
            }
        }

        public virtual ButtonType Type
        {
            get { return ButtonType.None; }
        }
    }
}
