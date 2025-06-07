using System;
using System.Collections.Generic;

public class ListingActivity : Activity
{
    private List<string> _prompts;
    private List<string> _usedPrompts;
    private int _count;

    public ListingActivity() : base(
        "Listing",
        "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area."
    )
    {
        _prompts = new List<string>
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };

        _usedPrompts = new List<string>();
    }

    public void Run()
    {
        DisplayStartingMessage();
        
        Console.WriteLine("List as many responses you can to the following prompt:");
        Console.WriteLine();
        Console.WriteLine($"--- {GetRandomPrompt()} ---");
        Console.WriteLine();
        Console.Write("You may begin in: ");
        ShowCountDown(5);
        Console.WriteLine();

        DateTime startTime = DateTime.Now;
        DateTime endTime = startTime.AddSeconds(_duration);
        _count = 0;

        while (DateTime.Now < endTime)
        {
            Console.Write("> ");
            string response = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(response))
            {
                _count++;
            }
        }

        Console.WriteLine($"You listed {_count} items!");
        DisplayEndingMessage();
    }

    private string GetRandomPrompt()
    {
        if (_usedPrompts.Count == _prompts.Count)
        {
            _usedPrompts.Clear();
        }

        Random random = new Random();
        string prompt;
        do
        {
            int index = random.Next(_prompts.Count);
            prompt = _prompts[index];
        } while (_usedPrompts.Contains(prompt));

        _usedPrompts.Add(prompt);
        return prompt;
    }
} 