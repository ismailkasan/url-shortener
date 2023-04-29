using FluentValidation;
using FluentValidation.AspNetCore;
using LiteDB;
using URLShortener.Common;
using URLShortener.Data;
using URLShortener.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add constroller and Filters.
#pragma warning disable CS0618 // Type or member is obsolete
builder.Services.AddControllers(opt =>
            {
                opt.Filters.Add(typeof(ValidatorActionFilter), 1);
            }).AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining(typeof(Program))).ConfigureApiBehaviorOptions(opt =>
             {
                 opt.SuppressModelStateInvalidFilter = true;
             }); ;
#pragma warning restore CS0618 // Type or member is obsolete

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add LiteDb service
builder.Services.AddSingleton<ILiteDatabase, LiteDatabase>(_ => new LiteDatabase("url-shortener.db"));

// Add Fluent Validations 
builder.Services.AddTransient<IValidator<UrlRequestDto>, UrlRequestDtoValidator>();

// Add services to the container.
builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IUrlService, UrlService>();
builder.Services.AddScoped<IUrlRepository<UrlModel>, UrlRepository>();
builder.Services.AddScoped<IBaseRepository<Log>, LogRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

// Add custom middlewares
app.ConfigureCustomRedirectionMiddleware();
app.ConfigureCustomExceptionMiddleware();

app.Run();
