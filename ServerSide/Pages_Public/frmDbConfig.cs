using Guna.UI2.WinForms;
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

                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings["HOST"].Value = txtHost.Text;
                config.AppSettings.Settings["PORT"].Value = txtHost.Text;
                config.AppSettings.Settings["DB"].Value = txtHost.Text;
                config.AppSettings.Settings["USER"].Value = txtHost.Text;
                config.AppSettings.Settings["PASS"].Value = txtHost.Text;
                config.Save(ConfigurationSaveMode.Modified);
                MessageBox.Show("บันทึกรายการสำเร็จ กรุณาเปิดโปรแกรมใหม่อีกครั้ง", "บันทึกรายการ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดผลาดในการบันทึก\n Error : {ex.Message}", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
