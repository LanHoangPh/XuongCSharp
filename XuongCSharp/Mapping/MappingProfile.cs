using XuongCSharp.DataAccess.Entities;
namespace XuongCSharp.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Staff mappings
            CreateMap<Staff, StaffDto>()
                .ForMember(dest => dest.Locations, opt => opt.MapFrom(src => MapStaffLocations(src)));

            CreateMap<CreateStaffDto, Staff>();
            CreateMap<UpdateStaffDto, Staff>();

            // Import log mappings
            CreateMap<ImportLog, ImportLogDto>();
            CreateMap<ImportLogDetail, ImportLogDetailDto>();
        }

        private List<StaffLocationDto> MapStaffLocations(Staff staff)
        {
            var locations = new List<StaffLocationDto>();

            foreach (var staffMajorFacility in staff.StaffMajorFacilities)
            {
                if (staffMajorFacility.MajorFacility?.DepartmentFacility == null)
                    continue;

                var facility = staffMajorFacility.MajorFacility.DepartmentFacility.Facility;
                var department = staffMajorFacility.MajorFacility.DepartmentFacility.Department;
                var major = staffMajorFacility.MajorFacility.Major;

                if (facility == null || department == null || major == null)
                    continue;

                locations.Add(new StaffLocationDto
                {
                    FacilityId = facility.Id,
                    FacilityName = facility.Name,
                    DepartmentId = department.Id,
                    DepartmentName = department.Name,
                    MajorId = major.Id,
                    MajorName = major.Name,
                    StaffId = staff.Id

                });
            }

            return locations;
        }
    }
}
