using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Data.Models_IB;

using Data.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Data.Models;
using Data.Models_QLTaiKhoan;

namespace SaleDoanInbound
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<qltourContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))/*.EnableSensitiveDataLogging()*/);
            services.AddDbContext<SaleDoanIBDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SaleDoanIBConnection"))/*.EnableSensitiveDataLogging()*/);
            services.AddDbContext<qltaikhoanContext>(options => options.UseSqlServer(Configuration.GetConnectionString("QLTaiKhoanConnection"))/*.EnableSensitiveDataLogging()*/);

            services.AddTransient<IKhachHangRepository, KhachHangRepository>();
            services.AddTransient<IQuocGiaRepository, QuocGiaRepository>();
            services.AddTransient<IQuanRepository, QuanRepository>();
            services.AddTransient<IDMNganhNgheRepository, DMNganhNgheRepository>();
            services.AddTransient<IKhuVucTGRepository, KhuVucTGRepository>();
            services.AddTransient<ICacNoiDungHuyTourRepository, CacNoiDungHuyTourRepository>();
            services.AddTransient<ILoaiTourRepository, LoaiTourRepository>();
            services.AddTransient<IChiNhanhRepository, ChiNhanhRepository>();
            services.AddTransient<IDiemTQRepository, DiemTQRepository>();
            services.AddTransient<IThanhPhoRepository, ThanhPhoRepository>();
            services.AddTransient<ITourIBRepository, TourIBRepository>();
            services.AddTransient<ICTVATRepository, CTVATRepository>();
            services.AddTransient<IInvoiceRepository, InvoiceRepository>();
            services.AddTransient<IBienNhanRepository, BienNhanRepository>();
            services.AddTransient<IChiTietBNRepository, ChiTietBNRepository>();
            services.AddTransient<IUnitOfWork, UnitOfwork>();

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            var supportedCultures = new[] { new CultureInfo("en-AU") };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-AU"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
