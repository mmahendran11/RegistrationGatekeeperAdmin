<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageSystemAccess.aspx.cs" MasterPageFile="~/Site.Master" Inherits="RegistrationGatekeeperAdmin.Views.Admin.ManageSystemAccess" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="body-content">
        <h3>Manage SystemAccess</h3>

        <asp:LinkButton ID="lbsysaccess" runat="server" onclick="lbsysaccess_Click">Add a new Systemaccess</asp:LinkButton><br /><br />
        <asp:GridView ID="gvSysaccess" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="SystemAccessId" DataSourceID="edsSystemAccesses"
            ForeColor="#333333" GridLines="None" Width="100%" PageSize="45">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:CommandField ShowEditButton="True" />
                <asp:BoundField DataField="SystemAccessId" HeaderText="SystemAccess Id" ReadOnly="True" SortExpression="SystemAccessId" Visible="false" />
                <asp:BoundField DataField="SystemAccessName" HeaderText="SystemAccess name" SortExpression="SystemAccessName" />
                <asp:CheckBoxField DataField="Active" HeaderText="Active" SortExpression="Active" />
            </Columns>
        </asp:GridView>
        <br />
        <asp:PlaceHolder ID="phNew" runat="server" Visible="false">
            <div class="row">
                <div class="col-md-4">
                    SystemAccessName:<br />
                    <asp:TextBox ID="tbSysaccessname" runat="server" Text=""></asp:TextBox>
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

        <asp:LinkButton ID="lbsysaccess2" runat="server" onclick="lbsysaccess_Click">Add a new Systemaccess</asp:LinkButton>
        <asp:EntityDataSource ID="edsSystemAccesses" runat="server" ConnectionString="name=MHCC_RegistrationEntities" DefaultContainerName="MHCC_RegistrationEntities" EnableFlattening="False" EnableInsert="True" EnableUpdate="True" EntitySetName="SystemAccesses" OrderBy="[it].SystemAccessName" OnInserted="edsSystemAccesses_Inserted" OnUpdated="edsSystemAccesses_Updated">
        </asp:EntityDataSource>

    </div>
</asp:Content>
