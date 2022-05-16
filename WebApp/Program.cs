using WebApp.Data.Services;
using WebApp.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MoodContext>();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddTransient<PublisherService>();
builder.Services.AddTransient<BookService>();
builder.Services.AddTransient<AuthorService>();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
 options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthorization();
app.MapControllers();
app.UseCors(policy => policy
 .AllowAnyOrigin()
 .AllowAnyMethod()
 .AllowAnyHeader());
app.Run();