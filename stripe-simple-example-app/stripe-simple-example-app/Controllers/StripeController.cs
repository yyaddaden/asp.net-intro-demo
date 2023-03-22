using Microsoft.AspNetCore.Mvc;
using Stripe;
using stripe_simple_example_app.Models;
using System.Reflection;

namespace stripe_simple_example_app.Controllers
{
    public class StripeController : Controller
    {
        private Models.Cart cart;

        public StripeController() 
        {
            cart = new Models.Cart()
            {
                Items = new List<Item>()
                {
                    new Models.Item() { Id = "1", Title = "Laptop", Price = 1000, Quantity = 0},
                    new Models.Item() { Id = "2", Title = "iPhone", Price = 500, Quantity = 0},
                    new Models.Item() { Id = "3", Title = "Mouse", Price = 30, Quantity = 0}
                }
            }
;
        }

        [HttpGet]
        public IActionResult Cart()
        {
            return View(cart);
        }

        [HttpPost]
        public IActionResult Cart(Models.Cart _cart)
        {
            if (ModelState.IsValid)
            {
                long totalAmount = 0;

                foreach (Models.Item item in _cart.Items)
                {
                    if(item.IsSelected)
                        totalAmount += item.Price * item.Quantity;
                }

                if(totalAmount == 0)
                    return View(_cart);
                else
                    return RedirectToAction("Invoice", new { _totalAmount = totalAmount, _name = _cart.Name });
            }
            
            return View(_cart);
        }

        [HttpGet]
        public IActionResult Invoice(long _totalAmount, string _name)
        {
            ViewBag.TotalAmount = _totalAmount;
            ViewBag.TotalAmountStripe = _totalAmount * 100;
            ViewBag.Name = _name;
            return View();
        }

        [HttpPost]
        public IActionResult Invoice(string stripeToken, string stripeEmail)
        {
            /* CLIENT */
            CustomerCreateOptions optionsCustomer = new CustomerCreateOptions
            {
                Email = stripeEmail,
                Name = Request.Form["name"],
            };

            CustomerService serviceCustomer = new CustomerService();
            Customer customer = serviceCustomer.Create(optionsCustomer);

            /* FACTURE */
            ChargeCreateOptions optionsCharge = new ChargeCreateOptions
            {
                Amount = long.Parse(Request.Form["amount"]) * 100,
                Currency = "CAD",
                Description = "Facture",
                Source = stripeToken,
                ReceiptEmail = stripeEmail,
            };

            ChargeService service = new ChargeService();
            Charge charge = service.Create(optionsCharge);

            /* TRAITEMENT */
            if (charge.Status == "succeeded")
            {
                string BalanceTransactionId = charge.BalanceTransactionId;
                ViewBag.AmountPaid = charge.Amount / 100;
                ViewBag.BalanceTxId = BalanceTransactionId;
                ViewBag.Customer = customer.Name;

                return View("Success");
            }

            return View("Error");
        }
    }
}
