using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Business.Models;

namespace ShoppingCart.Business.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public Dictionary<string, (Product Product, int Stock)> products { get; }

        public ProductRepository()
        {
            products = new Dictionary<string, (Product Product, int Stock)>();
            Add(new Product("EOSR1", "Canon EOS R", 1099), 2);
            Add(new Product("EOS70D", "Canon EOS 70D", 699), 1);
            Add(new Product("ATOMOSNV", "Atomos Ninja V", 799), 0);
            Add(new Product("SM7B", "Shure SM7B", 399), 5);
        }

        private void Add(Product product, int stock)
        {
            products[product.ArticleId] = (product, stock);
        }

        public Product FindBy(string articleId)
        {
            return products.ContainsKey(articleId)
                ? products[articleId].Product
                : null;
        }

        public int GetStockFor(string articleId)
        {
            return products.ContainsKey(articleId)
                ? products[articleId].Stock
                : 0;
        }

        public IEnumerable<Product> All()
        {
            return products.Select(x => x.Value.Product);
        }

        public void DecreaseStockBy(string articleId, int amount)
        {
            if (!products.ContainsKey(articleId))
            {
                return;
            }

            products[articleId] = (products[articleId].Product, products[articleId].Stock - amount);
        }

        public void IncreaseStockBy(string articleId, int amount)
        {
            if (products.ContainsKey(articleId))
            {
                return;
            }

            products[articleId] = (products[articleId].Product, products[articleId].Stock + amount);
        }
    }
}