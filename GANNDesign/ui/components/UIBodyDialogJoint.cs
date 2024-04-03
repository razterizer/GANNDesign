using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GANNDesign.ui.components
{
    class UIBodyDialogJoint : UIBodyDialog
    {
        public UIBodyDialogJoint()
        {
            this.Text = "Joint Data";

            label1.Text = "Mass:";
            label2.Visible = false;
            textBox2.Visible = false;
            checkBox1.Text = "Has Contact Sensor";
            checkBox2.Text = "Is Head";
            label3.Visible = false;
            textBox3.Visible = false;
        }

        public float Mass
        {
            get { return Param1; }
            set { Param1 = value; }
        }

        public bool HasContactSensor
        {
            get { return CheckBox1; }
            set { CheckBox1 = value; }
        }

        public bool IsHead
        {
            get { return CheckBox2; }
            set { CheckBox2 = value; }
        }

        protected override void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox2)
                CheckBox1 = true;
        }

    }
}
