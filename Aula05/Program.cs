using Modelo;
using Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

FillCustomerData();
app.Run();

static void FillCustomerData()
{
    for(int i = 1; i<=10; i++)
    {
        Customer customer = new()
        {
            Id = i, 
            Name = $"Customer {i}",
            HomeAddress = new Address()
            {
                Id = i, 
                AddressType = "Casa",
                City = "Videira",
                Country = "Brasil",
                StateProvince = "SC",
                PostalCode = "89564-494",
                StreetLine1 = "Rua da minha casa",
                StreetLine2 = "Sua casa"
            }

        };
        CustomerData.Customers.Add(customer);
    }
}
