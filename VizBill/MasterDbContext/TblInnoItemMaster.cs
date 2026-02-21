using System;
using System.Collections.Generic;

namespace VizBill.MasterDbContext;

public partial class TblInnoItemMaster
{
    public long ItemId { get; set; }

    public long ShopId { get; set; }

    public long? CategoryId { get; set; }

    public string ItemName { get; set; } = null!;

    public decimal Price { get; set; }

    public bool? IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedOn { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public long? ModifiedBy { get; set; }

    public virtual TblInnoCategoryMaster? Category { get; set; }

    public virtual TblInnoShopMaster? Shop { get; set; } = null!;

    public virtual ICollection<TblInnoBillItemMapping>? TblInnoBillItemMappings { get; set; } = new List<TblInnoBillItemMapping>();
}
