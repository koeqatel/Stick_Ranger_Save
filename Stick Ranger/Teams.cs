using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stick_Ranger
{
    public class Team
    {
        public string firstName { get; set; }
        public string secondName { get; set; }
        public string thirdName { get; set; }
        public string lastName { get; set; }
        public string saveCode { get; set; }

        public Team(string FirstName, string SecondName, string ThirdName, string LastName, string SaveCode)
        {
            firstName = FirstName;
            secondName = SecondName;
            thirdName = ThirdName;
            lastName = LastName;
            saveCode = SaveCode;
        }
    }
}
