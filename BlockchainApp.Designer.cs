using System;

namespace BlockchainAssignment
{
    partial class BlockchainApp
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.txtPubKey = new System.Windows.Forms.TextBox();
            this.txtPrivKey = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.txtAmnt = new System.Windows.Forms.TextBox();
            this.txtFee = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtRKey = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.cmbo_MineMethod = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button11 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.aPow = new System.Windows.Forms.RichTextBox();
            this.txtMineTime = new System.Windows.Forms.TextBox();
            this.button15 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.InfoText;
            this.richTextBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.richTextBox1.Location = new System.Drawing.Point(12, 12);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(403, 298);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(468, 28);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(54, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Print";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(431, 28);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(29, 20);
            this.textBox1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(431, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Enter Block Index";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(291, 313);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(110, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Create Wallet";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // txtPubKey
            // 
            this.txtPubKey.Location = new System.Drawing.Point(91, 316);
            this.txtPubKey.Name = "txtPubKey";
            this.txtPubKey.Size = new System.Drawing.Size(194, 20);
            this.txtPubKey.TabIndex = 5;
            // 
            // txtPrivKey
            // 
            this.txtPrivKey.Location = new System.Drawing.Point(91, 346);
            this.txtPrivKey.Name = "txtPrivKey";
            this.txtPrivKey.Size = new System.Drawing.Size(194, 20);
            this.txtPrivKey.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 319);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Public Key";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 346);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Private Key";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(291, 344);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(110, 23);
            this.button3.TabIndex = 10;
            this.button3.Text = "Validate Keys";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(609, 154);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(89, 34);
            this.button4.TabIndex = 11;
            this.button4.Text = "Create Transaction";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // txtAmnt
            // 
            this.txtAmnt.Location = new System.Drawing.Point(503, 180);
            this.txtAmnt.Name = "txtAmnt";
            this.txtAmnt.Size = new System.Drawing.Size(100, 20);
            this.txtAmnt.TabIndex = 12;
            // 
            // txtFee
            // 
            this.txtFee.Location = new System.Drawing.Point(503, 206);
            this.txtFee.Name = "txtFee";
            this.txtFee.Size = new System.Drawing.Size(100, 20);
            this.txtFee.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(454, 183);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Amount";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(472, 208);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Fee";
            // 
            // txtRKey
            // 
            this.txtRKey.Location = new System.Drawing.Point(503, 154);
            this.txtRKey.Name = "txtRKey";
            this.txtRKey.Size = new System.Drawing.Size(100, 20);
            this.txtRKey.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(426, 154);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Reciever Key";
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(528, 28);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 19;
            this.button6.Text = "Read All";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(609, 190);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(89, 36);
            this.button7.TabIndex = 20;
            this.button7.Text = "Read Pending Transactions";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(291, 373);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(110, 23);
            this.button9.TabIndex = 22;
            this.button9.Text = "Check Balance";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(434, 108);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(112, 36);
            this.button10.TabIndex = 23;
            this.button10.Text = "Full Blockchain Validation";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(431, 62);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(112, 40);
            this.button8.TabIndex = 24;
            this.button8.Text = "Generate New Block (Multi-Thread)";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // cmbo_MineMethod
            // 
            this.cmbo_MineMethod.AllowDrop = true;
            this.cmbo_MineMethod.FormattingEnabled = true;
            this.cmbo_MineMethod.Items.AddRange(new object[] {
            "Greedy",
            "Unpredictable",
            "Altruistic",
            "Personal"});
            this.cmbo_MineMethod.Location = new System.Drawing.Point(552, 70);
            this.cmbo_MineMethod.Name = "cmbo_MineMethod";
            this.cmbo_MineMethod.Size = new System.Drawing.Size(76, 21);
            this.cmbo_MineMethod.TabIndex = 25;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(551, 54);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 13);
            this.label7.TabIndex = 26;
            this.label7.Text = "Mining Method";
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(528, 309);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(89, 50);
            this.button11.TabIndex = 27;
            this.button11.Text = "Enable Autominer";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(528, 256);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(89, 44);
            this.button13.TabIndex = 29;
            this.button13.Text = "Enable aPoW";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // aPow
            // 
            this.aPow.BackColor = System.Drawing.SystemColors.HighlightText;
            this.aPow.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.aPow.Location = new System.Drawing.Point(422, 256);
            this.aPow.Name = "aPow";
            this.aPow.Size = new System.Drawing.Size(100, 142);
            this.aPow.TabIndex = 31;
            this.aPow.Text = "APoW:";
            // 
            // txtMineTime
            // 
            this.txtMineTime.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.txtMineTime.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMineTime.Location = new System.Drawing.Point(704, 0);
            this.txtMineTime.Name = "txtMineTime";
            this.txtMineTime.Size = new System.Drawing.Size(10, 13);
            this.txtMineTime.TabIndex = 32;
            this.txtMineTime.TextChanged += new System.EventHandler(this.txtMineTime_TextChanged);
            // 
            // button15
            // 
            this.button15.Location = new System.Drawing.Point(638, 62);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(76, 73);
            this.button15.TabIndex = 34;
            this.button15.Text = "Generate Random Transactions";
            this.button15.UseVisualStyleBackColor = true;
            this.button15.Click += new System.EventHandler(this.button15_Click);
            // 
            // BlockchainApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(715, 402);
            this.Controls.Add(this.button15);
            this.Controls.Add(this.txtMineTime);
            this.Controls.Add(this.aPow);
            this.Controls.Add(this.button13);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmbo_MineMethod);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtRKey);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtFee);
            this.Controls.Add(this.txtAmnt);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPrivKey);
            this.Controls.Add(this.txtPubKey);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.richTextBox1);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "BlockchainApp";
            this.Text = "Blockchain App";
            this.Load += new System.EventHandler(this.BlockchainApp_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtPubKey;
        private System.Windows.Forms.TextBox txtPrivKey;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox txtAmnt;
        private System.Windows.Forms.TextBox txtFee;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtRKey;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.ComboBox cmbo_MineMethod;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.RichTextBox aPow;
        private System.Windows.Forms.TextBox txtMineTime;
        private System.Windows.Forms.Button button15;
    }
}

