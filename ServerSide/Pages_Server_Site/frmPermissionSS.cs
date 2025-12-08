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
    public partial class frmPermissionSS : Form
    {
        public frmPermissionSS(AccountManagementModel model)
        {
            InitializeComponent();
            _model = model;
        }


        private readonly AccountManagementModel _model;
        private List<PermissionModel> permissiosServerSite = new List<PermissionModel>();
        private List<PermissionModel> permissionClientSite = new List<PermissionModel>();
        private void frmPermissionSS_Load(object sender, EventArgs e)
        {
            if (_model.Position == "User")
                pnMenuSS.Enabled = false;

            lblFullName.Text = _model.FullName;
            lblPosition.Text = _model.Position;

            // load current permission
            GetCurrentPermission();
        }

        /// <summary>
        /// Get current permission 
        /// </summary>
        void GetCurrentPermission()
        {
            PermissionDb permissionDb = new PermissionDb();
            List<PermissionModel> permissionModels = permissionDb.GetAllPermissionByAccountId(_model.Id);
            if (permissionModels != null)
                foreach (var item in permissionModels)
                {
                    switch (item.MenuType)
                    {
                        case "Server Site":
                            foreach (Guna2CheckBox cb in pnMenuSS.Controls.OfType<Guna2CheckBox>())
                            {
                                if (cb.Tag.ToString() == item.Menuname)
                                    cb.Checked = true;
                            }
                            break;
                        case "Client Site":
                            foreach (Guna2CheckBox cb in pnMenuCS.Controls.OfType<Guna2CheckBox>())
                            {
                                if (cb.Tag.ToString() == item.Menuname)
                                    cb.Checked = true;
                            }
                            break;
                    }
                }
        }

        /// <summary>
        /// define new permission in List<PermisssionModel></PermisssionModel>
        /// </summary>
        void DefinePermission()
        {
            // define parameter
            permissiosServerSite.Clear();
            foreach (Guna2CheckBox cb in pnMenuSS.Controls.OfType<Guna2CheckBox>())
            {
                if (cb.Checked)
                {
                    permissiosServerSite.Add(new PermissionModel
                    {
                        AccountId = _model.Id,
                        Menuname = cb.Tag.ToString(),
                        MenuType = "Server Site"
                    });
                }
            }
            permissionClientSite.Clear();
            foreach (Guna2CheckBox cb in pnMenuCS.Controls.OfType<Guna2CheckBox>())
            {
                if (cb.Checked)
                {
                    permissionClientSite.Add(new PermissionModel
                    {
                        AccountId = _model.Id,
                        Menuname = cb.Tag.ToString(),
                        MenuType = "Client Site"
                    });
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool isCheck = false;
            // Check value is empty
            foreach (Guna2CheckBox cb in pnMenuSS.Controls.OfType<Guna2CheckBox>())
            {
                if (cb.Checked)
                    isCheck = true;
            }

            foreach (Guna2CheckBox cb in pnMenuCS.Controls.OfType<Guna2CheckBox>())
            {
                if (cb.Checked)
                    isCheck = true;
            }

            if (!isCheck)
            {
                msg.Icon = MessageDialogIcon.Warning;
                msg.Buttons = MessageDialogButtons.OK;
                msg.Show("Please select a menu before save", "Select menu");
                return;
            }

            // defin new permission
            DefinePermission();

            // delete old permission
            PermissionDb permissionDb = new PermissionDb();
            if (permissionDb.Delete(_model.Id))
            {
                bool isSuccess = false;
                // if success and then add new permission menu
                if (permissionClientSite.Count > 0)
                {
                    if (permissionDb.AddNew(permissionClientSite))
                        isSuccess = true;
                }

                if (permissiosServerSite.Count > 0)
                {
                    if (permissionDb.AddNew(permissiosServerSite))
                        isSuccess = true;
                }

                if (isSuccess)
                {
                    msg.Icon = MessageDialogIcon.Information;
                    msg.Buttons = MessageDialogButtons.OK;
                    msg.Show("Define permission success", "Successfully");
                    this.Close();
                }
            }
        }
    }
}
