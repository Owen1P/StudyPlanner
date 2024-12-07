using System;

namespace StudyPlannerAppUI.Models
{
    
    public class StudySession
    {
        // Properties
        public string Subject { get; set; }  
        public DateTime Date { get; set; }  
        public double HoursStudied { get; set; } 
        public string Mood { get; set; }  
        public string Feelings { get; set; }  

        // Constructor
        public StudySession(string subject, DateTime date, double hoursStudied, string mood, string feelings)
        {
            Subject = subject;
            Date = date;
            HoursStudied = hoursStudied;
            Mood = mood;
            Feelings = feelings;
        }

        // Method to display session details
        public void DisplayDetails()
        {
            Console.WriteLine($"Subject: {Subject}");
            Console.WriteLine($"Date: {Date.ToShortDateString()}");
            Console.WriteLine($"Hours Studied: {HoursStudied}");
            Console.WriteLine($"Mood: {Mood}");
            Console.WriteLine($"Feelings: {Feelings}");
        }
    }
}
