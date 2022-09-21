<%@ Page  Title="Admin" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Index.aspx.cs" Inherits="RegistrationGatekeeperAdmin.Views.Admin.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="body-content">
        <h1>Admin page</h1>
    </div>
    <br />
    <div class="row">
        
        <div class="col-md-4">
            <p><a class="btn btn-default" href="ManageOrganizations.aspx">Manage organizations &raquo;</a></p>
            <p>
                Select this option if you want to manage the organization list.
            </p>
        </div>
        <div class="col-md-4">
            <p><a class="btn btn-default" href="Credentials.aspx">Manage Credentials &raquo;</a></p>
            <p>Select this option if you want to know the Credentials.</p>
        </div>
        <div class="col-md-4">
            <p><a class="btn btn-default" href="ManageCernerPosition.aspx">Manage Cerner Position &raquo;</a></p>
            <p>
                Select this option if you want to know Cerner Position.
            </p>
        </div>
       
    </div>
    <div class="row">
        <div class="col-md-4">
            <p><a class="btn btn-default" href="ManageNursingAncillaryCredential.aspx">Manage NursingAncillaryCredential &raquo;</a></p>
            <p>
                Select this option if you want to manage the NursingAncillaryCredential.
            </p>
        </div>
        <div class="col-md-4">
            <p><a class="btn btn-default" href="ManageSystemAccess.aspx">Manage SystemAccess &raquo;</a></p>
            <p>
                Select this option if you want to manage the SystemAccess.
            </p>
        </div>

        <div class="col-md-4">
            <p><a class="btn btn-default" href="ManageGateKeeperStatus.aspx">Manage GateKeeperStatus &raquo;</a></p>
            <p>
                Select this option if you want to manage the GateKeeperStatus.
            </p>
        </div>

    </div>

    

    <%--This section is really more for web developer type--%>
    

</asp:Content>
