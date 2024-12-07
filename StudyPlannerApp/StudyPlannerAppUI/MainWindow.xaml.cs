using System.Windows;
using StudyPlannerAppUI.Models;
using System.Windows.Controls;
using System.Windows.Input;
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Win32;



namespace StudyPlannerAppUI
{
    public partial class MainWindow : Window
    {
        private StudySessionManager manager = new StudySessionManager();

        public MainWindow()
        {
            InitializeComponent();
        }

            private void AddStudySession_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddStudySessionWindow();
            addWindow.ShowDialog();

            if (addWindow.IsSaved)
            {
                var session = new StudySession(
                    addWindow.Subject,
                    addWindow.Date,
                    addWindow.HoursStudied,
                    addWindow.Mood,
                    addWindow.Feelings
                );

                manager.AddStudySession(session);
                MessageBox.Show("Study session added successfully!");
            }
        }


                private void ViewStudyHistory_Click(object sender, RoutedEventArgs e)
        {
            var sessions = manager.GetAllSessions();

            if (sessions.Count == 0)
            {
                MessageBox.Show("No study sessions found.");
                return;
            }

            string history = "Study History:\n";
            foreach (var session in sessions)
            {
                history += $"- {session.Subject} on {session.Date.ToShortDateString()} ({session.HoursStudied} hours)\n";
                history += $"  Mood: {session.Mood}, Feelings: {session.Feelings}\n";
            }

            MessageBox.Show(history);
        }


                private void FilterBySubject_Click(object sender, RoutedEventArgs e)
        {
            // Prompt for subject
            string subject = Microsoft.VisualBasic.Interaction.InputBox(
                "Enter the subject to filter by:",
                "Filter by Subject",
                ""
            );

            if (string.IsNullOrWhiteSpace(subject))
            {
                MessageBox.Show("Subject cannot be empty.");
                return;
            }

            // Get filtered sessions
            var filteredSessions = manager.FilterBySubject(subject);

            if (filteredSessions.Count == 0)
            {
                MessageBox.Show($"No study sessions found for subject: {subject}");
                return;
            }

            // Display filtered results
            string result = $"Study Sessions for Subject: {subject}\n";
            foreach (var session in filteredSessions)
            {
                result += $"- {session.Subject} on {session.Date.ToShortDateString()} ({session.HoursStudied} hours)\n";
                result += $"  Mood: {session.Mood}, Feelings: {session.Feelings}\n";
            }

            MessageBox.Show(result);
        }


                private void FilterByMood_Click(object sender, RoutedEventArgs e)
        {
            // Prompt for mood
            string mood = Microsoft.VisualBasic.Interaction.InputBox(
                "Enter the mood to filter by:",
                "Filter by Mood",
                ""
            );

            if (string.IsNullOrWhiteSpace(mood))
            {
                MessageBox.Show("Mood cannot be empty.");
                return;
            }

            // Get filtered sessions
            var filteredSessions = manager.FilterByMood(mood);

            if (filteredSessions.Count == 0)
            {
                MessageBox.Show($"No study sessions found with mood: {mood}");
                return;
            }

            // Display filtered results
            string result = $"Study Sessions with Mood: {mood}\n";
            foreach (var session in filteredSessions)
            {
                result += $"- {session.Subject} on {session.Date.ToShortDateString()} ({session.HoursStudied} hours)\n";
                result += $"  Mood: {session.Mood}, Feelings: {session.Feelings}\n";
            }

            MessageBox.Show(result);
        }


                private void ViewTotalHours_Click(object sender, RoutedEventArgs e)
        {
            var totals = manager.GetTotalHoursPerSubject();

            if (totals.Count == 0)
            {
                MessageBox.Show("No study sessions available for analysis.");
                return;
            }

            string result = "Total Hours Per Subject:\n";
            foreach (var total in totals)
            {
                result += $"- {total.Key}: {total.Value} hours\n";
            }

            MessageBox.Show(result);
        }


                private void ViewMoodTrends_Click(object sender, RoutedEventArgs e)
        {
            var trends = manager.GetMoodTrends();

            if (trends.Count == 0)
            {
                MessageBox.Show("No study sessions available for analysis.");
                return;
            }

            string result = "Mood Trends:\n";
            foreach (var trend in trends)
            {
                result += $"- {trend.Key}: {trend.Value} sessions\n";
            }

            MessageBox.Show(result);
        }


                private void ExportData_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "CSV Files (*.csv)|*.csv",
                FileName = "StudySessions.csv"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    using (var writer = new StreamWriter(saveFileDialog.FileName))
                    {
                        // Write CSV header
                        writer.WriteLine("Subject,Date,HoursStudied,Mood,Feelings");

                        // Write each study session
                        foreach (var session in manager.GetAllSessions())
                        {
                            writer.WriteLine($"{session.Subject},{session.Date:yyyy-MM-dd},{session.HoursStudied},{session.Mood},{session.Feelings}");
                        }
                    }

                    MessageBox.Show("Data exported successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while exporting data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


               private void ImportData_Click(object sender, RoutedEventArgs e)
{
    var openFileDialog = new Microsoft.Win32.OpenFileDialog
    {
        Filter = "CSV Files (*.csv)|*.csv"
    };

    if (openFileDialog.ShowDialog() == true)
    {
        try
        {
            using (var reader = new StreamReader(openFileDialog.FileName))
            {
                // Skip the header line
                reader.ReadLine();

                // Read each line and parse it into a StudySession
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    // Skip null or empty lines
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }

                    var parts = line.Split(',');

                    // Ensure the line has the correct number of parts (columns)
                    if (parts.Length == 5)
                    {
                        try
                        {
                            var session = new StudySession(
                                parts[0],                            // Subject
                                DateTime.Parse(parts[1]),           // Date
                                double.Parse(parts[2]),             // Hours Studied
                                parts[3],                           // Mood
                                parts[4]                            // Feelings
                            );

                            manager.AddStudySession(session);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error parsing line: {line}\nDetails: {ex.Message}", "Parse Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Skipping malformed line: {line}", "Malformed Line", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }

            MessageBox.Show("Data imported successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"An error occurred while importing data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}

    }
}
