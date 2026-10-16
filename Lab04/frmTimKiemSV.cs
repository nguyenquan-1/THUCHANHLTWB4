
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Lab04
{
    public partial class frmTimKiemSV : Form
    {
        public frmTimKiemSV()
        {
            InitializeComponent();

            // Gan su kien cho cac nut
            btnTim.Click += btnTim_Click;
            btnLamMoi.Click += btnLamMoi_Click;
            btnDong.Click += btnDong_Click;
            this.Load += frmTimKiemSV_Load;
        }

        // Khoi tao form
        private void frmTimKiemSV_Load(object sender, EventArgs e)
        {
            LoadKhoa();
            LoadSinhVien();
        }

        // Tai danh sach khoa vao ComboBox
        private void LoadKhoa()
        {
            try
            {
                using (StudentModel db = new StudentModel())
                {
                    var listKhoa = db.Faculties
                        .Select(f => new
                        {
                            f.FacultyID,
                            f.FacultyName
                        })
                        .ToList();

                    // Tao danh sach khoa co them lua chon tat ca
                    var danhSach = new List<object>();

                    danhSach.Add(new
                    {
                        FacultyID = "",
                        FacultyName = "Tất cả khoa"
                    });

                    foreach (var khoa in listKhoa)
                    {
                        danhSach.Add(new
                        {
                            FacultyID = khoa.FacultyID,
                            FacultyName = khoa.FacultyName
                        });
                    }

                    cmbKhoa.DataSource = null;
                    cmbKhoa.DisplayMember = "FacultyName";
                    cmbKhoa.ValueMember = "FacultyID";
                    cmbKhoa.DataSource = danhSach;
                    cmbKhoa.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tải danh sách khoa: " + ex.Message,
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Tai toan bo sinh vien
        private void LoadSinhVien()
        {
            try
            {
                using (StudentModel db = new StudentModel())
                {
                    var ketQua = db.Students
                        .Select(s => new
                        {
                            s.StudentID,
                            s.FullName,
                            s.AverageScore,
                            s.Faculty.FacultyName
                        })
                        .ToList();

                    dgvKetQua.DataSource = null;
                    dgvKetQua.DataSource = ketQua;

                    DinhDangBang();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tải sinh viên: " + ex.Message,
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Tim kiem theo MSSV, ho ten va khoa
        private void btnTim_Click(object sender, EventArgs e)
        {
            string mssv = txtMSSV.Text.Trim();
            string hoTen = txtHoTen.Text.Trim();

            // Lay ma khoa dang chon
            string maKhoa = "";

            if (cmbKhoa.SelectedValue != null)
            {
                maKhoa = cmbKhoa.SelectedValue.ToString();
            }

            try
            {
                using (StudentModel db = new StudentModel())
                {
                    var query = db.Students.AsQueryable();

                    // Tim theo MSSV
                    if (!string.IsNullOrWhiteSpace(mssv))
                    {
                        // So sanh theo chuoi de phu hop MSSV dang string
                        query = query.Where(
                            s => s.StudentID.ToString().Contains(mssv));
                    }

                    
                    if (!string.IsNullOrWhiteSpace(hoTen))
                    {
                        query = query.Where(
                            s => s.FullName.Contains(hoTen));
                    }

                    
                    if (!string.IsNullOrWhiteSpace(maKhoa))
                    {
                        int idKhoa;

                        if (int.TryParse(maKhoa, out idKhoa))
                        {
                            query = query.Where(
                                s => s.FacultyID == idKhoa);
                        }
                    }

                    var ketQua = query
                        .Select(s => new
                        {
                            s.StudentID,
                            s.FullName,
                            s.AverageScore,
                            s.Faculty.FacultyName
                        })
                        .ToList();

                    dgvKetQua.DataSource = null;
                    dgvKetQua.DataSource = ketQua;

                    DinhDangBang();

                    if (ketQua.Count == 0)
                    {
                        MessageBox.Show(
                            "Không tìm thấy sinh viên phù hợp!",
                            "Kết quả tìm kiếm",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tìm kiếm: " + ex.Message,
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Dinh dang bang ket qua
        private void DinhDangBang()
        {
            if (dgvKetQua.Columns.Count == 0)
                return;

            if (dgvKetQua.Columns["StudentID"] != null)
                dgvKetQua.Columns["StudentID"].HeaderText = "MSSV";

            if (dgvKetQua.Columns["FullName"] != null)
                dgvKetQua.Columns["FullName"].HeaderText = "Họ tên";

            if (dgvKetQua.Columns["AverageScore"] != null)
                dgvKetQua.Columns["AverageScore"].HeaderText = "Điểm TB";

            if (dgvKetQua.Columns["FacultyName"] != null)
                dgvKetQua.Columns["FacultyName"].HeaderText = "Khoa";

            dgvKetQua.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvKetQua.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvKetQua.MultiSelect = false;
            dgvKetQua.ReadOnly = true;
            dgvKetQua.AllowUserToAddRows = false;
        }

        // Lam moi dieu kien va hien thi lai toan bo sinh vien
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMSSV.Clear();
            txtHoTen.Clear();

            if (cmbKhoa.Items.Count > 0)
                cmbKhoa.SelectedIndex = 0;

            LoadSinhVien();
            txtMSSV.Focus();
        }

        // Dong form
        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
