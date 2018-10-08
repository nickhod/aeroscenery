using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AeroScenery.UI
{
    public partial class FileOperationProgressForm : Form
    {
        public string MessageText
        {
            get
            {
                return this.messageLabel.Text;
            }
            set
            {
                this.messageLabel.Text = value;
            }
        }

        public string Title
        {
            get
            {
                return this.Text;
            }
            set
            {
                this.Text = value;
            }
        }

        public Task FileOperationTask { get; set; }

        private bool processing { get; set; }

        public FileOperationProgressForm()
        {
            InitializeComponent();
        }

        public async Task DoTaskAsync()
        {
            this.processing = true;
            this.Show();

            await this.FileOperationTask;

            this.processing = false;
            this.Close();
        }

        private void FileOperationProgressForm_Load(object sender, EventArgs e)
        {

        }

        private void FileOperationProgressForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // If the file operation is processing, prevent close
            if (this.processing)
            {
                e.Cancel = true;
            }
        }
    }
}
