using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using dotnet_store.Models;

namespace dotnet_store.Controllers;

public class HomeController : Controller
{
    private readonly DataContext _context;
    public HomeController(DataContext context)
    {
        _context = context;
    }

    public ActionResult Index()
    {
        var urunler = _context.Urunler.Where(urun => urun.Aktif && urun.Anasayfa).ToList();
        ViewData["Kategori"]=_context.Kategori.ToList();
        return View(urunler);
    }
}
