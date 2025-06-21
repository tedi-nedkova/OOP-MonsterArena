using MonsterArena.HelperClasses;

namespace MonsterArena
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Monster Arena";

            GameManager.Instance.Menu();
        }
    }
}
