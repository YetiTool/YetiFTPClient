using System.Diagnostics;
using System.Drawing;
using System.IO;
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

        private string GetTemporaryDirectory()
        {
            string tempFolder = Path.GetTempFileName();
            File.Delete(tempFolder);
            Directory.CreateDirectory(tempFolder);
            return tempFolder;
        }

        //TODO: Cleanup 
        //Move .visible into seperate functions e.g.
        //TransferFailed()
        //TransferSuccessful()
        private async void FileDropped(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            int transferCount = 0;
            foreach (string file in files)
            {
                FileAttributes attr = File.GetAttributes(file);
                if (file.EndsWith(".gcode") || file.EndsWith(".nc") || attr.HasFlag(FileAttributes.Directory))
                {
                    transferCount += 1;
                    if (attr.HasFlag(FileAttributes.Directory))
                    {
                        transferCount += new DirectoryInfo(file).GetFiles().Length;
                    }

                    UploadIcon.Visible = false;
                    CrossIcon.Visible = false;
                    UploadingGIF.Visible = true;
                    TransferLabel.Text = "Transferring " + Path.GetFileName(file);
                    TransferCount.Text = "";
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
                        TransferCount.Text = "Files transferred: " + transferCount;
                    }
                    else
                    {
                        TickIcon.Visible = false;
                        UploadingGIF.Visible = false;
                        CrossIcon.Visible = true;
                        TransferLabel.Text = "Transfer Failed";
                    }
                } else
                {
                    UploadIcon.Visible = false;
                    TickIcon.Visible = false;
                    UploadingGIF.Visible = false;
                    CrossIcon.Visible = true;
                    TransferLabel.Text = "Transfer Failed";
                    TransferCount.Text = "The file you tried to upload is not a job file!";
                }
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Brushes.Black), new Rectangle(0, 0, panel2.Width - 1, panel2.Height - 1));
        }

        private void HelpButton_Click(object sender, System.EventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://www.yetitool.com/SUPPORT/KNOWLEDGE-BASE/smartbench1-sending-jobs-to-smartbench-smarttransfer") { UseShellExecute = true });
        }

        private void HelpLabel_Click(object sender, System.EventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://www.yetitool.com/SUPPORT/KNOWLEDGE-BASE/smartbench1-sending-jobs-to-smartbench-smarttransfer") { UseShellExecute = true });
        }
    }
}
