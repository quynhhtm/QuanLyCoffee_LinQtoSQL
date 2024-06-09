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
    public class BLPhaChe
    {
        public BindingSource DSNguyenLieu(string MaThucUong)
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            var query = from phache in qlBH.PhaChes
                        join thucuong in qlBH.ThucUongs on phache.MaThucUong equals thucuong.MaThucUong
                        join nguyenlieu in qlBH.NguyenLieus on phache.MaNL equals nguyenlieu.MaNL
                        where thucuong.MaThucUong == MaThucUong
                        select new { nguyenlieu.MaNL, MaThucUongNL = thucuong.MaThucUong, nguyenlieu.TenNL };

            BindingSource dsl = new BindingSource();
            dsl.DataSource = query.ToList();
            return dsl;
        }

        public BindingSource TimKiemThongTin(string text)
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            var query = from thucuong in qlBH.ThucUongs
                        join nhomthucuong in qlBH.NhomThucUongs on thucuong.MaNhom equals nhomthucuong.MaNhom
                        where thucuong.MaThucUong.Contains(text) || thucuong.TenThucUong.Contains(text)
                        select new { thucuong.MaThucUong, thucuong.TenThucUong, thucuong.DonGia, nhomthucuong.TenNhom };

            BindingSource dsl = new BindingSource();
            dsl.DataSource = query.ToList();
            return dsl;
        }

        public void XoaPhaChe(ref string err, string MaThucUong, string MaNL)
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            var query = from pc in qlBH.PhaChes
                        where pc.MaThucUong == MaThucUong && pc.MaNL == MaNL
                        select pc;
            qlBH.PhaChes.DeleteAllOnSubmit(query);
            qlBH.SubmitChanges();
        }

        public void XoaThucUong(ref string err, string MaThucUong)
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            var query = from pc in qlBH.PhaChes
                        where pc.MaThucUong == MaThucUong
                        select pc;
            qlBH.PhaChes.DeleteAllOnSubmit(query);
            qlBH.SubmitChanges();
        }

        public void XoaNguyenLieu(ref string err, string MaNL)
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            var query = from pc in qlBH.PhaChes
                        where pc.MaNL == MaNL
                        select pc;
            qlBH.PhaChes.DeleteAllOnSubmit(query);
            qlBH.SubmitChanges();
        }

        public void ThemPhaChe(string MaThucUong, string MaNL, ref string err)
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            PhaChe phache = new PhaChe();
            phache.MaThucUong = MaThucUong;
            phache.MaNL = MaNL;
            qlBH.PhaChes.InsertOnSubmit(phache);
            qlBH.PhaChes.Context.SubmitChanges();
        }

    }
}
