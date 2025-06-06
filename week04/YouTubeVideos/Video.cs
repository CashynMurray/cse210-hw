using System;
using System.Collections.Generic;

public class Video
{
    private string _title;
    private string _author;
    private int _length;
    private List<Comment> _comments;

    public Video(string title, string author, int length)
    {
        _title = title;
        _author = author;
        _length = length;
        _comments = new List<Comment>();
    }

    public void AddComment(Comment comment)
    {
        _comments.Add(comment);
    }

    public int GetNumberOfComments()
    {
        return _comments.Count;
    }

    public string GetDisplayText()
    {
        string videoDetails = $"Title: {_title}, Author: {_author}, Length: {_length} seconds, Number of Comments: {GetNumberOfComments()}";
        
        string commentsText = "\nComments:";
        if (_comments.Count == 0)
        {
            commentsText += " No comments yet.";
        } else {
            foreach (Comment comment in _comments)
            {
                commentsText += "\n" + comment.GetDisplayText();
            }
        }

        return videoDetails + commentsText;
    }
} 