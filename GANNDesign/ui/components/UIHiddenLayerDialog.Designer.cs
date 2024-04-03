namespace GANNDesign.ui.components
{
    partial class UIHiddenLayerDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelNumNeurons = new System.Windows.Forms.Label();
            this.textBoxNumNeurons = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(131, 82);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(45, 82);
            // 
            // labelNumNeurons
            // 
            this.labelNumNeurons.AutoSize = true;
            this.labelNumNeurons.Location = new System.Drawing.Point(12, 31);
            this.labelNumNeurons.Name = "labelNumNeurons";
            this.labelNumNeurons.Size = new System.Drawing.Size(60, 13);
            this.labelNumNeurons.TabIndex = 4;
            this.labelNumNeurons.Text = "# Neurons:";
            // 
            // textBoxNumNeurons
            // 
            this.textBoxNumNeurons.Location = new System.Drawing.Point(78, 28);
            this.textBoxNumNeurons.Name = "textBoxNumNeurons";
            this.textBoxNumNeurons.Size = new System.Drawing.Size(100, 20);
            this.textBoxNumNeurons.TabIndex = 5;
            // 
            // UIHiddenLayerDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(223, 117);
            this.Controls.Add(this.labelNumNeurons);
            this.Controls.Add(this.textBoxNumNeurons);
            this.Name = "UIHiddenLayerDialog";
            this.Text = "Hidden Layer Data";
            this.Controls.SetChildIndex(this.textBoxNumNeurons, 0);
            this.Controls.SetChildIndex(this.labelNumNeurons, 0);
            this.Controls.SetChildIndex(this.buttonOK, 0);
            this.Controls.SetChildIndex(this.buttonCancel, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelNumNeurons;
        private System.Windows.Forms.TextBox textBoxNumNeurons;
    }
}