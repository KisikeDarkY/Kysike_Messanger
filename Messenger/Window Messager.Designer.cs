namespace Messenger
{
    partial class Window_Messager
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
            this.UsingRTB = new System.Windows.Forms.RichTextBox();
            this.Send_button = new System.Windows.Forms.Button();
            this.OutputRTB = new System.Windows.Forms.RichTextBox();
            this.Exit_button = new System.Windows.Forms.Button();
            this.State_messageCB = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // UsingRTB
            // 
            this.UsingRTB.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.UsingRTB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UsingRTB.Location = new System.Drawing.Point(68, 461);
            this.UsingRTB.Name = "UsingRTB";
            this.UsingRTB.Size = new System.Drawing.Size(653, 32);
            this.UsingRTB.TabIndex = 0;
            this.UsingRTB.Text = "";
            // 
            // Send_button
            // 
            this.Send_button.BackColor = System.Drawing.Color.LightGray;
            this.Send_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Send_button.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Send_button.Location = new System.Drawing.Point(671, 461);
            this.Send_button.Name = "Send_button";
            this.Send_button.Size = new System.Drawing.Size(50, 32);
            this.Send_button.TabIndex = 1;
            this.Send_button.Text = "Send";
            this.Send_button.UseVisualStyleBackColor = false;
            this.Send_button.Click += new System.EventHandler(this.Send_button_Click);
            // 
            // OutputRTB
            // 
            this.OutputRTB.BackColor = System.Drawing.Color.Gray;
            this.OutputRTB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OutputRTB.Location = new System.Drawing.Point(68, 19);
            this.OutputRTB.Name = "OutputRTB";
            this.OutputRTB.ReadOnly = true;
            this.OutputRTB.Size = new System.Drawing.Size(653, 436);
            this.OutputRTB.TabIndex = 2;
            this.OutputRTB.Text = "";
            // 
            // Exit_button
            // 
            this.Exit_button.BackColor = System.Drawing.Color.LightGray;
            this.Exit_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Exit_button.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Exit_button.Location = new System.Drawing.Point(12, 14);
            this.Exit_button.Name = "Exit_button";
            this.Exit_button.Size = new System.Drawing.Size(50, 26);
            this.Exit_button.TabIndex = 3;
            this.Exit_button.Text = "Exit";
            this.Exit_button.UseVisualStyleBackColor = false;
            this.Exit_button.Click += new System.EventHandler(this.Exit_button_Click);
            // 
            // State_messageCB
            // 
            this.State_messageCB.AutoCheck = false;
            this.State_messageCB.AutoSize = true;
            this.State_messageCB.Location = new System.Drawing.Point(47, 463);
            this.State_messageCB.Name = "State_messageCB";
            this.State_messageCB.Size = new System.Drawing.Size(15, 14);
            this.State_messageCB.TabIndex = 4;
            this.State_messageCB.TabStop = false;
            this.State_messageCB.ThreeState = true;
            this.State_messageCB.UseVisualStyleBackColor = true;
            // 
            // Window_Messager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(733, 505);
            this.Controls.Add(this.State_messageCB);
            this.Controls.Add(this.Exit_button);
            this.Controls.Add(this.OutputRTB);
            this.Controls.Add(this.Send_button);
            this.Controls.Add(this.UsingRTB);
            this.Name = "Window_Messager";
            this.Text = "Window_Messager";
            this.Load += new System.EventHandler(this.Window_Messager_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox UsingRTB;
        private System.Windows.Forms.Button Send_button;
        private System.Windows.Forms.RichTextBox OutputRTB;
        private System.Windows.Forms.Button Exit_button;
        private System.Windows.Forms.CheckBox State_messageCB;
    }
}