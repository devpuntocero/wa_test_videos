using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wa_test_videos
{
    public partial class ctrl_configuracion : System.Web.UI.Page
    {
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
            Guid id_user = mdl_user.str_fiduser;
            lbl_idfuser.Text = id_user.ToString();
            Guid id_fuser = Guid.Parse(lbl_idfuser.Text);

            using (db_videos_testEntities data_user = new db_videos_testEntities())
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
        }
        protected void cmd_save_Click(object sender, EventArgs e)
        {
            if (rb_add.Checked || rb_editar.Checked)
            {
                lbl_mnsj.Visible = false;
                lbl_mnsj.Text = "";

                Guid id_fcenter = Guid.Parse(lbl_idcenter.Text);
                string str_date = txt_date.Text;
                string str_hora = txt_hora.Text;
                string str_format = ddl_fhora.SelectedValue;
                string dateString = str_date + " " + str_hora + " " + str_format;
                DateTime str_horario = DateTime.Parse(dateString);

                if (rb_add.Checked)
                {
                    using (var insert_fiscal = new db_videos_testEntities())
                    {
                        var items_fiscal = new inf_fecha_transformacion
                        {
                            horario = str_horario,
                            id_usuario = mdl_user.str_fiduser,
                            id_centro = id_fcenter,
                            fecha_registro = DateTime.Now

                        };
                        insert_fiscal.inf_fecha_transformacion.Add(items_fiscal);
                        insert_fiscal.SaveChanges();
                    }
                    clean_txt();
                    lbl_mnsj.Visible = true;
                    lbl_mnsj.Text = "Datos agregada con exito";
                }
                else if (rb_editar.Checked)
                {
                    foreach (GridViewRow row in gv_usuarios.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox chkRow = (row.Cells[3].FindControl("chk_select") as CheckBox);
                            if (chkRow.Checked)
                            {
                                int str_code = Convert.ToInt32(row.Cells[1].Text);

                                using (var data_address = new db_videos_testEntities())
                                {
                                    var items_address = (from c in data_address.inf_fecha_transformacion
                                                         where c.id_fecha_transformacion == str_code
                                                         select c).FirstOrDefault();

                                    items_address.horario = str_horario;

                                    data_address.SaveChanges();
                                }

                            }
                        }
                    }

                    lbl_mnsj.Visible = true;
                    lbl_mnsj.Text = "Datos agregada con exito";
                }


            }
            else
            {
                lbl_mnsj.Visible = true;
                lbl_mnsj.Text = "Favor de seleccionar una acción";
            }
        }
        protected void chk_OnCheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gv_usuarios.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[3].FindControl("chk_select") as CheckBox);
                    if (chkRow.Checked)
                    {
                        int str_code = Convert.ToInt32(row.Cells[1].Text);

                        using (db_videos_testEntities data_user = new db_videos_testEntities())
                        {
                            var inf_user = (from u in data_user.inf_fecha_transformacion
                                            where u.id_fecha_transformacion == str_code
                                            select new
                                            {
                                                u.id_fecha_transformacion,
                                                u.horario,
                                                u.fecha_registro

                                            }).FirstOrDefault();

                            DateTime? str_date = new DateTime();
                            str_date = inf_user.horario;
                            txt_date.Text = str_date.Value.ToShortDateString();
                            txt_hora.Text = str_date.Value.ToLongTimeString();
                            string str_tt = inf_user.horario.Value.ToString("tt");
                            ddl_fhora.SelectedValue = str_tt.ToLower();
                        }
                    }
                }
            }

        }
        protected void rb_add_CheckedChanged(object sender, EventArgs e)
        {
            rb_editar.Checked = false;
            clean_txt();
            gv_usuarios.Visible = false;
        }

        private void clean_txt()
        {
            txt_date.Text = "";
            txt_hora.Text = "";
            ddl_fhora.SelectedValue = "Seleccionar";
            lbl_mnsj.Visible = false;
        }

        protected void rb_editar_CheckedChanged(object sender, EventArgs e)
        {
            rb_add.Checked = false;
            clean_txt();

            using (db_videos_testEntities data_user = new db_videos_testEntities())
            {
                var inf_user = (from u in data_user.inf_fecha_transformacion
                                select new
                                {

                                    u.id_fecha_transformacion,
                                    u.horario,
                                    u.fecha_registro

                                }).ToList();

                gv_usuarios.DataSource = inf_user;
                gv_usuarios.DataBind();
                gv_usuarios.Visible = true;
            }
        }


    }
}