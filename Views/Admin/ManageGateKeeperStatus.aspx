<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageGateKeeperStatus.aspx.cs" MasterPageFile="~/Site.Master" Inherits="RegistrationGatekeeperAdmin.Views.Admin.ManageGateKeeperStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="body-content">
        <h3>Manage GateKeeperStatus</h3>

        <asp:LinkButton ID="lbgkpsts" runat="server" onclick="lbgkpsts_Click">Add a new GateKeeperStatus</asp:LinkButton><br /><br />
        <asp:GridView ID="gvGtkepsts" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="GateKeeperStatusId" DataSourceID="edsGateKeeperStatus"
            ForeColor="#333333" GridLines="None" Width="100%" PageSize="45">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:CommandField ShowEditButton="True" />
                <asp:BoundField DataField="GateKeeperStatusId" HeaderText="GateKeeperStatus Id" ReadOnly="True" SortExpression="GateKeeperStatusId" Visible="false" />
                <asp:BoundField DataField="GateKeeperStatusName" HeaderText="GateKeeperStatus name" SortExpression="GateKeeperStatusName" />
                <asp:CheckBoxField DataField="Active" HeaderText="Active" SortExpression="Active" />
            </Columns>
        </asp:GridView>
        <br />
        <asp:PlaceHolder ID="phNew" runat="server" Visible="false">
            <div class="row">
                <div class="col-md-4">
                    GateKeeperStatus:<br />
                    <asp:TextBox ID="tbGateKeeperStatus" runat="server" Text=""></asp:TextBox>
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

        <asp:LinkButton ID="lbgkpsts2" runat="server" onclick="lbgkpsts_Click">Add a new GateKeeperStatus</asp:LinkButton>
        <asp:EntityDataSource ID="edsGateKeeperStatus" runat="server" ConnectionString="name=MHCC_RegistrationEntities" DefaultContainerName="MHCC_RegistrationEntities" EnableFlattening="False" EnableInsert="True" EnableUpdate="True" EntitySetName="GateKeeperStatus" OrderBy="[it].GateKeeperStatusName" OnInserted="edsGateKeeperStatus_Inserted" OnUpdated="edsGateKeeperStatus_Updated">
        </asp:EntityDataSource>

    </div>
</asp:Content>
