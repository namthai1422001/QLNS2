using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QLNS.QLNS
{
    /// <summary>
    /// Quyền: 4
    /// Chức năng: 7
    /// Hành động: Xem (5)
    /// </summary>
    public partial class Quatrinhlamviec : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.Title = "Quá trình làm việc";
                loadRole();
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    loaddulieu(Request.QueryString["id"]);
                }
            }
        }

        #region Security
        //Phan quyen tren trang dua vao day!
        private void loadRole()
        {
            if (Session["UserRolls"] == null)
            {
                Response.Redirect(ResolveUrl("~/Login"));
            }
            List<int> UserRolls = Session["UserRolls"] as List<int>;
            if (UserRolls.Where(p => p == 4).Count() == 0)
            {
                Response.Redirect(ResolveUrl("~/DontAllow"));
            }
        }

        //Ghi lai nhat ky he thong
        private void DiarySystem(int chucnang, int hanhdong, string doituong)
        {
            try
            {
                dbLinQDataContext db = new dbLinQDataContext();
                SYS_Nguoidung objData = db.SYS_Nguoidungs.Where(p => p.ID == new Guid(Session["UserID"].ToString())).FirstOrDefault();
                if (objData != null)
                {
                    db.sp_SYS_Nhatkyhethong_Insert(objData.Username, chucnang, hanhdong, doituong);
                }
                else
                {
                    Response.Redirect("Login");
                }
            }
            catch
            {
                Response.Redirect("Login");
            };
        }
        #endregion

        #region Methods
        private void loaddulieu(string manv)
        {
            dbLinQDataContext db = new dbLinQDataContext();
            var nvien = (from p in db.PB_Nhanviens
                        where p.MaNV == manv
                        select new
                        {
                            HoTen = p.HoNV + " " + p.TenNV
                        }).FirstOrDefault();

            if (nvien != null)
            {
                int stt = 1;
                lblHoTenH2.Text = nvien.HoTen;


                var lstThaydoiphongban = (from pbannvien in db.PB_Thaydoiphongbans
                                          join pban in db.PB_Phongbans
                                          on pbannvien.Maphong equals pban.Maphong
                                          where pbannvien.MaNV == manv
                                          orderby pbannvien.Ngayapdung descending
                                          select new
                                          {
                                              pban.Tenphong,
                                              pbannvien.Ngayapdung,
                                              pbannvien.LyDo,
                                          }).ToList();

                var lstDataThaydoiphongban = (from p in lstThaydoiphongban
                                           select new
                                           {
                                               STT = stt++,
                                               p.Tenphong,
                                               p.Ngayapdung,
                                               p.LyDo
                                           }).ToList();
                rpDataChuyenphongban.DataSource = lstDataThaydoiphongban;
                rpDataChuyenphongban.DataBind();


                stt = 1;
                var lstThaydoichucvu = (from cvunvien in db.PB_Thaydoichucvus
                                        join cvu in db.DIC_Chucvus
                                        on cvunvien.Machucvu equals cvu.Machucvu
                                        where cvunvien.MaNV == manv
                                        orderby cvunvien.Ngayapdung descending
                                        select new
                                        {
                                            cvu.Tenchucvu,
                                            cvunvien.Ngayapdung,
                                            cvunvien.LyDo,
                                        }).ToList();

                var lstDataThaydoichucvu = (from p in lstThaydoichucvu
                                            select new
                                            {
                                                STT = stt++,
                                                p.Tenchucvu,
                                                p.Ngayapdung,
                                                p.LyDo
                                            }).ToList();
                rpDataThaydoichucvu.DataSource = lstDataThaydoichucvu;
                rpDataThaydoichucvu.DataBind();



                stt = 1;
                var lstThaydoicongviec = (from cvunvien in db.PB_Thaydoicongviecs
                                        join cvu in db.DIC_Congviecs
                                        on cvunvien.Macongviec equals cvu.Macongviec
                                        where cvunvien.MaNV == manv
                                        orderby cvunvien.Ngayapdung descending
                                        select new
                                        {
                                            cvu.Tencongviec,
                                            cvunvien.Ngayapdung,
                                            cvunvien.LyDo,
                                        }).ToList();

                var lstDataThaydoicongviec = (from p in lstThaydoicongviec
                                            select new
                                            {
                                                STT = stt++,
                                                p.Tencongviec,
                                                p.Ngayapdung,
                                                p.LyDo
                                            }).ToList();
                rpDataThaydoicongviec.DataSource = lstDataThaydoicongviec;
                rpDataThaydoicongviec.DataBind();



                stt = 1;
                var lstDienbienluong = (from bacluongnvien in db.PB_Thaydoibacluongs
                                        join bluong in db.DIC_BacLuongs
                                        on bacluongnvien.BacLuong equals bluong.Bac
                                        join ngluong in db.DIC_NgachLuongs
                                        on bacluongnvien.MaNgach equals ngluong.MaNgach
                                        where bacluongnvien.MaNV == manv
                                        orderby bacluongnvien.Ngayapdung descending
                                        select new
                                        {
                                            ngluong.TenNgach,
                                            bluong.Tenbac,
                                            bacluongnvien.Hesoluong,
                                            bacluongnvien.Ngayapdung,
                                            bacluongnvien.LyDo
                                        }).ToList();

                var lstDataDienbienluong = (from p in lstDienbienluong
                                            select new
                                            {
                                                STT = stt++,
                                                p.TenNgach,
                                                p.Tenbac,
                                                p.Hesoluong,
                                                p.Ngayapdung,
                                                p.LyDo
                                            }).ToList();
                rpDataDienbienluong.DataSource = lstDataDienbienluong;
                rpDataDienbienluong.DataBind();



                stt = 1;
                var lstDicongtac = (from ctacnvien in db.PB_Dicongtacs
                                    where ctacnvien.MaNV == manv
                                    orderby ctacnvien.Tungay descending
                                    select new
                                    {
                                        ctacnvien.Tungay,
                                        ctacnvien.Denngay,
                                        ctacnvien.Noicongtac,
                                        ctacnvien.Veviec,
                                        ctacnvien.LyDo,
                                        ctacnvien.Tiendicongtac
                                    }).ToList();

                var lstDataDicongtac = (from p in lstDicongtac
                                        select new
                                        {
                                            STT = stt++,
                                            p.Tungay,
                                            p.Denngay,
                                            p.Noicongtac,
                                            p.Veviec,
                                            p.LyDo,
                                            p.Tiendicongtac
                                        }).ToList();
                rpDataDicongtac.DataSource = lstDataDicongtac;
                rpDataDicongtac.DataBind();



                stt = 1;
                var lstTamung = (from tamungnvien in db.PB_TamungNhanviens
                                 where tamungnvien.MaNV == manv
                                 orderby tamungnvien.Ngaytamung descending
                                 select new
                                 {
                                     tamungnvien.Ngaytamung,
                                     tamungnvien.Sotien,
                                     tamungnvien.LyDo
                                 }).ToList();

                var lstDataTamung = (from p in lstTamung
                                     select new
                                     {
                                         STT = stt++,
                                         p.Ngaytamung,
                                         p.Sotien,
                                         p.LyDo
                                     }).ToList();
                rpDataTamung.DataSource = lstDataTamung;
                rpDataTamung.DataBind();


                stt = 1;
                var lstKhenthuong = (from khenthuongnvien in db.PB_KhenthuongNhanviens
                                 where khenthuongnvien.MaNV == manv
                                 orderby khenthuongnvien.Ngaykhenthuong descending
                                 select new
                                 {
                                     khenthuongnvien.Tenkhenthuong,
                                     khenthuongnvien.Hinhthuckhenthuong,
                                     khenthuongnvien.Ngaykhenthuong,
                                     khenthuongnvien.Sotien,
                                     khenthuongnvien.LyDo
                                 }).ToList();

                var lstDataKhenthuong = (from p in lstKhenthuong
                                     select new
                                     {
                                         STT = stt++,
                                         p.Tenkhenthuong,
                                         p.Hinhthuckhenthuong,
                                         p.Ngaykhenthuong,
                                         p.Sotien,
                                         p.LyDo
                                     }).ToList();
                rpDataKhenthuong.DataSource = lstDataKhenthuong;
                rpDataKhenthuong.DataBind();


                DiarySystem(7, 5, manv);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this,GetType(),"alert","alert('Vui lòng truy cập đúng cách!'); window.location = '../Index';",true);
            }
        }
        #endregion
    }
}
