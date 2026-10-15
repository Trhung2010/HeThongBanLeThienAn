using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Resolve SQLite relative to the API folder, regardless of the launch directory.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Thiếu cấu hình kết nối SQLite.");
var sqlite = new Microsoft.Data.Sqlite.SqliteConnectionStringBuilder(connectionString);
if (sqlite.DataSource != ":memory:" && !Path.IsPathRooted(sqlite.DataSource))
    sqlite.DataSource = Path.Combine(builder.Environment.ContentRootPath, sqlite.DataSource);
builder.Configuration["ConnectionStrings:DefaultConnection"] = sqlite.ConnectionString;
builder.Services.AddDbContext<SupermarketDbContext>(options => options.UseSqlite(sqlite.ConnectionString));

// Cấu hình dịch vụ xác thực JWT Bearer
var jwtSecret = builder.Configuration["JwtSettings:Secret"] ??
"SupermarketSecretKeyDoAnMonHoc2026SecureString!!";

builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSecret)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// Cấu hình dịch vụ ADO.NET Connection Factory
builder.Services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();

// Cấu hình dịch vụ và middleware khác
builder.Services.AddScoped<CheckoutService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Apply versioned migrations; never delete business data based on row counts.
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<SupermarketDbContext>();
    dbContext.Database.Migrate();
}

// Cấu hình middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Bắt buộc gọi UseAuthentication trước UseAuthorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();