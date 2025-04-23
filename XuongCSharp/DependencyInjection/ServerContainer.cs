namespace XuongCSharp.DependencyInjection
{
    public static class ServerContainer
    {
        public static IServiceCollection AddFulent(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation(); 
            services.AddValidatorsFromAssemblyContaining<CreateStaffDtoValidator>(); 
            services.AddValidatorsFromAssemblyContaining<UpdateStaffDtoValidator>(); 
            services.AddValidatorsFromAssemblyContaining<StaffDepartmentDtoValidator>();
            return services;
        }
    }
}
