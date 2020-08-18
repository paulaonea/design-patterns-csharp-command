using System;
using System.Diagnostics.CodeAnalysis;
using ShoppingCart.Business.Commands;
using ShoppingCart.Business.Repositories;

namespace ShoppingCart
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var shoppingCartRepository = new ShoppingCartRepository();
            var productRepository = new ProductRepository();

            var product = productRepository.FindBy("SM7B");
            
            var addToCartCommand = new AddToCartCommand(shoppingCartRepository, productRepository, product);
            var increaseQuantityCommand = new ChangeQuantityCommand(ChangeQuantityCommand.Operation.Increase, 
                                                            productRepository, 
                                                            shoppingCartRepository, 
                                                            product);
            var manager = new CommandManager();
            manager.Invoke(addToCartCommand);
            manager.Invoke(increaseQuantityCommand);
            manager.Invoke(increaseQuantityCommand);
            manager.Invoke(increaseQuantityCommand);
            manager.Invoke(increaseQuantityCommand);

            PrintCart(shoppingCartRepository);
            
            manager.Undo();
            PrintCart(shoppingCartRepository);

        }

        static void PrintCart(ShoppingCartRepository shoppingCartRepository)
        {
            var totalPrice = 0m;
            foreach (var lineItem in shoppingCartRepository.LineItems)
            {
                var price = lineItem.Value.Product.Price * lineItem.Value.Quantity;

                Console.WriteLine($"{lineItem.Key} " +
                                  $"${lineItem.Value.Product.Price} x {lineItem.Value.Quantity} = ${price}");

                totalPrice += price;
            }

            Console.WriteLine($"Total price:\t${totalPrice}");
        }
    }
}