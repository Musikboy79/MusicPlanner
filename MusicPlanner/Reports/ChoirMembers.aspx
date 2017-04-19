<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChoirMembers.aspx.cs" Inherits="MusicPlanner.Reports.ChoirMembers" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <rsweb:ReportViewer ID="ChoirReportViewer" runat="server" Font-Names="Verdana" Font-Size="8pt" ProcessingMode="Remote" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
            <ServerReport ReportServerUrl="http://173.88.251.215/reportserver" />
        </rsweb:ReportViewer>
    
    </div>
    </form>
</body>
</html>
