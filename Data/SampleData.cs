using System;
using System.Collections.Generic;
using ProductClass.Model;

namespace ProductClass.Data
{
    public static class SampleData
    {
        public static List<Product> GetProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    MaSP = "SP001",
                    TenSP = "Laptop Dell Inspiron",
                    LoaiSP = "Laptop",
                    DonGia = 15500000m,
                    SoLuong = 10,
                    NgayNhap = new DateTime(2025, 1, 15),
                    ConKinhDoanh = true,
                    DuongDanAnh = ""
                },
                new Product
                {
                    MaSP = "SP002",
                    TenSP = "Chuột Logitech M331",
                    LoaiSP = "Phụ kiện",
                    DonGia = 350000m,
                    SoLuong = 50,
                    NgayNhap = new DateTime(2025, 2, 3),
                    ConKinhDoanh = true,
                    DuongDanAnh = ""
                },
                new Product
                {
                    MaSP = "SP003",
                    TenSP = "Bàn phím cơ Keychron K2",
                    LoaiSP = "Phụ kiện",
                    DonGia = 1800000m,
                    SoLuong = 0,
                    NgayNhap = new DateTime(2024, 11, 20),
                    ConKinhDoanh = false,
                    DuongDanAnh = ""
                }
            };
        }
    }
}