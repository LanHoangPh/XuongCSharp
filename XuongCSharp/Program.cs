using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using XuongCSharp.Validations;

namespace XuongCSharp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddFulent();

            // Register services
            builder.Services.AddScoped<IStaffService, StaffService>();

            // Add CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowBlazor", builder =>
                {
                    builder.WithOrigins("https://localhost:7020") 
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors("AllowBlazor");
            app.MapControllers();

            app.Run();
        }
    }
}
