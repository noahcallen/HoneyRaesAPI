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
// ================================================================================================================ GET
app.MapGet("/serviceTickets", () =>
{
    return serviceTickets;
});

app.MapGet("/serviceTickets/{id}", (int id) =>
{
    ServiceTicket serviceTicket = serviceTickets.FirstOrDefault(st => st.Id == id);
    if (serviceTicket == null)
    {
        return Results.NotFound();
    }
    serviceTicket.Employee = employees.FirstOrDefault(e => e.Id == serviceTicket.EmployeeId);
    return Results.Ok(serviceTicket);
});

app.MapGet("/customers", () =>
{
    return customers;
});

app.MapGet("/customers/{id}", (int id) =>
{
    Customer customer = customers.FirstOrDefault(c => c.Id == id);
    if (customer == null)
    {
        return Results.NotFound();
    }
    customer.ServiceTickets = serviceTickets.Where(st => st.EmployeeId == id).ToList();
    return Results.Ok(customer);
});


app.MapGet("/employees", () =>
{
    return employees;
});

app.MapGet("/employees/{id}", (int id) =>
{
    Employee employee = employees.FirstOrDefault(e => e.Id == id);
    if (employee == null)
    {
        return Results.NotFound();
    }
    employee.ServiceTickets = serviceTickets.Where(st => st.EmployeeId == id).ToList();
    return Results.Ok(employee);
});

// ================================================================================================================ POST

app.MapPost("/servicetickets", (ServiceTicket serviceTicket) =>
{
    // creates a new id (When we get to it later, our SQL database will do this for us like JSON Server did!)
    serviceTicket.Id = serviceTickets.Max(st => st.Id) + 1;
    serviceTickets.Add(serviceTicket);
    return serviceTicket;
});

// ================================================================================================================ POST

app.MapDelete("/serviceTickets/{id}", (int id) =>
{
    var ticket = serviceTickets.FirstOrDefault(st => st.Id == id);

    if (ticket == null)
    {
        return Results.NotFound();
    }
    serviceTickets.Remove(ticket);
    return Results.Ok(ticket);
});

app.Run();
