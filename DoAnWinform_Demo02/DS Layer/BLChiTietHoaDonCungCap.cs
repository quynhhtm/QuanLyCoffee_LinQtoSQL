using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnWinform_Demo02.DS_Layer
{
    public class BLChiTietHoaDonCungCap
    {
        public void CapNhatThongTin02(string MaNL, string MaHD, int SoLuong, float DonGia, ref string err)
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            var query = (from chitiethd in qlBH.ChiTietHoaDonCungCaps
                         where chitiethd.MaHD == MaHD && chitiethd.MaNL == MaNL
                         select chitiethd).SingleOrDefault();

            if (query != null)
            {
                query.SoLuong = SoLuong;
                query.DonGia = DonGia;
                qlBH.SubmitChanges();
            }
        }

        public void XoaChiTietHoaDonCungCap02(ref string err, string MaNL, string MaHD)
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            var query = from chitiethd in qlBH.ChiTietHoaDonCungCaps
                        where chitiethd.MaHD == MaHD && chitiethd.MaNL == chitiethd.MaNL
                        select chitiethd;
            qlBH.ChiTietHoaDonCungCaps.DeleteAllOnSubmit(query);
            qlBH.SubmitChanges();
        }

        public void XoaNguyenLieu(ref string err, string MaNL)
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            var query = from chitiethd in qlBH.ChiTietHoaDonCungCaps
                        where chitiethd.MaNL == chitiethd.MaNL
                        select chitiethd;
            qlBH.ChiTietHoaDonCungCaps.DeleteAllOnSubmit(query);
            qlBH.SubmitChanges();
        }

        public void XoaHoaDon(ref string err,string MaHD)
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            var query = from chitiethd in qlBH.ChiTietHoaDonCungCaps
                        where chitiethd.MaHD == MaHD 
                        select chitiethd;
            qlBH.ChiTietHoaDonCungCaps.DeleteAllOnSubmit(query);
            qlBH.SubmitChanges();
        }

        public void ThemChiTietHoaDonCungCap02(string MaNL, string MaHD, int SoLuong, float DonGia, ref string err)
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            ChiTietHoaDonCungCap chitiethd = new ChiTietHoaDonCungCap();
            chitiethd.MaNL = MaNL;
            chitiethd.MaHD = MaHD;
            chitiethd.SoLuong = SoLuong;
            chitiethd.DonGia = DonGia;
            qlBH.ChiTietHoaDonCungCaps.InsertOnSubmit(chitiethd);
            qlBH.ChiTietHoaDonCungCaps.Context.SubmitChanges();
        }
    }
}
