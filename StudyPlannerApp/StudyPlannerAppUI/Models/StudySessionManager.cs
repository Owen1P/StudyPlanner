using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace StudyPlannerAppUI.Models
{
    public class StudySessionManager
    {
        private const string FileName = "StudySessions.json";
        private List<StudySession> studySessions;

        // Constructor
        public StudySessionManager()
        {
            studySessions = LoadFromFile();
        }

        // Method to add a new study session
        public void AddStudySession(StudySession session)
        {
            studySessions.Add(session);
            SaveToFile(); // Save to file after adding
            Console.WriteLine("Study session added successfully!");
        }

        // Method to display all study sessions
        public void DisplayAllSessions()
        {
            if (studySessions.Count == 0)
            {
                Console.WriteLine("No study sessions found.");
                return;
            }

            Console.WriteLine("Study Session History:");
            foreach (var session in studySessions)
            {
                Console.WriteLine("--------------------------");
                session.DisplayDetails();
            }
        }

              
        // Method to calculate total hours per subject
        public void TotalHoursPerSubject()
        {
            if (studySessions.Count == 0)
            {
                Console.WriteLine("No study sessions available for analysis.");
                return;
            }

            var subjectHours = new Dictionary<string, double>();

            foreach (var session in studySessions)
            {
                if (subjectHours.ContainsKey(session.Subject))
                {
                    subjectHours[session.Subject] += session.HoursStudied;
                }
                else
                {
                    subjectHours[session.Subject] = session.HoursStudied;
                }
            }

            Console.WriteLine("Total Hours Per Subject:");
            foreach (var kvp in subjectHours)
            {
                Console.WriteLine($"- {kvp.Key}: {kvp.Value} hours");
            }
        }


        // Method to analyze mood trends
        public void MoodTrends()
        {
            if (studySessions.Count == 0)
            {
                Console.WriteLine("No study sessions available for analysis.");
                return;
            }

            var moodCounts = new Dictionary<string, int>();

            foreach (var session in studySessions)
            {
                if (moodCounts.ContainsKey(session.Mood))
                {
                    moodCounts[session.Mood]++;
                }
                else
                {
                    moodCounts[session.Mood] = 1;
                }
            }

            Console.WriteLine("Mood Trends:");
            foreach (var kvp in moodCounts)
            {
                Console.WriteLine($"- {kvp.Key}: {kvp.Value} sessions");
            }
        }

           
        // Method to save study sessions to a file
        private void SaveToFile()
        {
            try
            {
                string json = JsonSerializer.Serialize(studySessions, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(FileName, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to file: {ex.Message}");
            }
        }

                public Dictionary<string, int> GetMoodTrends()
        {
            return studySessions
                .GroupBy(s => s.Mood)
                .ToDictionary(g => g.Key, g => g.Count());
        }


                public Dictionary<string, double> GetTotalHoursPerSubject()
        {
            return studySessions
                .GroupBy(s => s.Subject)
                .ToDictionary(g => g.Key, g => g.Sum(s => s.HoursStudied));
        }


                public List<StudySession> GetAllSessions()
        {
            return new List<StudySession>(studySessions);
        }
                
                public List<StudySession> FilterByMood(string mood)
        {
            return studySessions.Where(s => s.Mood.Equals(mood, StringComparison.OrdinalIgnoreCase)).ToList();
        }

                public List<StudySession> FilterBySubject(string subject)
        {
            return studySessions.Where(s => s.Subject.Equals(subject, StringComparison.OrdinalIgnoreCase)).ToList();
        }


        // Method to load study sessions from a file
        private List<StudySession> LoadFromFile()
        {
            try
            {
                if (File.Exists(FileName))
                {
                    string json = File.ReadAllText(FileName);
                    return JsonSerializer.Deserialize<List<StudySession>>(json) ?? new List<StudySession>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading from file: {ex.Message}");
            }
            return new List<StudySession>();
        }
    }
}
