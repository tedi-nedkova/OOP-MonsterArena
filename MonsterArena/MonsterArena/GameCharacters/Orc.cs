namespace MonsterArena.GameCharacters
{
    public class Orc : Monster
    {
        public Orc()
            : base("Orc", 90, 35, 8, true)
        {
        }

        public override void Attack(Character target)
        {
            double damage = this.AttackPower;

            Console.WriteLine("Orc roars and strikes!");

            target.TakeDamage(damage);
        }
    }
}
