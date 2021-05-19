using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace YetiFTPClient
{
    public partial class TransferScreen : Form
    {

        private string ip;
        private FTPConnection connection;
        public TransferScreen(Control sender)
        {
            InitializeComponent();

            SelectedBench.Text += sender.Text;
            ip = sender.Text;

            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(Panel_DragEnter);
            this.DragDrop += new DragEventHandler(Panel_DragDrop);

            connection = new FTPConnection(ip, "pi", "pi");
        }


        private void Panel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void Panel_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                bool success = connection.Upload(file);

                if (success)
                    Status.Text = "Status: " + Path.GetFileName(file) + " successfully transferred";
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Brushes.Black), new Rectangle(0, 0, panel2.Width - 1, panel2.Height - 1));
        }
    }
}
