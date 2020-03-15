using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba2
{
    interface IMovment
    {
        void Move();
    }
    class Human : IMovment, IComponent
    {
        public string Title { get; set; }
        public void Drow()
        {
            Console.WriteLine(Title);

        }
        public IComponent Find(string title )
        {
            return Title == title ? this : null;
        }
        public string name;
        public Human(string name)
        {
            this.name = name;
        }
        public virtual void Move()
        {
            Console.WriteLine("Человек с именем: " + name);
            Console.WriteLine("Двигается пешком по x");
        }
    }
    class Walk
    {
        public void Travel(IMovment movment)
        {
            movment.Move(); 
        }
    }
    interface ISwim
    {
        void Swim();
    }
    class Shark : ISwim
    {
        public void Swim()
        {
            Console.WriteLine("Плывет на акуле по y");
        }
    }
    class SharkToHumanAdapter : IMovment
    {
        Shark shark;
        public SharkToHumanAdapter(Shark c)
        {
            shark = c;
        }
        public void Move()
        {
            shark.Swim();
        }

    }
    abstract class HumanDecorator : Human
    {
        protected Human human;
        public HumanDecorator(string name, Human human): base(name)
        {
            this.human = human;
        }

    }
    class YoungHuman : HumanDecorator
    {
        public YoungHuman(Human human) : base(human.name + " - молодой",human)
        {}
        public override void Move()
        {
            Console.WriteLine("Человек с именем " + name);
            Console.WriteLine("Двигается пешком по x");
        }
    }
    public interface IComponent
    {
        
        string Title { get; set; }
        void Drow();
        IComponent Find(string title);
    }
    public class Map : IComponent
    {
        private readonly List<IComponent> map = new List<IComponent>();
        public string Title { get; set; }
        public void AddComponent(IComponent component)
        {
            map.Add(component);
        }
        public IComponent Find(string title)
        {
            for(int i= 0; i< map.Count; i++)
            {
                if (map[i].Title==title)
                    Console.WriteLine("Мы нашли ваш Title: " + map[i].Title);
            }
            return null;
        }
        public void Drow()
        { Console.WriteLine(Title); }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Walk walk = new Walk();
            Human human = new Human("Илья");
            walk.Travel(human);
            Shark shark = new Shark();
            IMovment sharktransport = new SharkToHumanAdapter(shark);
            walk.Travel(sharktransport);
             Human human1 = new YoungHuman(human);
            walk.Travel(human1);
            Map map = new Map();
            human.Title = "LOL";
            human1.Title = "RUKA";
            map.AddComponent(human);
            map.AddComponent(human1);
            map.Find("LOL");
            human.Find("LOL");

            Console.Read();

        }
    }
}
