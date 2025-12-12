using Guna.UI2.WinForms;
using Serilog;
using ServerSide.Dbcontent;
using ServerSide.Models;
using ServerSide.Pages_Client_Site;
using ServerSide.Pages_Server_Site;
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

namespace ServerSide.Pages
{
    public partial class frmMainSS : Form
    {
        public frmMainSS(AccountManagementModel model)
        {
            InitializeComponent();
            _employee = model;
        }

        private readonly AccountManagementModel _employee;

        private void Select_Menu(object sender, EventArgs e)
        {
            Guna2Button btn = sender as Guna2Button;
            this.Hide();
            switch (btn.Tag.ToString())
            {
                case "ImpoortERP":

                    break;
                case "ImportExcel":
                    frmImportExcel frmImportExcel = new frmImportExcel(_employee);
                    Log.Information("========================== open importexcel from");
                    frmImportExcel.ShowDialog();
                    break;
                case "AccountManagement":
                    frmAccountManagementSS frmAccountManagementSS = new frmAccountManagementSS();
                    Log.Information("========================== open AccountManagement from");
                    frmAccountManagementSS.ShowDialog();
                    break;
                case "TicketReport":
                    break;
                case "ReportInformation":
                    frmReportInformation frmReportInformation = new frmReportInformation(_employee);
                    Log.Information("========================== open ReportInformation from");
                    this.Hide();
                    frmReportInformation.ShowDialog();
                    this.Show();
                    break;
                case "VerifyInformation":
                    frmVerify frmVerify = new frmVerify(_employee);
                    Log.Information("========================== open VerifyInformation from");
                    this.Hide();
                    frmVerify.ShowDialog();
                    this.Show();
                    break;
            }
            this.Show();
        }

        void CheckPermission()
        {
            PermissionDb permissionDb = new PermissionDb();
            List<PermissionModel> permissionModels = permissionDb.GetAllPermissionByAccountId(_employee.Id);
            foreach (var item in permissionModels)
            {
                foreach (Guna2Button btn in flowLayoutPanel1.Controls.OfType<Guna2Button>())
                {
                    if (item.Menuname == btn.Tag.ToString())
                    {
                        btn.Enabled = true;
                        break;
                    }
                }
            }
        }
        private async void frmMain_Load(object sender, EventArgs e)
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
    }
}
