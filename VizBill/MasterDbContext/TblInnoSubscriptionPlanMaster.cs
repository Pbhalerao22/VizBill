using System;
using System.Collections.Generic;

namespace VizBill.MasterDbContext;

public partial class TblInnoSubscriptionPlanMaster
{
    public long PlanId { get; set; }

    public string PlanName { get; set; } = null!;

    public decimal Price { get; set; }

    public string BillingCycle { get; set; } = null!;

    public int? MaxBillsPerDay { get; set; }

    public int? MaxItems { get; set; }

    public bool? WhatsappEnabled { get; set; }

    public bool? MultiShopEnabled { get; set; }

    public bool? IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedOn { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public long? ModifiedBy { get; set; }

    public virtual ICollection<TblInnoShopSubscriptionMapping> TblInnoShopSubscriptionMappings { get; set; } = new List<TblInnoShopSubscriptionMapping>();
}
