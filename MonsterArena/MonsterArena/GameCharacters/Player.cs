using MonsterArena.Inventory;
using MonsterArena.Inventory.SpecialItems;

namespace MonsterArena.GameCharacters
{
    public class Player : Character
    {
        public event Action OnCriticalHit;
        public event Action OnLevelUp;

        private int xp;

        public int Xp
        {
            get { return xp; }
            set { xp = value; }
        }


        public Bag<Item> Inventory { get; private set; }

        public Player(string name, double health, double attackPower, int level, bool isAlive, int xp)
            : base(name, health, attackPower, level, isAlive)
        {
            this.Inventory = new Bag<Item>();
            this.Xp = xp;
        }

        public override void Attack(Character target)
        {
            double damage = this.AttackPower;

            double critChance = new Random().Next(1,11);
            if (critChance > 8)
            {
                damage += 0.3 * damage;
                OnCriticalHit?.Invoke();

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Critical Hit!");
                Console.ResetColor();
            }

            target.TakeDamage(damage);
        }

        public void GainXP(int xp)
        {
            this.Xp += xp;

            if (Xp >= Level * 100)
            {
                LevelUp();
                OnLevelUp?.Invoke();

                Console.WriteLine($"{this.Name} leveled up to Level {this.Level}!");
            }
        }

        public virtual void Defend(double damage)
        {
            double defendChance = new Random().Next(1,11);
            if (defendChance > 6)
            {
                this.DecreaseHealth(damage / 2);
                Console.WriteLine($"{this.Name} defends and takes {damage/2} damage");
            }
        }

        public void UpPowerOnLevelUp()
        {
            this.IncreaseAttackPoints(5);
        }

        public void UpHealthOnLevelUp()
        {
            this.IncreaseHealth(10);
        }
    }
}
