using RegistrationGatekeeperAdmin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RegistrationGatekeeperAdmin.Classes
{

    public class DBADUser
    {
        private MHCC_ADEntities _db = new MHCC_ADEntities();

        public Guid Guid { get; set; }

        public string Username { get; set; }

        public string DisplayName { get; set; }

        public string JobTitle { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string LastFirstName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Department { get; set; }

        public string Manager { get; set; }

        public string AlternatePhone { get; set; }

        public string DepartmentFloor { get; set; }

        public string Company { get; set; }

        public string DistinguishedName { get; set; }

        public DateTime LastUpdated { get; set; }

        public string MHCNumber { get; set; }

        public string MemberOf { get; set; }

        public DBADUser GetAllByUsername(string username)
        {
            if (username.Contains("\\"))
            {
                username = username.Split('\\')[1];
            }
            if (username.Contains("@"))
            {
                username = username.Split('@')[0];
            }

            DBADUser user = new DBADUser();

            var qry = (from x in _db.Colleagues where x.Username.ToLower() == (username.ToLower()) select x);
            if (qry.Any())
            {
                foreach (var item in qry)
                {
                    user.Guid = item.GUID;
                    user.Username = item.Username;
                    user.DisplayName = item.DisplayName;
                    user.JobTitle = item.JobTitle;
                    user.FirstName = item.FirstName;
                    user.MiddleName = item.MiddleName;
                    user.LastName = item.LastName;
                    user.LastFirstName = item.LastFirstName;
                    user.Phone = item.Phone;
                    user.Email = item.EMail;
                    user.Department = item.Department;
                    user.Manager = item.Manager;
                    user.AlternatePhone = item.AlternatePhone;
                    user.DepartmentFloor = item.DepartmentFloor;
                    user.Company = item.Company;
                    user.DistinguishedName = item.DistinguishedName;
                    user.LastUpdated = item.LastUpdated;
                    user.MHCNumber = item.MHCNumber;
                    user.MemberOf = item.MemberOf;
                }
            }
            return user;
        }

        public DBADUser GetUserByUsername(string username)
        {
            if (username.Contains("\\"))
            {
                username = username.Split('\\')[1];
            }
            if (username.Contains("@"))
            {
                username = username.Split('@')[0];
            }

            DBADUser user = new DBADUser();

            var qry = (from x in _db.Colleagues where x.Username.ToLower() == (username.ToLower()) select x);
            if (qry.Any())
            {
                foreach (var item in qry)
                {
                    user.Guid = item.GUID;
                    user.Username = item.Username;
                    user.DisplayName = item.DisplayName;
                    user.JobTitle = item.JobTitle;
                    user.FirstName = item.FirstName;
                    user.MiddleName = item.MiddleName;
                    user.LastName = item.LastName;
                    user.LastFirstName = item.LastFirstName;
                    user.Phone = item.Phone;
                    user.Email = item.EMail;
                    user.Department = item.Department;
                    user.Manager = item.Manager;
                    user.AlternatePhone = item.AlternatePhone;
                    user.DepartmentFloor = item.DepartmentFloor;
                    user.Company = item.Company;
                    user.DistinguishedName = item.DistinguishedName;
                    user.LastUpdated = item.LastUpdated;
                    user.MHCNumber = item.MHCNumber;
                    user.MemberOf = item.MemberOf;
                }
            }
            return user;
        }

        public static IEnumerable<DBADUser> GetUsersFromSearch(string searchExpression, string byField)
        {
            MHCC_ADEntities db = new MHCC_ADEntities();
            List<DBADUser> results = new List<DBADUser>();

            searchExpression = searchExpression.ToLower();

            switch (byField.ToLower())
            {
                case "email address":
                    var qryEmailAddress = (from x in db.Colleagues where (x.EMail.ToLower().Contains(searchExpression) && x.Active==true) select x);
                    foreach (var item in qryEmailAddress)
                    {
                        results.Add(new DBADUser
                        {
                            Guid = item.GUID,
                            Username = item.Username,
                            DisplayName = item.DisplayName,
                            JobTitle = item.JobTitle,
                            FirstName = item.FirstName,
                            MiddleName = item.MiddleName,
                            LastName = item.LastName,
                            LastFirstName = item.LastFirstName,
                            Phone = item.Phone,
                            Email = item.EMail,
                            Department = item.Department,
                            Manager = item.Manager,
                            AlternatePhone = item.AlternatePhone,
                            DepartmentFloor = item.DepartmentFloor,
                            Company = item.Company,
                            DistinguishedName = item.DistinguishedName,
                            LastUpdated = item.LastUpdated,
                            MHCNumber = item.MHCNumber,
                            MemberOf = item.MemberOf
                        });
                    }
                    break;
                case "display name":
                    var qryDisplayName = (from x in db.Colleagues where (x.DisplayName.ToLower().Contains(searchExpression) && x.Active == true) select x);
                    foreach (var item in qryDisplayName)
	                {
		                results.Add(new DBADUser
                            {
                                Guid = item.GUID,
                                Username = item.Username,
                                DisplayName = item.DisplayName,
                                JobTitle = item.JobTitle,
                                FirstName = item.FirstName,
                                MiddleName = item.MiddleName,
                                LastName = item.LastName,
                                LastFirstName = item.LastFirstName,
                                Phone = item.Phone,
                                Email = item.EMail,
                                Department = item.Department,
                                Manager = item.Manager,
                                AlternatePhone = item.AlternatePhone,
                                DepartmentFloor = item.DepartmentFloor,
                                Company = item.Company,
                                DistinguishedName = item.DistinguishedName,
                                LastUpdated = item.LastUpdated,
                                MHCNumber = item.MHCNumber,
                                MemberOf = item.MemberOf
                            });
	                }
                    break;
                case "userid full":
                    var qryUserIdFull = (from x in db.Colleagues where (x.Username.ToLower() == searchExpression && x.Active == true) select x);
                    foreach (var item in qryUserIdFull)
                    {
                        results.Add(new DBADUser
                        {
                            Guid = item.GUID,
                            Username = item.Username,
                            DisplayName = item.DisplayName,
                            JobTitle = item.JobTitle,
                            FirstName = item.FirstName,
                            MiddleName = item.MiddleName,
                            LastName = item.LastName,
                            LastFirstName = item.LastFirstName,
                            Phone = item.Phone,
                            Email = item.EMail,
                            Department = item.Department,
                            Manager = item.Manager,
                            AlternatePhone = item.AlternatePhone,
                            DepartmentFloor = item.DepartmentFloor,
                            Company = item.Company,
                            DistinguishedName = item.DistinguishedName,
                            LastUpdated = item.LastUpdated,
                            MHCNumber = item.MHCNumber,
                            MemberOf = item.MemberOf
                        });
                    }
                    break;
                case "userid partial":
                    var qryUserIdPartial = (from x in db.Colleagues where (x.Username.ToLower().Contains(searchExpression) && x.Active == true) select x);
                    foreach (var item in qryUserIdPartial)
                    {
                        results.Add(new DBADUser
                        {
                            Guid = item.GUID,
                            Username = item.Username,
                            DisplayName = item.DisplayName,
                            JobTitle = item.JobTitle,
                            FirstName = item.FirstName,
                            MiddleName = item.MiddleName,
                            LastName = item.LastName,
                            LastFirstName = item.LastFirstName,
                            Phone = item.Phone,
                            Email = item.EMail,
                            Department = item.Department,
                            Manager = item.Manager,
                            AlternatePhone = item.AlternatePhone,
                            DepartmentFloor = item.DepartmentFloor,
                            Company = item.Company,
                            DistinguishedName = item.DistinguishedName,
                            LastUpdated = item.LastUpdated,
                            MHCNumber = item.MHCNumber,
                            MemberOf = item.MemberOf
                        });
                    }
                    break;
                case "guid":
                    Guid compare = new Guid(searchExpression);
                    var qryGUID = (from x in db.Colleagues where (x.GUID == compare && x.Active == true) select x);
                    foreach (var item in qryGUID)
                    {
                        results.Add(new DBADUser
                        {
                            Guid = item.GUID,
                            Username = item.Username,
                            DisplayName = item.DisplayName,
                            JobTitle = item.JobTitle,
                            FirstName = item.FirstName,
                            MiddleName = item.MiddleName,
                            LastName = item.LastName,
                            LastFirstName = item.LastFirstName,
                            Phone = item.Phone,
                            Email = item.EMail,
                            Department = item.Department,
                            Manager = item.Manager,
                            AlternatePhone = item.AlternatePhone,
                            DepartmentFloor = item.DepartmentFloor,
                            Company = item.Company,
                            DistinguishedName = item.DistinguishedName,
                            LastUpdated = item.LastUpdated,
                            MHCNumber = item.MHCNumber,
                            MemberOf = item.MemberOf
                        });
                    }
                    break;
                case "job title":
                    var qryJobTitle = (from x in db.Colleagues where (x.JobTitle.ToLower().Contains(searchExpression) && x.Active == true) select x);
                    foreach (var item in qryJobTitle)
                    {
                        results.Add(new DBADUser
                        {
                            Guid = item.GUID,
                            Username = item.Username,
                            DisplayName = item.DisplayName,
                            JobTitle = item.JobTitle,
                            FirstName = item.FirstName,
                            MiddleName = item.MiddleName,
                            LastName = item.LastName,
                            LastFirstName = item.LastFirstName,
                            Phone = item.Phone,
                            Email = item.EMail,
                            Department = item.Department,
                            Manager = item.Manager,
                            AlternatePhone = item.AlternatePhone,
                            DepartmentFloor = item.DepartmentFloor,
                            Company = item.Company,
                            DistinguishedName = item.DistinguishedName,
                            LastUpdated = item.LastUpdated,
                            MHCNumber = item.MHCNumber,
                            MemberOf = item.MemberOf
                        });
                    }
                    break;
                case "department":
                    var qryDepartment = (from x in db.Colleagues where (x.Department.ToLower().Contains(searchExpression) && x.Active == true) select x);
                    foreach (var item in qryDepartment)
	                {
		                results.Add(new DBADUser
                            {
                                Guid = item.GUID,
                                Username = item.Username,
                                DisplayName = item.DisplayName,
                                JobTitle = item.JobTitle,
                                FirstName = item.FirstName,
                                MiddleName = item.MiddleName,
                                LastName = item.LastName,
                                LastFirstName = item.LastFirstName,
                                Phone = item.Phone,
                                Email = item.EMail,
                                Department = item.Department,
                                Manager = item.Manager,
                                AlternatePhone = item.AlternatePhone,
                                DepartmentFloor = item.DepartmentFloor,
                                Company = item.Company,
                                DistinguishedName = item.DistinguishedName,
                                LastUpdated = item.LastUpdated,
                                MHCNumber = item.MHCNumber,
                                MemberOf = item.MemberOf
                            });
	                }
                    break;
                case "phone":
                    var qryPhone = (from x in db.Colleagues where (x.Phone.ToLower().Contains(searchExpression) && x.Active == true) select x);
                    foreach (var item in qryPhone)
                    {
                        results.Add(new DBADUser
                        {
                            Guid = item.GUID,
                            Username = item.Username,
                            DisplayName = item.DisplayName,
                            JobTitle = item.JobTitle,
                            FirstName = item.FirstName,
                            MiddleName = item.MiddleName,
                            LastName = item.LastName,
                            LastFirstName = item.LastFirstName,
                            Phone = item.Phone,
                            Email = item.EMail,
                            Department = item.Department,
                            Manager = item.Manager,
                            AlternatePhone = item.AlternatePhone,
                            DepartmentFloor = item.DepartmentFloor,
                            Company = item.Company,
                            DistinguishedName = item.DistinguishedName,
                            LastUpdated = item.LastUpdated,
                            MHCNumber = item.MHCNumber,
                            MemberOf = item.MemberOf
                        });
                    }
                    break;
                default:
                    
                    break;
	        }
                //(from x in db.Colleagues where x.DisplayName.Contains(searchExpression) select x);
            return results;
        }
    }
}