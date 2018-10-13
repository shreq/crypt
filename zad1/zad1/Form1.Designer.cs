namespace zad1
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
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.helpB = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.keyTB = new System.Windows.Forms.TextBox();
            this.ketL = new System.Windows.Forms.Label();
            this.filepathTB = new System.Windows.Forms.TextBox();
            this.filepathL = new System.Windows.Forms.Label();
            this.filepathB = new System.Windows.Forms.Button();
            this.encryptB = new System.Windows.Forms.Button();
            this.decryptB = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(95, 262);
            this.linkLabel1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(284, 13);
            this.linkLabel1.TabIndex = 0;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Click here to continue learning how to build a desktop app!";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel1_LinkClicked);
            // 
            // helpB
            // 
            this.helpB.Location = new System.Drawing.Point(395, 258);
            this.helpB.Margin = new System.Windows.Forms.Padding(2);
            this.helpB.Name = "helpB";
            this.helpB.Size = new System.Drawing.Size(70, 20);
            this.helpB.TabIndex = 2;
            this.helpB.Text = "help";
            this.helpB.UseVisualStyleBackColor = true;
            this.helpB.Click += new System.EventHandler(this.HelpB_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 148);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 174);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "label2";
            // 
            // keyTB
            // 
            this.keyTB.Location = new System.Drawing.Point(68, 12);
            this.keyTB.Name = "keyTB";
            this.keyTB.Size = new System.Drawing.Size(321, 20);
            this.keyTB.TabIndex = 6;
            this.keyTB.Text = "00010011 00110100 01010111 01111001 10011011 10111100 11011111 11110001";
            this.keyTB.TextChanged += new System.EventHandler(this.KeyTB_TextChanged);
            // 
            // ketL
            // 
            this.ketL.AutoSize = true;
            this.ketL.Location = new System.Drawing.Point(12, 15);
            this.ketL.Name = "ketL";
            this.ketL.Size = new System.Drawing.Size(28, 13);
            this.ketL.TabIndex = 7;
            this.ketL.Text = "Key:";
            // 
            // filepathTB
            // 
            this.filepathTB.Location = new System.Drawing.Point(68, 38);
            this.filepathTB.Name = "filepathTB";
            this.filepathTB.Size = new System.Drawing.Size(321, 20);
            this.filepathTB.TabIndex = 8;
            this.filepathTB.Text = "./../../../image.jpg";
            // 
            // filepathL
            // 
            this.filepathL.AutoSize = true;
            this.filepathL.Location = new System.Drawing.Point(12, 42);
            this.filepathL.Name = "filepathL";
            this.filepathL.Size = new System.Drawing.Size(50, 13);
            this.filepathL.TabIndex = 9;
            this.filepathL.Text = "File path:";
            // 
            // filepathB
            // 
            this.filepathB.Location = new System.Drawing.Point(395, 38);
            this.filepathB.Name = "filepathB";
            this.filepathB.Size = new System.Drawing.Size(69, 20);
            this.filepathB.TabIndex = 10;
            this.filepathB.Text = "load";
            this.filepathB.UseVisualStyleBackColor = true;
            this.filepathB.Click += new System.EventHandler(this.FilepathB_Click);
            // 
            // encryptB
            // 
            this.encryptB.Location = new System.Drawing.Point(12, 81);
            this.encryptB.Name = "encryptB";
            this.encryptB.Size = new System.Drawing.Size(223, 41);
            this.encryptB.TabIndex = 11;
            this.encryptB.Text = "Encrypt";
            this.encryptB.UseVisualStyleBackColor = true;
            this.encryptB.Click += new System.EventHandler(this.EncryptB_Click);
            // 
            // decryptB
            // 
            this.decryptB.Location = new System.Drawing.Point(241, 81);
            this.decryptB.Name = "decryptB";
            this.decryptB.Size = new System.Drawing.Size(223, 41);
            this.decryptB.TabIndex = 12;
            this.decryptB.Text = "Decrypt";
            this.decryptB.UseVisualStyleBackColor = true;
            this.decryptB.Click += new System.EventHandler(this.DecryptB_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 289);
            this.Controls.Add(this.decryptB);
            this.Controls.Add(this.encryptB);
            this.Controls.Add(this.filepathB);
            this.Controls.Add(this.filepathL);
            this.Controls.Add(this.filepathTB);
            this.Controls.Add(this.ketL);
            this.Controls.Add(this.keyTB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.helpB);
            this.Controls.Add(this.linkLabel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "DESXXX";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;

        private System.Windows.Forms.Label ketL;
        private System.Windows.Forms.TextBox keyTB;
        private System.Windows.Forms.Label filepathL;
        private System.Windows.Forms.TextBox filepathTB;
        private System.Windows.Forms.Button filepathB;
        private System.Windows.Forms.Button encryptB;
        private System.Windows.Forms.Button helpB;
        private System.Windows.Forms.Button decryptB;
    }
}
