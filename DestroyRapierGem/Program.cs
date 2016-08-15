using System;
using Ensage;
using Ensage.Common.Extensions;
using Ensage.Common.Menu;

namespace DestroyRapierGem
{
    class Program
    {
        private static Hero _me;
        private static readonly Menu Menu = new Menu("Destroy rapier/gem", "menu", true);
        

        private static readonly MenuItem Destroyge = new MenuItem("destroygem", "destroy gem").SetValue(new KeyBind('U', KeyBindType.Press));
        
        private static readonly MenuItem Destroyrap = new MenuItem("destroyrapier", "destroy rapier").SetValue(new KeyBind('Y', KeyBindType.Press));



        static void Main()
        {
            Game.OnUpdate += Onupdate;
            Menu.AddItem(Destroyge);
            Menu.AddItem(Destroyrap);
            
            Menu.AddToMainMenu();
        }

        public static void Onupdate(EventArgs args)
        {
            _me = ObjectManager.LocalHero;
            if (Game.IsKeyDown(Destroyge.GetValue<KeyBind>().Key))
                Destroygem();
            if (Game.IsKeyDown(Destroyrap.GetValue<KeyBind>().Key))
                Destroyrapier();
        }

        public static void Destroyrapier()
        {
            if (_me.GetLeveledItem("item_rapier") != null)
                _me.DestroyItem(_me.GetLeveledItem("item_rapier"));
        }
        public static void Destroygem()
        {
            if (_me.GetLeveledItem("item_gem") != null)
                _me.DestroyItem(_me.GetLeveledItem("item_gem"));
        }
    }
}
