using RegistrationGatekeeperAdmin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

namespace RegistrationGatekeeperAdmin.Classes
{
    class McUser
    {
        private  MHCC_ADEntities _dbAD = new MHCC_ADEntities();

        public Guid Guid { get; set; }

        public string DisplayName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string JobTitle { get; set; }

        public string Department { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string LastFirstName { get; set; }

        public string Manager { get; set; }

        public string MiddleName { get; set; }

        public string AlternatePhone { get; set; }

        public string DepartmentFloor { get; set; }

        public string MHCNumber { get; set; }

        public string Company { get; set; }

        public string DistinguishedName { get; set; }

        public string UserPrincipalName { get; set; }

        public bool UserEnabled { get; set; }

        //public Guid NMHGuid { get; set; }

        //Keep in lower case comparing with lowercase.
        public static string[] domainWhitelist = { "mclaren.org", "northernhealth.org", "atos.net", "cvrcnm.com", "michiganhvs.com", "mmponline.com", "porthuronhosp.org", "karmanos.org" };

        //This will be faster since it is not querying AD for the userid for every field
        public McUser GetUserByUsername(string username)
        {
            McUser user = new McUser();
            if (username.Contains("\\"))
            {
                username = username.Split('\\')[1];
            }
            if (username.Contains("@"))
            {
                username = username.Split('@')[0];
            }

            var qryColleagues = from x in _dbAD.Colleagues where (x.Username == username) select x;
            if (qryColleagues.Any())
            {
                foreach (var myUser in qryColleagues)
                {
                    user.Guid = myUser.GUID;
                    user.Username = myUser.Username;
                    user.DisplayName = myUser.DisplayName;
                    user.JobTitle = myUser.JobTitle;
                    user.Phone = myUser.Phone;
                    user.Email = myUser.EMail;
                    user.Department = myUser.Department;
                    user.DepartmentFloor = myUser.DepartmentFloor;
                    user.LastName = myUser.LastName;
                    user.FirstName = myUser.FirstName;
                    user.LastFirstName = myUser.LastFirstName;
                    user.Manager = myUser.Manager;
                    user.MiddleName = myUser.MiddleName;
                    user.AlternatePhone = myUser.AlternatePhone;
                    user.Company = myUser.Company;
                    user.MHCNumber = myUser.MHCNumber;
                }
            }

            return user;
        }


        public McUser GetUserByUserGuid(Guid userguid)
        {
            McUser user = new McUser();

            var qryColleagues = from x in _dbAD.Colleagues where (x.GUID == userguid) select x;
            if (qryColleagues.Any())
            {
                foreach (var myUser in qryColleagues)
                {
                    user.Guid = myUser.GUID;
                    user.Username = myUser.Username;
                    user.DisplayName = myUser.DisplayName;
                    user.JobTitle = myUser.JobTitle;
                    user.Phone = myUser.Phone;
                    user.Email = myUser.EMail;
                    user.Department = myUser.Department;
                    user.DepartmentFloor = myUser.DepartmentFloor;
                    user.LastName = myUser.LastName;
                    user.FirstName = myUser.FirstName;
                    user.LastFirstName = myUser.LastFirstName;
                    user.Manager = myUser.Manager;
                    user.MiddleName = myUser.MiddleName;
                    user.AlternatePhone = myUser.AlternatePhone;
                    user.Company = myUser.Company;
                    user.MHCNumber = myUser.MHCNumber;
                }
            }

            return user;
        }

        public static IEnumerable<McUser> GetUsersFromGroup(string group, string searchExpression = "")
        {
            List<McUser> results = new List<McUser>();

            //PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "NMH");
            PrincipalContext ctx = new PrincipalContext(ContextType.Domain);

            GroupPrincipal searchGroup = GroupPrincipal.FindByIdentity(ctx, group);
            if (searchGroup != null)
            {
                foreach (var user in searchGroup.Members.Where(x => x.Name.Contains(searchExpression)))
                {
                    UserPrincipal theUser = user as UserPrincipal;
                    if (theUser.Enabled == true)
                    {
                        //Put the try in so incase someone adds a group in it will just ignore it
                        try
                        {
                            McUser myUser = new McUser();
                            myUser.Guid = new Guid(user.Guid.ToString());
                            myUser.DisplayName = user.DisplayName;
                            myUser.Username = user.SamAccountName;
                            myUser.JobTitle = user.Description;
                            myUser.FirstName = theUser.GivenName;
                            myUser.LastName = theUser.Surname;
                            myUser.LastFirstName = string.Format("{0}, {1}", theUser.Surname, theUser.GivenName);
                            myUser.Phone = theUser.VoiceTelephoneNumber;
                            myUser.Email = theUser.EmailAddress;
                            myUser.MiddleName = theUser.MiddleName;
                            myUser.DistinguishedName = theUser.DistinguishedName;
                            myUser.UserPrincipalName = theUser.UserPrincipalName;
                            myUser.MHCNumber = theUser.EmployeeId;

                            if (string.IsNullOrWhiteSpace(myUser.Email))
                            {
                                myUser.Email = theUser.UserPrincipalName;
                            }
                            else if (myUser.Email.Contains('@'))
                            {
                                if (domainWhitelist.Contains(myUser.Email.Split('@')[1].ToLower()))
                                {
                                    //Email is in whitelist so okay
                                }
                                else
                                {
                                    myUser.Email = theUser.UserPrincipalName;
                                }
                            }
                            else
                            {
                                myUser.Email = theUser.UserPrincipalName;
                            }

                            results.Add(myUser);

                            //Changed so I could do the if part on the email to make sure if on the list or take the UPN
                            //results.Add(new McUser
                            //{
                            //    Guid = new Guid(user.Guid.ToString()),
                            //    DisplayName = user.DisplayName,
                            //    Username = user.SamAccountName,
                            //    JobTitle = user.Description,
                            //    FirstName = theUser.GivenName,
                            //    LastName = theUser.Surname,
                            //    LastFirstName = string.Format("{0}, {1}", theUser.Surname, theUser.GivenName),
                            //    Phone = theUser.VoiceTelephoneNumber,
                            //    Email = theUser.EmailAddress,
                            //    //Email = theUser.UserPrincipalName,
                            //    MiddleName = theUser.MiddleName,
                            //    DistinguishedName = theUser.DistinguishedName,
                            //    UserPrincipalName = theUser.UserPrincipalName
                            //});
                        }
                        catch
                        {
                        };
                    }
                }
            }

            return results;
        }

        public static IEnumerable<McUser> GetUsersFromSearch(string searchExpression)
        {
            List<McUser> results = new List<McUser>();

            //PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "NMH", "OU=User, DC=NMH, DC=NMRHS, DC=NET");
            //PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "McLaren", "OU=MNM, OU=People, DC=Mclaren, DC=Org");
            //PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "McLaren", "OU=People, DC=Mclaren, DC=Org");
            PrincipalContext ctx = new PrincipalContext(ContextType.Domain);

            UserPrincipal searchUser = new UserPrincipal(ctx);
            string searchString = String.Empty;
            if (searchExpression.Length > 0)
            {
                searchString = String.Format("*{0}*", searchExpression);
            }
            else
            {
                searchString = "*";
            }

            searchUser.DisplayName = searchString;
            PrincipalSearcher srch = new PrincipalSearcher(searchUser);

            foreach (var match in srch.FindAll())
            {
                UserPrincipal theUser = match as UserPrincipal;

                McUser myUser = new McUser();
                myUser.Guid = new Guid(match.Guid.ToString());
                myUser.DisplayName = match.DisplayName;
                myUser.Username = match.SamAccountName;
                myUser.JobTitle = match.Description;
                myUser.FirstName = theUser.GivenName;
                myUser.LastName = theUser.Surname;
                myUser.LastFirstName = string.Format("{0}, {1}", theUser.Surname, theUser.GivenName);
                myUser.Phone = theUser.VoiceTelephoneNumber;
                myUser.Email = theUser.EmailAddress;
                myUser.MiddleName = theUser.MiddleName;
                myUser.DistinguishedName = theUser.DistinguishedName;
                myUser.UserPrincipalName = theUser.UserPrincipalName;
                myUser.MHCNumber = theUser.EmployeeId;

                if (string.IsNullOrWhiteSpace(myUser.Email))
                {
                    myUser.Email = theUser.UserPrincipalName;
                }
                else if (myUser.Email.Contains('@'))
                {
                    if (domainWhitelist.Contains(myUser.Email.Split('@')[1].ToLower()))
                    {
                        //Email is in whitelist so okay
                    }
                    else
                    {
                        myUser.Email = theUser.UserPrincipalName;
                    }
                }
                else
                {
                    myUser.Email = theUser.UserPrincipalName;
                }

                results.Add(myUser);

                //Changed so I could do the check on the email field
                //results.Add(new McUser
                //{
                //    Guid = new Guid(match.Guid.ToString()),
                //    DisplayName = match.DisplayName,
                //    Username = match.SamAccountName,
                //    JobTitle = match.Description,
                //    FirstName = theUser.GivenName,
                //    LastName = theUser.Surname,
                //    LastFirstName = string.Format("{0}, {1}", theUser.Surname, theUser.GivenName),
                //    Phone = theUser.VoiceTelephoneNumber,
                //    //Email = theUser.EmailAddress,
                //    Email = theUser.UserPrincipalName,
                //    MiddleName = theUser.MiddleName,
                //    DistinguishedName = theUser.DistinguishedName,
                //    UserPrincipalName = theUser.UserPrincipalName
                //});
            }

            return results;
        }
    }
}
