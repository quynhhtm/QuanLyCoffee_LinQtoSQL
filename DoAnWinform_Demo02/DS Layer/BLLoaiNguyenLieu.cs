using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnWinform_Demo02.DS_Layer
{
    public class BLLoaiNguyenLieu
    {
        public System.Data.Linq.Table<LoaiNguyenLieu> DSLoaiNguyenLieu02()
        {
            DataSet ds = new DataSet();
            DoAnDataContext qlBH = new DoAnDataContext();
            return qlBH.LoaiNguyenLieus;
        }

        public void CapNhatThongTin(string MaLoaiNL, string TenLoaiNL, ref string err)
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            var query = (from loainl in qlBH.LoaiNguyenLieus
                         where loainl.MaLoaiNL == MaLoaiNL
                         select loainl).SingleOrDefault();

            if (query != null)
            {
                query.TenLoaiNL = TenLoaiNL;
                qlBH.SubmitChanges();
            }
        }
        public BindingSource TimKiemThongTin02(string text)
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            var query = from loainl in qlBH.LoaiNguyenLieus
                        where loainl.MaLoaiNL.Contains(text) || loainl.TenLoaiNL.Contains(text)
                        select loainl;
            BindingSource dsl = new BindingSource();
            dsl.DataSource = query.ToList();
            return dsl;
        }

        public void XoaLoaiNguyenLieu02(ref string err, string MaLoaiNL)
        {
            BLNguyenLieu blNL = new BLNguyenLieu();
            blNL.XoaLoaiNguyenLieu(ref err, MaLoaiNL);
            DoAnDataContext qlBH = new DoAnDataContext();
            var query = from loainl in qlBH.LoaiNguyenLieus
                        where loainl.MaLoaiNL == MaLoaiNL
                        select loainl;
            qlBH.LoaiNguyenLieus.DeleteAllOnSubmit(query);
            qlBH.SubmitChanges();
        }

        public void ThemLoaiNguyenLieu02(string TenLoaiNL, ref string err)
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            LoaiNguyenLieu loainl = new LoaiNguyenLieu();
            BLKhoaChinh khoa = new BLKhoaChinh();
            loainl.MaLoaiNL = khoa.LoaiNguyenLieu();
            loainl.TenLoaiNL = TenLoaiNL;
            qlBH.LoaiNguyenLieus.InsertOnSubmit(loainl);
            qlBH.LoaiNguyenLieus.Context.SubmitChanges();
        }
    }
}
