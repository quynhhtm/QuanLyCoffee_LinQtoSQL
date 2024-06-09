using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnWinform_Demo02.DS_Layer
{
    public class BLKhachHang
    {
        public System.Data.Linq.Table<KhachHang> DsKhachHang()
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            return qlBH.KhachHangs;
        }

        public void ThemKhachHang(string TenKH, string DiaChi, string SDT, ref string err)
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            KhachHang kh = new KhachHang();
            BLKhoaChinh khoa = new BLKhoaChinh();
            kh.MaKH = khoa.KhachHang();
            kh.TenKH = TenKH;
            kh.DiaChi = DiaChi;
            kh.SDT = SDT;
            qlBH.KhachHangs.InsertOnSubmit(kh);
            qlBH.KhachHangs.Context.SubmitChanges();
        }

        public void XoaKhachHang(ref string err, string MaKH)
        {
            BLHoaDonThanhToan blHoaDon = new BLHoaDonThanhToan();
            blHoaDon.XoaKhachHang(ref err, MaKH);
            DoAnDataContext qlBH = new DoAnDataContext();
            var query = from kh in qlBH.KhachHangs
                        where kh.MaKH == MaKH
                        select kh;
            qlBH.KhachHangs.DeleteAllOnSubmit(query);
            qlBH.SubmitChanges();
        }

        public void CapNhatThongTin(string MaKH, string TenKH, string DiaChi, string SDT, ref string err)
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            var query = (from kh in qlBH.KhachHangs
                         where kh.MaKH == MaKH
                         select kh).SingleOrDefault();

            if (query != null)
            {
                query.TenKH = TenKH;
                query.DiaChi = DiaChi;
                query.SDT = SDT;
                qlBH.SubmitChanges();
            }
        }

        public BindingSource TimKiemThongTin(string text)
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            var query = from kh in qlBH.KhachHangs
                        where kh.MaKH.Contains(text) || kh.TenKH.Contains(text) || kh.DiaChi.Contains(text) || kh.SDT.Contains(text)
                        select kh;
            BindingSource dsl = new BindingSource();
            dsl.DataSource = query.ToList();
            return dsl;
        }

        public string MaKhachHangMoi()
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            var query = qlBH.KhachHangs.OrderByDescending(kh => kh.MaKH).Take(1);
            KhachHang khachhang = query.FirstOrDefault();
            if (khachhang != null)
            {
                return khachhang.MaKH;
            }
            return "KH001";
        }
    }
}
