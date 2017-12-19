﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wa_transcript
{
    public partial class ctrl_usuario : System.Web.UI.Page
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
            int save_user = (int)(Session["ss_save_user"]);

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

                lbl_name.Text = inf_user.nombres + " " + inf_user.a_paterno + " " + inf_user.a_materno;
                lbl_profile_user.Text = inf_user.desc_tipo_usuario;
                lbl_id_profile_user.Text = inf_user.id_tipo_usuario.ToString();
                lbl_user_centerCP.Text = inf_user.nombre;
                lbl_id_centerCP.Text = inf_user.id_centro.ToString();

                switch (save_user)
                {

                    case 2:
                        lbl_reg.Text = "Registro de Administrador";

                        break;
                    case 3:
                        lbl_reg.Text = "Registro de Supervisor";

                        break;
                    case 4:
                        lbl_reg.Text = "Registro de Operador";

                        break;
                }
            }
        }
        protected void cmd_save_Click(object sender, EventArgs e)
        {
            if (rb_add.Checked == false & rb_edit.Checked == false & rb_del.Checked == false)
            {

                lblModalTitle.Text = "tranScript";
                lblModalBody.Text = "Favor de seleccionar una acción";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                upModal.Update();
  
            }
            else
            {

                Guid str_idcentro = Guid.Parse(lbl_id_centerCP.Text);
                Guid str_iduser = Guid.NewGuid();

                string str_nameuser = txt_name_user.Text.ToUpper();
                string str_apater = txt_apater.Text.ToUpper();
                string str_amater = txt_amater.Text.ToUpper();
                string str_codeuser = txt_code_user.Text.ToLower();
                string str_password = mdl_encrypta.Encrypt(txt_password.Text.ToLower());
                Guid f_id_user;

                if (rb_add.Checked)
                {

                    try
                    {
                        string str_filter_code;
                        using (db_transcriptEntities data_user = new db_transcriptEntities())
                        {
                            var items_user = (from c in data_user.inf_usuarios
                                              where c.codigo_usuario == str_codeuser
                                              select c).FirstOrDefault();

                            str_filter_code = items_user.codigo_usuario.ToString();
                        }

                        if (str_codeuser == str_filter_code)
                        {
                            clean_data();
                            lblModalTitle.Text = "tranScript";
                            lblModalBody.Text = "Usuario ya existe en la base, agregar otro usuario";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                            upModal.Update();
                        }
                    }
                    catch
                    {
                        int save_user = (int)(Session["ss_save_user"]);

                        using (var insert_user = new db_transcriptEntities())
                        {
                            var items_user = new inf_usuarios
                            {
                                id_usuario = str_iduser,
                                nombres = str_nameuser,
                                a_paterno = str_apater,
                                a_materno = str_amater,
                                id_tipo_usuario = save_user,
                                codigo_usuario = str_codeuser,
                                clave = str_password,
                                id_estatus = 1,
                                fecha_registro = DateTime.Now,
                                id_centro = str_idcentro
                            };
                            insert_user.inf_usuarios.Add(items_user);
                            insert_user.SaveChanges();
                        }
                        clean_data();
                        lblModalTitle.Text = "tranScript";
                        lblModalBody.Text = "Datos de usuario guardados con éxito";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                        upModal.Update();
                    }
                }
                else if (rb_edit.Checked)
                {
               

                    foreach (GridViewRow row in gv_usuarios.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox chkRow = (row.Cells[1].FindControl("chk_select") as CheckBox);
                            if (chkRow.Checked)
                            {
                                string codeuser = row.Cells[1].Text;

                                using (db_transcriptEntities data_user = new db_transcriptEntities())
                                {
                                    var items_user = (from c in data_user.inf_usuarios
                                                      where c.codigo_usuario == codeuser
                                                      select c).FirstOrDefault();

                                    f_id_user = items_user.id_usuario;
                                }

                                using (var data_user = new db_transcriptEntities())
                                {
                                    var items_user = (from c in data_user.inf_usuarios
                                                      where c.id_usuario == f_id_user
                                                      select c).FirstOrDefault();

                                    items_user.nombres = str_nameuser;
                                    items_user.a_paterno = str_apater;
                                    items_user.a_materno = str_amater;
                                    items_user.codigo_usuario = str_codeuser;
                                    items_user.clave = str_password;

                                    data_user.SaveChanges();
                                }

                                clean_data();

                                rb_edit.Checked = false;

                                gv_usuarios.Visible = false;
                                txt_search.Visible = false;
                                cmd_search.Visible = false;

                                lblModalTitle.Text = "tranScript";
                                lblModalBody.Text = "Datos de usuario actualizados con éxito";
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                                upModal.Update();
                            }
                        }
                    }
                }
               
                else if (rb_del.Checked)
                {
  

                    foreach (GridViewRow row in gv_usuarios.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox chkRow = (row.Cells[0].FindControl("chk_select") as CheckBox);
                            if (chkRow.Checked)
                            {
                                string codeuser = row.Cells[1].Text;

                                using (db_transcriptEntities data_user = new db_transcriptEntities())
                                {
                                    var items_user = (from c in data_user.inf_usuarios
                                                      where c.codigo_usuario == codeuser
                                                      select c).FirstOrDefault();

                                    f_id_user = items_user.id_usuario;
                                }

                                using (var data_user = new db_transcriptEntities())
                                {
                                    var items_user = (from c in data_user.inf_usuarios
                                                      where c.id_usuario == f_id_user
                                                      select c).FirstOrDefault();

                                    items_user.id_estatus = 3;

                                    data_user.SaveChanges();
                                }
                                clean_data();

                                rb_del.Checked = false;

                                gv_usuarios.Visible = false;
                                txt_search.Visible = false;
                                cmd_search.Visible = false;

                                lblModalTitle.Text = "tranScript";
                                lblModalBody.Text = "Datos de usuario eliminados con éxito";
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                                upModal.Update();
                            }
                        }
                    }
                }
            }

        }
        private void flist_user(int?[] id_flist_user)
        {


            if (lbl_id_profile_user.Text == "2")
            {
                using (db_transcriptEntities data_user = new db_transcriptEntities())
                {
                    var inf_user = (from u in data_user.inf_usuarios
                                    join e in data_user.fact_estatus on u.id_estatus equals e.id_estatus
                                    where id_flist_user.Contains(u.id_tipo_usuario)
                                    where u.id_usuario != id_fuser
                                    where u.id_estatus == 1

                                    select new
                                    {
                                        u.codigo_usuario,
                                        e.desc_estatus,
                                        u.fecha_nacimiento,
                                        u.nombres,
                                        u.a_paterno,
                                        u.a_materno

                                    }).ToList();

                    gv_usuarios.DataSource = inf_user;
                    gv_usuarios.DataBind();
                    gv_usuarios.Visible = true;
                }
            }
            else
            {
                using (db_transcriptEntities data_user = new db_transcriptEntities())
                {
                    var inf_user = (from u in data_user.inf_usuarios
                                    join e in data_user.fact_estatus on u.id_estatus equals e.id_estatus
                                    where id_flist_user.Contains(u.id_tipo_usuario)
                                    where u.id_usuario != id_fuser
                                    where u.id_estatus == 1
                                    select new
                                    {
                                        u.codigo_usuario,
                                        e.desc_estatus,
                                        u.fecha_nacimiento,
                                        u.nombres,
                                        u.a_paterno,
                                        u.a_materno

                                    }).ToList();

                    gv_usuarios.DataSource = inf_user;
                    gv_usuarios.DataBind();
                    gv_usuarios.Visible = true;
                }
            }

            if (rb_edit.Checked)
            {
                
            }
        }
        protected void rb_add_CheckedChanged(object sender, EventArgs e)
        {
            rb_edit.Checked = false;
            
            rb_del.Checked = false;

            rfv_name_user.Enabled = true;
            rfv_amater.Enabled = true;
            rfv_apater.Enabled = true;
            rfv_email_im.Enabled = true;
            rfv_password.Enabled = true;

            clean_data();

            gv_usuarios.Visible = false;
            txt_search.Visible = false;
            cmd_search.Visible = false;

        }
        protected void rb_edit_CheckedChanged(object sender, EventArgs e)
        {
            rb_add.Checked = false;
            
            rb_del.Checked = false;
            txt_search.Visible = true;
            cmd_search.Visible = true;

            rfv_name_user.Enabled = false;
            rfv_amater.Enabled = false;
            rfv_apater.Enabled = false;
            rfv_email_im.Enabled = false;
            rfv_password.Enabled = false;

            clean_data();
            int? save_user = (int)(Session["ss_save_user"]);
            var two_user = new int?[] { save_user };
            flist_user(two_user);
        }
        protected void rb_drop_CheckedChanged(object sender, EventArgs e)
        {
            rb_edit.Checked = false;
            rb_add.Checked = false;
            rb_del.Checked = false;

            txt_search.Visible = true;
            cmd_search.Visible = true;

            clean_data();
            int? save_user = (int)(Session["ss_save_user"]);
            var two_user = new int?[] { save_user };
            flist_user(two_user);
        }
        protected void rb_del_CheckedChanged(object sender, EventArgs e)
        {
            rb_edit.Checked = false;
            
            rb_add.Checked = false;

            txt_search.Visible = true;
            cmd_search.Visible = true;

            clean_data();
            int? save_user = (int)(Session["ss_save_user"]);
            var two_user = new int?[] { save_user };
            flist_user(two_user);
        }
        private void clean_data()
        {
            txt_search.Text = "";
            txt_name_user.Text = "";
            txt_apater.Text = "";
            txt_amater.Text = "";
            txt_code_user.Text = "";
            txt_password.Text = "";
        }
        protected void chk_OnCheckedChanged(object sender, EventArgs e)
        {
            Guid id_fuser;
            rfv_name_user.Enabled = true;
            rfv_amater.Enabled = true;
            rfv_apater.Enabled = true;
            rfv_email_im.Enabled = true;
            rfv_password.Enabled = true;

            foreach (GridViewRow row in gv_usuarios.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chk_select") as CheckBox);
                    if (chkRow.Checked)
                    {
                        row.BackColor = Color.Orange;
                        string codeuser = row.Cells[1].Text;

                        using (db_transcriptEntities data_user = new db_transcriptEntities())
                        {
                            var items_user = (from c in data_user.inf_usuarios
                                              where c.codigo_usuario == codeuser
                                              select c).FirstOrDefault();

                            id_fuser = items_user.id_usuario;
                        }
                        int? save_user = (int)(Session["ss_save_user"]);

                        if (save_user == 2)
                        {
                            using (db_transcriptEntities data_user = new db_transcriptEntities())
                            {
                                var inf_user = (from u in data_user.inf_usuarios
                                                where u.id_usuario == id_fuser
                                                select new
                                                {                                                  
                                                    u.nombres,
                                                    u.a_paterno,
                                                    u.a_materno,
                                                    u.codigo_usuario,
                                                    u.clave

                                                }).FirstOrDefault();


                                txt_name_user.Text = inf_user.nombres;
                                txt_apater.Text = inf_user.a_paterno;
                                txt_amater.Text = inf_user.a_materno;
                                txt_code_user.Text = inf_user.codigo_usuario;
                                txt_password.Text = inf_user.clave;
                            }
                        }
                        else
                        {
                            using (db_transcriptEntities data_user = new db_transcriptEntities())
                            {
                                var inf_user = (from u in data_user.inf_usuarios
                                                where u.id_usuario == id_fuser
                                                select new
                                                {
                                                    u.nombres,
                                                    u.a_paterno,
                                                    u.a_materno,
                                                    u.codigo_usuario,
                                                    u.clave

                                                }).FirstOrDefault();


                                txt_name_user.Text = inf_user.nombres;
                                txt_apater.Text = inf_user.a_paterno;
                                txt_amater.Text = inf_user.a_materno;
                                txt_code_user.Text = inf_user.codigo_usuario;
                                txt_password.Text = inf_user.clave;
                            }
                        }
                    }
                    else
                    {
                        row.BackColor = Color.White;
                    }
                }
            }
        }

        protected void cmd_search_Click(object sender, EventArgs e)
        {
            string str_userb = txt_search.Text;
            int? save_user = (int)(Session["ss_save_user"]);
            var two_user = new int?[] { save_user };

            using (db_transcriptEntities data_user = new db_transcriptEntities())
            {
                var inf_user = (from u in data_user.inf_usuarios
                                join est in data_user.fact_estatus on u.id_estatus equals est.id_estatus
                                where u.nombres.Contains(str_userb)
                                where two_user.Contains(u.id_tipo_usuario)
                                where u.id_estatus == 1

                                select new
                                {
                                    u.codigo_usuario,
                                    est.desc_estatus,
                                    u.fecha_nacimiento,
                                    u.nombres,
                                    u.a_paterno,
                                    u.a_materno

                                }).ToList();

                if (inf_user.Count == 0)
                {
                    gv_usuarios.DataSource = inf_user;
                    gv_usuarios.DataBind();
                    gv_usuarios.Visible = true;

                    lblModalTitle.Text = "tranScript";
                    lblModalBody.Text = "Usuario no exite o tiene un perfil diferente";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    upModal.Update();


                }
                else
                {
                    gv_usuarios.DataSource = inf_user;
                    gv_usuarios.DataBind();
                    gv_usuarios.Visible = true;
                }
            }
        }
    }
}