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
            this.button1 = new System.Windows.Forms.Button();
            this.helloWorldLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.keyTB = new System.Windows.Forms.TextBox();
            this.ketL = new System.Windows.Forms.Label();
            this.filepathTB = new System.Windows.Forms.TextBox();
            this.filepathL = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(123, 270);
            this.linkLabel1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(284, 13);
            this.linkLabel1.TabIndex = 0;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Click here to continue learning how to build a desktop app!";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel1_LinkClicked);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(218, 240);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 28);
            this.button1.TabIndex = 2;
            this.button1.Text = "Click Me!";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // helloWorldLabel
            // 
            this.helloWorldLabel.AutoSize = true;
            this.helloWorldLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.helloWorldLabel.Location = new System.Drawing.Point(231, 9);
            this.helloWorldLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.helloWorldLabel.Name = "helloWorldLabel";
            this.helloWorldLabel.Size = new System.Drawing.Size(64, 26);
            this.helloWorldLabel.TabIndex = 3;
            this.helloWorldLabel.Text = "Yeet!";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(76, 190);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(76, 203);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "label2";
            // 
            // keyTB
            // 
            this.keyTB.Location = new System.Drawing.Point(132, 62);
            this.keyTB.Name = "keyTB";
            this.keyTB.Size = new System.Drawing.Size(321, 20);
            this.keyTB.TabIndex = 6;
            this.keyTB.TextChanged += new System.EventHandler(this.KeyTB_TextChanged);
            // 
            // ketL
            // 
            this.ketL.AutoSize = true;
            this.ketL.Location = new System.Drawing.Point(76, 65);
            this.ketL.Name = "ketL";
            this.ketL.Size = new System.Drawing.Size(28, 13);
            this.ketL.TabIndex = 7;
            this.ketL.Text = "Key:";
            // 
            // filepathTB
            // 
            this.filepathTB.Location = new System.Drawing.Point(132, 88);
            this.filepathTB.Name = "filepathTB";
            this.filepathTB.Size = new System.Drawing.Size(321, 20);
            this.filepathTB.TabIndex = 8;
            this.filepathTB.TextChanged += new System.EventHandler(this.FilepathTB_TextChanged);
            // 
            // filepathL
            // 
            this.filepathL.AutoSize = true;
            this.filepathL.Location = new System.Drawing.Point(76, 91);
            this.filepathL.Name = "filepathL";
            this.filepathL.Size = new System.Drawing.Size(50, 13);
            this.filepathL.TabIndex = 9;
            this.filepathL.Text = "File path:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 292);
            this.Controls.Add(this.filepathL);
            this.Controls.Add(this.filepathTB);
            this.Controls.Add(this.ketL);
            this.Controls.Add(this.keyTB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.helloWorldLabel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.linkLabel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label helloWorldLabel;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox keyTB;
        public System.Windows.Forms.Label ketL;
        public System.Windows.Forms.TextBox filepathTB;
        public System.Windows.Forms.Label filepathL;
    }
}

