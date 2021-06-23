﻿// <auto-generated />
using System;
using AduabaNeptune.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AduabaNeptune.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210623102427_Data type fix")]
    partial class Datatypefix
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AduabaNeptune.Data.Entities.BillingAddress", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AlternatePhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactPersonsName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("Landmark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("BillingAddresses");
                });

            modelBuilder.Entity("AduabaNeptune.Data.Entities.Card", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("CCV")
                        .HasMaxLength(3)
                        .HasColumnType("int");

                    b.Property<string>("CardHolderName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CardNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("AduabaNeptune.Data.Entities.Cart", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("AduabaNeptune.Data.Entities.CartItem", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CartId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("OrderId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProductId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CartId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("AduabaNeptune.Data.Entities.Category", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("AduabaNeptune.Data.Entities.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AvatarUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("AduabaNeptune.Data.Entities.Order", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BillingAddressId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OrderNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrderStatusId")
                        .HasColumnType("int");

                    b.Property<string>("PaymentType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShippingAddressId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("BillingAddressId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("OrderStatusId");

                    b.HasIndex("ShippingAddressId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("AduabaNeptune.Data.Entities.OrderStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("OrderStatuses");
                });

            modelBuilder.Entity("AduabaNeptune.Data.Entities.PaymentHistory", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("OrderId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("OrderNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PaymentStatusId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("OrderId");

                    b.HasIndex("PaymentStatusId");

                    b.ToTable("PaymentHistories");
                });

            modelBuilder.Entity("AduabaNeptune.Data.Entities.PaymentStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PaymentStatuses");
                });

            modelBuilder.Entity("AduabaNeptune.Data.Entities.Product", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CategoryId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("SubCategory")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SubCategoryId")
                        .HasColumnType("int");

                    b.Property<int>("VendorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("SubCategoryId");

                    b.HasIndex("VendorId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("AduabaNeptune.Data.Entities.Promotion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PromotionTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Promotions");
                });

            modelBuilder.Entity("AduabaNeptune.Data.Entities.ShippingAddress", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AlternatePhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactPersonsName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("Landmark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("ShippingAddresses");
                });

            modelBuilder.Entity("AduabaNeptune.Data.Entities.SubCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("SubCategories");
                });

            modelBuilder.Entity("AduabaNeptune.Data.Entities.Vendor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateJoined")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShopName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Vendors");
                });

            modelBuilder.Entity("AduabaNeptune.Data.Entities.WishList", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("WishLists");
                });

            modelBuilder.Entity("AduabaNeptune.Data.Entities.WishListItem", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProductId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("WishListId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("WishListId");

                    b.ToTable("WishListItems");
                });

            modelBuilder.Entity("AduabaNeptune.Data.Entities.BillingAddress", b =>
                {
                    b.HasOne("AduabaNeptune.Data.Entities.Customer", "Customer")
                        .WithMany("BillingAddresses")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("AduabaNeptune.Data.Entities.Card", b =>
                {
                    b.HasOne("AduabaNeptune.Data.Entities.Customer", "Customer")
                        .WithMany("Cards")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("AduabaNeptune.Data.Entities.Cart", b =>
                {
                    b.HasOne("AduabaNeptune.Data.Entities.Customer", "Customer")
                        .WithMany("Cart")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("AduabaNeptune.Data.Entities.CartItem", b =>
                {
                    b.HasOne("AduabaNeptune.Data.Entities.Cart", "Cart")
                        .WithMany("CartItems")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AduabaNeptune.Data.Entities.Customer", null)
                        .WithMany("CartItems")
                        .HasForeignKey("CustomerId");

                    b.HasOne("AduabaNeptune.Data.Entities.Order", "Order")
                        .WithMany("CartItems")
                        .HasForeignKey("OrderId");

                    b.HasOne("AduabaNeptune.Data.Entities.Product", "Product")
                        .WithMany("CartItems")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cart");

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("AduabaNeptune.Data.Entities.Order", b =>
                {
                    b.HasOne("AduabaNeptune.Data.Entities.BillingAddress", "BillingAddress")
                        .WithMany("Orders")
                        .HasForeignKey("BillingAddressId");

                    b.HasOne("AduabaNeptune.Data.Entities.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AduabaNeptune.Data.Entities.OrderStatus", "OrderStatus")
                        .WithMany()
                        .HasForeignKey("OrderStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AduabaNeptune.Data.Entities.ShippingAddress", "ShippingAddress")
                        .WithMany("Orders")
                        .HasForeignKey("ShippingAddressId");

                    b.Navigation("BillingAddress");

                    b.Navigation("Customer");

                    b.Navigation("OrderStatus");

                    b.Navigation("ShippingAddress");
                });

            modelBuilder.Entity("AduabaNeptune.Data.Entities.PaymentHistory", b =>
                {
                    b.HasOne("AduabaNeptune.Data.Entities.Customer", null)
                        .WithMany("PaymentHistories")
                        .HasForeignKey("CustomerId");

                    b.HasOne("AduabaNeptune.Data.Entities.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId");

                    b.HasOne("AduabaNeptune.Data.Entities.PaymentStatus", "PaymentStatus")
                        .WithMany()
                        .HasForeignKey("PaymentStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("PaymentStatus");
                });

            modelBuilder.Entity("AduabaNeptune.Data.Entities.Product", b =>
                {
                    b.HasOne("AduabaNeptune.Data.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AduabaNeptune.Data.Entities.SubCategory", null)
                        .WithMany("Product")
                        .HasForeignKey("SubCategoryId");

                    b.HasOne("AduabaNeptune.Data.Entities.Vendor", "Vendor")
                        .WithMany("Products")
                        .HasForeignKey("VendorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Vendor");
                });

            modelBuilder.Entity("AduabaNeptune.Data.Entities.ShippingAddress", b =>
                {
                    b.HasOne("AduabaNeptune.Data.Entities.Customer", "Customer")
                        .WithMany("ShippingAddresses")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("AduabaNeptune.Data.Entities.SubCategory", b =>
                {
                    b.HasOne("AduabaNeptune.Data.Entities.Category", "Category")
                        .WithMany("SubCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("AduabaNeptune.Data.Entities.WishList", b =>
                {
                    b.HasOne("AduabaNeptune.Data.Entities.Customer", "Customer")
                        .WithMany("Wishlist")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("AduabaNeptune.Data.Entities.WishListItem", b =>
                {
                    b.HasOne("AduabaNeptune.Data.Entities.Product", "Product")
                        .WithMany("WishListItems")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AduabaNeptune.Data.Entities.WishList", "WishList")
                        .WithMany("WishListItems")
                        .HasForeignKey("WishListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("WishList");
                });

            modelBuilder.Entity("AduabaNeptune.Data.Entities.BillingAddress", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("AduabaNeptune.Data.Entities.Cart", b =>
                {
                    b.Navigation("CartItems");
                });

            modelBuilder.Entity("AduabaNeptune.Data.Entities.Category", b =>
                {
                    b.Navigation("SubCategories");
                });

            modelBuilder.Entity("AduabaNeptune.Data.Entities.Customer", b =>
                {
                    b.Navigation("BillingAddresses");

                    b.Navigation("Cards");

                    b.Navigation("Cart");

                    b.Navigation("CartItems");

                    b.Navigation("Orders");

                    b.Navigation("PaymentHistories");

                    b.Navigation("ShippingAddresses");

                    b.Navigation("Wishlist");
                });

            modelBuilder.Entity("AduabaNeptune.Data.Entities.Order", b =>
                {
                    b.Navigation("CartItems");
                });

            modelBuilder.Entity("AduabaNeptune.Data.Entities.Product", b =>
                {
                    b.Navigation("CartItems");

                    b.Navigation("WishListItems");
                });

            modelBuilder.Entity("AduabaNeptune.Data.Entities.ShippingAddress", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("AduabaNeptune.Data.Entities.SubCategory", b =>
                {
                    b.Navigation("Product");
                });

            modelBuilder.Entity("AduabaNeptune.Data.Entities.Vendor", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("AduabaNeptune.Data.Entities.WishList", b =>
                {
                    b.Navigation("WishListItems");
                });
#pragma warning restore 612, 618
        }
    }
}
