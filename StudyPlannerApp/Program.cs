using System;

namespace StudyPlannerAppUI.Models
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create an instance of StudySessionManager
            StudySessionManager manager = new StudySessionManager();

            while (true)
            {
                // Display menu options
               Console.WriteLine("\nStudy Planner Menu:");
                Console.WriteLine("1. Add a Study Session");
                Console.WriteLine("2. View Study History");
                Console.WriteLine("3. Filter by Subject");
                Console.WriteLine("4. Filter by Mood");
                Console.WriteLine("5. View Total Hours Per Subject");
                Console.WriteLine("6. View Mood Trends");
                Console.WriteLine("7. Exit");
                Console.Write("Enter your choice (1-7): ");

                // Get user input
                string choice = Console.ReadLine()!;

                switch (choice)
                 {
            case "1":
                AddStudySession(manager);
                break;
            case "2":
                manager.DisplayAllSessions();
                break;
            case "3":
                FilterBySubject(manager);
                break;
            case "4":
                FilterByMood(manager);
                break;
            case "5":
                manager.TotalHoursPerSubject();
                break;
            case "6":
                manager.MoodTrends();
                break;
            case "7":
                Console.WriteLine("Exiting the application. Goodbye!");
                return;
            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
                 }
            }
        }

        // Method to handle adding a study session
        static void AddStudySession(StudySessionManager manager)
        {
            Console.WriteLine("\nAdd a New Study Session:");

            // Get user inputs with null checks
            string subject;
            do
            {
                Console.Write("Enter the subject: ");
                subject = Console.ReadLine()!;
                if (string.IsNullOrWhiteSpace(subject))
                {
                    Console.WriteLine("Subject cannot be empty. Please try again.");
                }
            } while (string.IsNullOrWhiteSpace(subject));

            Console.Write("Enter the date (yyyy-mm-dd): ");
            string dateInput = Console.ReadLine()!;
            DateTime date;
            while (!DateTime.TryParse(dateInput, out date))
            {
                Console.Write("Invalid date format. Please enter again (yyyy-mm-dd): ");
                dateInput = Console.ReadLine()!;
            }

            Console.Write("Enter hours studied: ");
            string hoursInput = Console.ReadLine()!;
            double hoursStudied;
            while (!double.TryParse(hoursInput, out hoursStudied) || hoursStudied <= 0)
            {
                Console.Write("Invalid input. Please enter a positive number: ");
                hoursInput = Console.ReadLine()!;
            }

            string mood;
            do
            {
                Console.Write("Enter your mood: ");
                mood = Console.ReadLine()!;
                if (string.IsNullOrWhiteSpace(mood))
                {
                    Console.WriteLine("Mood cannot be empty. Please try again.");
                }
            } while (string.IsNullOrWhiteSpace(mood));

            Console.Write("Enter your feelings/comments: ");
            string feelings = Console.ReadLine()!;

            // Create and add the new study session
            StudySession session = new StudySession(subject, date, hoursStudied, mood, feelings);
            manager.AddStudySession(session);

            // Get and display a motivational message
            string message = MotivationalEngine.GetMessage(mood);
            Console.WriteLine($"\nMotivational Message: {message}");
            Console.WriteLine("\nStudy session added successfully!");
        }

        // Method to filter by subject
        static void FilterBySubject(StudySessionManager manager)
        {
            Console.Write("Enter the subject to filter by: ");
            string subject = Console.ReadLine()!;
            manager.FilterBySubject(subject);
        }

        // Method to filter by mood
        static void FilterByMood(StudySessionManager manager)
        {
            Console.Write("Enter the mood to filter by: ");
            string mood = Console.ReadLine()!;
            manager.FilterByMood(mood);
        }
    }
}
