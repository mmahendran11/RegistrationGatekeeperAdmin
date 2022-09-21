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
                        <asp:EntityDataSource ID="edsGatekeeperStatus" runat="server" ConnectionString="name=MHCC_RegistrationEntities" DefaultContainerName="MHCC_RegistrationEntities" EnableFlattening="False" EntitySetName="GateKeeperStatus" OrderBy="[it].GateKeeperStatusName" AutoGenerateWhereClause="true" Where="">
                            <WhereParameters>
                                <asp:Parameter DbType="Boolean" DefaultValue="True" Name="Active" />
                            </WhereParameters>
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
                                    <asp:LinkButton ID="btnMassAssign" runat="server" CommandArgument='<%# Eval("RegistrationId") %>' CommandName="MassAssign" Text="Update GK" />
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
                            <asp:BoundField DataField="CernerPositionName" HeaderText="Cerner Position" SortExpression="CernerPositionName" />
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
                    <asp:Panel ID="pnlDetail" runat="server">
                        <br />
                        <asp:FormView ID="fvRegistration" runat="server" DataKeyNames="RegistrationId" DataSourceID="edsRegistrationSelected" Width="100%">
                            <ItemTemplate>
                                <div class="row .active">
                                    <div class="col-md-4">
                                        <strong>Registration Id:</strong><br />
                                        <asp:Label ID="RegistrationIdLabel" runat="server" Text='<%# Eval("RegistrationId") %>' />
                                    </div>
                                    <div class="col-md-4">
                                        <strong>Created Date Time:</strong><br />
                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("CreatedDateTime") %>' />
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-4">
                                        <strong>First Name:</strong><br />
                                        <asp:Label ID="FirstNameLabel" runat="server" Text='<%# Bind("FirstName") %>' /><br />
                                        <asp:TextBox ID="tbFirstName" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <strong>Middle Name:</strong><br />
                                        <asp:Label ID="MiddleNameLabel" runat="server" Text='<%# Bind("MiddleName") %>' /><br />
                                        <asp:TextBox ID="tbMiddleName" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <strong>Last Name:</strong><br />
                                        <asp:Label ID="LastNameLabel" runat="server" Text='<%# Bind("LastName") %>' /><br />
                                        <asp:TextBox ID="tbLastName" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-6">
                                        <strong>Relationship:</strong><br />
                                        <asp:Label ID="RelationshipIdLabel" runat="server" Text='<%# Eval("Relationship.RelationshipName") %>' />
                                    </div>
                                    <div class="col-md-6">
                                        <strong>Primary Organization:</strong><br />
                                        <asp:Label ID="PrimaryOrganizationIdLabel" runat="server" Text='<%# Bind("Organization.OrganizationName") %>' /><br />
                                        <asp:DropDownList ID="ddlPrimaryLocation" runat="server" DataSourceID="edsOrganization" DataTextField="OrganizationName" DataValueField="OrganizationId" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Choose One</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:EntityDataSource ID="edsOrganization" runat="server" ConnectionString="name=MHCC_RegistrationEntities" DefaultContainerName="MHCC_RegistrationEntities" EnableFlattening="False" EntitySetName="Organizations" Select="it.[OrganizationId], it.[OrganizationName], it.[Active]" AutoGenerateWhereClause="True" Where="" OrderBy="[it].OrganizationName" EntityTypeFilter="">
                                            <%-- Want to show all for gatekeeper--%>
                                            <%--
                                            <WhereParameters>
                                                <asp:Parameter DbType="Boolean" DefaultValue="True" Name="Active" />
                                            </WhereParameters>
                                            --%>
                                        </asp:EntityDataSource>
                                    </div>
                                </div>
                                <br />
                                <asp:Panel ID="pnlEmployee" runat="server" Visible='<%# (((int)Eval("Relationship.FlowType") == 1) || ((int)Eval("Relationship.FlowType") == 2)) %>'>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <strong>HR Employee Id:</strong><br />
                                            <asp:Label ID="HREmployeeIdLabel" runat="server" Text='<%# Eval("HREmployeeId") %>' />
                                            <asp:TextBox ID="tbHREmployeeId" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4">
                                            <strong>Email</strong><br />
                                            <asp:Label ID="HREmployeeEmailLabel" runat="server" Text='<%# Eval("HREmployeeEmail") %>' />
                                            <asp:TextBox ID="tbHREmployeeEmail" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4">
                                            <strong>Supervisor</strong><br />
                                            <asp:Label ID="SupervisorLabel" runat="server" Text='<%# Eval("HRSupervisor") %>' />
                                            <asp:TextBox ID="tbHRSupervisor" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-4">
                                            <strong>HR Company:</strong><br />
                                            <asp:Label ID="HRCompanyLabel" runat="server" Text='<%# Eval("HRCompany") %>' />
                                            <asp:TextBox ID="tbHRCompany" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4">
                                            <strong>HR Department</strong><br />
                                            <asp:Label ID="HRDepartmentLabel" runat="server" Text='<%# Eval("HRDepartment") %>' />
                                            <asp:TextBox ID="tbHRDepartment" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                </asp:Panel>
                                <asp:Panel ID="pnlProvider" runat="server" Visible='<%# ((int)Eval("Relationship.FlowType") == 4) %>'>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <strong>Provider NPI:</strong><br />
                                            <asp:Label ID="ProviderNPILabel" runat="server" Text='<%# Eval("ProviderNPI") %>' /><br />
                                            <asp:TextBox ID="tbProviderNPI" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4">
                                            <strong>ProviderSpecialty:</strong><br />
                                            <asp:Label ID="ProviderSpecialtyLabel" runat="server" Text='<%# Eval("ProviderSpecialty") %>' /><br />
                                            <asp:TextBox ID="tbProviderSpecialty" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4">
                                            <strong>Credential:</strong><br />
                                            <asp:Label ID="ProviderCredentialLabel" runat="server" Text='<%# Eval("ProviderCredentialsName") %>' /><br />
                                            <asp:DropDownList ID="ddlProviderCredentials" runat="server" DataSourceID="edsCredentials" DataTextField="CredentialName" DataValueField="CredentialId" AppendDataBoundItems="true">
                                                <asp:ListItem Value="0">Choose One</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <br />
                                </asp:Panel>
                                <asp:Panel ID="pnlNursing" runat="server" Visible='<%# ((int)Eval("Relationship.FlowType") == 6) %>'>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <strong>Nursing Ancillary Credential:</strong><br />
                                            <asp:Label ID="NursingAncillaryCredentialLabel" runat="server" Text='<%# Eval("NursingAncillaryCertification") %>' /><br />
                                            <asp:DropDownList ID="ddlNursingAncillaryCredential" runat="server" DataSourceID="edsNursingAncillaryCredentials" DataTextField="Certification" DataValueField="NursingAncillaryCredentialId" AppendDataBoundItems="true">
                                                <asp:ListItem Value="0">Choose One</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <br />
                                </asp:Panel>
                                <asp:Panel ID="pnlNonEmployee" runat="server" Visible='<%# (!(((int)Eval("Relationship.FlowType") == 1) || ((int)Eval("Relationship.FlowType") == 2))) %>'>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <strong>Title:</strong><br />
                                            <asp:Label ID="TitleExternalLabel" runat="server" Text='<%# Eval("TitleExternal") %>' /><br />
                                            <asp:TextBox ID="tbTitleExternal" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4">
                                            <strong>Company:</strong><br />
                                            <asp:Label ID="CompanyExternalLabel" runat="server" Text='<%# Eval("CompanyExternal") %>' /><br />
                                            <asp:TextBox ID="tbCompanyExternal" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4">
                                            <strong>City:</strong><br />
                                            <asp:Label ID="CityExternalLabel" runat="server" Text='<%# Eval("CityExternal") %>' /><br />
                                            <asp:TextBox ID="tbCityExternal" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-4">
                                            <strong>Conact name:</strong><br />
                                            <asp:Label ID="ContactNameExternalLabel" runat="server" Text='<%# Eval("ContactNameExternal") %>' /><br />
                                            <asp:TextBox ID="tbContactNameExternal" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4">
                                            <strong>Contact phone number:</strong><br />
                                            <asp:Label ID="ContactPhoneExternalLabel" runat="server" Text='<%# Eval("ContactPhoneExternal") %>' /><br />
                                            <asp:TextBox ID="tbContactPhoneExternal" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                </asp:Panel>
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:Button ID="btnUpdateUserInfo" runat="server" Text="Update User Information" OnClick="btnUpdateUserInfo_Click" BackColor="#3333FF" ForeColor="White" />
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-4">
                                        <strong>Gatekeeper Status:</strong><br />
                                        <asp:Label ID="GateKeeperStatusIdLabel" runat="server" Text='<%# Eval("GateKeeperStatu.GateKeeperStatusName") %>' />
                                    </div>
                                    <div class="col-md-4">
                                        <strong>Gatekeeper Name:</strong><br />
                                        <asp:Label ID="GateKeeperValidatedDisplayNameLabel" runat="server" Text='<%# Eval("GateKeeperDisplayName") %>' />
                                    </div>
                                    <div class="col-md-4">
                                        <strong>Gatekeeper Date:</strong><br />
                                        <asp:Label ID="GateKeeperValidatedDateLabel" runat="server" Text='<%# Bind("GateKeeperValidatedDate") %>' />
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <strong>Cerner Position:</strong><br />
                                        <asp:Label ID="CernerPositionIdLabel" runat="server" Text='<%# Bind("CernerPositionName") %>' />
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <strong>Gatekeeper Notes:</strong><br />
                                        <asp:Label ID="GateKeeperNoteLabel" runat="server" Text='<%# Bind("GateKeeperNote") %>' />
                                    </div>
                                </div>
                                <br />

<%--                                
                                <asp:Panel ID="pnlGateKeeper" runat="server" Visible='<%# isGateKeeper() %>'>
                                    <hr />
                                    <div class="row">
                                        <div class="col-md-11">
                                            <strong>Gatekeeper Notes:</strong><br />
                                            <asp:TextBox ID="tbGatekeeperNote" runat="server" MaxLength="250"></asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-11">
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
                                    <br />
                                    <div class="row">
                                        <div class="col-md-11">
                                            <strong>Gatekeeper Status:</strong><br />
                                            <asp:DropDownList ID="ddlGatekeeperStatus" runat="server" DataSourceID="edsGatekeeperStatus" DataTextField="GateKeeperStatusName" DataValueField="GateKeeperStatusId" AppendDataBoundItems="True">
                                                <asp:ListItem Selected="True" Text="Not Assigned"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:EntityDataSource ID="edsGatekeeperStatus" runat="server" ConnectionString="name=MHCC_RegistrationEntities" DefaultContainerName="MHCC_RegistrationEntities" EnableFlattening="False" EntitySetName="GateKeeperStatus" OrderBy="[it].GateKeeperStatusName">
                                            </asp:EntityDataSource>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-11">
                                            <asp:Button ID="btnUpdateGateKeeper" runat="server" Text="Update Gatekeeper Information" OnClick="btnUpdateGateKeeper_Click" BackColor="#3333FF" ForeColor="White" />
                                        </div>
                                    </div>
                                </asp:Panel>
--%>

                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <strong>There is no record selected for view/editting.</strong>
                            </EmptyDataTemplate>

                        </asp:FormView>

                        <asp:EntityDataSource ID="edsRegistrationSelected" runat="server" ConnectionString="name=MHCC_RegistrationEntities" DefaultContainerName="MHCC_RegistrationEntities" EnableFlattening="False" EntitySetName="Registrations" Where="" AutoGenerateWhereClause="true" Include="Relationship, Organization, SystemIdentitys, SystemIdentitys.SystemAccess, SystemIdentitys.Organization, GateKeeperStatu" EnableUpdate="True">
                            <WhereParameters>
                                <asp:ControlParameter ControlID="gvRegistration" DbType="Int32" Name="RegistrationId" DefaultValue="0" PropertyName="SelectedValue" />
                            </WhereParameters>
                        </asp:EntityDataSource>
                        <asp:EntityDataSource ID="edsCredentials" runat="server" ConnectionString="name=MHCC_RegistrationEntities" DefaultContainerName="MHCC_RegistrationEntities" EnableFlattening="False" EntitySetName="Credentials" OrderBy="[it].CredentialName">
                        </asp:EntityDataSource>
                        <asp:EntityDataSource ID="edsNursingAncillaryCredentials" runat="server" ConnectionString="name=MHCC_RegistrationEntities" DefaultContainerName="MHCC_RegistrationEntities" EnableFlattening="False" EntitySetName="NursingAncillaryCredentials" OrderBy="[it].Certification">
                        </asp:EntityDataSource>

                    </asp:Panel>

                </div>
            </div>

        </div>
    </div>

</asp:Content>
