using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GANNDesign.ui.components
{
    public partial class UIHiddenLayerDialog : UIDialog
    {
        public UIHiddenLayerDialog()
        {
            InitializeComponent();
        }

        public int NumNeurons
        {
            get { return get_int_value(textBoxNumNeurons); }
            set { set_int_value(textBoxNumNeurons, value); }
        }
    }
}
