using FluentValidation;
using static MudCilent.Components.Pages.StafffView.AddDepartmentDialog;

namespace MudCilent.Validations
{
    public class AddDepartmentViewModelValidator : AbstractValidator<AddDepartmentViewModel>
    {
        public AddDepartmentViewModelValidator()
        {
            RuleFor(x => x.FacilityId).NotEqual(Guid.Empty).WithMessage("Cơ sở là bắt buộc");
            RuleFor(x => x.DepartmentId).NotEqual(Guid.Empty).WithMessage("Bộ môn là bắt buộc");
            RuleFor(x => x.MajorId).NotEqual(Guid.Empty).WithMessage("Chuyên ngành là bắt buộc");
        }
    }
}
