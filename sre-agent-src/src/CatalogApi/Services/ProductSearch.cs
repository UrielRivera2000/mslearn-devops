using CatalogApi.Models;

namespace CatalogApi.Services;

public static class ProductSearch
{
    public static IEnumerable<Product> Apply(IEnumerable<Product> products, bool hideOutOfStock)
    {
        if (hideOutOfStock)
            products = products.Where(p => !p.IsOutOfStock);

        // OrderBy is stable, so the original order is kept within each group
        return products.OrderBy(p => p.IsOutOfStock).ToList();
    }
}
