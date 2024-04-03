using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace GANNDesign.ui.components
{
    class UIHiddenLayer : UILayer
    {
        UICheckBox m_checkbox_active;
        UICheckBox m_checkbox_recurrent;
        UIHiddenLayerDialog m_dialog;
 
        public UIHiddenLayer(Rectangle layer_box_bounds)
            : base(layer_box_bounds, "Hidden Layer", Brushes.Cornsilk)
        {
            Point p;
            p = LayerBoxBounds.Location;
            int checkbox_size = 8;
            int checkbox_y = (LayerBoxBounds.Height - checkbox_size) / 2;
            p.Offset(-50 - checkbox_size, checkbox_y);
            m_checkbox_active = new UICheckBox(p, checkbox_size);
            p = LayerBoxBounds.Location;
            p.Offset(LayerBoxBounds.Width + 50, checkbox_y);
            m_checkbox_recurrent = new UICheckBox(p, checkbox_size);
            m_checkbox_recurrent.Visible = false;

            m_dialog = new UIHiddenLayerDialog();
        }

        public bool Active
        {
            get { return m_checkbox_active.Checked; }
            set
            {
                m_checkbox_active.Checked = value;
                m_checkbox_recurrent.Visible = value;
            }
        }

        public bool Recurrent
        {
            get { return m_checkbox_recurrent.Checked; }
            set { m_checkbox_recurrent.Checked = value; }
        }

        public new int NumNeurons
        {
            get { return base.NumNeurons; }
            set { m_num_neurons = value; }
        }

        public override void Draw(Graphics g)
        {
            m_checkbox_active.Draw(g);
            m_checkbox_recurrent.Draw(g);

            if (m_checkbox_active.Checked)
            {
                if (m_checkbox_recurrent.Checked)
                {
                    Pen arrow = new Pen(Color.Black);
                    arrow.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
                    g.DrawArc(arrow, new Rectangle(LayerBoxBounds.Right - 1, LayerBoxBounds.Top, LayerBoxBounds.Height, LayerBoxBounds.Height), 210, 300);
                }

                base.Draw(g);
            }
        }

        public override void MouseDown(Point p)
        {
            base.MouseDown(p);
            m_checkbox_active.MouseDown(p);
            m_checkbox_recurrent.MouseDown(p);
        }

        public override bool MouseUp(Point p, IWin32Window owner)
        {
            bool point_in_layer_box = false;
            if (base.MouseUp(p, owner))
            {
                point_in_layer_box = true;
                
                m_dialog.NumNeurons = m_num_neurons;
                if (DialogResult.OK == m_dialog.ShowDialog(owner))
                {
                    m_num_neurons = m_dialog.NumNeurons;
                }
            }
            m_checkbox_active.MouseUp(p);
            m_checkbox_recurrent.MouseUp(p);

            m_checkbox_recurrent.Visible = m_checkbox_active.Checked;
            return point_in_layer_box;
        }

        public override bool MouseMove(Point p)
        {
            bool point_in_box = false;
            if (base.MouseMove(p))
                point_in_box = true;
            if (m_checkbox_active.MouseMove(p))
                point_in_box = true;
            if (m_checkbox_recurrent.MouseMove(p))
                point_in_box = true;
            return point_in_box;
        }
    }
}
