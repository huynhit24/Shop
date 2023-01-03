using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    public class HomeModel
    {
        MyDataDataContext data = new MyDataDataContext();
        public List<Laptop> GetListLaptop()
        {
            List<Laptop> list = data.Laptops.Where(n => n.trangthai == true).ToList();
            return list;
        }
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
            /*List<Laptop> list = data.Laptops.Where(n => n.trangthai == true 
                                                && (n.ngaycapnhat.GetValueOrDefault() <= DateTime.Today 
                                                && n.ngaycapnhat.GetValueOrDefault().Day >= DateTime.Today.Day - 7) 
                                                && n.ngaycapnhat.GetValueOrDefault().Month == DateTime.Today.Month
                                                && n.ngaycapnhat.GetValueOrDefault().Year == DateTime.Today.Year).OrderByDescending(n => n.ngaycapnhat).Take(8).ToList();*/
            List<Laptop> list = data.Laptops.Where(n => n.trangthai == true
                                                && (n.ngaycapnhat.GetValueOrDefault() <= DateTime.Today)).OrderByDescending(n => n.ngaycapnhat).Take(8).ToList();
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

        public List<ChiTietDonHang> GetListChiTietDonHangTheoDonDatHang(int? id)// lấy ra danh sách meta theo Laptop
        {
            List<ChiTietDonHang> list = data.ChiTietDonHangs.Where(n => n.madon == id).ToList();
            return list;
        }

        public List<Hang> GetListHang()// lấy ra ds tất cả các hãng
        {
            return data.Hangs.ToList();
        }
        public List<Hang> GetListHang_Num(int number)// lấy ra ds tất cả các hãng
        {
            return data.Hangs.Take(number).ToList();
        }
        public List<NhuCau> GetListNhuCau()// lấy ra ds tất cả các nhu cầu
        {
            return data.NhuCaus.ToList();
        }
        public List<NhuCau> GetListNhuCau_Num(int number)// lấy ra ds tất cả các nhu cầu
        {
            return data.NhuCaus.Take(number).ToList();
        }
        public List<ChuDe> GetListChuDe()// lấy ra ds tất cả các chủ đề
        {
            return data.ChuDes.ToList();
        }
        public List<ChuDe> GetListChuDe_Num(int number)// lấy ra ds tất cả các chủ đề
        {
            return data.ChuDes.Take(number).ToList();
        }

        public List<QuangCao> GetListQuangCao()// lấy ra ds các qc được kích hoạt sử dụng
        {
            return data.QuangCaos.Where(n => n.trangthai == true).ToList();
        }

        public double LoiNhuan()// tính lợi nhuận ngày
        {
            double temp = 0;
            foreach (var item in data.ChiTietDonHangs)
            {
                if(item.soluong != null && item.dongia != null)
                {
                    temp += (double)(Convert.ToDouble(item.soluong) * Convert.ToDouble(item.dongia));
                }
            }
            return (double)temp;
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

        ///Thống kê theo các tiêu chí Laptop
        
        //Đếm số lượng Laptop mới nhất
        public int DemLaptopMoiNhat()
        {
            int count = data.Laptops.Where(n => n.trangthai == true && (n.ngaycapnhat.GetValueOrDefault() <= DateTime.Today 
                                            && n.ngaycapnhat.GetValueOrDefault().Day >= DateTime.Today.Day - 7 
                                            && n.ngaycapnhat.GetValueOrDefault().Month == DateTime.Today.Month 
                                            && n.ngaycapnhat.GetValueOrDefault().Year == DateTime.Today.Year)).OrderByDescending(n => n.ngaycapnhat).Count();
            return count;
        }

        //Đếm số lượng Laptop rẻ nhất
        public int DemLaptopReNhat()
        {
            int count = data.Laptops.Where(n => n.trangthai == true 
                                            && (n.ngaycapnhat.GetValueOrDefault() <= DateTime.Today 
                                            && n.ngaycapnhat.GetValueOrDefault().Day >= DateTime.Today.Day - 7)).OrderByDescending(n => n.ngaycapnhat).Count();
            return count;
        }

        //Đếm số lượng Laptop giá rẻ (<= 15 triệu đồng)
        public int DemLaptopGiaRe()// đếm số lượng sản phẩm bán
        {
            int count = data.Laptops.Where(n => n.trangthai == true && n.giaban <= 15000000).Count();
            return count;
        }

        //Đếm số lượng Laptop giá cao (>= 30 triệu)
        public int DemLaptopGiaCao()// đếm số lượng sản phẩm bán
        {
            int count = data.Laptops.Where(n => n.trangthai == true && n.giaban >= 30000000).Count();
            return count;
        }

        //Đếm số vote sản phẩm
        public int DemVoteLaptop(int id)
        {
            try
            {
                int count = data.DanhGias.Where(n => n.trangthai == true && n.malaptop == id).Count();
                return count;
            }
            catch (Exception)
            {
                return 0;
            }
            
        }

        //Đếm số vote sản phẩm trong bảng Đánh giá
        public int CountComment()
        {
            try
            {
                int count = data.BinhLuans.Where(n => n.trangthai == true).Count();
                return count;
            }
            catch (Exception)
            {
                return 0;
            }

        }

        //Đếm số vote sản phẩm trong bảng Tin tức
        public int CountPost()
        {
            try
            {
                int count = data.TinTucs.Where(n => n.xuatban == true).Count();
                return count;
            }
            catch (Exception)
            {
                return 0;
            }

        }

        //Đếm số vote sản phẩm trong bảng 
        public int CountDanhGia()
        {
            try
            {
                int count = data.DanhGias.Where(n => n.trangthai == true).Count();
                return count;
            }
            catch (Exception)
            {
                return 0;
            }

        }

        //Đếm đơn hàng đã hủy
        public int CountInvoiceCancel()
        {
            try
            {
                int count = data.DonHangs.Where(n => n.tinhtrang.ToString() == "1").Count();
                return count;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        //Đếm tổng star rồi chia
        public double DemStarDanhGia(int id)
        {
            try
            {
                int countVote = data.DanhGias.Where(n => n.trangthai == true && n.malaptop == id).Count();
                int vote = (int)data.DanhGias.Where(n => n.trangthai == true && n.malaptop == id).Sum(item => item.vote);
                double voteAgv = 0;
                if(countVote != 0)
                {
                    voteAgv = vote / countVote;
                }
                return voteAgv;
            }
            catch (Exception)
            {
                return 0;
            }    
        }

        //Đếm tất cả đơn hàng
        public int CountInvoice()
        {
            try
            {
                int count = data.DonHangs.Count();
                return count;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        //Đếm tất cả Laptop
        public int CountLaptop()
        {
            try
            {
                int count = data.Laptops.Count();
                return count;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        //Đếm tất cả Account
        public int CountAccount()
        {
            try
            {
                int count = data.AspNetUsers.Count();
                return count;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /**
         * Top 4 Khách VIP
         */

        //Lấy TOP 4 khách hàng VIP
        /*public List<AspNetUser> GET_TOP_4_USERS_VIP()
        {
            List<AspNetUser> list = data.AspNetUsers.Where(n => n.DonHangs.Count(m => m.ChiTietDonHangs.GroupBy(o => o.madon).Sum()));
            return list;
        }*/
    }
}