using System;
using System.Collections.Generic;

namespace VizBill.MasterDbContext;

public partial class TblInnoBillMaster
{
    public long BillId { get; set; }

    public long ShopId { get; set; }

    public string BillNumber { get; set; } = null!;

    public string? CustomerMobile { get; set; }

    public decimal TotalAmount { get; set; }

    public long PaymentModeId { get; set; }

    public string? Notes { get; set; }

    public DateTime? BillDate { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedOn { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public long? ModifiedBy { get; set; }

    public virtual TblInnoPaymentModeMaster PaymentMode { get; set; } = null!;

    public virtual TblInnoShopMaster Shop { get; set; } = null!;

    public virtual ICollection<TblInnoBillItemMapping> TblInnoBillItemMappings { get; set; } = new List<TblInnoBillItemMapping>();
}
