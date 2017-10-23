using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wa_test_videos
{
    public partial class ctrl_seguimiento : System.Web.UI.Page
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
        protected void chk_OnCheckedChanged(object sender, EventArgs e)
        {
            Panel1.Visible = false;
            lbl_mnsj.Visible = false;
            div_panel_ie.Visible = false;

        }

        protected void rb_load_CheckedChanged(object sender, EventArgs e)
        {
            lbl_mnsj.Visible = false;
            rb_active.Checked = false;
        }

        protected void rb_active_CheckedChanged(object sender, EventArgs e)
        {
            rb_load.Checked = false;
            lbl_mnsj.Visible = false;
        }

        protected void cmd_ver_Click(object sender, EventArgs e)
        {
            lbl_mnsj.Visible = false;


            System.Web.HttpBrowserCapabilities browser = Request.Browser;
            string s = browser.Browser;

            if (s == "Chrome")
            {
                div_panel_ie.Visible = false;
                div_panel.Visible = true;
            }
            else if (s == "Firefox")
            {
                div_panel_ie.Visible = false;
                div_panel.Visible = true;
            }
            else if (s == "InternetExplorer")
            {
                div_panel_ie.Visible = true;
            }

            foreach (GridViewRow row in gv_files.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[3].FindControl("chk_select") as CheckBox);
                    if (chkRow.Checked)
                    {
                        string str_namefile = "videos/" + row.Cells[3].Text;

                        //play_video.Visible = true;
                        //play_video.Attributes["src"] = str_namefile;
                        Panel1.Visible = true;
                        Panel1.Controls.Add(CrearControlVideo(str_namefile));
                    }
                }
            }

        }
        private Control CrearControlVideo(string str_namefile)
        {
            StringBuilder sa = new StringBuilder();
            // sa.Append("<center>");
            sa.Append("<OBJECT ID=\"Player\" Object Type=\"video/x-ms-wmv\" width=\"640\" height=\"480\" VIEWASTEXT > ");
            sa.Append("<PARAM name=\"autoStart\" value=\"false\">");
            sa.Append(string.Format("<PARAM name=\"SRC\" value=\"{0}\">", str_namefile));// IE needs this extra push when using MIME type not class id
            sa.Append(string.Format("<PARAM name=\"URL\" value=\"{0}\">", str_namefile));
            sa.Append("<PARAM name=\"AutoSize\" value=\"False\"");
            sa.Append("<PARAM name=\"rate\" value=\"1\">");
            sa.Append("<PARAM name=\"balance\" value=\"0\">");
            sa.Append("<PARAM name=\"enabled\" value=\"true\">");
            sa.Append("<PARAM name=\"enabledContextMenu\" value=\"true\">");
            sa.Append("<PARAM name=\"fullScreen\" value=\"false\">");
            sa.Append("<PARAM name=\"playCount\" value=\"1\">");
            sa.Append("<PARAM name=\"volume\" value=\"30\">  ");
            sa.Append("</OBJECT>");
            //  sa.Append("</center>");

            return new LiteralControl(sa.ToString());
        }

        protected void cmd_search_Click(object sender, EventArgs e)
        {
            lbl_mnsj.Visible = false;
            lbl_mnsj.Text = "";
            int str_idload;
            if (rb_load.Checked || rb_active.Checked)
            {
                if (rb_load.Checked)
                {
                    str_idload = 1;
                    filter_videos(str_idload);
                    lbl_status.Visible = true;
                    ddl_estatus.Visible = true;
                    cmd_save.Visible = true;
                }

                else if (rb_active.Checked)
                {
                    str_idload = 3;
                    filter_videos(str_idload);
                    lbl_status.Visible = false;
                    ddl_estatus.Visible = false;
                    cmd_save.Visible = false;
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
            string str_expediente = txt_expedient.Text;
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
                                    where inf_m.fecha_registro >= str_fdateini && inf_m.fecha_registro <= str_fdatefin
                                    where inf_m.expediente == str_expediente
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
                                    where inf_m.expediente == str_expediente
                                    where inf_m.fecha_registro >= str_fdateini && inf_m.fecha_registro <= str_fdatefin
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
        }

        protected void MyButtonClick(object sender, System.EventArgs e)
        {
            //Get the button that raised the event
            Button btn = (Button)sender;

            //Get the row that contains this button
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;

            foreach (GridViewRow row in gv_files.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[3].FindControl("chk_select") as CheckBox);
                    if (chkRow.Checked)
                    {
                        string str_namefile = "videos/" + row.Cells[3].Text;

                        //play_video.Visible = true;
                        //play_video.Attributes["src"] = str_namefile;
                        Panel1.Controls.Add(CrearControlVideo(str_namefile));
                    }
                }
            }
        }

        protected void cmd_save_Click(object sender, EventArgs e)
        {
            Panel1.Visible = false;
            if (ddl_estatus.SelectedValue != "Seleccionar")
            {
                int str_estatus;

                foreach (GridViewRow row in gv_files.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (row.Cells[3].FindControl("chk_select") as CheckBox);
                        if (chkRow.Checked)
                        {
                            string str_namefile = row.Cells[3].Text;


                            if (rb_load.Checked)
                            {

                                if (ddl_estatus.SelectedValue == "Bien")
                                {
                                    str_estatus = 1;
                                    using (var data_user = new db_videos_testEntities())
                                    {
                                        var items_user = (from c in data_user.inf_material
                                                          where c.archivo == str_namefile
                                                          select c).FirstOrDefault();

                                        items_user.id_estatus_qa = str_estatus;
                                        items_user.id_estatus_material = 3;


                                        data_user.SaveChanges();
                                    }
                                }
                                else
                                {
                                    str_estatus = 2;
                                    using (var data_user = new db_videos_testEntities())
                                    {
                                        var items_user = (from c in data_user.inf_material
                                                          where c.archivo == str_namefile
                                                          select c).FirstOrDefault();

                                        items_user.id_estatus_qa = str_estatus;
                                        items_user.id_estatus_material = 1;


                                        data_user.SaveChanges();
                                    }
                                }

                            }

                            lbl_mnsj.Visible = true;
                            lbl_mnsj.Text = "Registro actualizado con Exito.";
                        }
                    }
                }
            }
            else
            {
                lbl_mnsj.Visible = true;
                lbl_mnsj.Text = "Favor de seleccionar estatus.";
            }
        }
    }
}