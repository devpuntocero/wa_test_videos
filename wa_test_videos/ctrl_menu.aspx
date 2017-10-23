﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ctrl_menu.aspx.cs" Inherits="wa_test_videos.ctrl_menu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="section">
        <div class="container">
            <div class="row">
                <div class="col-md-1">
                    <a href="ctrl_acceso.aspx">
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
                    <h1 class="text-center">Bienvenido</h1>
                </div>
            </div>
            <div class="row animated bounceInUp">
                <div class="col-md-4 text-center" id="div_tracing" runat="server">
                    <h5>Grabaciones</h5>
                    <asp:ImageButton ID="img_tracing" runat="server" ImageUrl="~/images/iconos/grabaciones@2x.png"  OnClick="img_tracing_Click" />
                </div>

                <div class="col-md-4 text-center" id="div_material" runat="server">
                    <h5>Herramientas</h5>
                    <asp:ImageButton ID="img_material" runat="server" ImageUrl="~/images/iconos/herramientas@2x.png"  OnClick="img_material_Click" />
                </div>
                <div class="col-md-4 text-center" id="div_control_users" runat="server">
                    <h5>Control de Usuarios</h5>
                    <asp:ImageButton ID="img_control_users" runat="server" ImageUrl="~/images/iconos/contro de usuarios@2x.png"  OnClick="img_control_users_Click" />
                </div>
            </div>
            <div class="col-md-6 text-center" id="div_resumen" runat="server">
                <h5>Resumen</h5>
                <asp:ImageButton ID="img_summary" runat="server" ImageUrl="~/images/iconos/resumen estadisticas@2x.png"  OnClick="img_summary_Click" />

            </div>
            <div class="row animated bounceInUp">
                <div class="col-md-6 text-center" id="div_control_centers" runat="server">
                    <h5>Control de Centros</h5>
                    <asp:ImageButton ID="img_control_centers" runat="server" ImageUrl="~/images/iconos/control de centros@2x.png"  OnClick="img_control_centers_Click" />
                </div>

            </div>
        </div>
    </div>
</asp:Content>
