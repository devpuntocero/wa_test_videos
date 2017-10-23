using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wa_test_videos
{
    public partial class ctrl_videos_consultados : System.Web.UI.Page
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

                lbl_name.Text = inf_user.nombres + " " + inf_user.a_paterno + " " + inf_user.a_materno;
                lbl_profile_user.Text = inf_user.desc_tipo_usuario;
                lbl_id_profile_user.Text = inf_user.id_tipo_usuario.ToString();
                lbl_user_centerCP.Text = inf_user.nombre;
                lbl_id_centerCP.Text = inf_user.id_centro.ToString();


            }
        }

        protected void cmd_search_Click(object sender, EventArgs e)
        {
            lbl_mnsj.Visible = false;
            lbl_mnsj.Text = "";
            int str_idload;
            if (rb_active.Checked)
            {
                if (rb_active.Checked)
                {
                    str_idload = 1;
                    filter_videos(str_idload);


                }

                else if (rb_active.Checked)
                {
                    str_idload = 3;
                    filter_videos(str_idload);


                }


                DataControlField dataControlField = gv_files.Columns.Cast<DataControlField>().SingleOrDefault(x => x.HeaderText == "Ver");
                int str_id_type_user = Convert.ToInt32(lbl_id_profile_user.Text);
                switch (str_id_type_user)
                {

                    case 1:


                        break;
                    case 2:




                        if (dataControlField != null)
                            dataControlField.Visible = true;

                        break;
                    case 3:

                        if (dataControlField != null)
                            dataControlField.Visible = true;

                        break;
                    case 4:

                        if (dataControlField != null)
                            dataControlField.Visible = false;

                        break;
                }

            }
            else
            {
                lbl_mnsj.Visible = true;
                lbl_mnsj.Text = "Favor de seleccionar una acción";
            }
        }
        private void filter_videos(int str_idload)
        {

            DateTime str_fdateini = Convert.ToDateTime(txt_dateini.Text);
            DateTime str_fdatefin = Convert.ToDateTime(txt_datefin.Text);
            Guid id_user = mdl_user.str_fiduser;
            if (lbl_id_profile_user.Text == "4")
            {
                using (db_videos_testEntities data_user = new db_videos_testEntities())
                {
                    var inf_user = (from inf_u in data_user.inf_usuarios
                                    join inf_m in data_user.inf_material on inf_u.id_usuario equals inf_m.id_usuario
                                    join inf_em in data_user.fact_estatus_material on inf_m.id_estatus_material equals inf_em.id_estatus_material
                                    where inf_u.id_usuario == id_user
                                    where inf_m.id_estatus_material == str_idload
                                    where inf_m.fecha_registro <= str_fdatefin

                                    select new
                                    {
                                        inf_u.codigo_usuario,
                                        inf_u.nombres,
                                        inf_u.a_paterno,
                                        inf_u.a_materno,
                                        inf_m.expediente,

                                        inf_m.archivo,
                                        inf_m.bits,
                                        inf_m.fecha_registro,


                                    }).ToList();

                    gv_files.DataSource = inf_user;
                    gv_files.DataBind();
                    gv_files.Visible = true;

                }
            }
            else
            {
                using (db_videos_testEntities data_user = new db_videos_testEntities())
                {
                    var inf_user = (from inf_u in data_user.inf_usuarios
                                    join inf_m in data_user.inf_material on inf_u.id_usuario equals inf_m.id_usuario
                                    join inf_em in data_user.fact_estatus_material on inf_m.id_estatus_material equals inf_em.id_estatus_material
                                    where inf_m.id_estatus_material == str_idload
                                    where inf_m.fecha_registro <= str_fdatefin
                                    select new
                                    {
                                        inf_u.codigo_usuario,
                                        inf_u.nombres,
                                        inf_u.a_paterno,
                                        inf_u.a_materno,
                                        inf_m.expediente,

                                        inf_m.archivo,
                                        inf_m.bits,
                                        inf_m.fecha_registro,
                                        Percentage = ""

                                    }).ToList();

                    gv_files.DataSource = inf_user;
                    gv_files.DataBind();
                    gv_files.Visible = true;
                    //cmd_save.Visible = false;

                }
            }
        }
    }
}