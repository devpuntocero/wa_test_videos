using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wa_transcript
{
    public partial class ctrl_ruta_videos : System.Web.UI.Page
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
            using (db_transcriptEntities data_user = new db_transcriptEntities())
            {
                var items_user = (from c in data_user.inf_ruta_videos
                                  select c).Count();

                if (items_user != 0)
                {
                    rb_add_routevideos.Visible = false;
                }
            }
        }

        protected void cmd_save_path_Click(object sender, EventArgs e)
        {

            Guid id_fcenter = Guid.Parse(lbl_idcenter.Text);
            int str_count, str_fpath;

            string str_user = txt_user.Text;
            string str_pass = txt_pass.Text;
            var networkPath = txt_path_videos.Text;

            try
            {
                using (new NetworkConnection(networkPath, new NetworkCredential(str_user, str_pass)))
                {

                    using (db_transcriptEntities data_user = new db_transcriptEntities())
                    {
                        var items_user = (from c in data_user.inf_ruta_videos
                                          select c).Count();

                        str_count = items_user;
                    }

                    if (str_count == 0)
                    {
                        using (var insert_fiscal = new db_transcriptEntities())
                        {
                            var items_fiscal = new inf_ruta_videos
                            {
                                desc_ruta_fin = @"C:\inetpub\wwwroot\videos",
                                ruta_user_ini = str_user,
                                ruta_pass_ini = str_pass,
                                desc_ruta_ini = networkPath,
                                id_usuario = id_fuser,

                                id_centro = id_fcenter,
                                fecha_registro = DateTime.Now

                            };
                            insert_fiscal.inf_ruta_videos.Add(items_fiscal);
                            insert_fiscal.SaveChanges();
                        }

                        using (db_transcriptEntities data_user = new db_transcriptEntities())
                        {
                            var inf_user = (from u in data_user.inf_ruta_videos

                                            select new
                                            {

                                                u.id_ruta_videos,
                                                u.desc_ruta_ini,
                                                u.fecha_registro

                                            }).ToList();

                            gv_routevideosf.DataSource = inf_user;
                            gv_routevideosf.DataBind();
                            gv_routevideosf.Visible = true;
                        }
                        txt_user.Text = "";
                        txt_pass.Text = "";
                        txt_path_videos.Text = "";
                        rb_add_routevideos.Visible = false;

                        lblModalTitle.Text = "tranScript";
                        lblModalBody.Text = "Ruta de de videos guardada con éxito";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                        upModal.Update();

                    }
                    else
                    {
                        using (db_transcriptEntities data_user = new db_transcriptEntities())
                        {
                            var items_user = (from c in data_user.inf_ruta_videos
                                              select c).FirstOrDefault();

                            str_fpath = items_user.id_ruta_videos;
                        }

                        using (var data_address = new db_transcriptEntities())
                        {
                            var items_address = (from c in data_address.inf_ruta_videos
                                                 where c.id_ruta_videos == str_fpath
                                                 select c).FirstOrDefault();

                            items_address.ruta_user_ini = str_user;
                            items_address.ruta_pass_ini = str_pass;
                            items_address.desc_ruta_ini = networkPath;
                            data_address.SaveChanges();
                        }
                        txt_user.Text = "";
                        txt_pass.Text = "";
                        txt_path_videos.Text = "";

                        using (db_transcriptEntities data_user = new db_transcriptEntities())
                        {
                            var inf_user = (from u in data_user.inf_ruta_videos

                                            select new
                                            {

                                                u.id_ruta_videos,
                                                u.desc_ruta_ini,
                                                u.fecha_registro

                                            }).ToList();

                            gv_routevideos.DataSource = inf_user;
                            gv_routevideos.DataBind();
                            gv_routevideos.Visible = true;
                        }
                        lblModalTitle.Text = "tranScript";
                        lblModalBody.Text = "Ruta de de videos actualizada con éxito";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                        upModal.Update();
                    }
                }
            }
            catch (DirectoryNotFoundException ex)
            {
                string str_msj = ex.Message.ToString();

                lblModalTitle.Text = "tranScript";
                lblModalBody.Text = str_msj;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                upModal.Update();
            }
            //if (Directory.Exists(str_path_video))
            //{

            //}
            //else
            //{

            //    lblModalTitle.Text = "tranScript";
            //    lblModalBody.Text = "Directorio invalido, favor de reintentar";
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
            //    upModal.Update();
            //}


        }

        protected void rb_add_routevideos_CheckedChanged(object sender, EventArgs e)
        {
            rb_edit_routevideos.Checked = false;
            div_ruta_videos.Visible = true;
            gv_routevideosf.Visible = false;
            gv_routevideos.Visible = false;
            txt_path_videos.Text = "";
        }

        protected void rb_edit_routevideos_CheckedChanged(object sender, EventArgs e)
        {
            rb_add_routevideos.Checked = false;
            div_ruta_videos.Visible = true;
            gv_routevideosf.Visible = false;
            txt_path_videos.Text = "";

            using (db_transcriptEntities data_user = new db_transcriptEntities())
            {
                var inf_user = (from u in data_user.inf_ruta_videos
                                select new
                                {
                                    u.id_ruta_videos,
                                    u.desc_ruta_ini,
                                    u.fecha_registro

                                }).ToList();

                if (inf_user.Count == 0)
                {
                    rb_edit_routevideos.Checked = false;
                    div_ruta_videos.Visible = false;
                    txt_path_videos.Text = "";

                    lblModalTitle.Text = "tranScript";
                    lblModalBody.Text = "Sin registro, favor de agregar uno";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    upModal.Update();
                }
                else
                {

                    gv_routevideos.DataSource = inf_user;
                    gv_routevideos.DataBind();
                    gv_routevideos.Visible = true;
                }
            }
        }

        protected void chkselect_routevideos(object sender, EventArgs e)
        {

            foreach (GridViewRow row in gv_routevideos.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chk_routevideos") as CheckBox);
                    if (chkRow.Checked)
                    {
                        row.BackColor = Color.Orange;
                        int str_code = Convert.ToInt32(row.Cells[1].Text);

                        using (db_transcriptEntities data_user = new db_transcriptEntities())
                        {
                            var inf_user = (from u in data_user.inf_ruta_videos
                                            select new
                                            {
                                                u.id_ruta_videos,
                                                u.desc_ruta_ini,
                                                u.fecha_registro

                                            }).FirstOrDefault();

                            txt_path_videos.Text = inf_user.desc_ruta_ini;
                        }
                    }
                    else
                    {
                        row.BackColor = Color.White;
                    }
                }
            }
        }



        protected void cmd_test_path_Click(object sender, EventArgs e)
        {

            string str_user = txt_user.Text;
            string str_pass = txt_pass.Text;
            var networkPath = txt_path_videos.Text;

            try
            {
                using (new NetworkConnection(networkPath, new NetworkCredential(str_user, str_pass)))
                {

                    lblModalTitle.Text = "tranScript";
                    lblModalBody.Text = "Ruta y credenciales correcto";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    upModal.Update();
                }
            }
            catch
            {

                lblModalTitle.Text = "tranScript";
                lblModalBody.Text = lblModalBody.Text = "Ruta y credenciales incorrecto,favor de verificar o contactar al Administrador";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                upModal.Update();
            }
        }

        public class NetworkConnection : IDisposable
        {
            #region Variables

            /// <summary>
            /// The full path of the directory.
            /// </summary>
            private readonly string _networkName;

            #endregion

            #region Constructors

            /// <summary>
            /// Initializes a new instance of the <see cref="NetworkConnection"/> class.
            /// </summary>
            /// <param name="networkName">
            /// The full path of the network share.
            /// </param>
            /// <param name="credentials">
            /// The credentials to use when connecting to the network share.
            /// </param>
            public NetworkConnection(string networkName, NetworkCredential credentials)
            {
                _networkName = networkName;

                var netResource = new NetResource
                {
                    Scope = ResourceScope.GlobalNetwork,
                    ResourceType = ResourceType.Disk,
                    DisplayType = ResourceDisplaytype.Share,
                    RemoteName = networkName.TrimEnd('\\')
                };

                var result = WNetAddConnection2(
                    netResource, credentials.Password, credentials.UserName, 0);

                if (result != 0)
                {
                    throw new Win32Exception(result);
                }
            }

            #endregion

            #region Events

            /// <summary>
            /// Occurs when this instance has been disposed.
            /// </summary>
            public event EventHandler<EventArgs> Disposed;

            #endregion

            #region Public methods

            /// <summary>
            /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
            /// </summary>
            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            #endregion

            #region Protected methods

            /// <summary>
            /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
            /// </summary>
            /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
            protected virtual void Dispose(bool disposing)
            {
                if (disposing)
                {
                    var handler = Disposed;
                    if (handler != null)
                        handler(this, EventArgs.Empty);
                }

                WNetCancelConnection2(_networkName, 0, true);
            }

            #endregion

            #region Private static methods

            /// <summary>
            ///The WNetAddConnection2 function makes a connection to a network resource. The function can redirect a local device to the network resource.
            /// </summary>
            /// <param name="netResource">A <see cref="NetResource"/> structure that specifies details of the proposed connection, such as information about the network resource, the local device, and the network resource provider.</param>
            /// <param name="password">The password to use when connecting to the network resource.</param>
            /// <param name="username">The username to use when connecting to the network resource.</param>
            /// <param name="flags">The flags. See http://msdn.microsoft.com/en-us/library/aa385413%28VS.85%29.aspx for more information.</param>
            /// <returns></returns>
            [DllImport("mpr.dll")]
            private static extern int WNetAddConnection2(NetResource netResource,
                                                         string password,
                                                         string username,
                                                         int flags);

            /// <summary>
            /// The WNetCancelConnection2 function cancels an existing network connection. You can also call the function to remove remembered network connections that are not currently connected.
            /// </summary>
            /// <param name="name">Specifies the name of either the redirected local device or the remote network resource to disconnect from.</param>
            /// <param name="flags">Connection type. The following values are defined:
            /// 0: The system does not update information about the connection. If the connection was marked as persistent in the registry, the system continues to restore the connection at the next logon. If the connection was not marked as persistent, the function ignores the setting of the CONNECT_UPDATE_PROFILE flag.
            /// CONNECT_UPDATE_PROFILE: The system updates the user profile with the information that the connection is no longer a persistent one. The system will not restore this connection during subsequent logon operations. (Disconnecting resources using remote names has no effect on persistent connections.)
            /// </param>
            /// <param name="force">Specifies whether the disconnection should occur if there are open files or jobs on the connection. If this parameter is FALSE, the function fails if there are open files or jobs.</param>
            /// <returns></returns>
            [DllImport("mpr.dll")]
            private static extern int WNetCancelConnection2(string name, int flags, bool force);

            #endregion

            /// <summary>
            /// Finalizes an instance of the <see cref="NetworkConnection"/> class.
            /// Allows an <see cref="System.Object"></see> to attempt to free resources and perform other cleanup operations before the <see cref="System.Object"></see> is reclaimed by garbage collection.
            /// </summary>
            ~NetworkConnection()
            {
                Dispose(false);
            }
        }

        #region Objects needed for the Win32 functions
#pragma warning disable 1591

        /// <summary>
        /// The net resource.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public class NetResource
        {
            public ResourceScope Scope;
            public ResourceType ResourceType;
            public ResourceDisplaytype DisplayType;
            public int Usage;
            public string LocalName;
            public string RemoteName;
            public string Comment;
            public string Provider;
        }

        /// <summary>
        /// The resource scope.
        /// </summary>
        public enum ResourceScope
        {
            Connected = 1,
            GlobalNetwork,
            Remembered,
            Recent,
            Context
        };

        /// <summary>
        /// The resource type.
        /// </summary>
        public enum ResourceType
        {
            Any = 0,
            Disk = 1,
            Print = 2,
            Reserved = 8,
        }

        /// <summary>
        /// The resource displaytype.
        /// </summary>
        public enum ResourceDisplaytype
        {
            Generic = 0x0,
            Domain = 0x01,
            Server = 0x02,
            Share = 0x03,
            File = 0x04,
            Group = 0x05,
            Network = 0x06,
            Root = 0x07,
            Shareadmin = 0x08,
            Directory = 0x09,
            Tree = 0x0a,
            Ndscontainer = 0x0b
        }
#pragma warning restore 1591
        #endregion
    }
}