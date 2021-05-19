using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using System.Diagnostics;

namespace YetiFTPClient
{
    public partial class MainForm : Form
    {

        //Default rasp pi ftp
        private string USERNAME = "pi";
        private string PASSWORD = "pi";

        public MainForm()
        {
            InitializeComponent();

            //Get default gateway
            string defaultGateway = GetGateway();
            string subnet = defaultGateway.Split(".")[0] + "." + defaultGateway.Split(".")[1] + "." + defaultGateway.Split(".")[2];

            //Start scan
            Scan(subnet);
        }

        //This is run once the scan is completed to open the list of devices
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.WindowState = FormWindowState.Normal;
            this.Focus();
            this.Show();
        }

        /*
         * Get each possible IP on the local network, scan to check whether they're alive, then check for rasp. pi macip.
         * Then check for smartbench_name.txt and read name. If no file present (likely an older easycut version),
         * check whether easycut source (main.py) exists.
         */
        private void Scan(string defaultGateway)
        {
            NetworkScanner scanner = new NetworkScanner(defaultGateway);
            List<String> scannedAddresses = scanner.GetConnections();

            foreach (string ip in scannedAddresses)
            {
                try
                {
                    if (scanner.GetMacByIp(ip).StartsWith("b8-27") || scanner.GetMacByIp(ip).StartsWith("dc-a6-32"))
                    {
                        FTPConnection connection = new FTPConnection(ip, USERNAME, PASSWORD);
                        string name = connection.GetSmartbenchName();
                        if (name != null)
                        {
                            AddBench(new SmartBench(ip, name, connection));
                            Debug.WriteLine("Added bench with name: " + name);
                        }
                        else
                        {
                            if (connection.TryConnect())
                            {
                                AddBench(ip);
                                Debug.WriteLine("Added bench with ip: " + ip);
                            }
                        }
                    }
                }
                catch
                {
                    Debug.WriteLine("Couldn't get MAC IP from Device: " + ip);
                }
            }
        }

        //Get default gateway
        private string GetGateway()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }

            return null;
        }

        //Add smartbench to list by IP (no bench name found)
        private void AddBench(string ip)
        {
            Label label = new Label();
            Image image = Properties.Resources.pc;
            label.Size = new Size(100, 150);
            label.Image = image;
            label.Text = ip;
            label.Name = ip;
            label.Anchor = AnchorStyles.None;
            label.TextAlign = ContentAlignment.BottomCenter;
            label.Click += new EventHandler(imageClick);
            tableLayoutPanel1.Controls.Add(label);
        }

        //Add smartbench to list by object of bench storing ip, name and the ftp connection object
        private void AddBench(SmartBench bench)
        {
            Label label = new Label();
            Image image = Properties.Resources.pc;
            label.Size = new Size(100, 150);
            label.Image = image;
            label.Text = bench.GetName();
            label.Name = bench.GetIP();
            label.Anchor = AnchorStyles.None;
            label.TextAlign = ContentAlignment.BottomCenter;
            label.Click += new EventHandler(imageClick);
            tableLayoutPanel1.Controls.Add(label);
        }

        //Open unique transfer screen on click
        private void imageClick(object sender, EventArgs e)
        {
            TransferScreen screen = new TransferScreen((Control)sender);
            screen.Show();
        }
    }
}
