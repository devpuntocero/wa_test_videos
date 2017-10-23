using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wa_test_videos
{
    public partial class ctrl_material : System.Web.UI.Page
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

            using (db_videos_testEntities data_user = new db_videos_testEntities())
            {
                var inf_user = (from i_u in data_user.inf_usuarios
                                join i_tu in data_user.fact_tipo_usuarios on i_u.id_tipo_usuario equals i_tu.id_tipo_usuario
                                join i_e in data_user.inf_centro on i_u.id_centro equals i_e.id_centro
                                where i_u.id_usuario == id_user
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

                int str_id_type_user = inf_user.id_tipo_usuario;
                switch (str_id_type_user)
                {

                    case 1:
                        div_configuration.Visible = true;

                        break;
                    case 2:
                        div_configuration.Visible = true;

                        break;
                    case 3:
                        div_configuration.Visible = true;

                        break;
                    case 4:
                        div_configuration.Visible = false;
                        break;
                }
            }
        }

        protected void img_videos_load_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("ctrl_videos_carga.aspx");
        }

        protected void img_transformation_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("ctrl_transformar.aspx");
        }

        protected void img_configuration_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("ctrl_configuracion.aspx");
        }
    }
}