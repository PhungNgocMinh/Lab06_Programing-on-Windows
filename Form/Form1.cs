using ProductClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ProductClass.Data;    //  For BindingSource

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

        }

        private void lblDonGia_Click(object sender, EventArgs e)
        {

        }
    }
}

