using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Zust.Business.Abstract;
using Zust.Business.Concrete;
using Zust.DataAccess.Abstract;
using Zust.DataAccess.Concrete.EfEntityFramework;
using Zust.DataAccess.Concrete.EFEntityFramework;
using Zust.Entities;
using Zust.WebUI.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });



builder.Services.AddScoped<IUserDal, EFUserDal>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IFriendRequestDal, EFFriendRequestDal>();
builder.Services.AddScoped<IFriendRequestService, FriendRequestService>();

builder.Services.AddScoped<IFriendDal, EFFriendDal>();
builder.Services.AddScoped<IFriendService, FriendService>();

builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<INotificationDal, EFNotificationDal>();

builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IMessageDal, EFMessageDal>();

builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<IChatDal, EFChatDal>();

builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IPostDal, EFPostDal>();

builder.Services.AddScoped<ICommentDal, EFCommentDal>();
builder.Services.AddScoped<ICommentService, CommentService>();


var conn = builder.Configuration.GetConnectionString("myconn");

builder.Services.AddDbContext<AppDBContext>(opt =>
{
    opt.UseSqlServer(conn);
});


builder.Services.AddIdentity<AppUser, AppRole>()
    .AddEntityFrameworkStores<AppDBContext>()
    .AddDefaultTokenProviders();
builder.Services.AddSignalR();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute("Default", "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapHub<ChatHub>("/chathub");
    endpoints.MapHub<FriendRequestHub>("/friendrequesthub");
});
app.Run();
