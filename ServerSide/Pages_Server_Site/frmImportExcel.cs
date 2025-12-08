using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
using Serilog;
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
using System.Windows.Forms.VisualStyles;
namespace ServerSide.Pages
{
    public partial class frmImportExcel : Form
    {
        public frmImportExcel(AccountManagementModel model)
        {
            InitializeComponent();

            dgv.RowTemplate.Height = 40;
            dgv.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgv.DefaultCellStyle.Font = new Font("Athiti", 12);
            dgv.DefaultCellStyle.ForeColor = Color.Black;

            dgv.Columns[0].Width = 200;
            dgv.Columns[1].Width = 150;
            dgv.Columns[2].Width = 150;
            dgv.Columns[3].Width = 150;
            dgv.Columns[4].Width = 150;
            dgv.Columns[5].Width = 200;
            dgv.Columns[6].Width = 230;
            dgv.Columns[7].Width = 150;
            dgv.Columns[8].Width = 250;
            dgv.Columns[9].Width = 165;
            dgv.Columns[10].Width = 250;
            dgv.Columns[11].Width = 165;
            dgv.Columns[12].Width = 200;
            dgv.Columns[13].Width = 150;
            dgv.Columns[14].Width = 150;

            _employeeModel = model;
        }


        private readonly AccountManagementModel _employeeModel;


        void AddNewJobNumber()
        {

            // start import 
            ImportExcelModel model = new ImportExcelModel();
            int numSuccess = 0;
            int numError = 0;
            OrderManagementDb orderManagementDb = new OrderManagementDb();
            foreach (DataGridViewRow rw in dgv.Rows)
            {
                model.JobNumber = rw.Cells[0].Value.ToString();
                model.WeightType = rw.Cells[1].Value.ToString();
                model.PoBuy = rw.Cells[2].Value.ToString();
                model.PoSale = rw.Cells[3].Value.ToString();
                model.SuppireName = rw.Cells[4].Value.ToString();
                model.CustomerName = rw.Cells[5].Value.ToString();
                model.Productname = rw.Cells[6].Value.ToString();
                model.RawMatName = rw.Cells[7].Value.ToString();
                model.StartStationName = rw.Cells[8].Value.ToString();
                model.StartStationType = rw.Cells[9].Value.ToString();
                model.EndStationName = rw.Cells[10].Value.ToString();
                model.EndStationType = rw.Cells[11].Value.ToString();
                model.TransportName = rw.Cells[12].Value.ToString();
                model.LIcensePlate = rw.Cells[13].Value.ToString();
                model.DriverName = rw.Cells[14].Value.ToString();
                model.EmployeeCreate = _employeeModel.FullName;

                if (orderManagementDb.AddNewOrderNumberForPlanning(model))
                {
                    numSuccess++;
                }
                else
                    numError++;
            }

            if (numError > 0)
            {
                msg.Icon = Guna.UI2.WinForms.MessageDialogIcon.Warning;
                msg.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                msg.Show("Import the excel success and error\n" +
                   $"Total complate : {numSuccess}\n" +
                   $"Total error : {numError} \n\n" +
                   $"Error : " + orderManagementDb.ERR, "Import a excel file");
            }
            else
            {
                msg.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                msg.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                msg.Show("Import the excel successfully\n" +
       $"Total complate : {numSuccess}\n" +
       $"Total error : {numError}", "Import a excel file");
            }

            dgv.Rows.Clear();
        }

        /// <summary>
        /// สำหรับเก็บวันที่
        /// </summary>
        private List<string> dateCreate { get; set; } = new List<string>();

        private void frmImportExcel_Load(object sender, EventArgs e)
        {
            int x = (this.Width - pnLoad.Width) / 2;
            int y = (this.Height - pnLoad.Height) / 2;
            pnLoad.Location = new Point(x, y);
            pnLoad.Visible = false;
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (dgv.Rows.Count == 0)
            {
                msg.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                msg.Icon = Guna.UI2.WinForms.MessageDialogIcon.Warning;
                msg.Show("Please import a excel file", "Import excel file");
                return;
            }
            else
            {
                // เช็คก่อนว่ามี order ซ้ำอยู่หรือไม่
                OrderManagementDb order = new OrderManagementDb();
                string datez = DateTime.Now.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.CreateSpecificCulture("en-EN"));
                List<OrderManageModel> list = order.GetOrderDate(datez + " 00:00:00", datez + " 23:59:59", "All");
                if (list != null && list.Count > 0) // มีข้อมูลของวันที่นี้อยู่
                {
                    msg.Icon = Guna.UI2.WinForms.MessageDialogIcon.Warning;
                    msg.Buttons = Guna.UI2.WinForms.MessageDialogButtons.YesNo;
                    DialogResult rs = msg.Show($"Has already order of {datez} must be first old orders", "Order is already");
                    if (rs == DialogResult.Yes)
                    {
                        AddNewJobNumber();
                    }
                }
                else
                {
                    AddNewJobNumber();
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (dgv.Rows.Count == 0)
            {
                dgv.Rows.Add();
            }
            else
            {
                bool isNull = true;
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    object cell = dgv.Rows[dgv.Rows.Count - 1].Cells[i].Value;
                    if (cell != null)
                    {
                        isNull = false;
                    }
                }
                if (isNull)
                {
                    MessageBox.Show("เพิ่มรายการไว้อยู่แล้ว กรูณากรอกข้อมูลให้ครบ");
                    return;
                }
                dgv.Rows.Add();
            }
        }

        void ShowLoader(bool isOpen)
        {
            switch (isOpen)
            {
                case true:
                    BeginInvoke(new MethodInvoker(delegate ()
                    {
                        pnInformation.Visible = false;
                        pnLoad.Visible = true;
                    }));
                    break;
                case false:
                    BeginInvoke(new MethodInvoker(delegate ()
                    {
                        pnInformation.Visible = true;
                        pnLoad.Visible = false;
                    }));
                    break;
            }
        }

        async Task LoadFileFromExcel()
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "xlsx |* xlsx";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {

                    Log.Information("load excel");
                    Log.Information("file name : " + openFileDialog.FileName);
                    txtFileName.Text = $"File name : {openFileDialog.FileName}";
                    ShowLoader(true);
                    await Task.Delay(1000);
                    await Task.Run(async () =>
                    {
                        int countBuy = 0, countSele = 0, countMix = 0;
                        XLWorkbook excel = new XLWorkbook(openFileDialog.FileName);
                        var sheet = excel.Worksheet(1);
                        foreach (var row in sheet.RowsUsed())
                        {
                            if (row.RowNumber() != 1)
                            {
                                List<string> data = new List<string>();
                                foreach (var cell in row.Cells())
                                {

                                    if (cell.DataType == XLDataType.Text)
                                    {
                                        data.Add(cell.Value.GetText());
                                        Console.WriteLine(cell.Value.ToString());
                                    }
                                    if (cell.DataType == XLDataType.Number)
                                    {
                                        data.Add(cell.GetString());
                                        Console.WriteLine(cell.Value.ToString());
                                    }
                                }

                                if (data.Count == 16)
                                {
                                    Log.Information($"add new data : {data[0]},{data[1]}, {data[2]}, {data[3]}, {data[4]}, {data[5]}, {data[6]}, {data[7]}, {data[8]},{data[9]}, {data[10]}, {data[11]}, {data[12]},{data[13]},{data[14]},{data[15]}");
                                    // เช็คว่ามีวันที่ซ้ำกันหรือไม่
                                    if (!dateCreate.Contains(data[15]))
                                    {
                                        dateCreate.Add(data[15]);
                                    }

                                    BeginInvoke(new MethodInvoker(delegate ()
                                    {
                                        dgv.Rows.Add(data[0], data[1], data[2], data[3], data[4], data[5], data[6], data[7], data[8], data[9], data[10], data[11], data[12], data[13], data[14], data[15]);
                                    }));
                                }

                                switch (data[1])
                                {
                                    case "BUY":
                                        countBuy++;
                                        break;
                                    case "SALE":
                                        countSele++;
                                        break;
                                    case "MIX":
                                        countMix++;
                                        break;
                                }
                                BeginInvoke(new MethodInvoker(delegate ()
                                {
                                    lblCountBuy.Text = $"{countBuy} รายการ";
                                    lblCountSale.Text = $"{countSele} รายการ";
                                    lblCountMove.Text = $"{countMix} รายการ";
                                }));
                            }
                            await Task.Delay(50);
                        }
                        //    await  FormatDataGridUI();
                        ShowLoader(false);
                    });
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดผลาด \nERROR : " + ex.Message, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ShowLoader(false);
                return;
            }
        }


        private async void SelectExcelFile(object sender, EventArgs e)
        {
            // check data in datagrid
            if (dgv.Rows.Count > 0)
            {
                DialogResult dialogResult = MessageBox.Show("มีข้อมูลที่ดึงเอาไว้แล้วหากดึงข้อมูลใหม่ ข้อมูลในตารางด้านล้างจะถูกลบ ยืนยันหรือไม่", "เรียกข้อมูลใหม่", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    dgv.Rows.Clear();
                    await LoadFileFromExcel();
                }
            }
            else
            {
                await LoadFileFromExcel();
            }
        }

        private void txtFileName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
