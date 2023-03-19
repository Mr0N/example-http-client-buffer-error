var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

Task.Run(async() =>
{
    try
    {
        await Task.Delay(4000);
        using var client = new HttpClient();
        var read = await (await client.GetAsync("http://localhost:5256/File"))
            .Content
            .ReadAsStreamAsync();
        using var file = new FileStream(Path.GetTempFileName(), FileMode.Create,
            FileAccess.ReadWrite,
            FileShare.None, 4000,FileOptions.DeleteOnClose);
        read.CopyTo(file);
    }
    catch(Exception ex) {
        Console.WriteLine(ex.ToString());
    }
});
app.Run();
