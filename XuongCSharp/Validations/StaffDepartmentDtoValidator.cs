namespace XuongCSharp.Validations
{
    public class StaffDepartmentDtoValidator: AbstractValidator<StaffDepartmentDto>
    {
        public StaffDepartmentDtoValidator()
        {
            RuleFor(x => x.FacilityId).NotEqual(Guid.Empty).WithMessage("Cơ sở là bắt buộc");
            RuleFor(x => x.DepartmentId).NotEqual(Guid.Empty).WithMessage("Bộ môn là bắt buộc");
            RuleFor(x => x.MajorId).NotEqual(Guid.Empty).WithMessage("Chuyên ngành là bắt buộc");
        }
    }
}
