

using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;

namespace MatchGoalAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

			var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
			builder.Services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(connectionString));

			builder.Services.AddIdentity<ApplicationUser,IdentityRole>(options =>
									   options.SignIn.RequireConfirmedAccount = true)
	        .AddEntityFrameworkStores<ApplicationDbContext>();

            // Add services to the container.

            builder.Services.AddAuthentication()
                .AddCookie("Cookies", options =>
                {
                    options.LoginPath = "/Account/Login";
                    options.LogoutPath = "/Account/Logout";
                    options.Cookie.Name = "accessToken";
					options.Cookie.HttpOnly = true;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                    options.Cookie.SameSite = SameSiteMode.Strict;
					options.AccessDeniedPath = "/Account/AccessDenied";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(120);
				});

			builder.Services.AddHttpClient();

			builder.Services.AddScoped<IBaseRepository<Match>,MatchRepository>();
            builder.Services.AddScoped<IMatchRepository, MatchRepository>();
            builder.Services.AddScoped<IAuthRepository, AuthRepository>();

			builder.Services.AddLogging(logging =>
			{
				logging.AddConsole();
				logging.AddDebug();
			});

			builder.Services.AddAutoMapper(typeof(MatchProfile));

			builder.Services.AddControllers()
	        .AddJsonOptions(options =>
	        {
		    options.JsonSerializerOptions.Converters.Add(
			new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, allowIntegerValues: true) // allowIntegerValues: true
		    );
		    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
		    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
	        }); ;
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            // JWT Authentication
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
					RequireExpirationTime = true,
					ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                    ClockSkew = TimeSpan.Zero
                };
            });

			var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

			// if token come in cookies only then add it to the request header 
			app.Use(async (context, next) =>
            {
                if (context.Request.Cookies.TryGetValue("accessToken",out string accessToken))
				{
					context.Request.Headers.Add("Authorization", "Bearer " + accessToken);
				}
                await next(context);
			});

            app.UseAuthentication();
			app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
