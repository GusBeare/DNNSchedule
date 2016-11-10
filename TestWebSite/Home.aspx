<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="TestWebSite.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>Click the button to run the schedule code</div>
        <hr/>
    <asp:Button ID="btnRunSomeWork" runat="server" OnClick="btnRunSomeWork_OnClick" Text="Button" />  
    </div>
    </form>
</body>
</html>
