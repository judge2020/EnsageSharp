using System;
using System.Linq;
using Ensage;
using Ensage.Common.Extensions;
using Ensage.Common.Menu;

namespace AbaddonDeny
{
    internal class Program
    {
        private static readonly Menu Menu = new Menu("Abaddon Deny", "abaddondeny", true, "npc_dota_hero_abaddon", true); //test commit
        private static void Main()
        {
            Game.OnUpdate += Game_OnUpdate;
            Menu.AddItem(new MenuItem("toggle", "Enabled").SetValue(true));
            Menu.AddToMainMenu();
            Console.WriteLine("Abaddon Deny loaded!");
        }
        public static void Game_OnUpdate(EventArgs args)
        {
            var me = ObjectManager.LocalHero;
            if (!Game.IsInGame || me.ClassID != ClassID.CDOTA_Unit_Hero_Abaddon) return;
            if (me.IsWaitingToSpawn) return;
            if (!Menu.Item("toggle").GetValue<bool>()) return;
            var q = me.Spellbook.SpellQ;
            var qlvl = me.Spellbook.SpellQ.Level;
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
            if (qdmg <= me.Health && me.Health != 1 && me.Health != 0) return;
            var closestUnit = ObjectManager.GetEntities<Unit>()
                .Where(x =>(!x.Equals(me) && ((x is Hero && !x.IsIllusion) || (x is Creep && x.IsSpawned)) && x.IsAlive && x.IsVisible))
                .OrderBy(x => x.Distance2D(ObjectManager.LocalHero))
                .FirstOrDefault();
            foreach (var modifier in ObjectManager.LocalHero.Modifiers)
            {
                if (modifier.Name.Contains("modifier_abaddon_aphotic_shield"))
                    return;
                q.UseAbility(closestUnit);
            }
        }
    }
}





