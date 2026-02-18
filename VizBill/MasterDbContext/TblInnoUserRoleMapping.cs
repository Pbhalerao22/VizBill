using System;
using System.Collections.Generic;

namespace VizBill.MasterDbContext;

public partial class TblInnoUserRoleMapping
{
    public long UserRoleId { get; set; }

    public long UserId { get; set; }

    public long RoleId { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedOn { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public long? ModifiedBy { get; set; }

    public virtual TblInnoRoleMaster Role { get; set; } = null!;

    public virtual TblInnoUserMaster User { get; set; } = null!;
}
