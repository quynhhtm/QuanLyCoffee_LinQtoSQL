﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnWinform_Demo02.DS_Layer
{
    public class BLNhomThucUong
    {
        public BindingSource DsNhomThucUong02()
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            var query = from nhomthucuong in qlBH.NhomThucUongs
                        select nhomthucuong;
            BindingSource bds = new BindingSource();
            bds.DataSource = query.ToList();
            return bds;
        }

        public void ThemNhomThucUong02(string TenNhom, ref string err)
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            NhomThucUong nhomthucuong = new NhomThucUong();
            BLKhoaChinh khoa = new BLKhoaChinh();
            nhomthucuong.MaNhom = khoa.NhomThucUong();
            nhomthucuong.TenNhom = TenNhom;
            qlBH.NhomThucUongs.InsertOnSubmit(nhomthucuong);
            qlBH.NhomThucUongs.Context.SubmitChanges();
        }

        public void XoaNhomThucUong02(ref string err, string MaNhom)
        {
            BLThucUong bLThucUong = new BLThucUong();
            bLThucUong.XoaNhomThucUong(ref err, MaNhom);
            DoAnDataContext qlBH = new DoAnDataContext();
            var query = from nhomthucuong in qlBH.NhomThucUongs
                        where nhomthucuong.MaNhom == MaNhom
                        select nhomthucuong;
            qlBH.NhomThucUongs.DeleteAllOnSubmit(query);
            qlBH.SubmitChanges();
        }

        public void CapNhatThongTin02(string MaNhom, string TenNhom, ref string err)
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            var query = (from nhomthucuong in qlBH.NhomThucUongs
                         where nhomthucuong.MaNhom == MaNhom
                         select nhomthucuong).SingleOrDefault();

            if (query != null)
            {
                query.TenNhom = TenNhom;
                qlBH.SubmitChanges();
            }
        }

        public BindingSource TimKiemThongTin02(string text)
        {
            DoAnDataContext qlBH = new DoAnDataContext();
            var query = from nhomthucuong in qlBH.NhomThucUongs
                        where nhomthucuong.MaNhom.Contains(text) || nhomthucuong.TenNhom.Contains(text)
                        select nhomthucuong;
            BindingSource dsl = new BindingSource();
            dsl.DataSource = query.ToList();
            return dsl;
        }
    }
}
