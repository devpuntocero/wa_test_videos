﻿<%@ Page Title="" Language="C#" MasterPageFile="~/master_transcript.Master" AutoEventWireup="true" CodeBehind="ctrl_menu_usuarios.aspx.cs" Inherits="wa_transcript.ctrl_menu_usuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="section">
        <div class="container">
            <div class="form-group form-group-sm text-success">
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
                        <h1 class="text-center">Control de usuarios</h1>
                    </div>
                </div>
                <div class="row animated bounceInUp">
                    <div class="col-md-3 text-center" id="div_perfil" runat="server">
                        <h5>
                            <asp:Label ID="lbl_perfil" runat="server" Text="Perfil"></asp:Label></h5>
                        <asp:ImageButton ID="img_perfil" runat="server" ImageUrl="~/images/iconos/perfil@2x.png" Width="64" Height="64" OnClick="img_perfil_Click" />
                    </div>
                    <div class="col-md-3 text-center" id="div_administrador" runat="server">
                        <h5>
                            <asp:Label ID="lbl_administrador" runat="server" Text="Administrador"></asp:Label></h5>
                        <asp:ImageButton ID="img_administrador" runat="server" ImageUrl="~/images/iconos/administrador@2x.png" Width="64" Height="64" OnClick="img_administrador_Click" />
                    </div>
                    <div class="col-md-3 text-center" id="div_superintendent" runat="server">
                        <h5>
                            <asp:Label ID="lbl_superintendent" runat="server" Text="Supervisor"></asp:Label></h5>
                        <asp:ImageButton ID="img_superintendent" runat="server" ImageUrl="~/images/iconos/supervisor@2x.png" Width="64" Height="64" OnClick="img_superintendent_Click"  />
                    </div>
                    <div class="col-md-3 text-center" id="div_operator" runat="server">
                        <h5>
                            <asp:Label ID="lbl_operator" runat="server" Text="Operador"></asp:Label></h5>
                        <asp:ImageButton ID="img_operator" runat="server" ImageUrl="~/images/iconos/operador@2x.png" Width="64" Height="64" OnClick="img_operator_Click"  />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
