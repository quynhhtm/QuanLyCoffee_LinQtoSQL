using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnWinform_Demo02.DS_Layer
{
    public class BLNhanVien
    {
        public System.Data.Linq.Table<NhanVien> DSNhanVien()
        {
            DataSet ds = new DataSet();
            DoAnDataContext qlBH = new DoAnDataContext();
            return qlBH.NhanViens;
        }

        public void CapNhatThongTin(string MaNV, string HoTenNV, DateTime NgSinh, string DiaChi, string Phai, string MaLLV, ref string err)
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            var query = (from nv in qlBH.NhanViens
                         where nv.MaNV == MaNV
                         select nv).SingleOrDefault();

            if (query != null)
            {
                query.HoTenNV = HoTenNV;
                query.NgSinh = NgSinh.Date;
                query.DiaChi = DiaChi;
                query.Phai = Phai;
                query.MaLLV = MaLLV;
                qlBH.SubmitChanges();
            }
        }

        public void XoaNhanVien(ref string err, string MaNV)
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            var query = (from nv in qlBH.HoaDonThanhToans
                         where nv.MaNV == MaNV
                         select nv).SingleOrDefault();

            if (query != null)
            {
                query.MaNV = null;
                qlBH.SubmitChanges();
            }

            var queryNV = from nv in qlBH.NhanViens
                        where nv.MaNV == MaNV
                        select nv;
            qlBH.NhanViens.DeleteAllOnSubmit(queryNV);
            qlBH.SubmitChanges();
        }

        public void ThemNhanVien(string HoTenNV, DateTime NgSinh, string DiaChi, string Phai, string MaLLV, ref string err)
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            NhanVien nv = new NhanVien();
            BLKhoaChinh khoa = new BLKhoaChinh();
            nv.MaNV = khoa.NhanVien();
            nv.HoTenNV = HoTenNV;
            nv.NgSinh = NgSinh.Date;
            nv.DiaChi = DiaChi;
            nv.Phai = Phai;
            nv.MaLLV = MaLLV;
            qlBH.NhanViens.InsertOnSubmit(nv);
            qlBH.NhanViens.Context.SubmitChanges();
        }
        public BindingSource TimKiemThongTin(string text)
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            var query = from nv in qlBH.NhanViens
                        where nv.MaNV.Contains(text) || nv.HoTenNV.Contains(text) || nv.Phai.Contains(text) || nv.DiaChi.Contains(text)
                        select nv;
            BindingSource dsl = new BindingSource();
            dsl.DataSource = query.ToList();
            return dsl;
        }

        public string MaNhanVienMoi()
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            var query = qlBH.NhanViens.OrderByDescending(nv => nv.MaNV).Take(1);
            NhanVien nhanvien = query.FirstOrDefault();
            if (nhanvien != null)
            {
                return nhanvien.MaNV;
            }
            return "NV001";
        }
    }
}
