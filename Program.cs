
using Microsoft.EntityFrameworkCore;
using RentWheelzAPI.Repository;

namespace RentWheelzAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add CORS services.
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
            //configure dbcontext 
            builder.Services.AddDbContext<RentWheelzDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            // Configure Users Repository
            builder.Services.AddScoped<IUsersRepository, UsersRepository>();
            // Configure Car Repository
            builder.Services.AddScoped<ICarRepository, CarRepository>();
            // Configure Reservation Repository
            builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            // Use CORS policy.
            app.UseCors("AllowSpecificOrigins");

            app.MapControllers();

            app.Run();
        }
    }
}
