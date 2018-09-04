using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KendoUIExample.Models;
using Cosmonaut;
using Cosmonaut.Extensions;

namespace KendoUIExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICosmosStore<CryptoViewModel> _cosmosStore;
        public HomeController(ICosmosStore<CryptoViewModel> cosmosStore)
        {
            _cosmosStore = cosmosStore;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var crypto = await _cosmosStore.Query().ToListAsync();
            return View(crypto);

        }
        [HttpPost("delete/{cryptoId}")]
        public async Task<IActionResult> DeleteCrypto(string cryptoId)
        {
            await _cosmosStore.RemoveByIdAsync(cryptoId);
            return RedirectToAction("Index");
        }

        [HttpPost("addcrypto")]
        public async Task<IActionResult> AddCrypto([FromForm] CryptoViewModel cryptoToAdd)
        {
            if(cryptoToAdd.valueUSD != null)
            {
                cryptoToAdd.valueBTC = (double.Parse(cryptoToAdd.valueUSD) / 7000).ToString();
                cryptoToAdd.valueETH = (double.Parse(cryptoToAdd.valueUSD) / 283).ToString();
            }
            
            await _cosmosStore.AddAsync(cryptoToAdd);
            return RedirectToAction("Index");
        }

        [HttpPost("updatecrypto")]
        public async Task<IActionResult> UpdateCrypto([FromForm] CryptoViewModel cryptoToUpdate)
        {
            if (cryptoToUpdate.valueUSD != null)
            {
                cryptoToUpdate.valueBTC = (double.Parse(cryptoToUpdate.valueUSD) / 7000).ToString();
                cryptoToUpdate.valueETH = (double.Parse(cryptoToUpdate.valueUSD) / 283).ToString();
            }
                
            var result = await _cosmosStore.UpdateAsync(cryptoToUpdate);
            return RedirectToAction("Index");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
