namespace Task4
{
    public class Dough { }
    public class Sause { }
    public class Cheese { }
    public class Veggies { }
    public class Sausage { }
    public class ThinCrustDough : Dough { }
    public class ThickCrustDough : Dough { }
    public class MarinaraSause : Sause { }
    public class PlumTomatoSause : Sause { }
    public class SomeCheese1 : Cheese { }
    public class SomeCheese2 : Cheese { }
    public class FreshOvosch : Veggies { }
    public class DeliciousOvosch : Veggies { }
    public class CoolOvosch : Veggies { }
    public class FreshSausage : Sausage { }
    public class FrozenSausage : Sausage { }
    public interface PizzaIngredientFactory
    {
        public Dough createDough();
        public Sause createSause();
        public Cheese createCheese();
        public Veggies[] createVeggies();
        public Sausage createSausage();
    }

    public class NyPizzaIngredientsFactory : PizzaIngredientFactory
    {
        public Dough createDough()
        {
            return new ThinCrustDough();
        }

        public Sause createSause()
        {
            return new MarinaraSause();
        }

        public Cheese createCheese()
        {
            return new SomeCheese1();
        }

        public Veggies[] createVeggies()
        {
            Veggies[] veggies = new Veggies[2];
            veggies[0] = new CoolOvosch();
            veggies[1] = new FreshOvosch();
            return veggies;
        }

        public Sausage createSausage()
        {
            return new FreshSausage();
        }
    }

    public class ChicagoPizzaIngredientsFactory : PizzaIngredientFactory
    {
        public Dough createDough()
        {
            return new ThickCrustDough();
        }

        public Sause createSause()
        {
            return new PlumTomatoSause();
        }

        public Cheese createCheese()
        {
            return new SomeCheese2();
        }

        public Veggies[] createVeggies()
        {
            Veggies[] veggies = new Veggies[2];
            veggies[0] = new DeliciousOvosch();
            veggies[1] = new FreshOvosch();
            return veggies;
        }

        public Sausage createSausage()
        {
            return new FrozenSausage();
        }
    }


    public abstract class Pizza
    {
        public string name;
        public Dough dough;
        public Sause sause;
        public Cheese cheese;
        public Veggies[] veggies;
        public Sausage sausage;

        abstract public void prepare();

        public void bake()
        {
            name += ", backed";
        }

        public void cut()
        {
            name += ", cutted";
        }

        public void box()
        {
            name += " and in a box!";
        }

        public void check()
        {
            Console.WriteLine(name);
        }
    }

    public class CheesePizza : Pizza
    {
        private PizzaIngredientFactory ingredientFactory;

        public CheesePizza(PizzaIngredientFactory ingredientFactory)
        {
            this.ingredientFactory = ingredientFactory;
        }

        public override void prepare()
        {
            this.name = "Cheese pizza";
            this.dough = ingredientFactory.createDough();
            this.sause = ingredientFactory.createSause();
            this.cheese = ingredientFactory.createCheese();
        }
    }

    public class PepperoniPizza : Pizza
    {
        private PizzaIngredientFactory ingredientFactory;

        public PepperoniPizza(PizzaIngredientFactory ingredientFactory)
        {
            this.ingredientFactory = ingredientFactory;
        }

        public override void prepare()
        {
            this.name = "Pepperoni pizza";
            this.dough = ingredientFactory.createDough();
            this.sause = ingredientFactory.createSause();
            this.cheese = ingredientFactory.createCheese();
            this.sausage = ingredientFactory.createSausage();
        }
    }

    public class GreekPizza : Pizza
    {
        private PizzaIngredientFactory ingredientFactory;

        public GreekPizza(PizzaIngredientFactory ingredientFactory)
        {
            this.ingredientFactory = ingredientFactory;
        }

        public override void prepare()
        {
            this.name = "Greek pizza";
            this.dough = ingredientFactory.createDough();
            this.sause = ingredientFactory.createSause();
            this.cheese = ingredientFactory.createCheese();
            this.veggies = ingredientFactory.createVeggies();
        }
    }


    public abstract class PizzaStore
    {
        public Pizza orderPizza(string type)
        {
            Pizza pizza = createPizza(type);

            pizza.prepare();
            pizza.bake();
            pizza.cut();
            pizza.box();
            return pizza;
        }


        public Pizza createPizza(string type)
        {
            Pizza piza = null;
            PizzaIngredientFactory ingredientFactory = this.getPizzaIgredientFactory();

            if (type == "cheese")
            {
                piza = new CheesePizza(ingredientFactory);
            }
            else if (type == "greek")
            {
                piza = new GreekPizza(ingredientFactory);
            }
            else if (type == "pepperoni")
            {
                piza = new PepperoniPizza(ingredientFactory);
            }
            return piza;
        }

        public abstract PizzaIngredientFactory getPizzaIgredientFactory();
    }


    public class NyPizzaStore : PizzaStore
    {
        public override PizzaIngredientFactory getPizzaIgredientFactory()
        {
            return new NyPizzaIngredientsFactory();
        }
    }

    public class ChicagoPizzaStore : PizzaStore
    {
        public override PizzaIngredientFactory getPizzaIgredientFactory()
        {
            return new ChicagoPizzaIngredientsFactory();
        }
    }



class Program
    {
        static void Main(string[] args)
        {
            Pizza pizza = (new NyPizzaStore()).orderPizza("cheese");
            pizza.check();
            Console.WriteLine("Bon appetit!");
        }
    }
}