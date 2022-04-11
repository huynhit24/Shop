using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    public class GioHang
    {
        MyDataDataContext data = new MyDataDataContext();
        public int malaptop { get; set; }
        [Display(Name = "Tên Laptop")]
        public string tenlaptop { get; set; }

        [Display(Name = "Ảnh bìa")]
        public string hinh { get; set; }

        [Display(Name = "Giá bán")]
        public Double giaban { get; set; }

        [Display(Name = "Số lượng")]
        public int iSoluong { get; set; }
        [Display(Name = "Thành tiền")]
        public Double dThanhTien
        {
            get { return iSoluong * giaban; }
        }

        public GioHang(int id)
        {
            malaptop = id;
            Laptop laptop = data.Laptops.Single(n => n.malaptop == malaptop);
            tenlaptop = laptop.tenlaptop;
            hinh = laptop.hinh;
            giaban = double.Parse(laptop.giaban.ToString());
            iSoluong = 1;

        }
    }
}