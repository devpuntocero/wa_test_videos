using NReco.VideoConverter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wa_transcript
{

    public partial class AsyncForm : System.Web.UI.Page
    {

        //IAsyncResult RecuperarDatos(object sender, EventArgs e, AsyncCallback cb, object state)
        //{
        //}
        //void CargarGrilla(IAsyncResult ar)
        //{
        //}

    }
    public partial class ctrl_conversion : System.Web.UI.Page
    {
        static Guid id_fuser;
        static string str_session, str_video;
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
        protected void chk_OnCheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gv_files.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[3].FindControl("chk_select") as CheckBox);

                    if (chkRow.Checked)
                    {
                        row.BackColor = Color.Orange;

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

            using (var data_user = new db_transcriptEntities())
            {
                var items_user = (from c in data_user.inf_material
                                  where c.id_estatus_material == 6
                                  select c).Count();

                if (items_user != 0)
                {
                    lblModalTitle.Text = "tranScript";
                    lblModalBody.Text = "Se estan convirtiendo videos, favor de esperar";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    upModal.Update();

                }
                else
                {
                    var two_user = new int?[] { 1, 5 };
                    flist_user(two_user);
                }

            }
        }
        private void flist_user(int?[] str_idload)
        {
            CultureInfo en = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = en;
            string str_dateini = txt_dateini.Text;
            string str_datefin = txt_datefin.Text;

            DateTime str_fdateini = DateTime.Parse(str_dateini);
            DateTime str_fdatefin = DateTime.Parse(str_datefin);
     
            if (lbl_idprofileuser.Text == "4")
            {
                using (db_transcriptEntities data_user = new db_transcriptEntities())
                {
                    var inf_user = (from inf_m in data_user.inf_material
                                    join inf_em in data_user.fact_estatus_material on inf_m.id_estatus_material equals inf_em.id_estatus_material
                                    where str_idload.Contains(inf_m.id_estatus_material)
                                    where inf_m.fecha_registro >= str_fdateini && inf_m.fecha_registro <= str_fdatefin
                                    select new
                                    {
                                        inf_m.sesion,
                                        inf_m.titulo,
                                        inf_m.localizacion,
                                        inf_m.tipo,
                                        inf_m.archivo,
                                        inf_m.duracion,
                                        inf_m.fecha_registro,
                                        inf_em.desc_estatus_material,
                                        inf_m.id_control

                                    }).ToList();

                    gv_files.DataSource = inf_user;
                    gv_files.DataBind();
                    gv_files.Visible = true;

                }
            }
            else
            {
                using (db_transcriptEntities data_user = new db_transcriptEntities())
                {
                    var inf_user = (from inf_m in data_user.inf_material
                                    join inf_em in data_user.fact_estatus_material on inf_m.id_estatus_material equals inf_em.id_estatus_material
                                    where str_idload.Contains(inf_m.id_estatus_material)
                                    where inf_m.fecha_registro >= str_fdateini && inf_m.fecha_registro <= str_fdatefin
                                    select new
                                    {
                                        inf_m.sesion,
                                        inf_m.titulo,
                                        inf_m.localizacion,
                                        inf_m.tipo,
                                        inf_m.archivo,
                                        inf_m.duracion,
                                        inf_m.fecha_registro,
                                        inf_em.desc_estatus_material,
                                        inf_m.id_control

                                    }).ToList();

                    gv_files.DataSource = inf_user;
                    gv_files.DataBind();
                    gv_files.Visible = true;
                    //cmd_save.Visible = false;

                }
            }
        }

        protected void gv_files_RowCommand(object sender, GridViewCommandEventArgs e)
        {


            foreach (GridViewRow row in gv_files.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[3].FindControl("chk_select") as CheckBox);
                    if (chkRow.Checked)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            Button btnButton = (Button)row.FindControl("cmd_action");
                            if (row.Cells[8].Text == "ERROR")
                            {
                                str_session = row.Cells[1].Text;
                                str_video = row.Cells[5].Text;

                                using (var data_mat = new db_transcriptEntities())
                                {
                                    var items_mat = (from c in data_mat.inf_material
                                                     where c.sesion == str_session
                                                     select c).FirstOrDefault();

                                    items_mat.id_estatus_material = 6;

                                    data_mat.SaveChanges();
                                }
                                var two_user = new int?[] { 1, 6 };
                                flist_user(two_user);

                                lblModalTitle.Text = "tranScript";
                                lblModalBody.Text = "Comienza proceso de Conversión, favor de esperar.";
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                                upModal.Update();

                            }
                            else if (row.Cells[8].Text == "CARGADO")
                            {
                                Guid id_fcenter = Guid.Parse(lbl_idcenter.Text);
                                div_panel.Visible = true;
                                UpdatePanel2.Update();
                                using (var insert_fiscal = new db_transcriptEntities())
                                {
                                    var items_fiscal = new inf_log_videos
                                    {
                                        sesion = str_session,
                                        video = str_video,
                                        id_usuario = id_fuser,
                                        id_centro = id_fcenter,
                                        fecha_registro = DateTime.Now,
                                        fecha_registro_alt = DateTime.Now
                                    };

                                    insert_fiscal.inf_log_videos.Add(items_fiscal);
                                    insert_fiscal.SaveChanges();
                                }

                                string str_namefile = @"videos\" + row.Cells[5].Text;

                                play_video.Visible = true;
                                play_video.Attributes["src"] = str_namefile;
                            }
                        }
                    }
                }
            }  
        }
        private static void DoWork(object sender, DoWorkEventArgs e)
        {
            // Long running background operation
            
            using (var data_user = new db_transcriptEntities())
            {
                var items_user = (from c in data_user.inf_material
                                  where c.id_estatus_material == 6
                                  select c).ToList();

                foreach (var item in items_user)
                {
                    string str_path_ini, str_path_fin;

                    using (db_transcriptEntities data_path = new db_transcriptEntities())
                    {
                        var count_path = (from c in data_path.inf_ruta_videos
                                          select c).FirstOrDefault();

                        str_path_fin = count_path.desc_ruta_fin;
                        str_path_ini = count_path.desc_ruta_fin + "\\" + str_video;


                    }
                    str_path_ini = str_path_fin + "\\" + item.archivo.ToString().Replace(".mp4", ".wmv");

                    string str_file_save = str_path_ini.ToString();
                    string str_save_file = str_path_ini.ToString().Replace(".wmv", ".mp4");
                    var ffMpeg = new NReco.VideoConverter.FFMpegConverter();

                    var ffProbe = new NReco.VideoInfo.FFProbe();
                    var videoInfo = ffProbe.GetMediaInfo(str_file_save);

                    string str_duration_wmv = videoInfo.Duration.Hours + ":" + videoInfo.Duration.Minutes + ":" + videoInfo.Duration.Seconds;

                    try
                    {

                        using (var data_mat = new db_transcriptEntities())
                        {
                            var items_mat = (from c in data_mat.inf_material
                                             where c.sesion == str_session
                                             select c).FirstOrDefault();

                            items_mat.id_estatus_material = 6;

                            data_mat.SaveChanges();
                        }

                        var two_user = new int?[] { 6 };

                        ffMpeg.ConvertMedia(str_file_save, str_save_file, Format.mp4);


                        using (var data_mat = new db_transcriptEntities())
                        {
                            var items_mat = (from c in data_mat.inf_material
                                             where c.sesion == str_session
                                             select c).FirstOrDefault();

                            items_mat.id_estatus_material = 1;

                            data_mat.SaveChanges();
                        }

                        two_user = new int?[] { 1 };



                    }
                    catch
                    {
                        using (var data_mat = new db_transcriptEntities())
                        {
                            var items_mat = (from c in data_mat.inf_material
                                             where c.sesion == str_session
                                             select c).FirstOrDefault();

                            items_mat.id_estatus_material = 5;

                            data_mat.SaveChanges();
                        }

                        
                    }
                    var videoInfo_mp4 = ffProbe.GetMediaInfo(str_file_save);

                    string str_duration_mp4 = videoInfo_mp4.Duration.Hours + ":" + videoInfo_mp4.Duration.Minutes + ":" + videoInfo_mp4.Duration.Seconds;

                    if (str_duration_wmv == str_duration_mp4)
                    {
                        File.Delete(str_file_save);

                    }
                    else
                    {
                        using (var data_mat = new db_transcriptEntities())
                        {
                            var items_mat = (from c in data_user.inf_material
                                             where c.sesion == str_session
                                             select c).FirstOrDefault();

                            items_mat.id_estatus_material = 5;

                            data_user.SaveChanges();
                        }
                    }
                }
            }
        }
        private static void WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // log when the worker is completed.
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

        protected void gv_files_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Button btnButton = (Button)e.Row.FindControl("cmd_action");
                if (e.Row.Cells[8].Text == "ERROR")
                {

                    btnButton.Text = "Convertir";
                    btnButton.Enabled = true;
                }
                else if (e.Row.Cells[8].Text == "CARGADO")
                {
                    btnButton.Text = "Visualizar";
                    btnButton.Enabled = true;
                }
                else if (e.Row.Cells[8].Text == "CONVIRTIENDO")
                {
                    btnButton.Text = "Esperar";
                    btnButton.Enabled = false;
                }
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gv_files.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    if (row.Cells[8].Text == "CONVIRTIENDO")
                    {
                        using (var data_user = new db_transcriptEntities())
                        {
                            var items_user = (from c in data_user.inf_material
                                              where c.id_estatus_material == 6
                                              select c).Count();

                            if (items_user != 0)
                            {
                                BackgroundWorker worker = new BackgroundWorker();
                                worker.DoWork += new DoWorkEventHandler(DoWork);
                                worker.WorkerReportsProgress = false;

                                worker.RunWorkerAsync();
                            }
                            else
                            {
                                var two_user = new int?[] { 1, 5 };
                                flist_user(two_user);
                            }

                        }
                    }

                    
                }
            }
            
        }
        void worker_DoWork(ref int progress,
          ref object result, params object[] arguments)
        {
            // Get the value which passed to this operation.
            string input = string.Empty;
            if (arguments.Length > 0)
            {
                input = arguments[0].ToString();
            }

            // Need 10 seconds to complete this operation.
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(100);

                progress += 1;
            }

            // The operation is completed.
            progress = 100;
            result = "Operation is completed. The input is \"" + input + "\".";
        }
    }
}