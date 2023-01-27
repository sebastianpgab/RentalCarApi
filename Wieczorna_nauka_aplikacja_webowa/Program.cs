using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NLog.Web;
using System.Reflection;
using System.Text;
using Wieczorna_nauka_aplikacja_webowa;
using Wieczorna_nauka_aplikacja_webowa.Authorization;
using Wieczorna_nauka_aplikacja_webowa.Controllers.Services;
using Wieczorna_nauka_aplikacja_webowa.Entities;
using Wieczorna_nauka_aplikacja_webowa.Middleware;
using Wieczorna_nauka_aplikacja_webowa.Models;
using Wieczorna_nauka_aplikacja_webowa.Models.Validators;
using Wieczorna_nauka_aplikacja_webowa.Services;
using Wieczorna_nauka_aplikacja_webowa.Services.Services;


var builder = WebApplication.CreateBuilder();

builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Host.UseNLog();

//configure service


    var authenitactionSettings = new AuthenticationSettings();
    //bindowanie obiektów z apisettingsjson do obektu authenitactionSettings - bindowanie (wi¹zaæ)
    //czyli proces w ktorym dane z jsona wi¹¿emy z obiketem authenitactionSettings;
    builder.Configuration.GetSection("Authentication").Bind(authenitactionSettings);

    builder.Services.AddSingleton(authenitactionSettings);
//skonfigurowanie serwisu, sprawdzajacego poprawnoœæ tokenu wys³anego przez klienta w nag³ówku autentykacji
    builder.Services.AddAuthentication(option =>
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
    builder.Services.AddAuthorization(options =>
    {
        //dodanie w³aœnej polityki sprawdzaj¹cej czy u¿ytkownik ma ustawiona narodowoœæ 
        //potem mo¿emy to wykorzystaæ w kontrolerze po Nationality mo¿emy okresliæ wymagana nardowoœæ jak ni¿ej
        options.AddPolicy("HasNationality", /*s³u¿y do dynamicznego budowania polityki autoryzacji*/ builder => builder.RequireClaim("Nationality"/*"German"*/));
        options.AddPolicy("AtLeast20", builder => builder.AddRequirements(new MinimumAgeRequirement(20)));
        options.AddPolicy("CreatedAtLeast2RentalCars", builder => builder.AddRequirements(new CreatedMultipleRentalCarsRequirement(2)));
        options.AddPolicy("MailIsGmail", builder => builder.AddRequirements(new UsingMailIsGmail("gmail.com")));
    });
    builder.Services.AddScoped<IAuthorizationHandler, UsingMailIsGmailRequirement>();
    builder.Services.AddScoped<IAuthorizationHandler, CreatedMultipleRentalCarsRequirementHandler>();
    builder.Services.AddScoped<IAuthorizationHandler, MinimumAgeRequirementHandler>();
    builder.Services.AddScoped<IAuthorizationHandler, ResourceOperationRequirementHandler>();
    builder.Services.AddControllers().AddFluentValidation();

    builder.Services.AddScoped<RentalCarSeeder>();
    builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
    builder.Services.AddScoped<IRentalCarService, RentalCarService>();
    builder.Services.AddScoped<IVehicleService, VehicleService>();
    builder.Services.AddScoped<IAccountService, AccountService>();
    builder.Services.AddScoped<IAddressService, AddressService>();
    builder.Services.AddScoped<ErrorHandlingMiddleware>();
    builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
    builder.Services.AddScoped<IValidator<RegisterUserDto/*model jaki walidujemy*/>, RegisterUserDtoValidator/*walidacje modelu*/>();
    builder.Services.AddScoped<IValidator<RentalCarQuery/*model jaki walidujemy*/>, RentalCarValidator/*walidacje modelu*/>();
    builder.Services.AddMvcCore().AddAuthorization();
    builder.Services.AddScoped<RequestTimeMiddleware>();
    builder.Services.AddScoped<IUserContextService, UserContextService>();
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddSwaggerGen();
    builder.Services.AddCors(options =>
        {
            options.AddPolicy("FrontEndClient", policyBuilder =>
            policyBuilder.AllowAnyMethod().AllowAnyHeader().WithOrigins(builder.Configuration["AllowedOrignis"]));
        });
    builder.Services.AddDbContext<RentalCarDbContext>
          (options => options.UseSqlServer(builder.Configuration.GetConnectionString("RentalCarDbConnection")));

//configure

var app = builder.Build();

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<RentalCarSeeder>();

app.UseResponseCaching();
app.UseStaticFiles();
app.UseCors("FrontEndClient");
seeder.Seed();
if (app.Environment.IsDevelopment())
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

app.Run();