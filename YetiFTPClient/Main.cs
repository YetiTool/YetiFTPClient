using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading.Tasks;

namespace YetiFTPClient
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();

            //Get default gateway
            string defaultGateway = GetGateway();

            //Start scan
            NewScan(defaultGateway);
        }

        //This is run once the scan is completed to open the list of devices
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.WindowState = FormWindowState.Normal;
            this.Focus();
            this.Show();
        }

        private void NewScan(string defaultGateway)
        {
            NetworkScanner scanner = new NetworkScanner(defaultGateway);
            List<SmartBench> benches = scanner.GetSmartbenches();
            foreach(SmartBench bench in benches)
            {
                AddBench(bench);
            }

            if (benches.Count < 1)
                TitleLabel.Text = "Couldn't find any SmartBenches";
        }

        //Get default gateway
        private string GetGateway()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    string gateway = ip.ToString();
                    string defaultGateway = gateway.Split(".")[0] + "." + gateway.Split(".")[1] + "." + gateway.Split(".")[2];
                    return defaultGateway;
                }
            }

            return null;
        }

        //Add smartbench to list by object of bench storing ip, name and the ftp connection object
        private void AddBench(SmartBench bench)
        {
            Label label = new Label();
            Image image = Properties.Resources.sb_icon;
            label.Size = new Size(100, 150);
            label.Image = image;
            label.Text = bench.GetName();
            label.Name = bench.GetIP();
            label.Anchor = AnchorStyles.None;
            label.TextAlign = ContentAlignment.BottomCenter;
            label.Click += new EventHandler(imageClick);
            if(tableLayoutPanel1.InvokeRequired)
            {
                tableLayoutPanel1.Invoke(new MethodInvoker(delegate { tableLayoutPanel1.Controls.Add(label); TitleLabel.Text = "Select a SmartBench"; }));
            } else
                tableLayoutPanel1.Controls.Add(label);
        }

        //Open unique transfer screen on click
        private void imageClick(object sender, EventArgs e)
        {
            TransferScreen screen = new TransferScreen((Control)sender);
            screen.Show();
        }

        private void HelpButton_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://www.yetitool.com/SUPPORT/KNOWLEDGE-BASE/smartbench1-file-transfer-app") { UseShellExecute = true });
        }

        private void HelpLabel_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://www.yetitool.com/SUPPORT/KNOWLEDGE-BASE/smartbench1-file-transfer-app") { UseShellExecute = true });
        }

        private void RetryIcon_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.Controls.Clear();
            TitleLabel.Text = "Refreshing...";
            Task.Run(() => { NewScan(GetGateway()); });
        }
    }
}
