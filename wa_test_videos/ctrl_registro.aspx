﻿<%@ Page Title="" Language="C#" MasterPageFile="~/master_transcript.Master" AutoEventWireup="true" CodeBehind="ctrl_registro.aspx.cs" Inherits="wa_transcript.ctrl_registro" %>

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
                                <a href="ctrl_acceso.aspx">
                                    <img alt="" src="images/ico_exit.png" /></a>
                            </div>
                            <br />
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <h3 class="text-center">Registro de administrador y juzgado</h3>
                            </div>
                            <div class="col-md-4">
                                <asp:Image CssClass="center-block img-responsive" ID="Image1" runat="server" ImageUrl="~/images/business-and-office/png/301-team-2.png" Width="128" Height="128" />
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
                    </div>
                </div>
            </div>
        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="section">
                <div class="container">
                    <div class="row" id="div_fiscal" runat="server" visible="true">
                        <div class="col-md-4">
                            <asp:Image CssClass="center-block img-responsive" ID="Image2" runat="server" ImageUrl="~/images/business-and-office/png/301-office-block.png" Width="128" Height="128" />
                        </div>
                        <div class="col-md-4">
                            <h5>
                                <asp:Label CssClass="control-label" ID="lbl_business_name" runat="server" Text="*Centro" Visible="True"></asp:Label></h5>
                            <asp:TextBox CssClass="form-control" ID="txt_business_name" runat="server" placeholder="Capturar nombre de centro" Visible="True"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfv_business_name" runat="server"
                                ControlToValidate="txt_business_name"
                                ErrorMessage="Campo Requerido"
                                ForeColor="orange">
                            </asp:RequiredFieldValidator>
                            <h5>
                                <asp:Label CssClass="control-label" ID="lbl_state" runat="server" Text="*Estado"></asp:Label></h5>
                            <asp:DropDownList CssClass="form-control" ID="ddl_state" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_state_SelectedIndexChanged"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfv_state" runat="server"
                                ErrorMessage="Campo Requerido"
                                ControlToValidate="ddl_state" InitialValue="0"
                                ForeColor="orange">
                            </asp:RequiredFieldValidator>
                            <h5>
                                <asp:Label CssClass="control-label" ID="lbl_municipality" runat="server" Text="*Municipio"></asp:Label></h5>
                            <asp:DropDownList CssClass="form-control" ID="ddl_municipality" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfv_municipality" runat="server"
                                ErrorMessage="Campo Requerido"
                                ControlToValidate="ddl_municipality" InitialValue="0"
                                ForeColor="orange">
                            </asp:RequiredFieldValidator>
                            <h5>
                                <asp:Label CssClass="control-label" ID="lbl_colony" runat="server" Text="*Colonia"></asp:Label></h5>
                            <asp:TextBox CssClass="form-control" ID="txt_colony" runat="server" placeholder="Capturar Colonia"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfv_colony" runat="server"
                                ControlToValidate="txt_colony"
                                ErrorMessage="Campo Requerido"
                                ForeColor="orange">
                            </asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">
                            <h5>
                                <asp:Label CssClass="control-label" ID="lbl_street" runat="server" Text="*Calle"></asp:Label></h5>
                            <asp:TextBox CssClass="form-control" ID="txt_street" runat="server" placeholder="Capturar Calle"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfv_street" runat="server"
                                ControlToValidate="txt_street"
                                ErrorMessage="Campo Requerido"
                                ForeColor="orange">
                            </asp:RequiredFieldValidator>

                            <h5>
                                <asp:Label CssClass="control-label" ID="lbl_cp" runat="server" Text="*Código Postal"></asp:Label></h5>
                            <asp:TextBox CssClass="form-control" ID="txt_cp" runat="server" placeholder="Capturar Código Postal" MaxLength="6"></asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="txt_cp_MaskedEditExtender" runat="server" TargetControlID="txt_cp" Mask="99999" />
                            <asp:RequiredFieldValidator ID="rfv_cp" runat="server"
                                ControlToValidate="txt_cp"
                                ErrorMessage="Campo Requerido"
                                ForeColor="orange">
                            </asp:RequiredFieldValidator>
                            <h5>
                                <asp:Label CssClass="control-label" ID="lbl_phone" runat="server" Text="*Teléfono"></asp:Label></h5>
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
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12 text-right">
                            <asp:Button CssClass="btn btn-success" ID="cmd_save" runat="server" Text="Guardar" OnClick="cmd_save_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                            <h4 class="modal-title">
                                <asp:Label ID="lblModalTitle" runat="server" Text=""></asp:Label></h4>
                        </div>
                        <div class="modal-body">
                            <asp:Label ID="lblModalBody" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="modal-footer">
                            <button class="btn btn-success" data-dismiss="modal" aria-hidden="true" onclick="window.location.href='/ctrl_acceso.aspx'">Ok </button>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_state" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
