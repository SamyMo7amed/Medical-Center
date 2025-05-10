using Medical_CenterAPI.Data;
using Medical_CenterAPI.ExtenstionMethods;
using Medical_CenterAPI.Models;
using Medical_CenterAPI.Service;
using Medical_CenterAPI.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;



var builder = WebApplication.CreateBuilder(args);

// 1. Add Configuration Files FIRST
builder.Configuration.AddJsonFile("Secrets.json");

// 2. Add DbContext and Data Services
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

// 3. Add Identity
builder.Services.AddIdentity<AppUser, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// 4. Add Authentication (JWT)
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
        RoleClaimType = ClaimTypes.Role  // Critical for [Authorize(Roles)]
    };
});

// 5. Add Authorization Policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Manager", policy => policy.RequireRole("Manager"));
    options.AddPolicy("Doctor", policy => policy.RequireRole("Doctor"));
    options.AddPolicy("Assistant", policy => policy.RequireRole("Assistant"));
    options.AddPolicy("Patient", policy => policy.RequireRole("Patient"));
});

// 6. Add AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// 7. Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyPolicy", pol => pol.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// 8. Add Other Services (UnitOfWork, etc.)
builder.Services.RegisterDI();

// 9. Add Controllers
builder.Services.AddControllers();

// 10. Add Swagger




builder.Services.AddSwaggerGen(op =>
{
    op.SwaggerDoc("v1", new OpenApiInfo()
    {
        Version = "v1",
        Title = "Medical_center api",
        Description = "This api for project in our college",
        Contact = new OpenApiContact()
        {
            Name = "This API was developed by Engineer Salspil Amin and Engineer Sami Mohamed.",
            Url = new Uri("https://github.com/SamyMo7amed/Medical-Center")
        }
    });

    // Define the Bearer security scheme
    op.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header
    });

    // Add security requirement (fixed)
    op.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer" // Must match the security definition ID
                }
            },
            new List<string>() // No scopes needed for JWT
        }
    });
});




var app = builder.Build();



using (var scope = app.Services.CreateScope())
{

    var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
    var check = await unitOfWork.UserManager.FindByEmailAsync(builder.Configuration["Admin:gmail"]!);

    if (check == null)
    {
        var user = new AppUser() { UserName = "Tanta", Email = builder.Configuration["Admin:gmail"]!, PhoneNumber = builder.Configuration["Admin:number"] };
        var Password = builder.Configuration["Admin:pass"]!;
        user.Password = Password;   
        user.ConfirmPassword=Password;
        user.EmailConfirmed = true;
        var result = await unitOfWork.UserManager.CreateAsync(user, Password);
        if (result.Succeeded)
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
            if (!await roleManager.RoleExistsAsync("Manager"))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>("Manager"));
                await unitOfWork.CommitAsync();
            }
            await unitOfWork.UserManager.AddToRoleAsync(user, "Manager");
            await unitOfWork.CommitAsync();
        }
      
    }
}

using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<InitializeRoles>();
    await initializer.InitRoles();
}


// Configure the HTTP request pipeline.




// 2. HTTPS Redirection
app.UseHttpsRedirection();

// 3. Serve static files (e.g., images, CSS)
app.UseStaticFiles(); // ✅ Place before routing

// 4. Routing
app.UseRouting();

// 5. CORS
app.UseCors("MyPolicy");

// 6. Authentication & Authorization
app.UseAuthentication(); // 🔑 Authenticate users
app.UseAuthorization();  // ✅ Must be between UseRouting and endpoints

// 7. Swagger (for API documentation)
app.UseSwagger();
app.UseSwaggerUI();

// 8. Endpoints (controllers, minimal APIs, etc.)
app.MapOpenApi();
app.MapControllers();


app.Run();
