using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
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

            var name = Application.ProductName;
            var version = Application.ProductVersion;
            this.Text = System.String.Format("{0} | Transfer Your Files (v{1})", name, version);

            SelectedBench.Text += sender.Text;
            ip = sender.Name;
            IPLabel.Text = ip;

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
            FileDropped(sender, e);
        }

        private async void FileDropped(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                UploadIcon.Visible = false;
                UploadingGIF.Visible = true;
                TransferLabel.Text = "Transferring...";
                bool success = false;
                await Task.Run(() =>
                {
                    success = connection.Upload(file);
                });

                if (success)
                {
                    UploadingGIF.Visible = false;
                    CrossIcon.Visible = false;
                    TickIcon.Visible = true;
                    TransferLabel.Text = "Transfer Successful";
                } else
                {
                    TickIcon.Visible = false;
                    UploadingGIF.Visible = false;
                    CrossIcon.Visible = true;
                    TransferLabel.Text = "Transfer Failed";
                }

            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Brushes.Black), new Rectangle(0, 0, panel2.Width - 1, panel2.Height - 1));
        }

        private void HelpButton_Click(object sender, System.EventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://www.yetitool.com/SUPPORT/KNOWLEDGE-BASE/smartbench1-file-transfer-app") { UseShellExecute = true });
        }

        private void HelpLabel_Click(object sender, System.EventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://www.yetitool.com/SUPPORT/KNOWLEDGE-BASE/smartbench1-file-transfer-app") { UseShellExecute = true });
        }
    }
}
