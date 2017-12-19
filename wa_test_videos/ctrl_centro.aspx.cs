using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wa_transcript
{
    public partial class ctrl_centro : System.Web.UI.Page
    {
        static Guid id_fuser;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                    inf_user();
                    load_ddl();

                }
                else
                {
                    inf_user();
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

        private void load_ddl()
        {

            using (db_transcriptEntities data_user = new db_transcriptEntities())
            {
                var items_user = (from c in data_user.fact_estado
                                  select c).ToList();

                ddl_state.DataSource = items_user;
                ddl_state.DataTextField = "desc_estado";
                ddl_state.DataValueField = "id_estado";
                ddl_state.DataBind();
                ddl_state.Items.Insert(0, new ListItem("--Seleccionar Estado--", "0"));
            }
            ddl_municipality.Items.Insert(0, new ListItem("--Seleccionar Municipio--", "0"));
        }

        private void clean_text()
        {

            ddl_state.SelectedValue = "0";
            ddl_municipality.SelectedValue = "0";
            txt_colony.Text = "";
            txt_street.Text = "";
            txt_cp.Text = "";
            txt_business_name.Text = "";
            txt_phone.Text = "";
            txt_phone_alt.Text = "";
        }

        private void LoadMunicipality(int id_state)
        {
            using (db_transcriptEntities data_user = new db_transcriptEntities())
            {
                var items_user = (from c in data_user.fact_municipio
                                  where c.id_estado == id_state
                                  select c).ToList();
                ddl_municipality.DataSource = items_user;
                ddl_municipality.DataTextField = "desc_municipio";
                ddl_municipality.DataValueField = "id_municipio";
                ddl_municipality.DataBind();
                ddl_municipality.Items.Insert(0, new ListItem("--Seleccionar Municipio--", "0"));
            }
        }

        protected void ddl_state_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id_state = Convert.ToInt32(ddl_state.SelectedValue);

            LoadMunicipality(id_state);
        }

        protected void cmd_save_Click(object sender, EventArgs e)
        {

            Guid id_fempresa = Guid.Parse(lbl_idcenter.Text);

            string str_business_name = txt_business_name.Text.ToUpper();

            int str_municipality = Convert.ToInt32(ddl_municipality.SelectedValue);
            string str_colony = txt_colony.Text.ToUpper();
            string str_street = txt_street.Text.ToUpper();
            string str_cp = txt_cp.Text;
            string str_phone = txt_phone.Text;
            string str_phonealt = txt_phone_alt.Text;

            using (var data_addressF = new db_transcriptEntities())
            {
                var items_addressF = (from c in data_addressF.inf_centro
                                      where c.id_centro == id_fempresa
                                      select c).FirstOrDefault();


                items_addressF.nombre = str_business_name;
                items_addressF.id_municipio = str_municipality;
                items_addressF.colonia = str_colony;
                items_addressF.calle_num = str_street;
                items_addressF.cp = str_cp;
                items_addressF.telefono = str_phone;
                items_addressF.telefono_alt = str_phonealt;

                data_addressF.SaveChanges();
            }
            chkb_editar.Checked = false;
            clean_text();

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
      
            lblModalTitle.Text = "tranScript";
            lblModalBody.Text = "Datos de juzgado actualizados con éxito";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
            upModal.Update();
        }

        protected void chkb_editar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkb_editar.Checked)
            {
                try
                {
                    Guid id_fempresa = Guid.Parse(lbl_idcenter.Text);

                    using (db_transcriptEntities data_user = new db_transcriptEntities())
                    {
                        var inf_empresa = (from i_c in data_user.inf_centro
                                           join i_m in data_user.fact_municipio on i_c.id_municipio equals i_m.id_municipio
                                           join i_e in data_user.fact_estado on i_m.id_estado equals i_e.id_estado

                                           where i_c.id_centro == id_fempresa

                                           select new
                                           {
                                               i_c.nombre,
                                               i_e.id_estado,
                                               i_c.id_municipio,
                                               i_c.colonia,
                                               i_c.calle_num,
                                               i_c.cp,
                                               i_c.telefono,
                                               i_c.telefono_alt

                                           }).FirstOrDefault();

                        txt_business_name.Text = inf_empresa.nombre;
                        ddl_state.SelectedValue = inf_empresa.id_estado.ToString();
                        LoadMunicipality(inf_empresa.id_estado);
                        ddl_municipality.SelectedValue = inf_empresa.id_municipio.ToString();
                        txt_colony.Text = inf_empresa.colonia;
                        txt_street.Text = inf_empresa.calle_num;
                        txt_cp.Text = inf_empresa.cp;
                        txt_phone.Text = inf_empresa.telefono;
                        txt_phone_alt.Text = inf_empresa.telefono_alt;
                    }
                }
                catch
                {

                }
            }
            else
            {
                clean_text();
            }
        }
    }
}