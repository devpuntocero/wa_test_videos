using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wa_transcript
{
    public partial class ctrl_dias_respaldo : System.Web.UI.Page
    {
        static Guid id_fuser;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                    inf_user();

                }
                else
                {

                }
            }
            catch
            {
                Response.Redirect("ctrl_acceso.aspx");
            }
        }
        private void inf_user()
        {
            id_fuser = (Guid)(Session["ss_id_user"]);
            using (db_transcriptEntities data_user = new db_transcriptEntities())
            {
                var inf_user = (from i_u in data_user.inf_usuarios
                                join i_tu in data_user.fact_tipo_usuarios on i_u.id_tipo_usuario equals i_tu.id_tipo_usuario
                                join i_e in data_user.inf_centro on i_u.id_centro equals i_e.id_centro
                                where i_u.id_usuario == id_fuser
                                select new
                                {
                                    i_u.nombres,
                                    i_u.a_paterno,
                                    i_u.a_materno,
                                    i_tu.desc_tipo_usuario,
                                    i_tu.id_tipo_usuario,
                                    i_e.nombre,
                                    i_e.id_centro

                                }).FirstOrDefault();

                lbl_fuser.Text = inf_user.nombres + " " + inf_user.a_paterno + " " + inf_user.a_materno;
                lbl_profileuser.Text = inf_user.desc_tipo_usuario;
                lbl_idprofileuser.Text = inf_user.id_tipo_usuario.ToString();
                lbl_centername.Text = inf_user.nombre;
                lbl_idcenter.Text = inf_user.id_centro.ToString();

            }
            using (db_transcriptEntities data_user = new db_transcriptEntities())
            {
                var inf_user = (from u in data_user.inf_caducidad_videos
                                select new
                                {
                                    u.id_caducidad_videos,
                                    u.dias_caducidad,
                                    u.fecha_registro

                                }).ToList();

                if (inf_user.Count != 0)
                {
                    rb_add_dayvideos.Visible = false;
                }
            }
        }
        protected void rb_add_dayvideos_CheckedChanged(object sender, EventArgs e)
        {
            rb_edit_dayvideos.Checked = false;
            txt_days.Text = "";
            div_infdayvideos.Visible = true;
        }
        protected void rb_edit_dayvideos_CheckedChanged(object sender, EventArgs e)
        {
            rb_add_dayvideos.Checked = false;

            div_infdayvideos.Visible = true;
            rb_add_dayvideos.Checked = false;
            gv_dayvideosf.Visible = false;
            txt_days.Text = "";



            using (db_transcriptEntities data_user = new db_transcriptEntities())
            {
                var inf_user = (from u in data_user.inf_caducidad_videos
                                select new
                                {
                                    u.id_caducidad_videos,
                                    u.dias_caducidad,
                                    u.fecha_registro

                                }).ToList();

                if (inf_user.Count == 0)
                {
                    rb_edit_dayvideos.Checked = false;
              

                    lblModalTitle.Text = "tranScript";
                    lblModalBody.Text = "Sin registro, favor de agregar uno";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    upModal.Update();
                }
                else
                {
                    gv_dayvideos.DataSource = inf_user;
                    gv_dayvideos.DataBind();
                    gv_dayvideos.Visible = true;
                }
            }
        }
    
        protected void cmd_save_days_Click(object sender, EventArgs e)
        {
            int str_ndias = Convert.ToInt32(txt_days.Text);
            Guid id_fcenter = Guid.Parse(lbl_idcenter.Text);
            int str_count;

            using (db_transcriptEntities data_user = new db_transcriptEntities())
            {
                var items_user = (from c in data_user.inf_caducidad_videos

                                  select c).Count();

                str_count = items_user;
            }

            if (str_count == 0)
            {
                using (var insert_fiscal = new db_transcriptEntities())
                {
                    var items_fiscal = new inf_caducidad_videos
                    {
                        dias_caducidad = str_ndias,
                        id_usuario = id_fuser,
                        id_centro = id_fcenter,
                        fecha_registro = DateTime.Now

                    };
                    insert_fiscal.inf_caducidad_videos.Add(items_fiscal);
                    insert_fiscal.SaveChanges();
                }

                txt_days.Text = "";
                using (db_transcriptEntities data_user = new db_transcriptEntities())
                {
                    var inf_user = (from u in data_user.inf_caducidad_videos
                                    where u.dias_caducidad == str_ndias
                                    select new
                                    {

                                        u.id_caducidad_videos,
                                        u.dias_caducidad,
                                        u.fecha_registro

                                    }).ToList();

                    gv_dayvideosf.DataSource = inf_user;
                    gv_dayvideosf.DataBind();
                    gv_dayvideosf.Visible = true;
                }

                rb_add_dayvideos.Visible = false;

                lblModalTitle.Text = "tranScript";
                lblModalBody.Text = "Caducidad de videos, agregado con éxito";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                upModal.Update();

            }
            else
            {
                foreach (GridViewRow row in gv_dayvideos.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (row.Cells[0].FindControl("chk_select") as CheckBox);
                        if (chkRow.Checked)
                        {
                            int str_code = Convert.ToInt32(row.Cells[1].Text);

                            using (var data_address = new db_transcriptEntities())
                            {
                                var items_address = (from c in data_address.inf_caducidad_videos
                                                     where c.id_caducidad_videos == str_code
                                                     select c).FirstOrDefault();

                                items_address.dias_caducidad = str_ndias;
                                data_address.SaveChanges();
                            }

                            txt_days.Text = ""; ;
                            lblModalTitle.Text = "tranScript";
                            lblModalBody.Text = "Caducidad de videos, actualizado con éxito";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                            upModal.Update();

                            using (db_transcriptEntities data_user = new db_transcriptEntities())
                            {
                                var inf_user = (from u in data_user.inf_caducidad_videos
                                                where u.id_caducidad_videos == str_code
                                                select new
                                                {
                                                    u.id_caducidad_videos,
                                                    u.dias_caducidad,
                                                    u.fecha_registro

                                                }).ToList();

                                gv_dayvideos.DataSource = inf_user;
                                gv_dayvideos.DataBind();
                                gv_dayvideos.Visible = true;
                            }
                        }
                    }
                }
               

            }
        }

        protected void chkselect_dayvideos(object sender, EventArgs e)
        {

            foreach (GridViewRow row in gv_dayvideos.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chk_select") as CheckBox);
                    if (chkRow.Checked)
                    {
                        row.BackColor = Color.Orange;
                        int str_code = Convert.ToInt32(row.Cells[1].Text);

                        using (db_transcriptEntities data_user = new db_transcriptEntities())
                        {
                            var inf_user = (from u in data_user.inf_caducidad_videos
                                            where u.id_caducidad_videos == str_code
                                            select new
                                            {
                                                u.id_caducidad_videos,
                                                u.dias_caducidad,
                                                u.fecha_registro


                                            }).FirstOrDefault();

                            txt_days.Text = inf_user.dias_caducidad.ToString();
                        }
                    }
                    else
                    {
                        row.BackColor = Color.White;
                        
                    }
                }
            }
        }
    }
}