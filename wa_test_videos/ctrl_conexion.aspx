<%@ Page Title="" Language="C#" MasterPageFile="~/master_transcript.Master" AutoEventWireup="true" CodeBehind="ctrl_conexion.aspx.cs" Inherits="wa_transcript.ctrl_conexion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="section">
                <div class="container">
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-1">
                                <a href="ctrl_configuracion.aspx">
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
                                    <asp:Label ID="lbl_fuser" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lbl_idfuser" runat="server" Text="" Visible="false"></asp:Label>
                                    <br />
                                    <asp:Label ID="lbl_profile" runat="server" Text="Perfil: "></asp:Label>
                                    <asp:Label ID="lbl_profileuser" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lbl_idprofileuser" runat="server" Text="" Visible="false"></asp:Label>
                                    <br />
                                    <asp:Label ID="lbl_center" runat="server" Text="Centro: "></asp:Label>
                                    <asp:Label ID="lbl_centername" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lbl_idcenter" runat="server" Text="" Visible="false"></asp:Label>
                                </p>
                            </div>
                        </div>
                        <div class="row">

                            <div class="col-md-12 text-left">
                                <div class="col-md-10 text-left">
                                    <div class="panel-group" runat="server" id="pg_credentials">
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                                <h4 class="text-center">Conexiones</h4>
                                                <asp:RadioButton CssClass="radio-inline text-right" ID="rb_add_credentials" runat="server" Text="Agregar" AutoPostBack="True" OnCheckedChanged="rb_add_credentials_CheckedChanged" />
                                                <asp:RadioButton CssClass="radio-inline text-right" ID="rb_edit_credentials" runat="server" Text="Editar" AutoPostBack="True" OnCheckedChanged="rb_edit_credentials_CheckedChanged" />
                                            </div>
                                            <div class="panel-body">
                                                <div class="row" id="div_infcredentials" runat="server" visible="false">
                                                    <div class="col-md-12">
                                                        <br />
                                                        <asp:GridView CssClass="table" ID="gv_credentials" runat="server" AutoGenerateColumns="False" AllowPaging="true" OnRowCommand="gv_credentials_RowCommand">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Seleccionar">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chk_select" runat="server" onclick="CheckOne(this)" OnCheckedChanged="chkselect_credentials" AutoPostBack="true" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="id_credenciales" HeaderText="ID" SortExpression="id_credenciales" Visible="true" />
                                                                <asp:BoundField DataField="ip" HeaderText="IP" SortExpression="ip" />
                                                                <asp:BoundField DataField="usuario" HeaderText="Usuario" SortExpression="usuario" />
                                                                <asp:BoundField DataField="fecha_registro" HeaderText="Fecha de Registro" SortExpression="fecha_registro" DataFormatString="{0:dd/MM/yyyy}" />
                                                                <asp:TemplateField HeaderText="Validar">
                                                                    <ItemTemplate>
                                                                        <asp:Button CssClass="btn btn-success" ID="cmd_ver" runat="server" Text="Validar" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                        <br />
                                                    </div>

                                                    <div class="col-md-4">
                                                        <asp:TextBox CssClass="form-control" ID="txt_ip" runat="server" placeholder="IP"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfv_ip" runat="server"
                                                            ControlToValidate="txt_ip"
                                                            ErrorMessage="Campo Requerido"
                                                            ForeColor="orange">
                                                        </asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="IpValidator" ControlToValidate="txt_ip" runat="server" ValidationExpression="^((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\.){3}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})$"
                                                            ErrorMessage="Enter valid IP Address" CssClass="comments" Display="None"></asp:RegularExpressionValidator>

                                                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="PNReqE" TargetControlID="IpValidator"  HighlightCssClass="highlight" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox CssClass="form-control" ID="txt_user" runat="server" placeholder="Usuario"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfv_user" runat="server"
                                                            ControlToValidate="txt_user"
                                                            ErrorMessage="Campo Requerido"
                                                            ForeColor="orange">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <div class="input-group">
                                                                <asp:TextBox CssClass="form-control" ID="txt_pass" runat="server" placeholder="Contraseña"></asp:TextBox>
                                                                <span class="input-group-btn">
                                                                    <asp:Button CssClass="btn btn-success" ID="cmd_save_credentials" runat="server" Text="Guardar" OnClick="cmd_save_credentials_Click" />
                                                                </span>
                                                            </div>
                                                            <asp:RequiredFieldValidator ID="rfv_pass" runat="server"
                                                                ControlToValidate="txt_pass"
                                                                ErrorMessage="Campo Requerido"
                                                                ForeColor="orange">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <br />
                                                        <asp:GridView CssClass="table" ID="gv_credentialsf" runat="server" AutoGenerateColumns="False" AllowPaging="true">
                                                            <Columns>
                                                                <asp:BoundField DataField="id_credenciales" HeaderText="ID" SortExpression="id_credenciales" Visible="true" />
                                                                <asp:BoundField DataField="ip" HeaderText="IP" SortExpression="ip" />
                                                                <asp:BoundField DataField="usuario" HeaderText="Usuario" SortExpression="usuario" />
                                                                <asp:BoundField DataField="fecha_registro" HeaderText="Fecha de Registro" SortExpression="fecha_registro" DataFormatString="{0:dd/MM/yyyy}" />
                                                            </Columns>
                                                        </asp:GridView>
                                                        <br />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <!-- Bootstrap Modal Dialog -->
    <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title">
                                <asp:Label ID="lblModalTitle" runat="server" Text=""></asp:Label></h4>
                        </div>
                        <div class="modal-body">
                            <asp:Label ID="lblModalBody" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="modal-footer">
                            <button class="btn btn-success" data-dismiss="modal" aria-hidden="true">Ok</button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
