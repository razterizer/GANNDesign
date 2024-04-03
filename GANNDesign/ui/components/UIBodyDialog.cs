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
    public partial class UIBodyDialog : UIDialog
    {
        public UIBodyDialog()
        {
            InitializeComponent();
        }

        protected float Param1
        {
            get { return get_float_value(textBox1); }
            set { set_float_value(textBox1, value); }
        }

        protected float Param2
        {
            get { return get_float_value(textBox2); }
            set { set_float_value(textBox2, value); }
        }

        protected float Param3
        {
            get { return get_float_value(textBox3); }
            set { set_float_value(textBox3, value); }
        }

        protected bool CheckBox1
        {
            get { return checkBox1.Checked; }
            set { checkBox1.Checked = value; }
        }

        protected bool CheckBox2
        {
            get { return checkBox2.Checked; }
            set { checkBox2.Checked = value; }
        }

        protected virtual void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected virtual void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
