using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wa_test_videos
{
    public partial class ctrl_acceso : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void cmd_login_Click(object sender, EventArgs e)
        {
            string str_codeuser = txt_code_user.Text;
            string str_password = mdl_encrypta.Encrypt(txt_password.Text);
            string str_password_V;
            int? str_id_type_user, str_iduser_status;
            Guid str_id_user;

            try
            {
                using (db_videos_testEntities data_user = new db_videos_testEntities())
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

                        mdl_user.str_fiduser = mdl_user.code_user(str_codeuser);

                        //Session["ss_id_user"] = str_id_user;
                        Response.Redirect("ctrl_menu.aspx");
                    }
                    else
                    {
                        lbl_err.Visible = true;
                        lbl_err.Text = "Contraseña Incorrecta, Favor de contactar al Administrador.";
                    }
                }
            }
            catch
            {
                lbl_err.Visible = true;
                lbl_err.Text = "Usuario Incorrecto, Favor de contactar al Administrador.";
            }
        }
    }
}