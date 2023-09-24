using System;
using static System.Console;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DevTeams.Repository.DeveloperRepository;
using DevTeams.Repository.DevloperTeamRepository;
using DevTeams.Data.Entities;
using System.Reflection.Metadata;



public class ProgramUi
{
    private readonly DevRepository _devRepo = new DevRepository();
    private readonly DevTeamRepository _devTeamRepo = new DevTeamRepository();
    private bool IsRunning = true;


    public void RunApplication()
    {
        Run();
    }



    private void Run()
    {
        while (IsRunning)
        {
            Clear();
            Console.WriteLine("Weclome to Dev-Teams");
            Console.WriteLine("1. Add Developer");
            Console.WriteLine("2. Add Team");
            Console.WriteLine("3. Add Developer to Team");
            Console.WriteLine("4. Remove Developer from Team");
            Console.WriteLine("5. List Developers without Pluralsight Access");
            Console.WriteLine("6. List All Teams");
            Console.WriteLine("7. List All Devlopers");
            Console.WriteLine("0. Exit");
            Console.Write("Enter your choice: ");

            string userInput = ReadLine();


            switch (userInput)
            {
                case "0":
                    CloseApplication();
                    break;

                case "1":
                    AddDeveloper();
                    break;

                case "2":
                    GetTeam();
                    break;

                case "3":
                    AddToTeam();
                    break;

                case "4":
                    RemoveFromTeam();
                    break;

                case "5":
                    PluralSightAccess();
                    break;

                case "6":
                    ListAllTeams();
                    break;

                case "7":
                    ListAllDevlopers();
                    break;

                default:
                    System.Console.WriteLine("Invalid Selection, Press any key to continue.");
                    ReadKey();
                    break;
            }

        }
    }

    private void ListAllDevlopers()
    {
        Console.Clear();
        Console.WriteLine("List of all developers:");

        List<Developer> developers = _devRepo.GetAllDevelopers();

        Console.WriteLine($"Total Developers: {developers.Count}");

        foreach (var developer in developers)
        {
            Console.WriteLine($"Developer ID: {developer.Id}, Full Name: {developer.FullName}, Pluralsight Access: {(developer.HasPluralsight ? "Yes" : "No")}");
        }

        Console.WriteLine("Press any key to continue.");
        Console.ReadKey();

    }

    private void CloseApplication()
    {
        Clear();
        IsRunning = false;
    }

    private void ListAllTeams()
    {
        Console.Clear();
        Console.WriteLine("List of all teams:");

        List<DeveloperTeam> teams = _devTeamRepo.GetAllDevTeams();

        Console.WriteLine($"Total Teams: {teams.Count}");

        foreach (var team in teams)
        {
            Console.WriteLine($"Team ID: {team.TeamId}, Team Name: {team.TeamName}");
        }

        Console.WriteLine("Press any key to continue.");
        Console.ReadKey();
    }

    private void PluralSightAccess()
    {
        Console.Clear();
        Console.WriteLine("Developers without Pluralsight access:");

        List<Developer> developersWithoutPluralsight = _devRepo.GetDevelopersWithoutPluralsight();

        if (developersWithoutPluralsight.Count > 0)
        {
            foreach (var developer in developersWithoutPluralsight)
            {
                Console.WriteLine($"{developer.FirstName} {developer.LastName}");
            }
        }
        else
        {
            Console.WriteLine("All developers have Pluralsight access.");
        }

        Console.WriteLine("Press any key to continue.");
        Console.ReadKey();
    }

    private void RemoveFromTeam()
    {
        Console.Clear();
        Console.WriteLine("Remove a developer from a team");
        Console.Write("Enter Team ID: ");

        if (int.TryParse(Console.ReadLine(), out int teamId))
        {
            DeveloperTeam team = _devTeamRepo.GetTeamById(teamId);

            if (team != null)
            {
                Console.Write("Enter Developer ID to remove: ");

                if (int.TryParse(Console.ReadLine(), out int developerId))
                {
                    Developer developerToRemove = _devRepo.GetDeveloperById(developerId);

                    if (developerToRemove != null)
                    {
                        if (team.DevsOnTeam.Remove(developerToRemove))
                        {
                            Console.WriteLine($"{developerToRemove.FullName} removed from Team {team.TeamName}");
                        }
                        else
                        {
                            Console.WriteLine($"{developerToRemove.FullName} is not in Team {team.TeamName}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Developer not found.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Developer ID.");
                }
            }
            else
            {
                Console.WriteLine("Team not found.");
            }

            System.Console.WriteLine("Do you want to remove another developer to a team?");
            System.Console.WriteLine("1. Yes");
            System.Console.WriteLine("2. No");

            string userInput = ReadLine();

            switch (userInput)
            {
                case "1":
                    AddToTeam();
                    break;

                case "2":
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                    break;

            }
        }
    }


    private void AddToTeam()
    {
        Console.Clear();
        Console.WriteLine("Add a developer to a team");

        Console.Write("Enter Team ID: ");
        if (int.TryParse(Console.ReadLine(), out int teamId))
        {
            DeveloperTeam team = _devTeamRepo.GetTeamById(teamId);

            if (team != null)
            {
                Console.Write("Enter Developer ID to add to the team: ");
                if (int.TryParse(Console.ReadLine(), out int developerId))
                {
                    Developer developerToAdd = _devRepo.GetDeveloperById(developerId);

                    if (developerToAdd != null)
                    {
                        team.DevsOnTeam.Add(developerToAdd);
                        Console.WriteLine($"{developerToAdd.FullName} added to Team {team.TeamName}");
                    }
                    else
                    {
                        Console.WriteLine("Developer not found.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Developer ID.");
                }
            }
            else
            {
                Console.WriteLine("Team not found.");
            }
        }
        else
        {
            Console.WriteLine("Invalid Team ID.");
        }


        System.Console.WriteLine("Do you want to add another developer to a team?");
        System.Console.WriteLine("1. Yes");
        System.Console.WriteLine("2. No");

        string userInput = ReadLine();

        switch (userInput)
        {
            case "1":
                AddToTeam();
                break;

            case "2":
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
                break;

        }


    }

    private void GetTeam()
    {
        {
            Console.Clear();
            Console.WriteLine("Add a new team");
            Console.Write("Team Name: ");
            string teamName = Console.ReadLine();

            DeveloperTeam devTeam = new DeveloperTeam
            {
                TeamName = teamName
            };

            _devTeamRepo.AddDevTeam(devTeam);
            Console.WriteLine("Team added successfully.");
        }
    }

    private void AddDeveloper()
    {
        {
            Console.Clear();
            Console.WriteLine("Add a new developer");
            Console.Write("First Name: ");
            string firstName = Console.ReadLine();
            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();
            Console.Write("Has Pluralsight access (true/false): ");
            if (bool.TryParse(Console.ReadLine(), out bool hasPluralsight))
            {
                Developer developer = new Developer
                {
                    FirstName = firstName,
                    LastName = lastName,
                    HasPluralsight = hasPluralsight
                };

                _devRepo.AddDeveloper(developer);
                Console.WriteLine("Developer added successfully.");
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter 'true' or 'false' for Pluralsight access.");
            }

        }





    }
}