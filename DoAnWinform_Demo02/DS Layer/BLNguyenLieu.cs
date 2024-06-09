using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnWinform_Demo02.DS_Layer
{
    public class BLNguyenLieu
    {
        public BindingSource DsNguyenLieu02()
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            var query = from nguyenlieu in qlBH.NguyenLieus
                        join loainl in qlBH.LoaiNguyenLieus on nguyenlieu.MaLoaiNL equals loainl.MaLoaiNL
                        select new { nguyenlieu.MaNL, nguyenlieu.TenNL, loainl.TenLoaiNL };
            BindingSource bds = new BindingSource();
            bds.DataSource = query.ToList();
            return bds;
        }

        public void ThemNguyenLieu02(string TenNL, string MaLoaiNL, ref string err)
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            NguyenLieu nl = new NguyenLieu();
            BLKhoaChinh khoa = new BLKhoaChinh();
            nl.MaNL = khoa.NguyenLieu();
            nl.TenNL = TenNL;
            nl.MaLoaiNL = MaLoaiNL;
            qlBH.NguyenLieus.InsertOnSubmit(nl);
            qlBH.NguyenLieus.Context.SubmitChanges();
        }

        public void XoaNguyenLieu02(ref string err, string MaNL)
        {
            BLChiTietHoaDonCungCap blChiTietHD = new BLChiTietHoaDonCungCap();
            blChiTietHD.XoaNguyenLieu(ref err, MaNL);
            BLPhaChe blPhaChe = new BLPhaChe();
            blPhaChe.XoaNguyenLieu(ref err, MaNL);

            DoAnDataContext qlBH = new DoAnDataContext();
            var query = from nl in qlBH.NguyenLieus
                        where nl.MaNL == MaNL
                        select nl;
            qlBH.NguyenLieus.DeleteAllOnSubmit(query);
            qlBH.SubmitChanges();
        }

        public void XoaLoaiNguyenLieu(ref string err, string MaLoaiNL)
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            var query = from nl in qlBH.NguyenLieus
                        where nl.MaLoaiNL == MaLoaiNL
                        select nl;
            var dsnguyenlieu = from nl in qlBH.NguyenLieus
                        where nl.MaLoaiNL == MaLoaiNL
                        select nl.MaNL;

            foreach(var nl in dsnguyenlieu)
            {
                BLChiTietHoaDonCungCap blChiTietHD = new BLChiTietHoaDonCungCap();
                blChiTietHD.XoaNguyenLieu(ref err, nl.ToString());
                BLPhaChe blPhaChe = new BLPhaChe();
                blPhaChe.XoaNguyenLieu(ref err, nl.ToString());
            }
            qlBH.NguyenLieus.DeleteAllOnSubmit(query);
            qlBH.SubmitChanges();
        }

        public void CapNhatThongTin02(string MaNL, string TenNL, string MaLoaiNL, ref string err)
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            var query = (from nl in qlBH.NguyenLieus
                         where nl.MaNL == MaNL
                         select nl).SingleOrDefault();

            if (query != null)
            {
                query.TenNL = TenNL;
                query.MaLoaiNL = MaLoaiNL;
                qlBH.SubmitChanges();
            }
        }

        public BindingSource TimKiemThongTin02(string text)
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            var query = from nl in qlBH.NguyenLieus
                        join loainl in qlBH.LoaiNguyenLieus on nl.MaLoaiNL equals loainl.MaLoaiNL
                        where nl.MaNL.Contains(text) || nl.TenNL.Contains(text) || loainl.TenLoaiNL.Contains(text)
                        select new { nl.MaNL, nl.TenNL, loainl.TenLoaiNL };
            BindingSource dsl = new BindingSource();
            dsl.DataSource = query.ToList();
            return dsl;
        }
    }
}
