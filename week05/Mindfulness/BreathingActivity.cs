using System;
using System.Threading;

public class BreathingActivity : Activity
{
    public BreathingActivity() : base(
        "Breathing",
        "This activity will help you relax by walking your through breathing in and out slowly. Clear your mind and focus on your breathing."
    )
    {
    }

    public void Run()
    {
        DisplayStartingMessage();
        
        DateTime startTime = DateTime.Now;
        DateTime endTime = startTime.AddSeconds(_duration);

        while (DateTime.Now < endTime)
        {
            ShowBreathingAnimation(4, true); 
            ShowBreathingAnimation(6, false); 
        }

        DisplayEndingMessage();
    }
} 