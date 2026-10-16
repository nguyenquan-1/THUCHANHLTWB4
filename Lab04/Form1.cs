
using System;
using System.Linq;
using System.Windows.Forms;

namespace Lab04
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Gán sự kiện trực tiếp để các button hoạt động
            btnThem.Click -= btnThem_Click;
            btnThem.Click += btnThem_Click;

            btnSua.Click -= btnSua_Click;
            btnSua.Click += btnSua_Click;

            btnXoa.Click -= btnXoa_Click;
            btnXoa.Click += btnXoa_Click;

            btnThoat.Click -= btnThoat_Click;
            btnThoat.Click += btnThoat_Click;
            btnQuanLyKhoa.Click += btnQuanLyKhoa_Click;
            btnTimKiem.Click += btnTimKiem_Click;
            dgvStudent.CellClick -= dgvStudent_CellClick;
            dgvStudent.CellClick += dgvStudent_CellClick;

            this.Load -= Form1_Load;
            this.Load += Form1_Load;
        }

        // =========================
        // FORM LOAD
        // =========================
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                LoadFaculties();
                LoadStudents();
                ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tải dữ liệu: " + ex.Message,
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================
        // TẢI DANH SÁCH KHOA
        // =========================
        private void LoadFaculties()
        {
            using (StudentModel context = new StudentModel())
            {
                var faculties = context.Faculties.ToList();

                cmbFaculty.DataSource = null;
                cmbFaculty.DisplayMember = "FacultyName";
                cmbFaculty.ValueMember = "FacultyID";
                cmbFaculty.DataSource = faculties;
            }
        }

        // =========================
        // TẢI DANH SÁCH SINH VIÊN
        // =========================
        private void LoadStudents()
        {
            using (StudentModel context = new StudentModel())
            {
                var students = context.Students
                    .Select(s => new
                    {
                        MaSV = s.StudentID,
                        HoTen = s.FullName,
                        TenKhoa = s.Faculty.FacultyName,
                        DiemTB = s.AverageScore
                    })
                    .ToList();

                // Hiển thị đầy đủ dữ liệu
                dgvStudent.DataSource = null;
                dgvStudent.AutoGenerateColumns = true;
                dgvStudent.Columns.Clear();
                dgvStudent.DataSource = students;
            }

            // Đặt tên các cột
            if (dgvStudent.Columns.Count >= 4)
            {
                dgvStudent.Columns["MaSV"].HeaderText = "Mã số SV";
                dgvStudent.Columns["HoTen"].HeaderText = "Họ tên";
                dgvStudent.Columns["TenKhoa"].HeaderText = "Tên khoa";
                dgvStudent.Columns["DiemTB"].HeaderText = "Điểm TB";

                dgvStudent.Columns["MaSV"].Visible = true;
                dgvStudent.Columns["HoTen"].Visible = true;
                dgvStudent.Columns["TenKhoa"].Visible = true;
                dgvStudent.Columns["DiemTB"].Visible = true;
            }

            dgvStudent.ReadOnly = true;
            dgvStudent.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvStudent.MultiSelect = false;
            dgvStudent.AllowUserToAddRows = false;
            dgvStudent.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
        }

        // =========================
        // RESET FORM
        // =========================
        private void ResetForm()
        {
            txtMaSV.Clear();
            txtHoTen.Clear();
            txtDiemTB.Clear();

            if (cmbFaculty.Items.Count > 0)
                cmbFaculty.SelectedIndex = 0;

            dgvStudent.ClearSelection();
            txtMaSV.Focus();
        }

        // =========================
        // KIỂM TRA DỮ LIỆU
        // =========================
        private bool ValidateInput(out float diem)
        {
            diem = 0;

            if (string.IsNullOrWhiteSpace(txtMaSV.Text) ||
                string.IsNullOrWhiteSpace(txtHoTen.Text) ||
                string.IsNullOrWhiteSpace(txtDiemTB.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return false;
            }

            if (txtMaSV.Text.Trim().Length != 10)
            {
                MessageBox.Show(
                    "Mã số sinh viên phải có 10 kí tự!");
                txtMaSV.Focus();
                return false;
            }

            if (!float.TryParse(txtDiemTB.Text.Trim(), out diem))
            {
                MessageBox.Show(
                    "Điểm trung bình phải là số!");
                txtDiemTB.Focus();
                return false;
            }

            if (diem < 0 || diem > 10)
            {
                MessageBox.Show(
                    "Điểm trung bình phải từ 0 đến 10!");
                txtDiemTB.Focus();
                return false;
            }

            if (cmbFaculty.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn khoa!");
                return false;
            }

            return true;
        }

        // =========================
        // NÚT THÊM
        // =========================
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(out float diem))
                return;

            string maSV = txtMaSV.Text.Trim();

            try
            {
                using (StudentModel context = new StudentModel())
                {
                    bool exists = context.Students
                        .Any(s => s.StudentID == maSV);

                    if (exists)
                    {
                        MessageBox.Show(
                            "Mã sinh viên đã tồn tại!");
                        return;
                    }

                    Student student = new Student
                    {
                        StudentID = maSV,
                        FullName = txtHoTen.Text.Trim(),
                        AverageScore = diem,
                        FacultyID = Convert.ToInt32(
                            cmbFaculty.SelectedValue)
                    };

                    context.Students.Add(student);
                    context.SaveChanges();
                }

                LoadStudents();
                ResetForm();

                MessageBox.Show(
                    "Thêm mới dữ liệu thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi thêm sinh viên: " + ex.Message);
            }
        }

        // =========================
        // NÚT SỬA
        // =========================
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(out float diem))
                return;

            string maSV = txtMaSV.Text.Trim();

            try
            {
                using (StudentModel context = new StudentModel())
                {
                    Student student = context.Students
                        .FirstOrDefault(
                            s => s.StudentID == maSV);

                    if (student == null)
                    {
                        MessageBox.Show(
                            "Không tìm thấy MSSV cần sửa!");
                        return;
                    }

                    student.FullName = txtHoTen.Text.Trim();
                    student.AverageScore = diem;
                    student.FacultyID = Convert.ToInt32(
                        cmbFaculty.SelectedValue);

                    context.SaveChanges();
                }

                LoadStudents();
                ResetForm();

                MessageBox.Show(
                    "Cập nhật dữ liệu thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi sửa sinh viên: " + ex.Message);
            }
        }

        // =========================
        // NÚT XÓA
        // =========================
        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maSV = txtMaSV.Text.Trim();

            if (string.IsNullOrWhiteSpace(maSV))
            {
                MessageBox.Show(
                    "Vui lòng chọn sinh viên cần xóa!");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa sinh viên " +
                maSV + " không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            try
            {
                using (StudentModel context = new StudentModel())
                {
                    Student student = context.Students
                        .FirstOrDefault(
                            s => s.StudentID == maSV);

                    if (student == null)
                    {
                        MessageBox.Show(
                            "Không tìm thấy MSSV cần xóa!");
                        return;
                    }

                    context.Students.Remove(student);
                    context.SaveChanges();
                }

                LoadStudents();
                ResetForm();

                MessageBox.Show(
                    "Xóa sinh viên thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi xóa sinh viên: " + ex.Message);
            }
        }

        // =========================
        // CHỌN SINH VIÊN TRÊN BẢNG
        // =========================
        private void dgvStudent_CellClick(
            object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row =
                dgvStudent.Rows[e.RowIndex];

            txtMaSV.Text =
                Convert.ToString(row.Cells[0].Value);

            txtHoTen.Text =
                Convert.ToString(row.Cells[1].Value);

            txtDiemTB.Text =
                Convert.ToString(row.Cells[3].Value);

            string tenKhoa =
                Convert.ToString(row.Cells[2].Value);

            if (!string.IsNullOrWhiteSpace(tenKhoa))
                cmbFaculty.Text = tenKhoa;
        }

        // =========================
        // NÚT THOÁT
        // =========================
        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có muốn thoát chương trình không?",
                "Xác nhận thoát",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
                this.Close();
        }

        private void btnQuanLyKhoa_Click(object sender, EventArgs e)
        {
            frmFaculty frm = new frmFaculty();
            frm.ShowDialog();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            using (frmTimKiemSV frm = new frmTimKiemSV())
            {
                frm.ShowDialog();
            }
        }
    }
}
