//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RegistrationGatekeeperAdmin.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class SystemAccess
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SystemAccess()
        {
            this.SystemIdentitys = new HashSet<SystemIdentity>();
        }
    
        public int SystemAccessId { get; set; }
        public string SystemAccessName { get; set; }
        public bool Active { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SystemIdentity> SystemIdentitys { get; set; }
    }
}