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
        public List<Laptop> GetListLaptop_LASTEST()// lấy ra danh sách Laptop thep ngày mới nhất là ngày hiện tại
        {
            List<Laptop> list = data.Laptops.Where(n => n.trangthai == true && n.ngaycapnhat.GetValueOrDefault() == DateTime.Today).OrderByDescending(n => n.ngaycapnhat).Take(8).ToList();
            return list;
        }
        public List<Laptop> GetListLaptop_TOPSELLING()// lấy ra danh sách Laptop giả rẻ hơn 15tr
        {
            List<Laptop> list = data.Laptops.Where(n => n.trangthai == true && n.giaban <= 15000000).Take(8).ToList();
            return list;
        }
        public List<Laptop> GetListLaptopTheoHang(int id)// lấy ra danh sách Laptop theo hãng
        {
            List<Laptop> list = data.Laptops.Where(n => n.trangthai == true && n.mahang == id).ToList();
            return list;
        }
        public List<Laptop> GetListLaptopTheoNhuCau(int id)// láy ra danh sách Laptop theo nhu cầu
        {
            List<Laptop> list = data.Laptops.Where(n => n.trangthai == true && n.manhucau == id).ToList();
            return list;
        }

        public List<DanhGia> GetListNhanXetTheoLaptop(int? id)// lấy ra danh sách đánh giá nhận xét theo laptop
        {
            List<DanhGia> list = data.DanhGias.Where(n => n.malaptop == id).ToList();
            return list;
        }

        public List<MetaLaptop> GetListMetaLaptopTheoLaptop(int? id)// lấy ra danh sách meta theo Laptop
        {
            List<MetaLaptop> list = data.MetaLaptops.Where(n => n.malaptop == id).ToList();
            return list;
        }

        public List<Hang> GetListHang()// lấy ra ds tất cả các hãng
        {
            return data.Hangs.ToList();
        }
        public List<NhuCau> GetListNhuCau()// lấy ra ds tất cả các nhu cầu
        {
            return data.NhuCaus.ToList();
        }

        public List<ChuDe> GetListChuDe()// lấy ra ds tất cả các chủ đề
        {
            return data.ChuDes.ToList();
        }

        public List<QuangCao> GetListQuangCao()// lấy ra ds các qc được kích hoạt sử dụng
        {
            return data.QuangCaos.Where(n => n.trangthai == true).ToList();
        }

        public decimal LoiNhuan()// tính lợi nhuận ngày
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

        public int DemHoaDon()// đếm số hóa đơn
        {
            int count = data.DonHangs.Count();
            return count;
        }

        public int DemKhachHang()// đếm số lượng khách hàng
        {
            int count = data.AspNetUsers.Count();
            return count;
        }

        public int DemSanPhamBan()// đếm số lượng sản phẩm bán
        {
            int count = data.Laptops.Where(n => n.trangthai == true).Count();
            return count;
        }
    }
}