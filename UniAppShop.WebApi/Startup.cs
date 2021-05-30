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
            #region core����

            // ���� api �������
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

            #region ����swagger

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "UniAppShop.WebApi", Version = "v1" });
                #region swaggerʹ�ü�Ȩ���
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Description = "ֱ�����¿�������Bearer {token}(ע������֮����һ���ո�)",
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

            #region jwt �����֤����

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

            #region ���EasyCaching

            services.AddEasyCaching(option =>
            {
                option.UseInMemory();

                // ʹ��InMemory��򵥵�����
                option.UseInMemory("default");

                //// ʹ��InMemory�Զ��������
                //option.UseInMemory(options => 
                //{
                //     // DBConfig�����ÿ��Provider����������
                //     options.DBConfig = new InMemoryCachingOptions
                //     {
                //         // InMemory�Ĺ���ɨ��Ƶ�ʣ�Ĭ��ֵ��60��
                //         ExpirationScanFrequency = 60, 
                //         // InMemory����󻺴�����, Ĭ��ֵ��10000
                //         SizeLimit = 100 
                //     };
                //     // Ԥ��������ͬһʱ��ȫ��ʧЧ������Ϊÿ��key�Ĺ���ʱ�����һ�������������Ĭ��ֵ��120��
                //     options.MaxRdSecond = 120;
                //     // �Ƿ�����־��Ĭ��ֵ��false
                //     options.EnableLogging = false;
                //     // �������Ĵ��ʱ��, Ĭ��ֵ��5000����
                //     options.LockMs = 5000;
                //     // û�л�ȡ��������ʱ������ʱ�䣬Ĭ��ֵ��300����
                //     options.SleepMs = 300;
                // }, "m2");         

                //// ��ȡ�����ļ�
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

            #region ����

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

            app.UseAuthentication();//��֤�м��
            app.UseAuthorization();

            #region ��̬�ļ�

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
