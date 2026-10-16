namespace Lab04_4
{
    partial class frmDonHang
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
            this.chkTatCaTrongThang = new System.Windows.Forms.CheckBox();
            this.lblThoiGian = new System.Windows.Forms.Label();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.lblGach = new System.Windows.Forms.Label();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.dgvDonHang = new System.Windows.Forms.DataGridView();
            this.colSTT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoHD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNgayDat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNgayGiao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colThanhTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblTongCong = new System.Windows.Forms.Label();
            this.txtTongCong = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDonHang)).BeginInit();
            this.SuspendLayout();
            // 
            // chkTatCaTrongThang
            // 
            this.chkTatCaTrongThang.AutoSize = true;
            this.chkTatCaTrongThang.Location = new System.Drawing.Point(12, 31);
            this.chkTatCaTrongThang.Name = "chkTatCaTrongThang";
            this.chkTatCaTrongThang.Size = new System.Drawing.Size(134, 17);
            this.chkTatCaTrongThang.TabIndex = 0;
            this.chkTatCaTrongThang.Text = "Xem tất cả trong tháng";
            this.chkTatCaTrongThang.UseVisualStyleBackColor = true;
            // 
            // lblThoiGian
            // 
            this.lblThoiGian.AutoSize = true;
            this.lblThoiGian.Location = new System.Drawing.Point(13, 82);
            this.lblThoiGian.Name = "lblThoiGian";
            this.lblThoiGian.Size = new System.Drawing.Size(107, 13);
            this.lblThoiGian.TabIndex = 1;
            this.lblThoiGian.Text = "Thời Gian Giao Hàng";
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTuNgay.Location = new System.Drawing.Point(26, 120);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(200, 20);
            this.dtpTuNgay.TabIndex = 2;
            // 
            // lblGach
            // 
            this.lblGach.AutoSize = true;
            this.lblGach.Location = new System.Drawing.Point(245, 126);
            this.lblGach.Name = "lblGach";
            this.lblGach.Size = new System.Drawing.Size(14, 13);
            this.lblGach.TabIndex = 3;
            this.lblGach.Text = "~";
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDenNgay.Location = new System.Drawing.Point(266, 119);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(200, 20);
            this.dtpDenNgay.TabIndex = 4;
            // 
            // dgvDonHang
            // 
            this.dgvDonHang.AllowUserToAddRows = false;
            this.dgvDonHang.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDonHang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDonHang.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSTT,
            this.colSoHD,
            this.colNgayDat,
            this.colNgayGiao,
            this.colThanhTien});
            this.dgvDonHang.Location = new System.Drawing.Point(9, 159);
            this.dgvDonHang.Name = "dgvDonHang";
            this.dgvDonHang.ReadOnly = true;
            this.dgvDonHang.Size = new System.Drawing.Size(779, 266);
            this.dgvDonHang.TabIndex = 5;
            // 
            // colSTT
            // 
            this.colSTT.HeaderText = "STT";
            this.colSTT.Name = "colSTT";
            this.colSTT.ReadOnly = true;
            // 
            // colSoHD
            // 
            this.colSoHD.HeaderText = "Số HĐ";
            this.colSoHD.Name = "colSoHD";
            this.colSoHD.ReadOnly = true;
            // 
            // colNgayDat
            // 
            this.colNgayDat.HeaderText = "Ngày Đặt Hàng";
            this.colNgayDat.Name = "colNgayDat";
            this.colNgayDat.ReadOnly = true;
            // 
            // colNgayGiao
            // 
            this.colNgayGiao.HeaderText = "Ngày Giao Hàng";
            this.colNgayGiao.Name = "colNgayGiao";
            this.colNgayGiao.ReadOnly = true;
            // 
            // colThanhTien
            // 
            this.colThanhTien.HeaderText = "Thành Tiền";
            this.colThanhTien.Name = "colThanhTien";
            this.colThanhTien.ReadOnly = true;
            // 
            // lblTongCong
            // 
            this.lblTongCong.AutoSize = true;
            this.lblTongCong.Location = new System.Drawing.Point(332, 468);
            this.lblTongCong.Name = "lblTongCong";
            this.lblTongCong.Size = new System.Drawing.Size(60, 13);
            this.lblTongCong.TabIndex = 6;
            this.lblTongCong.Text = "Tổng Cộng";
            // 
            // txtTongCong
            // 
            this.txtTongCong.Location = new System.Drawing.Point(418, 468);
            this.txtTongCong.Name = "txtTongCong";
            this.txtTongCong.ReadOnly = true;
            this.txtTongCong.Size = new System.Drawing.Size(157, 20);
            this.txtTongCong.TabIndex = 7;
            this.txtTongCong.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // frmDonHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 548);
            this.Controls.Add(this.txtTongCong);
            this.Controls.Add(this.lblTongCong);
            this.Controls.Add(this.dgvDonHang);
            this.Controls.Add(this.dtpDenNgay);
            this.Controls.Add(this.lblGach);
            this.Controls.Add(this.dtpTuNgay);
            this.Controls.Add(this.lblThoiGian);
            this.Controls.Add(this.chkTatCaTrongThang);
            this.Name = "frmDonHang";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.frmDonHang_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDonHang)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkTatCaTrongThang;
        private System.Windows.Forms.Label lblThoiGian;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.Label lblGach;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.DataGridView dgvDonHang;
        private System.Windows.Forms.Label lblTongCong;
        private System.Windows.Forms.TextBox txtTongCong;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSTT;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoHD;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNgayDat;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNgayGiao;
        private System.Windows.Forms.DataGridViewTextBoxColumn colThanhTien;
    }
}

