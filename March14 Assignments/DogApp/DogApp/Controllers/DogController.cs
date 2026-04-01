using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DogApp.Models;

namespace DogApp.Controllers
{
    public class DogController : Controller
    {
        private static List<Dog> dogs = new List<Dog>();
        private readonly IWebHostEnvironment _environment;

        public DogController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        // GET: DogController
        public ActionResult Index(string? search)
        {
            var filtered = string.IsNullOrWhiteSpace(search)
                ? dogs
                : dogs.Where(d => d.Name?.Contains(search, StringComparison.OrdinalIgnoreCase) == true).ToList();

            return View(filtered);
        }

        // GET: DogController/Details/5
        public ActionResult Details(int id)
        {
            return View(dogs.FirstOrDefault(d => d.ID == id));
        }

        // GET: DogController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DogController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Dog d, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                // Assign ID automatically
                d.ID = dogs.Count > 0 ? dogs.Max(x => x.ID) + 1 : 1;

                if (imageFile != null && imageFile.Length > 0)
                {
                    var imageName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                    var path = Path.Combine(_environment.WebRootPath, "images", imageName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        imageFile.CopyTo(stream);
                    }

                    d.ImagePath = "/images/" + imageName;
                }

                dogs.Add(d);
                return RedirectToAction("Index");
            }

            return View(d);
        }

        // GET: DogController/Edit/5
        public ActionResult Edit(int id)
        {
            var dog = dogs.FirstOrDefault(d => d.ID == id);

            if (dog == null)
                return NotFound();

            return View(dog);
        }

        // POST: DogController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Dog d, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                var existing = dogs.FirstOrDefault(x => x.ID == d.ID);

                if (existing != null)
                {
                    existing.Name = d.Name;
                    existing.Description = d.Description;
                    existing.Age = d.Age;

                    // only update image if a new one is uploaded
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        var imageName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                        var path = Path.Combine(_environment.WebRootPath, "images", imageName);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            imageFile.CopyTo(stream);
                        }

                        existing.ImagePath = "/images/" + imageName;
                    }
                }

                return RedirectToAction("Index");
            }

            return View(d);
        }
        // GET: DogController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(dogs.FirstOrDefault(d => d.ID == id));
           
        }

        // POST: DogController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            
                var dog = dogs.FirstOrDefault(d => d.ID == id);

                if (dog != null)
                {
                    dogs.Remove(dog);
                }

                return RedirectToAction("Index");
            
           
        }
    }
}