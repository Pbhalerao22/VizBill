using System;
using System.Collections.Generic;

namespace VizBill.MasterDbContext;

public partial class TblInnoShopMaster
{
    public long ShopId { get; set; }

    public long OwnerUserId { get; set; }

    public string ShopName { get; set; } = null!;

    public string? ShopCategory { get; set; }

    public string? WhatsappNumber { get; set; }

    public string? AdvertisementMsg { get; set; }

    public byte[]? CompanyLogo { get; set; }

    public bool? IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedOn { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public long? ModifiedBy { get; set; }

    public virtual TblInnoUserMaster OwnerUser { get; set; } = null!;

    public virtual ICollection<TblInnoBillMaster> TblInnoBillMasters { get; set; } = new List<TblInnoBillMaster>();

    public virtual ICollection<TblInnoItemMaster> TblInnoItemMasters { get; set; } = new List<TblInnoItemMaster>();

    public virtual ICollection<TblInnoShopSubscriptionMapping> TblInnoShopSubscriptionMappings { get; set; } = new List<TblInnoShopSubscriptionMapping>();

    public virtual ICollection<TblInnoUserShopMapping> TblInnoUserShopMappings { get; set; } = new List<TblInnoUserShopMapping>();
}
