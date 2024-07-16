using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Quartz;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using TeachMate.Api;
using TeachMate.Domain;
using TeachMate.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ProducesAttribute("application/json", "text/plain", "text/json"));
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.UseDateOnlyTimeOnlyStringConverters();
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "TeachMate Swagger API",
        Description = "An ASP.NET Core Web API for TeachMate Web App",
        TermsOfService = new Uri("https://example.com/terms"),
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme.
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });

    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

// Add Database configuration
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add Cors
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Add Authenticaion and Authorization
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"] ?? ""))
    };
});
builder.Services.AddAuthorization();
builder.Services.AddSingleton<IAuthorizationMiddlewareResultHandler, CustomAuthorizationMiddlewareResultHandler>();

// Add Http Context Accessor
builder.Services.AddHttpContextAccessor();

// Add Fluent Validation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<IAssemblyMarker>();

// Add DateOnly and TimeOnly Converters
builder.Services.AddDateOnlyTimeOnlyStringConverters();

// Add Configs
builder.Services.Configure<GoogleAuthConfig>(builder.Configuration.GetSection("GoogleOAuth"));
builder.Services.Configure<EmailConfig>(builder.Configuration.GetSection("Gmail"));
builder.Services.Configure<AblyConfig>(builder.Configuration.GetSection("Ably"));
builder.Services.Configure<ZaloPayConfig>(builder.Configuration.GetSection("ZaloPay"));
builder.Services.Configure<MomoConfig>(builder.Configuration.GetSection("Momo"));
builder.Services.Configure<VnPayConfig>(builder.Configuration.GetSection("VnPay"));

// Add User-Defined Services
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IConfigService, ConfigService>();
builder.Services.AddScoped<IHttpContextService, HttpContextService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IGoogleAuthService, GoogleAuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ILearningModuleService, LearningModuleService>();

builder.Services.AddScoped<ISearchTutor, SearchTutor>();
builder.Services.AddScoped<ISearchClass, SearchClass>();
builder.Services.AddScoped<IFeedbackService, FeedbackService>();

builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IUserDetailService, UserDetailService>();
builder.Services.AddScoped<IScheduleService, ScheduleService>();

builder.Services.AddScoped<IEmailOtp, EmailOTPService>();

builder.Services.AddScoped<IZaloPayService, ZaloPayService>();
builder.Services.AddScoped<IMomoService, MomoService>();
builder.Services.AddScoped<IVnPayService, VnPayService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IReportService, ReportService>();

builder.Services.AddScoped<ILearningMaterialService, LearningMaterialService>();
builder.Services.AddScoped<ICertificateService, CertificateService>();

// Add Cron-jobs
//builder.Services.AddQuartz(q =>
//{
//    var jobKey = new JobKey("NotifyUsersToPay");
//    q.AddJob<NotifyUsersToPay>(opts => opts.WithIdentity(jobKey));

//    q.AddTrigger(opts => opts
//        .ForJob(jobKey)
//        .WithIdentity("NotifyUsersToPay-trigger")
//        // this will trigger at the end of every month
//        .WithCronSchedule("0 0 0 L * ?"));

//});
// Add Cron-jobs
DateTime scheduleTime = new DateTime(2024, 6, 27, 19, 35, 0);

if (DateTime.Compare(scheduleTime, DateTime.Now) <= 0)
{
    Console.WriteLine("Không thể lập lịch cho một ngày quá khứ hoặc hiện tại.");
}
else
{
    var cronExpression = $"0 {scheduleTime.Minute} {scheduleTime.Hour} {scheduleTime.Day} {scheduleTime.Month} ? {scheduleTime.Year}";

    builder.Services.AddQuartz(q =>
    {
        var jobKey = new JobKey("NotifyUsersToPay");
        q.AddJob<NotifyUsersToPay>(opts => opts.WithIdentity(jobKey));

        q.AddTrigger(opts => opts
            .ForJob(jobKey)
            .WithIdentity("NotifyUsersToPay-trigger")
            .WithCronSchedule(cronExpression));
    });

    // Đăng ký QuartzHostedService và cấu hình
    builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
}



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseGlobalExceptionHandler();

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();