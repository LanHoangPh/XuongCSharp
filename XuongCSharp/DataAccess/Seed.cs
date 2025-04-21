using XuongCSharp.DataAccess.Entities;

namespace XuongCSharp.DataAccess
{
    public static class Seed
    {
        public static void GenerateSeed(this ModelBuilder modelBuilder)
        {
            var staffs = new List<Staff>
            {
                new Staff
                {
                    Id = new Guid("550e8400-e29b-41d4-a716-446655440000"),
                    Status = 1,
                    CreatedDate = 1627849200000,
                    LastModifiedDate = 1627935600000,
                    AccountFe = "fe_account1@fe.edu.vn",
                    AccountFpt = "fpt_account1@fpt.edu.vn",
                    Name = "John wick",
                    StaffCode = "ST001"
                },
                new Staff
                {
                    Id = new Guid("550e8400-e29b-41d4-a716-446655440001"),
                    Status = 1,
                    CreatedDate = 1627849200000,
                    LastModifiedDate = 1627935600000,
                    AccountFe = "fe_account2@fe.edu.vn",
                    AccountFpt = "fpt_account2@fpt.edu.vn",
                    Name = "Top1 Flo",
                    StaffCode = "ST002"
                },
                new Staff
                {
                    Id = new Guid("550e8400-e29b-41d4-a716-446655440002"),
                    Status = 1,
                    CreatedDate = 1627849200000,
                    LastModifiedDate = 1627935600000,
                    AccountFe = "fe_account3@fe.edu.vn",
                    AccountFpt = "fpt_account3@fpt.edu.vn",
                    Name = "Lục Luật",
                    StaffCode = "ST003"
                }
            };
            foreach (var staff in staffs)
            {
                modelBuilder.Entity<Staff>().HasData(staff);
            }

            // Facility
            var facilities = new List<Facility>
            {
                new Facility
                {
                    Id = new Guid("550e8400-e29b-41d4-a716-446655440010"),
                    Status = 1,
                    CreatedDate = 1627849200000,
                    LastModifiedDate = 1627935600000,
                    Code = "FAC001",
                    Name = "HN"
                },
                new Facility
                {
                    Id = new Guid("550e8400-e29b-41d4-a716-446655440011"),
                    Status = 1,
                    CreatedDate = 1627849200000,
                    LastModifiedDate = 1627935600000,
                    Code = "FAC002",
                    Name = "HCM"
                }
            };
            foreach (var facility in facilities)
            {
                modelBuilder.Entity<Facility>().HasData(facility);
            }

            // Department
            var departments = new List<Department>
            {
                new Department
                {
                    Id = new Guid("550e8400-e29b-41d4-a716-446655440020"),
                    Status = 1,
                    CreatedDate = 1627849200000,
                    LastModifiedDate = 1627935600000,
                    Code = "DEP001",
                    Name = "Department One"
                },
                new Department
                {
                    Id = new Guid("550e8400-e29b-41d4-a716-446655440021"),
                    Status = 1,
                    CreatedDate = 1627849200000,
                    LastModifiedDate = 1627935600000,
                    Code = "DEP002",
                    Name = "Department Two"
                },
                new Department
                {
                    Id = new Guid("550e8400-e29b-41d4-a716-446655440022"),
                    Status = 1,
                    CreatedDate = 1627849200000,
                    LastModifiedDate = 1627935600000,
                    Code = "DEP003",
                    Name = "Department Three"
                }
            };
            foreach (var department in departments)
            {
                modelBuilder.Entity<Department>().HasData(department);
            }

            // DepartmentFacility
            var departmentFacilities = new List<DepartmentFacility>
            {
                new DepartmentFacility
                {
                    Id = new Guid("550e8400-e29b-41d4-a716-446655440030"),
                    Status = 1,
                    CreatedDate = 1627849200000,
                    LastModifiedDate = 1627935600000,
                    IdDepartment = new Guid("550e8400-e29b-41d4-a716-446655440020"),
                    IdFacility = new Guid("550e8400-e29b-41d4-a716-446655440010"),
                    IdStaff = new Guid("550e8400-e29b-41d4-a716-446655440000")
                },
                new DepartmentFacility
                {
                    Id = new Guid("550e8400-e29b-41d4-a716-446655440031"),
                    Status = 1,
                    CreatedDate = 1627849200000,
                    LastModifiedDate = 1627935600000,
                    IdDepartment = new Guid("550e8400-e29b-41d4-a716-446655440021"),
                    IdFacility = new Guid("550e8400-e29b-41d4-a716-446655440011"),
                    IdStaff = new Guid("550e8400-e29b-41d4-a716-446655440001")
                },
                new DepartmentFacility
                {
                    Id = new Guid("550e8400-e29b-41d4-a716-446655440032"),
                    Status = 1,
                    CreatedDate = 1627849200000,
                    LastModifiedDate = 1627935600000,
                    IdDepartment = new Guid("550e8400-e29b-41d4-a716-446655440022"),
                    IdFacility = new Guid("550e8400-e29b-41d4-a716-446655440011"),
                    IdStaff = new Guid("550e8400-e29b-41d4-a716-446655440002")
                }
            };
            foreach (var departmentFacility in departmentFacilities)
            {
                modelBuilder.Entity<DepartmentFacility>().HasData(departmentFacility);
            }

            // Major
            var majors = new List<Major>
            {
                new Major
                {
                    Id = new Guid("550e8400-e29b-41d4-a716-446655440040"),
                    Status = 1,
                    CreatedDate = 1627849200000,
                    LastModifiedDate = 1627935600000,
                    Name = "Major One",
                    Code = "MAJ001"
                },
                new Major
                {
                    Id = new Guid("550e8400-e29b-41d4-a716-446655440041"),
                    Status = 1,
                    CreatedDate = 1627849200000,
                    LastModifiedDate = 1627935600000,
                    Name = "Major Two",
                    Code = "MAJ002"
                },
                new Major
                {
                    Id = new Guid("550e8400-e29b-41d4-a716-446655440042"),
                    Status = 1,
                    CreatedDate = 1627849200000,
                    LastModifiedDate = 1627935600000,
                    Name = "Major Three",
                    Code = "MAJ003"
                }
            };
            foreach (var major in majors)
            {
                modelBuilder.Entity<Major>().HasData(major);
            }

            // MajorFacility
            var majorFacilities = new List<MajorFacility>
            {
                new MajorFacility
                {
                    Id = new Guid("550e8400-e29b-41d4-a716-446655440050"),
                    Status = 1,
                    CreatedDate = 1627849200000,
                    LastModifiedDate = 1627935600000,
                    IdDepartmentFacility = new Guid("550e8400-e29b-41d4-a716-446655440030"),
                    IdMajor = new Guid("550e8400-e29b-41d4-a716-446655440040")
                },
                new MajorFacility
                {
                    Id = new Guid("550e8400-e29b-41d4-a716-446655440051"),
                    Status = 1,
                    CreatedDate = 1627849200000,
                    LastModifiedDate = 1627935600000,
                    IdDepartmentFacility = new Guid("550e8400-e29b-41d4-a716-446655440031"),
                    IdMajor = new Guid("550e8400-e29b-41d4-a716-446655440041")
                },
                new MajorFacility
                {
                    Id = new Guid("550e8400-e29b-41d4-a716-446655440052"),
                    Status = 1,
                    CreatedDate = 1627849200000,
                    LastModifiedDate = 1627935600000,
                    IdDepartmentFacility = new Guid("550e8400-e29b-41d4-a716-446655440032"),
                    IdMajor = new Guid("550e8400-e29b-41d4-a716-446655440042")
                }
            };
            foreach (var majorFacility in majorFacilities)
            {
                modelBuilder.Entity<MajorFacility>().HasData(majorFacility);
            }

            // StaffMajorFacility
            var staffMajorFacilities = new List<StaffMajorFacility>
            {
                new StaffMajorFacility
                {
                    Id = new Guid("550e8400-e29b-41d4-a716-446655440060"),
                    Status = 1,
                    CreatedDate = 1627849200000,
                    LastModifiedDate = 1627935600000,
                    IdMajorFacility = new Guid("550e8400-e29b-41d4-a716-446655440050"),
                    IdStaff = new Guid("550e8400-e29b-41d4-a716-446655440000")
                },
                new StaffMajorFacility
                {
                    Id = new Guid("550e8400-e29b-41d4-a716-446655440061"),
                    Status = 1,
                    CreatedDate = 1627849200000,
                    LastModifiedDate = 1627935600000,
                    IdMajorFacility = new Guid("550e8400-e29b-41d4-a716-446655440051"),
                    IdStaff = new Guid("550e8400-e29b-41d4-a716-446655440001")
                },
                new StaffMajorFacility
                {
                    Id = new Guid("550e8400-e29b-41d4-a716-446655440062"),
                    Status = 1,
                    CreatedDate = 1627849200000,
                    LastModifiedDate = 1627935600000,
                    IdMajorFacility = new Guid("550e8400-e29b-41d4-a716-446655440052"),
                    IdStaff = new Guid("550e8400-e29b-41d4-a716-446655440002")
                }
            };
            foreach (var staffMajorFacility in staffMajorFacilities)
            {
                modelBuilder.Entity<StaffMajorFacility>().HasData(staffMajorFacility);
            }
        }
    }
}
