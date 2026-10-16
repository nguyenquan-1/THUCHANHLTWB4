
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using System.Data.Entity;
using Lab04_4;

namespace Lab04_4
{
    public partial class frmDonHang : Form
    {
        public frmDonHang()
        {
            InitializeComponent();

            // Su dung ngay hien tai lam khoang tim kiem mac dinh
            dtpTuNgay.Value = DateTime.Today;
            dtpDenNgay.Value = DateTime.Today;

            dtpTuNgay.ValueChanged += NgayThayDoi;
            dtpDenNgay.ValueChanged += NgayThayDoi;
            chkTatCaTrongThang.CheckedChanged +=
                chkTatCaTrongThang_CheckedChanged;

            dgvDonHang.AutoGenerateColumns = false;
            dgvDonHang.ReadOnly = true;
            dgvDonHang.AllowUserToAddRows = false;
            dgvDonHang.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            GanCot();
            TaiDonHang();
        }

        // Gan du lieu vao 5 cot cua DataGridView
        private void GanCot()
        {
            colSTT.DataPropertyName = "STT";
            colSoHD.DataPropertyName = "SoHD";
            colNgayDat.DataPropertyName = "NgayDat";
            colNgayGiao.DataPropertyName = "NgayGiao";
            colThanhTien.DataPropertyName = "ThanhTien";

            colNgayDat.DefaultCellStyle.Format = "dd/MM/yyyy";
            colNgayGiao.DefaultCellStyle.Format = "dd/MM/yyyy";
            colThanhTien.DefaultCellStyle.Format = "N0";
        }

        private void NgayThayDoi(object sender, EventArgs e)
        {
            if (!chkTatCaTrongThang.Checked)
                TaiDonHang();
        }

        private void chkTatCaTrongThang_CheckedChanged(
            object sender, EventArgs e)
        {
            if (chkTatCaTrongThang.Checked)
            {
                DateTime dauThang = new DateTime(
                    DateTime.Today.Year,
                    DateTime.Today.Month, 1);

                DateTime cuoiThang = dauThang
                    .AddMonths(1).AddDays(-1);

                dtpTuNgay.Value = dauThang;
                dtpDenNgay.Value = cuoiThang;
            }
            else
            {
                dtpTuNgay.Value = DateTime.Today;
                dtpDenNgay.Value = DateTime.Today;
            }

            TaiDonHang();
        }

        private void TaiDonHang()
        {
            DateTime tuNgay = dtpTuNgay.Value.Date;
            DateTime denNgay = dtpDenNgay.Value.Date;

            if (tuNgay > denNgay)
            {
                dgvDonHang.DataSource = null;
                txtTongCong.Text = "0";
                return;
            }

            try
            {
                // Doi ProductOrderEntities neu context cua ban
                // co ten khac trong file EDMX.
                using (var db = new ProductOrderEntities())
                {
                    string sql = @"
                        SELECT
                            ROW_NUMBER() OVER
                                (ORDER BY i.InvoiceNo) AS STT,
                            i.InvoiceNo AS SoHD,
                            i.OrderDate AS NgayDat,
                            i.DeliveryDate AS NgayGiao,
                            ISNULL(SUM(o.Price * o.Quantity), 0)
                                AS ThanhTien
                        FROM dbo.Invoice AS i
                        LEFT JOIN dbo.[Order] AS o
                            ON i.InvoiceNo = o.InvoiceNo
                        WHERE i.DeliveryDate >= @TuNgay
                          AND i.DeliveryDate < @DenNgay
                        GROUP BY
                            i.InvoiceNo,
                            i.OrderDate,
                            i.DeliveryDate
                        ORDER BY i.DeliveryDate, i.InvoiceNo";

                    var danhSach = db.Database
                        .SqlQuery<DonHangHienThi>(
                            sql,
                            new SqlParameter("@TuNgay", tuNgay),
                            new SqlParameter("@DenNgay",
                                denNgay.AddDays(1)))
                        .ToList();

                    dgvDonHang.DataSource = danhSach;

                    decimal tongCong = danhSach.Sum(
                        x => x.ThanhTien);

                    txtTongCong.Text = tongCong.ToString("N0");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Khong tai duoc don hang.\n" + ex.Message,
                    "Loi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Kieu du lieu dung de hien thi tren DataGridView
        public class DonHangHienThi
        {
            public long STT { get; set; }
            public string SoHD { get; set; }
            public DateTime NgayDat { get; set; }
            public DateTime NgayGiao { get; set; }
            public decimal ThanhTien { get; set; }
        }

        private void frmDonHang_Load(object sender, EventArgs e)
        {

        }
    }
}

