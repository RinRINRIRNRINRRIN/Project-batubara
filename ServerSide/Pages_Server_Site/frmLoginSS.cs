using Serilog;
using ServerSide.Dbcontent;
using ServerSide.Models;
using ServerSide.Pages;
using ServerSide.Pages_Public;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TruckScale.functions;
namespace ServerSide
{
    public partial class frmLoginSS : Form
    {
        public frmLoginSS()
        {
            InitializeComponent();
            Log.Information("Open the program");
        }


        async Task TestConnect()
        {
            btnLogin.Enabled = false;
            gbLoader.Visible = true;
            await Task.Delay(1500);
            bool isSuccess = false;
            await Task.Run(() =>
            {
                if (ConnectDb.Connect())
                    isSuccess = true;
                else
                    isSuccess = false;
            });
            await Task.Delay(1500);
            if (isSuccess)
            {
                lblMessageSERVER.Text = "เชื่อมต่อฐานข้อมูลสำเร็จ";
                btnLogin.Enabled = true;
                gbLoader.Visible = false;
            }
            else
            {
                lblMessageSERVER.Text = "ไม่สามารถเชื่อมต่อฐานข้อมูล";
                MessageBox.Show(ConnectDb.ERR);
            }
        }

        private async void frmLogin_Load(object sender, EventArgs e)
        {
            VersionF versionF = new VersionF();
            lblVersion.Text = $"V{versionF.GetFullVersion()},Build Date : {versionF.GetBuildDate()}";

            // ทดสอบเชื่อมต่อ
            await TestConnect();
        }

        void Login(string _user, string _pass)
        {
            AccountManagementDb accountManagementDb = new AccountManagementDb();
            AccountManagementModel accountManagementModel = accountManagementDb.GetAccountByUsernameAndPassword(_user, _pass);

            if (accountManagementModel == null)
            {
                msg.Icon = Guna.UI2.WinForms.MessageDialogIcon.Warning;
                msg.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                msg.Show($"Username or Password is incorect \n{accountManagementDb.ERR}", "Incorrect");
                return;
            }
            else
            {
                frmMainSS frmMain = new frmMainSS(accountManagementModel);
                this.Hide();
                frmMain.ShowDialog();
                txtPassword.Clear();
                txtUsername.Clear();
                this.Show();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login(txtUsername.Text, txtPassword.Text);
        }

        private void pictureBox3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //this.Hide();
            //frmDbConfig frmDbConfig = new frmDbConfig();
            //frmDbConfig.ShowDialog();
            //this.Show();
        }

        private void pictureBox3_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                frmDbConfig frmDbConfig = new frmDbConfig();
                frmDbConfig.ShowDialog();
                this.Show();
            }
            catch
            {

            }

        }
    }
}
