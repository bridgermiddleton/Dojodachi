using System.ComponentModel.DataAnnotations;
namespace Dojodachi.Models
{
    public class PetDojodachi
    {
        public int Happiness{get;set;}
        public int Fullness{get;set;}
        public int Energy{get;set;}
        public int Meals{get;set;}
        public string Text{get;set;}

        public PetDojodachi()
        {
            Happiness = 20;
            Fullness = 20;
            Energy = 50;
            Meals = 3;
            Text = "Welcome to the Dojodachi Game!  Have fun with your Dojodachi and get all fields to 100 to win!";
        }


    }
}