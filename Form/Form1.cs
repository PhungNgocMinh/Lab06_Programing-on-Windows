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
using ProductClass.Model; //  For BindingSource

namespace ProductClass.Form
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        private readonly ProductRepository _repository = new ProductRepository();   //  Data storing & mediator for BindingSource
        private readonly BindingSource _bindingSource = new BindingSource();
        public Form1()
        {
            InitializeComponent();
            SetupBinding();
        }
        
        private void SetupBinding()
        {
            _bindingSource.DataSource = _repository.Products;
            dgvSanPham.DataSource = _bindingSource;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //  ComboBox product type
            cboLoaiSP.Items.Clear();
            cboLoaiSP.Items.Add("Đồ uống");
            cboLoaiSP.Items.Add("Bánh ngọt");
            cboLoaiSP.Items.Add("Văn phòng phẩm");
            cboLoaiSP.Items.Add("Khác");
            
            dgvSanPham.CellClick -= dgvSanPham_CellClick;
            dgvSanPham.CellClick += dgvSanPham_CellClick;
        }
        
        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //  Check for valid click-event triggered place
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
        
        private void grpThongTin_Enter(object sender, EventArgs e)
        {

        }

        private void txtMaSP_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTenSP_TextChanged(object sender, EventArgs e)
        {

        }

        private void cboLoaiSP_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtDonGia_TextChanged(object sender, EventArgs e)
        {

        }

        private void nudSoLuong_ValueChanged(object sender, EventArgs e)
        {

        }

        private void lblNgayNhap_Click(object sender, EventArgs e)
        {

        }

        private void lblSoLuong_Click(object sender, EventArgs e)
        {

        }

        private void lblMaSP_Click(object sender, EventArgs e)
        {

        }

        private void lblTenSP_Click(object sender, EventArgs e)
        {

        }

        private void lblLoaiSP_Click(object sender, EventArgs e)
        {

        }

        private void chkConKinhDoanh_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dtpNgayNhap_ValueChanged(object sender, EventArgs e)
        {

        }

        private void picAnh_Click(object sender, EventArgs e)
        {

        }

        private void grpAnh_Enter(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {

        }

        private void btnChonAnh_Click(object sender, EventArgs e)
        {

        }

        private void lblDuongDanAnh_Click(object sender, EventArgs e)
        {

        }

        private void dgvSanPham_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Click event for editing data
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSanPham.Rows[e.RowIndex];
                txtMaSP.Text = row.Cells["MaSP"].Value?.ToString();
                txtTenSP.Text = row.Cells["TenSP"].Value?.ToString();
                cboLoaiSP.SelectedItem = row.Cells["LoaiSP"].Value?.ToString();
                txtDonGia.Text = row.Cells["DonGia"].Value?.ToString();

                if (row.Cells["SoLuong"].Value != null)
                {
                    nudSoLuong.Value = Convert.ToDecimal(row.Cells["SoLuong"].Value);
                }

                if (row.Cells["NgayNhap"].Value != null)
                {
                    dtpNgayNhap.Value = Convert.ToDateTime(row.Cells["NgayNhap"].Value);
                }

                if (row.Cells["ConKinhDoanh"].Value != null)
                {
                    chkConKinhDoanh.Checked = Convert.ToBoolean(row.Cells["ConKinhDoanh"].Value);
                }
            }
        }

        private void lblDonGia_Click(object sender, EventArgs e)
        {

        }
    }
}

