namespace XuongCSharp.Validations
{
    public class CreateStaffDtoValidator : AbstractValidator<CreateStaffDto>
    {
        public CreateStaffDtoValidator()
        {
            RuleFor(x => x.StaffCode)
                .NotEmpty().WithMessage("Staff Code không đuọc bỏ trống ")
                .MaximumLength(15).WithMessage("Staff code ko vượt quá 15 ký tự");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name không đc bỏ trống")
                .MaximumLength(100).WithMessage("Name không được vượt quá 100 ký tự");

            RuleFor(x => x.AccountFpt)
                .NotEmpty().WithMessage("FPT email không được bỏ trống ")
                .MaximumLength(100).WithMessage("FPT email không được vượt quá 100 ký tự")
                .Matches(@"^[a-zA-Z0-9._-]+@fpt\.edu\.vn$").WithMessage("FPT email cần kết thúc đuôi @fpt.edu.vn")
                .Must(email => !ContainsWhitespace(email) && !ContainsVietnameseChars(email))
                    .WithMessage("FPT email không được chứa ký tự Vietnamese");

            RuleFor(x => x.AccountFe)
                .NotEmpty().WithMessage("FE email không được bỏ trống")
                .MaximumLength(100).WithMessage("FE email không được vượt quá 100 ký tự")
                .Matches(@"^[a-zA-Z0-9._-]+@fe\.edu\.vn$").WithMessage("FE email cần kết thúc đuôi @fe.edu.vn")
                .Must(email => !ContainsWhitespace(email) && !ContainsVietnameseChars(email))
                    .WithMessage("FE email không được chứa ký tự Vietnamese");
        }

        private bool ContainsWhitespace(string? value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;
            return value.Contains(' ');
        }

        private bool ContainsVietnameseChars(string? value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;

            string vietnameseChars = "àáạảãâầấậẩẫăằắặẳẵèéẹẻẽêềếệểễìíịỉĩòóọỏõôồốộổỗơờớợởỡùúụủũưừứựửữỳýỵỷỹđ";
            return value.ToLower().Any(c => vietnameseChars.Contains(c));
        }
    }
}
