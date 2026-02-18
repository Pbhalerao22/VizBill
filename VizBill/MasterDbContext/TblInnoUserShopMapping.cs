using System;
using System.Collections.Generic;

namespace VizBill.MasterDbContext;

public partial class TblInnoUserShopMapping
{
    public long UserShopId { get; set; }

    public long UserId { get; set; }

    public long ShopId { get; set; }

    public bool? IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedOn { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public long? ModifiedBy { get; set; }

    public virtual TblInnoShopMaster Shop { get; set; } = null!;

    public virtual TblInnoUserMaster User { get; set; } = null!;
}
