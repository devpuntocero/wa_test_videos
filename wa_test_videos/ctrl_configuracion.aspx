<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ctrl_configuracion.aspx.cs" Inherits="wa_test_videos.ctrl_configuracion" %>
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
                                <a href="ctrl_material.aspx">
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
                            <div class="col-md-12">
                                <h2 class="text-center">
                                    <asp:Label ID="lbl_reg" runat="server" Text="Horario de Transformación"></asp:Label></h2>
                            </div>
                        </div>
                          <div class=" col-md-12 text-right">
                            <asp:RadioButton CssClass="radio-inline" ID="rb_add" runat="server" Text="Agregar" AutoPostBack="True" OnCheckedChanged="rb_add_CheckedChanged" />

                            <asp:RadioButton CssClass="radio-inline" ID="rb_editar" runat="server" Text="Editar" AutoPostBack="True" OnCheckedChanged="rb_editar_CheckedChanged" />
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
                                            <asp:CheckBox ID="chk_select" runat="server" onclick="CheckOne(this)" OnCheckedChanged="chk_OnCheckedChanged" AutoPostBack="true" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="id_fecha_transformacion" HeaderText="ID" SortExpression="id_fecha_transformacion" Visible="true" />
                                    <asp:BoundField DataField="horario" HeaderText="Horario" SortExpression="horario" />
                                    <asp:BoundField DataField="fecha_registro" HeaderText="Fecha de Registro" SortExpression="fecha_registro" DataFormatString="{0:d}" HtmlEncode="false" />
                                </Columns>
                            </asp:GridView>
                            <br />
                        </div>
                        <div class=" col-md-2">
                            <h5>
                                <asp:Label CssClass="control-label" ID="lbl_fecha" runat="server" Text="*Fecha"></asp:Label></h5>
                            <asp:TextBox CssClass="form-control" ID="txt_date" runat="server" placeholder="año/mes/día"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="ce_date" runat="server" BehaviorID="ce_date" TargetControlID="txt_date" Format="yyyy/MM/dd" />
                            <asp:RequiredFieldValidator ID="rfv_date" runat="server"
                                ControlToValidate="txt_date"
                                ErrorMessage="Campo Requerido"
                                ForeColor="orange">
                            </asp:RequiredFieldValidator>
                        </div>
                        <div class=" col-md-2">
                            <h5>
                                <asp:Label CssClass="control-label" ID="lbl_hora" runat="server" Text="*Hora"></asp:Label></h5>
                            <asp:TextBox CssClass="form-control" ID="txt_hora" runat="server" placeholder="hh/mm/ss"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfv_hora" runat="server"
                                ControlToValidate="txt_date"
                                ErrorMessage="Campo Requerido"
                                ForeColor="orange">
                            </asp:RequiredFieldValidator>
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                                AcceptAMPM="true"
                                MaskType="Time"
                                Mask="99:99:99"
                                ErrorTooltipEnabled="true"
                                InputDirection="RightToLeft"
                                CultureName="es-ES"
                                TargetControlID="txt_hora"
                                MessageValidatorTip="true" />
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <h5>
                                    <asp:Label CssClass="control-label" ID="lbl_fhora" runat="server" Text="*Formato"></asp:Label></h5>
                                <div class="input-group">
                                    <asp:DropDownList CssClass="form-control" ID="ddl_fhora" runat="server">
                                        <asp:ListItem>Seleccionar</asp:ListItem>
                                        <asp:ListItem>am</asp:ListItem>
                                        <asp:ListItem>pm</asp:ListItem>
                                    </asp:DropDownList>
                                    <span class="input-group-btn">
                                        <asp:Button CssClass="btn btn-success" ID="cmd_save" runat="server" Text="Guardar" OnClick="cmd_save_Click" />
                                    </span>
                                </div>
                                <asp:RequiredFieldValidator ID="rfv_fhora" runat="server"
                                    ControlToValidate="ddl_fhora" InitialValue="Seleccionar"
                                    ErrorMessage="Campo Requerido"
                                    ForeColor="orange">
                                </asp:RequiredFieldValidator>
                            </div>
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
