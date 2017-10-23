﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ctrl_usuario.aspx.cs" Inherits="wa_test_videos.ctrl_usuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="section">
                <div class="container">
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-1">
                                <a href="ctrl_menu_usuarios.aspx">
                                    <img alt="" src="images/ico_back.png" /></a>
                            </div>
                            <div class="col-md-1">
                                <a href="ctrl_acceso.aspx">
                                    <img alt="" src="images/ico_exit.png" /></a>
                            </div>
                            <br />
                            <div class="col-md-10">
                                <p class="text-right text-success">
                                    <asp:Label ID="lbl_welcome" runat="server" Text="Bienvenid@: "></asp:Label>
                                    <asp:Label ID="lbl_name" runat="server" Text=""></asp:Label>
                                    <br />
                                    <asp:Label ID="lbl_profilelbl" runat="server" Text="Perfil: "></asp:Label>
                                    <asp:Label ID="lbl_profile_user" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lbl_id_profile_user" runat="server" Text="" Visible="false"></asp:Label>
                                    <br />
                                    <asp:Label ID="lbl_user_centerP" runat="server" Text="Centro: "></asp:Label>
                                    <asp:Label ID="lbl_user_centerCP" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lbl_id_centerCP" runat="server" Text="" Visible="false"></asp:Label>
                                </p>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <h2 class="text-center">
                                    <asp:Label ID="lbl_reg" runat="server" Text=""></asp:Label></h2>
                            </div>
                            <div class="col-md-12 text-right">
                                <asp:RadioButton CssClass="radio-inline" ID="rb_add" runat="server" Text="Agregar" AutoPostBack="True" OnCheckedChanged="rb_add_CheckedChanged" />
                                <asp:RadioButton CssClass="radio-inline" ID="rb_edit" runat="server" Text="Editar" AutoPostBack="True" OnCheckedChanged="rb_edit_CheckedChanged" />
                                <asp:RadioButton CssClass="radio-inline" ID="rb_drop" runat="server" Text="Baja" AutoPostBack="True" OnCheckedChanged="rb_drop_CheckedChanged" />
                                <asp:RadioButton CssClass="radio-inline" ID="rb_del" runat="server" Text="Eliminar" AutoPostBack="True" OnCheckedChanged="rb_del_CheckedChanged" />
                            </div>
                            <div class="col-md-offset-3 col-md-6">
                                <div class="form-group">
                                    <div class="input-group">
                                        <asp:TextBox CssClass="form-control" ID="txt_search" runat="server" placeholder="Buscar" Visible="false"></asp:TextBox>
                                        <span class="input-group-btn">
                                            <asp:Button CssClass="btn btn-success" ID="cmd_search" runat="server" Text="Ir" Visible="false" />
                                        </span>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <script>
                                    function CheckOne(obj) {
                                        var grid = obj.parentNode.parentNode.parentNode;
                                        var inputs = grid.getElementsByTagName("input");
                                        for (var i = 0; i < inputs.length; i++) {
                                            if (inputs[i].type == "checkbox") {
                                                if (obj.checked && inputs[i] != obj && inputs[i].checked) {
                                                    inputs[i].checked = false;
                                                }
                                            }
                                        }
                                    }
                                </script>
                                <br />
                                <asp:GridView CssClass="table" ID="gv_usuarios" runat="server" AutoGenerateColumns="False" AllowPaging="true">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_select" runat="server" onclick="CheckOne(this)" OnCheckedChanged="chk_OnCheckedChanged" AutoPostBack="true"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="codigo_usuario" HeaderText="ID de Usuario" SortExpression="codigo_usuario" Visible="true" />
                                        <asp:BoundField DataField="desc_estatus" HeaderText="Estatus" SortExpression="desc_estatus" />
                                        <asp:BoundField DataField="fecha_nacimiento" HeaderText="Fecha de Nacimiento" SortExpression="fecha_nacimiento" DataFormatString="{0:dd-MM-yyyy}" HtmlEncode="false" />
                                        <asp:BoundField DataField="nombres" HeaderText="Nombre de Usuario" SortExpression="nombres" />
                                        <asp:BoundField DataField="a_paterno" HeaderText="Apellido Paterno" SortExpression="a_paterno" />
                                        <asp:BoundField DataField="a_materno" HeaderText="Apellido Materno" SortExpression="a_materno" />
                                    </Columns>
                                </asp:GridView>
                                <br />
                            </div>
                            <div class="col-md-12">
                                <h5 class="text-center">Datos de Usuario</h5>
                            </div>
                            <div class="col-md-4">
                                <h5>
                                    <asp:Label CssClass="control-label" ID="lbl_gender" runat="server" Text="*Género"></asp:Label></h5>
                                <asp:DropDownList CssClass="form-control" ID="ddl_gender" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfv_licence" runat="server"
                                    ErrorMessage="Campo Requerido"
                                    ControlToValidate="ddl_gender" InitialValue="0"
                                    ForeColor="orange">
                                </asp:RequiredFieldValidator>
                                <h5>
                                    <asp:Label CssClass="control-label" ID="lbl_birthday" runat="server" Text="*Fecha de Nacimiento"></asp:Label></h5>
                                <asp:TextBox CssClass="form-control" ID="txt_birthday" runat="server" ForeColor="Black" placeholder="Fecha de Nacimiento"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="txtce" runat="server" BehaviorID="txtce" TargetControlID="txt_birthday" Format="yyyy/MM/dd" />
                                <asp:RequiredFieldValidator ID="rfv_birthday" runat="server"
                                    ControlToValidate="txt_birthday"
                                    ErrorMessage="Campo Requerido"
                                    ForeColor="orange">
                                </asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-4">
                                <h5>
                                    <asp:Label CssClass="control-label" ID="lbl_name_user" runat="server" Text="*Nombre(s)"></asp:Label></h5>
                                <asp:TextBox CssClass="form-control" ID="txt_name_user" runat="server" placeholder="Capturar Nombre(s)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfv_name_user" runat="server"
                                    ControlToValidate="txt_name_user"
                                    ErrorMessage="Campo Requerido"
                                    ForeColor="orange">
                                </asp:RequiredFieldValidator>
                                <h5>
                                    <asp:Label CssClass="control-label" ID="lbl_apater" runat="server" Text="*Apellido Paterno"></asp:Label></h5>
                                <asp:TextBox CssClass="form-control" ID="txt_apater" runat="server" placeholder="Capturar Apellido Paterno"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfv_apater" runat="server"
                                    ControlToValidate="txt_apater"
                                    ErrorMessage="Campo Requerido"
                                    ForeColor="orange">
                                </asp:RequiredFieldValidator>
                                <h5>
                                    <asp:Label CssClass="control-label" ID="lbl_amater" runat="server" Text="*Apellido Materno"></asp:Label></h5>
                                <asp:TextBox CssClass="form-control" ID="txt_amater" runat="server" placeholder="Capturar Apellido Materno"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfv_amater" runat="server"
                                    ControlToValidate="txt_amater"
                                    ErrorMessage="Campo Requerido"
                                    ForeColor="orange">
                                </asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-4">
                                <h5>
                                    <asp:Label CssClass="control-label" ID="lbl_code_user" runat="server" Text="*Usuario"></asp:Label></h5>
                                <asp:TextBox CssClass="form-control" ID="txt_code_user" runat="server" placeholder="Capturar Usuario"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfv_email_im" runat="server"
                                    ControlToValidate="txt_code_user"
                                    ErrorMessage="Campo Requerido"
                                    ForeColor="orange">
                                </asp:RequiredFieldValidator>
                                <h5>
                                    <asp:Label CssClass="control-label" ID="lbl_password" runat="server" Text="*Contraseña"></asp:Label></h5>
                                <asp:TextBox CssClass="form-control" ID="txt_password" runat="server" placeholder="Capturar Contraseña" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfv_password" runat="server"
                                    ControlToValidate="txt_password"
                                    ErrorMessage="Campo Requerido"
                                    ForeColor="orange">
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row" id="div_contact" runat="server">
                            <div class="col-md-4">
                                <h5>
                                    <asp:Label CssClass="control-label" ID="lbl_state" runat="server" Text="Estado"></asp:Label></h5>
                                <asp:DropDownList CssClass="form-control" ID="ddl_state" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_state_SelectedIndexChanged"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfv_state" runat="server"
                                    ErrorMessage="Campo Requerido"
                                    ControlToValidate="ddl_state" InitialValue="0"
                                    ForeColor="orange">
                                </asp:RequiredFieldValidator>
                                <h5>
                                    <asp:Label CssClass="control-label" ID="lbl_municipality" runat="server" Text="Municipio"></asp:Label></h5>
                                <asp:DropDownList CssClass="form-control" ID="ddl_municipality" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfv_municipality" runat="server"
                                    ErrorMessage="Campo Requerido"
                                    ControlToValidate="ddl_municipality" InitialValue="0"
                                    ForeColor="orange">
                                </asp:RequiredFieldValidator>
                                <h5>
                                    <asp:Label CssClass="control-label" ID="lbl_colony" runat="server" Text="Colonia"></asp:Label></h5>
                                <asp:TextBox CssClass="form-control" ID="txt_colony" runat="server" placeholder="Capturar Colonia"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfv_colony" runat="server"
                                    ControlToValidate="txt_colony"
                                    ErrorMessage="Campo Requerido"
                                    ForeColor="orange">
                                </asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-4">
                                <h5>
                                    <asp:Label CssClass="control-label" ID="lbl_street" runat="server" Text="Calle"></asp:Label></h5>
                                <asp:TextBox CssClass="form-control" ID="txt_street" runat="server" placeholder="Capturar Calle"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfv_street" runat="server"
                                    ControlToValidate="txt_street"
                                    ErrorMessage="Campo Requerido"
                                    ForeColor="orange">
                                </asp:RequiredFieldValidator>

                                <h5>
                                    <asp:Label CssClass="control-label" ID="lbl_cp" runat="server" Text="Código Postal"></asp:Label></h5>
                                <asp:TextBox CssClass="form-control" ID="txt_cp" runat="server" placeholder="Capturar Código Postal" MaxLength="6"></asp:TextBox>
                                <ajaxToolkit:MaskedEditExtender ID="txt_cp_MaskedEditExtender" runat="server" TargetControlID="txt_cp" Mask="99999" />
                                <asp:RequiredFieldValidator ID="rfv_cp" runat="server"
                                    ControlToValidate="txt_cp"
                                    ErrorMessage="Campo Requerido"
                                    ForeColor="orange">
                                </asp:RequiredFieldValidator>
                                <h5>
                                    <asp:Label CssClass="control-label" ID="lbl_phone" runat="server" Text="Teléfono"></asp:Label></h5>
                                <asp:TextBox CssClass="form-control" ID="txt_phone" runat="server" placeholder="Capturar Teléfono"></asp:TextBox>
                                <ajaxToolkit:MaskedEditExtender ID="txt_phone_MaskedEditExtender" runat="server" TargetControlID="txt_phone" Mask="(52) 99.99.99.99.99.99 ext 99999" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                    ControlToValidate="txt_phone"
                                    ErrorMessage="Campo Requerido"
                                    ForeColor="orange">
                                </asp:RequiredFieldValidator>
                                <h5>
                                    <asp:Label CssClass="control-label" ID="lbl_phone_alt" runat="server" Text="Teléfono Alterno"></asp:Label></h5>
                                <asp:TextBox CssClass="form-control" ID="txt_phone_alt" runat="server" placeholder="Capturar Teléfono Alterno"></asp:TextBox>
                                <ajaxToolkit:MaskedEditExtender ID="txt_phone_alt_MaskedEditExtender" runat="server" TargetControlID="txt_phone_alt" Mask="(52) 99.99.99.99.99.99 ext 99999" />
                            </div>
                            <div class="col-md-4">
                                <h5>
                                    <asp:Label CssClass="control-label" ID="lbl_email" runat="server" Text="Correo Electrónico"></asp:Label></h5>
                                <asp:TextBox CssClass="form-control" ID="txt_email" runat="server" placeholder="Capturar Correo Electrónico"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfv_email" runat="server"
                                    ControlToValidate="txt_email"
                                    ErrorMessage="Campo Requerido"
                                    ForeColor="orange">
                                </asp:RequiredFieldValidator>
                                <h5>
                                    <asp:Label CssClass="control-label" ID="lbl_email_alt" runat="server" Text="Correo Electrónico Alterno"></asp:Label></h5>
                                <asp:TextBox CssClass="form-control" ID="txt_email_alt" runat="server" placeholder="Capturar Correo Electrónico Alterno"></asp:TextBox>
                            </div>
                            <br />
                            <div class="col-md-12 text-right">
                                <asp:Button CssClass="btn btn-success" ID="cmd_save" runat="server" Text="Guardar" OnClick="cmd_save_Click" />
                            </div>
                            <div class="col-md-12">
                                <h3 class="text-center text-warning">
                                    <asp:Label ID="lbl_mnsj" runat="server" Visible="True"></asp:Label>
                                </h3>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
