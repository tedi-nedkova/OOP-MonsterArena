namespace MonsterArena.GameCharacters
{
    public abstract class Character
    {
        private string name;

        public string Name
        {
            get { return name; }
            private set
            { 
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Invalid character name!");
                }

                name = value;
            }
        }

        private double health;

        public double Health
        {
            get { return health; }
            private set
            {
                if (value <= 0)
                {
                    this.IsAlive = false;
                }

                health = value;
            }
        }

        private double attackPower;

        public double AttackPower
        {
            get { return attackPower; }
            private set 
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Invalid character attack power!");
                }

                attackPower = value;
            }
        }

        private int level;

        public int Level
        {
            get { return level; }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Invalid character level!");
                }

                level = value;
            }
        }

        private bool isAlive;

        public bool IsAlive
        {
            get { return isAlive; }
            private set 
            {
                isAlive = value;
            }
        }

        public Character(string name, double health, double attackPower, int level, bool isAlive)
        {
            Name = name;
            Health = health;
            AttackPower = attackPower;
            Level = level;
            IsAlive = isAlive;
        }

        public abstract void Attack(Character target);

        public virtual void Defent(int demage)
        {
            double damageReduced = demage * 0.5;

            this.Health -= damageReduced;

            Console.WriteLine($"{this.Name} defends and takes {damageReduced} damage");
        }

        public void TakeDamage(double amount)
        {
            this.Health -= amount;

            if (!IsAlive)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;

                Console.WriteLine($"{Name} has been defeated!");

                Console.ResetColor();
            }
        }

        public void LevelUp()
        {
            this.Level++;
        }

        public void DecreaseHealth(double amount)
        {
            this.Health -= amount;
        }

        public void IncreaseHealth(double amount)
        {
            this.Health += amount;
        }

        public void IncreaseAttackPoints(double amount)
        {
            this.AttackPower += amount;
        }
    }
}
