using System;
using System.Collections.Generic;
using System.IO;

public class GoalManager
{
    private List<Goal> _goals;
    private int _score;

    public GoalManager()
    {
        _goals = new List<Goal>();
        _score = 0;
    }

    public void Start()
    {
        bool running = true;
        while (running)
        {
            Console.WriteLine("\nEternal Quest Menu:");
            Console.WriteLine("1. Display Player Info");
            Console.WriteLine("2. List Goal Names");
            Console.WriteLine("3. List Goal Details");
            Console.WriteLine("4. Create New Goal");
            Console.WriteLine("5. Record Event");
            Console.WriteLine("6. Save Goals");
            Console.WriteLine("7. Load Goals");
            Console.WriteLine("8. Quit");
            Console.Write("Select an option: ");
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    DisplayPlayerInfo();
                    break;
                case "2":
                    ListGoalNames();
                    break;
                case "3":
                    ListGoalDetails();
                    break;
                case "4":
                    CreateGoal();
                    break;
                case "5":
                    RecordEvent();
                    break;
                case "6":
                    SaveGoals();
                    break;
                case "7":
                    LoadGoals();
                    break;
                case "8":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    public void DisplayPlayerInfo()
    {
        Console.WriteLine($"\nCurrent Score: {_score}");
    }

    public void ListGoalNames()
    {
        Console.WriteLine("\nGoal Names:");
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetType().Name}: {_goals[i].GetDetailsString()}");
        }
        if (_goals.Count == 0)
        {
            Console.WriteLine("No goals available.");
        }
    }

    public void ListGoalDetails()
    {
        Console.WriteLine("\nGoal Details:");
        foreach (var goal in _goals)
        {
            Console.WriteLine(goal.GetDetailsString());
        }
        if (_goals.Count == 0)
        {
            Console.WriteLine("No goals available.");
        }
    }

    public void CreateGoal()
    {
        Console.WriteLine("\nSelect the type of goal to create:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        Console.Write("Enter your choice: ");
        string choice = Console.ReadLine();

        Console.Write("Enter the goal name: ");
        string name = Console.ReadLine();
        Console.Write("Enter a short description: ");
        string description = Console.ReadLine();
        Console.Write("Enter the points for this goal: ");
        int points = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case "1":
                _goals.Add(new SimpleGoal(name, description, points));
                Console.WriteLine("Simple goal created!");
                break;
            case "2":
                _goals.Add(new EternalGoal(name, description, points));
                Console.WriteLine("Eternal goal created!");
                break;
            case "3":
                Console.Write("Enter the target number of completions: ");
                int target = int.Parse(Console.ReadLine());
                Console.Write("Enter the bonus points for completion: ");
                int bonus = int.Parse(Console.ReadLine());
                _goals.Add(new ChecklistGoal(name, description, points, target, bonus));
                Console.WriteLine("Checklist goal created!");
                break;
            default:
                Console.WriteLine("Invalid choice. Goal not created.");
                break;
        }
    }

    public void RecordEvent()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("No goals available to record an event.");
            return;
        }

        Console.WriteLine("\nSelect a goal to record an event:");
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
        }
        Console.Write("Enter the number of the goal: ");
        if (int.TryParse(Console.ReadLine(), out int goalIndex) && goalIndex >= 1 && goalIndex <= _goals.Count)
        {
            Goal selectedGoal = _goals[goalIndex - 1];
            selectedGoal.RecordEvent();
            int pointsEarned = selectedGoal.GetPoints();

            if (selectedGoal is ChecklistGoal checklistGoal)
            {
                if (checklistGoal.IsComplete() && checklistGoal.GetDetailsString().Contains("[X]"))
                {
                    pointsEarned += (int)checklistGoal.GetType().GetField("_bonus", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(checklistGoal);
                }
            }

            _score += pointsEarned;
            Console.WriteLine($"Event recorded! You earned {pointsEarned} points.");
        }
        else
        {
            Console.WriteLine("Invalid selection.");
        }
    }

    public void SaveGoals()
    {
        Console.Write("Enter the filename to save to: ");
        string filename = Console.ReadLine();
        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            outputFile.WriteLine(_score);
            foreach (Goal goal in _goals)
            {
                outputFile.WriteLine(goal.GetStringRepresentation());
            }
        }
        Console.WriteLine("Goals and score saved successfully.");
    }

    public void LoadGoals()
    {
        Console.Write("Enter the filename to load from: ");
        string filename = Console.ReadLine();
        if (!File.Exists(filename))
        {
            Console.WriteLine("File not found.");
            return;
        }
        string[] lines = File.ReadAllLines(filename);
        _goals.Clear();
        if (lines.Length > 0)
        {
            _score = int.Parse(lines[0]);
            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] parts = line.Split(":");
                if (parts.Length < 2) continue;
                string type = parts[0];
                string[] details = parts[1].Split(",");
                switch (type)
                {
                    case "SimpleGoal":
                        _goals.Add(new SimpleGoal(details[0], details[1], int.Parse(details[2])));
                        if (bool.TryParse(details[3], out bool isComplete) && isComplete)
                        {
                            ((SimpleGoal)_goals[_goals.Count - 1]).RecordEvent();
                        }
                        break;
                    case "EternalGoal":
                        _goals.Add(new EternalGoal(details[0], details[1], int.Parse(details[2])));
                        break;
                    case "ChecklistGoal":
                        var checklist = new ChecklistGoal(details[0], details[1], int.Parse(details[2]), int.Parse(details[4]), int.Parse(details[5]));
                        typeof(ChecklistGoal).GetField("_amountCompleted", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(checklist, int.Parse(details[3]));
                        _goals.Add(checklist);
                        break;
                }
            }
        }
        Console.WriteLine("Goals and score loaded successfully.");
    }
} 