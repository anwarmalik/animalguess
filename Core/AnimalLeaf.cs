using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnimalGuess.Core
{
    public class AnimalLeaf : Node
    {

        public AnimalLeaf(string animal)
        {
            Value = animal;
        }

        public override string GetQuestion()
        {
            return $"Is it a {Value}?";
        }


        public override void YesAnswered()
        {

            Console.WriteLine("Yessssss....! I know that.");
        }

        public override void NoAnswered()
        {
            Console.WriteLine("Sorry, could not find it.");
            AddAnimal();
        }

        private void AddAnimal()
        {
            Console.WriteLine("Which Animal it is?");
            var animalName = Console.ReadLine();
            Console.WriteLine($"What is the question that differentate {Value} and {animalName}");
            var questionName = Console.ReadLine();
            Console.WriteLine($"Is the answer to this question is yes or no for new animal: type y or n");
            var answer = Console.ReadLine();
            var question = new QuestionNode(questionName);
            var animal = new AnimalLeaf(animalName);
            if (answer.Equals("y", StringComparison.InvariantCultureIgnoreCase))
            {
                question.YesQuestion = animal;
                question.NoQuestion = this;
            }
            else
            {
                question.YesQuestion = this;
                question.NoQuestion = animal;
            }
            animal.Parent  = question;
            if (this == Parent.YesQuestion) {
                Parent.YesQuestion = question;
            }
            else{
                Parent.NoQuestion = question;
            }
            Parent = question;

        }
    }
}
