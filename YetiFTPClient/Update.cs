using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace YetiFTPClient
{
    public partial class Update : Form
    {
        public Update()
        {
            InitializeComponent();
            var name = Application.ProductName;
            var version = Application.ProductVersion;
            this.Text = String.Format("{0} | Loading (v{1})", name, version);
            this.Focus();
        }

        private void YesButton_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://www.yetitool.com/SUPPORT/KNOWLEDGE-BASE/smartbench1-sending-jobs-to-smartbench-smarttransfer-installing-the-yeti-tool-smarttransfer-app") { UseShellExecute = true });
            this.Close();
        }

        private void NoButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
