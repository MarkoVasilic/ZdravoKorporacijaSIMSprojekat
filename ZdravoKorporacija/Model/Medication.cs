using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKorporacija.Model
{
    public class Medication
    {
        public int Id;
        public String Name;
        public List<String> Ingerdients;

        public Medication()
        {
        }

        public Medication(int id, string name, List<string> ingerdients)
        {
            Id = id;
            Name = name;
            Ingerdients = ingerdients;
        }
    }
}
