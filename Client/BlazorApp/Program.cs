using BlazorApp.Components;
using BlazorApp.Components.Auth;
using BlazorApp.Services.CommentService;
using BlazorApp.Services.PostService;
using BlazorApp.Services.UserService;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.Services.AddScoped(sp => new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5026")
        });
        builder.Services.AddScoped<IUserService, HttpUserService>();
        builder.Services.AddScoped<IPostService, HttpPostService>();
        builder.Services.AddScoped<ICommentService, HttpCommentService>();
        builder.Services.AddScoped<AuthenticationStateProvider, SimpleAuthProvider>();

        

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}