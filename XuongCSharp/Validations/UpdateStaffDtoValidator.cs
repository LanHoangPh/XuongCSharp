namespace XuongCSharp.Validations
{
    public class UpdateStaffDtoValidator: AbstractValidator<UpdateStaffDto>
    {
        public UpdateStaffDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters");

            RuleFor(x => x.AccountFpt)
                .NotEmpty().WithMessage("FPT email is required")
                .MaximumLength(100).WithMessage("FPT email cannot exceed 100 characters")
                .Matches(@"^[a-zA-Z0-9._-]+@fpt\.edu\.vn$").WithMessage("FPT email must end with @fpt.edu.vn")
                .Must(email => !ContainsWhitespace(email) && !ContainsVietnameseChars(email))
                    .WithMessage("FPT không được chứa ký tự Vietnamese");

            RuleFor(x => x.AccountFe)
                .NotEmpty().WithMessage("FE email is required")
                .MaximumLength(100).WithMessage("FE email cannot exceed 100 characters")
                .Matches(@"^[a-zA-Z0-9._-]+@fe\.edu\.vn$").WithMessage("FE email must end with @fe.edu.vn")
                .Must(email => !ContainsWhitespace(email) && !ContainsVietnameseChars(email))
                    .WithMessage("FE không được chứa ký tự Vietnamese");
        }

        private bool ContainsWhitespace(string value)
        {
            return value.Contains(" ");
        }

        private bool ContainsVietnameseChars(string value)
        {
            string vietnameseChars = "àáạảãâầấậẩẫăằắặẳẵèéẹẻẽêềếệểễìíịỉĩòóọỏõôồốộổỗơờớợởỡùúụủũưừứựửữỳýỵỷỹđ";
            return value.ToLower().Any(c => vietnameseChars.Contains(c));
        }
    }
}
