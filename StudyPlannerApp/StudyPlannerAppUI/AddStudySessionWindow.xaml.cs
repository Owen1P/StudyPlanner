using System;
using System.Windows;
using StudyPlannerAppUI.Models;

namespace StudyPlannerAppUI
{
    public partial class AddStudySessionWindow : Window
    {
        // Properties to store user input
        public string Subject { get; private set; } = string.Empty;
        public string Mood { get; private set; } = string.Empty;
        public string Feelings { get; private set; } = string.Empty;
        public double HoursStudied { get; private set; }
        public DateTime Date { get; private set; }
        public bool IsSaved { get; private set; } = false;

        // Constructor
        public AddStudySessionWindow()
        {
            InitializeComponent();
        }

        // Save Button Click Event
       private void SaveButton_Click(object sender, RoutedEventArgs e)
{
    try
    {
        // Parse Date
        if (!DateTime.TryParse(DateTextBox.Text, out var date))
        {
            MessageBox.Show("Invalid date format. Please use yyyy-mm-dd.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        Date = date;

        // Parse Hours Studied
        if (!double.TryParse(HoursTextBox.Text, out var hours))
        {
            MessageBox.Show("Invalid hours format. Please enter a numeric value.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        HoursStudied = hours;

        // Assign other properties
        Subject = SubjectTextBox.Text;
        Mood = MoodTextBox.Text;
        Feelings = FeelingsTextBox.Text;

        // Ensure Mood is not null or empty
        if (string.IsNullOrWhiteSpace(Mood))
        {
            MessageBox.Show("Please enter a mood to get a motivational message.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        // Retrieve and display motivational message
        string motivationalMessage;
        try
        {
            Console.WriteLine($"Mood entered: {Mood}");
            motivationalMessage = MotivationalEngine.GetMotivationalMessage(Mood);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error retrieving motivational message: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        MessageBox.Show(motivationalMessage, "Motivational Message", MessageBoxButton.OK, MessageBoxImage.Information);

        // Mark as saved
        IsSaved = true;

        // Close the window
        this.Close();
    }
    catch (Exception ex)
    {
        MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
    }
}

        // Cancel Button Click Event
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
