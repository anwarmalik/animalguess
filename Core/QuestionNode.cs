using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace AnimalGuess.Core
{
    public class QuestionNode : Node
    {

        public QuestionNode(string text)
        {
            Value = text;
        }


        public override string GetQuestion()
        {
            return Value;
        }

        public override void YesAnswered()
        {
            YesQuestion.Ask();
        }

        public override void NoAnswered()
        {
            NoQuestion.Ask();
        }
    }
}
