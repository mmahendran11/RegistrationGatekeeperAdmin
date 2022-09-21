<%@ Page Title="ManageCernerPositions" Language="C#" AutoEventWireup="true" CodeBehind="ManageCernerPosition.aspx.cs" MasterPageFile="~/Site.Master" Inherits="RegistrationGatekeeperAdmin.Views.Admin.ManageCernerPosition" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="body-content">
        <h3>Manage CernerPositions</h3>

        <asp:LinkButton ID="lbCernerPosition" runat="server" onclick="lbCernerPosition_Click">Add a new CernerPositionName</asp:LinkButton><br /><br />
        <asp:GridView ID="gvCernerPositions" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="CernerPositionId" DataSourceID="edsCernerPositions"
            ForeColor="#333333" GridLines="None" Width="100%" PageSize="45">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:CommandField ShowEditButton="True" />
                <asp:BoundField DataField="CernerPositionId" HeaderText="CernerPosition Id" ReadOnly="True" SortExpression="CernerPositionId" Visible="false" />
                <asp:BoundField DataField="CernerPositionName" HeaderText="CernerPosition name" SortExpression="CernerPositionName" />
               
               
                <asp:CheckBoxField DataField="Active" HeaderText="Active" SortExpression="Active" />
            </Columns>
        </asp:GridView>
        <br />
        <asp:PlaceHolder ID="phNew" runat="server" Visible="false">
            <div class="row">
                <div class="col-md-4">
                    CernerPosition Name:<br />
                    <asp:TextBox ID="tbCernerPostionName" runat="server" Text=""></asp:TextBox>
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

        <asp:LinkButton ID="lbCernerPosition2" runat="server" onclick="lbCernerPosition_Click">Add a new CernerPositionName</asp:LinkButton>
        <asp:EntityDataSource ID="edsCernerPositions" runat="server" ConnectionString="name=MHCC_RegistrationEntities" DefaultContainerName="MHCC_RegistrationEntities" EnableFlattening="False" EnableInsert="True" EnableUpdate="True" EntitySetName="CernerPositions" OrderBy="[it].CernerPositionName" OnInserted="edsCernerPositions_Inserted" OnUpdated="edsCernerPositions_Updated">
        </asp:EntityDataSource>

    </div>
</asp:Content>
