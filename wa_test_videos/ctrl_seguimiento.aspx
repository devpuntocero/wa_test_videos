<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ctrl_seguimiento.aspx.cs" Inherits="wa_test_videos.ctrl_seguimiento" %>
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
                                <a href="ctrl_menu.aspx">
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
                        </div>
                        <div class=" col-md-2">
                            <h5>
                                <asp:Label CssClass="control-label" ID="lbl_expedientr" runat="server" Text="*Expediente"></asp:Label></h5>
                            <asp:TextBox CssClass="form-control" ID="txt_expedient" runat="server" placeholder="Capturar Expediente"></asp:TextBox>
                        </div>

                        <div class=" col-md-2">
                            <h5>
                                <asp:Label CssClass="control-label" ID="lbl_fechaini" runat="server" Text="*Fecha Inicial"></asp:Label></h5>
                            <asp:TextBox CssClass="form-control" ID="txt_dateini" runat="server" placeholder="Buscar fecha incial"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="ce_dateini" runat="server" BehaviorID="ce_dateini" TargetControlID="txt_dateini" Format="yyyy/MM/dd" />
                            <asp:RequiredFieldValidator ID="rfv_dateini" runat="server"
                                ControlToValidate="txt_dateini"
                                ErrorMessage="Campo Requerido"
                                ForeColor="orange">
                            </asp:RequiredFieldValidator>
                        </div>
                        <div class=" col-md-2">
                            <h5>
                                <asp:Label CssClass="control-label" ID="lbl_fechafin" runat="server" Text="*Fecha Final"></asp:Label></h5>
                            <asp:TextBox CssClass="form-control" ID="txt_datefin" runat="server" placeholder="Buscar fecha final"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="ce_datefin" runat="server" BehaviorID="ce_datefin" TargetControlID="txt_datefin" Format="yyyy/MM/dd" />
                            <asp:RequiredFieldValidator ID="rfv_datefin" runat="server"
                                ControlToValidate="txt_datefin"
                                ErrorMessage="Campo Requerido"
                                ForeColor="orange">
                            </asp:RequiredFieldValidator>
                        </div>
                        <div class=" col-md-4 text-right">
                            <asp:RadioButton CssClass="radio-inline" ID="rb_load" runat="server" Text="Cargados" AutoPostBack="True" OnCheckedChanged="rb_load_CheckedChanged" />

                            <asp:RadioButton CssClass="radio-inline" ID="rb_active" runat="server" Text="Activos" AutoPostBack="True" OnCheckedChanged="rb_active_CheckedChanged" />
                        </div>
                        <div class="col-md-12">
                            <h5></h5>
                            <asp:Button CssClass="btn btn-success" ID="cmd_search" runat="server" Text="Buscar" OnClick="cmd_search_Click" />
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
                            <asp:GridView CssClass="table" ID="gv_files" runat="server" AutoGenerateColumns="False" AllowPaging="True">
                                <Columns>
                                    <asp:TemplateField HeaderText="Seleccionar">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_select" runat="server" onclick="CheckOne(this)" OnCheckedChanged="chk_OnCheckedChanged" AutoPostBack="true" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="expediente" HeaderText="Expediente" SortExpression="expediente" Visible="true" />
                                    <asp:BoundField DataField="archivo" HeaderText="Archivo" SortExpression="archivo" />
                                    <asp:BoundField DataField="codigo_usuario" HeaderText="ID de Usuario" SortExpression="codigo_usuario" Visible="true" />
                                    <asp:BoundField DataField="nombres" HeaderText="Nombre de Usuario" SortExpression="nombres" />
                                    <asp:BoundField DataField="a_paterno" HeaderText="Apellido Paterno" SortExpression="a_paterno" />
                                    <asp:BoundField DataField="a_materno" HeaderText="Apellido Materno" SortExpression="a_materno" />
                                    <asp:BoundField DataField="bits" HeaderText="Tamaño MB" SortExpression="bits" DataFormatString="{0:0,0}" />
                                    <asp:BoundField DataField="fecha_registro" HeaderText="Fecha Registro" SortExpression="fecha_registro" DataFormatString="{0:dd-MM-yyyy}" HtmlEncode="false" />
                                    <asp:TemplateField HeaderText="Ver" Visible="false">
                                        <ItemTemplate>
                                            <asp:Button CssClass="btn btn-success" ID="cmd_ver" runat="server" Text="Ver" OnClick="cmd_ver_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="row" id="div_status" runat="server">
                            <div class="col-md-8 text-left">
                            </div>
                            <div class="col-md-1 text-left">
                                <h5>
                                    <asp:Label CssClass="control-label" ID="lbl_status" runat="server" Text="*Estatus" Visible="false"></asp:Label></h5>

                            </div>
                            <div class="col-md-2 text-right">

                                <asp:DropDownList CssClass="form-control" ID="ddl_estatus" runat="server" Visible="false">
                                    <asp:ListItem>Seleccionar</asp:ListItem>
                                    <asp:ListItem>Bien</asp:ListItem>
                                    <asp:ListItem>Mal</asp:ListItem>
                                </asp:DropDownList>

                            </div>
                            <div class="col-md-1 text-left">
                                <asp:Button CssClass="btn btn-success" ID="cmd_save" runat="server" Text="Guardar" OnClick="cmd_save_Click" Visible="false" />
                            </div>
                        </div>
                        <br />
                        <div class="row" id="div_panel_ie" runat="server">
                            <div class="col-md-12 text-center">
                                <asp:Panel ID="Panel1" runat="server" CssClass=" img-thumbnail"></asp:Panel>
                            </div>
                        </div>
                        <div class="row" id="div_panel" runat="server">
                        <div class="col-md-12 text-center">
                            <video id="play_video" runat="server" visible="false" class="img-thumbnail" controls="controls">
                                <source src="demo" type='video/mp4;codecs="avc1.42E01E, mp4a.40.2"' />
                            </video>
                        </div>
                        </div>
                        <div class="row">
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
