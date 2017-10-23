using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wa_test_videos
{
    public partial class ctrl_perfil : System.Web.UI.Page
    {
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

                }
            }
            catch
            {
                Response.Redirect("ctrl_acceso.aspx");
            }
        }

        private void load_ddl()
        {
            using (db_videos_testEntities entities = new db_videos_testEntities())
            {
                var data_gender = entities.fact_generos.ToList();

                ddl_gender.DataSource = data_gender;
                ddl_gender.DataTextField = "desc_genero";
                ddl_gender.DataValueField = "id_genero";
                ddl_gender.DataBind();
                ddl_gender.Items.Insert(0, new ListItem("--Seleccionar Género--", "0"));
            }

            using (db_videos_testEntities data_user = new db_videos_testEntities())
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

        private void inf_user()
        {
            Guid id_user = mdl_user.str_fiduser;
            lbl_idfuser.Text = id_user.ToString();
            Guid id_fuser = Guid.Parse(lbl_idfuser.Text);

            using (db_videos_testEntities data_user = new db_videos_testEntities())
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

                int str_id_type_user = inf_user.id_tipo_usuario;
                switch (str_id_type_user)
                {

                    case 1:


                        break;
                    case 2:


                        break;
                    case 3:


                        break;
                    case 4:

                        break;
                }
            }
        }
        protected void cmd_save_Click(object sender, EventArgs e)
        {
            int str_gender = Convert.ToInt32(ddl_gender.SelectedValue);
            DateTime str_birthday = Convert.ToDateTime(txt_birthday.Text);
            string str_nameuser = txt_name_user.Text.ToUpper();
            string str_apater = txt_apater.Text.ToUpper();
            string str_amater = txt_amater.Text.ToUpper();
            string str_codeuser = txt_code_user.Text.ToLower();
            string str_password = mdl_encrypta.Encrypt(txt_password.Text.ToLower());

            Guid id_user = mdl_user.str_fiduser;

            using (var data_user = new db_videos_testEntities())
            {
                var items_user = (from c in data_user.inf_usuarios
                                  where c.id_usuario == id_user
                                  select c).FirstOrDefault();

                items_user.codigo_usuario = str_codeuser;
                items_user.nombres = str_nameuser;
                items_user.a_paterno = str_apater;
                items_user.a_materno = str_amater;
                items_user.clave = str_password;
                items_user.id_genero = str_gender;
                items_user.fecha_nacimiento = str_birthday;

                data_user.SaveChanges();
            }
            lbl_mnsj.Visible = true;
            lbl_mnsj.Text = "Registro actualizado con Exito";

            if (rb_edit.Checked == false)
            {
                lbl_mnsj.Visible = true;
                lbl_mnsj.Text = "Favor de seleccionar una acción";
            }
            else
            {

                int str_municipality = Convert.ToInt32(ddl_municipality.SelectedValue);
                string str_colony = txt_colony.Text.ToUpper();
                string str_street = txt_street.Text.ToUpper();
                string str_cp = txt_cp.Text;
                string str_phone = txt_phone.Text;
                string str_phonealt = txt_phone_alt.Text;
                string str_email = txt_email.Text.ToLower();
                string str_emailalt = txt_email_alt.Text.ToUpper();
                int str_count_contact;

                using (db_videos_testEntities data_count = new db_videos_testEntities())
                {
                    var items_contact = (from c in data_count.inf_contacto
                                         where c.id_usuario == id_user
                                         select c).Count();

                    str_count_contact = Convert.ToInt32(items_contact);
                }

                if (str_count_contact == 0)
                {
                    using (var insert_user_center = new db_videos_testEntities())
                    {

                        var items_user = new inf_contacto
                        {

                            id_usuario = id_user,
                            id_municipio = str_municipality,
                            colonia = str_colony,
                            calle_num = str_street,
                            cp = str_cp,
                            telefono = str_phone,
                            telefono_alt = str_phonealt,
                            email = str_email,
                            email_alt = str_emailalt,

                        };
                        insert_user_center.inf_contacto.Add(items_user);
                        insert_user_center.SaveChanges();
                    }

                    lbl_mnsj.Visible = true;
                    lbl_mnsj.Text = "Datos de contacto registrados con exito.";
                }
                else
                {
                    using (var data_address = new db_videos_testEntities())
                    {
                        var items_address = (from c in data_address.inf_contacto
                                             where c.id_usuario == id_user
                                             select c).FirstOrDefault();

                        items_address.id_municipio = str_municipality;
                        items_address.colonia = str_colony;
                        items_address.calle_num = str_street;
                        items_address.cp = str_cp;
                        items_address.telefono = str_phone;
                        items_address.telefono_alt = str_phonealt;
                        items_address.email = str_email;
                        items_address.email_alt = str_emailalt;

                        data_address.SaveChanges();
                    }

                    lbl_mnsj.Visible = true;
                    lbl_mnsj.Text = "Datos de contacto actualizados con exito";
                }
            }
        }
        protected void rb_edit_CheckedChanged(object sender, EventArgs e)
        {

            Guid id_user = mdl_user.str_fiduser;

            using (db_videos_testEntities data_user = new db_videos_testEntities())
            {
                var inf_user = (from u in data_user.inf_usuarios
                                join tu in data_user.fact_tipo_usuarios on u.id_tipo_usuario equals tu.id_tipo_usuario
                                where u.id_usuario == id_user

                                select new
                                {
                                    u.codigo_usuario,
                                    u.id_genero,
                                    u.fecha_nacimiento,
                                    u.nombres,
                                    u.a_paterno,
                                    u.a_materno,

                                }).FirstOrDefault();

                ddl_gender.SelectedValue = inf_user.id_genero.ToString();
                DateTime str_birthday = new DateTime();
                str_birthday = Convert.ToDateTime(inf_user.fecha_nacimiento);
                txt_birthday.Text = str_birthday.ToShortDateString();
                txt_name_user.Text = inf_user.nombres;
                txt_apater.Text = inf_user.a_paterno;
                txt_amater.Text = inf_user.a_materno;
                txt_code_user.Text = inf_user.codigo_usuario;
            }
            try
            {
                using (db_videos_testEntities data_user = new db_videos_testEntities())
                {
                    var items_user = (from i_user in data_user.inf_usuarios
                                      join i_address in data_user.inf_contacto on i_user.id_usuario equals i_address.id_usuario
                                      join i_municipaly in data_user.fact_municipio on i_address.id_municipio equals i_municipaly.id_municipio
                                      join i_state in data_user.fact_estado on i_municipaly.id_estado equals i_state.id_estado
                                      where i_user.id_usuario == id_user
                                      select new
                                      {

                                          i_state.id_estado,
                                          i_address.id_municipio,
                                          i_address.colonia,
                                          i_address.calle_num,
                                          i_address.cp,
                                          i_address.telefono,
                                          i_address.telefono_alt,
                                          i_address.email,
                                          i_address.email_alt

                                      }).FirstOrDefault();


                    int str_id_address_state = items_user.id_estado;
                    int? str_id_address_municipality = items_user.id_municipio;
                    string str_address_colony = items_user.colonia;
                    string str_address_street = items_user.calle_num;
                    string str_address_postal_code = items_user.cp;
                    string str_address_phone = items_user.telefono;
                    string str_address_phone_alt = items_user.telefono_alt;
                    string str_address_email = items_user.email;
                    string str_address_email_alt = items_user.email_alt;

                    ddl_state.SelectedValue = str_id_address_state.ToString();
                    LoadMunicipality(str_id_address_state);
                    ddl_municipality.SelectedValue = str_id_address_municipality.ToString();
                    txt_colony.Text = str_address_colony;
                    txt_street.Text = str_address_street;
                    txt_cp.Text = str_address_postal_code;
                    txt_phone.Text = str_address_phone;
                    txt_phone_alt.Text = str_address_phone_alt;
                    txt_email.Text = str_address_email;
                    txt_email_alt.Text = str_address_email_alt;
                }
            }
            catch
            {
                lbl_mnsj.Visible = true;
                lbl_mnsj.Text = "Sin datos de contacto, favor de agregarlos";
            }
        }
        protected void img_address_Click(object sender, ImageClickEventArgs e)
        {
            Session["ss_fid_user"] = (Guid)(Session["ss_id_user"]);
            Response.Redirect("ctrl_contacto.aspx");
        }

        protected void img_school_Click(object sender, ImageClickEventArgs e)
        {
            Session["ss_fid_user"] = (Guid)(Session["ss_id_user"]);
            Response.Redirect("ctrl_escolares.aspx");
        }

        protected void img_invoice_Click(object sender, ImageClickEventArgs e)
        {
            Session["ss_fid_user"] = (Guid)(Session["ss_id_user"]);
            Response.Redirect("ctrl_fiscales.aspx");
        }

        protected void img_banking_Click(object sender, ImageClickEventArgs e)
        {
            Session["ss_fid_user"] = (Guid)(Session["ss_id_user"]);
            Response.Redirect("ctrl_bancarios.aspx");
        }

        protected void img_expertise_Click(object sender, ImageClickEventArgs e)
        {
            Session["ss_fid_user"] = (Guid)(Session["ss_id_user"]);
            Response.Redirect("ctrl_experiencia.aspx");
        }

        protected void ddl_state_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id_state = Convert.ToInt32(ddl_state.SelectedValue);

            LoadMunicipality(id_state);
        }
        private void LoadMunicipality(int id_state)
        {
            using (db_videos_testEntities data_user = new db_videos_testEntities())
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
    }
}