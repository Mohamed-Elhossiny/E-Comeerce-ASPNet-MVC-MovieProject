using MovieProject.Models;

namespace MovieProject.Repository.OrderRepository
{
	public interface IOrderRepository
	{
		Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress);
		Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId,string userRole);
	}
}
