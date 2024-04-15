using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Image_to_Base64_with_Export
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string imageLocation;

            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Image Files(*.BMP;*.JPG;*.PNG;*.JPEG)|*.BMP;*.JPG;*.PNG;*.JPEG" +
                "|All files(*.*)|*.*";

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    imageLocation = dialog.FileName;
                    pictureBox1.ImageLocation = imageLocation;

                    richTextBox1.Clear();

                    string base64Text;

                    byte[] imageArray = System.IO.File.ReadAllBytes(dialog.FileName);
                    base64Text = Convert.ToBase64String(imageArray); //base64Text must be global but I'll use  richtext
                    richTextBox1.Text = base64Text;
                }
            }

            catch (Exception)
            {
                MessageBox.Show("An Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(richTextBox1.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            string filename = "";

            saveDialog.Filter = "Rich Text File (*.rtf)|*.rtf|Plain Text File (*.txt)|*.txt";

            saveDialog.DefaultExt = "*.txt";
            saveDialog.FilterIndex = 2;
            saveDialog.Title = "Save the contents";

            DialogResult retval = saveDialog.ShowDialog();
            if (retval == DialogResult.OK)
                filename = saveDialog.FileName;
            else
                return;

            RichTextBoxStreamType stream_type;

            if (saveDialog.FilterIndex == 2)
                stream_type = RichTextBoxStreamType.PlainText;
            else
                stream_type = RichTextBoxStreamType.RichText;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();

            OpenFileDialog file_open = new OpenFileDialog();

            file_open.Filter = "Rich Text File (*.rtf)|*.rtf| Plain Text File (*.txt)|*.txt";
            file_open.FilterIndex = 2;
            file_open.Title = "Open a Base64 text file";

            RichTextBoxStreamType stream_type;
            stream_type = RichTextBoxStreamType.RichText;
            if (DialogResult.OK == file_open.ShowDialog())
            {
                if (string.IsNullOrEmpty(file_open.FileName))
                    return;
                if (file_open.FilterIndex == 2)
                    stream_type = RichTextBoxStreamType.PlainText;
                
                richTextBox1.LoadFile(file_open.FileName, stream_type);
            }


        }
    }
}
