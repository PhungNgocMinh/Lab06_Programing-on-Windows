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

        private void Form1_Load(object sender, EventArgs e)
        {
            cboLoaiSP.Items.Clear();
            cboLoaiSP.Items.AddRange(new string[] { "Nước giải khát", "Bánh ngọt", "Tráng miệng", "Khác" });

            dgvSanPham.CellClick -= dgvSanPham_CellClick;
            dgvSanPham.CellClick += dgvSanPham_CellClick;
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
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtMaSP.Text) || string.IsNullOrWhiteSpace(txtTenSP.Text))
            {
                MessageBox.Show("Mã và tên sản phẩm không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!decimal.TryParse(txtDonGia.Text, out decimal price) || price <= 0)
            {
                MessageBox.Show("Đơn giá phải là số và lớn hơn 0!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
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

            Product newProduct = new Product
            {
                MaSP = txtMaSP.Text,
                TenSP = txtTenSP.Text,
                LoaiSP = cboLoaiSP.SelectedItem?.ToString() ?? "Khác",
                DonGia = decimal.Parse(txtDonGia.Text),
                SoLuong = (int)nudSoLuong.Value,
                NgayNhap = dtpNgayNhap.Value,
                ConKinhDoanh = chkConKinhDoanh.Checked,
                DuongDanAnh = txtDuongDanAnh.Text
            };

            _bindingSource.Add(newProduct);
            MessageBox.Show("Đã thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearInput();
        }

        public void btnSua_Click(object sender, EventArgs e)
        {
            if (_bindingSource.Current != null && ValidateInput())
            {
                Product current = (Product)_bindingSource.Current;
                current.MaSP = txtMaSP.Text;
                current.TenSP = txtTenSP.Text;
                current.LoaiSP = cboLoaiSP.SelectedItem?.ToString() ?? "Khác";
                current.DonGia = decimal.Parse(txtDonGia.Text);
                current.SoLuong = (int)nudSoLuong.Value;
                current.NgayNhap = dtpNgayNhap.Value;
                current.ConKinhDoanh = chkConKinhDoanh.Checked;
                current.DuongDanAnh = txtDuongDanAnh.Text;

                _bindingSource.ResetCurrentItem();
                MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void btnXoa_Click(object sender, EventArgs e)
        {
            if (_bindingSource.Current != null)
            {
                if (MessageBox.Show("Bạn có chắc muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _bindingSource.RemoveCurrent();
                    ClearInput();
                }
            }
        }

        public void btnTimKiem_Click(object sender, EventArgs e)
        {
            TextBox txtTimKiem = this.Controls.Find("txtTimKiem", true).FirstOrDefault() as TextBox;
            if (txtTimKiem != null)
            {
                string keyword = txtTimKiem.Text.ToLower();
                var searchResult = _repository.Products.Where(p =>
                    (p.TenSP != null && p.TenSP.ToLower().Contains(keyword)) ||
                    (p.MaSP != null && p.MaSP.ToLower().Contains(keyword))).ToList();

                _bindingSource.DataSource = new SortableBindingList<Product>(searchResult);
            }
        }

        public void btnLamMoi_Click(object sender, EventArgs e)
        {
            _bindingSource.DataSource = _repository.Products;
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
