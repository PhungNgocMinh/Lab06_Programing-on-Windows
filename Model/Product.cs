using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductClass.Model
    {
            public class Product
            {
                public string MaSP { get; set; }
                public string TenSP { get; set; }
                public string LoaiSP { get; set; }
                public decimal DonGia { get; set; }
                public int SoLuong { get; set; }
                public DateTime NgayNhap { get; set; }
                public bool ConKinhDoanh { get; set; }
                public string DuongDanAnh { get; set; }
                
                ////////////////////Giả sử đổi đc 5000 -> ,5,000 VNĐ
                public string DonGiaVND => DonGia.ToString("#,##0") + " VNĐ";
                public string NgayNhapText => NgayNhap.ToString("dd/MM/yyyy");
                public Product()
                {
                    NgayNhap = DateTime.Now;
                    ConKinhDoanh = true;
                }

                ///////////////////////// Constructor 
                public Product(string maSP, string tenSP, string loaiSP, decimal donGia,
                               int soLuong, DateTime ngayNhap, bool conKinhDoanh, string duongDanAnh)
                /***Huong dan nhap Product: Product sp1 = new Product("SP001", "But bi", "Van phong pham", 5000, 100, DateTime.now, true, @"Images\butbiriel.png");*/
                /////////////////////////////////////////////////////// MaSP    tenSP      loaiSP          donGia SoLg ngayNhap    con`KD   duongDanAnh                                                
                {
                    MaSP = maSP;
                    TenSP = tenSP;
                    LoaiSP = loaiSP;
                    DonGia = donGia;
                    SoLuong = soLuong;
                    NgayNhap = ngayNhap;
                    ConKinhDoanh = conKinhDoanh;
                    DuongDanAnh = duongDanAnh;
                }

                public override string ToString()
                {
                    return $"{MaSP} - {TenSP}";
                }
            }
        }
