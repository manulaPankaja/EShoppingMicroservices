using Marten.Schema;

namespace CatalogAPI.Data
{
    public class CatalogInitialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession();

            if (await session.Query<Product>().AnyAsync())
                return;

            //Marten UPSERT will carter for existing records
            session.Store<Product>(GetPreconfiguredProducts());
            await session.SaveChangesAsync();
        }

        public static IEnumerable<Product> GetPreconfiguredProducts() => new List<Product>()
        {
            new Product()
            {
                Id = new Guid(),
                Name = "IPhone X",
                Description = "Description",
                ImageFile = "product-1.png",
                Price = 942.00M,
                Category = new List<string> {"mobile phone"}
            }
        };
    }
}
