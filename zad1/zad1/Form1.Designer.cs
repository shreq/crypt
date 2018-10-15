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
            this.helpB = new System.Windows.Forms.Button();
            this.keyTB = new System.Windows.Forms.TextBox();
            this.ketL = new System.Windows.Forms.Label();
            this.filepathTB = new System.Windows.Forms.TextBox();
            this.filepathL = new System.Windows.Forms.Label();
            this.filepathB = new System.Windows.Forms.Button();
            this.encryptB = new System.Windows.Forms.Button();
            this.decryptB = new System.Windows.Forms.Button();
            this.textinTB = new System.Windows.Forms.TextBox();
            this.textoutTB = new System.Windows.Forms.TextBox();
            this.textL = new System.Windows.Forms.Label();
            this.textB = new System.Windows.Forms.Button();
            this.keyx1TB = new System.Windows.Forms.TextBox();
            this.keyx2TB = new System.Windows.Forms.TextBox();
            this.keyx1L = new System.Windows.Forms.Label();
            this.keyx2L = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // helpB
            // 
            this.helpB.Location = new System.Drawing.Point(394, 236);
            this.helpB.Margin = new System.Windows.Forms.Padding(2);
            this.helpB.Name = "helpB";
            this.helpB.Size = new System.Drawing.Size(70, 20);
            this.helpB.TabIndex = 2;
            this.helpB.Text = "help";
            this.helpB.UseVisualStyleBackColor = true;
            this.helpB.Click += new System.EventHandler(this.HelpB_Click);
            // 
            // keyTB
            // 
            this.keyTB.Location = new System.Drawing.Point(69, 64);
            this.keyTB.Name = "keyTB";
            this.keyTB.Size = new System.Drawing.Size(321, 20);
            this.keyTB.TabIndex = 6;
            this.keyTB.Text = "00010011 00110100 01010111 01111001 10011011 10111100 11011111 11110001";
            this.keyTB.TextChanged += new System.EventHandler(this.KeyTB_TextChanged);
            // 
            // ketL
            // 
            this.ketL.AutoSize = true;
            this.ketL.Location = new System.Drawing.Point(13, 67);
            this.ketL.Name = "ketL";
            this.ketL.Size = new System.Drawing.Size(28, 13);
            this.ketL.TabIndex = 7;
            this.ketL.Text = "Key:";
            // 
            // filepathTB
            // 
            this.filepathTB.Location = new System.Drawing.Point(69, 90);
            this.filepathTB.Name = "filepathTB";
            this.filepathTB.Size = new System.Drawing.Size(321, 20);
            this.filepathTB.TabIndex = 8;
            this.filepathTB.Text = "./../../../image.png";
            // 
            // filepathL
            // 
            this.filepathL.AutoSize = true;
            this.filepathL.Location = new System.Drawing.Point(13, 94);
            this.filepathL.Name = "filepathL";
            this.filepathL.Size = new System.Drawing.Size(50, 13);
            this.filepathL.TabIndex = 9;
            this.filepathL.Text = "File path:";
            // 
            // filepathB
            // 
            this.filepathB.Location = new System.Drawing.Point(396, 90);
            this.filepathB.Name = "filepathB";
            this.filepathB.Size = new System.Drawing.Size(70, 20);
            this.filepathB.TabIndex = 10;
            this.filepathB.Text = "load";
            this.filepathB.UseVisualStyleBackColor = true;
            this.filepathB.Click += new System.EventHandler(this.FilepathB_Click);
            // 
            // encryptB
            // 
            this.encryptB.Location = new System.Drawing.Point(12, 187);
            this.encryptB.Name = "encryptB";
            this.encryptB.Size = new System.Drawing.Size(223, 41);
            this.encryptB.TabIndex = 11;
            this.encryptB.Text = "Encrypt";
            this.encryptB.UseVisualStyleBackColor = true;
            this.encryptB.Click += new System.EventHandler(this.EncryptB_Click);
            // 
            // decryptB
            // 
            this.decryptB.Location = new System.Drawing.Point(241, 187);
            this.decryptB.Name = "decryptB";
            this.decryptB.Size = new System.Drawing.Size(223, 41);
            this.decryptB.TabIndex = 12;
            this.decryptB.Text = "Decrypt";
            this.decryptB.UseVisualStyleBackColor = true;
            this.decryptB.Click += new System.EventHandler(this.DecryptB_Click);
            // 
            // textinTB
            // 
            this.textinTB.Location = new System.Drawing.Point(69, 116);
            this.textinTB.Name = "textinTB";
            this.textinTB.Size = new System.Drawing.Size(321, 20);
            this.textinTB.TabIndex = 13;
            // 
            // textoutTB
            // 
            this.textoutTB.Location = new System.Drawing.Point(69, 142);
            this.textoutTB.Name = "textoutTB";
            this.textoutTB.Size = new System.Drawing.Size(321, 20);
            this.textoutTB.TabIndex = 14;
            // 
            // textL
            // 
            this.textL.AutoSize = true;
            this.textL.Location = new System.Drawing.Point(14, 119);
            this.textL.Name = "textL";
            this.textL.Size = new System.Drawing.Size(31, 13);
            this.textL.TabIndex = 15;
            this.textL.Text = "Text:";
            // 
            // textB
            // 
            this.textB.Location = new System.Drawing.Point(396, 116);
            this.textB.Name = "textB";
            this.textB.Size = new System.Drawing.Size(70, 20);
            this.textB.TabIndex = 16;
            this.textB.Text = "load";
            this.textB.UseVisualStyleBackColor = true;
            this.textB.Click += new System.EventHandler(this.TextB_Click);
            // 
            // keyx1TB
            // 
            this.keyx1TB.Location = new System.Drawing.Point(69, 12);
            this.keyx1TB.Name = "keyx1TB";
            this.keyx1TB.Size = new System.Drawing.Size(321, 20);
            this.keyx1TB.TabIndex = 17;
            this.keyx1TB.TextChanged += new System.EventHandler(this.Keyx1TB_TextChanged);
            // 
            // keyx2TB
            // 
            this.keyx2TB.Location = new System.Drawing.Point(69, 38);
            this.keyx2TB.Name = "keyx2TB";
            this.keyx2TB.Size = new System.Drawing.Size(321, 20);
            this.keyx2TB.TabIndex = 18;
            this.keyx2TB.TextChanged += new System.EventHandler(this.Keyx2TB_TextChanged);
            // 
            // keyx1L
            // 
            this.keyx1L.AutoSize = true;
            this.keyx1L.Location = new System.Drawing.Point(14, 15);
            this.keyx1L.Name = "keyx1L";
            this.keyx1L.Size = new System.Drawing.Size(54, 13);
            this.keyx1L.TabIndex = 19;
            this.keyx1L.Text = "xor Key 1:";
            // 
            // keyx2L
            // 
            this.keyx2L.AutoSize = true;
            this.keyx2L.Location = new System.Drawing.Point(14, 41);
            this.keyx2L.Name = "keyx2L";
            this.keyx2L.Size = new System.Drawing.Size(54, 13);
            this.keyx2L.TabIndex = 20;
            this.keyx2L.Text = "xor Key 2:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 267);
            this.Controls.Add(this.keyx2L);
            this.Controls.Add(this.keyx1L);
            this.Controls.Add(this.keyx2TB);
            this.Controls.Add(this.keyx1TB);
            this.Controls.Add(this.textB);
            this.Controls.Add(this.textL);
            this.Controls.Add(this.textoutTB);
            this.Controls.Add(this.textinTB);
            this.Controls.Add(this.decryptB);
            this.Controls.Add(this.encryptB);
            this.Controls.Add(this.filepathB);
            this.Controls.Add(this.filepathL);
            this.Controls.Add(this.filepathTB);
            this.Controls.Add(this.ketL);
            this.Controls.Add(this.keyTB);
            this.Controls.Add(this.helpB);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "DESXXX";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ketL;
        private System.Windows.Forms.TextBox keyTB;
        private System.Windows.Forms.Label filepathL;
        private System.Windows.Forms.TextBox filepathTB;
        private System.Windows.Forms.Button filepathB;
        private System.Windows.Forms.Button encryptB;
        private System.Windows.Forms.Button helpB;
        private System.Windows.Forms.Button decryptB;
        private System.Windows.Forms.TextBox textinTB;
        private System.Windows.Forms.TextBox textoutTB;
        private System.Windows.Forms.Label textL;
        private System.Windows.Forms.Button textB;
        private System.Windows.Forms.TextBox keyx1TB;
        private System.Windows.Forms.TextBox keyx2TB;
        private System.Windows.Forms.Label keyx1L;
        private System.Windows.Forms.Label keyx2L;
    }
}
