using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    public class HomeModel
    {
        MyDataDataContext data = new MyDataDataContext();
        public List<Laptop> GetListLaptop_OTHER()
        {
            List<Laptop> list = data.Laptops.Where(n => n.trangthai == true).Take(4).ToList();
            return list;
        }
        public List<Laptop> GetListLaptop_FEATURED()
        {
            List<Laptop> list = data.Laptops.Where(n => n.trangthai == true).Take(8).ToList();
            return list;
        }
        public List<Laptop> GetListLaptop_LASTEST()
        {
            List<Laptop> list = data.Laptops.Where(n => n.trangthai == true && n.ngaycapnhat.GetValueOrDefault() == DateTime.Today).OrderByDescending(n => n.ngaycapnhat).Take(8).ToList();
            return list;
        }
        public List<Laptop> GetListLaptop_TOPSELLING()
        {
            List<Laptop> list = data.Laptops.Where(n => n.trangthai == true && n.giaban <= 15000000).Take(8).ToList();
            return list;
        }
        public List<Laptop> GetListLaptopTheoHang(int id)
        {
            List<Laptop> list = data.Laptops.Where(n => n.trangthai == true && n.mahang == id).ToList();
            return list;
        }
        public List<Laptop> GetListLaptopTheoNhuCau(int id)
        {
            List<Laptop> list = data.Laptops.Where(n => n.trangthai == true && n.manhucau == id).ToList();
            return list;
        }

        public List<DanhGia> GetListNhanXetTheoLaptop(int? id)
        {
            List<DanhGia> list = data.DanhGias.Where(n => n.malaptop == id).ToList();
            return list;
        }

        public List<MetaLaptop> GetListMetaLaptopTheoLaptop(int? id)
        {
            List<MetaLaptop> list = data.MetaLaptops.Where(n => n.malaptop == id).ToList();
            return list;
        }

        public List<Hang> GetListHang()
        {
            return data.Hangs.ToList();
        }
        public List<NhuCau> GetListNhuCau()
        {
            return data.NhuCaus.ToList();
        }

        public List<ChuDe> GetListChuDe()
        {
            return data.ChuDes.ToList();
        }

        public List<QuangCao> GetListQuangCao()
        {
            return data.QuangCaos.Where(n => n.trangthai == true).ToList();
        }

        public decimal LoiNhuan()
        {
            decimal money = 0;
            decimal? temp = 0;
            foreach (var item in data.ChiTietDonHangs)
            {
                temp = item.soluong * item.dongia;
            }
            money = (decimal)temp;
            return money;
        }

        public int DemHoaDon()
        {
            int count = data.DonHangs.Count();
            return count;
        }

        public int DemKhachHang()
        {
            int count = data.AspNetUsers.Count();
            return count;
        }

        public int DemSanPhamBan()
        {
            int count = data.Laptops.Where(n => n.trangthai == true).Count();
            return count;
        }
    }
}