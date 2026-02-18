using System;
using System.Collections.Generic;

namespace VizBill.MasterDbContext;

public partial class TblInnoCategoryMaster
{
    public long CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public DateTime CreatedOn { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public long? ModifiedBy { get; set; }

    public virtual ICollection<TblInnoItemMaster> TblInnoItemMasters { get; set; } = new List<TblInnoItemMaster>();
}
