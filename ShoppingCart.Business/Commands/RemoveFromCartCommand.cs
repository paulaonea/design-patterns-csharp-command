using ShoppingCart.Business.Models;
using ShoppingCart.Business.Repositories;

namespace ShoppingCart.Business.Commands
{
    public class RemoveFromCartCommand : ICommand
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IProductRepository _productRepository;
        private readonly Product _product;

        public RemoveFromCartCommand(IShoppingCartRepository shoppingCartRepository, IProductRepository productRepository, Product product)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _productRepository = productRepository;
            _product = product;
        }
        public bool CanExecute()
        {
            if (_product == null) return false;
            {
                return _shoppingCartRepository.Get(_product.ArticleId).Quantity > 0;
            }
        }

        public void Execute()
        {
            if (_product == null) return;
            {
                var lineItem = _shoppingCartRepository.Get(_product.ArticleId);
                _shoppingCartRepository.RemoveAll(_product.ArticleId);
                _productRepository.IncreaseStockBy(_product.ArticleId, lineItem.Quantity);
            }
        }

        public void Undo()
        {
            throw new System.NotImplementedException();
        }
    }
}