using Guna.UI2.WinForms;
using ServerSide.Dbcontent;
using ServerSide.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServerSide.Pages_Server_Site
{
    public partial class frmAccountManagementSS : Form
    {
        public frmAccountManagementSS()
        {
            InitializeComponent();

            int x = (this.Width - gbInformation.Width) / 2;
            int y = (this.Height - gbInformation.Height) / 2;
            gbInformation.Location = new Point(x, y);
        }

        void ClearInformationUI()
        {
            foreach (Guna2TextBox txt in gbInformation.Controls.OfType<Guna2TextBox>())
                txt.Clear();
            cbbPosition.Items.Clear();
            tgsStatus.Checked = false;
            lblId.Text = "0";
        }

        void ShowInformation(bool isShow)
        {
            switch (isShow)
            {
                case true:
                    gbInformation.Visible = true;
                    dgv.Visible = false;
                    txtSearch.Visible = false;
                    btnAdd.Visible = false;
                    break;
                case false:
                    gbInformation.Visible = false;
                    dgv.Visible = true;
                    txtSearch.Visible = true;
                    btnAdd.Visible = true;
                    break;
            }
        }

        void GetAccout()
        {
            AccountManagementDb accountManagementDb = new AccountManagementDb();
            List<AccountManagementModel> list = accountManagementDb.GetAllAccount();
            if (list != null)
            {
                dgv.Rows.Clear();
                foreach (AccountManagementModel model in list)
                    dgv.Rows.Add(null, null, null, model.Id, model.Username, model.Password, model.FullName, model.Status, model.Position);

                // formatUI
                foreach (DataGridViewRow rw in dgv.Rows)
                    switch (rw.Cells["cl_status"].Value.ToString())
                    {
                        case "Active":
                            rw.DefaultCellStyle.ForeColor = Color.Green;
                            break;
                        case "Inactive":
                            rw.DefaultCellStyle.ForeColor = Color.Red;
                            break;
                    }
            }
        }

        private void frmAccountManagementSS_Load(object sender, EventArgs e)
        {
            GetAccout();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ShowInformation(true);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            bool isHaveValue = false;
            foreach (Guna2TextBox txt in gbInformation.Controls.OfType<Guna2TextBox>())
                if (txt.Text != "")
                {
                    isHaveValue = true;
                    break;
                }

            if (cbbPosition.Text != "")
                isHaveValue = true;

            if (tgsStatus.Checked)
                isHaveValue = true;


            if (isHaveValue)
            {
                msg.Buttons = MessageDialogButtons.YesNo;
                msg.Icon = MessageDialogIcon.Question;
                DialogResult dir = msg.Show("Do you want to cancel the information?", "Cancel");
                if (dir == DialogResult.Yes)
                {
                    ClearInformationUI();
                }
                else
                {
                    return; // ถ้าไม่ยืนยันยกเลิก ก็ไม่ต้องทำอะไรต่อ
                }
            }

            // ซ่อนฟอร์มกรอกข้อมูล และแสดงส่วนอื่น
            ShowInformation(false);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Checkค่าว่าง
            foreach (Guna2TextBox txt in gbInformation.Controls.OfType<Guna2TextBox>())
                if (txt.Text == "")
                {
                    msg.Icon = MessageDialogIcon.Warning;
                    msg.Buttons = MessageDialogButtons.OK;
                    msg.Show("Please fill in textbox before save", "Fill in textbox");
                    return;
                }

            if (cbbPosition.Text == "")
            {
                msg.Icon = MessageDialogIcon.Warning;
                msg.Buttons = MessageDialogButtons.OK;
                msg.Show("Please select position", "Position");
                return;
            }

            if (!tgsStatus.Checked)
            {
                msg.Icon = MessageDialogIcon.Question;
                msg.Buttons = MessageDialogButtons.YesNo;
                DialogResult dir = msg.Show("The status is inactive do you want to continus?", "Status is not active");
                if (dir == DialogResult.No)
                    return;
            }
            #region Define parameter in model
            string _status = "";
            switch (tgsStatus.Checked)
            {
                case true:
                    _status = "Active";
                    break;
                case false:
                    _status = "Inactive";
                    break;
            }

            int id = int.Parse(lblId.Text);
            AccountManagementModel model = new AccountManagementModel
            {
                Id = id,
                Username = txtUsername.Text,
                Password = txtPassword.Text,
                FullName = txtFullName.Text,
                Status = _status,
                Position = cbbPosition.Text
            };
            #endregion

            // check insert or update
            if (id == 0) // INSERT 
            {
                AccountManagementDb accountManagementDb = new AccountManagementDb();
                if (accountManagementDb.AddNew(model))
                {
                    msg.Icon = MessageDialogIcon.Information;
                    msg.Buttons = MessageDialogButtons.OK;
                    msg.Show("Add new\n" +
                        $"Username : {txtUsername.Text}\n" +
                        $"Password : {txtPassword.Text}\n" +
                        $"FullName : {txtFullName.Text}\n" +
                        $"Successfully", "Success");
                    ClearInformationUI();
                    ShowInformation(false);
                    GetAccout();
                }
                else
                {
                    msg.Icon = MessageDialogIcon.Error;
                    msg.Buttons = MessageDialogButtons.OK;
                    msg.Show("Invalid for add new account\n" +
                        "1.Please check the username is not duplicate\n" +
                        $"\nERROR : {accountManagementDb.ERR}", "Add new account error");
                    return;
                }
            }
            else // UPDATE
            {
                AccountManagementDb accountManagementDb = new AccountManagementDb();
                if (accountManagementDb.UpdateAccount(model))
                {
                    msg.Icon = MessageDialogIcon.Information;
                    msg.Buttons = MessageDialogButtons.OK;
                    msg.Show("Update account\n" +
                        $"FullName : {txtFullName.Text}\n" +
                        $"Successfully", "Success");
                }
                else
                {
                    msg.Icon = MessageDialogIcon.Error;
                    msg.Buttons = MessageDialogButtons.OK;
                    msg.Show("Invalid for update  account\n" +
                        "1.Please check the username is not duplicate\n" +
                        $"\nERROR : {accountManagementDb.ERR}", "Add new account error");
                    return;
                }
            }
            // Clear ui
            ClearInformationUI();
            ShowInformation(false);
            GetAccout();
        }

        private void cbbPosition_DropDown(object sender, EventArgs e)
        {
            cbbPosition.Items.Clear();
            cbbPosition.Items.Add("User");
            cbbPosition.Items.Add("Admin");
            cbbPosition.Items.Add("Super");
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string clName = dgv.Columns[e.ColumnIndex].Name;
            int _id = int.Parse(dgv.Rows[e.RowIndex].Cells["cl_id"].Value.ToString());
            string _user = dgv.Rows[e.RowIndex].Cells["cl_user"].Value.ToString();
            string _pass = dgv.Rows[e.RowIndex].Cells["cl_pass"].Value.ToString();
            string _fulname = dgv.Rows[e.RowIndex].Cells["cl_fullname"].Value.ToString();
            string _status = dgv.Rows[e.RowIndex].Cells["cl_status"].Value.ToString();
            string _position = dgv.Rows[e.RowIndex].Cells["cl_position"].Value.ToString();
            switch (clName)
            {
                case "cl_delete":
                    msg.Buttons = MessageDialogButtons.YesNo;
                    msg.Icon = MessageDialogIcon.Question;
                    DialogResult dir = msg.Show("Do you want to delete\n" +
                        $"Username : {_user}\n" +
                        $"Password : {_pass}\n" +
                        $"FullName : {_fulname}", "Delete");
                    if (dir == DialogResult.Yes)
                    {
                        AccountManagementDb db = new AccountManagementDb();
                        if (db.Delete(_id))
                        {
                            msg.Icon = MessageDialogIcon.Information;
                            msg.Buttons = MessageDialogButtons.OK;
                            msg.Show("Delete success", "Success");
                            GetAccout();
                        }
                    }
                    break;
                case "cl_edit":
                    txtUsername.Text = _user;
                    txtPassword.Text = _pass;
                    txtFullName.Text = _fulname;
                    lblId.Text = _id.ToString();
                    switch (_status)
                    {
                        case "Active":
                            tgsStatus.Checked = true;
                            break;
                        case "Inactive":
                            tgsStatus.Checked = false;
                            break;
                    }
                    cbbPosition.Items.Add(_position);
                    cbbPosition.SelectedIndex = 0;
                    ShowInformation(true);
                    break;
                case "cl_permission":
                    AccountManagementModel model = new AccountManagementModel
                    {
                        Id = _id,
                        Username = _user,
                        FullName = _fulname,
                        Position = _position
                    };
                    frmPermissionSS frmPermissionSS = new frmPermissionSS(model);
                    this.Hide();
                    frmPermissionSS.ShowDialog();
                    this.Show();
                    break;
            }
        }

        private void tgsStatus_CheckedChanged(object sender, EventArgs e)
        {
            if (tgsStatus.Checked)
                lblStatus.Text = "Status : Active";
            else
                lblStatus.Text = "Status : Inactive";
        }
    }
}
