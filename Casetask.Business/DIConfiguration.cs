using Casetask.Business.Services;
using Casetask.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Casetask.Business;

public class DIConfiguration
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(DtoEntityMapperProfile));
        services.AddScoped<ITeacherService, TeacherService>();
        services.AddScoped<ISubjectService, SubjectService>();
    }
}
