using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnWinform_Demo02.DS_Layer
{
    public class BLChiTietHoaDonThanhToan
    {
        public System.Data.Linq.Table<ChiTietHoaDonThanhToan> DSChiTietHoaDonThanhToan()
        {
            DataSet ds = new DataSet();
            DoAnDataContext qlBH = new DoAnDataContext();
            return qlBH.ChiTietHoaDonThanhToans;
        }

        public void XoaChiTietHoaDonThanhToan(ref string err, string MaHD, string MaThucUong)
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            var query = from chitiethd in qlBH.ChiTietHoaDonThanhToans
                        where chitiethd.MaHD == MaHD && chitiethd.MaThucUong == MaThucUong
                        select chitiethd;
            qlBH.ChiTietHoaDonThanhToans.DeleteAllOnSubmit(query);
            qlBH.SubmitChanges();
        }

        public void XoaHoaDonThanhToan(ref string err, string MaHD)
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            var query = from chitiethd in qlBH.ChiTietHoaDonThanhToans
                        where chitiethd.MaHD == MaHD 
                        select chitiethd;
            qlBH.ChiTietHoaDonThanhToans.DeleteAllOnSubmit(query);
            qlBH.SubmitChanges();
        }

        public void XoaThucUong(ref string err, string MaThucUong)
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            var query = from chitiethd in qlBH.ChiTietHoaDonThanhToans
                        where chitiethd.MaThucUong == MaThucUong
                        select chitiethd;
            qlBH.ChiTietHoaDonThanhToans.DeleteAllOnSubmit(query);
            qlBH.SubmitChanges();
        }

        public void ThemChiTietHoaDonThanhToan(string MaThucUong, string MaHD, int SoLuong, ref string err)
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            ChiTietHoaDonThanhToan chitiethd = new ChiTietHoaDonThanhToan();
            chitiethd.MaHD = MaHD;
            chitiethd.MaThucUong = MaThucUong;
            chitiethd.SoLuong = SoLuong;
            qlBH.ChiTietHoaDonThanhToans.InsertOnSubmit(chitiethd);
            qlBH.ChiTietHoaDonThanhToans.Context.SubmitChanges();
        }
    }
}
