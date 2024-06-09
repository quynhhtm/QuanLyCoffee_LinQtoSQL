using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnWinform_Demo02.DS_Layer
{
    public class BLHoaDonThanhToan
    {
        public void ThemHoaDonThanhToan(DateTime NgayLap, string MaKH, string MaNV, ref string err)
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            HoaDonThanhToan hoadon = new HoaDonThanhToan();
            BLKhoaChinh khoa = new BLKhoaChinh();
            hoadon.MaHD = khoa.HoaDonThanhToan();
            hoadon.NgayLap = NgayLap.Date;
            hoadon.MaKH = MaKH;
            hoadon.MaNV = MaNV;
            qlBH.HoaDonThanhToans.InsertOnSubmit(hoadon);
            qlBH.HoaDonThanhToans.Context.SubmitChanges();
        }

        public void XoaHoaDonThanhToan(ref string err, string MaHD)
        {
            BLChiTietHoaDonThanhToan blChiTietHD = new BLChiTietHoaDonThanhToan();
            blChiTietHD.XoaHoaDonThanhToan(ref err, MaHD);
            DoAnDataContext qlBH = new DoAnDataContext();
            var query = from hoadon in qlBH.HoaDonThanhToans
                        where hoadon.MaHD == MaHD
                        select hoadon;
            qlBH.HoaDonThanhToans.DeleteAllOnSubmit(query);
            qlBH.SubmitChanges();
        }

        public void XoaKhachHang(ref string err, string MaKH)
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            var query = from hoadon in qlBH.HoaDonThanhToans
                        where hoadon.MaKH == MaKH
                        select hoadon;
            var dsHoaDon= from hoadon in qlBH.HoaDonThanhToans
                       where hoadon.MaKH == MaKH
                       select hoadon.MaHD;
            foreach(var MaHD in dsHoaDon)
            {
                BLChiTietHoaDonThanhToan blChiTietHD = new BLChiTietHoaDonThanhToan();
                blChiTietHD.XoaHoaDonThanhToan(ref err, MaHD);
            }
            qlBH.HoaDonThanhToans.DeleteAllOnSubmit(query);
            qlBH.SubmitChanges();
        }

        public void CapNhatThongTin02(string MaHD, DateTime NgayLap, string MaKH, string MaNV, ref string err)
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            var query = (from hoadon in qlBH.HoaDonThanhToans
                         where hoadon.MaHD == MaHD
                         select hoadon).SingleOrDefault();

            if (query != null)
            {
                query.NgayLap = NgayLap.Date;
                query.MaKH = MaKH;
                query.MaNV = MaNV;
                qlBH.SubmitChanges();
            }
        }

        public string MaHoaDonMoi()
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            var query = qlBH.HoaDonThanhToans.OrderByDescending(hd => hd.MaHD).Take(1);
            HoaDonThanhToan hoadon = query.FirstOrDefault();
            if (hoadon != null)
            {
                return hoadon.MaHD;
            }
            return "HDTT001";
        }
    }
}
