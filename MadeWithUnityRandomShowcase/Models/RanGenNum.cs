using System;

namespace MadeWithUnityRandomShowcase.Models
{
    public class RanGenNum
    {
        public int number;

        public RanGenNum()
        {
            Random rand = new Random();
            number = rand.Next(0, 100);
        }
    }
}