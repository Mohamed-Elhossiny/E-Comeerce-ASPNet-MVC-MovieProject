using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieProject.Data.Cart;
using MovieProject.Repository.MoviesRepository;
using MovieProject.Repository.OrderRepository;
using MovieProject.ViewModel.CartViewModel;
using NuGet.Versioning;
using System.Security.Claims;

namespace MovieProject.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IMoviesRepository moviesRepository;
        private readonly ShoppingCart shoppingCart;
        private readonly IOrderRepository orderRepository;

        public OrdersController(IMoviesRepository moviesRepository, ShoppingCart shoppingCart,IOrderRepository orderRepository)
        {
            this.moviesRepository = moviesRepository;
            this.shoppingCart = shoppingCart;
            this.orderRepository = orderRepository;
        }

        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            string userRole = User.FindFirstValue(ClaimTypes.Role);
            var orders =await orderRepository.GetOrdersByUserIdAndRoleAsync(userId,userRole);
            return View(orders);
        }
        public IActionResult ShoppingCart()
        {
            var items = shoppingCart.GetShoppingCartItems();
            var response = new ShoppingCartViewModel();
            shoppingCart.ShoppingCartItems = items;
            response.ShoppingCart = shoppingCart;
            response.ShoppingCartTotal = shoppingCart.GetShoppingCartTotal();
            return View(response);
        }
        [Authorize]
        public async Task<IActionResult> AddItemToShoppingCart(int id)
        {
            var item = await moviesRepository.GetMovieByIdAsync(id);
            if (item != null)
            {
                shoppingCart.AddItemToCart(item);
            }
            return RedirectToAction("ShoppingCart");
        }

        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
        {
            var item = await moviesRepository.GetMovieByIdAsync(id);
            if (item != null)
            {
                shoppingCart.RemoveItemFromCart(item);
            }
            return RedirectToAction("ShoppingCart");
        }

        public async Task<IActionResult> CompleteOrder()
        {
            var items = shoppingCart.GetShoppingCartItems();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userEmailAddress = User.FindFirstValue(ClaimTypes.Email);

            await orderRepository.StoreOrderAsync(items, userId, userEmailAddress);
            await shoppingCart.ClearShoppingCartAsync();
            return View("OrderCompleted");
        }

    }
}
