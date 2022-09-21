<%@ Page Title="Manage Credentials"  Language="C#" AutoEventWireup="true" CodeBehind="Credentials.aspx.cs" MasterPageFile="~/Site.Master" Inherits="RegistrationGatekeeperAdmin.Views.Admin.Credentials" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="body-content">
        <h3>Manage Credentials</h3>

        <asp:LinkButton ID="lbCredential2" runat="server" onclick="lbCredential_Click">Add a new Credential</asp:LinkButton><br /><br />
        <asp:GridView ID="gvCredential" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="CredentialId" DataSourceID="edsCredentials"
            ForeColor="#333333" GridLines="None" Width="100%" PageSize="30">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:CommandField ShowEditButton="True" />
                <asp:BoundField DataField="CredentialId" HeaderText="Credential Id" ReadOnly="True" SortExpression="CredentialId" Visible="false" />
                <asp:BoundField DataField="CredentialName" HeaderText="Credential name" SortExpression="CredentialName" />
               
               
                <asp:CheckBoxField DataField="Active" HeaderText="Active" SortExpression="Active" />
            </Columns>
        </asp:GridView>
        <br />
        <asp:PlaceHolder ID="phNew" runat="server" Visible="false">
            <div class="row">
                <div class="col-md-4">
                    Credential name:<br />
                    <asp:TextBox ID="tbCredentialName" runat="server" Text=""></asp:TextBox>
                </div>
                
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

        <asp:LinkButton ID="lbCredential" runat="server" onclick="lbCredential_Click">Add a new Credential</asp:LinkButton>
        <asp:EntityDataSource ID="edsCredentials" runat="server" ConnectionString="name=MHCC_RegistrationEntities" DefaultContainerName="MHCC_RegistrationEntities" EnableFlattening="False" EnableInsert="True" EnableUpdate="True" EntitySetName="Credentials" OrderBy="[it].CredentialName" OnInserted="edsCredentials_Inserted" OnUpdated="edsCredentials_Updated">
        </asp:EntityDataSource>

    </div>
</asp:Content>
