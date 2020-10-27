using RadiusR_Customer_Setup_Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RadiusR_Customer_Setup_Hash_Utility
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            FileCodeTextbox.MaxLength = int.MaxValue;
        }

        private void GenerateButton_Click(object sender, EventArgs e)
        {
            var passwordHash = RawHashCheckbox.Checked ? PasswordTextbox.Text : GetHashString(PasswordTextbox.Text);
            var generatedKey = GetHashString(passwordHash + KeyFragmentTextbox.Text);
            PasswordHashTextbox.Text = passwordHash;
            GeneratedKeyTextbox.Text = generatedKey;
        }

        private string GetHashString(string input)
        {
            var algorithm = SHA1.Create();
            return string.Join("", algorithm.ComputeHash(Encoding.UTF8.GetBytes(input)).Select(b => b.ToString("x2")));
        }

        private void PasswordHashCopyButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(PasswordHashTextbox.Text);
        }

        private void GeneratedKeyCopyButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(GeneratedKeyTextbox.Text);
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            if (FileDialog.ShowDialog() == DialogResult.OK)
            {
                FileTextbox.Text = FileDialog.FileName;
                var file = new FileStream(FileDialog.FileName, FileMode.Open);
                FileCodeTextbox.Text = FileConverter.GetFileCode(file);
                file.Close();
            }
        }

        private void FileCopyButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(FileCodeTextbox.Text);
        }

        private void DecodeButton_Click(object sender, EventArgs e)
        {
            FileDialog.CheckFileExists = false;
            if (FileDialog.ShowDialog() == DialogResult.OK)
            {
                using (var fileStream = File.Create(FileDialog.FileName))
                {
                    FileConverter.WriteToStream(fileStream, DecodeTextbox.Text);

                }
            }
            FileDialog.CheckFileExists = true;
        }
    }
}
