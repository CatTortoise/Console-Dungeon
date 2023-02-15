//Basic C# for Games
//Dor Ben-Dor
//Final Project 
//Yshai flising
namespace Console_Dungeon
{
    public static class My_IO
    {
        #region Input
        public static string GetStringInput(string meseg)
        {
            string input = "";

            Console.WriteLine(meseg);
            input = Console.ReadLine();
            return input;
        }

        public static bool ExitCheck(string meseg)
        {
            if (meseg == "")
            {
                meseg = "To exit type Y";
            }
            string input = GetStringInput(meseg);
            return (input == "y" || input == "Y");
        }

        public static int GetIntInput(string meseg)
        {
            string input = "";
            input = GetStringInput(meseg);
            try
            {
                return int.Parse(input);
            }
            catch
            {
                return GetIntInput(meseg);
            }
        }

        public static void PrintColoredMessage(string message, ConsoleColor color)
        {
            ConsoleColor foregroundColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ForegroundColor = foregroundColor;
        }


        #endregion

        #region UX

        public static void VisualSeparator()
        {
            Console.WriteLine();
            int width = Console.WindowWidth;
            for (int i = 0; i < width; i++)
            {
                Console.Write("*");
            }
            Console.WriteLine();
        }

        #endregion

    }

}