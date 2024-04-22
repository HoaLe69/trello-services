using Microsoft.EntityFrameworkCore;
using trello_services.Configuration;
using trello_services.Data;
using trello_services.IRepository;
using trello_services.Services.Implement;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using trello_services.Models;
using System.Text;
using trello_services.Services;
using JwtRoleAuthentication.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var jwtOptionsSection = builder.Configuration.GetRequiredSection("Jwt");
builder.Services.Configure<JwtOptions>(jwtOptionsSection);


// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// DI automapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
// add dbcontext
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectString"));
});
// jwt services
builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwtOptions => {

    var configKey = jwtOptionsSection["Key"];
    var key = Encoding.UTF8.GetBytes(configKey);

    jwtOptions.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key)

    };
});
// lowercase api path
builder.Services.AddRouting(options => options.LowercaseUrls = true);
// declare repository pattern
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IWorkspaceRepository, WorkspaceRepository>();
builder.Services.AddScoped<IUserOfWorkspaceRepository, UserOfWorkspaceRepository>();
builder.Services.AddScoped<IBoardRepository, BoardRepository>();
builder.Services.AddScoped<IUserBoardRepository , UserBoardRepository>();
builder.Services.AddScoped<IColumnRepository , ColumnRepository>();
builder.Services.AddScoped<ILabelRepository , LabelRepository>();
builder.Services.AddScoped<ICardLabelRepository , CardLabelRepository>();
builder.Services.AddScoped<IChecklistDetailRepository , ChecklistDetailRepository>();
builder.Services.AddScoped<IChecklistRepository ,  ChecklistRepository>();
builder.Services.AddScoped<ICardRepository , CardRepository>();
builder.Services.AddScoped<IUserCardRepository , UserCardRepository>();
builder.Services.AddScoped<IJwtServices ,  JwtServices>();
builder.Services.AddScoped<IAuth , AuthServices>();

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Test API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();
