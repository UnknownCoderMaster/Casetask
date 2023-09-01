using Casetask.Business;
using Casetask.Business.Services;
using Casetask.Common.Interfaces;
using Casetask.Common.Model;
using Casetask.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Casetask;

public static class DIConfiguration
{
    public static void RegisterServices(this IServiceCollection services)
    {
		//repositories
		services.AddScoped<IGenericRepository<Teacher>, GenericRepository<Teacher>>();
		services.AddScoped<IGenericRepository<Subject>, GenericRepository<Subject>>();
		services.AddScoped<IGenericRepository<Student>, GenericRepository<Student>>();
		services.AddScoped<IGenericRepository<Score>, GenericRepository<Score>>();
		services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();


		//services
		services.AddAutoMapper(typeof(DtoEntityMapperProfile));
        services.AddScoped<ITeacherService, TeacherService>();
        services.AddScoped<ISubjectService, SubjectService>();
        services.AddScoped<IStudentService, StudentService>();
		services.AddScoped<IUserService, UserService>();
		services.AddScoped<IAuthService, AuthService>();
    }

	public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddAuthentication(options =>
		{
			options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

			options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		})
		.AddJwtBearer(options =>
		{
			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidateAudience = false,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,
				ValidIssuer = configuration["JWT:ValidIssuer"],
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))
			};
		});
	}

	public static void AddSwaggerService(this IServiceCollection services)
	{
		services.AddSwaggerGen(p =>
		{
			p.ResolveConflictingActions(ad => ad.First());
			p.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
			{
				Name = "Authorization",
				Type = SecuritySchemeType.ApiKey,
				BearerFormat = "JWT",
				In = ParameterLocation.Header
			});

			p.AddSecurityRequirement(new OpenApiSecurityRequirement
			{
				{
					new OpenApiSecurityScheme()
					{
						Reference = new OpenApiReference()
						{
							Type = ReferenceType.SecurityScheme,
							Id = "Bearer"
						}
					},
					new string[] { }
				}
			});
		});
	}
}
