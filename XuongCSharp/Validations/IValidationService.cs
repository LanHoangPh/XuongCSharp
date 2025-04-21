namespace XuongCSharp.Validations
{
    public interface IValidationService
    {
        Task ValidateAsync<T>(T model, IValidator<T> validator);
    }
}
