using System.Collections.Generic;
using ShoppingCart.Business.Models;
using ShoppingCart.Business.Repositories;

namespace ShoppingCart
{
    public class ShoppingCartRepository : IShoppingCartRepository    
    {
        public Dictionary<string, (Product Product, int Quantity)> LineItems =
            new Dictionary<string, (Product Product, int Quantity)>();
        
        public IEnumerable<(Product Product, int Quantity)> All()
        {
            return LineItems.Values;
        }
        
        public (Product Product, int Quantity) Get(string articleId)
        {
            return LineItems.ContainsKey(articleId)
                ? LineItems[articleId]
                : (default, default);
        }
        
        public void Add(Product product)
        {
            if (LineItems.ContainsKey(product.ArticleId))
            {
                IncreaseQuantity(product.ArticleId);
            }
            else
            {
                LineItems[product.ArticleId] = (product, 1);
            }
        }
        
        public void DecreaseQuantity(string articleId)
        {
            if (LineItems.ContainsKey(articleId))
            {
                var (product, quantity) = LineItems[articleId];
                if (quantity == 1)
                {
                    LineItems.Remove(articleId);
                }
                else
                {
                    LineItems[articleId] = (product, quantity - 1);
                }
            }
            else throw new KeyNotFoundException($"article with id {articleId} is not in the cart.");
        }

        public void RemoveAll(string articleId)
        {
            LineItems.Remove(articleId);
        }

        public void IncreaseQuantity(string articleId)
        {
            if (LineItems.ContainsKey(articleId))
            {
                var (product, quantity) = LineItems[articleId];
                LineItems[articleId] = (product, quantity + 1);
            }
            else throw new KeyNotFoundException($"product with id {articleId} not in the cart, please add.");
        }
    }
}