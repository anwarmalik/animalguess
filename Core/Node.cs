using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace AnimalGuess.Core
{
    public abstract class Node
    {
        public abstract string GetQuestion();
        public string Value { get; set; }

        public void Ask()
        {
            Console.WriteLine(GetQuestion());
            var s = Console.ReadLine();

            if (s.Equals("y", StringComparison.InvariantCultureIgnoreCase))
            {
                YesAnswered();
            }
            else
            {
                NoAnswered();
            }
        }

        public Node Parent;

        public Node YesQuestion { get; set; }
        public Node NoQuestion { get; set; }
        public abstract void YesAnswered();
        public abstract void NoAnswered();
    }
}
