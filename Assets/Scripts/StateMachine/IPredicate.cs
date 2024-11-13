public interface IPredicate
{
    bool Evaluate();
}

public class FuncPredicate : IPredicate
{
    private FuncPredicate<bool> func;

}
