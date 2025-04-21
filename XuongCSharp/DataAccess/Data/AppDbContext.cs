using XuongCSharp.DataAccess.Entities;

namespace XuongCSharp.DataAccess.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public virtual DbSet<Department> Departments { get; set; }

        public virtual DbSet<DepartmentFacility> DepartmentFacilities { get; set; }

        public virtual DbSet<Facility> Facilities { get; set; }

        public virtual DbSet<Major> Majors { get; set; }

        public virtual DbSet<MajorFacility> MajorFacilities { get; set; }

        public virtual DbSet<Staff> Staffs { get; set; }

        public virtual DbSet<StaffMajorFacility> StaffMajorFacilities { get; set; }
        public DbSet<ImportLog> ImportLogs { get; set; }
        public DbSet<ImportLogDetail> ImportLogDetails { get; set; }
        public DbSet<StaffStatusLog> StaffStatusLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.GenerateSeed();
            modelBuilder.Entity<Staff>().ToTable("staff", schema: "dbo");
            modelBuilder.Entity<Facility>().ToTable("facility", schema: "dbo");
            modelBuilder.Entity<Department>().ToTable("department", schema: "dbo");
            modelBuilder.Entity<DepartmentFacility>().ToTable("department_facility", schema: "dbo");
            modelBuilder.Entity<Major>().ToTable("major", schema: "dbo");
            modelBuilder.Entity<MajorFacility>().ToTable("major_facility", schema: "dbo");
            modelBuilder.Entity<StaffMajorFacility>().ToTable("staff_major_facility", schema: "dbo");
            modelBuilder.Entity<ImportLog>().ToTable("import_log", schema: "dbo");
            modelBuilder.Entity<ImportLogDetail>().ToTable("import_log_detail", schema: "dbo");
            modelBuilder.Entity<StaffStatusLog>().ToTable("staff_status_log", schema: "dbo");

            // Configure foreign keys (as discussed previously)
            modelBuilder.Entity<DepartmentFacility>()
                .HasOne(df => df.Department)
                .WithMany(d => d.DepartmentFacilities)
                .HasForeignKey(df => df.IdDepartment)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<DepartmentFacility>()
                .HasOne(df => df.Facility)
                .WithMany(f => f.DepartmentFacilities)
                .HasForeignKey(df => df.IdFacility)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<DepartmentFacility>()
                .HasOne(df => df.Staff)
                .WithMany(s => s.DepartmentFacilities)
                .HasForeignKey(df => df.IdStaff)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<MajorFacility>()
                .HasOne(mf => mf.DepartmentFacility)
                .WithMany(df => df.MajorFacilities)
                .HasForeignKey(mf => mf.IdDepartmentFacility)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MajorFacility>()
                .HasOne(mf => mf.Major)
                .WithMany(m => m.MajorFacilities)
                .HasForeignKey(mf => mf.IdMajor)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StaffMajorFacility>()
                .HasOne(smf => smf.MajorFacility)
                .WithMany(mf => mf.StaffMajorFacilities)
                .HasForeignKey(smf => smf.IdMajorFacility)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StaffMajorFacility>()
                .HasOne(smf => smf.Staff)
                .WithMany(s => s.StaffMajorFacilities)
                .HasForeignKey(smf => smf.IdStaff)
                .OnDelete(DeleteBehavior.Cascade);

                modelBuilder.Entity<StaffStatusLog>()
                .HasOne(ssl => ssl.Staff)
                .WithMany()
                .HasForeignKey(ssl => ssl.StaffId);

            modelBuilder.Entity<ImportLogDetail>()
                .HasOne(ild => ild.ImportLog)
                .WithMany(il => il.ImportLogDetails)
                .HasForeignKey(ild => ild.ImportLogId);
        }
        //public virtual DbSet<ImportHistory> ImportHistories { get; set; }
        //public virtual DbSet<ImportHistoryDetail> ImportHistoryDetails { get; set; }

    }
}
