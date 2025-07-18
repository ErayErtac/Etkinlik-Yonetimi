using dotnet_store.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnet_store.Controllers;

public class KategoriController : Controller
{
    private readonly DataContext _context;
    public KategoriController(DataContext context)
    {
        _context = context;
    }

    public ActionResult Index()
    {
        var kategoriler = _context.Kategori.Select(i => new KategoriGetModel
        {
            Id = i.Id,
            KategoriAdi = i.KategoriAdi,
            Url = i.Url,
            UrunSayisi = i.Uruns.Count
        }).ToList();
        return View(kategoriler);
    }

    [HttpGet]
    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(KategoriGetModel model)
    {
        if (ModelState.IsValid)
        {
            var entity = new Kategori
            {
                KategoriAdi = model.KategoriAdi,
                Url = model.Url
            };

            _context.Kategori.Add(entity);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        return View();
    }

    public ActionResult Edit(int id)
    {
        var entity = _context.Kategori.Select(i => new KategoriEditModel
        {
            Id = i.Id,
            KategoriAdi = i.KategoriAdi,
            Url = i.Url
        }).FirstOrDefault(i => i.Id == id);

        return View(entity);
    }

    [HttpPost]
    public ActionResult Edit(int id, KategoriEditModel model)
    {
        if (id != model.Id)
        {
            return RedirectToAction("Index");
        }

        var entity = _context.Kategori.FirstOrDefault(i => i.Id == model.Id);

        if (entity != null)
        {
            entity.KategoriAdi = model.KategoriAdi;
            entity.Url = model.Url;

            _context.SaveChanges();

            TempData["Mesaj"] = $"{entity.KategoriAdi} kategorisi güncellendi.";

            return RedirectToAction("Index");
        }

        return View(model);
    }

    public ActionResult Delete(int? id)
    {
        if (id == null)
        {
            return RedirectToAction("Index");
        }

        var entity = _context.Kategori.FirstOrDefault(i => i.Id == id);

        if (entity != null)
        {
            return View(entity);
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteConfirm(int? id)
    {
        if (id == null)
        {
            return RedirectToAction("Index");
        }

        var entity = _context.Kategori.FirstOrDefault(i => i.Id == id);

        if (entity != null)
        {
            _context.Kategori.Remove(entity);
            _context.SaveChanges();

            TempData["Mesaj"] = $"{entity.KategoriAdi} kategorisi silindi.";
        }
        return RedirectToAction("Index");
    }

}