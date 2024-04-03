using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GANNDesign.ui.components
{
    class UIBodyDialogAngularSpring : UIBodyDialog
    {
        public UIBodyDialogAngularSpring()
        {
            this.Text = "Angular Spring Data";

            label1.Text = "Stiffness:";
            label2.Text = "Damping:";
            checkBox1.Text = "Is Muscle";
            checkBox2.Visible = false;
            label3.Text = "Muscle Gain:";
            label3.Visible = false;
            textBox3.Visible = false;
        }

        public float Stiffness
        {
            get { return Param1; }
            set { Param1 = value; }
        }

        public float Damping
        {
            get { return Param2; }
            set { Param2 = value; }
        }

        public bool IsMuscle
        {
            get { return CheckBox1; }
            set { CheckBox1 = value; }
        }

        public float MuscleGain
        {
            get { return Param3; }
            set { Param3 = value; }
        }
        
        protected override void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            label3.Visible = CheckBox1;
            textBox3.Visible = CheckBox1;
        }
    }
}
