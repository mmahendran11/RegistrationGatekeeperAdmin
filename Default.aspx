<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RegistrationGatekeeperAdmin._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="body-content">
        <h1>One McLaren User Registration Portal - Gatekeepers Review</h1>
    </div>

    <div class="row">
        <div class="col-md-3">
            <strong>Registration date created:</strong><br />
            <table style="width: 100%; padding: 5px;">
                <tr>
                    <td style="width: 25%">From:</td>
                    <td style="width: 75%">
                        <asp:TextBox ID="tbRegistrationDateStart" runat="server" TextMode="Date"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%">To:</td>
                    <td style="width: 75%">
                        <asp:TextBox ID="tbRegistrationDateEnd" runat="server" TextMode="Date"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <hr />
            <strong>Primary Organization:</strong><br />
            <asp:DropDownList ID="ddlPrimaryOrganization" runat="server" AppendDataBoundItems="True" DataSourceID="edsOrganization" DataTextField="OrganizationName" DataValueField="OrganizationId">
                <asp:ListItem Selected="True">All</asp:ListItem>
            </asp:DropDownList>
            <asp:EntityDataSource ID="edsOrganization" runat="server" ConnectionString="name=MHCC_RegistrationEntities" DefaultContainerName="MHCC_RegistrationEntities" EnableFlattening="False" EntitySetName="Organizations" Select="it.[OrganizationId], it.[OrganizationName], it.[Active]" OrderBy="[it].OrganizationName" EntityTypeFilter="">
            </asp:EntityDataSource>
            <hr />
            <strong>Type of relationship:</strong><br />
            <asp:DropDownList ID="ddlRelationship" runat="server" AppendDataBoundItems="true" DataSourceID="edsRelationship" DataTextField="RelationshipName" DataValueField="RelationshipId">
                <asp:ListItem>All</asp:ListItem>
            </asp:DropDownList>
            <asp:EntityDataSource ID="edsRelationship" runat="server" ConnectionString="name=MHCC_RegistrationEntities" DefaultContainerName="MHCC_RegistrationEntities" EnableFlattening="False" EntitySetName="Relationships" Select="it.[RelationshipId], it.[RelationshipName], it.[Active]" AutoGenerateWhereClause="True" Where="" OrderBy="[it].RelationshipName" EntityTypeFilter="">
            </asp:EntityDataSource>
            <hr />
            <strong>First name contains:</strong><br />
            <asp:TextBox ID="tbFirstNameContains" runat="server"></asp:TextBox>
            <hr />
            <strong>Middle name contains:</strong><br />
            <asp:TextBox ID="tbMiddleNameContains" runat="server"></asp:TextBox>
            <hr />
            <strong>Last name contains:</strong><br />
            <asp:TextBox ID="tbLastNameContains" runat="server"></asp:TextBox>
            <hr />
        </div>

        <div class="col-md-8">
            <div class="row">
                <div class="col-md-2">
                    <asp:Button ID="btnRefresh" runat="server" Text="Apply Filters" OnClick="btnRefresh_Click" />
                </div>
                <div class="col-md-2">
                    <asp:Button ID="btnReset" runat="server" Text="Reset Default" CausesValidation="false" OnClick="btnReset_Click" />
                </div>
            </div>
            <asp:Panel ID="pnlMassAssignGatekeeper" runat="server" Visible='<%# isGateKeeper() %>'>
                <div class="row">
                    <div class="col-md-4">
                        <strong>Gatekeeper Notes:</strong><br />
                        <asp:TextBox ID="tbGatekeeperNote" runat="server" TextMode="MultiLine" MaxLength="250"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <strong>Gatekeeper Status:</strong><br />
                        <asp:DropDownList ID="ddlGatekeeperStatus" runat="server" DataSourceID="edsGatekeeperStatus" DataTextField="GateKeeperStatusName" DataValueField="GateKeeperStatusId" AppendDataBoundItems="True">
                            <asp:ListItem Selected="True" Text="Not Assigned"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:EntityDataSource ID="edsGatekeeperStatus" runat="server" ConnectionString="name=MHCC_RegistrationEntities" DefaultContainerName="MHCC_RegistrationEntities" EnableFlattening="False" EntitySetName="GateKeeperStatus" OrderBy="[it].GateKeeperStatusName">
                        </asp:EntityDataSource>
                    </div>
                    <div class="col-md-4">
                        <strong>Gatekeeper Cerner Position:</strong><br />
                        <asp:DropDownList ID="ddlGatekeeperCernerPosition" runat="server" DataSourceID="edsGatekeeperCernerPosition" DataTextField="CernerPositionName" DataValueField="CernerPositionId" AppendDataBoundItems="True">
                            <asp:ListItem Selected="True" Text="Not Assigned"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:EntityDataSource ID="edsGatekeeperCernerPosition" runat="server" ConnectionString="name=MHCC_RegistrationEntities" DefaultContainerName="MHCC_RegistrationEntities" EnableFlattening="False" EntitySetName="CernerPositions" OrderBy="[it].CernerPositionName" AutoGenerateWhereClause="true" Where="">
                            <WhereParameters>
                                <asp:Parameter DbType="Boolean" DefaultValue="True" Name="Active" />
                            </WhereParameters>
                        </asp:EntityDataSource>
                    </div>
                </div>
            </asp:Panel>
            <br />
            <div class="row">
                <div class="col-md-11">
                    <asp:GridView ID="gvRegistration" runat="server" AutoGenerateColumns="False" DataKeyNames="RegistrationId" DataSourceID="edsRegistration" AllowPaging="True" AllowSorting="True" OnRowCommand="FireRowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="Assign">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnMassAssign" runat="server" CommandArgument='<%# Eval("RegistrationId") %>' CommandName="MassAssign" Text="Mass Update Values" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" />
                            <asp:BoundField DataField="RegistrationId" HeaderText="Reg Id" ReadOnly="True" SortExpression="RegistrationId" />
                            <asp:BoundField DataField="CreatedDateTime" HeaderText="Created Date Time" SortExpression="CreatedDateTime" />
                            <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName" />
                            <asp:BoundField DataField="MiddleName" HeaderText="Middle Name" SortExpression="MiddleName" />
                            <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName" />
                            <asp:BoundField DataField="OrganizationAbbreviation" HeaderText="Organization" SortExpression="OrganizationAbbreviation" />
                            <asp:BoundField DataField="RelationshipName" HeaderText="Relationship" SortExpression="RelationshipName" />
                            <asp:BoundField DataField="GatekeeperStatusName" HeaderText="GK Status" SortExpression="GatekeeperStatusName" />
                            <asp:BoundField DataField="GatekeeperDisplayName" HeaderText="GK Name" SortExpression="GatekeeperDisplayName" />
                        </Columns>
                        <EmptyDataTemplate>
                            <strong>There are no records that match the provided requirements.</strong>
                        </EmptyDataTemplate>
                    </asp:GridView>
                    <asp:EntityDataSource ID="edsRegistration" runat="server" ConnectionString="name=MHCC_RegistrationEntities" DefaultContainerName="MHCC_RegistrationEntities"
                        EnableFlattening="False" EnableUpdate="True" EntitySetName="Registrations" OrderBy="it.CreatedDateTime" Include="SystemIdentitys">
                    </asp:EntityDataSource>
                    <asp:QueryExtender ID="qeedsRegistration" runat="server" TargetControlID="edsRegistration">
                        <asp:CustomExpression OnQuerying="FilterReport"></asp:CustomExpression>
                
                        <asp:RangeExpression DataField="CreatedDateTime" MinType="Inclusive" MaxType="Inclusive">
                            <asp:ControlParameter ControlID="tbRegistrationDateStart" />
                            <asp:ControlParameter ControlID="tbRegistrationDateEnd" />
                        </asp:RangeExpression>
                
                        <asp:SearchExpression SearchType="Contains" DataFields="LastName">
                            <asp:ControlParameter ControlID="tbLastNameContains" />
                        </asp:SearchExpression>
                        <asp:SearchExpression SearchType="Contains" DataFields="FirstName">
                            <asp:ControlParameter ControlID="tbFirstNameContains" />
                        </asp:SearchExpression>
                        <asp:SearchExpression SearchType="Contains" DataFields="MiddleName">
                            <asp:ControlParameter ControlID="tbMiddleNameContains" />
                        </asp:SearchExpression>
                    </asp:QueryExtender>
                </div>
            </div>

        </div>
    </div>

</asp:Content>
