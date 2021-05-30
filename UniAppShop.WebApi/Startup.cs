using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniAppShop.IRepository;
using UniAppShop.IService;
using UniAppShop.Repository;
using UniAppShop.Service;
using UniAppShop.WebApi.AutofacHelper;
using EasyCaching.Core;
using EasyCaching.InMemory;

namespace UniAppShop.WebApi
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
            #region core跨域

            // 处理 api 请求跨域
            services.AddCors(options =>
            {
                options.AddPolicy("cors",
                    builder =>
                    {
                        builder.AllowAnyMethod()
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .WithExposedHeaders("Content-Disposition");
                    });
            });

            #endregion

            #region 配置swagger

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "UniAppShop.WebApi", Version = "v1" });
                #region swagger使用鉴权组件
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Description = "直接在下框中输入Bearer {token}(注意两者之间是一个空格)",
                    Name = "Authorization",
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference=new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{ }
                    }
                });
                #endregion
            });

            #endregion

            #region jwt 添加认证服务

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(options =>
            //{
            //    options.SaveToken = true;
            //    options.RequireHttpsMetadata = false;
            //    options.TokenValidationParameters = new TokenValidationParameters()
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidAudience = "https://www.cnblogs.com/chengtian",
            //        ValidIssuer = "https://www.cnblogs.com/chengtian",
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SecureKeySecureKeySecureKeySecureKeySecureKeySecureKey"))
            //    };
            //});

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SDMC-CJAS1-SAD-DFSFA-SADHJVF-VF")),
                        ValidateIssuer = true,
                        ValidIssuer = "http://localhost:54644",
                        ValidateAudience = true,
                        ValidAudience = "http://localhost:54644",
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(60)
                    };
                });

            #endregion

            #region 添加EasyCaching

            services.AddEasyCaching(option =>
            {
                option.UseInMemory();

                // 使用InMemory最简单的配置
                option.UseInMemory("default");

                //// 使用InMemory自定义的配置
                //option.UseInMemory(options => 
                //{
                //     // DBConfig这个是每种Provider的特有配置
                //     options.DBConfig = new InMemoryCachingOptions
                //     {
                //         // InMemory的过期扫描频率，默认值是60秒
                //         ExpirationScanFrequency = 60, 
                //         // InMemory的最大缓存数量, 默认值是10000
                //         SizeLimit = 100 
                //     };
                //     // 预防缓存在同一时间全部失效，可以为每个key的过期时间添加一个随机的秒数，默认值是120秒
                //     options.MaxRdSecond = 120;
                //     // 是否开启日志，默认值是false
                //     options.EnableLogging = false;
                //     // 互斥锁的存活时间, 默认值是5000毫秒
                //     options.LockMs = 5000;
                //     // 没有获取到互斥锁时的休眠时间，默认值是300毫秒
                //     options.SleepMs = 300;
                // }, "m2");         

                //// 读取配置文件
                //option.UseInMemory(Configuration, "m3");
            });

            #endregion

            services.AddControllers();
            
        }

        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterModule<ConfigureAutofac>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();                
            }

            #region 跨域

            app.UseCors("cors");

            #endregion

            #region swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "UniAppShop.WebApi V1");                
            });
            #endregion

            app.UseRouting();

            app.UseAuthentication();//认证中间件
            app.UseAuthorization();

            #region 静态文件

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "UploadFiles")),
                RequestPath = new PathString("/UploadFiles")
            });

            #endregion           

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
