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
            this.predictedWordLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // drawPanel
            // 
            this.drawPanel.BackColor = System.Drawing.SystemColors.Window;
            this.drawPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.drawPanel.Location = new System.Drawing.Point(166, 38);
            this.drawPanel.Name = "drawPanel";
            this.drawPanel.Size = new System.Drawing.Size(1398, 321);
            this.drawPanel.TabIndex = 0;
            this.drawPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.drawPanel_Paint);
            this.drawPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.drawPanel_MouseDown);
            this.drawPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.drawPanel_MouseMove);
            this.drawPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.drawPanel_MouseUp);
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(12, 336);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(148, 23);
            this.clearButton.TabIndex = 12;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // testConnectedComponentsWindowButton
            // 
            this.testConnectedComponentsWindowButton.Location = new System.Drawing.Point(12, 282);
            this.testConnectedComponentsWindowButton.Name = "testConnectedComponentsWindowButton";
            this.testConnectedComponentsWindowButton.Size = new System.Drawing.Size(143, 35);
            this.testConnectedComponentsWindowButton.TabIndex = 13;
            this.testConnectedComponentsWindowButton.Text = "Display Connected Components";
            this.testConnectedComponentsWindowButton.UseVisualStyleBackColor = true;
            this.testConnectedComponentsWindowButton.Click += new System.EventHandler(this.testConnectedComponentsWindowButton_Click);
            // 
            // sendBytesToPythonDebugButton
            // 
            this.sendBytesToPythonDebugButton.Location = new System.Drawing.Point(13, 232);
            this.sendBytesToPythonDebugButton.Name = "sendBytesToPythonDebugButton";
            this.sendBytesToPythonDebugButton.Size = new System.Drawing.Size(142, 35);
            this.sendBytesToPythonDebugButton.TabIndex = 14;
            this.sendBytesToPythonDebugButton.Text = "Send Bytes To Python";
            this.sendBytesToPythonDebugButton.UseVisualStyleBackColor = true;
            this.sendBytesToPythonDebugButton.Click += new System.EventHandler(this.sendBytesToPythonDebugButton_Click);
            // 
            // startPythonClientButton
            // 
            this.startPythonClientButton.Location = new System.Drawing.Point(13, 181);
            this.startPythonClientButton.Name = "startPythonClientButton";
            this.startPythonClientButton.Size = new System.Drawing.Size(142, 36);
            this.startPythonClientButton.TabIndex = 15;
            this.startPythonClientButton.Text = "Start Python Client";
            this.startPythonClientButton.UseVisualStyleBackColor = true;
            this.startPythonClientButton.Click += new System.EventHandler(this.startPythonClientButton_Click);
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.ForeColor = System.Drawing.Color.Red;
            this.statusLabel.Location = new System.Drawing.Point(1364, 362);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(94, 13);
            this.statusLabel.TabIndex = 16;
            this.statusLabel.Text = "Status: Not Ready";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(163, 369);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Messages:";
            // 
            // lastMessageLabel
            // 
            this.lastMessageLabel.AutoSize = true;
            this.lastMessageLabel.Location = new System.Drawing.Point(232, 369);
            this.lastMessageLabel.Name = "lastMessageLabel";
            this.lastMessageLabel.Size = new System.Drawing.Size(10, 13);
            this.lastMessageLabel.TabIndex = 18;
            this.lastMessageLabel.Text = " ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(163, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Predicted word: ";
            // 
            // predictedWordLabel
            // 
            this.predictedWordLabel.AutoSize = true;
            this.predictedWordLabel.Location = new System.Drawing.Point(260, 12);
            this.predictedWordLabel.Name = "predictedWordLabel";
            this.predictedWordLabel.Size = new System.Drawing.Size(0, 13);
            this.predictedWordLabel.TabIndex = 20;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1576, 391);
            this.Controls.Add(this.predictedWordLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lastMessageLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.startPythonClientButton);
            this.Controls.Add(this.sendBytesToPythonDebugButton);
            this.Controls.Add(this.testConnectedComponentsWindowButton);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.drawPanel);
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
        private System.Windows.Forms.Label predictedWordLabel;
    }
}

