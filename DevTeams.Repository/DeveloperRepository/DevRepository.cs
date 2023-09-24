using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevTeams.Data.Entities;

namespace DevTeams.Repository.DeveloperRepository
{
    public class DevRepository
    {
        //Create our Fake Database
        private readonly List<Developer> _devDbContext = new List<Developer>();

        //Database Id base value;
        private int _count = 0;

        public bool AddDeveloper(Developer developer)
        {
            if (developer is null)
            {
                return false;

            }
            else
            {
                _count++;
                developer.Id = _count;
                _devDbContext.Add(developer);
                return true;
            }
        }

        public List<Developer> GetAllDevelopers()
        {
            return _devDbContext;
        }

        public List<Developer> GetDevelopersWithoutPluralsight()
{
    var developersWithoutPluralsight = _devDbContext.Where(dev => !dev.HasPluralsight).ToList();
    return developersWithoutPluralsight;
}
public Developer GetDeveloperById(int developerId)
{
    return _devDbContext.FirstOrDefault(dev => dev.Id == developerId)!;
}


    }
}