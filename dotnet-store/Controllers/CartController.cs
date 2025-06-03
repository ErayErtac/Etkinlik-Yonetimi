using System.Threading.Tasks;
using dotnet_store.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnet_store.Controllers;

[Authorize]
public class CartController : Controller
{
    private readonly DataContext _context;
    public CartController(DataContext context)
    {
        _context = context;
    }

    public async Task<ActionResult> Index()
    {
        var cart = await GetCart();
        return View(cart);
    }

    [HttpPost]
    public async Task<ActionResult> AddToCart(int urunId, int miktar = 1)
    {
        var cart = await GetCart();

        var item = cart.CartItems.Where(i => i.UrunId == urunId).FirstOrDefault();

        if (item != null)
        {
            item.Miktar += 1;
        }
        else
        {
            cart.CartItems.Add(new CartItem
            {
                UrunId = urunId,
                Miktar = miktar
            });
        }

        await _context.SaveChangesAsync();

        return RedirectToAction("Index", "Cart");
    }

    [HttpPost]
    public async Task<ActionResult> RemoveItem(int cartItemId)
    {
        var cart = await GetCart();

        var item = cart.CartItems.Where(i => i.CartItemId == cartItemId).FirstOrDefault();

        if (item != null)
        {
            cart.CartItems.Remove(item);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction("Index", "Cart");
    }


    private async Task<Cart> GetCart()
    {
        var customerId = User.Identity?.Name;

        var cart = await _context.Carts
                            .Include(i => i.CartItems)
                            .ThenInclude(i => i.Urun)
                            .Where(i => i.CustomerId == customerId)
                            .FirstOrDefaultAsync();

        if (cart == null)
        {
            cart = new Cart { CustomerId = customerId! };
            _context.Carts.Add(cart);           // change tracking
            // await _context.SaveChangesAsync();
        }

        return cart;
    }
}