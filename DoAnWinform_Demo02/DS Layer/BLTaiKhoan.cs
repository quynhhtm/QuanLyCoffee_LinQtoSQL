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
    public class BLTaiKhoan
    {
        public BindingSource DSTaiKhoan()
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            var query = from tk in qlBH.TaiKhoans
                        where tk.QuanLy != null
                        select new { tk.TenTK, tk.MatKhau };
            BindingSource dsl = new BindingSource();
            dsl.DataSource = query.ToList();
            return dsl;
        }
        public bool KiemTra(string TenTK, string MatKhau)
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            var query = qlBH.TaiKhoans.Where(tk=>tk.TenTK==TenTK && tk.MatKhau==MatKhau && tk.QuanLy != null).Select(tk=>tk.TenTK).FirstOrDefault();
            
            return query != null;
        }

        public void TaoTaiKhoan(string TenTK, string MatKhau, ref string err)
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            TaiKhoan taikhoan = new TaiKhoan();
            var NQL = qlBH.TaiKhoans.Where(tk => tk.QuanLy == null).Select(tk => tk.TenTK).FirstOrDefault();

            taikhoan.TenTK = TenTK;
            taikhoan.MatKhau = MatKhau;
            taikhoan.QuanLy = NQL.ToString();
            
            qlBH.TaiKhoans.InsertOnSubmit(taikhoan);
            qlBH.TaiKhoans.Context.SubmitChanges();
        }

        public bool KTQuanLy(string TenNQL, string MatKhauNQL)
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            var query = qlBH.TaiKhoans.Where(tk => tk.TenTK == TenNQL && tk.MatKhau == MatKhauNQL && tk.QuanLy == null).Select(tk => tk.TenTK).FirstOrDefault();
            return query != null;
        }

        public void ThayDoiMatKhau(string TenTK, string MatKhau, ref string err)
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            var query = (from tk in qlBH.TaiKhoans
                         where tk.TenTK == TenTK
                         select tk).SingleOrDefault();

            if (query != null)
            {
                query.MatKhau = MatKhau;
                qlBH.SubmitChanges();
            }
        }

        public BindingSource TimKiem(string text)
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            var query = from tk in qlBH.TaiKhoans
                        where tk.TenTK.Contains(text)
                        select new { tk.TenTK, tk.MatKhau };
            BindingSource dsl = new BindingSource();
            dsl.DataSource = query.ToList();
            return dsl;
        }

        public void XoaTaiKhoan(string TenTK, ref string err)
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            var query = from tk in qlBH.TaiKhoans
                        where tk.TenTK == TenTK
                        select tk;
            qlBH.TaiKhoans.DeleteAllOnSubmit(query);
            qlBH.SubmitChanges();
        }
    }
}
