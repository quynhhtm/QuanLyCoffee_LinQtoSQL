﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace DoAnWinform_Demo02.DS_Layer
{
    public class BLLichLV
    {
        public System.Data.Linq.Table<LichLamViec> DSLichLV02()
        {
            DataSet ds = new DataSet();
            DoAnDataContext qlBH = new DoAnDataContext();
            return qlBH.LichLamViecs;
        }

        public BindingSource DSLichLVNhanVien02()
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            var query = from lichlv in qlBH.LichLamViecs
                        join nv in qlBH.NhanViens on lichlv.MaLLV equals nv.MaLLV
                        select new { nv.MaNV, nv.HoTenNV, lichlv.MaLLV, lichlv.TenLLV, lichlv.TgBatDau, lichlv.TgKetThuc };

            BindingSource dsl = new BindingSource();
            dsl.DataSource = query.ToList();
            return dsl;
        }

        public void CapNhatThongTin02(string MaLLV, string TenLLV, TimeSpan TgBatDau, TimeSpan TgKetThuc, ref string err)
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            var query = (from lichlv in qlBH.LichLamViecs
                         where lichlv.MaLLV == MaLLV
                         select lichlv).SingleOrDefault();

            if (query != null)
            {
                query.TenLLV = TenLLV;
                query.TgBatDau = TgBatDau;
                query.TgKetThuc = TgKetThuc;
                qlBH.SubmitChanges();
            }
        }
        public BindingSource TimKiemThongTin02(string text)
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            var query = from lichlv in qlBH.LichLamViecs
                        join nv in qlBH.NhanViens on lichlv.MaLLV equals nv.MaLLV
                        where nv.MaNV.ToLower().Contains(text) || nv.HoTenNV.ToLower().Contains(text) || lichlv.TenLLV.ToLower().Contains(text)
                        select new { nv.MaNV, nv.HoTenNV, lichlv.MaLLV, lichlv.TenLLV, lichlv.TgBatDau, lichlv.TgKetThuc };
            BindingSource dsl = new BindingSource();
            dsl.DataSource = query.ToList();
            return dsl;
        }
    }
}
