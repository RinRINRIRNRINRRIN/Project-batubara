
using ServerSide.Dbcontent;
using ServerSide.Models;
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

namespace ServerSide.Pages_Server_Site
{
    public partial class frmVerify : Form
    {
        public frmVerify(AccountManagementModel accountManagementModel)
        {
            InitializeComponent();
            this._account = accountManagementModel;
            dgv.DefaultCellStyle.ForeColor = Color.Black;
        }

        private readonly AccountManagementModel _account;

        void getOrderVerify()
        {
            OrderManagementDb orderManagementDb = new OrderManagementDb();
            List<OrderManageModel> lists = orderManagementDb.getOrderVerifyIsNotSuccess();
            dgv.Rows.Clear();
            guna2HtmlLabel1.Text = $"<a> ผลลัพท์ </a> <a style = 'color:red'> 0 </a> <a> รายการ </a>";
            if (lists != null)
            {
                if (lists.Count == 0)
                {
                    lblNotFoundVerify.Visible = true;
                    return;
                }
                else // defin order into dgv
                {             
                    if (lists != null)
                        foreach (var item in lists)
                        {
                            dgv.Rows.Add("", item.Id, item.OrderNumber, item.WeightType, item.JobNumber, item.ProductNamez, item.RawMatName, item.CustomerName, item.TransportName, item.LIcensePlate, item.StartStationName, item.EndStationName, item.StartStationType, item.EndStationType, item.PoBuy, item.PoSale, item.DriverName, item.Status, item.ReferenceNumber);
                        }
                    guna2HtmlLabel1.Text = $"<a> ผลลัพท์ </a> <a style = 'color:red'> {lists.Count} </a> <a> รายการ </a>";
                    guna2HtmlLabel1.Font = new Font("Athiti", 12, FontStyle.Regular);
                }
            }
        }

        private void frmVerify_Load(object sender, EventArgs e)
        {
            getOrderVerify();
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string button = dgv.Columns[e.ColumnIndex].Name;
                string OrderNumber = dgv.Rows[e.RowIndex].Cells["cl_ordernumber"].Value.ToString();
                string JobNumber = dgv.Rows[e.RowIndex].Cells["cl_jobnumber"].Value.ToString();
                int Id = int.Parse(dgv.Rows[e.RowIndex].Cells["cl_id"].Value.ToString());
                frmOrderDetail frmOrderDetail = new frmOrderDetail(Id, _account);
                this.Hide();
                frmOrderDetail.ShowDialog();
                this.Show();
                getOrderVerify();
            }
            catch
            {


            }

        }

        private void lblNotFoundVerify_Click(object sender, EventArgs e)
        {

        }
    }
}
