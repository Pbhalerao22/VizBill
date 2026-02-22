using System;
using System.Collections.Generic;

namespace VizBill.MasterDbContext;

public partial class TblInnoRoleMaster
{
    public long RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public DateTime CreatedOn { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public long? ModifiedBy { get; set; }

    public virtual ICollection<TblInnoUserRoleMapping> TblInnoUserRoleMappings { get; set; } = new List<TblInnoUserRoleMapping>();
}
