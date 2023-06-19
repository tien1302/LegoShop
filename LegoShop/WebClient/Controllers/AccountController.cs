using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using BusinessObject.Models;
using Newtonsoft.Json.Linq;
using BusinessObject.DTOs;

namespace WebClient.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient client = null;
        private string AccountApiUrl = "";

        public AccountController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            AccountApiUrl = "http://localhost:5155/api/account";
        }
        public async Task<IActionResult> Login(AccountLoginDTO acc)
        {
            HttpResponseMessage httpResponse = await client.GetAsync($"{AccountApiUrl}?$filter=Email eq '{acc.Email}'&$top=1");
            var result = await httpResponse.Content.ReadAsStringAsync();
            try
            {
                httpResponse.EnsureSuccessStatusCode();
            }
            catch
            {

            }
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var employee = JsonSerializer.Deserialize<Account>(result, options);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Details(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"{AccountApiUrl}/{id}");
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            Account acc = JsonSerializer.Deserialize<Account>(strData, options);
            return View(acc);
        }

        public async Task<ActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegisterDTO p)
        {
            if (ModelState.IsValid)
            {
                string strData = JsonSerializer.Serialize(p);
                var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(AccountApiUrl, contentData);
                if (response.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Insert successfully!";
                }
                else
                {
                    ViewBag.Message = "Error while calling WebAPI!";
                }
            }
            else
                ViewBag.Message = "Error!";
            return View(p);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int? id)
        {
            HttpResponseMessage response = await client.DeleteAsync($"{AccountApiUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Delete successfully!";
            }
            else
            {
                TempData["Message"] = "Error while calling WebAPI!";
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"{AccountApiUrl}/{id}");
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var acc = JsonSerializer.Deserialize<RegisterDTO>(strData, options);
            return View(acc);
        }
        [HttpPost]
        public async Task<IActionResult> Update(RegisterDTO p)
        {
            if (ModelState.IsValid)
            {
                string strData = JsonSerializer.Serialize(p);
                var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsJsonAsync($"{AccountApiUrl}/{p.AccountId}", contentData);
                if (response.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Insert successfully!";
                }
                else
                {
                    ViewBag.Message = "Error while calling WebAPI!";
                }
            }
            else
                ViewBag.Message = "Error!";
            return View(p);
        }
    }
}
