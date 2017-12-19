using JAVS.Networking;
using JAVS.Networking.Services;
using JAVS.Publishing.JVL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wa_transcript
{
    public partial class ctrl_conexion : System.Web.UI.Page
    {
        static Guid id_fuser;
        private WebDataProvider provider;
        private SystemConnection connection;
        private EventManagerSessionControl sessionControl;
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

            using (db_transcriptEntities data_user = new db_transcriptEntities())
            {
                var items_user = (from c in data_user.inf_credenciales
                                  select c).Count();

                if (items_user != 0)
                {
                    rb_add_credentials.Visible = false;

                }
            }


        }


        protected void chkselect_credentials(object sender, EventArgs e)
        {

            foreach (GridViewRow row in gv_credentials.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chk_select") as CheckBox);
                    if (chkRow.Checked)
                    {
                        row.BackColor = Color.Orange;
                        int str_code = Convert.ToInt32(row.Cells[1].Text);

                        using (db_transcriptEntities data_user = new db_transcriptEntities())
                        {
                            var inf_user = (from u in data_user.inf_credenciales
                                            where u.id_credenciales == str_code
                                            select new
                                            {
                                                u.ip,
                                                u.usuario,
                                                u.clave,
                                                u.fecha_registro

                                            }).FirstOrDefault();

                            txt_ip.Text = inf_user.ip;
                            txt_user.Text = inf_user.usuario;
                            txt_pass.Text = inf_user.clave;
                        }
                    }
                    else
                    {
                        row.BackColor = Color.White;
                        chkRow.Checked = false;
                    }
                }
            }
        }
        protected void cmd_save_credentials_Click(object sender, EventArgs e)
        {
            string str_ip = txt_ip.Text;
            string str_user = txt_user.Text;
            string str_pass = txt_pass.Text;
            Guid id_fcenter = Guid.Parse(lbl_idcenter.Text);
            int str_count;


            using (db_transcriptEntities data_user = new db_transcriptEntities())
            {
                var items_user = (from c in data_user.inf_credenciales
                                  select c).Count();

                str_count = items_user;
            }

            if (str_count == 0)
            {
                using (var insert_fiscal = new db_transcriptEntities())
                {
                    var items_fiscal = new inf_credenciales
                    {
                        ip = str_ip,
                        usuario = str_user,
                        clave = str_pass,
                        id_usuario = id_fuser,
                        id_centro = id_fcenter,
                        fecha_registro = DateTime.Now

                    };
                    insert_fiscal.inf_credenciales.Add(items_fiscal);
                    insert_fiscal.SaveChanges();
                }

                clean_txt();
                using (db_transcriptEntities data_user = new db_transcriptEntities())
                {
                    var inf_user = (from u in data_user.inf_credenciales
                                    where u.ip == str_ip
                                    select new
                                    {
                                        u.id_credenciales,
                                        u.ip,
                                        u.usuario,
                                        u.fecha_registro

                                    }).ToList();

                    gv_credentialsf.DataSource = inf_user;
                    gv_credentialsf.DataBind();
                    gv_credentialsf.Visible = true;
                }

                rb_add_credentials.Visible = false;

                lblModalTitle.Text = "tranScript";
                lblModalBody.Text = "Datos de conexión guardados con éxito";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                upModal.Update();

            }
            else if (rb_edit_credentials.Checked)
            {
                foreach (GridViewRow row in gv_credentials.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (row.Cells[0].FindControl("chk_select") as CheckBox);
                        if (chkRow.Checked)
                        {
                            int str_code = Convert.ToInt32(row.Cells[1].Text);

                            using (var data_address = new db_transcriptEntities())
                            {
                                var items_address = (from c in data_address.inf_credenciales
                                                     where c.id_credenciales == str_code
                                                     select c).FirstOrDefault();

                                items_address.ip = str_ip;
                                items_address.usuario = str_user;
                                items_address.clave = str_pass;
                                data_address.SaveChanges();
                            }

                            clean_txt();
                            lblModalTitle.Text = "tranScript";
                            lblModalBody.Text = "Horario de Conversión de videos, actualizado con éxito";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                            upModal.Update();

                            using (db_transcriptEntities data_user = new db_transcriptEntities())
                            {
                                var inf_user = (from u in data_user.inf_credenciales
                                                where u.ip == str_ip
                                                select new
                                                {
                                                    u.id_credenciales,
                                                    u.ip,
                                                    u.usuario,
                                                    u.fecha_registro

                                                }).ToList();

                                gv_credentials.DataSource = inf_user;
                                gv_credentials.DataBind();
                                gv_credentials.Visible = true;
                            }
                        }
                    }
                }

            }
        }

        protected void gv_usuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void rb_add_credentials_CheckedChanged(object sender, EventArgs e)
        {
            rb_edit_credentials.Checked = false;
            clean_txt();
            div_infcredentials.Visible = true;
        }

        protected void rb_edit_credentials_CheckedChanged(object sender, EventArgs e)
        {
            rb_add_credentials.Checked = false;
            div_infcredentials.Visible = true;

            gv_credentialsf.Visible = false;
            clean_txt();

            using (db_transcriptEntities data_user = new db_transcriptEntities())
            {
                var inf_user = (from u in data_user.inf_credenciales
                                select new
                                {
                                    u.id_credenciales,
                                    u.ip,
                                    u.usuario,
                                    u.fecha_registro

                                }).ToList();

                if (inf_user.Count == 0)
                {
                    rb_edit_credentials.Checked = false;

                    lblModalTitle.Text = "tranScript";
                    lblModalBody.Text = "Sin registro, favor de agregar uno";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    upModal.Update();
                }
                else
                {
                    gv_credentials.DataSource = inf_user;
                    gv_credentials.DataBind();
                    gv_credentials.Visible = true;
                }
            }
        }

        private void clean_txt()
        {
            txt_ip.Text = "";
            txt_user.Text = "";
            txt_pass.Text = "";
        }

        protected void gv_credentials_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            this.provider = new WebDataProvider();
            this.provider.Port = 80;
            this.provider.Username = "m";
            this.provider.Password = "m";

            provider.Host = txt_ip.Text;
            provider.Username = txt_user.Text;
            provider.Password = txt_pass.Text;

            GetLocations();

            ////First create a SystemConnection.
            //this.connection = new SystemConnection();

            ////To receive session notifications, subscribe to the session protocol service.
            //this.connection.ServicesSubscribed.Add(JAVS.Protocol.Session.Service.Number);

            ////Events may fire from a background thread. When handling events within a System.Windows.Forms GUI, 
            ////set the sync object to a control to avoid cross-threading issues.
            //this.connection.SyncObject = this;

            ////The Registered event will be triggered after a successful connection and the recorder
            ////is ready to handle commands.
            //this.connection.Registered += new EventHandler(connection_Registered);
            //this.connection.Disconnected += new EventHandler<DisconnectedEventArgs>(connection_Disconnected);

            ////Create an EventManagerSessionControl which provides session protocol commands and events.
            //this.sessionControl = new EventManagerSessionControl(this.connection);
            //this.sessionControl.SessionChanged += new EventHandler(sessionControl_SessionChanged);
            //this.sessionControl.RecordingChanged += new EventHandler(sessionControl_RecordingChanged);

            ////Connect to the recorder.
            //if (this.connection.Connect(this.textBoxIPAddress.Text, 6110))
            //{
            //    this.buttonDisconnect.Enabled = true;
            //}
            //else
            //{
            //    MessageBox.Show("Failed to establish connection");
            //}
        }

        private void GetLocations()
        {
            try
            {
                IList<string> locations = this.provider.GetLocations();
                DataSet ds_list = new DataSet();
                DataTable ordersTable = new DataTable();

                //ds_list.Tables = locations;
            }
            catch (Exception e)
            {
                lblModalTitle.Text = "tranScript";
                lblModalBody.Text = "Falla de conexión, favor de reitentar o contactar al administrador";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                upModal.Update();
            }
        }
    }
}