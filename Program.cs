using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using StansAssociates_Backend.Concrete.IServices;
using StansAssociates_Backend.Concrete.Services;
using StansAssociates_Backend.Entities;
using StansAssociates_Backend.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var env = builder.Environment.EnvironmentName ?? "Development";
//var defaultPort = env.Equals("QA", StringComparison.CurrentCultureIgnoreCase) ? 5001 : env.Equals("Production", StringComparison.CurrentCultureIgnoreCase) ? 5002 : 5000;
//var port = Environment.GetEnvironmentVariable("PORT") ?? defaultPort.ToString();
//builder.WebHost.UseUrls($"http://localhost:{port}");

builder.Services.AddDbContext<StansassociatesAntonyContext>(options =>
{
    options.UseMySql(
            builder.Configuration.GetConnectionString("DefaultConnection"),
            ServerVersion.Parse("8.0.28"))
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors();
});

builder.Services.AddHttpClient();

builder.Services.AddControllers()
    .AddNewtonsoftJson(o =>
    {
        o.SerializerSettings.ContractResolver = new DefaultContractResolver();
        o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        o.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Local;
        o.SerializerSettings.DateFormatString = "yyyy'-'MM'-'dd'T'HH':'mm':'ssZ";
    });

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("StansAssociates Backend Admin", new OpenApiInfo
    {
        Title = $"StansAssociates Backend Admin {env}",
        Version = "1.0",
        Description = "JobPortal using ASP.NET CORE 9",
        Contact = new OpenApiContact
        {
            Name = "",
            Email = "",
        },
    });
    c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme.",
    });
    c.OperationFilter<AuthOperationFilter>();
});

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var securityKey = builder.Configuration.GetValue<string>("JwtOptions:SecurityKey")
                      ?? throw new InvalidOperationException("JWT SecurityKey is not configured");
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration.GetValue<string>("JwtOptions:Issuer"),
        ValidAudience = builder.Configuration.GetValue<string>("JwtOptions:Audience"),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey))
    };
});

builder.Services.AddCors(o => o.AddPolicy("CorsPolicy", policyBuilder =>
{
    policyBuilder
        .AllowAnyMethod()
        .AllowAnyHeader()
        .WithOrigins("http://localhost:4200", "https://localhost:4200", "http://13.201.150.140")
        .SetIsOriginAllowedToAllowWildcardSubdomains()
        .AllowCredentials();
}));

builder.Services.AddHttpContextAccessor();
builder.Services.AddRazorPages();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IAccountServices, AccountServices>();
builder.Services.AddScoped<ICurrentUserServices, CurrentUserServices>();
builder.Services.AddScoped<IDashboardServices, DashboardServices>();
builder.Services.AddScoped<IFileExtensionService, FileExtensionService>();
builder.Services.AddScoped<IMemberServices, MemberServices>();
builder.Services.AddScoped<IRouteServices, RouteServices>();
builder.Services.AddScoped<IStorageServices, StorageServices>();
builder.Services.AddScoped<IStudentServices, StudentService>();
builder.Services.AddScoped<IDropDownServices, DropDownServices>();
builder.Services.AddScoped<ISchoolServices, SchoolServices>();
builder.Services.AddScoped<ITeamServices, TeamServices>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStatusCodePages("text/plain", "Status code page, status code: {0}");
app.UseHttpsRedirection();
app.UseCookiePolicy();
app.UseStaticFiles();
app.UseDefaultFiles();
app.UseRouting();
app.UseHsts();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/StansAssociates Backend Admin/swagger.json", "Admin API");
    c.RoutePrefix = "swagger";
    //c.SwaggerEndpoint("/swagger/StansAssociates Backend/swagger.json", "StansAssociates Backend");
    c.DocExpansion(DocExpansion.None);
    c.DefaultModelsExpandDepth(-1);
    c.DisplayRequestDuration();
});

app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();

#pragma warning disable ASP0014 // Suggest using top level route registrations
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
#pragma warning restore ASP0014 // Suggest using top level route registrations

app.Use(async (context, next) =>
{
    if (context.Request.Path.HasValue && context.Request.Path.Value != "/")
    {
        context.Response.ContentType = "text/html";
        await context.Response.SendFileAsync(
            builder.Environment.ContentRootFileProvider.GetFileInfo("wwwroot/index.html")
        );
        return;
    }
    await next();
});

app.Run();