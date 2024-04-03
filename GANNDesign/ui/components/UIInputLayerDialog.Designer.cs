namespace GANNDesign.ui.components
{
    partial class UIInputLayerDialog
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
            this.listViewSignals = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // listViewSignals
            // 
            this.listViewSignals.CheckBoxes = true;
            this.listViewSignals.GridLines = true;
            this.listViewSignals.Location = new System.Drawing.Point(12, 12);
            this.listViewSignals.Name = "listViewSignals";
            this.listViewSignals.Size = new System.Drawing.Size(260, 197);
            this.listViewSignals.TabIndex = 4;
            this.listViewSignals.UseCompatibleStateImageBehavior = false;
            this.listViewSignals.View = System.Windows.Forms.View.List;
            // 
            // UIInputLayerDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.listViewSignals);
            this.Name = "UIInputLayerDialog";
            this.Text = "Input Layer Data";
            this.Load += new System.EventHandler(this.UIInputLayerDialog_Load);
            this.Controls.SetChildIndex(this.listViewSignals, 0);
            this.Controls.SetChildIndex(this.buttonOK, 0);
            this.Controls.SetChildIndex(this.buttonCancel, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewSignals;

    }
}