using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PdfMaker
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

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            label2.Visible = true;

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void panel1_DragLeave(object sender, EventArgs e)
        {
            label2.Visible = false;
        }

        private bool FileValidate(FilePresentation file)
        {
            return file.Extension == "doc" || file.Extension == "docx";
        }

        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            List<FilePresentation> err = new();

            foreach (var item in files)
            {
                FilePresentation file = new(item);

                if (!FileValidate(file))
                {
                    err.Add(file);
                    continue;
                }

                Task.Factory.StartNew(() =>
                {
                    using (var doc = new WebSupergoo.WordGlue3.Doc(file.GetFullPath()))
                    {
                        doc.SaveAs(file.GetFullPath("pdf"));
                    }
                });
            }

            if (err.Count() == 0)
            {
                label2.Text = "Successfull!";
            }
            else
            {
                label2.Text = $"{files.Length - err.Count()} of {files.Length} Succeed";
            }
        }

    }
}
