namespace PredatorPrey.Models.ViewModels;

public class GraphicsVM
{
    //  жертвы
    public double Prey { get; set; }
    public double E { get; set; }
    public double a { get; set; }
    
    //  хищники
    public double Predator { get; set; }
    public double o { get; set; }
    public double b { get; set; }

    public GraphicsVM(double prey, double predator, double e, double a,  double o, double b)
    {
        Prey = prey;
        E = e;
        this.a = a;
        Predator = predator;
        this.o = o;
        this.b = b;
    }
}