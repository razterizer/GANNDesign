using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GANNDesign.ui.components
{
    class UIOutputLayer : UILayer
    {
        public UIOutputLayer(Rectangle layer_box_bounds)
            : base(layer_box_bounds, "Output Layer", Brushes.LightPink)
        {
        }

        public override bool MouseMove(Point p)
        {
            return false;
        }

        public new int NumNeurons
        {
            get { return base.NumNeurons; }
            set { m_num_neurons = value; }
        }
    }
}
