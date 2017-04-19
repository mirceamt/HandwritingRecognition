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
            this.SuspendLayout();
            // 
            // previousButton
            // 
            this.previousButton.Location = new System.Drawing.Point(267, 486);
            this.previousButton.Name = "previousButton";
            this.previousButton.Size = new System.Drawing.Size(75, 23);
            this.previousButton.TabIndex = 0;
            this.previousButton.Text = "Previous";
            this.previousButton.UseVisualStyleBackColor = true;
            this.previousButton.Click += new System.EventHandler(this.previousButton_Click);
            // 
            // nextButton
            // 
            this.nextButton.Location = new System.Drawing.Point(417, 486);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(75, 23);
            this.nextButton.TabIndex = 1;
            this.nextButton.Text = "Next";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // connectedComponentPanel
            // 
            this.connectedComponentPanel.Location = new System.Drawing.Point(12, 12);
            this.connectedComponentPanel.Name = "connectedComponentPanel";
            this.connectedComponentPanel.Size = new System.Drawing.Size(785, 441);
            this.connectedComponentPanel.TabIndex = 2;
            this.connectedComponentPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.connectedComponentPanel_Paint);
            // 
            // ConnectedComponentsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 552);
            this.Controls.Add(this.connectedComponentPanel);
            this.Controls.Add(this.nextButton);
            this.Controls.Add(this.previousButton);
            this.Name = "ConnectedComponentsWindow";
            this.Text = "connectedComponentsWindow";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button previousButton;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Panel connectedComponentPanel;
    }
}