namespace Messenger
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.Reg_button = new System.Windows.Forms.Button();
            this.Name_TextBox = new System.Windows.Forms.TextBox();
            this.Tag_TextBox = new System.Windows.Forms.TextBox();
            this.Pasword_TextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Input_button = new System.Windows.Forms.Button();
            this.Input_tagTB = new System.Windows.Forms.TextBox();
            this.Input_PasswordTB = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Reg_button
            // 
            this.Reg_button.Location = new System.Drawing.Point(71, 313);
            this.Reg_button.Name = "Reg_button";
            this.Reg_button.Size = new System.Drawing.Size(106, 32);
            this.Reg_button.TabIndex = 0;
            this.Reg_button.Text = "Registration";
            this.Reg_button.UseVisualStyleBackColor = true;
            this.Reg_button.Click += new System.EventHandler(this.Reg_button_Click);
            // 
            // Name_TextBox
            // 
            this.Name_TextBox.Location = new System.Drawing.Point(71, 78);
            this.Name_TextBox.Name = "Name_TextBox";
            this.Name_TextBox.Size = new System.Drawing.Size(118, 20);
            this.Name_TextBox.TabIndex = 1;
            // 
            // Tag_TextBox
            // 
            this.Tag_TextBox.Location = new System.Drawing.Point(71, 117);
            this.Tag_TextBox.Name = "Tag_TextBox";
            this.Tag_TextBox.Size = new System.Drawing.Size(118, 20);
            this.Tag_TextBox.TabIndex = 2;
            // 
            // Pasword_TextBox
            // 
            this.Pasword_TextBox.Location = new System.Drawing.Point(71, 158);
            this.Pasword_TextBox.Name = "Pasword_TextBox";
            this.Pasword_TextBox.Size = new System.Drawing.Size(118, 20);
            this.Pasword_TextBox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Tag";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 161);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Password";
            // 
            // Input_button
            // 
            this.Input_button.Location = new System.Drawing.Point(278, 313);
            this.Input_button.Name = "Input_button";
            this.Input_button.Size = new System.Drawing.Size(106, 32);
            this.Input_button.TabIndex = 7;
            this.Input_button.Text = "Input";
            this.Input_button.UseVisualStyleBackColor = true;
            this.Input_button.Click += new System.EventHandler(this.Input_button_Click);
            // 
            // Input_tagTB
            // 
            this.Input_tagTB.Location = new System.Drawing.Point(278, 78);
            this.Input_tagTB.Name = "Input_tagTB";
            this.Input_tagTB.Size = new System.Drawing.Size(118, 20);
            this.Input_tagTB.TabIndex = 8;
            // 
            // Input_PasswordTB
            // 
            this.Input_PasswordTB.Location = new System.Drawing.Point(278, 117);
            this.Input_PasswordTB.Name = "Input_PasswordTB";
            this.Input_PasswordTB.Size = new System.Drawing.Size(118, 20);
            this.Input_PasswordTB.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(237, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Tag";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(219, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Password";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(441, 357);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Input_PasswordTB);
            this.Controls.Add(this.Input_tagTB);
            this.Controls.Add(this.Input_button);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Pasword_TextBox);
            this.Controls.Add(this.Tag_TextBox);
            this.Controls.Add(this.Name_TextBox);
            this.Controls.Add(this.Reg_button);
            this.HelpButton = true;
            this.Name = "Form1";
            this.Text = "Registration";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Reg_button;
        private System.Windows.Forms.TextBox Name_TextBox;
        private System.Windows.Forms.TextBox Tag_TextBox;
        private System.Windows.Forms.TextBox Pasword_TextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Input_button;
        private System.Windows.Forms.TextBox Input_tagTB;
        private System.Windows.Forms.TextBox Input_PasswordTB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}

