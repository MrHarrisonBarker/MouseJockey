
using System.Windows.Forms;

namespace MouseJockey
{
    partial class ControlGlass
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.playBack1Control = new System.Windows.Forms.Panel();
            this.grandMasterControl = new System.Windows.Forms.Panel();
            this.grandMasterHold = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 46);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(168, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(150, 46);
            this.button2.TabIndex = 1;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // radioButton1
            // 
            this.radioButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Enabled = false;
            this.radioButton1.Location = new System.Drawing.Point(602, 1988);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(27, 26);
            this.radioButton1.TabIndex = 2;
            this.radioButton1.TabStop = true;
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // trackBar1
            // 
            this.trackBar1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.trackBar1.BackColor = System.Drawing.SystemColors.Control;
            this.trackBar1.Location = new System.Drawing.Point(758, 1720);
            this.trackBar1.Maximum = 255;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar1.Size = new System.Drawing.Size(90, 202);
            this.trackBar1.TabIndex = 3;
            // 
            // playBack1Control
            // 
            this.playBack1Control.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.playBack1Control.BackColor = System.Drawing.Color.White;
            this.playBack1Control.Location = new System.Drawing.Point(617, 1788);
            this.playBack1Control.Name = "playBack1Control";
            this.playBack1Control.Size = new System.Drawing.Size(50, 159);
            this.playBack1Control.TabIndex = 4;
            // 
            // grandMasterControl
            // 
            this.grandMasterControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.grandMasterControl.BackColor = System.Drawing.Color.White;
            this.grandMasterControl.Location = new System.Drawing.Point(45, 1788);
            this.grandMasterControl.Name = "grandMasterControl";
            this.grandMasterControl.Size = new System.Drawing.Size(50, 159);
            this.grandMasterControl.TabIndex = 6;
            // 
            // grandMasterHold
            // 
            this.grandMasterHold.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.grandMasterHold.AutoSize = true;
            this.grandMasterHold.Checked = true;
            this.grandMasterHold.Enabled = false;
            this.grandMasterHold.Location = new System.Drawing.Point(30, 1988);
            this.grandMasterHold.Name = "grandMasterHold";
            this.grandMasterHold.Size = new System.Drawing.Size(27, 26);
            this.grandMasterHold.TabIndex = 5;
            this.grandMasterHold.TabStop = true;
            this.grandMasterHold.UseVisualStyleBackColor = true;
            // 
            // ControlGlass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::MouseJockey.Properties.Resources.BackgroundImage;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(3394, 2036);
            this.Controls.Add(this.grandMasterControl);
            this.Controls.Add(this.grandMasterHold);
            this.Controls.Add(this.playBack1Control);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.MinimizeBox = false;
            this.Name = "ControlGlass";
            this.Text = "ControlGlass";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button button1;
        public System.Windows.Forms.Button button2;
        public System.Windows.Forms.RadioButton radioButton1;
        public System.Windows.Forms.TrackBar trackBar1;
        public System.Windows.Forms.Panel playBack1Control;
        public System.Windows.Forms.Panel grandMasterControl;
        public System.Windows.Forms.RadioButton grandMasterHold;
    }
}

