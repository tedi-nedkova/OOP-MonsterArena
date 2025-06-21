namespace MonsterArena.GameCharacters
{
    public class Goblin : Monster
    {
        public Goblin() : base("Goblin", 95, 30, 5, true)
        {
        }

        public override void Attack(Character target)
        {
            double damage = this.AttackPower;

            Console.WriteLine("Goblin swings!");

            target.TakeDamage(damage);
        }
    }
}
