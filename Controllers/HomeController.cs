using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dojodachi.Models;
using Microsoft.AspNetCore.Http;

namespace Dojodachi.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("dojodachi")]
        public IActionResult MainPage()
        {
            if (HttpContext.Session.GetString("exists") != null)
            {
                PetDojodachi yourPet = new PetDojodachi();
                yourPet.Fullness = (int)HttpContext.Session.GetInt32("Fullness");
                yourPet.Happiness = (int)HttpContext.Session.GetInt32("Happiness");
                yourPet.Meals = (int)HttpContext.Session.GetInt32("Meals");
                yourPet.Energy = (int)HttpContext.Session.GetInt32("Energy");
                yourPet.Text = (string)HttpContext.Session.GetString("Text");
                return View(yourPet);
                
            }
            else
            {
                HttpContext.Session.SetString("exists", "yes");
                PetDojodachi yourPet = new PetDojodachi();
                HttpContext.Session.SetInt32("Fullness",yourPet.Fullness);
                HttpContext.Session.SetInt32("Happiness", yourPet.Happiness);
                HttpContext.Session.SetInt32("Meals", yourPet.Meals);
                HttpContext.Session.SetInt32("Energy", yourPet.Energy);
                HttpContext.Session.SetString("Text", yourPet.Text);
                return View(yourPet);
            }
            
            
        }
        [HttpPost]
        public IActionResult Feed()
        {
            if (HttpContext.Session.GetInt32("Meals") > 0)
            {
                Random rand = new Random();
                if (rand.Next(4) == 0)
                {
                    HttpContext.Session.SetString("Text","You tried to feed your Dojodachi, but he wasn't feeling it...");
                    return RedirectToAction("MainPage");
                }
                else
                {
                    int fullnessNumber = rand.Next(1, 11);
                    int fullness = (int)HttpContext.Session.GetInt32("Fullness") + fullnessNumber;
                    HttpContext.Session.SetInt32("Fullness",fullness);
                    int mealCount = (int)HttpContext.Session.GetInt32("Meals") - 1;
                    HttpContext.Session.SetInt32("Meals", mealCount);
                    HttpContext.Session.SetString("Text",$"You fed your Dojodachi! +{fullnessNumber} Fullness, -1 Meal");
                    return RedirectToAction("MainPage");
                }
            }
            else
            {
                return RedirectToAction("MainPage");
            }
            

        }
        public IActionResult Play()
        {
            if ((int)HttpContext.Session.GetInt32("Energy") > 0)
            {
                Random rand = new Random();
                if (rand.Next(4) == 0)
                {
                    HttpContext.Session.SetString("Text","You tried to play with your Dojodachi but it wasn't feeling it...");
                    return RedirectToAction("MainPage");
                }
                else
                {
                    int happinessAddition = rand.Next(5, 11);
                    int energy = (int)HttpContext.Session.GetInt32("Energy") - 5;
                    HttpContext.Session.SetInt32("Energy", energy);
                    int happiness = (int)HttpContext.Session.GetInt32("Happiness") + happinessAddition;
                    HttpContext.Session.SetInt32("Happiness", happiness);
                    HttpContext.Session.SetString("Text",$"You played with your Dojodachi! +{happinessAddition} Happiness, -5 Energy");
                    return RedirectToAction("MainPage");
                }
                

            }
            else
            {
                HttpContext.Session.SetString("Text","I'm out of energy!");
                return RedirectToAction("MainPage");
            }
                

        }
        public IActionResult Work()
        {
            Random rand = new Random();
            int mealAddition = rand.Next(1,4);
            int energy = (int)HttpContext.Session.GetInt32("Energy") - 5;
            HttpContext.Session.SetInt32("Energy", energy);
            int meals = (int)HttpContext.Session.GetInt32("Meals") + mealAddition;
            HttpContext.Session.SetInt32("Meals", meals);
            ViewBag.Text = $"You worked your Dojodachi! +{mealAddition} Meals, -5 Energy";
            return RedirectToAction("MainPage");
        }
        public IActionResult Sleep()
        {
            int energy = (int)HttpContext.Session.GetInt32("Energy") + 15;
            HttpContext.Session.SetInt32("Energy", energy);
            int fullness = (int)HttpContext.Session.GetInt32("Fullness") - 5;
            HttpContext.Session.SetInt32("Fullness", fullness);
            int happiness = (int)HttpContext.Session.GetInt32("Happiness") - 5;
            HttpContext.Session.SetInt32("Happiness", happiness);
            ViewBag.Text = $"Dojodachi slept! +15 Energy, -5 Fullness, -5 Happiness";
            return RedirectToAction("MainPage");

        }
        public IActionResult Restart()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("MainPage");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
