namespace ArenaGame
{
    public class Hero
    {
        public string Name { get; private set; }

        public int Health { get; private set; }

        public int Strength { get; private set; }

        protected int StartingHealth { get; private set; }

        public bool IsDead { get { return Health <= 0; } }
        public Weapon Weapon { get; set; }

        public Hero(string name)
        {
            Name = name;
            Health = 500;
            StartingHealth = Health;
            Strength = 100;
        }

        public virtual int Attack()
        {
            return (Strength * Random.Shared.Next(80, 121)) / 100;
        }

        public virtual void TakeDamage(int incomingDamage)
        {
            if (incomingDamage < 0) return;
            Health = Health - incomingDamage;
        }

        protected bool ThrowDice(int chance)
        {
            int dice = Random.Shared.Next(101);
            return dice <= chance;
        }

        protected void Heal(int value)
        {
            Health = Health + value;
            if (Health > StartingHealth) Health = StartingHealth;
        }
    }

    public class Mage : Hero
    {
        public Mage(string name) : base(name)
        {
            Strength = 80;
            Health = 400;
            StartingHealth = Health;
        }

        public override int Attack()
        {
            int attack = base.Attack();
            attack += new Random().Next(20, 51); // Добавяне на магически щети
            return attack;
        }

        public override void TakeDamage(int incomingDamage)
        {
            base.TakeDamage(incomingDamage);
            if (ThrowDice(20))
            {
                Heal(30); // Лекува се с 30 единици при шанс от 20%
            }
        }
    }

    public class Paladin : Hero
    {
        public Paladin(string name) : base(name)
        {
            Strength = 90;
            Health = 600;
            StartingHealth = Health;
        }

        public override int Attack()
        {
            int attack = base.Attack();
            if (ThrowDice(10))
            {
                attack = attack * 2; 
            }
            return attack;
        }

        public override void TakeDamage(int incomingDamage)
        {
            base.TakeDamage(incomingDamage);
            if (ThrowDice(15))
            {
                Heal(50); 
            }
        }
    }

    public class Weapon
        {
            public string Name { get; private set; }
            public int Damage { get; private set; }
            public int SpecialChance { get; private set; }

            public Weapon(string name, int damage, int specialChance)
            {
                Name = name;
                Damage = damage;
                SpecialChance = specialChance;
            }

            public virtual int CalculateDamage()
            {
                if (ThrowDice(SpecialChance))
                {
                    return Damage * 2; 
                }
                return Damage;
            }

            protected bool ThrowDice(int chance)
            {
                int dice = Random.Shared.Next(101);
                return dice <= chance;
            }
        }
    

        public class Sword : Weapon
        {
            public Sword() : base("Sword", 30, 10) { }

            public override int CalculateDamage()
            {
                if (ThrowDice(SpecialChance))
                {
                    return Damage * 3; 
                }
                return Damage;
            }
        }

        public class Bow : Weapon
        {
            public Bow() : base("Bow", 25, 20) { }

            public override int CalculateDamage()
            {
                if (ThrowDice(SpecialChance))
                {
                    return Damage * 2; 
                }
                return Damage;
            }
        }

        public class Axe : Weapon
        {
            public Axe() : base("Axe", 35, 15) { }

            public override int CalculateDamage()
            {
                if (ThrowDice(SpecialChance))
                {
                    return Damage + 20; 
                }
                return Damage;
            }
        }
}
