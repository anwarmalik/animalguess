using System;
using System.Linq;
using AnimalGuess.Core;
using Newtonsoft.Json;

namespace AnimalGuess
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Gues the animal from following list or any animal!");
            Console.WriteLine("Answer to any question asked with y or n.. y for Yes and n or No." );

            var data = new AnimalGuess.Core.AnimalGuesGame();
            data.Initialize();
        }

    }
}
