using Guna.UI2.WinForms;
using Serilog;
using ServerSide.Dbcontent;
using ServerSide.Models;
using ServerSide.Pages_Public;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServerSide.Pages_Client_Site
{
    public partial class frmMainCS : Form
    {
        public frmMainCS(AccountManagementModel accountManagementModel)
        {
            InitializeComponent();
            _accountManagementModel = accountManagementModel;
        }

        private readonly AccountManagementModel _accountManagementModel;

        void CheckPermission()
        {
            PermissionDb permissionDb = new PermissionDb();
            List<PermissionModel> permissionModels = permissionDb.GetAllPermissionByAccountId(_accountManagementModel.Id);
            foreach (var item in permissionModels)
            {
                foreach (Guna2Button btn in this.Controls.OfType<Guna2Button>())
                {
                    if (item.Menuname == btn.Tag.ToString())
                    {
                        btn.Enabled = true;
                        break;
                    }
                }
            }
        }

        private async void frmMainCS_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            CheckPermission();
            try
            {
                Ping ping = new Ping();
                PingReply pingReply = await ping.SendPingAsync("www.thaiscale.co.th", 5000);
                if (pingReply.Status == IPStatus.Success)
                {
                    await webView21.EnsureCoreWebView2Async(null);
                    webView21.CoreWebView2.Settings.IsScriptEnabled = true;
                    webView21.Source = new Uri("https://www.thaiscale.co.th/");
                }
                else { webView21.Visible = false; }
            }
            catch
            {
                webView21.Visible = false;
            }
        }

        private void btnJobManagement_Click(object sender, EventArgs e)
        {
            frmJobManagementCS frmJobManagementCS = new frmJobManagementCS(_accountManagementModel);
            this.Hide();
            Log.Information("========================== open jobmenege from");
            frmJobManagementCS.ShowDialog();
            this.Show();
            //panel1.Controls.Clear();
            //frmJobManagementCS.TopLevel = false;
            //frmJobManagementCS.Dock = DockStyle.Fill;
            //panel1.Controls.Add(frmJobManagementCS);
            //frmJobManagementCS.Show();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            frmReportInformation frmReportInformation = new frmReportInformation(_accountManagementModel);
            this.Hide();
            Log.Information("========================== open reportinformation from");
            frmReportInformation.ShowDialog();
            this.Show();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {

        }

        private void webView21_NavigationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e)
        {
            if (!e.IsSuccess)
            {
                // โหลดไม่สำเร็จ → ซ่อน WebView2
                webView21.Visible = false;
            }
            else
            {
                webView21.Visible = true; // โหลดสำเร็จ
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            frmSetting frmSetting = new frmSetting();
            this.Hide();
            Log.Information("========================== open setting from");
            frmSetting.ShowDialog();
            this.Show();
        }
    }
}
