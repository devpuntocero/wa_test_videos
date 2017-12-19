using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wa_transcript
{
    public partial class ctrl_agenda_conversion : System.Web.UI.Page
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
                var items_user = (from c in data_user.inf_fecha_transformacion
                                  select c).Count();

                if (items_user != 0)
                {
                    rb_add_transformation.Visible = false;
                }
            }

        }

        protected void rb_add_transformation_CheckedChanged(object sender, EventArgs e)
        {
            rb_edit_transformation.Checked = false;
            clean_txt();
            div_inftransformation.Visible = true;
        }
        private void clean_txt()
        {
            txt_date.Text = "";
            txt_hora.Text = "";
            ddl_fhora.SelectedValue = "Seleccionar";
            gv_transformationf.Visible = false;
            gv_transformation.Visible = false;
        }
        protected void cmd_save_Click(object sender, EventArgs e)
        {
            if (rb_add_transformation.Checked || rb_edit_transformation.Checked)
            {

                Guid id_fcenter = Guid.Parse(lbl_idcenter.Text);
                string str_date = txt_date.Text;
                string str_hora = txt_hora.Text;
                string str_format = ddl_fhora.SelectedValue;
                string dateString = str_date + " " + str_hora + " " + str_format;
                DateTime str_horario = DateTime.Parse(dateString);

                if (rb_add_transformation.Checked)
                {
                    using (var insert_fiscal = new db_transcriptEntities())
                    {
                        var items_fiscal = new inf_fecha_transformacion
                        {
                            horario = str_horario,
                            id_usuario = id_fuser,
                            id_centro = id_fcenter,
                            fecha_registro = DateTime.Now

                        };
                        insert_fiscal.inf_fecha_transformacion.Add(items_fiscal);
                        insert_fiscal.SaveChanges();
                    }
                    clean_txt();
                    using (db_transcriptEntities data_user = new db_transcriptEntities())
                    {
                        var inf_user = (from u in data_user.inf_fecha_transformacion
                                        where u.horario == str_horario
                                        select new
                                        {

                                            u.id_fecha_transformacion,
                                            u.horario,
                                            u.fecha_registro

                                        }).ToList();

                        gv_transformationf.DataSource = inf_user;
                        gv_transformationf.DataBind();
                        gv_transformationf.Visible = true;
                    }
                    rb_add_transformation.Visible = false;
                    lblModalTitle.Text = "tranScript";
                    lblModalBody.Text = "Horario de conversión de videos, agregado con éxito";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    upModal.Update();
                }
                else if (rb_edit_transformation.Checked)
                {
                    foreach (GridViewRow row in gv_transformation.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox chkRow = (row.Cells[0].FindControl("chk_select") as CheckBox);
                            if (chkRow.Checked)
                            {
                                int str_code = Convert.ToInt32(row.Cells[1].Text);

                                using (var data_address = new db_transcriptEntities())
                                {
                                    var items_address = (from c in data_address.inf_fecha_transformacion
                                                         where c.id_fecha_transformacion == str_code
                                                         select c).FirstOrDefault();

                                    items_address.horario = str_horario;

                                    data_address.SaveChanges();
                                }

                                clean_txt();
                                lblModalTitle.Text = "tranScript";
                                lblModalBody.Text = "Horario de conversión de videos, actualizado con éxito";
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                                upModal.Update();

                                using (db_transcriptEntities data_user = new db_transcriptEntities())
                                {
                                    var inf_user = (from u in data_user.inf_fecha_transformacion
                                                    select new
                                                    {
                                                        u.id_fecha_transformacion,
                                                        u.horario,
                                                        u.fecha_registro

                                                    }).ToList();


                                    gv_transformation.DataSource = inf_user;
                                    gv_transformation.DataBind();
                                    gv_transformation.Visible = true;

                                }
                            }
                        }
                    }
                }
            }
            else
            {

                lblModalTitle.Text = "tranScript";
                lblModalBody.Text = "Favor de seleccionar una accion";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                upModal.Update();
            }
        }
        protected void rb_edit_transformation_CheckedChanged(object sender, EventArgs e)
        {
            div_inftransformation.Visible = true;
            rb_add_transformation.Checked = false;
            gv_transformationf.Visible = false;
            clean_txt();

            using (db_transcriptEntities data_user = new db_transcriptEntities())
            {
                var inf_user = (from u in data_user.inf_fecha_transformacion
                                select new
                                {
                                    u.id_fecha_transformacion,
                                    u.horario,
                                    u.fecha_registro

                                }).ToList();

                if (inf_user.Count == 0)
                {
                    rb_edit_transformation.Checked = false;
                    rfv_date.Enabled = true;
                    rfv_hora.Enabled = true;
                    rfv_fhora.Enabled = true;

                    lblModalTitle.Text = "tranScript";
                    lblModalBody.Text = "Sin registro, favor de agregar uno";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    upModal.Update();
                }
                else
                {
                    gv_transformation.DataSource = inf_user;
                    gv_transformation.DataBind();
                    gv_transformation.Visible = true;
                }
            }
        }
        protected void chkselect_transformation(object sender, EventArgs e)
        {

            foreach (GridViewRow row in gv_transformation.Rows)
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
                            var inf_user = (from u in data_user.inf_fecha_transformacion
                                            where u.id_fecha_transformacion == str_code
                                            select new
                                            {
                                                u.id_fecha_transformacion,
                                                u.horario,
                                                u.fecha_registro

                                            }).FirstOrDefault();

                            DateTime str_date = new DateTime();
                            CultureInfo ci = CultureInfo.InvariantCulture;
                            str_date = Convert.ToDateTime(inf_user.horario);
                            txt_date.Text = str_date.ToShortDateString();
                            txt_hora.Text = str_date.ToString("hh:mm.F", ci);
                            string str_tt = inf_user.horario.Value.ToString("tt");
                            ddl_fhora.SelectedValue = str_tt.ToLower();
                        }
                    }
                    else
                    {
                        row.BackColor = Color.White;
                        chkRow.Checked = false;
                    }
                }
            }
        }
    }
}