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
            this.x1TextBox = new System.Windows.Forms.TextBox();
            this.y1TextBox = new System.Windows.Forms.TextBox();
            this.x2TextBox = new System.Windows.Forms.TextBox();
            this.y2TextBox = new System.Windows.Forms.TextBox();
            this.drawLineButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.clearButton = new System.Windows.Forms.Button();
            this.testConnectedComponentsWindowButton = new System.Windows.Forms.Button();
            this.sendBytesToPythonDebugButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // drawPanel
            // 
            this.drawPanel.BackColor = System.Drawing.SystemColors.Window;
            this.drawPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.drawPanel.Location = new System.Drawing.Point(166, 12);
            this.drawPanel.Name = "drawPanel";
            this.drawPanel.Size = new System.Drawing.Size(706, 321);
            this.drawPanel.TabIndex = 0;
            this.drawPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.drawPanel_Paint);
            this.drawPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.drawPanel_MouseDown);
            this.drawPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.drawPanel_MouseMove);
            this.drawPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.drawPanel_MouseUp);
            // 
            // x1TextBox
            // 
            this.x1TextBox.Location = new System.Drawing.Point(210, 339);
            this.x1TextBox.Name = "x1TextBox";
            this.x1TextBox.Size = new System.Drawing.Size(100, 20);
            this.x1TextBox.TabIndex = 1;
            // 
            // y1TextBox
            // 
            this.y1TextBox.Location = new System.Drawing.Point(210, 366);
            this.y1TextBox.Name = "y1TextBox";
            this.y1TextBox.Size = new System.Drawing.Size(100, 20);
            this.y1TextBox.TabIndex = 2;
            // 
            // x2TextBox
            // 
            this.x2TextBox.Location = new System.Drawing.Point(210, 392);
            this.x2TextBox.Name = "x2TextBox";
            this.x2TextBox.Size = new System.Drawing.Size(100, 20);
            this.x2TextBox.TabIndex = 3;
            // 
            // y2TextBox
            // 
            this.y2TextBox.Location = new System.Drawing.Point(210, 418);
            this.y2TextBox.Name = "y2TextBox";
            this.y2TextBox.Size = new System.Drawing.Size(100, 20);
            this.y2TextBox.TabIndex = 4;
            // 
            // drawLineButton
            // 
            this.drawLineButton.Location = new System.Drawing.Point(210, 457);
            this.drawLineButton.Name = "drawLineButton";
            this.drawLineButton.Size = new System.Drawing.Size(75, 23);
            this.drawLineButton.TabIndex = 5;
            this.drawLineButton.Text = "Draw Line";
            this.drawLineButton.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(163, 373);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "y1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(163, 346);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "x1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(163, 399);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "x2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(163, 425);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(18, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "y2";
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(12, 310);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(148, 23);
            this.clearButton.TabIndex = 12;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // testConnectedComponentsWindowButton
            // 
            this.testConnectedComponentsWindowButton.Location = new System.Drawing.Point(12, 256);
            this.testConnectedComponentsWindowButton.Name = "testConnectedComponentsWindowButton";
            this.testConnectedComponentsWindowButton.Size = new System.Drawing.Size(143, 35);
            this.testConnectedComponentsWindowButton.TabIndex = 13;
            this.testConnectedComponentsWindowButton.Text = "Display Connected Components";
            this.testConnectedComponentsWindowButton.UseVisualStyleBackColor = true;
            this.testConnectedComponentsWindowButton.Click += new System.EventHandler(this.testConnectedComponentsWindowButton_Click);
            // 
            // sendBytesToPythonDebugButton
            // 
            this.sendBytesToPythonDebugButton.Location = new System.Drawing.Point(13, 206);
            this.sendBytesToPythonDebugButton.Name = "sendBytesToPythonDebugButton";
            this.sendBytesToPythonDebugButton.Size = new System.Drawing.Size(142, 35);
            this.sendBytesToPythonDebugButton.TabIndex = 14;
            this.sendBytesToPythonDebugButton.Text = "Send Bytes To Python";
            this.sendBytesToPythonDebugButton.UseVisualStyleBackColor = true;
            this.sendBytesToPythonDebugButton.Click += new System.EventHandler(this.sendBytesToPythonDebugButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.sendBytesToPythonDebugButton);
            this.Controls.Add(this.testConnectedComponentsWindowButton);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.drawLineButton);
            this.Controls.Add(this.y2TextBox);
            this.Controls.Add(this.x2TextBox);
            this.Controls.Add(this.y1TextBox);
            this.Controls.Add(this.x1TextBox);
            this.Controls.Add(this.drawPanel);
            this.Name = "Form1";
            this.Text = "Recunoasterea Textului Scris";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel drawPanel;
        private System.Windows.Forms.TextBox x1TextBox;
        private System.Windows.Forms.TextBox y1TextBox;
        private System.Windows.Forms.TextBox x2TextBox;
        private System.Windows.Forms.TextBox y2TextBox;
        private System.Windows.Forms.Button drawLineButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Button testConnectedComponentsWindowButton;
        private System.Windows.Forms.Button sendBytesToPythonDebugButton;
    }
}

