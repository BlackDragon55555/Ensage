using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ensage;
using Ensage.Common;
using SharpDX;
using SharpDX.Direct3D9;
using Ensage.Common.Menu;

namespace Skill_Locator
{
    class Program
    {
        static private Vector2 minimap_pos2d { get; set; }
        
        private static Font font;
        private static readonly Menu Menu = new Menu("SkillLocator", "SkillLocator", true);
        private static List<Vector2> pos = new List<Vector2>();
        static Dictionary<string, Color> colorselect = new Dictionary<string, Color>()
        {
            {"Red", Color.Red },
            {"Blue",Color.Blue },
            {"Teal",Color.Teal },
            {"Purple",Color.Purple },
            {"Yellow",Color.Yellow },
            {"Orange",Color.Orange },
            {"Pink",Color.Pink },
            {"Gray",Color.Gray },
            {"Light Blue",Color.LightBlue },
            {"Green",Color.Green },
            {"Brown",Color.Brown }
            
        };
      //  public static List<Vector2> Pos
       // {
        //    get { return pos; }
         //   set { pos = value; }
       // }
        static void Main(string[] args)
        {
            Menu.AddItem(new MenuItem("color", "Marker Color").SetValue(new StringList(new[] { "Red", "Blue", "Teal", "Purple", "Yellow","Orange","Pink","Gray","Light Blue","Green","Brown" })));
            Menu.AddItem(new MenuItem("delay", "Delay before mark gone").SetValue(new Slider(2000, 1000, 5000)));
            Menu.AddItem(new MenuItem("fontsize", "Change Size (Reload after change)").SetValue(new Slider(0, 0, 10)));
            Menu.AddItem(new MenuItem("calibrate", "Calibrate Mode").SetValue(false));
            Menu.AddItem(new MenuItem("recx", "Recalibrate X Axis").SetValue(new Slider(-13, -20, 20)));
            Menu.AddItem(new MenuItem("recy", "Recalibrate Y Axis").SetValue(new Slider(-10, -20, 20)));
            Menu.AddToMainMenu();
            font = new Font(
                Drawing.Direct3DDevice9,
                new FontDescription
                {
                    FaceName = "Arial", Height = 20 + Menu.Item("fontsize").GetValue<Slider>().Value, OutputPrecision = FontPrecision.Character, Quality = FontQuality.ClearTypeNatural, CharacterSet = FontCharacterSet.Ansi, MipLevels = 3, PitchAndFamily = FontPitchAndFamily.Modern, Weight = FontWeight.Heavy, Width = 12 + Menu.Item("fontsize").GetValue<Slider>().Value
                });
                
            ObjectManager.OnAddEntity += Skill_entity;
            Drawing.OnEndScene += draw;
            Drawing.OnPreReset += Drawing_OnPreReset;
            Drawing.OnPostReset += Drawing_OnPostReset;

        }
        static void Drawing_OnPostReset(EventArgs args)
        {
            font.OnResetDevice();
        }

        static void Drawing_OnPreReset(EventArgs args)
        {
            font.OnLostDevice();
        }
        private static void draw(EventArgs args)
        {
            if (Menu.Item("calibrate").GetValue<bool>())
            {
                Vector2 positiondebug = HUDInfo.WorldToMinimap(ObjectManager.LocalHero.NetworkPosition);
                font.DrawText(null, "X", (int)positiondebug.X + Menu.Item("recx").GetValue<Slider>().Value, (int)positiondebug.Y + Menu.Item("recy").GetValue<Slider>().Value, colorselect[Menu.Item("color").GetValue<StringList>().SelectedValue]);
            }
            try
            {
                foreach (Vector2 position in pos.ToList())
                {
                    
                    font.DrawText(null, "X", (int)position.X + Menu.Item("recx").GetValue<Slider>().Value, (int)position.Y + Menu.Item("recy").GetValue<Slider>().Value, colorselect[Menu.Item("color").GetValue<StringList>().SelectedValue]);
                }
                
            }
            catch
            {
                Console.WriteLine("Error Drawing Text");
            }
        }
        private static async void remover (Vector2 val)
        {
            await Task.Delay(Menu.Item("delay").GetValue<Slider>().Value);
            pos.RemoveAt(0);
            if (pos.Contains(val))
            {
                pos.Remove(val);
            }
        }

        private static void Skill_entity(EntityEventArgs args)
        {
            try
            {
                if (!args.Entity.Name.Contains("npc_dota_creep") && !args.Entity.Name.Contains("npc_dota_neutral") && !args.Entity.Name.Contains("npc_dota_badguys") && !args.Entity.Name.Contains("npc_dota_goodguys") && !args.Entity.Name.Contains("dota_scene_entity") && !args.Entity.Name.Contains("point_hmd") && !args.Entity.Name.Contains("dota_item") && !args.Entity.Name.Contains("ent_dota_tree") && !args.Entity.Name.Contains("dota_world") && !args.Entity.Name.Contains("ambient_") && args.Entity.NetworkPosition.X != 0 && !args.Entity.Name.Contains("npc_dota_hero_") && !args.Entity.Name.Contains("effigy") && !args.Entity.Name.Contains("prop_dynamic") && !args.Entity.Name.Contains("world_layer") && !args.Entity.NetworkName.Contains("Spawner") && !args.Entity.Name.Contains("dota_shop") && !args.Entity.Name.Contains("fountain") && !args.Entity.Name.Contains("halloffame") && !args.Entity.Name.Contains("info_player") && !args.Entity.Name.Contains("info_particle") && !args.Entity.Name.Contains("Camera") && !args.Entity.Name.Contains("tonemap") && args.Entity.NetworkName != "CDOTAPlayer" && !args.Entity.Owner.IsVisible)
                {
                    //Game.PrintMessage(args.Entity.Name + " " + args.Entity.NetworkName);
                    minimap_pos2d = HUDInfo.WorldToMinimap(args.Entity.NetworkPosition);
                    pos.Add(minimap_pos2d);
                    //Game.PrintMessage(minimap_pos2d.X + " " + minimap_pos2d.Y);
                    remover(minimap_pos2d);
                }
            }
            catch
            {
                try
                {
                    if (!args.Entity.Name.Contains("Item_Physical") && args.Entity.Team != ObjectManager.LocalHero.Team)
                    {
                       // Game.PrintMessage("exception " + args.Entity.Name + " " + args.Entity.Team +args.Entity.IsVisibleForTeam(ObjectManager.LocalHero.Team));
                        minimap_pos2d = HUDInfo.WorldToMinimap(args.Entity.NetworkPosition);
                        pos.Add(minimap_pos2d);
                        remover(minimap_pos2d);
                    }
                }
                catch
                {
                   // Game.PrintMessage("exception2 " + args.Entity.Name);
                    minimap_pos2d = HUDInfo.WorldToMinimap(args.Entity.NetworkPosition);
                    pos.Add(minimap_pos2d);
                    remover(minimap_pos2d);
                }
            }            
        }
    }
}
