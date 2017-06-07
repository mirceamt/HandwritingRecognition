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
            this.SuspendLayout();
            // 
            // drawPanel
            // 
            this.drawPanel.BackColor = System.Drawing.SystemColors.Window;
            this.drawPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.drawPanel.Location = new System.Drawing.Point(165, 128);
            this.drawPanel.Name = "drawPanel";
            this.drawPanel.Size = new System.Drawing.Size(1292, 321);
            this.drawPanel.TabIndex = 0;
            this.drawPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.drawPanel_Paint);
            this.drawPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.drawPanel_MouseDown);
            this.drawPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.drawPanel_MouseMove);
            this.drawPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.drawPanel_MouseUp);
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(11, 426);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(296, 44);
            this.clearButton.TabIndex = 12;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // testConnectedComponentsWindowButton
            // 
            this.testConnectedComponentsWindowButton.Location = new System.Drawing.Point(11, 372);
            this.testConnectedComponentsWindowButton.Name = "testConnectedComponentsWindowButton";
            this.testConnectedComponentsWindowButton.Size = new System.Drawing.Size(286, 67);
            this.testConnectedComponentsWindowButton.TabIndex = 13;
            this.testConnectedComponentsWindowButton.Text = "Display Connected Components";
            this.testConnectedComponentsWindowButton.UseVisualStyleBackColor = true;
            this.testConnectedComponentsWindowButton.Click += new System.EventHandler(this.testConnectedComponentsWindowButton_Click);
            // 
            // sendBytesToPythonDebugButton
            // 
            this.sendBytesToPythonDebugButton.Location = new System.Drawing.Point(12, 322);
            this.sendBytesToPythonDebugButton.Name = "sendBytesToPythonDebugButton";
            this.sendBytesToPythonDebugButton.Size = new System.Drawing.Size(284, 67);
            this.sendBytesToPythonDebugButton.TabIndex = 14;
            this.sendBytesToPythonDebugButton.Text = "Send Bytes To Python";
            this.sendBytesToPythonDebugButton.UseVisualStyleBackColor = true;
            this.sendBytesToPythonDebugButton.Click += new System.EventHandler(this.sendBytesToPythonDebugButton_Click);
            // 
            // startPythonClientButton
            // 
            this.startPythonClientButton.Location = new System.Drawing.Point(12, 271);
            this.startPythonClientButton.Name = "startPythonClientButton";
            this.startPythonClientButton.Size = new System.Drawing.Size(284, 69);
            this.startPythonClientButton.TabIndex = 15;
            this.startPythonClientButton.Text = "Start Python Client";
            this.startPythonClientButton.UseVisualStyleBackColor = true;
            this.startPythonClientButton.Click += new System.EventHandler(this.startPythonClientButton_Click);
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.ForeColor = System.Drawing.Color.Red;
            this.statusLabel.Location = new System.Drawing.Point(1363, 452);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(186, 25);
            this.statusLabel.TabIndex = 16;
            this.statusLabel.Text = "Status: Not Ready";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(162, 452);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 26);
            this.label1.TabIndex = 17;
            this.label1.Text = "Messages:";
            // 
            // lastMessageLabel
            // 
            this.lastMessageLabel.AutoSize = true;
            this.lastMessageLabel.Location = new System.Drawing.Point(231, 452);
            this.lastMessageLabel.Name = "lastMessageLabel";
            this.lastMessageLabel.Size = new System.Drawing.Size(18, 25);
            this.lastMessageLabel.TabIndex = 18;
            this.lastMessageLabel.Text = " ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(162, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Predicted words: ";
            // 
            // collectNewDataButton
            // 
            this.collectNewDataButton.Location = new System.Drawing.Point(12, 221);
            this.collectNewDataButton.Name = "collectNewDataButton";
            this.collectNewDataButton.Size = new System.Drawing.Size(284, 75);
            this.collectNewDataButton.TabIndex = 21;
            this.collectNewDataButton.Text = "Collect New Data";
            this.collectNewDataButton.UseVisualStyleBackColor = true;
            this.collectNewDataButton.Click += new System.EventHandler(this.collectNewDataButton_Click);
            // 
            // predictedWordsTextBox
            // 
            this.predictedWordsTextBox.Location = new System.Drawing.Point(268, 6);
            this.predictedWordsTextBox.Name = "predictedWordsTextBox";
            this.predictedWordsTextBox.Size = new System.Drawing.Size(1189, 20);
            this.predictedWordsTextBox.TabIndex = 22;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1469, 473);
            this.Controls.Add(this.predictedWordsTextBox);
            this.Controls.Add(this.collectNewDataButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lastMessageLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.startPythonClientButton);
            this.Controls.Add(this.sendBytesToPythonDebugButton);
            this.Controls.Add(this.testConnectedComponentsWindowButton);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.drawPanel);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "Form1";
            this.Text = "Recunoasterea Textului Scris";
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
    }
}

