using System.Globalization;
using System.Resources;
using IOManagerBLL;
using IOManagerBLL.Classes;
using IOManagerBLL.Model;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Study_ASPNET_Core.Resources;
using Study_ASPNET_Core.Services.FileIOService;

namespace Study_ASPNET_Core
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /* ================================================
             *  builder 설정 시작
             * ================================================
             */
            var builder = WebApplication.CreateBuilder(args);

            // 다국어 지원을 위한 리소스 매니저 추가
            builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

            builder.Services.AddControllersWithViews()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix) // 뷰에 대한 다국어 지원
                .AddDataAnnotationsLocalization(options =>                      // 데이터 어노테이션에 대한 다국어 지원
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                        factory.Create(typeof(Lan));
                });

            // 문화권 설정
            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("ko"),
                    new CultureInfo("en")
                };

                options.DefaultRequestCulture = new RequestCulture("ko");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            /* ================================================
             * 의존 주입 시작
             * ================================================
             */


            /* ================================================
             *  의존 주입 종료
             * ================================================
             */

            /* ================================================
             *  app 설정 시작
             * ================================================
             */
            var app = builder.Build();

            app.UseRequestLocalization();

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            /* ================================================
             *  app 설정 종료
             * ================================================
             */

            app.Run();
        }
    }
}
