//ONLY LOVE MazaiPC ;) 
using System;
using System.Linq;
using System.Collections.Generic;

using Ensage;
using SharpDX;
using Ensage.Common.Extensions;
using Ensage.Common;
using SharpDX.Direct3D9;
using System.Windows.Input;

namespace AbaddonDeny
{
    internal class Program
    {
        private static bool activated = true;
        private static bool already = false;
        private static bool toggle = true;
        private static bool loaded;
        private static Hero me;
        private static Hero target;
        private static ParticleEffect rangeDisplay;
        static void Main(string[] args)
        {
            Game.OnUpdate += Game_OnUpdate;
            Console.WriteLine("Abaddon Deny loaded!");
        }

        public static void Game_OnUpdate(EventArgs args)
        {
            var me = ObjectMgr.LocalHero;
            if (!Game.IsInGame || me.ClassID != ClassID.CDOTA_Unit_Hero_Abaddon)
            {
                return;
            }

            if (activated)
            {
                if (!me.IsAlive)
                {
                    already = false;
                }
                if (me.IsAlive)
                {
                    var Q = me.Spellbook.SpellQ;
                    var qlvl = me.Spellbook.SpellQ.Level;
                    var qdmg = 0;
                    if (qlvl <= 0)
                    {
                        qdmg = 0;
                    }

                    if (qlvl == 1)
                    {
                        qdmg = 75;
                    }
                    if (qlvl == 2)
                    {
                        qdmg = 100;
                    }
                    if (qlvl == 3)
                    {
                        qdmg = 125;
                    }
                    if (qlvl == 4)
                    {
                        qdmg = 150;
                    }

                    
                    if (me.Health <= qdmg && me.Health != 1 && me.Health != 0)
                    {
                        var closestUnit =
                            ObjectMgr.GetEntities<Unit>()
                                .Where(
                                    x =>
                                    (!x.Equals(me) && ((x is Hero && !x.IsIllusion) || (x is Creep && x.IsSpawned)) && x.IsAlive
                                        && x.IsVisible))
                                .OrderBy(x => x.Distance2D(ObjectMgr.LocalHero))
                                .FirstOrDefault();
                        if (already == false)
                        {
                            foreach (var modifier in ObjectMgr.LocalHero.Modifiers)
                            {
                                if (modifier.Name.Contains("modifier_abaddon_aphotic_shield"))
                                {
                                    return;
                                }
                            }
                            already = true;
                            Q.UseAbility(closestUnit);
                            
                        }
                    }
                }
            }
        }
    }
}



