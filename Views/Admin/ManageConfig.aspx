<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageConfig.aspx.cs" MasterPageFile="~/Site.Master"  Inherits="RegistrationGatekeeperAdmin.Views.Admin.ManageConfig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="body-content">
        <h3>Manage Passwords</h3>

        <asp:LinkButton ID="lbConfig" runat="server" onclick="lbConfig_Click">Add a new GateKeeperStatus</asp:LinkButton><br /><br />
        <asp:GridView ID="gvGtkepsts" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="ConfigId" DataSourceID="edsConfigs"
            ForeColor="#333333" GridLines="None" Width="100%" PageSize="45">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:CommandField ShowEditButton="True" />
                <asp:BoundField DataField="ConfigId" HeaderText="Config Id" ReadOnly="True" SortExpression="ConfigId" Visible="false" />
                <asp:BoundField DataField="Password" HeaderText="Password name" SortExpression="Password" />
                <asp:BoundField DataField="Notes" HeaderText="Notes name" SortExpression="Notes" />
                <asp:CheckBoxField DataField="ConfirmTransferChoice" HeaderText="Confirm" SortExpression="ConfirmTransferChoice" />
            </Columns>
        </asp:GridView>
        <br />
        <asp:PlaceHolder ID="phNew" runat="server" Visible="false">
            <div class="row">
                <div class="col-md-4">
                    Password:<br />
                    <asp:TextBox ID="tbpwd" runat="server" Text=""></asp:TextBox>
                </div>

                 <div class="col-md-4">
                    Notes:<br />
                    <asp:TextBox ID="tbNotes" runat="server" Text=""></asp:TextBox>
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

        <asp:LinkButton ID="lbConfig2" runat="server" onclick="lbConfig_Click">Add a new Password</asp:LinkButton>
        <asp:EntityDataSource ID="edsConfigs" runat="server" ConnectionString="name=MHCC_RegistrationEntities" DefaultContainerName="MHCC_RegistrationEntities" EnableFlattening="False" EnableInsert="True" EnableUpdate="True" EntitySetName="Configs" OrderBy="[it].Notes" OnInserted="edsConfigs_Inserted" OnUpdated="edsConfigs_Updated">
        </asp:EntityDataSource>

    </div>
</asp:Content>