using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevTeams.Data.Entities;

namespace DevTeams.Repository.DevloperTeamRepository
{
    public class DevTeamRepository
    {
        private readonly List<DeveloperTeam> _devTeamDbContext = new List<DeveloperTeam>();

        private int _count = 0;

        public bool AddDevTeam(DeveloperTeam devTeam)
        {
            if (devTeam is null)
            {
                return false;
            }
            else
            {
                _count++;
                devTeam.TeamId = _count;
                _devTeamDbContext.Add(devTeam);
                return true;
            }
        }

        public List<DeveloperTeam> GetAllDevTeams()
        {
            return _devTeamDbContext;
        }

        public bool RemoveFromTeam(int teamId, int developerId)
        {
            DeveloperTeam team = _devTeamDbContext.FirstOrDefault(team => team.TeamId == teamId);

            if (team != null)
            {
                Developer developer = team.DevsOnTeam.FirstOrDefault(devloper => devloper.Id == developerId);

                if (developer != null)
                {
                    team.DevsOnTeam.Remove(developer);
                    Console.WriteLine($"Developer {developer.FullName} removed from Team {team.TeamName}");
                    return true;
                }
                else
                {
                    Console.WriteLine($"Developer with ID {developerId} is not in Team {team.TeamName}");
                }
            }
            else
            {
                Console.WriteLine($"Team with ID {teamId} not found");
            }

            return false;


        }
        public DeveloperTeam GetTeamById(int teamId)
        {
            return _devTeamDbContext.FirstOrDefault(team => team.TeamId == teamId);
        }


    }
}

