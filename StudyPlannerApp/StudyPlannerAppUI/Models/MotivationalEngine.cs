using System;
using System.Collections.Generic;

namespace StudyPlannerAppUI.Models
{
    public static class MotivationalEngine
    {
        private static readonly Dictionary<string, string> motivationalMessages;

        // Static constructor for error tracking
        static MotivationalEngine()
        {
            try
            {
                motivationalMessages = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                     { "Happy", "Keep up the great work! Your positive attitude is inspiring." },
                    { "Motivated", "You're unstoppable! Keep chasing your goals." },
                    { "Tired", "Rest is important too. Take a break and come back stronger!" },
                    { "Stressed", "Take a deep breath. You've got this, one step at a time." },
                    { "Focused", "Stay locked in. You're closer to your goal than you realize." },
                    { "Excited", "Your enthusiasm is contagious! Keep building on this energy!" },
                    { "Calm", "Your tranquility is your strength. Stay steady and focused." },
                    { "Overwhelmed", "Take things one step at a time. Small steps lead to big changes." },
                    { "Anxious", "Pause and breathe. You're stronger than you think." },
                    { "Confident", "Your belief in yourself is your superpower. Keep shining!" },
                    { "Curious", "Your hunger for knowledge is amazing. Keep exploring and learning!" },
                    { "Frustrated", "Challenges are opportunities to grow. You've got this!" },
                    { "Optimistic", "Your positive outlook is a gift. Great things are ahead!" },
                    { "Sad", "It's okay to feel this way. Take time to care for yourself." },
                    { "Determined", "Your drive will take you far. Stay on track and trust the process." },
                    { "Inspired", "Your ideas have the power to change the world. Take action!" },
                    { "Bored", "Why not try something new? A small change can spark excitement." },
                    { "Grateful", "Gratitude fuels positivity. Keep appreciating the little things." },
                    { "Lonely", "You're not alone. Reach out to someone who cares about you." },
                    { "Hopeful", "Hope lights the path forward. Keep believing in brighter days ahead." },
                    { "Relaxed", "Enjoy this moment of peace. It's well-deserved." },
                    { "Confused", "Clarity comes with time. Keep asking questions and seeking answers." },
                    { "Impatient", "Patience is a strength. Good things are worth waiting for." },
                    { "Fearful", "Courage is not the absence of fear but acting despite it. You can do this!" },
                    { "Joyful", "Your joy is your strength. Spread that happiness around!" },
                    { "Lost", "It's okay to feel this way. Take a moment to recalibrate and refocus." },
                    { "Playful", "Your sense of fun brings light to any situation. Keep it up!" },
                    { "Nervous", "Remember, nerves mean you care. Channel them into positive energy!" },
                    { "Empowered", "You have all the tools you need to succeed. Trust yourself." },
                    { "Angry", "Pause and reflect. Channel your energy into something productive." },
                    { "Content", "Savor this moment. Contentment is a beautiful thing." },
                    { "Restless", "Channel your energy into meaningful action. Start small and build up." },
                    { "Guilty", "Everyone makes mistakes. Forgive yourself and focus on moving forward." },
                    { "Proud", "Celebrate your achievements—you've earned it!" },
                    { "Resilient", "Your strength through adversity is incredible. Keep going!" },
                    { "Hungry", "Go get yourself a bite to eat! You've earned it!" }
        
                };
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"Error initializing MotivationalEngine: {ex.Message}");
                throw; // Re-throw to propagate the exception
            }
        }

        public static string GetMotivationalMessage(string mood)
        {
            if (string.IsNullOrWhiteSpace(mood))
            {
                return "No mood provided. Remember, you're doing great no matter what!";
            }

            if (motivationalMessages.TryGetValue(mood, out string? message) && message != null)
            {
                return message;
            }

            // Fallback for undefined moods
            return "Mood not recognized. You're doing amazing, keep it up!";
        }
    }
}
