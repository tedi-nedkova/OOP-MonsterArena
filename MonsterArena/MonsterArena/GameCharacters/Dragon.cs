namespace MonsterArena.GameCharacters
{
    public class Dragon : Monster
    {
        private static Random random = new Random();

        public Dragon() : base("Dragon", 100, 40, 10, true)
        {
        }

        public override void Attack(Character target)
        {
            double damage = this.AttackPower;

            int chance = random.Next(1,101);

            if (chance > 70)
            {
                FireBreath(target);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Dragon uses his firebreath and deals extra damage!");
                Console.ResetColor();
            }

            target.TakeDamage(damage);
        }

        private void FireBreath(Character target)
        {
            double damage = this.AttackPower + 20;

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"{Name} unleashes Fire Breath for {damage} damage!");
            Console.ResetColor();

            target.TakeDamage(damage);
        }
    }
}
