namespace TestApp
{
    partial class FormClient
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
            this.buttonConnect = new System.Windows.Forms.Button();
            this.buttonDisconnect = new System.Windows.Forms.Button();
            this.buttonSend = new System.Windows.Forms.Button();
            this.textBoxSend = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxReceived = new System.Windows.Forms.TextBox();
            this.buttonSendLog = new System.Windows.Forms.Button();
            this.buttonSendLong2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(3, 12);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(99, 23);
            this.buttonConnect.TabIndex = 0;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.Enabled = false;
            this.buttonDisconnect.Location = new System.Drawing.Point(3, 41);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(99, 23);
            this.buttonDisconnect.TabIndex = 1;
            this.buttonDisconnect.Text = "Disconnect";
            this.buttonDisconnect.UseVisualStyleBackColor = true;
            this.buttonDisconnect.Click += new System.EventHandler(this.buttonDisconnect_Click);
            // 
            // buttonSend
            // 
            this.buttonSend.Enabled = false;
            this.buttonSend.Location = new System.Drawing.Point(615, 13);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(75, 23);
            this.buttonSend.TabIndex = 2;
            this.buttonSend.Text = "Send";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // textBoxSend
            // 
            this.textBoxSend.Location = new System.Drawing.Point(202, 15);
            this.textBoxSend.Multiline = true;
            this.textBoxSend.Name = "textBoxSend";
            this.textBoxSend.Size = new System.Drawing.Size(407, 206);
            this.textBoxSend.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(108, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Message to send";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(104, 229);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Message received";
            // 
            // textBoxReceived
            // 
            this.textBoxReceived.Location = new System.Drawing.Point(202, 227);
            this.textBoxReceived.Multiline = true;
            this.textBoxReceived.Name = "textBoxReceived";
            this.textBoxReceived.ReadOnly = true;
            this.textBoxReceived.Size = new System.Drawing.Size(407, 206);
            this.textBoxReceived.TabIndex = 7;
            // 
            // buttonSendLog
            // 
            this.buttonSendLog.Enabled = false;
            this.buttonSendLog.Location = new System.Drawing.Point(3, 70);
            this.buttonSendLog.Name = "buttonSendLog";
            this.buttonSendLog.Size = new System.Drawing.Size(99, 23);
            this.buttonSendLog.TabIndex = 9;
            this.buttonSendLog.Text = "Send long msg";
            this.buttonSendLog.UseVisualStyleBackColor = true;
            this.buttonSendLog.Click += new System.EventHandler(this.buttonSendLog_Click);
            // 
            // buttonSendLong2
            // 
            this.buttonSendLong2.Enabled = false;
            this.buttonSendLong2.Location = new System.Drawing.Point(615, 41);
            this.buttonSendLong2.Name = "buttonSendLong2";
            this.buttonSendLong2.Size = new System.Drawing.Size(125, 23);
            this.buttonSendLong2.TabIndex = 10;
            this.buttonSendLong2.Text = "Send long msg + text";
            this.buttonSendLong2.UseVisualStyleBackColor = true;
            this.buttonSendLong2.Click += new System.EventHandler(this.buttonSendLong2_Click);
            // 
            // FormClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 449);
            this.Controls.Add(this.buttonSendLong2);
            this.Controls.Add(this.buttonSendLog);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxReceived);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxSend);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.buttonDisconnect);
            this.Controls.Add(this.buttonConnect);
            this.Name = "FormClient";
            this.Text = "FormClient";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Button buttonDisconnect;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.TextBox textBoxSend;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxReceived;
        private System.Windows.Forms.Button buttonSendLog;
        private System.Windows.Forms.Button buttonSendLong2;
    }
}