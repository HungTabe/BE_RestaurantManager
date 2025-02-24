using BE_RestaurantManagement.Models;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Models;

namespace BE_RestaurantManagement.Data
{

        public class RestaurantDbContext : DbContext
        {
            public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : base(options) { }

            // Định nghĩa DbSet cho từng entity (tương ứng với các bảng trong database)
            public DbSet<User> Users { get; set; }
            public DbSet<Role> Roles { get; set; }
            public DbSet<Customer> Customers { get; set; }
            public DbSet<Staff> Staffs { get; set; }
            public DbSet<Admin> Admins { get; set; }
            public DbSet<MenuItem> MenuItems { get; set; }
            public DbSet<Order> Orders { get; set; }
            public DbSet<OrderItem> OrderItems { get; set; }
            public DbSet<Feedback> Feedbacks { get; set; }
            public DbSet<Payment> Payments { get; set; }
            public DbSet<Table> Tables { get; set; }
            public DbSet<Shift> Shifts { get; set; }
            public DbSet<RevenueReport> RevenueReports { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                // Cấu hình quan hệ bảng

                // User - Role (1-N)
                modelBuilder.Entity<User>()
                    .HasOne(u => u.Role)
                    .WithMany(r => r.Users)
                    .HasForeignKey(u => u.RoleId);

                // Customer - Order (1-N)
                modelBuilder.Entity<Customer>()
                    .HasMany(c => c.Orders)
                    .WithOne(o => o.Customer)
                    .HasForeignKey(o => o.CustomerId);

                // Staff - Order (1-N) (Nhân viên xử lý đơn hàng)
                modelBuilder.Entity<Staff>()
                    .HasMany(s => s.ProcessedOrders)
                    .WithOne(o => o.Staff)
                    .HasForeignKey(o => o.StaffId);

                // Staff - Shift (1-N) (Lịch làm việc của nhân viên)
                modelBuilder.Entity<Staff>()
                    .HasMany(s => s.Shifts)
                    .WithOne(sh => sh.Staff)
                    .HasForeignKey(sh => sh.StaffId);

                modelBuilder.Entity<OrderItem>()
                    .HasOne(oi => oi.Order)
                    .WithMany(o => o.OrderItems)
                    .HasForeignKey(oi => oi.OrderId);

                modelBuilder.Entity<OrderItem>()
                    .HasOne(oi => oi.MenuItem)
                    .WithMany(m => m.OrderItems)
                    .HasForeignKey(oi => oi.MenuItemId);

                // Bảng trung gian Discount & Promotion (nếu có)
                // modelBuilder.Entity<Discount>()
                //     .HasMany(d => d.Orders)
                //     .WithOne(o => o.Discount)
                //     .HasForeignKey(o => o.DiscountId);

                base.OnModelCreating(modelBuilder);
            }
        }
    }
