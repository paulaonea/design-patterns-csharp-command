using System;
using ShoppingCart.Business.Models;
using ShoppingCart.Business.Repositories;

namespace ShoppingCart.Business.Commands
{
    public class AddToCartCommand : ICommand
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IProductRepository _productRepository;
        private readonly Product _product;
        
        public AddToCartCommand(IShoppingCartRepository shoppingCartRepository,
            IProductRepository productRepository,
            Product product)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _productRepository = productRepository;
            _product = product;
        }
        
        public bool CanExecute()
        {
            if (_product == null) return false;
            return _productRepository.GetStockFor(_product.ArticleId) > 0;
        }

        public void Execute()
        {
            if (_product == null) return;
            _shoppingCartRepository.Add(_product);
            _productRepository.DecreaseStockBy(_product.ArticleId, 1);
        }

        public void Undo()
        {
            if (_product == null) return;
            var lineItem = _shoppingCartRepository.Get(_product.ArticleId);
            _shoppingCartRepository.RemoveAll(_product.ArticleId);
            _productRepository.IncreaseStockBy(_product.ArticleId, lineItem.Quantity);
        }
    }
}