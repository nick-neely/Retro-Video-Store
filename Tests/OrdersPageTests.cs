using Bunit;
using Microsoft.EntityFrameworkCore;
using RetroVideoStore.Components.Pages;
using RetroVideoStore.Data;
using RetroVideoStore.Models;
using RetroVideoStore.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Xunit;

public class OrdersPageTests : TestContext
{
    private readonly List<Order> _testOrders;

    public OrdersPageTests()
    {
        _testOrders = new List<Order>
        {
            new Order { OrderID = Guid.NewGuid(), CustomerName = "John Doe", CustomerEmail = "johndoe@mail.com", OrderDate = DateTime.Now.AddDays(-3), TotalPrice = 100 },
            new Order { OrderID = Guid.NewGuid(), CustomerName = "Jane Smith", CustomerEmail = "janesmith@mail.com", OrderDate = DateTime.Now.AddDays(-1), TotalPrice = 200 },
            new Order { OrderID = Guid.NewGuid(), CustomerName = "Alice Johnson", CustomerEmail = "alicejohnson@mail.com", OrderDate = DateTime.Now.AddDays(-2), TotalPrice = 150 }
        };
    }

    private void ResetDatabase()
    {
        Services.RemoveAll<OrderService>();
        Services.AddSingleton<OrderService>(new OrderServiceMock(_testOrders));
    }

    [Fact]
    public void OrdersPage_RendersCorrectly()
    {
        ResetDatabase();
        var cut = RenderComponent<Orders>();

        var tableRows = cut.FindAll("tbody tr");

        Assert.Equal(3, tableRows.Count);
    }

    [Fact]
    public void OrdersPage_SearchFunctionality_Works()
    {
        ResetDatabase();
        var cut = RenderComponent<Orders>();

        cut.Find("input").Input("John Doe");
        var tableRows = cut.FindAll("tbody tr");

        Assert.Single(tableRows);
        Assert.Contains("John Doe", tableRows[0].TextContent);
    }

    [Fact]
    public void OrdersPage_SortByOrderDate_Works()
    {
        ResetDatabase();
        var cut = RenderComponent<Orders>();

        cut.Find("select[aria-label='Sort by order date']").Change("desc");
        var tableRows = cut.FindAll("tbody tr");

        Assert.Equal("Jane Smith", tableRows[0].QuerySelector("td:nth-child(2)").TextContent);
        Assert.Equal("Alice Johnson", tableRows[1].QuerySelector("td:nth-child(2)").TextContent);
        Assert.Equal("John Doe", tableRows[2].QuerySelector("td:nth-child(2)").TextContent);
    }
    
    [Fact]
    public void OrdersPage_SortByOrderDate_Ascending_Works()
    {
        ResetDatabase();
        var cut = RenderComponent<Orders>();

        cut.Find("select[aria-label='Sort by order date']").Change("asc");
        var tableRows = cut.FindAll("tbody tr");

        Assert.Equal("John Doe", tableRows[0].QuerySelector("td:nth-child(2)").TextContent);
        Assert.Equal("Alice Johnson", tableRows[1].QuerySelector("td:nth-child(2)").TextContent);
        Assert.Equal("Jane Smith", tableRows[2].QuerySelector("td:nth-child(2)").TextContent);
    }

    [Fact]
    public void OrdersPage_SortByTotalPrice_Works()
    {
        ResetDatabase();
        var cut = RenderComponent<Orders>();

        cut.Find("select[aria-label='Sort by total price']").Change("asc");
        var tableRows = cut.FindAll("tbody tr");

        Assert.Equal("John Doe", tableRows[0].QuerySelector("td:nth-child(2)").TextContent);
        Assert.Equal("Alice Johnson", tableRows[1].QuerySelector("td:nth-child(2)").TextContent);
        Assert.Equal("Jane Smith", tableRows[2].QuerySelector("td:nth-child(2)").TextContent);
    }
}

// Mock OrderService for testing
public class OrderServiceMock : OrderService
{
    private readonly List<Order> _orders;

    public OrderServiceMock(List<Order> orders) : base(CreateInMemoryDbContext(orders))
    {
        _orders = orders;
    }

    private static AppDbContext CreateInMemoryDbContext(List<Order> orders)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique database name for each test
            .Options;
        var context = new AppDbContext(options);

        // Seed
        context.Database.EnsureCreated();
        context.Orders.AddRange(orders);
        context.SaveChanges();

        return context;
    }
}