using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wieczorna_nauka_aplikacja_webowa.Authorization;
using Wieczorna_nauka_aplikacja_webowa.Controllers;
using Wieczorna_nauka_aplikacja_webowa.Controllers.Services;
using Wieczorna_nauka_aplikacja_webowa.Entities;
using Wieczorna_nauka_aplikacja_webowa.Middleware;
using Wieczorna_nauka_aplikacja_webowa.Models;
using Wieczorna_nauka_aplikacja_webowa.Services;
using Wieczorna_nauka_aplikacja_webowa.Services.Services;

namespace Wieczorna_nauka_aplikacja_webowa
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
            var authenitactionSettings = new AuthenticationSettings();
            //bindowanie obiektów z apisettingsjson do obektu authenitactionSettings - bindowanie (wi¹zaæ)
            //czyli proces w ktorym dane z jsona wi¹¿emy z obiketem authenitactionSettings;
            Configuration.GetSection("Authentication").Bind(authenitactionSettings);

            services.AddSingleton(authenitactionSettings);
            //skonfigurowanie serwisu, sprawdzajacego poprawnoœæ tokenu wys³anego przez klienta w nag³ówku autentykacji
            services.AddAuthentication(option =>
            {
                /*ten schemat bêdzie musia³ zostaæ okreœlony przez klienta w nag³ówku Autentykacji */
                option.DefaultAuthenticateScheme = "Bearer";
                option.DefaultScheme = "Bearer";
                option.DefaultChallengeScheme = "Bearer";

            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                //parametry walidacji po to aby sprawdziæ, czy dany token wys³any przez klienta
                //jest zgodny co wie serwer
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    //okreœlenie wydawcy danego tokenu
                    ValidIssuer = authenitactionSettings.JwtIssuer,
                    //okreslenie jakie podmioty mog¹ u¿ywaæ takiego tokenu 
                    ValidAudience = authenitactionSettings.JwtIssuer,
                    //klucz prywatny wygenrowany na podstawie wartoœci JwtKey, która zapisalismy w appsettings.json
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenitactionSettings.JwtKey)),

                };

            });
            services.AddAuthorization(options =>
            {
                //dodanie w³aœnej polityki sprawdzaj¹cej czy u¿ytkownik ma ustawiona narodowoœæ 
                //potem mo¿emy to wykorzystaæ w kontrolerze po Nationality mo¿emy okresliæ wymagana nardowoœæ jak ni¿ej
                options.AddPolicy("HasNationality", /*s³u¿y do dynamicznego budowania polityki autoryzacji*/ builder => builder.RequireClaim("Nationality"/*"German"*/));
                options.AddPolicy("AtLeast20", builder => builder.AddRequirements(new MinimumAgeRequirement(20)));
                options.AddPolicy("CreatedAtLeast2RentalCars", builder => builder.AddRequirements(new CreatedMultipleRentalCarsRequirement(2)));
                options.AddPolicy("MailIsGmail", builder => builder.AddRequirements(new UsingMailIsGmail("gmail.com")));
            });
            services.AddScoped<IProba, Proba>();
            services.AddScoped<IAuthorizationHandler, UsingMailIsGmailRequirement>();
            services.AddScoped<IAuthorizationHandler, CreatedMultipleRentalCarsRequirementHandler>();
            services.AddScoped<IAuthorizationHandler, MinimumAgeRequirementHandler>();
            services.AddScoped<IAuthorizationHandler, ResourceOperationRequirementHandler>();
            services.AddControllers().AddFluentValidation();
            services.AddDbContext<RentalCarDbContext>();
            services.AddScoped<RentalCarSeeder>();
            services.AddAutoMapper(this.GetType().Assembly);
            services.AddScoped<IRentalCarService, RentalCarService>();
            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ErrorHandlingMiddleware>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddScoped<IValidator<RegisterUserDto/*model jaki walidujemy*/>, RegisterUserDtoValidator/*walidacje modelu*/>();
            services.AddMvcCore().AddAuthorization();
            services.AddScoped<RequestTimeMiddleware>();
            services.AddScoped<IUserContextService, UserContextService>();
            services.AddHttpContextAccessor();
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, RentalCarSeeder seeder)
        {
            seeder.Seed();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseMiddleware<RequestTimeMiddleware>();
            //powiedzenie naszemu api ka¿dy request wysa³ny przez klienta api bêdzie podlega³ autentykacji
            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "RentalCar API");
            });

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
