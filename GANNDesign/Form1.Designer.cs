namespace GANNDesign
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.toolTipData = new System.Windows.Forms.ToolTip(this.components);
            this.tabControlDesign = new System.Windows.Forms.TabControl();
            this.tabPageBody = new System.Windows.Forms.TabPage();
            this.tabPageBrain = new System.Windows.Forms.TabPage();
            this.buttonSave = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.labelScaling = new System.Windows.Forms.Label();
            this.textBoxScaling = new System.Windows.Forms.TextBox();
            this.labelOutputGain = new System.Windows.Forms.Label();
            this.textBoxOutputGain = new System.Windows.Forms.TextBox();
            this.canvasBodyDesign = new GANNSim.Canvas();
            this.canvasBrainDesign = new GANNSim.Canvas();
            this.tabControlDesign.SuspendLayout();
            this.tabPageBody.SuspendLayout();
            this.tabPageBrain.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolTipData
            // 
            this.toolTipData.OwnerDraw = true;
            // 
            // tabControlDesign
            // 
            this.tabControlDesign.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlDesign.Controls.Add(this.tabPageBody);
            this.tabControlDesign.Controls.Add(this.tabPageBrain);
            this.tabControlDesign.Location = new System.Drawing.Point(12, 12);
            this.tabControlDesign.Name = "tabControlDesign";
            this.tabControlDesign.SelectedIndex = 0;
            this.tabControlDesign.Size = new System.Drawing.Size(686, 423);
            this.tabControlDesign.TabIndex = 0;
            // 
            // tabPageBody
            // 
            this.tabPageBody.Controls.Add(this.canvasBodyDesign);
            this.tabPageBody.Location = new System.Drawing.Point(4, 22);
            this.tabPageBody.Name = "tabPageBody";
            this.tabPageBody.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBody.Size = new System.Drawing.Size(678, 397);
            this.tabPageBody.TabIndex = 0;
            this.tabPageBody.Text = "Body";
            this.tabPageBody.UseVisualStyleBackColor = true;
            // 
            // tabPageBrain
            // 
            this.tabPageBrain.Controls.Add(this.canvasBrainDesign);
            this.tabPageBrain.Location = new System.Drawing.Point(4, 22);
            this.tabPageBrain.Name = "tabPageBrain";
            this.tabPageBrain.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBrain.Size = new System.Drawing.Size(598, 350);
            this.tabPageBrain.TabIndex = 1;
            this.tabPageBrain.Text = "Brain";
            this.tabPageBrain.UseVisualStyleBackColor = true;
            this.tabPageBrain.Enter += new System.EventHandler(this.tabPageBrain_Enter);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSave.Location = new System.Drawing.Point(12, 455);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 2;
            this.buttonSave.Text = "Save...";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "*.gannd";
            this.saveFileDialog1.Filter = "GANNSim Design files|*.gannd";
            this.saveFileDialog1.SupportMultiDottedExtensions = true;
            // 
            // buttonLoad
            // 
            this.buttonLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonLoad.Location = new System.Drawing.Point(93, 455);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(75, 23);
            this.buttonLoad.TabIndex = 3;
            this.buttonLoad.Text = "Load...";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "GANNSim Design files|*.gannd";
            this.openFileDialog1.SupportMultiDottedExtensions = true;
            // 
            // labelScaling
            // 
            this.labelScaling.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelScaling.AutoSize = true;
            this.labelScaling.Location = new System.Drawing.Point(202, 441);
            this.labelScaling.Name = "labelScaling";
            this.labelScaling.Size = new System.Drawing.Size(45, 13);
            this.labelScaling.TabIndex = 0;
            this.labelScaling.Text = "Scaling:";
            // 
            // textBoxScaling
            // 
            this.textBoxScaling.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxScaling.Location = new System.Drawing.Point(205, 457);
            this.textBoxScaling.Name = "textBoxScaling";
            this.textBoxScaling.Size = new System.Drawing.Size(100, 20);
            this.textBoxScaling.TabIndex = 1;
            this.textBoxScaling.Text = "0.5";
            // 
            // labelOutputGain
            // 
            this.labelOutputGain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelOutputGain.AutoSize = true;
            this.labelOutputGain.Location = new System.Drawing.Point(308, 442);
            this.labelOutputGain.Name = "labelOutputGain";
            this.labelOutputGain.Size = new System.Drawing.Size(67, 13);
            this.labelOutputGain.TabIndex = 0;
            this.labelOutputGain.Text = "Output Gain:";
            // 
            // textBoxOutputGain
            // 
            this.textBoxOutputGain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxOutputGain.Location = new System.Drawing.Point(311, 458);
            this.textBoxOutputGain.Name = "textBoxOutputGain";
            this.textBoxOutputGain.Size = new System.Drawing.Size(100, 20);
            this.textBoxOutputGain.TabIndex = 1;
            this.textBoxOutputGain.Text = "0.5";
            // 
            // canvasBodyDesign
            // 
            this.canvasBodyDesign.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.canvasBodyDesign.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.canvasBodyDesign.Location = new System.Drawing.Point(0, 0);
            this.canvasBodyDesign.Name = "canvasBodyDesign";
            this.canvasBodyDesign.Size = new System.Drawing.Size(678, 397);
            this.canvasBodyDesign.TabIndex = 0;
            this.canvasBodyDesign.Paint += new System.Windows.Forms.PaintEventHandler(this.canvasBodyDesign_Paint);
            this.canvasBodyDesign.MouseDown += new System.Windows.Forms.MouseEventHandler(this.canvasBodyDesign_MouseDown);
            this.canvasBodyDesign.MouseLeave += new System.EventHandler(this.canvasBodyDesign_MouseLeave);
            this.canvasBodyDesign.MouseMove += new System.Windows.Forms.MouseEventHandler(this.canvasBodyDesign_MouseMove);
            this.canvasBodyDesign.MouseUp += new System.Windows.Forms.MouseEventHandler(this.canvasBodyDesign_MouseUp);
            // 
            // canvasBrainDesign
            // 
            this.canvasBrainDesign.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.canvasBrainDesign.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.canvasBrainDesign.Location = new System.Drawing.Point(0, 0);
            this.canvasBrainDesign.Name = "canvasBrainDesign";
            this.canvasBrainDesign.Size = new System.Drawing.Size(598, 350);
            this.canvasBrainDesign.TabIndex = 1;
            this.canvasBrainDesign.Paint += new System.Windows.Forms.PaintEventHandler(this.canvasBrainDesign_Paint);
            this.canvasBrainDesign.MouseDown += new System.Windows.Forms.MouseEventHandler(this.canvasBrainDesign_MouseDown);
            this.canvasBrainDesign.MouseLeave += new System.EventHandler(this.canvasBrainDesign_MouseLeave);
            this.canvasBrainDesign.MouseMove += new System.Windows.Forms.MouseEventHandler(this.canvasBrainDesign_MouseMove);
            this.canvasBrainDesign.MouseUp += new System.Windows.Forms.MouseEventHandler(this.canvasBrainDesign_MouseUp);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 490);
            this.Controls.Add(this.textBoxOutputGain);
            this.Controls.Add(this.labelOutputGain);
            this.Controls.Add(this.textBoxScaling);
            this.Controls.Add(this.labelScaling);
            this.Controls.Add(this.buttonLoad);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.tabControlDesign);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GANNDesign";
            this.tabControlDesign.ResumeLayout(false);
            this.tabPageBody.ResumeLayout(false);
            this.tabPageBrain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GANNSim.Canvas canvasBodyDesign;
        private System.Windows.Forms.ToolTip toolTipData;
        private System.Windows.Forms.TabControl tabControlDesign;
        private System.Windows.Forms.TabPage tabPageBody;
        private System.Windows.Forms.TabPage tabPageBrain;
        private GANNSim.Canvas canvasBrainDesign;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label labelScaling;
        private System.Windows.Forms.TextBox textBoxScaling;
        private System.Windows.Forms.Label labelOutputGain;
        private System.Windows.Forms.TextBox textBoxOutputGain;
    }
}

