using System;
using System.Collections.Generic;

namespace VizBill.MasterDbContext;

public partial class TblInnoUserMaster
{
    public long UserId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string GoogleId { get; set; } = null!;

    public byte[]? ProfileImage { get; set; }

    public bool? IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedOn { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public long? ModifiedBy { get; set; }

    public string? PasswordHash { get; set; }

    public virtual ICollection<TblInnoShopMaster> TblInnoShopMasters { get; set; } = new List<TblInnoShopMaster>();

    public virtual ICollection<TblInnoShopSubscriptionMapping> TblInnoShopSubscriptionMappings { get; set; } = new List<TblInnoShopSubscriptionMapping>();

    public virtual ICollection<TblInnoUserRoleMapping> TblInnoUserRoleMappings { get; set; } = new List<TblInnoUserRoleMapping>();

    public virtual ICollection<TblInnoUserShopMapping> TblInnoUserShopMappings { get; set; } = new List<TblInnoUserShopMapping>();
}
