using ProductClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProductClass.Data;
using ProductClass.Model;
using ProductClass.Helpers;

namespace ProductClass.Form
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        private readonly ProductRepository _repository = new ProductRepository();
        private readonly BindingSource _bindingSource = new BindingSource();

        public Form1()
        {
            InitializeComponent();
            SetupBinding();
            this.Load += Form1_Load;
        }

        private void SetupBinding()
        {
            _bindingSource.DataSource = _repository.Products;
            dgvSanPham.DataSource = _bindingSource;
            GridSortConfigurator.Apply(dgvSanPham);
        }
        
        private void ShowAll()
        {
            _bindingSource.DataSource = _repository.Products;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cboLoaiSP.Items.Clear();
            cboLoaiSP.Items.AddRange(new string[] { "Nước giải khát", "Bánh ngọt", "Tráng miệng", "Khác" });

            dgvSanPham.CellClick -= dgvSanPham_CellClick;
            dgvSanPham.CellClick += dgvSanPham_CellClick;

            KhoiTaoCacNutBam();
        }

        private void KhoiTaoCacNutBam()
        {
            btnThem.Text = "Thêm";

            if (this.Controls.Find("btnSua", true).Length == 0)
            {
                Button btnSua = new Button
                {
                    Name = "btnSua",
                    Text = "Sửa",
                    Size = new Size(100, btnThem.Height),
                    Location = new Point(btnThem.Right + 10, btnThem.Top),
                    UseVisualStyleBackColor = true
                };
                btnSua.Click += btnSua_Click;
                this.Controls.Add(btnSua);
            }

            if (this.Controls.Find("btnXoa", true).Length == 0)
            {
                Button btnXoa = new Button
                {
                    Name = "btnXoa",
                    Text = "Xóa",
                    Size = new Size(100, btnThem.Height),
                    Location = new Point(btnThem.Right + 120, btnThem.Top),
                    UseVisualStyleBackColor = true
                };
                btnXoa.Click += btnXoa_Click;
                this.Controls.Add(btnXoa);
            }

            if (this.Controls.Find("btnTimKiem", true).Length == 0)
            {
                Button btnTimKiem = new Button
                {
                    Name = "btnTimKiem",
                    Text = "Tìm kiếm Mã SP",
                    Size = new Size(120, btnThem.Height),
                    Location = new Point(btnThem.Right + 230, btnThem.Top),
                    UseVisualStyleBackColor = true
                };
                btnTimKiem.Click += btnTimKiem_Click;
                this.Controls.Add(btnTimKiem);
            }

            if (this.Controls.Find("btnLamMoi", true).Length == 0)
            {
                Button btnLamMoi = new Button
                {
                    Name = "btnLamMoi",
                    Text = "Làm mới",
                    Size = new Size(100, btnThem.Height),
                    Location = new Point(btnThem.Right + 360, btnThem.Top),
                    UseVisualStyleBackColor = true
                };
                btnLamMoi.Click += btnLamMoi_Click;
                this.Controls.Add(btnLamMoi);
            }
        }

        private void ClearInput()
        {
            txtMaSP.Clear();
            txtTenSP.Clear();
            cboLoaiSP.SelectedIndex = -1;
            txtDonGia.Text = "0";
            nudSoLuong.Value = 0;
            dtpNgayNhap.Value = DateTime.Now;
            chkConKinhDoanh.Checked = false;
            txtDuongDanAnh.Clear();
            picAnh.Image = null;
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtMaSP.Text))
            {
                MessageBox.Show("Mã sản phẩm không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaSP.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtTenSP.Text))
            {
                MessageBox.Show("Tên sản phẩm không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTenSP.Focus();
                return false;
            }
            if (!decimal.TryParse(txtDonGia.Text, out decimal price) || price <= 0)
            {
                MessageBox.Show("Đơn giá phải là số và lớn hơn 0!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDonGia.Focus();
                return false;
            }
            return true;
        }

        private void TimKiemTheoMaSP()
        {
            if (string.IsNullOrWhiteSpace(txtMaSP.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã SP cần tìm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaSP.Focus();
                ShowAll();
                return;
            }

            _bindingSource.DataSource = _repository.Search(txtMaSP.Text);
        }

        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSanPham.Rows[e.RowIndex];
                Product selectedProduct = row.DataBoundItem as Product;

                if (selectedProduct != null)
                {
                    txtMaSP.Text = selectedProduct.MaSP;
                    txtTenSP.Text = selectedProduct.TenSP;
                    cboLoaiSP.SelectedItem = selectedProduct.LoaiSP;
                    txtDonGia.Text = selectedProduct.DonGia.ToString();
                    nudSoLuong.Value = selectedProduct.SoLuong;
                    dtpNgayNhap.Value = selectedProduct.NgayNhap;
                    chkConKinhDoanh.Checked = selectedProduct.ConKinhDoanh;
                    txtDuongDanAnh.Text = selectedProduct.DuongDanAnh;

                    if (!string.IsNullOrEmpty(selectedProduct.DuongDanAnh))
                    {
                        string absolutePath = Path.Combine(Application.StartupPath, selectedProduct.DuongDanAnh);

                        if (File.Exists(absolutePath))
                        {
                            try
                            {
                                if (picAnh.Image != null)
                                {
                                    picAnh.Image.Dispose();
                                    picAnh.Image = null;
                                }

                                using (FileStream fs = new FileStream(absolutePath, FileMode.Open, FileAccess.Read))
                                {
                                    using (Image img = Image.FromStream(fs))
                                    {
                                        picAnh.Image = new Bitmap(img);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Lỗi tải ảnh: " + ex.Message);
                                picAnh.Image = null;
                            }
                        }
                        else
                        {
                            string rootPath = Path.Combine(Application.StartupPath, "..\\..\\", selectedProduct.DuongDanAnh);

                            if (File.Exists(rootPath))
                            {
                                if (picAnh.Image != null) { picAnh.Image.Dispose(); picAnh.Image = null; }
                                using (FileStream fs = new FileStream(rootPath, FileMode.Open, FileAccess.Read))
                                {
                                    using (Image img = Image.FromStream(fs))
                                    {
                                        picAnh.Image = new Bitmap(img);
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy file ảnh tại:\n+ " + absolutePath + "\n+ " + rootPath, "Cảnh báo");
                                picAnh.Image = null;
                            }
                        }
                    }
                    else
                    {
                        picAnh.Image = null;
                    }
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            if (_repository.Products.Any(p => p.MaSP != null && p.MaSP.Equals(txtMaSP.Text.Trim(), StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Mã sản phẩm đã tồn tại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaSP.Focus();
                return;
            }

            Product newProduct = new Product
            {
                MaSP = txtMaSP.Text.Trim(),
                TenSP = txtTenSP.Text.Trim(),
                LoaiSP = cboLoaiSP.SelectedItem?.ToString() ?? "Khác",
                DonGia = decimal.Parse(txtDonGia.Text),
                SoLuong = (int)nudSoLuong.Value,
                NgayNhap = dtpNgayNhap.Value,
                ConKinhDoanh = chkConKinhDoanh.Checked,
                DuongDanAnh = txtDuongDanAnh.Text
            };

            _repository.Add(newProduct);
            ShowAll();
            MessageBox.Show("Đã thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearInput();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvSanPham.CurrentRow == null) return;
            
            Product current = dgvSanPham.CurrentRow.DataBoundItem as Product;
            if (current == null || !ValidateInput()) return;
            
            if (_repository.Exists(txtMaSP.Text.Trim(), current))   // ADD
            {
                MessageBox.Show("Mã sản phẩm đã tồn tại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            current.MaSP = txtMaSP.Text.Trim();
            current.TenSP = txtTenSP.Text.Trim();
            current.LoaiSP = cboLoaiSP.SelectedItem?.ToString() ?? "Khác";
            current.DonGia = decimal.Parse(txtDonGia.Text);
            current.SoLuong = (int)nudSoLuong.Value;
            current.NgayNhap = dtpNgayNhap.Value;
            current.ConKinhDoanh = chkConKinhDoanh.Checked;
            current.DuongDanAnh = txtDuongDanAnh.Text;

            _bindingSource.ResetCurrentItem();
            MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvSanPham.CurrentRow == null) return;
            Product current = dgvSanPham.CurrentRow.DataBoundItem as Product;
            if (current == null) return;

            if (MessageBox.Show("Bạn có chắc muốn xóa sản phẩm này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _repository.Products.Remove(current);
                ShowAll();
                ClearInput();
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            TimKiemTheoMaSP();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ShowAll();
            ClearInput();
        }

        private void grpThongTin_Enter(object sender, EventArgs e) { }
        private void txtMaSP_TextChanged(object sender, EventArgs e) { }
        private void txtTenSP_TextChanged(object sender, EventArgs e) { }
        private void cboLoaiSP_SelectedIndexChanged(object sender, EventArgs e) { }
        private void txtDonGia_TextChanged(object sender, EventArgs e) { }
        private void nudSoLuong_ValueChanged(object sender, EventArgs e) { }
        private void lblNgayNhap_Click(object sender, EventArgs e) { }
        private void lblSoLuong_Click(object sender, EventArgs e) { }
        private void lblMaSP_Click(object sender, EventArgs e) { }
        private void lblTenSP_Click(object sender, EventArgs e) { }
        private void lblLoaiSP_Click(object sender, EventArgs e) { }
        private void chkConKinhDoanh_CheckedChanged(object sender, EventArgs e) { }
        private void dtpNgayNhap_ValueChanged(object sender, EventArgs e) { }
        private void picAnh_Click(object sender, EventArgs e) { }
        private void grpAnh_Enter(object sender, EventArgs e) { }
        private void btnChonAnh_Click(object sender, EventArgs e) { }
        private void lblDuongDanAnh_Click(object sender, EventArgs e) { }
        private void dgvSanPham_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void lblDonGia_Click(object sender, EventArgs e) { }
    }
}