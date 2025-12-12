using Guna.UI2.WinForms;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServerSide.Pages_Public
{
    public partial class frmDbConfig : Form
    {
        public frmDbConfig()
        {
            InitializeComponent();
        }

        void getParameterFormAppConfig()
        {
            txtHost.Text = ConfigurationManager.AppSettings["HOST"];
            txtPort.Text = ConfigurationManager.AppSettings["PORT"];
            txtDb.Text = ConfigurationManager.AppSettings["DB"];
            txtUser.Text = ConfigurationManager.AppSettings["USER"];
            txtPass.Text = ConfigurationManager.AppSettings["PASS"];
            Log.Information("==Load config");
            Log.Information("host : " + txtHost.Text);
            Log.Information("port : " + txtHost.Text);
            Log.Information("Db : " + txtDb.Text);
            Log.Information("User : " + txtUser.Text);
            Log.Information("Pass : " + txtPass.Text);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Guna2TextBox item in this.Controls.OfType<Guna2TextBox>())
                {
                    if (item.Tag == "Require" && item.Text == "")
                    {
                        MessageBox.Show("กรุณากรอกข้อมูลที่จำเป็น", "ข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                }
                Log.Information("== save config");
                Log.Information("Host : " + txtHost.Text);
                Log.Information("Port : " + txtPort.Text);
                Log.Information("Db : " + txtDb.Text);
                Log.Information("User : " + txtUser.Text);
                Log.Information("Pass : " + txtPass.Text);
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings["HOST"].Value = txtHost.Text;
                config.AppSettings.Settings["PORT"].Value = txtPort.Text;
                config.AppSettings.Settings["DB"].Value = txtDb.Text;
                config.AppSettings.Settings["USER"].Value = txtUser.Text;
                config.AppSettings.Settings["PASS"].Value = txtPass.Text;
                config.Save(ConfigurationSaveMode.Modified);
                Log.Information("save success");
                MessageBox.Show("บันทึกรายการสำเร็จ กรุณาเปิดโปรแกรมใหม่อีกครั้ง", "บันทึกรายการ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดผลาดในการบันทึก\n Error : {ex.Message}", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Log.Error("frmDbConfig,btnPrint_click : " + ex.Message);
                return;
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDbConfig_Load(object sender, EventArgs e)
        {
            getParameterFormAppConfig();
        }
    }
}
