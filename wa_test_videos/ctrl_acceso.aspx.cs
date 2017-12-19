using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wa_transcript
{
    public partial class ctrl_acceso : System.Web.UI.Page
    {
        static Guid str_id_user;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                inf_user();
            }
            else
            {

            }
        }

        private void inf_user()
        {
            int n_useradmin;
            try
            {
                using (db_transcriptEntities data_user = new db_transcriptEntities())
                {
                    var v_user = (from u in data_user.inf_usuarios
                                  where u.id_tipo_usuario == 2
                                  select u).Count();

                    n_useradmin = v_user;

                    if (n_useradmin == 0)
                    {
                        lbl_registro.Visible = true;
                        lblModalTitle.Text = "tranScript";
                        lblModalBody.Text = "No existe administrador ni juzgado en la aplicación, favor de registrarlos";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                        upModal.Update();
                    }
                    else
                    {
                        lbl_registro.Visible = false;

                    }
                }
            }
            catch
            {
                lblModalTitle.Text = "tranScript";
                lblModalBody.Text = "Sin conexión a base de datos, contactar al administrador";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                upModal.Update();
            }

        }

        protected void cmd_login_Click(object sender, EventArgs e)
        {
            string str_codeuser = txt_code_user.Text;
            string str_password = mdl_encrypta.Encrypt(txt_password.Text);
            string str_password_V;
            int? str_id_type_user, str_iduser_status;
            

            try
            {
                using (db_transcriptEntities data_user = new db_transcriptEntities())
                {
                    var items_user = (from c in data_user.inf_usuarios
                                      where c.codigo_usuario == str_codeuser
                                      select c).FirstOrDefault();

                    str_id_user = items_user.id_usuario;
                    str_password_V = items_user.clave;
                    str_id_type_user = items_user.id_tipo_usuario;
                    str_iduser_status = items_user.id_estatus;

                    if (str_password_V == str_password && str_iduser_status == 1)
                    {

                        using (var insert_user = new db_transcriptEntities())
                        {
                            var user_session = new inf_sesion
                            {
                                fecha_registro = DateTime.Now,
                                id_estatus_sesion = 1,
                                id_aspx = 1,
                                id_usuario = str_id_user
                            };
                            insert_user.inf_sesion.Add(user_session);
                            insert_user.SaveChanges();
                        }

                        Session["ss_id_user"] = mdl_user.code_user(str_codeuser);
                        Response.Redirect("ctrl_menu.aspx");
                    }
                    else
                    {
                        lblModalTitle.Text = "tranScript";
                        lblModalBody.Text = "Contraseña incorrecta, favor de contactar al Administrador.";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                        upModal.Update();


                    }
                }
            }
            catch
            {
                lblModalTitle.Text = "tranScript";
                lblModalBody.Text = "Usuario incorrecto, favor de contactar al Administrador.";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                upModal.Update();


            }
        }
    }
}