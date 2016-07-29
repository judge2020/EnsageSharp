using System;
using System.Linq;
using Ensage;
using Ensage.Common.Extensions;
using Ensage.Common.Menu;

namespace AbaddonDeny
{
    internal class Program
    {
        private static Hero _me;
        private static string _isalived;
        private static readonly Menu Menu = new Menu("Abaddon Deny", "abaddondeny", true, "npc_dota_hero_abaddon", true); //test commit 1
        private static void Main()
        {
            Game.OnUpdate += Game_OnUpdate;
            Menu.AddItem(new MenuItem("toggle", "Enabled").SetValue(true));
            Menu.AddToMainMenu();
            Console.WriteLine("Abaddon Deny loaded!");
        }
        public static void Game_OnUpdate(EventArgs args)
        {
            _isalived = "yes";
            _me = ObjectManager.LocalHero;
            if (_me.IsAlive == false)
            {
                _isalived = "no";
            }
            if (!Game.IsInGame || _me.ClassID != ClassID.CDOTA_Unit_Hero_Abaddon) return;
            if (!Menu.Item("toggle").GetValue<bool>()) return;
            var q = _me.Spellbook.SpellQ;
            var qlvl = _me.Spellbook.SpellQ.Level;
            var qdmg = 0;
            if (qlvl <= 0)
                qdmg = 0;
            if (qlvl == 1)
                qdmg = 75;
            if (qlvl == 2)
                qdmg = 100;
            if (qlvl == 3)
                qdmg = 125;
            if (qlvl == 4)
                qdmg = 150;
            if (qdmg <= _me.Health && _me.Health != 1 && _me.Health != 0) return;
            
            var closestUnit = ObjectManager.GetEntities<Unit>()
                .Where(x =>(!x.Equals(_me) && ((x is Hero && !x.IsIllusion) || (x is Creep && x.IsSpawned)) && x.IsAlive && x.IsVisible))
                .OrderBy(x => x.Distance2D(ObjectManager.LocalHero))
                .FirstOrDefault();
            if (ObjectManager.LocalHero.Modifiers.Any(modifier => modifier.Name.Contains("modifier_abaddon_aphotic_shield"))) return;

            if (_isalived == "yes")
            {
                q.UseAbility(closestUnit);
            }
        }
    }
}





