
using System;
using System.Linq;
using System.Windows.Forms;

namespace Lab04
{
    public partial class frmFaculty : Form
    {
        private readonly StudentModel db = new StudentModel();

        public frmFaculty()
        {
            InitializeComponent();

            // Gan su kien cho cac nut
            btnThemSua.Click += btnThemSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnDong.Click += btnDong_Click;

            // Su kien chon dong trong bang
            dgvFaculty.CellClick += dgvFaculty_CellClick;

            // Tai du lieu khi mo form
            this.Load += frmFaculty_Load;
        }

        private void frmFaculty_Load(object sender, EventArgs e)
        {
            LoadFaculties();
        }

        // Hien thi danh sach khoa
        private void LoadFaculties()
        {
            try
            {
                using (StudentModel context = new StudentModel())
                {
                    var list = context.Faculties
                        .Select(f => new
                        {
                            f.FacultyID,
                            f.FacultyName,
                            f.TotalProfessor
                        })
                        .ToList();

                    dgvFaculty.AutoGenerateColumns = true;
                    dgvFaculty.DataSource = null;
                    dgvFaculty.DataSource = list;

                    dgvFaculty.Columns["FacultyID"].HeaderText = "Mã khoa";
                    dgvFaculty.Columns["FacultyName"].HeaderText = "Tên khoa";
                    dgvFaculty.Columns["TotalProfessor"].HeaderText =
                        "Tổng số giáo sư";

                    dgvFaculty.AutoSizeColumnsMode =
                        DataGridViewAutoSizeColumnsMode.Fill;
                    dgvFaculty.SelectionMode =
                        DataGridViewSelectionMode.FullRowSelect;
                    dgvFaculty.MultiSelect = false;
                    dgvFaculty.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách khoa: " + ex.Message);
            }
        }

        // Them khoa hoac cap nhat khoa
        private void btnThemSua_Click(object sender, EventArgs e)
        {
            int maKhoa;
            int tongSoGS;

            if (!int.TryParse(txtMaKhoa.Text.Trim(), out maKhoa))
            {
                MessageBox.Show("Mã khoa phải là số nguyên!");
                txtMaKhoa.Focus();
                return;
            }

            string tenKhoa = txtTenKhoa.Text.Trim();

            if (string.IsNullOrWhiteSpace(tenKhoa))
            {
                MessageBox.Show("Vui lòng nhập tên khoa!");
                txtTenKhoa.Focus();
                return;
            }

            if (!int.TryParse(txtTongSoGS.Text.Trim(), out tongSoGS)
                || tongSoGS < 0)
            {
                MessageBox.Show(
                    "Tổng số giáo sư phải là số nguyên không âm!");
                txtTongSoGS.Focus();
                return;
            }

            try
            {
                using (StudentModel context = new StudentModel())
                {
                    var faculty = context.Faculties
                        .FirstOrDefault(f => f.FacultyID == maKhoa);

                    if (faculty == null)
                    {
                        // Them khoa moi
                        faculty = new Faculty
                        {
                            FacultyID = maKhoa,
                            FacultyName = tenKhoa,
                            TotalProfessor = tongSoGS
                        };

                        context.Faculties.Add(faculty);
                        context.SaveChanges();

                        MessageBox.Show("Thêm khoa thành công!");
                    }
                    else
                    {
                        // Cap nhat khoa da ton tai
                        faculty.FacultyName = tenKhoa;
                        faculty.TotalProfessor = tongSoGS;

                        context.SaveChanges();

                        MessageBox.Show("Cập nhật khoa thành công!");
                    }
                }

                LoadFaculties();
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu dữ liệu: " + ex.Message);
            }
        }

        // Chon khoa tren bang de hien thi len cac o nhap
        private void dgvFaculty_CellClick(
            object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dgvFaculty.Rows[e.RowIndex];

            txtMaKhoa.Text =
                Convert.ToString(row.Cells["FacultyID"].Value);

            txtTenKhoa.Text =
                Convert.ToString(row.Cells["FacultyName"].Value);

            txtTongSoGS.Text =
                Convert.ToString(row.Cells["TotalProfessor"].Value);
        }

        // Xoa khoa
        private void btnXoa_Click(object sender, EventArgs e)
        {
            int maKhoa;

            if (!int.TryParse(txtMaKhoa.Text.Trim(), out maKhoa))
            {
                MessageBox.Show("Vui lòng chọn khoa cần xóa!");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn xóa khoa này?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            try
            {
                using (StudentModel context = new StudentModel())
                {
                    var faculty = context.Faculties
                        .FirstOrDefault(f => f.FacultyID == maKhoa);

                    if (faculty == null)
                    {
                        MessageBox.Show("Không tìm thấy khoa!");
                        return;
                    }

                    bool hasStudents = context.Students
                        .Any(s => s.FacultyID == maKhoa);

                    if (hasStudents)
                    {
                        MessageBox.Show(
                            "Không thể xóa khoa đang có sinh viên!");
                        return;
                    }

                    context.Faculties.Remove(faculty);
                    context.SaveChanges();
                }

                MessageBox.Show("Xóa khoa thành công!");
                LoadFaculties();
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa khoa: " + ex.Message);
            }
        }

        // Xoa noi dung cac o nhap
        private void ClearInputs()
        {
            txtMaKhoa.Clear();
            txtTenKhoa.Clear();
            txtTongSoGS.Clear();
            txtMaKhoa.Focus();
        }

        // Dong form
        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            db.Dispose();
            base.OnFormClosed(e);
        }
    }
}

