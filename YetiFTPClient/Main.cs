﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;

namespace YetiFTPClient
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();

            //Get default gateway
            string defaultGateway = GetDefaultGateway();

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

            var name = Application.ProductName;
            var version = Application.ProductVersion;
            this.Text = String.Format("{0} | Select a SmartBench (v{1})", name, version);
        }

        private void NewScan(string defaultGateway)
        {
            NetworkScanner scanner = new NetworkScanner(defaultGateway);
            List<SmartBench> benches = scanner.GetSmartbenches();
            foreach(SmartBench bench in benches)
            {
                AddBench(bench);
            }

            System.Diagnostics.Debug.WriteLine("Scan done");

            if (benches.Count < 1)
                if (TitleLabel.InvokeRequired)
                    TitleLabel.Invoke(new MethodInvoker(delegate { TitleLabel.Text = "Couldn't find any SmartBenches"; }));
                else
                    TitleLabel.Text = "Couldn't find any SmartBenches";
        }

        private string GetDefaultGateway()
        {
            try
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                Dictionary<bool, string> dict = new Dictionary<bool, string>();
                foreach(var ip in host.AddressList)
                {
                    if(ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        dict.Add(ip.ToString().StartsWith("192"), ip.ToString());
                    }
                }

                if(dict.ContainsKey(true))
                {
                    foreach(var pair in dict)
                    {
                        if (pair.Key == true)
                        {
                            string gateway = pair.Value;
                            string[] split = gateway.Split(".");

                            return split[0] + "." + split[1] + "." + split[2];
                        }
                    }
                } else
                {
                    string gateway = dict.First().Value;
                    string[] split = gateway.Split(".");

                    return split[0] + "." + split[1] + "." + split[2];
                }
            } catch
            {
                Logging.TryLog("Connection Error", "Couldn't find compatible adapter");
            }

            return "";
        }

        //Get default gateway
        private string GetGateway()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    if(ip.ToString().StartsWith("192"))
                    {
                        string gateway = ip.ToString();
                        string defaultGateway = gateway.Split(".")[0] + "." + gateway.Split(".")[1] + "." + gateway.Split(".")[2];
                        return defaultGateway;
                    }
                }
            }
            return "";
        }

        //Add smartbench to list by object of bench storing ip, name and the ftp connection object
        private void AddBench(SmartBench bench)
        {
            Label label = new Label();
            Image image = Properties.Resources.sb_icon;
            label.Size = new Size(150, 150);
            label.Image = image;
            label.Text = bench.GetName();
            label.Font = new Font("Raleway", 11);
            label.Name = bench.GetIP();
            label.ImageAlign = ContentAlignment.MiddleCenter;
            label.TextAlign = ContentAlignment.BottomCenter;
            label.Click += new EventHandler(imageClick);

            new ToolTip().SetToolTip(label, bench.GetFullName());
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
            Process.Start(new ProcessStartInfo("https://www.yetitool.com/SUPPORT/KNOWLEDGE-BASE/smartbench1-sending-jobs-to-smartbench-smarttransfer") { UseShellExecute = true });
        }

        private void HelpLabel_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://www.yetitool.com/SUPPORT/KNOWLEDGE-BASE/smartbench1-sending-jobs-to-smartbench-smarttransfer") { UseShellExecute = true });
        }

        private void RetryIcon_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.Controls.Clear();
            TitleLabel.Text = "Refreshing...";
            Task.Run(() => { NewScan(GetDefaultGateway()); });
        }

        private void SaveLogs_Click(object sender, EventArgs e)
        {
            Logging.SaveLogs();
        }

        private void ScanLogs_Paint(object sender, PaintEventArgs e)
        {
            new ToolTip().SetToolTip(SaveLogs, "This will save a log file to your desktop that can be used to diagnose issues.");
            e.Graphics.DrawRectangle(new Pen(Brushes.Black), new Rectangle(0, 0, SaveLogs.Width - 1, SaveLogs.Height - 1));
        }
    }
}
