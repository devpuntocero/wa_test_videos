using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wa_transcript
{
    public partial class ctrl_registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                load_ddl();

            }
            else
            {
                if (txt_code_user.Text != "")
                {
                    txt_password.Attributes["value"] = txt_password.Text;
                }
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

            txt_name_user.Text = "";
            txt_apater.Text = "";
            txt_amater.Text = "";
            txt_code_user.Text = "";
            txt_password.Text = "";

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
            Guid id_fempresa = Guid.Parse("9A3C8442-2B53-45B7-9B5C-144BFA9C93BE");
            Guid id_centro = Guid.NewGuid();
            Guid str_iduser = Guid.NewGuid();

            string str_nameuser = txt_name_user.Text.ToUpper();
            string str_apater = txt_apater.Text.ToUpper();
            string str_amater = txt_amater.Text.ToUpper();
            string str_codeuser = txt_code_user.Text.ToLower();
            string str_password = mdl_encrypta.Encrypt(txt_password.Text.ToLower());

            string str_business_name = txt_business_name.Text.ToUpper();
            int str_municipality = Convert.ToInt32(ddl_municipality.SelectedValue);
            string str_colony = txt_colony.Text.ToUpper();
            string str_street = txt_street.Text.ToUpper();
            string str_cp = txt_cp.Text;
            string str_phone = txt_phone.Text;
            string str_phonealt = txt_phone_alt.Text;


            using (var insert_fiscal = new db_transcriptEntities())
            {
                var items_fiscal = new inf_centro
                {
                    id_estatus = 1,
                    fecha_registro = DateTime.Now,
                    nombre = str_business_name,
                    id_municipio = str_municipality,
                    colonia = str_colony,
                    calle_num = str_street,
                    cp = str_cp,
                    telefono = str_phone,
                    telefono_alt = str_phonealt,
                    id_centro = id_centro,
                    id_empresa = id_fempresa,

                };
                insert_fiscal.inf_centro.Add(items_fiscal);
                insert_fiscal.SaveChanges();
            }

            using (var insert_user = new db_transcriptEntities())
            {
                var items_user = new inf_usuarios
                {
                    id_usuario = str_iduser,
                    nombres = str_nameuser,
                    a_paterno = str_apater,
                    a_materno = str_amater,
                    id_tipo_usuario = 2,
                    codigo_usuario = str_codeuser,
                    clave = str_password,
                    id_estatus = 1,
                    fecha_registro = DateTime.Now,
                    id_centro = id_centro
                };
                insert_user.inf_usuarios.Add(items_user);
                insert_user.SaveChanges();
            }

            clean_text();
            lblModalTitle.Text = "tranScript";
            lblModalBody.Text = "Datos de administrador y juzgado guardados con éxito";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
            upModal.Update();
        }
    }
}