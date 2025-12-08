using ServerSide.Dbcontent;
using ServerSide.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ServerSide.Pages_Public
{
    public partial class frmOrderDetail : Form
    {
        public frmOrderDetail(int _id, AccountManagementModel _account)
        {
            InitializeComponent();
            this.id = _id;
            accountManagementModel = _account;
        }

        private readonly int id;
        private readonly AccountManagementModel accountManagementModel;


        void getOrderById()
        {
            OrderManagementDb orderManagementDb = new OrderManagementDb();
            OrderManageModel model = orderManagementDb.GetOrderById(id);
            if (model != null)
                setParameter(model);
        }

        void setParameter(OrderManageModel model)
        {
            lblProductName.Text = model.ProductNamez;
            lblCustomerName.Text = model.CustomerName;
            lblWeightType.Text = model.WeightType;
            lblStartStationName.Text = model.StartStationName;
            lblStartStationType.Text = model.StartStationType;
            lblEndStationName.Text = model.EndStationName;
            lblEndStationType.Text = model.EndStationType;
            lblPoSale.Text = model.PoSale;
            lblPoBuy.Text = model.PoBuy;
            lblTransportSup.Text = model.TransportName;
            //        lblSupplier.Text = model.SuppireName;
            lblDriver.Text = model.DriverName;
            lblWeightStatus.Text = model.Status;
            lblVerifyStatus.Text = model.VerifyStatus;
            lblOrderNumber.Text = model.OrderNumber;
            lblJobNumber.Text = model.JobNumber;
            lblLicensePlate.Text = model.LIcensePlate;
            lblRawMat.Text = model.RawMatName;
            lblEmployeeVerify.Text = model.EmplpoyeeVerify;
            switch (model.VerifyStatus)
            {
                case "Wait":
                    lblVerifyStatus.ForeColor = Color.Goldenrod;
                    btnVerify.Enabled = true;
                    break;
                case "Approve":
                    lblVerifyStatus.ForeColor = Color.DarkGreen;
                    break;
                case "Not Check":
                    lblVerifyStatus.ForeColor = Color.Red;
                    break;
            }
            switch (model.Status)
            {
                case "Success":
                    lblWeightStatus.ForeColor = Color.Green;
                    break;
                case "Cancel":
                    lblWeightStatus.ForeColor = Color.Red;
                    break;
                case "Planning":
                    lblWeightStatus.ForeColor = Color.Goldenrod;
                    break;
                case "Process":
                    lblWeightStatus.ForeColor = Color.Blue;
                    break;
            }
            //if (model.StartStationType == "INSIDE" || model.EndStationType == "INSIDE")
            //    btnVerify.Enabled = false;
            // OUTSIDE
            lblOutsideFirstWeight.Text = model.OutSideFirstWeight.ToString("#,###");
            lblOutsideSecondWeight.Text = model.OutSideSecondWeight.ToString("#,###");
            lblOutsideNetWeight.Text = model.OutSideNetWeight.ToString("#,###");

            // INSIDE
            lblInsideNetWeight.Text = model.NetWeight.ToString("#,###");
            WeigthingDetailDb weigthingDetailDb = new WeigthingDetailDb();
            List<WeightDetailModel> models = weigthingDetailDb.GetDetailByOrderId(id);
            foreach (var item in models)
            {
                switch (item.WeightType)
                {
                    case "FIRST WEIGHT":
                        lblInsideFirstWeight.Text = item.NetWeight.ToString("#,###");
                        break;
                    case "SECOND WEIGHT":
                        lblInsideSecondWeight.Text = item.NetWeight.ToString("#,###");
                        break;
                }
            }
            // Calculator Diff Weight
            int netDiff = 0;
            if (model.NetWeight > model.OutSideNetWeight)
                netDiff = model.NetWeight - model.OutSideNetWeight;
            if (model.OutSideNetWeight > model.NetWeight)
                netDiff = model.OutSideNetWeight - model.NetWeight;

            lblDiffWeight.Text = netDiff.ToString("#,###");
        }

        private void frmOrderDetail_Load(object sender, EventArgs e)
        {
            getOrderById();
        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("คุณต้องการยืนยันการตรวจสอบหรือไม่ \n" +
                "น้ำหนักสุทธิที่แท่นภายใน : " + lblInsideNetWeight.Text + "\n" +
                "น้ำหนักสุทธิที่แท่นภายนอก : " + lblOutsideNetWeight.Text + "\n" +
                "น้ำหนักต่าง : " + lblDiffWeight.Text, "ยืนยันการตรวจสอบ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                // อัพเดทสถานะ verifystatus
                OrderManagementDb orderManagementDb = new OrderManagementDb();
                if (!orderManagementDb.UpdateVerifyStatus(id, "Approve", accountManagementModel.FullName))
                {
                    MessageBox.Show("เกิดข้อผิดผลาด ยืนยันการตรวจสอบ \n" +
                        "Error : " + orderManagementDb.ERR, "เกิข้อผิดผลาด", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                MessageBox.Show("ยืนยันการตรวจสอบสำเร็จ", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                return;
            }
        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
