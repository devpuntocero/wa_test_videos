<%@ Page Title="" Language="C#" MasterPageFile="~/master_transcript.Master" AutoEventWireup="true" CodeBehind="ctrl_configuracion.aspx.cs" Inherits="wa_transcript.ctrl_configuracion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="section">
        <div class="container">
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
                    <h1 class="text-center">Configuración</h1>
                </div>
            </div>
            <div class="row animated bounceInUp">
                <div class="col-md-3 text-center" id="div_credentials" runat="server">
                    <h5>Conexiones</h5>
                    <asp:ImageButton ID="img_credentials" runat="server" ImageUrl="~/images/iconos/grabaciones@2x.png" OnClick="img_credentials_Click" />
                </div>
                <div class="col-md-3 text-center" id="div_routevideos" runat="server">
                    <h5>Ruta de Carpeta de Videos</h5>
                    <asp:ImageButton ID="img_routevideos" runat="server" ImageUrl="~/images/iconos/herramientas@2x.png" OnClick="img_routevideos_Click" />
                </div>
                <div class="col-md-3 text-center" id="div_transformation" runat="server">
                    <h5>Fecha y hora de Conversión</h5>
                    <asp:ImageButton ID="img_transformation" runat="server" ImageUrl="~/images/iconos/videos@2x.png" OnClick="img_transformation_Click" />
                </div>
                <div class="col-md-3 text-center" id="div_dayvideos" runat="server">
                    <h5>Dias de respaldo de videos</h5>
                    <asp:ImageButton ID="img_dayvideos" runat="server" ImageUrl="~/images/iconos/control de centros@2x.png" OnClick="img_dayvideos_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
