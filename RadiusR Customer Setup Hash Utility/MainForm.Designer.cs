namespace RadiusR_Customer_Setup_Hash_Utility
{
    partial class MainForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.KeyFragmentTextbox = new System.Windows.Forms.TextBox();
            this.PasswordTextbox = new System.Windows.Forms.TextBox();
            this.GeneratedKeyTextbox = new System.Windows.Forms.TextBox();
            this.PasswordHashTextbox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.PasswordHashCopyButton = new System.Windows.Forms.Button();
            this.GeneratedKeyCopyButton = new System.Windows.Forms.Button();
            this.GenerateButton = new System.Windows.Forms.Button();
            this.MainTabcontrol = new System.Windows.Forms.TabControl();
            this.HashTabpage = new System.Windows.Forms.TabPage();
            this.RawHashCheckbox = new System.Windows.Forms.CheckBox();
            this.FileEncodeTabpage = new System.Windows.Forms.TabPage();
            this.FileCopyButton = new System.Windows.Forms.Button();
            this.FileCodeTextbox = new System.Windows.Forms.TextBox();
            this.BrowseButton = new System.Windows.Forms.Button();
            this.FileTextbox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.FileDecodeTabpage = new System.Windows.Forms.TabPage();
            this.DecodeButton = new System.Windows.Forms.Button();
            this.DecodeTextbox = new System.Windows.Forms.TextBox();
            this.FileDialog = new System.Windows.Forms.OpenFileDialog();
            this.label6 = new System.Windows.Forms.Label();
            this.HashSaltTextbox = new System.Windows.Forms.TextBox();
            this.HashSaltCopyButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.UsernameTextbox = new System.Windows.Forms.TextBox();
            this.MainTabcontrol.SuspendLayout();
            this.HashTabpage.SuspendLayout();
            this.FileEncodeTabpage.SuspendLayout();
            this.FileDecodeTabpage.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Key Fragment:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Password:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 140);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Generated Key:";
            // 
            // KeyFragmentTextbox
            // 
            this.KeyFragmentTextbox.Location = new System.Drawing.Point(92, 33);
            this.KeyFragmentTextbox.Name = "KeyFragmentTextbox";
            this.KeyFragmentTextbox.Size = new System.Drawing.Size(380, 20);
            this.KeyFragmentTextbox.TabIndex = 1;
            // 
            // PasswordTextbox
            // 
            this.PasswordTextbox.Location = new System.Drawing.Point(92, 59);
            this.PasswordTextbox.Name = "PasswordTextbox";
            this.PasswordTextbox.Size = new System.Drawing.Size(380, 20);
            this.PasswordTextbox.TabIndex = 2;
            // 
            // GeneratedKeyTextbox
            // 
            this.GeneratedKeyTextbox.Location = new System.Drawing.Point(92, 137);
            this.GeneratedKeyTextbox.Name = "GeneratedKeyTextbox";
            this.GeneratedKeyTextbox.ReadOnly = true;
            this.GeneratedKeyTextbox.Size = new System.Drawing.Size(380, 20);
            this.GeneratedKeyTextbox.TabIndex = 9;
            // 
            // PasswordHashTextbox
            // 
            this.PasswordHashTextbox.Location = new System.Drawing.Point(92, 111);
            this.PasswordHashTextbox.Name = "PasswordHashTextbox";
            this.PasswordHashTextbox.ReadOnly = true;
            this.PasswordHashTextbox.Size = new System.Drawing.Size(380, 20);
            this.PasswordHashTextbox.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Password Hash:";
            // 
            // PasswordHashCopyButton
            // 
            this.PasswordHashCopyButton.Location = new System.Drawing.Point(478, 109);
            this.PasswordHashCopyButton.Name = "PasswordHashCopyButton";
            this.PasswordHashCopyButton.Size = new System.Drawing.Size(75, 23);
            this.PasswordHashCopyButton.TabIndex = 4;
            this.PasswordHashCopyButton.Text = "Copy";
            this.PasswordHashCopyButton.UseVisualStyleBackColor = true;
            this.PasswordHashCopyButton.Click += new System.EventHandler(this.PasswordHashCopyButton_Click);
            // 
            // GeneratedKeyCopyButton
            // 
            this.GeneratedKeyCopyButton.Location = new System.Drawing.Point(478, 135);
            this.GeneratedKeyCopyButton.Name = "GeneratedKeyCopyButton";
            this.GeneratedKeyCopyButton.Size = new System.Drawing.Size(75, 23);
            this.GeneratedKeyCopyButton.TabIndex = 5;
            this.GeneratedKeyCopyButton.Text = "Copy";
            this.GeneratedKeyCopyButton.UseVisualStyleBackColor = true;
            this.GeneratedKeyCopyButton.Click += new System.EventHandler(this.GeneratedKeyCopyButton_Click);
            // 
            // GenerateButton
            // 
            this.GenerateButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.GenerateButton.Location = new System.Drawing.Point(92, 164);
            this.GenerateButton.Name = "GenerateButton";
            this.GenerateButton.Size = new System.Drawing.Size(380, 23);
            this.GenerateButton.TabIndex = 6;
            this.GenerateButton.Text = "Generate";
            this.GenerateButton.UseVisualStyleBackColor = true;
            this.GenerateButton.Click += new System.EventHandler(this.GenerateButton_Click);
            // 
            // MainTabcontrol
            // 
            this.MainTabcontrol.Controls.Add(this.HashTabpage);
            this.MainTabcontrol.Controls.Add(this.FileEncodeTabpage);
            this.MainTabcontrol.Controls.Add(this.FileDecodeTabpage);
            this.MainTabcontrol.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTabcontrol.Location = new System.Drawing.Point(0, 0);
            this.MainTabcontrol.Name = "MainTabcontrol";
            this.MainTabcontrol.SelectedIndex = 0;
            this.MainTabcontrol.Size = new System.Drawing.Size(568, 221);
            this.MainTabcontrol.TabIndex = 0;
            // 
            // HashTabpage
            // 
            this.HashTabpage.Controls.Add(this.UsernameTextbox);
            this.HashTabpage.Controls.Add(this.label7);
            this.HashTabpage.Controls.Add(this.HashSaltCopyButton);
            this.HashTabpage.Controls.Add(this.HashSaltTextbox);
            this.HashTabpage.Controls.Add(this.label6);
            this.HashTabpage.Controls.Add(this.RawHashCheckbox);
            this.HashTabpage.Controls.Add(this.label1);
            this.HashTabpage.Controls.Add(this.GenerateButton);
            this.HashTabpage.Controls.Add(this.label2);
            this.HashTabpage.Controls.Add(this.GeneratedKeyCopyButton);
            this.HashTabpage.Controls.Add(this.label3);
            this.HashTabpage.Controls.Add(this.PasswordHashCopyButton);
            this.HashTabpage.Controls.Add(this.label4);
            this.HashTabpage.Controls.Add(this.PasswordHashTextbox);
            this.HashTabpage.Controls.Add(this.KeyFragmentTextbox);
            this.HashTabpage.Controls.Add(this.GeneratedKeyTextbox);
            this.HashTabpage.Controls.Add(this.PasswordTextbox);
            this.HashTabpage.Location = new System.Drawing.Point(4, 22);
            this.HashTabpage.Name = "HashTabpage";
            this.HashTabpage.Padding = new System.Windows.Forms.Padding(3);
            this.HashTabpage.Size = new System.Drawing.Size(560, 195);
            this.HashTabpage.TabIndex = 0;
            this.HashTabpage.Text = "Hash";
            this.HashTabpage.UseVisualStyleBackColor = true;
            // 
            // RawHashCheckbox
            // 
            this.RawHashCheckbox.AutoSize = true;
            this.RawHashCheckbox.Location = new System.Drawing.Point(478, 61);
            this.RawHashCheckbox.Name = "RawHashCheckbox";
            this.RawHashCheckbox.Size = new System.Drawing.Size(76, 17);
            this.RawHashCheckbox.TabIndex = 7;
            this.RawHashCheckbox.Text = "Raw Hash";
            this.RawHashCheckbox.UseVisualStyleBackColor = true;
            // 
            // FileEncodeTabpage
            // 
            this.FileEncodeTabpage.Controls.Add(this.FileCopyButton);
            this.FileEncodeTabpage.Controls.Add(this.FileCodeTextbox);
            this.FileEncodeTabpage.Controls.Add(this.BrowseButton);
            this.FileEncodeTabpage.Controls.Add(this.FileTextbox);
            this.FileEncodeTabpage.Controls.Add(this.label5);
            this.FileEncodeTabpage.Location = new System.Drawing.Point(4, 22);
            this.FileEncodeTabpage.Name = "FileEncodeTabpage";
            this.FileEncodeTabpage.Padding = new System.Windows.Forms.Padding(3);
            this.FileEncodeTabpage.Size = new System.Drawing.Size(560, 195);
            this.FileEncodeTabpage.TabIndex = 1;
            this.FileEncodeTabpage.Text = "File Encode";
            this.FileEncodeTabpage.UseVisualStyleBackColor = true;
            // 
            // FileCopyButton
            // 
            this.FileCopyButton.Location = new System.Drawing.Point(460, 164);
            this.FileCopyButton.Name = "FileCopyButton";
            this.FileCopyButton.Size = new System.Drawing.Size(75, 23);
            this.FileCopyButton.TabIndex = 4;
            this.FileCopyButton.Text = "Copy";
            this.FileCopyButton.UseVisualStyleBackColor = true;
            this.FileCopyButton.Click += new System.EventHandler(this.FileCopyButton_Click);
            // 
            // FileCodeTextbox
            // 
            this.FileCodeTextbox.BackColor = System.Drawing.Color.Linen;
            this.FileCodeTextbox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.FileCodeTextbox.Location = new System.Drawing.Point(3, 86);
            this.FileCodeTextbox.MaxLength = 2147483647;
            this.FileCodeTextbox.Multiline = true;
            this.FileCodeTextbox.Name = "FileCodeTextbox";
            this.FileCodeTextbox.ReadOnly = true;
            this.FileCodeTextbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.FileCodeTextbox.Size = new System.Drawing.Size(554, 106);
            this.FileCodeTextbox.TabIndex = 3;
            // 
            // BrowseButton
            // 
            this.BrowseButton.Location = new System.Drawing.Point(477, 4);
            this.BrowseButton.Name = "BrowseButton";
            this.BrowseButton.Size = new System.Drawing.Size(75, 23);
            this.BrowseButton.TabIndex = 2;
            this.BrowseButton.Text = "Browse";
            this.BrowseButton.UseVisualStyleBackColor = true;
            this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // FileTextbox
            // 
            this.FileTextbox.Location = new System.Drawing.Point(40, 6);
            this.FileTextbox.Name = "FileTextbox";
            this.FileTextbox.ReadOnly = true;
            this.FileTextbox.Size = new System.Drawing.Size(431, 20);
            this.FileTextbox.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "File:";
            // 
            // FileDecodeTabpage
            // 
            this.FileDecodeTabpage.Controls.Add(this.DecodeButton);
            this.FileDecodeTabpage.Controls.Add(this.DecodeTextbox);
            this.FileDecodeTabpage.Location = new System.Drawing.Point(4, 22);
            this.FileDecodeTabpage.Name = "FileDecodeTabpage";
            this.FileDecodeTabpage.Size = new System.Drawing.Size(560, 195);
            this.FileDecodeTabpage.TabIndex = 2;
            this.FileDecodeTabpage.Text = "File Decode";
            this.FileDecodeTabpage.UseVisualStyleBackColor = true;
            // 
            // DecodeButton
            // 
            this.DecodeButton.Location = new System.Drawing.Point(477, 6);
            this.DecodeButton.Name = "DecodeButton";
            this.DecodeButton.Size = new System.Drawing.Size(75, 23);
            this.DecodeButton.TabIndex = 1;
            this.DecodeButton.Text = "Decode";
            this.DecodeButton.UseVisualStyleBackColor = true;
            this.DecodeButton.Click += new System.EventHandler(this.DecodeButton_Click);
            // 
            // DecodeTextbox
            // 
            this.DecodeTextbox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.DecodeTextbox.Location = new System.Drawing.Point(0, 87);
            this.DecodeTextbox.MaxLength = 2147483647;
            this.DecodeTextbox.Multiline = true;
            this.DecodeTextbox.Name = "DecodeTextbox";
            this.DecodeTextbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DecodeTextbox.Size = new System.Drawing.Size(560, 108);
            this.DecodeTextbox.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Salt:";
            // 
            // HashSaltTextbox
            // 
            this.HashSaltTextbox.Location = new System.Drawing.Point(92, 85);
            this.HashSaltTextbox.Name = "HashSaltTextbox";
            this.HashSaltTextbox.ReadOnly = true;
            this.HashSaltTextbox.Size = new System.Drawing.Size(380, 20);
            this.HashSaltTextbox.TabIndex = 7;
            // 
            // HashSaltCopyButton
            // 
            this.HashSaltCopyButton.Location = new System.Drawing.Point(477, 83);
            this.HashSaltCopyButton.Name = "HashSaltCopyButton";
            this.HashSaltCopyButton.Size = new System.Drawing.Size(75, 23);
            this.HashSaltCopyButton.TabIndex = 3;
            this.HashSaltCopyButton.Text = "Copy";
            this.HashSaltCopyButton.UseVisualStyleBackColor = true;
            this.HashSaltCopyButton.Click += new System.EventHandler(this.HashSaltCopyButton_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Username:";
            // 
            // UsernameTextbox
            // 
            this.UsernameTextbox.Location = new System.Drawing.Point(92, 7);
            this.UsernameTextbox.Name = "UsernameTextbox";
            this.UsernameTextbox.Size = new System.Drawing.Size(380, 20);
            this.UsernameTextbox.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 221);
            this.Controls.Add(this.MainTabcontrol);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RadiusR Customer Setup Hash Utility";
            this.MainTabcontrol.ResumeLayout(false);
            this.HashTabpage.ResumeLayout(false);
            this.HashTabpage.PerformLayout();
            this.FileEncodeTabpage.ResumeLayout(false);
            this.FileEncodeTabpage.PerformLayout();
            this.FileDecodeTabpage.ResumeLayout(false);
            this.FileDecodeTabpage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox KeyFragmentTextbox;
        private System.Windows.Forms.TextBox PasswordTextbox;
        private System.Windows.Forms.TextBox GeneratedKeyTextbox;
        private System.Windows.Forms.TextBox PasswordHashTextbox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button PasswordHashCopyButton;
        private System.Windows.Forms.Button GeneratedKeyCopyButton;
        private System.Windows.Forms.Button GenerateButton;
        private System.Windows.Forms.TabControl MainTabcontrol;
        private System.Windows.Forms.TabPage HashTabpage;
        private System.Windows.Forms.TabPage FileEncodeTabpage;
        private System.Windows.Forms.TextBox FileTextbox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button BrowseButton;
        private System.Windows.Forms.OpenFileDialog FileDialog;
        private System.Windows.Forms.TextBox FileCodeTextbox;
        private System.Windows.Forms.Button FileCopyButton;
        private System.Windows.Forms.TabPage FileDecodeTabpage;
        private System.Windows.Forms.TextBox DecodeTextbox;
        private System.Windows.Forms.Button DecodeButton;
        private System.Windows.Forms.CheckBox RawHashCheckbox;
        private System.Windows.Forms.Button HashSaltCopyButton;
        private System.Windows.Forms.TextBox HashSaltTextbox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox UsernameTextbox;
        private System.Windows.Forms.Label label7;
    }
}

