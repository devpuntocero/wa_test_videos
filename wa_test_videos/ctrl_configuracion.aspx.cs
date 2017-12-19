using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace wa_transcript
{
    public partial class ctrl_configuracion : System.Web.UI.Page
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


        protected void img_transformation_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("ctrl_agenda_conversion.aspx");
        }

        protected void img_dayvideos_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("ctrl_dias_respaldo.aspx");
        }

        protected void img_routevideos_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("ctrl_ruta_videos.aspx");
        }

        protected void img_credentials_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("ctrl_conexion.aspx");
        }
    }
}