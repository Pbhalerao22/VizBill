using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace VizBill.MasterDbContext;

public partial class PostgresContext : DbContext
{
    public PostgresContext()
    {
    }

    public PostgresContext(DbContextOptions<PostgresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblInnoBillItemMapping> TblInnoBillItemMappings { get; set; }

    public virtual DbSet<TblInnoBillMaster> TblInnoBillMasters { get; set; }

    public virtual DbSet<TblInnoCategoryMaster> TblInnoCategoryMasters { get; set; }

    public virtual DbSet<TblInnoItemMaster> TblInnoItemMasters { get; set; }

    public virtual DbSet<TblInnoPaymentModeMaster> TblInnoPaymentModeMasters { get; set; }

    public virtual DbSet<TblInnoRoleMaster> TblInnoRoleMasters { get; set; }

    public virtual DbSet<TblInnoShopMaster> TblInnoShopMasters { get; set; }

    public virtual DbSet<TblInnoShopSubscriptionMapping> TblInnoShopSubscriptionMappings { get; set; }

    public virtual DbSet<TblInnoSubscriptionPlanMaster> TblInnoSubscriptionPlanMasters { get; set; }

    public virtual DbSet<TblInnoUserMaster> TblInnoUserMasters { get; set; }

    public virtual DbSet<TblInnoUserRoleMapping> TblInnoUserRoleMappings { get; set; }

    public virtual DbSet<TblInnoUserShopMapping> TblInnoUserShopMappings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=masterdb.cnggws2s8y5a.ap-southeast-1.rds.amazonaws.com;Port=5432;Database=postgres;Username=postgres;Password=MasterDb12345;SSL Mode=Require;Trust Server Certificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblInnoBillItemMapping>(entity =>
        {
            entity.HasKey(e => e.BillItemId).HasName("tbl_inno_bill_item_mapping_pkey");

            entity.ToTable("tbl_inno_bill_item_mapping");

            entity.HasIndex(e => e.BillId, "idx_billitem_bill");

            entity.HasIndex(e => e.ItemId, "idx_billitem_item");

            entity.Property(e => e.BillItemId).HasColumnName("bill_item_id");
            entity.Property(e => e.BillId).HasColumnName("bill_id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.ItemName)
                .HasMaxLength(200)
                .HasColumnName("item_name");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified_on");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Total)
                .HasPrecision(12, 2)
                .HasColumnName("total");

            entity.HasOne(d => d.Bill).WithMany(p => p.TblInnoBillItemMappings)
                .HasForeignKey(d => d.BillId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tbl_inno_bill_item_mapping_bill_id_fkey");

            entity.HasOne(d => d.Item).WithMany(p => p.TblInnoBillItemMappings)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tbl_inno_bill_item_mapping_item_id_fkey");
        });

        modelBuilder.Entity<TblInnoBillMaster>(entity =>
        {
            entity.HasKey(e => e.BillId).HasName("tbl_inno_bill_master_pkey");

            entity.ToTable("tbl_inno_bill_master");

            entity.HasIndex(e => e.BillNumber, "idx_bill_number");

            entity.HasIndex(e => e.ShopId, "idx_bill_shop");

            entity.Property(e => e.BillId).HasColumnName("bill_id");
            entity.Property(e => e.BillDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("bill_date");
            entity.Property(e => e.BillNumber)
                .HasMaxLength(50)
                .HasColumnName("bill_number");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.CustomerMobile)
                .HasMaxLength(20)
                .HasColumnName("customer_mobile");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified_on");
            entity.Property(e => e.Notes)
                .HasMaxLength(300)
                .HasColumnName("notes");
            entity.Property(e => e.PaymentModeId).HasColumnName("payment_mode_id");
            entity.Property(e => e.ShopId).HasColumnName("shop_id");
            entity.Property(e => e.TotalAmount)
                .HasPrecision(12, 2)
                .HasColumnName("total_amount");

            entity.HasOne(d => d.PaymentMode).WithMany(p => p.TblInnoBillMasters)
                .HasForeignKey(d => d.PaymentModeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tbl_inno_bill_master_payment_mode_id_fkey");

            entity.HasOne(d => d.Shop).WithMany(p => p.TblInnoBillMasters)
                .HasForeignKey(d => d.ShopId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tbl_inno_bill_master_shop_id_fkey");
        });

        modelBuilder.Entity<TblInnoCategoryMaster>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("tbl_inno_category_master_pkey");

            entity.ToTable("tbl_inno_category_master");

            entity.HasIndex(e => e.CategoryName, "idx_category_name");

            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(100)
                .HasColumnName("category_name");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified_on");
        });

        modelBuilder.Entity<TblInnoItemMaster>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("tbl_inno_item_master_pkey");

            entity.ToTable("tbl_inno_item_master");

            entity.HasIndex(e => e.CategoryId, "idx_item_category");

            entity.HasIndex(e => e.ShopId, "idx_item_shop");

            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.ItemName)
                .HasMaxLength(200)
                .HasColumnName("item_name");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified_on");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.ShopId).HasColumnName("shop_id");

            entity.HasOne(d => d.Category).WithMany(p => p.TblInnoItemMasters)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("tbl_inno_item_master_category_id_fkey");

            entity.HasOne(d => d.Shop).WithMany(p => p.TblInnoItemMasters)
                .HasForeignKey(d => d.ShopId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tbl_inno_item_master_shop_id_fkey");
        });

        modelBuilder.Entity<TblInnoPaymentModeMaster>(entity =>
        {
            entity.HasKey(e => e.PaymentModeId).HasName("tbl_inno_payment_mode_master_pkey");

            entity.ToTable("tbl_inno_payment_mode_master");

            entity.HasIndex(e => e.ModeName, "tbl_inno_payment_mode_master_mode_name_key").IsUnique();

            entity.Property(e => e.PaymentModeId).HasColumnName("payment_mode_id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.ModeName)
                .HasMaxLength(50)
                .HasColumnName("mode_name");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified_on");
        });

        modelBuilder.Entity<TblInnoRoleMaster>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("tbl_inno_role_master_pkey");

            entity.ToTable("tbl_inno_role_master");

            entity.HasIndex(e => e.RoleName, "idx_role_name");

            entity.HasIndex(e => e.RoleName, "tbl_inno_role_master_role_name_key").IsUnique();

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified_on");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<TblInnoShopMaster>(entity =>
        {
            entity.HasKey(e => e.ShopId).HasName("tbl_inno_shop_master_pkey");

            entity.ToTable("tbl_inno_shop_master");

            entity.HasIndex(e => e.ShopId, "idx_active_shops").HasFilter("(is_deleted = false)");

            entity.HasIndex(e => e.ShopName, "idx_shop_name");

            entity.HasIndex(e => e.OwnerUserId, "idx_shop_owner");

            entity.Property(e => e.ShopId).HasColumnName("shop_id");
            entity.Property(e => e.AdvertisementMsg)
                .HasMaxLength(500)
                .HasColumnName("advertisement_msg");
            entity.Property(e => e.CompanyLogo).HasColumnName("company_logo");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified_on");
            entity.Property(e => e.OwnerUserId).HasColumnName("owner_user_id");
            entity.Property(e => e.ShopCategory)
                .HasMaxLength(100)
                .HasColumnName("shop_category");
            entity.Property(e => e.ShopName)
                .HasMaxLength(200)
                .HasColumnName("shop_name");
            entity.Property(e => e.WhatsappNumber)
                .HasMaxLength(20)
                .HasColumnName("whatsapp_number");

            entity.HasOne(d => d.OwnerUser).WithMany(p => p.TblInnoShopMasters)
                .HasForeignKey(d => d.OwnerUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_shop_owner");
        });

        modelBuilder.Entity<TblInnoShopSubscriptionMapping>(entity =>
        {
            entity.HasKey(e => e.SubscriptionId).HasName("tbl_inno_shop_subscription_mapping_pkey");

            entity.ToTable("tbl_inno_shop_subscription_mapping");

            entity.HasIndex(e => e.PlanId, "idx_shopsubscription_plan");

            entity.HasIndex(e => e.ShopId, "idx_shopsubscription_shop");

            entity.Property(e => e.SubscriptionId).HasColumnName("subscription_id");
            entity.Property(e => e.ApprovedBy).HasColumnName("approved_by");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.EndDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("end_date");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.IsTrial)
                .HasDefaultValue(false)
                .HasColumnName("is_trial");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified_on");
            entity.Property(e => e.PlanId).HasColumnName("plan_id");
            entity.Property(e => e.ShopId).HasColumnName("shop_id");
            entity.Property(e => e.StartDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("start_date");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");

            entity.HasOne(d => d.ApprovedByNavigation).WithMany(p => p.TblInnoShopSubscriptionMappings)
                .HasForeignKey(d => d.ApprovedBy)
                .HasConstraintName("tbl_inno_shop_subscription_mapping_approved_by_fkey");

            entity.HasOne(d => d.Plan).WithMany(p => p.TblInnoShopSubscriptionMappings)
                .HasForeignKey(d => d.PlanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tbl_inno_shop_subscription_mapping_plan_id_fkey");

            entity.HasOne(d => d.Shop).WithMany(p => p.TblInnoShopSubscriptionMappings)
                .HasForeignKey(d => d.ShopId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tbl_inno_shop_subscription_mapping_shop_id_fkey");
        });

        modelBuilder.Entity<TblInnoSubscriptionPlanMaster>(entity =>
        {
            entity.HasKey(e => e.PlanId).HasName("tbl_inno_subscription_plan_master_pkey");

            entity.ToTable("tbl_inno_subscription_plan_master");

            entity.HasIndex(e => e.PlanName, "idx_plan_name");

            entity.Property(e => e.PlanId).HasColumnName("plan_id");
            entity.Property(e => e.BillingCycle)
                .HasMaxLength(50)
                .HasColumnName("billing_cycle");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.MaxBillsPerDay).HasColumnName("max_bills_per_day");
            entity.Property(e => e.MaxItems).HasColumnName("max_items");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified_on");
            entity.Property(e => e.MultiShopEnabled)
                .HasDefaultValue(true)
                .HasColumnName("multi_shop_enabled");
            entity.Property(e => e.PlanName)
                .HasMaxLength(100)
                .HasColumnName("plan_name");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.WhatsappEnabled)
                .HasDefaultValue(true)
                .HasColumnName("whatsapp_enabled");
        });

        modelBuilder.Entity<TblInnoUserMaster>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("tbl_inno_user_master_pkey");

            entity.ToTable("tbl_inno_user_master");

            entity.HasIndex(e => e.Email, "idx_user_email");

            entity.HasIndex(e => e.Email, "tbl_inno_user_master_email_key").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .HasColumnName("email");
            entity.Property(e => e.GoogleId)
                .HasMaxLength(255)
                .HasColumnName("google_id");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified_on");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .HasColumnName("name");
            entity.Property(e => e.ProfileImage).HasColumnName("profile_image");
        });

        modelBuilder.Entity<TblInnoUserRoleMapping>(entity =>
        {
            entity.HasKey(e => e.UserRoleId).HasName("tbl_inno_user_role_mapping_pkey");

            entity.ToTable("tbl_inno_user_role_mapping");

            entity.HasIndex(e => e.RoleId, "idx_userrole_role");

            entity.HasIndex(e => e.UserId, "idx_userrole_user");

            entity.HasIndex(e => new { e.UserId, e.RoleId }, "uq_user_role").IsUnique();

            entity.Property(e => e.UserRoleId).HasColumnName("user_role_id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified_on");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Role).WithMany(p => p.TblInnoUserRoleMappings)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_userrole_role");

            entity.HasOne(d => d.User).WithMany(p => p.TblInnoUserRoleMappings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_userrole_user");
        });

        modelBuilder.Entity<TblInnoUserShopMapping>(entity =>
        {
            entity.HasKey(e => e.UserShopId).HasName("tbl_inno_user_shop_mapping_pkey");

            entity.ToTable("tbl_inno_user_shop_mapping");

            entity.HasIndex(e => e.ShopId, "idx_usershop_shop");

            entity.HasIndex(e => e.UserId, "idx_usershop_user");

            entity.HasIndex(e => new { e.UserId, e.ShopId }, "uq_user_shop").IsUnique();

            entity.Property(e => e.UserShopId).HasColumnName("user_shop_id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified_on");
            entity.Property(e => e.ShopId).HasColumnName("shop_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Shop).WithMany(p => p.TblInnoUserShopMappings)
                .HasForeignKey(d => d.ShopId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_usershop_shop");

            entity.HasOne(d => d.User).WithMany(p => p.TblInnoUserShopMappings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_usershop_user");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
