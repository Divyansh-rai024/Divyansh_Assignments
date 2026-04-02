using DBFirstEFinAsp.netcoreDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace DBFirstEFinAsp.netcoreDemo.Controllers
{
    public class NorthwindController : Controller
    {
        public IActionResult SpainCustomers()
        {
            NorthwndContext cnt = new NorthwndContext();

            var spainscustomers = cnt.Customers
                .Where(x => x.Country == "Spain")
                .Select(x => new SpainCustomerVM
                {
                    Cid = x.CustomerId,
                    CName = x.ContactName,
                    ComName = x.CompanyName
                }).ToList();

            return View(spainscustomers);
        }

        public IActionResult searchCustomer(string contactname)
        {
            NorthwndContext cnt = new NorthwndContext();

            var query = cnt.Customers
                .Where(x => x.ContactName == contactname)
                .Select(x => new Customer
                {
                    ContactName = x.ContactName,
                    ContactTitle = x.ContactTitle,
                    CompanyName = x.CompanyName
                })
                .SingleOrDefault();

            return View(query);
        }

        public ActionResult ProductsInCategory(string categoryname)
        {
            NorthwndContext cnt = new NorthwndContext();

            var productsinCategory = cnt.Products
                .Where(x => x.Category.CategoryName == categoryname)
                .Select(x => new ProdCat
                {
                    prodname = x.ProductName,
                    catname = x.Category.CategoryName
                }).ToList();

            return View(productsinCategory);
        }

        public ActionResult OrderRange(string range)
        {
            NorthwndContext cnt = new NorthwndContext();

            var range1 = Convert.ToInt16(range);

            var orders = cnt.OrdersQries.ToList();

            var custOrderCount = orders
                .GroupBy(x => new { x.CustomerId, x.CompanyName })
                .Where(g => g.Count() > range1)
                .Select(g => new Customer
                {
                    CustomerId = g.Key.CustomerId,
                    ContactName = g.Key.CompanyName
                }).ToList();

            return View(custOrderCount);
        }

        public IActionResult CustomerOrderDetails(string id)
        {
            NorthwndContext cnt = new NorthwndContext();

            var orders = cnt.OrdersQries
                .Where(o => o.CustomerId == id)
                .ToList()
                .OrderByDescending(o => o.OrderDate)
                .ToList();

            return View(orders);
        }
    }
}