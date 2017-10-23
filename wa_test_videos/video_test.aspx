<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="video_test.aspx.cs" Inherits="wa_test_videos.video_test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div>
            <div class="col-md-12 center-block">

                <ajaxToolkit:AjaxFileUpload ID="AjaxFileUpload1"
                    ThrobberID="myThrobber"
                    ContextKeys="fred"
                    AllowedFileTypes="xml"
                    MaximumNumberOfFiles="10"
                    runat="server" OnUploadComplete="AjaxFileUpload1_UploadComplete" />
            </div>

        </div>
        <div class="col-md-12 center-block">
            <asp:GridView CssClass="table" ID="GridView1" runat="server" AutoGenerateColumns="true" AllowPaging="true">
            </asp:GridView>
        </div>
    </form>
</body>
</html>
