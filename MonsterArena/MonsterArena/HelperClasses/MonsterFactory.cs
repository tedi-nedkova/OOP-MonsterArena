using MonsterArena.GameCharacters;

public static class MonsterFactory
{
    private static Random random;

    public static Monster CreateMonster()
    {
        
         Random random = new Random();

        int randomNumber = random.Next(1, 4);

        switch (randomNumber)
        {
            case 1:
                return new Goblin();
            case 2:
                return new Orc();
            case 3:
                return new Dragon();
            default:
                throw new ArgumentException("Invalid monster type!");
        }
    }
}
