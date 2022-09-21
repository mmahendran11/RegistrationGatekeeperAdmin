<%@ Page Title="Manage Organizations"  Language="C#" AutoEventWireup="true"  MasterPageFile="~/Site.Master" CodeBehind="ManageOrganizations.aspx.cs" Inherits="RegistrationGatekeeperAdmin.Views.Admin.ManageOrganizations" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="body-content">
        <h3>Manage Organizations</h3>

        <asp:LinkButton ID="lbNewOrganization2" runat="server" onclick="lbNewOrganization_Click">Add a new organization</asp:LinkButton><br /><br />
        <asp:GridView ID="gvOrganizations" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="OrganizationId" DataSourceID="edsOrganizations"
            ForeColor="#333333" GridLines="None" Width="100%" PageSize="45">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:CommandField ShowEditButton="True" />
                <asp:BoundField DataField="OrganizationId" HeaderText="Organization Id" ReadOnly="True" SortExpression="OrganizationId" Visible="false" />
                <asp:BoundField DataField="OrganizationName" HeaderText="Organization name" SortExpression="OrganizationName" />
                <asp:BoundField DataField="OrganizationAbbreviation" HeaderText="Organization Code" SortExpression="OrganizationAbbreviation" />
               
                <asp:CheckBoxField DataField="Active" HeaderText="Active" SortExpression="Active" />
            </Columns>
        </asp:GridView>
        <br />
        <asp:PlaceHolder ID="phNew" runat="server" Visible="false">
            <div class="row">
                <div class="col-md-4">
                    Organization name:<br />
                    <asp:TextBox ID="tbOrganizationName" runat="server" Text=""></asp:TextBox>
                </div>
                <div class="col-md-4">
                    Active directory admin group:<br />
                    <asp:TextBox ID="tbOrgCode" runat="server" Text=""></asp:TextBox>
                </div>
                 <%--<div class="col-md-4">
                    "Admin Active directory Manager group:<br />
                    <asp:TextBox ID="tbADMgrGroup" runat="server" Text=""></asp:TextBox>
                </div>--%>
                <div class="col-md-2">
                    <br />
                    <asp:CheckBox ID="cbActive" runat="server" Checked="true" Text="Active" />
                </div>
                <div class="col-md-2">
                    <br />
                    <asp:LinkButton ID="lbInsert" runat="server" Text="Insert" OnClick="lbInsert_Click" />
                    &nbsp;
                    <asp:LinkButton ID="lbClose" runat="server" onclick="lbClose_Click" Text="Close" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
                </div>
            </div>

        </asp:PlaceHolder>

        <asp:LinkButton ID="lbNewOrganization" runat="server" onclick="lbNewOrganization_Click">Add a new organization</asp:LinkButton>
        <asp:EntityDataSource ID="edsOrganizations" runat="server" ConnectionString="name=MHCC_RegistrationEntities" DefaultContainerName="MHCC_RegistrationEntities" EnableFlattening="False" EnableInsert="True" EnableUpdate="True" EntitySetName="Organizations" OrderBy="[it].OrganizationName" OnInserted="edsOrganizations_Inserted" OnUpdated="edsOrganizations_Updated">
        </asp:EntityDataSource>

    </div>
</asp:Content>