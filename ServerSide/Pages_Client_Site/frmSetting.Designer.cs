namespace ServerSide.Pages_Client_Site
{
    partial class frmSetting
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetting));
            this.guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.msg = new Guna.UI2.WinForms.Guna2MessageDialog();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            this.txtPrinterThermal = new Guna.UI2.WinForms.Guna2TextBox();
            this.pnScale = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.guna2Panel4 = new Guna.UI2.WinForms.Guna2Panel();
            this.cbbScaleName = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.cbbPort = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cbbBaudrate = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cbbDatabit = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbbStopbit = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cbbParity = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnScaleConnect = new Guna.UI2.WinForms.Guna2Button();
            this.txtEndpoint = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtWeight = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Panel5 = new Guna.UI2.WinForms.Guna2Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.guna2Panel3 = new Guna.UI2.WinForms.Guna2Panel();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            this.guna2ControlBox1 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.serialPort2 = new System.IO.Ports.SerialPort(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.guna2Panel2.SuspendLayout();
            this.pnScale.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2BorderlessForm1
            // 
            this.guna2BorderlessForm1.ContainerControl = this;
            this.guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2BorderlessForm1.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // msg
            // 
            this.msg.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
            this.msg.Caption = null;
            this.msg.Icon = Guna.UI2.WinForms.MessageDialogIcon.None;
            this.msg.Parent = null;
            this.msg.Style = Guna.UI2.WinForms.MessageDialogStyle.Light;
            this.msg.Text = null;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.White;
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(8, 8);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(72, 69);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 63;
            this.pictureBox3.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Athiti SemiBold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(29)))), ((int)(((byte)(67)))));
            this.label1.Location = new System.Drawing.Point(86, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 39);
            this.label1.TabIndex = 64;
            this.label1.Text = "Setting";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(88, 49);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(238, 25);
            this.label15.TabIndex = 65;
            this.label15.Text = "กำหนดการเชื่อมต่อเครื่องชั่ง หรือ APIs";
            // 
            // guna2Panel2
            // 
            this.guna2Panel2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.guna2Panel2.BorderRadius = 6;
            this.guna2Panel2.BorderThickness = 1;
            this.guna2Panel2.Controls.Add(this.txtPrinterThermal);
            this.guna2Panel2.Controls.Add(this.pnScale);
            this.guna2Panel2.Controls.Add(this.btnScaleConnect);
            this.guna2Panel2.Controls.Add(this.txtEndpoint);
            this.guna2Panel2.Controls.Add(this.txtWeight);
            this.guna2Panel2.Controls.Add(this.guna2Panel1);
            this.guna2Panel2.Controls.Add(this.guna2Panel5);
            this.guna2Panel2.Controls.Add(this.label2);
            this.guna2Panel2.Controls.Add(this.label6);
            this.guna2Panel2.Controls.Add(this.label10);
            this.guna2Panel2.Controls.Add(this.guna2Panel3);
            this.guna2Panel2.Location = new System.Drawing.Point(12, 83);
            this.guna2Panel2.Name = "guna2Panel2";
            this.guna2Panel2.Size = new System.Drawing.Size(913, 389);
            this.guna2Panel2.TabIndex = 73;
            // 
            // txtPrinterThermal
            // 
            this.txtPrinterThermal.Animated = true;
            this.txtPrinterThermal.BorderRadius = 4;
            this.txtPrinterThermal.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPrinterThermal.DefaultText = "";
            this.txtPrinterThermal.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtPrinterThermal.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtPrinterThermal.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtPrinterThermal.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtPrinterThermal.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtPrinterThermal.Font = new System.Drawing.Font("Athiti", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrinterThermal.ForeColor = System.Drawing.Color.Black;
            this.txtPrinterThermal.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtPrinterThermal.Location = new System.Drawing.Point(513, 169);
            this.txtPrinterThermal.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtPrinterThermal.Name = "txtPrinterThermal";
            this.txtPrinterThermal.PlaceholderForeColor = System.Drawing.Color.DarkGray;
            this.txtPrinterThermal.PlaceholderText = "ชื่อปริ้นเตอร์";
            this.txtPrinterThermal.SelectedText = "";
            this.txtPrinterThermal.Size = new System.Drawing.Size(372, 36);
            this.txtPrinterThermal.TabIndex = 97;
            // 
            // pnScale
            // 
            this.pnScale.Controls.Add(this.label5);
            this.pnScale.Controls.Add(this.label7);
            this.pnScale.Controls.Add(this.guna2Panel4);
            this.pnScale.Controls.Add(this.cbbScaleName);
            this.pnScale.Controls.Add(this.label8);
            this.pnScale.Controls.Add(this.label11);
            this.pnScale.Controls.Add(this.cbbPort);
            this.pnScale.Controls.Add(this.label9);
            this.pnScale.Controls.Add(this.cbbBaudrate);
            this.pnScale.Controls.Add(this.cbbDatabit);
            this.pnScale.Controls.Add(this.label3);
            this.pnScale.Controls.Add(this.cbbStopbit);
            this.pnScale.Controls.Add(this.cbbParity);
            this.pnScale.Controls.Add(this.label4);
            this.pnScale.Location = new System.Drawing.Point(31, 43);
            this.pnScale.Name = "pnScale";
            this.pnScale.Size = new System.Drawing.Size(454, 288);
            this.pnScale.TabIndex = 91;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.DimGray;
            this.label5.Location = new System.Drawing.Point(3, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 25);
            this.label5.TabIndex = 72;
            this.label5.Text = "COMPORT";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.DimGray;
            this.label7.Location = new System.Drawing.Point(9, 207);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 25);
            this.label7.TabIndex = 90;
            this.label7.Text = "เครื่องชั่ง";
            // 
            // guna2Panel4
            // 
            this.guna2Panel4.BackColor = System.Drawing.Color.White;
            this.guna2Panel4.BorderRadius = 6;
            this.guna2Panel4.FillColor = System.Drawing.Color.Red;
            this.guna2Panel4.Location = new System.Drawing.Point(210, 15);
            this.guna2Panel4.Name = "guna2Panel4";
            this.guna2Panel4.Size = new System.Drawing.Size(10, 10);
            this.guna2Panel4.TabIndex = 69;
            // 
            // cbbScaleName
            // 
            this.cbbScaleName.BackColor = System.Drawing.Color.Transparent;
            this.cbbScaleName.BorderRadius = 4;
            this.cbbScaleName.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbScaleName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbScaleName.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbbScaleName.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbbScaleName.Font = new System.Drawing.Font("Athiti", 12F);
            this.cbbScaleName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbbScaleName.ItemHeight = 30;
            this.cbbScaleName.Location = new System.Drawing.Point(8, 235);
            this.cbbScaleName.Name = "cbbScaleName";
            this.cbbScaleName.Size = new System.Drawing.Size(435, 36);
            this.cbbScaleName.TabIndex = 89;
            this.cbbScaleName.DropDown += new System.EventHandler(this.cbbScaleName_DropDown);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.DimGray;
            this.label8.Location = new System.Drawing.Point(224, 6);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(217, 25);
            this.label8.TabIndex = 80;
            this.label8.Text = "ระบบจะไม่แสดง Port ที่มีการ Pair";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.DimGray;
            this.label11.Location = new System.Drawing.Point(224, 70);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(66, 25);
            this.label11.TabIndex = 88;
            this.label11.Text = "Databits";
            this.label11.Click += new System.EventHandler(this.label11_Click);
            // 
            // cbbPort
            // 
            this.cbbPort.BackColor = System.Drawing.Color.Transparent;
            this.cbbPort.BorderRadius = 4;
            this.cbbPort.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbPort.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbbPort.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbbPort.Font = new System.Drawing.Font("Athiti", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbPort.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbbPort.ItemHeight = 30;
            this.cbbPort.Location = new System.Drawing.Point(8, 31);
            this.cbbPort.Name = "cbbPort";
            this.cbbPort.Size = new System.Drawing.Size(435, 36);
            this.cbbPort.TabIndex = 81;
            this.cbbPort.DropDown += new System.EventHandler(this.cbbPort_DropDown);
            this.cbbPort.SelectedIndexChanged += new System.EventHandler(this.cbbPort_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.DimGray;
            this.label9.Location = new System.Drawing.Point(226, 137);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 25);
            this.label9.TabIndex = 88;
            this.label9.Text = "Stopbits";
            // 
            // cbbBaudrate
            // 
            this.cbbBaudrate.BackColor = System.Drawing.Color.Transparent;
            this.cbbBaudrate.BorderRadius = 4;
            this.cbbBaudrate.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbBaudrate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbBaudrate.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbbBaudrate.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbbBaudrate.Font = new System.Drawing.Font("Athiti", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbBaudrate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbbBaudrate.ItemHeight = 30;
            this.cbbBaudrate.Location = new System.Drawing.Point(8, 98);
            this.cbbBaudrate.Name = "cbbBaudrate";
            this.cbbBaudrate.Size = new System.Drawing.Size(212, 36);
            this.cbbBaudrate.TabIndex = 83;
            this.cbbBaudrate.DropDown += new System.EventHandler(this.cbbBaudrate_DropDown);
            // 
            // cbbDatabit
            // 
            this.cbbDatabit.BackColor = System.Drawing.Color.Transparent;
            this.cbbDatabit.BorderRadius = 4;
            this.cbbDatabit.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbDatabit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbDatabit.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbbDatabit.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbbDatabit.Font = new System.Drawing.Font("Athiti", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbDatabit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbbDatabit.ItemHeight = 30;
            this.cbbDatabit.Location = new System.Drawing.Point(229, 98);
            this.cbbDatabit.Name = "cbbDatabit";
            this.cbbDatabit.Size = new System.Drawing.Size(212, 36);
            this.cbbDatabit.TabIndex = 87;
            this.cbbDatabit.DropDown += new System.EventHandler(this.cbbDatabit_DropDown);
            this.cbbDatabit.SelectedIndexChanged += new System.EventHandler(this.cbbDatabit_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.label3.Location = new System.Drawing.Point(3, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 25);
            this.label3.TabIndex = 84;
            this.label3.Text = "Baudrate";
            // 
            // cbbStopbit
            // 
            this.cbbStopbit.BackColor = System.Drawing.Color.Transparent;
            this.cbbStopbit.BorderRadius = 4;
            this.cbbStopbit.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbStopbit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbStopbit.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbbStopbit.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbbStopbit.Font = new System.Drawing.Font("Athiti", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbStopbit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbbStopbit.ItemHeight = 30;
            this.cbbStopbit.Location = new System.Drawing.Point(231, 165);
            this.cbbStopbit.Name = "cbbStopbit";
            this.cbbStopbit.Size = new System.Drawing.Size(212, 36);
            this.cbbStopbit.TabIndex = 87;
            this.cbbStopbit.DropDown += new System.EventHandler(this.cbbStopbit_DropDown);
            // 
            // cbbParity
            // 
            this.cbbParity.BackColor = System.Drawing.Color.Transparent;
            this.cbbParity.BorderRadius = 4;
            this.cbbParity.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbParity.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbbParity.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbbParity.Font = new System.Drawing.Font("Athiti", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbParity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbbParity.ItemHeight = 30;
            this.cbbParity.Location = new System.Drawing.Point(8, 165);
            this.cbbParity.Name = "cbbParity";
            this.cbbParity.Size = new System.Drawing.Size(212, 36);
            this.cbbParity.TabIndex = 85;
            this.cbbParity.DropDown += new System.EventHandler(this.cbbParity_DropDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.DimGray;
            this.label4.Location = new System.Drawing.Point(3, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 25);
            this.label4.TabIndex = 86;
            this.label4.Text = "Parity";
            // 
            // btnScaleConnect
            // 
            this.btnScaleConnect.Animated = true;
            this.btnScaleConnect.BorderRadius = 6;
            this.btnScaleConnect.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnScaleConnect.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnScaleConnect.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnScaleConnect.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnScaleConnect.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(30)))), ((int)(((byte)(62)))));
            this.btnScaleConnect.Font = new System.Drawing.Font("Athiti", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnScaleConnect.ForeColor = System.Drawing.Color.White;
            this.btnScaleConnect.Location = new System.Drawing.Point(330, 336);
            this.btnScaleConnect.Name = "btnScaleConnect";
            this.btnScaleConnect.Size = new System.Drawing.Size(143, 37);
            this.btnScaleConnect.TabIndex = 78;
            this.btnScaleConnect.Text = "เชื่อมต่อ";
            this.btnScaleConnect.Click += new System.EventHandler(this.btnScaleConnect_Click);
            // 
            // txtEndpoint
            // 
            this.txtEndpoint.Animated = true;
            this.txtEndpoint.BorderRadius = 4;
            this.txtEndpoint.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtEndpoint.DefaultText = "";
            this.txtEndpoint.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtEndpoint.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtEndpoint.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtEndpoint.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtEndpoint.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtEndpoint.Font = new System.Drawing.Font("Athiti", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEndpoint.ForeColor = System.Drawing.Color.Black;
            this.txtEndpoint.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtEndpoint.Location = new System.Drawing.Point(513, 49);
            this.txtEndpoint.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtEndpoint.Name = "txtEndpoint";
            this.txtEndpoint.PlaceholderForeColor = System.Drawing.Color.DarkGray;
            this.txtEndpoint.PlaceholderText = "https://";
            this.txtEndpoint.SelectedText = "";
            this.txtEndpoint.Size = new System.Drawing.Size(372, 36);
            this.txtEndpoint.TabIndex = 96;
            // 
            // txtWeight
            // 
            this.txtWeight.Animated = true;
            this.txtWeight.BorderRadius = 4;
            this.txtWeight.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtWeight.DefaultText = "";
            this.txtWeight.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtWeight.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtWeight.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtWeight.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtWeight.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtWeight.Font = new System.Drawing.Font("Athiti", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWeight.ForeColor = System.Drawing.Color.Black;
            this.txtWeight.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtWeight.Location = new System.Drawing.Point(40, 336);
            this.txtWeight.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtWeight.Name = "txtWeight";
            this.txtWeight.PlaceholderForeColor = System.Drawing.Color.DarkGray;
            this.txtWeight.PlaceholderText = "น้ำหนัก";
            this.txtWeight.ReadOnly = true;
            this.txtWeight.SelectedText = "";
            this.txtWeight.Size = new System.Drawing.Size(284, 36);
            this.txtWeight.TabIndex = 75;
            this.txtWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.White;
            this.guna2Panel1.BorderRadius = 6;
            this.guna2Panel1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(30)))), ((int)(((byte)(62)))));
            this.guna2Panel1.Location = new System.Drawing.Point(9, 11);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(10, 30);
            this.guna2Panel1.TabIndex = 68;
            // 
            // guna2Panel5
            // 
            this.guna2Panel5.BackColor = System.Drawing.Color.White;
            this.guna2Panel5.BorderRadius = 6;
            this.guna2Panel5.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(30)))), ((int)(((byte)(62)))));
            this.guna2Panel5.Location = new System.Drawing.Point(491, 124);
            this.guna2Panel5.Name = "guna2Panel5";
            this.guna2Panel5.Size = new System.Drawing.Size(10, 30);
            this.guna2Panel5.TabIndex = 94;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Athiti Medium", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(29)))), ((int)(((byte)(67)))));
            this.label2.Location = new System.Drawing.Point(25, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 31);
            this.label2.TabIndex = 69;
            this.label2.Text = "เครื่องชั่ง";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Athiti Medium", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(29)))), ((int)(((byte)(67)))));
            this.label6.Location = new System.Drawing.Point(507, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(139, 31);
            this.label6.TabIndex = 93;
            this.label6.Text = "Endpoint APIs";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Athiti Medium", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(29)))), ((int)(((byte)(67)))));
            this.label10.Location = new System.Drawing.Point(507, 123);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(155, 31);
            this.label10.TabIndex = 95;
            this.label10.Text = "Printer Thermal";
            // 
            // guna2Panel3
            // 
            this.guna2Panel3.BackColor = System.Drawing.Color.White;
            this.guna2Panel3.BorderRadius = 6;
            this.guna2Panel3.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(30)))), ((int)(((byte)(62)))));
            this.guna2Panel3.Location = new System.Drawing.Point(491, 11);
            this.guna2Panel3.Name = "guna2Panel3";
            this.guna2Panel3.Size = new System.Drawing.Size(10, 30);
            this.guna2Panel3.TabIndex = 92;
            // 
            // serialPort1
            // 
            this.serialPort1.ReadBufferSize = 1024;
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // guna2Button1
            // 
            this.guna2Button1.Animated = true;
            this.guna2Button1.BorderRadius = 6;
            this.guna2Button1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(30)))), ((int)(((byte)(62)))));
            this.guna2Button1.Font = new System.Drawing.Font("Athiti", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2Button1.ForeColor = System.Drawing.Color.White;
            this.guna2Button1.Location = new System.Drawing.Point(782, 478);
            this.guna2Button1.Name = "guna2Button1";
            this.guna2Button1.Size = new System.Drawing.Size(143, 37);
            this.guna2Button1.TabIndex = 98;
            this.guna2Button1.Text = "บันทึกการตั้งค่า";
            this.guna2Button1.Click += new System.EventHandler(this.guna2Button1_Click);
            // 
            // guna2ControlBox1
            // 
            this.guna2ControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ControlBox1.FillColor = System.Drawing.Color.White;
            this.guna2ControlBox1.IconColor = System.Drawing.Color.Blue;
            this.guna2ControlBox1.Location = new System.Drawing.Point(879, 8);
            this.guna2ControlBox1.Name = "guna2ControlBox1";
            this.guna2ControlBox1.Size = new System.Drawing.Size(45, 29);
            this.guna2ControlBox1.TabIndex = 99;
            this.guna2ControlBox1.Click += new System.EventHandler(this.guna2ControlBox1_Click);
            // 
            // serialPort2
            // 
            this.serialPort2.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort2_DataReceived);
            // 
            // frmSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(936, 523);
            this.Controls.Add(this.guna2ControlBox1);
            this.Controls.Add(this.guna2Button1);
            this.Controls.Add(this.guna2Panel2);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label15);
            this.Font = new System.Drawing.Font("Athiti", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Name = "frmSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmSetting";
            this.Load += new System.EventHandler(this.frmSetting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.guna2Panel2.ResumeLayout(false);
            this.guna2Panel2.PerformLayout();
            this.pnScale.ResumeLayout(false);
            this.pnScale.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private Guna.UI2.WinForms.Guna2MessageDialog msg;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label15;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel2;
        private Guna.UI2.WinForms.Guna2ComboBox cbbPort;
        private System.Windows.Forms.Label label8;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel4;
        private Guna.UI2.WinForms.Guna2Button btnScaleConnect;
        private Guna.UI2.WinForms.Guna2TextBox txtWeight;
        private System.Windows.Forms.Label label5;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private System.Windows.Forms.Label label2;
        private System.IO.Ports.SerialPort serialPort1;
        private Guna.UI2.WinForms.Guna2ComboBox cbbBaudrate;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private Guna.UI2.WinForms.Guna2ComboBox cbbDatabit;
        private Guna.UI2.WinForms.Guna2ComboBox cbbStopbit;
        private System.Windows.Forms.Label label4;
        private Guna.UI2.WinForms.Guna2ComboBox cbbParity;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private Guna.UI2.WinForms.Guna2ComboBox cbbScaleName;
        private System.Windows.Forms.Panel pnScale;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel5;
        private System.Windows.Forms.Label label10;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel3;
        private System.Windows.Forms.Label label6;
        private Guna.UI2.WinForms.Guna2TextBox txtEndpoint;
        private Guna.UI2.WinForms.Guna2TextBox txtPrinterThermal;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox1;
        private System.IO.Ports.SerialPort serialPort2;
    }
}