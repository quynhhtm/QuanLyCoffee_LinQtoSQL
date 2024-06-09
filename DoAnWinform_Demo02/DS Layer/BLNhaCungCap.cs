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
    public class BLNhaCungCap
    {
        public System.Data.Linq.Table<NhaCungCap> DSNhaCungCap02()
        {
            DataSet ds = new DataSet();
            DoAnDataContext qlBH = new DoAnDataContext();
            return qlBH.NhaCungCaps;
        }

        public void CapNhatThongTin02(string MaNCC, string TenNCC, ref string err)
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            var query = (from ncc in qlBH.NhaCungCaps
                         where ncc.MaNCC == MaNCC
                         select ncc).SingleOrDefault();

            if (query != null)
            {
                query.TenNCC = TenNCC;
                qlBH.SubmitChanges();
            }
        }
        public BindingSource TimKiemThongTin02(string text)
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            var query = from ncc in qlBH.NhaCungCaps
                        where ncc.MaNCC.Contains(text) || ncc.TenNCC.Contains(text)
                        select ncc;
            BindingSource dsl = new BindingSource();
            dsl.DataSource = query.ToList();
            return dsl;
        }

        public void XoaNhaCungCap02(ref string err, string MaNCC)
        {
            BLHoaDonCungCap blHoaDon = new BLHoaDonCungCap();
            blHoaDon.XoaNhaCungCap(ref err, MaNCC);
            DoAnDataContext qlBH = new DoAnDataContext();
            var query = from ncc in qlBH.NhaCungCaps
                        where ncc.MaNCC == MaNCC
                        select ncc;
            qlBH.NhaCungCaps.DeleteAllOnSubmit(query);
            qlBH.SubmitChanges();
        }

        public void ThemNhaCungCap02(string TenNCC, ref string err)
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            NhaCungCap ncc = new NhaCungCap();
            BLKhoaChinh khoa = new BLKhoaChinh();
            ncc.MaNCC = khoa.NhaCungCap();
            ncc.TenNCC = TenNCC;
            qlBH.NhaCungCaps.InsertOnSubmit(ncc);
            qlBH.NhaCungCaps.Context.SubmitChanges();
        }
    }
}
