using System;
using System.Collections.Generic;

namespace VizBill.MasterDbContext;

public partial class TblInnoShopSubscriptionMapping
{
    public long SubscriptionId { get; set; }

    public long ShopId { get; set; }

    public long PlanId { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool? IsTrial { get; set; }

    public long? ApprovedBy { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedOn { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public long? ModifiedBy { get; set; }

    public virtual TblInnoUserMaster? ApprovedByNavigation { get; set; }

    public virtual TblInnoSubscriptionPlanMaster Plan { get; set; } = null!;

    public virtual TblInnoShopMaster Shop { get; set; } = null!;
}
