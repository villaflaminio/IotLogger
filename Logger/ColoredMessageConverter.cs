using System;
using System.IO;
using log4net.Core;
using log4net.Layout.Pattern;

namespace Iot.logManager
{
    /// <summary>
    ///     Convert compute the logging messages, fully customizable, instead of xml configuration.
    /// </summary>
    public class ColoredMessageConverter : PatternLayoutConverter
    {
        protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            String dottedInitial = GetDottedInitial(loggingEvent.LoggerName);

            Console.Write("{0} | {1} ", DateTime.Now.ToString(), dottedInitial);
            switch (loggingEvent.Level.Name)
            {
                case "DEBUG":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case "WARN":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case "INFO":
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case "ERROR":
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Red;
                    break;
                case "FATAL":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.BackgroundColor = ConsoleColor.White;
                    break;
            }
            Console.Write("{0} ", loggingEvent.Level);


            // Reset Console Colors.
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(" | {0} \n", loggingEvent.RenderedMessage);
        }

        /// <summary>
        /// Get only the dotted initial from a string separated by dots.
        /// Example: "st.rulesystem.services.sharedMemory" -> "s.r.s.s"
        /// </summary>
        /// <param name="s">the string to be parsed</param>
        /// <returns>the parsed string as the example above</returns>
        private string GetDottedInitial(string s)
        {
            // Get an array of the words in the string.
            string[] splitted = s.Split('.');

            // Empty string to store the result.
            string result = "";

            // Counter to know when to stop the loop.
            int i = 0;

            // Represents the number of dots to be skipped.
            // Example:
            // k = 1 => st.rulesystem.services.sharedMemory -> s.r.s.sharedMemory
            // k = 2 => st.rulesystem.services.sharedMemory -> s.r.services.sharedMemory
            int k = 2;

            // Loop through the array of words.
            foreach (string str in splitted)
            {
                if (i < splitted.Length - k)
                    result += str[0] + ".";
                else
                    result += str + ".";

                i++;
            }

            // Remove the last dot.
            return result.Substring(0, result.Length - 1);
        }
    }
}