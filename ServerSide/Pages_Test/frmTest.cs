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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace ServerSide.Pages_Test
{
    public partial class frmTest : Form
    {
        public frmTest()
        {
            InitializeComponent();
        }

        private void frmTest_Load(object sender, EventArgs e)
        {
            using (var dlg = new frmDialog("ต้องการบันทึกข้อมูลหรือไม่?", "ยืนยัน"))
            {
                var result = dlg.ShowDialog(this); // หรือ dlg.ShowDialog();

                if (result == DialogResult.OK)
                {
                    // ผู้ใช้กด “ตกลง”
                    // ... เขียนโค้ดดำเนินการต่อที่นี่ ...
                }
                else
                {
                    // ผู้ใช้กด “ยกเลิก” หรือปิดหน้าต่าง
                }
            }
        }
    }
}
