using System;
using System.Threading;
using System.IO;

public class Activity
{
    protected string _name;
    protected string _description;
    protected int _duration;
    private static string _logFile = "activity_log.txt";

    public Activity(string name, string description)
    {
        _name = name;
        _description = description;
    }

    public void DisplayStartingMessage()
    {
        Console.WriteLine($"Welcome to the {_name} Activity");
        Console.WriteLine();
        Console.WriteLine(_description);
        Console.WriteLine();
        Console.Write("How long, in seconds, would you like for your session? ");
        _duration = int.Parse(Console.ReadLine());
        Console.WriteLine();
        Console.WriteLine("Get ready...");
        ShowSpinner(3);
    }

    public void DisplayEndingMessage()
    {
        Console.WriteLine();
        Console.WriteLine("Well done!");
        ShowSpinner(3);
        Console.WriteLine($"You have completed another {_duration} seconds of the {_name} Activity.");
        ShowSpinner(3);
        
        LogActivity();
    }

    private void LogActivity()
    {
        string logEntry = $"{DateTime.Now}: Completed {_name} Activity for {_duration} seconds\n";
        File.AppendAllText(_logFile, logEntry);
    }

    public void ShowSpinner(int seconds)
    {
        List<string> animationStrings = new List<string>();
        animationStrings.Add("|");
        animationStrings.Add("/");
        animationStrings.Add("-");
        animationStrings.Add("\\");
        animationStrings.Add("|");
        animationStrings.Add("/");
        animationStrings.Add("-");
        animationStrings.Add("\\");

        DateTime startTime = DateTime.Now;
        DateTime endTime = startTime.AddSeconds(seconds);

        int i = 0;
        while (DateTime.Now < endTime)
        {
            string s = animationStrings[i];
            Console.Write(s);
            Thread.Sleep(100);
            Console.Write("\b \b");

            i++;
            if (i >= animationStrings.Count)
            {
                i = 0;
            }
        }
    }

    public void ShowCountDown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write(i);
            Thread.Sleep(1000);
            Console.Write("\b \b");
        }
    }

    public void ShowBreathingAnimation(int seconds, bool isBreathingIn)
    {
        string message = isBreathingIn ? "Breathe in..." : "Breathe out...";
        int maxWidth = 20;
        
        for (int i = 0; i < seconds; i++)
        {
            Console.Clear();
            Console.WriteLine(message);
            
            int width = isBreathingIn ? 
                (int)((i + 1) * (maxWidth / (float)seconds)) : 
                (int)((seconds - i) * (maxWidth / (float)seconds));
            
            Console.Write("[");
            Console.Write(new string('=', width));
            Console.Write(new string(' ', maxWidth - width));
            Console.WriteLine("]");
            
            Thread.Sleep(1000);
        }
    }
} 