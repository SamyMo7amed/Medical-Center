using Medical_CenterAPI.Data;
using Medical_CenterAPI.ExtenstionMethods;
using Medical_CenterAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
 
builder.Services.AddControllers();

builder.Services.AddSwaggerGen(
op =>
{
    op.SwaggerDoc("v1", new OpenApiInfo()
    {
        Version = "v1",
        Title = "Medical_center api",
        Description = "This api for project in our college",
        Contact = new OpenApiContact()
        {
            Name = "This API was developed by Engineer Salspil Amin  and Engineer Sami Mohamed.",
            Url = new Uri("https://github.com/SamyMo7amed/Medical-Center")

        }
    });
    op.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme() {

        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,

    });
    op.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme()
            {
                Reference=new OpenApiReference()
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                },
                Name="Bearer",
                In=ParameterLocation.Header

            } , new List<string>{}

    }
    });


}
);
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));


builder.Configuration.AddJsonFile("Secrets.json");



// add configuration of Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))


    };
    options.SaveToken=true;
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ManagerOnly", policy => policy.RequireClaim("Manager"));
    options.AddPolicy("Doctor", policy => policy.RequireClaim("Doctor"));
    options.AddPolicy("Assistant", policy => policy.RequireClaim("Assistant"));
});

builder.Services.AddIdentity<AppUser, IdentityRole<Guid>>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyPolicy", pol =>
    {
        pol.AllowAnyMethod();
        pol.AllowAnyOrigin();
        pol.AllowAnyHeader();
    });
});
builder.Services.RegisterDI();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<InitializeRoles>();
    await initializer.InitRoles();
}


// Configure the HTTP request pipeline.

    
   
    app.UseSwagger();
  
    app.UseSwaggerUI();
    app.MapOpenApi();
    app.MapControllers();



app.UseHttpsRedirection();
app.UseRouting();   
app.UseCors("MyPolicy");          
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
app.UseStaticFiles();   // to make wwwroot work


app.Run();
