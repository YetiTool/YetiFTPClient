using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YetiFTPClient
{
    public partial class Loading : Form
    {
        private static Loading loading;
        private delegate void CloseDelegate();

        public Loading()
        {
            InitializeComponent();
        }

        public static void ShowLoading()
        {
            if (loading != null) return;
            loading = new Loading();
            Thread thread = new Thread(new ThreadStart(Loading.ShowForm));
            thread.IsBackground = true;
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        private static void ShowForm()
        {
            if (loading != null) Application.Run(loading);
        }

        public static void CloseForm()
        {
            loading?.Invoke(new CloseDelegate(Loading.CloseFromInternal));
        }

        private static void CloseFromInternal()
        {
            if(loading != null)
            {
                loading.Close();
                loading = null;
            }
        }

        private void HelpButton_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://www.yetitool.com/support/knowledge-base") { UseShellExecute = true });
        }
    }
}
