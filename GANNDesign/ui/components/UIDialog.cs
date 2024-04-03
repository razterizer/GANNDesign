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
    public partial class UIDialog : Form
    {
        public UIDialog()
        {
            InitializeComponent();
        }

        protected virtual void buttonOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        protected virtual void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        protected static float get_float_value(TextBox text_box)
        {
            float f_result;
            if (float.TryParse(text_box.Text,
                System.Globalization.NumberStyles.Any,
                System.Globalization.NumberFormatInfo.InvariantInfo,
                out f_result))
            {
                return f_result;
            }
            return float.NaN;
        }

        protected static void set_float_value(TextBox text_box, float value)
        {
            text_box.Text = value.ToString(System.Globalization.NumberFormatInfo.InvariantInfo);
        }

        protected static int get_int_value(TextBox text_box)
        {
            int i_result;
            if (int.TryParse(text_box.Text,
                out i_result))
            {
                return i_result;
            }
            return 0;
        }

        protected static void set_int_value(TextBox text_box, int value)
        {
            text_box.Text = value.ToString();
        }
    }
}
