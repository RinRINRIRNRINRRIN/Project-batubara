using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;
using System.Windows.Forms;

namespace ServerSide.Pages_Public
{
    public partial class frmDialog : Form
    {
        public frmDialog(string title, string message)
        {
            InitializeComponent();
            Text = title;
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.None;
            guna2BorderlessForm1.AnimationType = Guna2BorderlessForm.AnimateWindowType.AW_BLEND;
            guna2BorderlessForm1.BorderRadius = 10;
            guna2BorderlessForm1.AnimationInterval = 100;            
            MaximizeBox = false;
            MinimizeBox = false;

            Size = new Size(380, 200);

            var panel = new Guna2Panel { Dock = DockStyle.Fill, Padding = new Padding(20) };
            Controls.Add(panel);

            var lbl = new Label
            {
                Text = message,
                Dock = DockStyle.Fill,
                Font = new Font("Athiti", 14, FontStyle.Regular, GraphicsUnit.Point),
                AutoSize = false
            };
            panel.Controls.Add(lbl);

            var buttonsPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                FlowDirection = FlowDirection.RightToLeft,
                Height = 48,
                Padding = new Padding(0, 10, 0, 0)
            };
            panel.Controls.Add(buttonsPanel);

            var btnOk = new Guna2Button
            {
                Text = "ตกลง",
                Font = new Font("Athiti", 12, FontStyle.Bold),
                Width = 100,
                DialogResult = DialogResult.OK, // ถ้าคุณใช้ได้จากคอนโทรลนี้ก็พอ
                Animated = true
            };
            btnOk.Click += (s, e) => { this.DialogResult = DialogResult.OK; Close(); };
            buttonsPanel.Controls.Add(btnOk);

            var btnCancel = new Guna2Button
            {
                Text = "ยกเลิก",
                Font = new Font("Athiti", 12),
                Width = 100,
                Animated = true
            };
            btnCancel.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; Close(); };
            buttonsPanel.Controls.Add(btnCancel);

            // ให้กด Enter = ตกลง, Esc = ยกเลิก
            AcceptButton = btnOk;
            CancelButton = btnCancel;
        }
    }
}
