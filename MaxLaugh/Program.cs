using System;
using System.Timers;
using Ensage;
using Ensage.Common.Menu;

//again greetings from judge2020
//i am a script kiddie
//i basically compilate code from the internet
//to make my programs
// I hate creating things
//but I am completely fine with putting things together 
//and editing things


namespace MaxLaugh
{
    class Program
    {
        private static readonly Menu Menu = new Menu("Always Laugh", "abaddondeny", true);
        private static Timer aTimer;

        static void Main(string[] args)
        {
            Game.OnUpdate += Game_OnUpdate;
            Console.WriteLine("Always laugh loaded.");
            Menu.AddItem(new MenuItem("laugh", "Enabled").SetValue(true));
            Menu.AddToMainMenu();
            SetTimer();
        }
        
        

        public static void Game_OnUpdate(EventArgs args)
        {
            if (!Menu.Item("laugh").GetValue<bool>())
            {
                
                return;
            }
            if (aTimer.Enabled)
                return;
            SetTimer();
            
            
            
        }
        private static void SetTimer()
        {
            
            aTimer = new Timer(16000);
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }
        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (!Menu.Item("laugh").GetValue<bool>())
            {
                return;
            }
            Game.ExecuteCommand("say \"/laugh\"");
        }
    }
}
