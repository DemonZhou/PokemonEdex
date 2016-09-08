namespace Entity
{
    class Pokemon
    {
        string id; 
        public string Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        string imagepath;
        public string Imagepath
        {
            get
            {
                return imagepath;
            }
            set
            {
                imagepath = value;
            }
        }
        string attr;
        public string Attr
        {
            get
            {
                return attr;
            }
            set
            {
                attr = value;
            }
        }
        int phattack;
        public int Phattack
        {
            get
            {
                return phattack;
            }
            set
            {
                phattack = value;
            }
        }
        int phdefense;
        public int Phdefense
        {
            get
            {
                return phdefense;
            }
            set
            {
                phdefense = value;
            }
        }
        int spattack;
        public int Spattack
        {
            get
            {
                return spattack;
            }
            set
            {
                spattack = value;
            }
        }
        int spdefense;
        public int Spdefense
        {
            get
            {
                return spdefense;
            }
            set
            {
                spdefense = value;
            }
        }
        int hp;
        public int Hp
        {
            get
            {
                return hp;
            }
            set
            {
                hp = value;
            }
        }
        int sum;
        public int Sum
        {
            get
            {
                return sum;
            }
            set
            {
                sum = value;
            }
        }
        int speed;
        public int Speed
        {
            get
            {
                return speed;
            }
            set
            {
                speed = value;
            }
        }
        public Pokemon(string id,string name,string imagepath,
            string attr,int phattack,int phdefense,int spattack,
            int spdefense,int hp,int sum,int speed)
        {
            this.id = id;
            this.name = name;
            this.imagepath = imagepath;
            this.attr = attr;
            this.phattack = phattack;
            this.phdefense = phdefense;
            this.spattack = spattack;
            this.spdefense = spdefense;
            this.hp = hp;
            this.sum = sum;
            this.speed = speed;
        }
    }
}
