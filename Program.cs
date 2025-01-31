using HoneyRaesAPI.Models;

//Because we are using the namespace above, we can shorten our lists (not having to use the full name)
// List<HoneyRaesAPI.Models.Customer> customers = new List<HoneyRaesAPI.Models.Customer> {};
// List<HoneyRaesAPI.Models.Employee> employees = new List<HoneyRaesAPI.Models.Employee> {};
// List<HoneyRaesAPI.Models.ServiceTickets> serviceTickets = new List<HoneyRaesAPI.Models.ServiceTickets> {};

List<Customer> customers = new List<Customer> {
    new Customer { Id = 1, Name = "Casey", Address= "123 Funny Street Nashville TN, 33333" },
    new Customer { Id = 2, Name = "Jon", Address= "443 Stinky Road Lexington KY, 41004" },
    new Customer { Id = 3, Name = "Brian", Address= "223 Awesome Circle Lexington KY, 41004" }
};

List<Employee> employees = new List<Employee> {
    new Employee { Id = 1, Name="Noah", Specialty= "C#" },
    new Employee { Id = 2, Name="Odi", Specialty= "React" }
};

List<ServiceTicket> serviceTickets = new List<ServiceTicket> {
    new ServiceTicket { Id = 1, CustomerId = 1, EmployeeId = 1, Description = "BAD", Emergency = true, DateCompleted= DateTime.Now },
    new ServiceTicket { Id = 2, CustomerId = 2, EmployeeId = 2, Description = "GOOD", Emergency = false, DateCompleted= DateTime.Now },
    new ServiceTicket { Id = 3, CustomerId = 3, EmployeeId = 1, Description = "GOOD", Emergency = false, DateCompleted= DateTime.Now },
    new ServiceTicket { Id = 4, CustomerId = 4, Description = "DECENT", Emergency = false, DateCompleted= DateTime.Now },
    new ServiceTicket { Id = 5, CustomerId = 5, Description = "DECENT", Emergency = false },
};


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/servicetickets", () =>
{
    return serviceTickets;
});

app.MapGet("/servicetickets/{id}", (int id) =>
{
    return serviceTickets.FirstOrDefault(st => st.Id == id);
});
// app.MapGet("/weatherforecast", () =>
// {
//     var forecast =  Enumerable.Range(1, 5).Select(index =>
//         new WeatherForecast
//         (
//             DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//             Random.Shared.Next(-20, 55),
//             summaries[Random.Shared.Next(summaries.Length)]
//         ))
//         .ToArray();
//     return forecast;
// })
// .WithName("GetWeatherForecast")
// .WithOpenApi();

app.Run();

// record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
// {
//     public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
// }
