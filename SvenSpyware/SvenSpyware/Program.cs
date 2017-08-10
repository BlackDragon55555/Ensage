using System;
using System.Linq;
using Ensage;
using Ensage.Common.Extensions;
using Ensage.Common;
using Ensage.Common.Menu;
using System.Threading;

namespace SvenSpyware
{
    internal class Program
    {
        private static Ability stun, armor, ulti;
        private static Item Lompat, armlet, mjollnir, medallion, solar, soulRing, dust, bladeMail, bkb, abyssal,bloodthorn;
        private static readonly Menu Menu = new Menu("SvenRampage", "SvenRampage", true, "npc_dota_hero_sven", true);
        private static Hero me, target;
        private static bool combo;
        static void Main(string[] args)
        {
            Game.OnUpdate += Game_OnUpdate;
            Game.OnWndProc += Game_OnWndProc;
            var menu_utama = new Menu("Options", "opsi");
            menu_utama.AddItem(new MenuItem("enable", "enable").SetValue(true));
            menu_utama.AddItem(new MenuItem("ComboKey", "ComboKey").SetValue(new KeyBind('g', KeyBindType.Press)));
            menu_utama.AddItem(new MenuItem("useBKB", "useBKB").SetValue(true));
            Menu.AddSubMenu(menu_utama);
            Menu.AddToMainMenu();
        }

        public static void Game_OnUpdate(EventArgs args)
        {
            me = ObjectManager.LocalHero;

            if (!Game.IsInGame || Game.IsPaused || Game.IsWatchingGame)
                return;

            if (me.ClassId != ClassId.CDOTA_Unit_Hero_Sven)
                return;

            if (me == null)
                return;

            if (stun == null)
                stun = me.Spellbook.Spell1;
            if (armor == null)
                armor = me.Spellbook.Spell3;
            if (ulti == null)
                ulti = me.Spellbook.Spell4;

            if (Lompat == null)
                Lompat = me.FindItem("item_blink");
            if (bloodthorn == null)
                bloodthorn = me.FindItem("item_bloodthorn");
            if (armlet == null)
                armlet = me.FindItem("item_armlet");
            if (mjollnir == null)
                mjollnir = me.FindItem("item_mjollnir");
            if (dust == null)
                dust = me.FindItem("item_dust");
            if (bladeMail == null)
                bladeMail = me.FindItem("item_blade_mail");
            if (medallion == null)
                medallion = me.FindItem("item_medallion_of_courage");
            if (solar == null)
                solar = me.FindItem("item_solar_crest");
            if (soulRing == null)
                soulRing = me.FindItem("item_soul_ring");
            if (bkb == null)
                bkb = me.FindItem("item_black_king_bar");
            if (abyssal == null)
                abyssal = me.FindItem("item_abyssal_blade");

            if (combo && Menu.Item("enable").GetValue<bool>())
            {
                target = me.ClosestToMouseTarget(1000);
                if (target != null && target.IsAlive && !target.IsInvul() && !target.IsIllusion)
                {
                    if (me.CanAttack() && me.CanCast())
                    {

                        var linkens = target.Modifiers.Any(x => x.Name == "modifier_item_spheretarget") || target.Inventory.Items.Any(x => x.Name == "item_sphere");



                        if (bladeMail != null && bladeMail.CanBeCasted() && Utils.SleepCheck("blademail"))
                        {
                            bladeMail.UseAbility();
                            Utils.Sleep(150 + Game.Ping, "blademail");
                        }
                        if (bloodthorn != null && bloodthorn.CanBeCasted() && Utils.SleepCheck("bloodthorn"))
                        {
                            bloodthorn.UseAbility(target);
                            Utils.Sleep(150 + Game.Ping, "bloodthorn");
                        }

                        if (armlet != null && armlet.CanBeCasted() && Utils.SleepCheck("armlet1") && !armlet.IsToggled)
                        {
                            armlet.ToggleAbility();
                            Utils.Sleep(150 + Game.Ping, "armlet1");
                        }

                        if (mjollnir != null && mjollnir.CanBeCasted() && Utils.SleepCheck("mjollnir"))
                        {
                            mjollnir.UseAbility(me);
                            Utils.Sleep(150 + Game.Ping, "mjollnir");
                        }

                        if (ulti.CanBeCasted() && Utils.SleepCheck("ulti"))
                        {
                            ulti.UseAbility();
                            Utils.Sleep(150 + Game.Ping, "ulti");
                        }
                        if (armor.CanBeCasted() && Utils.SleepCheck("armor"))
                        {
                            armor.UseAbility();
                            Utils.Sleep(150 + Game.Ping, "armor");
                        }
                        Utils.ChainStun(me, 100, null, false);

                        if (bkb != null && bkb.CanBeCasted() && Utils.SleepCheck("bkb") && Menu.Item("useBKB").GetValue<bool>())
                        {
                            bkb.UseAbility();
                            Utils.Sleep(150 + Game.Ping, "bkb");
                        }

                        Utils.ChainStun(me, 200, null, false);

                        if (Lompat != null && Lompat.CanBeCasted() && me.Distance2D(target) > 300 && me.Distance2D(target) <= 1170 && Utils.SleepCheck("Lompat1"))
                        {
                            Lompat.UseAbility(target.Position);
                            Utils.Sleep(150 + Game.Ping, "Lompat1");
                        }

                        if (!ulti.CanBeCasted())
                            Utils.ChainStun(me, 200, null, false);

                        if (abyssal != null && abyssal.CanBeCasted() && Utils.SleepCheck("abyssal"))
                        {
                            abyssal.UseAbility(target);
                            Utils.Sleep(400 + Game.Ping, "abyssal");
                        }

                        if (abyssal != null)
                            Utils.ChainStun(me, 310, null, false);

                        if (medallion != null && medallion.CanBeCasted() && Utils.SleepCheck("medallion"))
                        {
                            medallion.UseAbility(target);
                            Utils.Sleep(150 + Game.Ping, "medallion");
                        }

                        if (solar != null && solar.CanBeCasted() && Utils.SleepCheck("solar"))
                        {
                            solar.UseAbility(target);
                            Utils.Sleep(200 + Game.Ping, "solar");
                        }

                        if (dust != null && dust.CanBeCasted() && (target.CanGoInvis() || target.IsInvisible()) && Utils.SleepCheck("dust"))
                        {
                            dust.UseAbility();
                            Utils.Sleep(200 + Game.Ping, "dust");
                        }

                        if (stun.CanBeCasted() && me.CanAttack() && !target.IsInvul() && Utils.SleepCheck("stun") && !linkens)
                        {
                            stun.UseAbility(target);
                            Utils.Sleep(150 + Game.Ping, "stun");
                        }

                        if (!stun.CanBeCasted() && Utils.SleepCheck("attack2"))
                        {
                            me.Attack(target);
                            Utils.Sleep(Game.Ping + 1000, "attack2");
                        }

                        if (armlet != null && Utils.SleepCheck("armlet") && me.CanCast() && armlet.IsToggled && (target == null || !target.IsAlive || !target.IsVisible))
                        {
                            armlet.ToggleAbility();
                            Utils.Sleep(150 + Game.Ping, "armlet");
                        }

                    }
                    else
                    {
                        if (armlet != null && !armlet.IsActivated)
                        {
                            armlet.ToggleAbility();
                        }
                        if (Utils.SleepCheck("attack1"))
                        {
                            me.Attack(target);
                            Utils.Sleep(1000, "attack1");
                        }
                    }
                }
            }
        }



        private static void Game_OnWndProc(WndEventArgs args)
        {
            if (!Game.IsChatOpen)
            {
                if (args.WParam == Menu.Item("ComboKey").GetValue<KeyBind>().Key)
                {
                    combo = true;
  
                }
                else
                {
                    combo = false;
                }

            }
        }

    }
}
