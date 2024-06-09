using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnWinform_Demo02.DS_Layer
{
    public class BLHoaDonCungCap
    {
        public System.Data.Linq.Table<HoaDonCungCap> DSHoaDonCungCap02()
        {
            DataSet ds = new DataSet();
            DoAnDataContext qlBH = new DoAnDataContext();
            return qlBH.HoaDonCungCaps;
        }

        public void CapNhatThongTin02(string MaHD, string MaNCC, ref string err)
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            var query = (from hd in qlBH.HoaDonCungCaps
                         where hd.MaHD == MaHD
                         select hd).SingleOrDefault();

            if (query != null)
            {
                query.MaNCC = MaNCC;
                qlBH.SubmitChanges();
            }
        }

        public void XoaHoaDonCungCap02(ref string err, string MaHD)
        {
            BLChiTietHoaDonCungCap blChiTietHD = new BLChiTietHoaDonCungCap();
            blChiTietHD.XoaHoaDon(ref err, MaHD);
            DoAnDataContext qlBH = new DoAnDataContext();
            var query = from hd in qlBH.HoaDonCungCaps
                        where hd.MaHD == MaHD
                        select hd;
            qlBH.HoaDonCungCaps.DeleteAllOnSubmit(query);
            qlBH.SubmitChanges();
        }

        public void XoaNhaCungCap(ref string err, string MaNCC)
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            var query = from hd in qlBH.HoaDonCungCaps
                        where hd.MaNCC == MaNCC
                        select hd;
            var dshoadon = from hd in qlBH.HoaDonCungCaps
                          where hd.MaNCC == MaNCC
                          select hd.MaHD;
            foreach (var hd in dshoadon)
            {
                BLChiTietHoaDonCungCap blChiTietHD = new BLChiTietHoaDonCungCap();
                blChiTietHD.XoaHoaDon(ref err, hd.ToString());
            }
            qlBH.HoaDonCungCaps.DeleteAllOnSubmit(query);
            qlBH.SubmitChanges();
        }

        public void ThemHoaDonCungCap02(DateTime NgayLap, string MaNCC, ref string err)
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            HoaDonCungCap hd = new HoaDonCungCap();
            BLKhoaChinh khoa = new BLKhoaChinh();
            hd.MaHD = khoa.HoaDonCungCap();
            hd.MaNCC = MaNCC;
            hd.NgayLap = NgayLap.Date;
            qlBH.HoaDonCungCaps.InsertOnSubmit(hd);
            qlBH.HoaDonCungCaps.Context.SubmitChanges();
        }

        public string MaHoaDonMoi()
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            var query = qlBH.HoaDonCungCaps.OrderByDescending(hd => hd.MaHD).Take(1);
            HoaDonCungCap hoadon = query.FirstOrDefault();
            if (hoadon != null)
            {
                return hoadon.MaHD;
            }
            return "HDCC001";
        }
    }
}
