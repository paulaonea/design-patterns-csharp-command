using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Business.Models;
using ShoppingCart.Business.Repositories;

namespace ShoppingCart.Business.Commands
{
    public class RemoveAllFromCart : ICommand
    {
        private readonly IProductRepository _productRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly Product _product;

        public RemoveAllFromCart(IProductRepository productRepository, 
            IShoppingCartRepository shoppingCartRepository, 
            Product product)
        {
            _productRepository = productRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _product = product;
        }
        public bool CanExecute()
        {
            return _shoppingCartRepository.All().Any();
        }

        public void Execute()
        {
            var cartItems = _shoppingCartRepository.All().ToList();
            foreach (var (product, quantity) in cartItems)
            {
                _shoppingCartRepository.RemoveAll(product.ArticleId);
                _productRepository.IncreaseStockBy(product.ArticleId, quantity);
            }
        }

        public void Undo()
        {
            throw new System.NotImplementedException();
        }
    }
}