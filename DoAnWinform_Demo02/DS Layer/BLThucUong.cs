using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace DoAnWinform_Demo02.DS_Layer
{
    public class BLThucUong
    {
        public BindingSource DSThucUong()
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            var query = from thucuong in qlBH.ThucUongs
                        join nhomthucuong in qlBH.NhomThucUongs on thucuong.MaNhom equals nhomthucuong.MaNhom
                        select new { thucuong.MaThucUong, thucuong.TenThucUong, thucuong.DonGia, nhomthucuong.TenNhom };
            BindingSource dsl = new BindingSource();
            dsl.DataSource = query.ToList();
            return dsl;
        }
        public void ThemThucUong(string TenThucUong, float DonGia, string MaNhom, ref string err)
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            BLKhoaChinh khoa = new BLKhoaChinh();
            ThucUong thucuong = new ThucUong();
            thucuong.MaThucUong = khoa.ThucUong();
            thucuong.TenThucUong = TenThucUong;
            thucuong.DonGia = DonGia;
            thucuong.MaNhom = MaNhom;

            qlBH.ThucUongs.InsertOnSubmit(thucuong);
            qlBH.ThucUongs.Context.SubmitChanges();
        }

        public void CapNhatThongTin(string MaThucUong, string TenThucUong, float DonGia, string MaNhom, ref string err)
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            var query = (from thucuong in qlBH.ThucUongs
                         where thucuong.MaThucUong == MaThucUong
                         select thucuong).SingleOrDefault();

            if (query != null)
            {
                query.TenThucUong = TenThucUong;
                query.DonGia = DonGia;
                query.MaNhom = MaNhom;
                qlBH.SubmitChanges();
            }
        }

        public void XoaThucUong(ref string err, string MaThucUong)
        {
            BLPhaChe blPhaChe = new BLPhaChe();
            BLChiTietHoaDonThanhToan blChiTietHD = new BLChiTietHoaDonThanhToan();
            blPhaChe.XoaThucUong(ref err, MaThucUong);
            blChiTietHD.XoaThucUong(ref err, MaThucUong);

            DoAnDataContext qlBH = new DoAnDataContext();
            var query = from thucuong in qlBH.ThucUongs
                        where thucuong.MaThucUong == MaThucUong
                        select thucuong;
            qlBH.ThucUongs.DeleteAllOnSubmit(query);
            qlBH.SubmitChanges();
        }

        public void XoaNhomThucUong(ref string err, string MaNhom)
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            var query = from thucuong in qlBH.ThucUongs
                        where thucuong.MaNhom == MaNhom
                        select thucuong;
            var dsThucUong = from thucuong in qlBH.ThucUongs
                        where thucuong.MaNhom == MaNhom
                             select thucuong.MaThucUong;
            foreach(var matu  in dsThucUong)
            {
                BLPhaChe blPhaChe = new BLPhaChe();
                BLChiTietHoaDonThanhToan blChiTietHD = new BLChiTietHoaDonThanhToan();
                blPhaChe.XoaThucUong(ref err, matu.ToString());
                blChiTietHD.XoaThucUong(ref err, matu.ToString());
            }
            qlBH.ThucUongs.DeleteAllOnSubmit(query);
            qlBH.SubmitChanges();
        }

        public BindingSource TimKiemThongTin(string text)
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            var query = from thucuong in qlBH.ThucUongs
                        join nhomthucuong in qlBH.NhomThucUongs on thucuong.MaNhom equals nhomthucuong.MaNhom
                        where thucuong.MaThucUong.Contains(text) || thucuong.TenThucUong.Contains(text) || thucuong.DonGia.ToString().Contains(text)
                        select new { thucuong.MaThucUong, thucuong.TenThucUong, thucuong.DonGia, nhomthucuong.TenNhom };
            BindingSource dsl = new BindingSource();
            dsl.DataSource = query.ToList();
            return dsl;
        }
    }
}
