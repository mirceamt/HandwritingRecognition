namespace HandwritingRecognition
{
    partial class ConnectedComponentsWindow
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
            this.previousButton = new System.Windows.Forms.Button();
            this.nextButton = new System.Windows.Forms.Button();
            this.connectedComponentPanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // previousButton
            // 
            this.previousButton.Location = new System.Drawing.Point(567, 1003);
            this.previousButton.Margin = new System.Windows.Forms.Padding(6);
            this.previousButton.Name = "previousButton";
            this.previousButton.Size = new System.Drawing.Size(150, 44);
            this.previousButton.TabIndex = 0;
            this.previousButton.Text = "Previous";
            this.previousButton.UseVisualStyleBackColor = true;
            this.previousButton.Click += new System.EventHandler(this.previousButton_Click);
            // 
            // nextButton
            // 
            this.nextButton.Location = new System.Drawing.Point(852, 1012);
            this.nextButton.Margin = new System.Windows.Forms.Padding(6);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(150, 44);
            this.nextButton.TabIndex = 1;
            this.nextButton.Text = "Next";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // connectedComponentPanel
            // 
            this.connectedComponentPanel.Location = new System.Drawing.Point(24, 23);
            this.connectedComponentPanel.Margin = new System.Windows.Forms.Padding(6);
            this.connectedComponentPanel.Name = "connectedComponentPanel";
            this.connectedComponentPanel.Size = new System.Drawing.Size(624, 494);
            this.connectedComponentPanel.TabIndex = 2;
            this.connectedComponentPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.connectedComponentPanel_Paint);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(24, 526);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(624, 468);
            this.panel1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(882, 23);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(624, 494);
            this.panel2.TabIndex = 4;
            // 
            // panel3
            // 
            this.panel3.Location = new System.Drawing.Point(1710, 59);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(255, 226);
            this.panel3.TabIndex = 5;
            // 
            // panel4
            // 
            this.panel4.Location = new System.Drawing.Point(1710, 375);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(255, 255);
            this.panel4.TabIndex = 6;
            // 
            // panel5
            // 
            this.panel5.Location = new System.Drawing.Point(1710, 720);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(255, 259);
            this.panel5.TabIndex = 7;
            // 
            // ConnectedComponentsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(2134, 1184);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.connectedComponentPanel);
            this.Controls.Add(this.nextButton);
            this.Controls.Add(this.previousButton);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "ConnectedComponentsWindow";
            this.Text = "connectedComponentsWindow";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button previousButton;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Panel connectedComponentPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
    }
}