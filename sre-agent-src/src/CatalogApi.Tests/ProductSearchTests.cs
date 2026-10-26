using CatalogApi.Models;
using CatalogApi.Services;
using Xunit;

public class ProductSearchTests
{
    private static List<Product> Sample() => new()
    {
        new Product { Name = "A", Stock = 0 },
        new Product { Name = "B", Stock = 5 },
        new Product { Name = "C", Stock = 0 },
        new Product { Name = "D", Stock = 1 },
    };

    [Fact]
    public void OutOfStockItemsAreListedLast()
    {
        var names = ProductSearch.Apply(Sample(), false).Select(p => p.Name);
        Assert.Equal(new[] { "B", "D", "A", "C" }, names);
    }

    [Fact]
    public void HideOutOfStockRemovesUnavailableItems()
    {
        var names = ProductSearch.Apply(Sample(), true).Select(p => p.Name);
        Assert.Equal(new[] { "B", "D" }, names);
    }

    [Fact]
    public void IsOutOfStockReflectsStock()
    {
        Assert.True(new Product { Stock = 0 }.IsOutOfStock);
        Assert.False(new Product { Stock = 1 }.IsOutOfStock);
    }
}
