<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ctrl_videos_consultados.aspx.cs" Inherits="wa_test_videos.ctrl_videos_consultados" %>
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
                                <a href="ctrl_menu_resumen.aspx">
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
                                    <asp:Label ID="lbl_reg" runat="server" Text="Consulta de Videos"></asp:Label></h2>
                            </div>
                        </div>
                        <div class=" col-md-2">
                            <h5>
                                <asp:Label CssClass="control-label" ID="lbl_fechaini" runat="server" Text="*Fecha Inicial"></asp:Label></h5>
                            <asp:TextBox CssClass="form-control" ID="txt_dateini" runat="server" placeholder="año/mes/día"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="ce_dateini" runat="server" BehaviorID="ce_dateini" TargetControlID="txt_dateini" Format="yyyy/MM/dd" />
                            <asp:RequiredFieldValidator ID="rfv_dateini" runat="server"
                                ControlToValidate="txt_dateini"
                                ErrorMessage="Campo Requerido"
                                ForeColor="orange">
                            </asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <h5>
                                    <asp:Label CssClass="control-label" ID="lbl_fechafin" runat="server" Text="*Fecha Final"></asp:Label></h5>
                                <div class="input-group">
                                    <asp:TextBox CssClass="form-control" ID="txt_datefin" runat="server" placeholder="año/mes/día"></asp:TextBox>
                                    <span class="input-group-btn">
                                        <asp:Button CssClass="btn btn-success" ID="cmd_search" runat="server" Text="Buscar" OnClick="cmd_search_Click" />
                                    </span>
                                </div>
                                <ajaxToolkit:CalendarExtender ID="ce_datefin" runat="server" BehaviorID="ce_datefin" TargetControlID="txt_datefin" Format="yyyy/MM/dd" />
                                <asp:RequiredFieldValidator ID="rfv_datefin" runat="server"
                                    ControlToValidate="txt_datefin"
                                    ErrorMessage="Campo Requerido"
                                    ForeColor="orange">
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class=" col-md-7 text-right">
                            <asp:RadioButton CssClass="radio-inline" ID="rb_active" runat="server" Text="Consultados" AutoPostBack="True" />
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
                                            <asp:CheckBox ID="chk_select" runat="server" onclick="CheckOne(this)" AutoPostBack="true" />
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
                                </Columns>
                            </asp:GridView>
                        </div>
                        <br />
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
