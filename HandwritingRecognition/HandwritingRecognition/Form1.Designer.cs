namespace HandwritingRecognition
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
            this.drawPanel = new System.Windows.Forms.Panel();
            this.clearButton = new System.Windows.Forms.Button();
            this.testConnectedComponentsWindowButton = new System.Windows.Forms.Button();
            this.sendBytesToPythonDebugButton = new System.Windows.Forms.Button();
            this.startPythonClientButton = new System.Windows.Forms.Button();
            this.statusLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lastMessageLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.collectNewDataButton = new System.Windows.Forms.Button();
            this.predictedWordsTextBox = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // drawPanel
            // 
            this.drawPanel.BackColor = System.Drawing.SystemColors.Window;
            this.drawPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.drawPanel.Location = new System.Drawing.Point(335, 209);
            this.drawPanel.Name = "drawPanel";
            this.drawPanel.Size = new System.Drawing.Size(1920, 321);
            this.drawPanel.TabIndex = 0;
            this.drawPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.drawPanel_Paint);
            this.drawPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.drawPanel_MouseDown);
            this.drawPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.drawPanel_MouseMove);
            this.drawPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.drawPanel_MouseUp);
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(16, 277);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(284, 44);
            this.clearButton.TabIndex = 12;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // testConnectedComponentsWindowButton
            // 
            this.testConnectedComponentsWindowButton.Location = new System.Drawing.Point(16, 204);
            this.testConnectedComponentsWindowButton.Name = "testConnectedComponentsWindowButton";
            this.testConnectedComponentsWindowButton.Size = new System.Drawing.Size(284, 67);
            this.testConnectedComponentsWindowButton.TabIndex = 13;
            this.testConnectedComponentsWindowButton.Text = "Display Connected Components";
            this.testConnectedComponentsWindowButton.UseVisualStyleBackColor = true;
            this.testConnectedComponentsWindowButton.Click += new System.EventHandler(this.testConnectedComponentsWindowButton_Click);
            // 
            // sendBytesToPythonDebugButton
            // 
            this.sendBytesToPythonDebugButton.Location = new System.Drawing.Point(16, 150);
            this.sendBytesToPythonDebugButton.Name = "sendBytesToPythonDebugButton";
            this.sendBytesToPythonDebugButton.Size = new System.Drawing.Size(284, 48);
            this.sendBytesToPythonDebugButton.TabIndex = 14;
            this.sendBytesToPythonDebugButton.Text = "Send Bytes To Python";
            this.sendBytesToPythonDebugButton.UseVisualStyleBackColor = true;
            this.sendBytesToPythonDebugButton.Click += new System.EventHandler(this.sendBytesToPythonDebugButton_Click);
            // 
            // startPythonClientButton
            // 
            this.startPythonClientButton.Location = new System.Drawing.Point(16, 98);
            this.startPythonClientButton.Name = "startPythonClientButton";
            this.startPythonClientButton.Size = new System.Drawing.Size(284, 46);
            this.startPythonClientButton.TabIndex = 15;
            this.startPythonClientButton.Text = "Start Python Client";
            this.startPythonClientButton.UseVisualStyleBackColor = true;
            this.startPythonClientButton.Click += new System.EventHandler(this.startPythonClientButton_Click);
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.ForeColor = System.Drawing.Color.Red;
            this.statusLabel.Location = new System.Drawing.Point(2069, 549);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(186, 25);
            this.statusLabel.TabIndex = 16;
            this.statusLabel.Text = "Status: Not Ready";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(335, 548);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 26);
            this.label1.TabIndex = 17;
            this.label1.Text = "Messages:";
            // 
            // lastMessageLabel
            // 
            this.lastMessageLabel.AutoSize = true;
            this.lastMessageLabel.Location = new System.Drawing.Point(457, 549);
            this.lastMessageLabel.Name = "lastMessageLabel";
            this.lastMessageLabel.Size = new System.Drawing.Size(18, 25);
            this.lastMessageLabel.TabIndex = 18;
            this.lastMessageLabel.Text = " ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(132, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(197, 26);
            this.label2.TabIndex = 19;
            this.label2.Text = "Predicted words: ";
            // 
            // collectNewDataButton
            // 
            this.collectNewDataButton.Location = new System.Drawing.Point(16, 47);
            this.collectNewDataButton.Name = "collectNewDataButton";
            this.collectNewDataButton.Size = new System.Drawing.Size(284, 45);
            this.collectNewDataButton.TabIndex = 21;
            this.collectNewDataButton.Text = "Collect New Data";
            this.collectNewDataButton.UseVisualStyleBackColor = true;
            this.collectNewDataButton.Click += new System.EventHandler(this.collectNewDataButton_Click);
            // 
            // predictedWordsTextBox
            // 
            this.predictedWordsTextBox.Location = new System.Drawing.Point(335, 7);
            this.predictedWordsTextBox.Name = "predictedWordsTextBox";
            this.predictedWordsTextBox.Size = new System.Drawing.Size(1920, 31);
            this.predictedWordsTextBox.TabIndex = 22;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.collectNewDataButton);
            this.panel1.Controls.Add(this.clearButton);
            this.panel1.Controls.Add(this.testConnectedComponentsWindowButton);
            this.panel1.Controls.Add(this.sendBytesToPythonDebugButton);
            this.panel1.Controls.Add(this.startPythonClientButton);
            this.panel1.Location = new System.Drawing.Point(12, 144);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(317, 430);
            this.panel1.TabIndex = 23;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(2268, 583);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.predictedWordsTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lastMessageLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.drawPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.Text = "Recunoasterea Textului Scris";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel drawPanel;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Button testConnectedComponentsWindowButton;
        private System.Windows.Forms.Button sendBytesToPythonDebugButton;
        private System.Windows.Forms.Button startPythonClientButton;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lastMessageLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button collectNewDataButton;
        private System.Windows.Forms.TextBox predictedWordsTextBox;
        private System.Windows.Forms.Panel panel1;
    }
}

