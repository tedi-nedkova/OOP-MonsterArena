namespace MonsterArena.GameCharacters
{
    public abstract class Monster : Character
    {
        public Monster(string name, double health, double attackPower, int level, bool isAlive)
            : base(name, health, attackPower, level, isAlive)
        {
        }
    }
}
