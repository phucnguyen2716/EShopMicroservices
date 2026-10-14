using System.Collections;
using Marten.Schema;

namespace Catalog.API.Data;
public class InitialCatalogData: IInitialData
{
    private readonly object[] _initialData;

    public InitialCatalogData(params object[] initialData)
    {
        _initialData = initialData;
    }

    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        await using var session = store.LightweightSession();
       
        session.Store<Product>(GetConfiguratedProducts());
        await session.SaveChangesAsync();
    }


    public static IEnumerable<Product> GetConfiguratedProducts() => new List<Product>
    {
            new Product
        {
            Id = Guid.NewGuid(),
            Name = "iPhone 18 Pro Max",
            Category = new List<string> { "Điện thoại", "Apple", "iPhone 18" },
            Description = "Điện thoại Apple iPhone 18 Pro Max, phiên bản cao cấp. Giá dùng để test API, chưa xác minh giá bán thực tế.",
            ImageFile = "iphone-18-pro-max.jpg",
            Price = 40000000m
        },
        new Product
        {
            Id = Guid.NewGuid(),
            Name = "iPhone 17 256GB",
            Category = new List<string> { "Điện thoại", "Apple", "iPhone 17" },
            Description = "Màn hình Super Retina XDR 6.3 inch, chip A19, bộ nhớ 256GB, camera sau 48MP.",
            ImageFile = "iphone-17-256gb.jpg",
            Price = 24590000m
        },
        new Product
        {
            Id = Guid.NewGuid(),
            Name = "iPhone 16 128GB",
            Category = new List<string> { "Điện thoại", "Apple", "iPhone 16" },
            Description = "Màn hình Super Retina XDR 6.1 inch, chip A18, bộ nhớ 128GB, camera sau 48MP.",
            ImageFile = "iphone-16-128gb.jpg",
            Price = 20990000m
        },
        new Product
        {
            Id = Guid.NewGuid(),
            Name = "iPhone 15 128GB",
            Category = new List<string> { "Điện thoại", "Apple", "iPhone 15" },
            Description = "Màn hình Super Retina XDR 6.1 inch, chip A16 Bionic, bộ nhớ 128GB, camera chính 48MP.",
            ImageFile = "iphone-15-128gb.jpg",
            Price = 18990000m
        },
        new Product
        {
            Id = Guid.NewGuid(),
            Name = "iPhone 14 128GB",
            Category = new List<string> { "Điện thoại", "Apple", "iPhone 14" },
            Description = "Màn hình Super Retina XDR 6.1 inch, chip A15 Bionic, bộ nhớ 128GB, camera kép 12MP.",
            ImageFile = "iphone-14-128gb.jpg",
            Price = 13990000m
        },
        new Product
        {
            Id = Guid.NewGuid(),
            Name = "iPhone 13 128GB",
            Category = new List<string> { "Điện thoại", "Apple", "iPhone 13" },
            Description = "Màn hình Super Retina XDR 6.1 inch, chip A15 Bionic, bộ nhớ 128GB, hệ thống camera kép.",
            ImageFile = "iphone-13-128gb.jpg",
            Price = 10990000m
        },
        new Product
        {
            Id = Guid.NewGuid(),
            Name = "iPhone 12 64GB",
            Category = new List<string> { "Điện thoại", "Apple", "iPhone 12" },
            Description = "Màn hình OLED 6.1 inch, chip A14 Bionic, bộ nhớ 64GB, hỗ trợ kết nối 5G.",
            ImageFile = "iphone-12-64gb.jpg",
            Price = 7990000m
        },
        new Product
        {
            Id = Guid.NewGuid(),
            Name = "iPhone 11 64GB",
            Category = new List<string> { "Điện thoại", "Apple", "iPhone 11" },
            Description = "Màn hình Liquid Retina HD 6.1 inch, chip A13 Bionic, bộ nhớ 64GB, camera kép.",
            ImageFile = "iphone-11-64gb.jpg",
            Price = 5990000m
        },
        new Product
        {
            Id = Guid.NewGuid(),
            Name = "iPhone X 64GB",
            Category = new List<string> { "Điện thoại", "Apple", "iPhone X" },
            Description = "Màn hình OLED Super Retina 5.8 inch, chip A11 Bionic, bộ nhớ 64GB, camera kép.",
            ImageFile = "iphone-x-64gb.jpg",
            Price = 3990000m
        },
        new Product
        {
            Id = Guid.NewGuid(),
            Name = "iPhone XR 64GB",
            Category = new List<string> { "Điện thoại", "Apple", "iPhone XR" },
            Description = "Màn hình Liquid Retina HD 6.1 inch, chip A12 Bionic, bộ nhớ 64GB, camera sau 12MP.",
            ImageFile = "iphone-xr-64gb.jpg",
            Price = 3490000m
        }

    };
}