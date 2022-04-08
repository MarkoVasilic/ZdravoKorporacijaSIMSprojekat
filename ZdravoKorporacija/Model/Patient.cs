using System;
using System.Collections.Generic;

namespace Model
{
    public class Patient : User
    {
        public List<String> allergens;

        public Appointment[] appointment;

    }
}