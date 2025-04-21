namespace XuongCSharp.DependencyInjection
{
    public static class ServerContainer
    {
        public static IServiceCollection AddFulent(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation(); //'IServiceCollection' does not contain a definition for 'AddFluentValidationAutoValidation' and no accessible extension method 'AddFluentValidationAutoValidation' accepting a first argument of type 'IServiceCollection' could be found (are you missing a using directive or an assembly reference?)
            services.AddValidatorsFromAssemblyContaining<CreateStaffDtoValidator>(); //'IServiceCollection' does not contain a definition for 'AddValidatorsFromAssemblyContaining' and no accessible extension method 'AddValidatorsFromAssemblyContaining' accepting a first argument of type 'IServiceCollection' could be found (are you missing a using directive or an assembly reference?)
            services.AddValidatorsFromAssemblyContaining<UpdateStaffDtoValidator>(); //'IServiceCollection' does not contain a definition for 'AddValidatorsFromAssemblyContaining' and no accessible extension method 'AddValidatorsFromAssemblyContaining' accepting a first argument of type 'IServiceCollection' could be found (are you missing a using directive or an assembly reference?)
            return services;
        }
    }
}
