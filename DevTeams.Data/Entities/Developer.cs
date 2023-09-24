using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevTeams.Data.Entities
{
    public class Developer
    {

        public Developer(){} //Empty Constructor
        
        public Developer(string firstName, string lastName, bool hasPluralsight) //Full Constructor
        {
            FirstName = firstName;
            this.LastName = lastName;
            HasPluralsight = hasPluralsight;

        }

        //Primary Key **Unique Identifier**
        public int Id { get; set; }
        public string FirstName { get; set; }= string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName
        {
            get{
                return $"{FirstName} {LastName}";
            }
        }
        public bool HasPluralsight {get; set;}
    }
}