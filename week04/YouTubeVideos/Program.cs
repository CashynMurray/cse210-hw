using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<Video> videos = new List<Video>();

        Video video1 = new Video("Catching Rainbow Trout on Dry Flies", "Trout Hunter", 850);
        video1.AddComment(new Comment("Fisherman1", "Great tips for dries!"));
        video1.AddComment(new Comment("AnglerJane", "My favorite fish to catch!"));
        video1.AddComment(new Comment("RiverGuide", "Awesome video quality."));
        videos.Add(video1);

        Video video2 = new Video("Techniques for Big Brown Trout", "Stream Stalker", 1120);
        video2.AddComment(new Comment("BrownTroutFan", "Need to try that nymphing technique."));
        video2.AddComment(new Comment("MountainFisher", "Caught a huge brown using this!"));
        video2.AddComment(new Comment("LureLover", "Makes me want to go fishing!"));
        videos.Add(video2);

        Video video3 = new Video("Fishing Hoppers in Late Summer", "HighCountryAngler", 780);
        video3.AddComment(new Comment("HopperHappy", "Love fishing hoppers!"));
        video3.AddComment(new Comment("BugLife", "Very effective pattern."));
        video3.AddComment(new Comment("DryFlyGuy", "When do you recommend using hoppers?"));
        video3.AddComment(new Comment("RiverRat", "Works every time in August.")); 
        videos.Add(video3);

        foreach (Video video in videos)
        {
            Console.WriteLine("==============================");
            Console.WriteLine(video.GetDisplayText());
            Console.WriteLine("==============================");
            Console.WriteLine();
        }
    }
}