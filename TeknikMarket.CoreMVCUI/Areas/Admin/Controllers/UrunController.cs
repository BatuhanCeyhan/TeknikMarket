﻿using AutoMapper;
using Core.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Cryptography;
using TeknikMarket.Business.Abstract;
using TeknikMarket.CoreMVCUI.Areas.Admin.Filter;
using TeknikMarket.Model.Entity;
using TeknikMarket.Model.ViewModel.Areas.Admin.Urunlers;

namespace TeknikMarket.CoreMVCUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UrunController : Controller
    {
        IKategoriBS kategoriBS;
        IUrunFiyatBS urunFiyatBS;
        IUrunFotografBS urunFotografBS;
        IUrunBS urunBS;
        IMapper mapper;
        ISessionManager session;

        public UrunController(IKategoriBS kategoriBS, IUrunFiyatBS urunFiyatBS, IUrunFotografBS urunFotografBS, IUrunBS urunBS, IMapper mapper, ISessionManager session)
        {
            this.kategoriBS = kategoriBS;
            this.urunFiyatBS = urunFiyatBS;
            this.urunFotografBS = urunFotografBS;
            this.urunBS = urunBS;
            this.mapper = mapper;
            this.session = session;
        }

        public IActionResult Add()
        {
            UrunAddViewModel model = new UrunAddViewModel()
            { 
                KategoriListesi = kategoriBS.GetAll().Select(x=> new SelectListItem { Text= x.KategoriAdiGorunumu , Value = x.Id.ToString() }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(UrunAddViewModel model)
        {
            Urun urun = mapper.Map<Urun>(model);
            Urun added = urunBS.Insert(urun);

            foreach (UrunFiyat item in model.UrunFiyatlari)
            {
                item.UrunId = added.Id;
                urunFiyatBS.Insert(item);
            }

            return Json(new { result = true, id = added.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddProductPhoto(IFormCollection data)
        {
            int urunid = int.Parse(data["UrunId"]);

            List<IFormFile> files = data.Files.ToList();
            int hata = 0;
            string hatamesaj = "";

            foreach (IFormFile item in files)
            {
                if (!item.ContentType.Contains("image/"))
                {
                    hata++;
                    hatamesaj += item.FileName + " dosyası resim değil" + Environment.NewLine;
                }
                else
                {
                    //Burası resimse
                    if (item.Length > 1048576) // 1MBdan büyükse (byte cisinden)
                    {
                        hata++;
                        hatamesaj += item.FileName + " dosyasi 1MB dan daha büyük" + Environment.NewLine;
                    }
                    else
                    {
                        string extension = Path.GetExtension(item.FileName);
                        string filename = RandomValueGenerator.UniqueFileName(extension);
                        string uploadpath = Directory.GetCurrentDirectory() + "/wwwroot/image/products/" + filename;
                        using(FileStream fs = new FileStream(uploadpath, FileMode.Create))
                        {
                            item.CopyTo(fs);
                        }
                        UrunFotograf urunFotograf = new UrunFotograf()
                        {
                            UrunId = urunid,
                            FotografAdresi = "/image/product/" + filename
                        };
                        hatamesaj += item.FileName + " dosyası başarıyla eklendi" + Environment.NewLine;
                        urunFotografBS.Insert(urunFotograf);
                    }
                }
            }
            if (hata>0)
            {
                return Json(new { result = false, mesaj = hatamesaj });
            }
            return Json(new { result = true, mesaj = "Ürün Başarıyla Eklendi" });
        }
    }
}
