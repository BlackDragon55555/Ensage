using System.Collections.Generic;
using System.Linq;
using Ensage;
using Ensage.Common.Menu;
using SharpDX;
using System;

namespace VisibleByEnemyDraw
{
    class Program
    {
        private static readonly Menu Menu = new Menu("VisibleByEnemy", "visibleByEnemy", true);

        private static readonly Dictionary<Unit, ParticleEffect> Effects = new Dictionary<Unit, ParticleEffect>();
        public static void Main(string[] args)
        {
            MenuItem item;

            item = new MenuItem("heroes", "Check allied heroes").SetValue(true);

            Menu.AddItem(item);

            item = new MenuItem("wards", "Check wards").SetValue(true);

            Menu.AddItem(item);

            item = new MenuItem("mines", "Check techies mines").SetValue(true);

            Menu.AddItem(item);

            item = new MenuItem("units", "Check controlled units (not lane creeps)").SetValue(true);

            Menu.AddItem(item);

            item = new MenuItem("buildings", "Check buildings").SetValue(true);

            Menu.AddItem(item);
            Menu.AddItem(new MenuItem("fontsize", "fontsize").SetValue(new Slider(15, 0, 100)));
            Menu.AddItem(new MenuItem("xpos", "xpos").SetValue(new Slider(-80, -200, 100)));
            Menu.AddItem(new MenuItem("ypos", "ypos").SetValue(new Slider(100, -100, 100)));
            Menu.AddToMainMenu();

            Drawing.OnDraw += Game_OnDraw;
        }

        private static void Game_OnDraw(EventArgs args)
        {
            var player = ObjectManager.LocalPlayer;
            if (player == null || player.Team == Team.Observer)
                return;
            
            var units = ObjectManager.GetEntities<Unit>().Where(
                x =>
             
                (Menu.Item("heroes").GetValue<bool>() && x is Hero && x.Team == player.Team)
             
                || (Menu.Item("wards").GetValue<bool>()
                    && (x.ClassId == ClassId.CDOTA_NPC_Observer_Ward
                        || x.ClassId == ClassId.CDOTA_NPC_Observer_Ward_TrueSight) && x.Team == player.Team)
               
                || (Menu.Item("mines").GetValue<bool>() && x.ClassId == ClassId.CDOTA_NPC_TechiesMines
                    && x.Team == player.Team)
                
                || (Menu.Item("units").GetValue<bool>() && !(x is Hero) && !(x is Building) && x.ClassId != ClassId.CDOTA_BaseNPC_Creep_Lane
                    && x.ClassId != ClassId.CDOTA_NPC_TechiesMines && x.ClassId != ClassId.CDOTA_NPC_Observer_Ward
                    && x.ClassId != ClassId.CDOTA_NPC_Observer_Ward_TrueSight && x.Team == player.Team)
                
                || (Menu.Item("buildings").GetValue<bool>() && x is Building && x.Team == player.Team)).ToList();


            foreach (var unit in units)
            {
                if (unit.IsVisibleToEnemies && unit.IsAlive)
                {
                    Vector2 screenPos;
                    var pos = unit.Position + new Vector3(0, 0, unit.HealthBarOffset);
                    Drawing.WorldToScreen(pos, out screenPos);
                    var start = screenPos + new Vector2(Menu.Item("xpos").GetValue<Slider>().Value, Menu.Item("ypos").GetValue<Slider>().Value);
                    var text = "Visible";
                    var textSize = Drawing.MeasureText(text, "Arial", new Vector2(10, 150), FontFlags.None);
                    var textPos = start + new Vector2(51 - textSize.X / 2, -textSize.Y / 2 + 2);
                    Drawing.DrawText(text, "Arial", textPos, new Vector2(Menu.Item("fontsize").GetValue<Slider>().Value, 150), Color.Red, FontFlags.AntiAlias | FontFlags.DropShadow);
                }
                else
                {

                }
            }
        }
    }
}
