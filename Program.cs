// =============================================================
// Program.cs
// The entry point of the application.
// Configures all services and the HTTP request pipeline.
// =============================================================

using FitnessTracker.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// -------------------------------------------------------
// 1. ADD SERVICES TO THE CONTAINER
// -------------------------------------------------------

// Add MVC (Model-View-Controller) support
builder.Services.AddControllersWithViews();


// Add Entity Framework with SQLite database
// The database file "FitnessTracker.db" will be created automatically
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=FitnessTracker.db"));


// Add Cookie Authentication
// This is how the app remembers that a user is logged in
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";       // Redirect here if not logged in
        options.LogoutPath = "/Account/Logout";     // Redirect here on logout
        options.AccessDeniedPath = "/Account/Login";
        options.ExpireTimeSpan = TimeSpan.FromHours(24); // Stay logged in for 24 hours
    });


// Add Session support (used for storing user info across requests)
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();


// -------------------------------------------------------
// 2. CONFIGURE THE HTTP REQUEST PIPELINE
// -------------------------------------------------------

// In production, use a friendly error page
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Serve CSS, JS, images from wwwroot folder
app.UseRouting();
app.UseSession();           // Enable session middleware
app.UseAuthentication();    // Check who the user is
app.UseAuthorization();     // Check what the user can access

// Default route: /Controller/Action/Id
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// -------------------------------------------------------
// 3. ENSURE DATABASE IS CREATED AND MIGRATED
// -------------------------------------------------------
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    // Creates the database and applies any pending migrations automatically
    db.Database.EnsureCreated();
}

app.Run();
