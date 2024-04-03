using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace GANNDesign.ui.components
{
    class UILayer
    {
        Rectangle m_layer_box_bounds;
        protected int m_num_neurons;
        string m_title;
        Brush m_bkg_brush;
        Point m_mouse_down;

        public UILayer(Rectangle layer_box_bounds, string title, Brush bkg_brush)
        {
            m_layer_box_bounds = layer_box_bounds;
            m_title = title;
            m_bkg_brush = bkg_brush;
            m_num_neurons = 0;
        }

        public virtual void Draw(Graphics g)
        {
            g.FillRectangle(m_bkg_brush, m_layer_box_bounds);
            g.DrawRectangle(Pens.Black, m_layer_box_bounds);
            string str = m_title + " (" + m_num_neurons + ")";
            SizeF str_size = g.MeasureString(str, SystemFonts.DefaultFont);
            g.DrawString(str, SystemFonts.DefaultFont, Brushes.Black, 
                m_layer_box_bounds.X + 0.5f * (m_layer_box_bounds.Width - str_size.Width), 
                m_layer_box_bounds.Y + 0.5f * (m_layer_box_bounds.Height - str_size.Height));
        }

        public Rectangle LayerBoxBounds
        {
            get { return m_layer_box_bounds; }
        }

        public virtual void MouseDown(Point p)
        {
            m_mouse_down = p;
        }

        public virtual bool MouseUp(Point p, IWin32Window owner)
        {
            if (this.LayerBoxBounds.Contains(m_mouse_down) &&
                this.LayerBoxBounds.Contains(p))
            {
                return true;
            }
            return false;
        }

        public virtual bool MouseMove(Point p)
        {
            return m_layer_box_bounds.Contains(p);
        }

        public virtual int NumNeurons
        {
            get { return m_num_neurons; }
        }
    }
}
