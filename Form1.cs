using iText.Kernel.Pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDF_Password_Remover
{
    public partial class Form1 : Form
    {
        string last_dir = "";
        string file_name = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.BackColor = System.Drawing.Color.Transparent;

        }

        public static void manipulatePdf(String src, String dest, String password)
        {
            try
            {
                byte[] password_bytes = Encoding.ASCII.GetBytes(password);

                PdfReader reader = new PdfReader(src, new ReaderProperties().SetPassword(password_bytes));
                reader.SetUnethicalReading(true);
                PdfWriter writer = new PdfWriter(dest,
                        new WriterProperties().SetStandardEncryption(null, password_bytes, EncryptionConstants.ALLOW_PRINTING,
                                EncryptionConstants.ENCRYPTION_AES_128 | EncryptionConstants.DO_NOT_ENCRYPT_METADATA));
                PdfDocument pdfDoc = new PdfDocument(reader, writer);
                pdfDoc.Close();
                MessageBox.Show("Successfully converted. Saved in : " + dest);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error" + ex.Message);
            }
            
	}

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox1_Click(object sender, EventArgs e)
        {
            btn_browse.PerformClick();
        }

        [STAThread]
        private void btn_browse_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.Filter = "PDF Files|*.pdf";
                openFileDialog1.ShowHelp = true;
                openFileDialog1.FileName = null;
                openFileDialog1.Title = "Add PDF...";
                DialogResult result = openFileDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string file = openFileDialog1.FileName;
                    textBox1.Text = file;
                    last_dir = Path.GetFileName(Path.GetDirectoryName(file));
                    file_name = Path.GetFileName(file);

                }
                openFileDialog1.Dispose();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
           

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string dest = textBox1.Text.Replace(".pdf", "_unlocked.pdf");
            manipulatePdf(textBox1.Text, dest, textBox2.Text);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
