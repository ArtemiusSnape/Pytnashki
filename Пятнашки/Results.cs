using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Пятнашки
{
    [Serializable]
    class Results:IComparable<Results>
    {
        public int Type = 0;
        public Results(int type,string player, DateTime date, int duration,int steps)
        {
            Player = player;
            Date = date;
            Duration = duration;
            Steps = steps;
            Type = type;
        }
        public Results()
        {

        }
        public string Player
        {
            get;
            set;
        }
        public DateTime Date
        {
            get;
            set;
        }
        public int Duration
        {
            get;
            set;
        }
        public int Steps
        {
            get;
            set;
        }
        public override string ToString()
        {
            return (Player + " || " + Duration + " seconds || " + Steps + " steps || " + Date);
        }
        public int CompareTo(Results a)
        {
            if (Type == 0)
            {
                if (a.Duration < this.Duration) return 1;
                if (a.Duration == this.Duration) return 0;
                else return -1;
            }
            else if (Type == 1)
            {

                if (a.Steps < this.Steps) return 1;
                if (a.Steps == this.Steps) return 0;
                else return -1;
            }
            else
            {

                if (a.Date > this.Date) return 1;
                if (a.Date == this.Date) return 0;
                else return -1;
            }
        }
        
        
    }

    
}
