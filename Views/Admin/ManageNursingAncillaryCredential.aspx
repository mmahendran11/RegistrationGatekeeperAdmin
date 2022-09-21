<%@ Page Title="ManageNursingAncillaryCredential"  Language="C#" AutoEventWireup="true" CodeBehind="ManageNursingAncillaryCredential.aspx.cs"  MasterPageFile="~/Site.Master"  Inherits="RegistrationGatekeeperAdmin.Views.Admin.ManageNursingAncillaryCredential" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="body-content">
        <h3>Manage NursingAncillaryCredential</h3>

        <asp:LinkButton ID="lbNursing2" runat="server" onclick="lbNursing_Click">Add a new NursingAncillaryCredential</asp:LinkButton><br /><br />
        <asp:GridView ID="gvNursingAncillaryCredentials" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="NursingAncillaryCredentialId" DataSourceID="edsNursingAncillaryCredentials"
            ForeColor="#333333" GridLines="None" Width="100%" PageSize="45">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:CommandField ShowEditButton="True" />
                <asp:BoundField DataField="NursingAncillaryCredentialId" HeaderText="Organization Id" ReadOnly="True" SortExpression="NursingAncillaryCredentialId" Visible="false" />
                <asp:BoundField DataField="Certification" HeaderText="Certification name" SortExpression="Certification" />
                <asp:BoundField DataField="CertificationCode" HeaderText="Certification Code" SortExpression="CertificationCode" />
               
                <asp:CheckBoxField DataField="Active" HeaderText="Active" SortExpression="Active" />
            </Columns>
        </asp:GridView>
        <br />
        <asp:PlaceHolder ID="phNew" runat="server" Visible="false">
            <div class="row">
                <div class="col-md-4">
                    NursingAncillaryCredential  Code:<br />
                    <asp:TextBox ID="tbCertificationCode" runat="server" Text=""></asp:TextBox>
                </div>
                <div class="col-md-4">
                    NursingAncillaryCredential Name:<br />
                    <asp:TextBox ID="tbCertification" runat="server" Text=""></asp:TextBox>
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

        <asp:LinkButton ID="lbNursing" runat="server" onclick="lbNursing_Click">Add a new NursingAncillaryCredential</asp:LinkButton>
        <asp:EntityDataSource ID="edsNursingAncillaryCredentials" runat="server" ConnectionString="name=MHCC_RegistrationEntities" DefaultContainerName="MHCC_RegistrationEntities" EnableFlattening="False" EnableInsert="True" EnableUpdate="True" EntitySetName="NursingAncillaryCredentials" OrderBy="[it].Certification" OnInserted="edsNursingAncillaryCredentials_Inserted" OnUpdated="edsNursingAncillaryCredentials_Updated">
        </asp:EntityDataSource>

    </div>
</asp:Content>
