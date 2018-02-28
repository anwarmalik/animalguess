using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.IO;


namespace AnimalGuess.Core
{

    public class NodeValue{
        public string QuestionText {get;set;}
        public bool IsAnimal {get;set;}
    }
    public class AnimalGuesGame {
        private string _fileToStore;
        private List<string> _animals;

        public AnimalGuesGame(){
            _fileToStore = $"{Directory.GetCurrentDirectory()}/data.txt";
        }

        public IEnumerable<Node> PersistNodes(Node questionNode){
            
            if (questionNode != null){
                yield return questionNode;
                foreach (var persistNode in PersistNodes(questionNode.YesQuestion))
                {
                    yield return persistNode;
                }
                foreach (var persistNode in PersistNodes(questionNode.NoQuestion))
                {
                    yield return persistNode;
                }
            }

        }

        public void Initialize(){
            var questionNode = RetriveAnimals();

            _animals = GetAllAnimals(questionNode).ToList();
            Console.WriteLine(" ");

            foreach (var item in _animals)
            {
                Console.WriteLine(item);
            }

            do
            {
                questionNode.Ask();
                Console.WriteLine(" ");
                Console.WriteLine("Do you want to play agian?");
                var s = Console.ReadLine();
                if (s.Equals("y", StringComparison.InvariantCultureIgnoreCase)){
                    Console.WriteLine(" ");
                    continue;
                }
                break;
            } while (true);

            SaveKnowledge(questionNode);
        }


        public QuestionNode RetriveAnimals(){

            return GetNode();

        }
        public void SaveKnowledge(QuestionNode questionNode){
            Console.WriteLine("Do you want to persist new animals if any?");
            var s = Console.ReadLine();
            if (s.Equals("y",StringComparison.InvariantCultureIgnoreCase)){
                var getNodes = PersistNodes(questionNode).Select(a => new NodeValue {QuestionText = a.Value, IsAnimal = a.GetType() == typeof(AnimalLeaf)});
                var text = JsonConvert.SerializeObject(getNodes);
                if (File.Exists(_fileToStore)) {
                    File.Delete(_fileToStore);
                }
                File.WriteAllText(_fileToStore, text);
            }
        }

        public IEnumerable<string> GetAllAnimals(Node questionNode){
                if (questionNode == null) yield break;

                if (questionNode is AnimalLeaf){
                    yield return questionNode.Value;
                }

                if (questionNode.YesQuestion != null){
                    foreach(var a in GetAllAnimals(questionNode.YesQuestion)){
                        yield return a;
                    }
                }
                if (questionNode.NoQuestion != null){
                    foreach(var a in GetAllAnimals(questionNode.NoQuestion)){
                        yield return a;
                    }
                }
        }
        
        public QuestionNode GetNode(){
            var questionNode = new QuestionNode("Has a trunk, trumpets and is grey?");
            var questionNode2 = new QuestionNode("Has a mane, roars and is yellow?");
            var elephent = new AnimalLeaf("Elephent");
            var lion = new AnimalLeaf("Lion");
            var cat = new AnimalLeaf("Cat");
            questionNode.YesQuestion = elephent;
            questionNode.NoQuestion = questionNode2;
            elephent.Parent = questionNode;
            questionNode2.Parent = questionNode;
            lion.Parent = questionNode2;
            cat.Parent = questionNode2;

            questionNode2.YesQuestion = lion;
            questionNode2.NoQuestion = cat;

            return questionNode;
        }
    }
}



