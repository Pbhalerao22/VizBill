using System;
using System.Collections.Generic;

namespace VizBill.MasterDbContext;

public partial class TblInnoBillItemMapping
{
    public long BillItemId { get; set; }

    public long BillId { get; set; }

    public long ItemId { get; set; }

    public string ItemName { get; set; } = null!;

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public decimal Total { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedOn { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public long? ModifiedBy { get; set; }

    public virtual TblInnoBillMaster Bill { get; set; } = null!;

    public virtual TblInnoItemMaster Item { get; set; } = null!;
}
