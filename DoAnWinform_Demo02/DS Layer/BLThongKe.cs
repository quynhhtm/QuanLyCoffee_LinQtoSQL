using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnWinform_Demo02
{
    public class BLThongKe
    {
        public BindingSource DoanhThuThang(string Nam)
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            int y = Convert.ToInt32(Nam);
            var query = from chitiethd in qlBH.ChiTietHoaDonThanhToans
                        join thucuong in qlBH.ThucUongs on chitiethd.MaThucUong equals thucuong.MaThucUong
                        join hd in qlBH.HoaDonThanhToans on chitiethd.MaHD equals hd.MaHD
                        where hd.NgayLap.Value.Year == y
                        group new { hd, chitiethd, thucuong } by hd.NgayLap.Value.Month into g
                        select new { Thang = g.Key, DoanhThu = g.Sum(s => s.chitiethd.SoLuong * s.thucuong.DonGia) };

            BindingSource dsl = new BindingSource();
            dsl.DataSource = query.ToList();
            return dsl;
        }

        public BindingSource DSNam()
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            var query = qlBH.HoaDonThanhToans.Select(hd => hd.NgayLap.Value.Year).Distinct();
            BindingSource dsl = new BindingSource();
            dsl.DataSource = query.ToList();
            return dsl;
        }

        public BindingSource DSThang()
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            var query = qlBH.HoaDonThanhToans.Select(hd => hd.NgayLap.Value.Month).Distinct();
            BindingSource dsl = new BindingSource();
            dsl.DataSource = query.ToList();
            return dsl;
        }

        public BindingSource ChiPhiNguyenLieu(string Nam)
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            int y = Convert.ToInt32(Nam);
            var query = from hd in qlBH.HoaDonCungCaps
                        join chitiethd in qlBH.ChiTietHoaDonCungCaps on hd.MaHD equals chitiethd.MaHD
                        where hd.NgayLap.Value.Year == y
                        group new { hd, chitiethd } by hd.NgayLap.Value.Month into g
                        select new { Thang = g.Key, ChiPhi = g.Sum(s => s.chitiethd.SoLuong * s.chitiethd.DonGia) };


            BindingSource dsl = new BindingSource();
            dsl.DataSource = query.ToList();
            return dsl;
        }

        public BindingSource TiLeNhomThucUongBanRa(string Thang, string Nam)
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            int y = Convert.ToInt32(Nam);
            int m = Convert.ToInt32(Thang);
            var query = from chitiethd in qlBH.ChiTietHoaDonThanhToans
                        join thucuong in qlBH.ThucUongs on chitiethd.MaThucUong equals thucuong.MaThucUong
                        join nhomthucuong in qlBH.NhomThucUongs on thucuong.MaNhom equals nhomthucuong.MaNhom
                        join hd in qlBH.HoaDonThanhToans on chitiethd.MaHD equals hd.MaHD
                        where hd.NgayLap.Value.Month == m && hd.NgayLap.Value.Year == y
                        group new { nhomthucuong, chitiethd } by new { nhomthucuong.MaNhom, nhomthucuong.TenNhom } into g
                        select new { TenNhom = g.Key.TenNhom, SoLuong = g.Sum(s => s.chitiethd.SoLuong) };

            BindingSource dls = new BindingSource();
            dls.DataSource = query.ToList();
            return dls;
        }

    }
}
