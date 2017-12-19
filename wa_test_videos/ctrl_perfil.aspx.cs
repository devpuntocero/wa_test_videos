using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wa_transcript
{
    public partial class ctrl_perfil : System.Web.UI.Page
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
        }
        protected void cmd_save_Click(object sender, EventArgs e)
        {
            string str_nameuser = txt_name_user.Text.ToUpper();
            string str_apater = txt_apater.Text.ToUpper();
            string str_amater = txt_amater.Text.ToUpper();
            string str_codeuser = txt_code_user.Text.ToLower();
            string str_password = mdl_encrypta.Encrypt(txt_password.Text.ToLower());

         

            using (var data_user = new db_transcriptEntities())
            {
                var items_user = (from c in data_user.inf_usuarios
                                  where c.id_usuario == id_fuser
                                  select c).FirstOrDefault();

                items_user.codigo_usuario = str_codeuser;
                items_user.nombres = str_nameuser;
                items_user.a_paterno = str_apater;
                items_user.a_materno = str_amater;
                items_user.clave = str_password;

                data_user.SaveChanges();
            }
            clean_data();

            chkb_editar.Checked = false;

            lblModalTitle.Text = "tranScript";
            lblModalBody.Text = "Datos de usuario actualizados con éxito";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
            upModal.Update();

        }
        private void clean_data()
        {

            txt_name_user.Text = "";
            txt_apater.Text = "";
            txt_amater.Text = "";
            txt_code_user.Text = "";
            txt_password.Text = "";
        }

        protected void chkb_editar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkb_editar.Checked)
            {
       

                using (db_transcriptEntities data_user = new db_transcriptEntities())
                {
                    var inf_user = (from u in data_user.inf_usuarios
                                    join tu in data_user.fact_tipo_usuarios on u.id_tipo_usuario equals tu.id_tipo_usuario
                                    where u.id_usuario == id_fuser

                                    select new
                                    {
                                        u.codigo_usuario,
                                        u.id_genero,
                                        u.fecha_nacimiento,
                                        u.nombres,
                                        u.a_paterno,
                                        u.a_materno,

                                    }).FirstOrDefault();

                    txt_name_user.Text = inf_user.nombres;
                    txt_apater.Text = inf_user.a_paterno;
                    txt_amater.Text = inf_user.a_materno;
                    txt_code_user.Text = inf_user.codigo_usuario;
                }
            }
            else
            {
                clean_data();
            }
        }
    }
}