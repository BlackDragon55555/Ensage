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
        public static List<Vector2> Pos
        {
            get { return pos; }
            set { pos = value; }
        }
        static void Main(string[] args)
        {
            Menu.AddItem(new MenuItem("color", "Marker Color").SetValue(new StringList(new[] { "Blue", "Teal", "Purple", "Yellow","Orange","Pink","Gray","Light Blue","Green","Brown" })));
            Menu.AddItem(new MenuItem("delay", "Delay before mark gone").SetValue(new Slider(2000, 1000, 5000)));
            Menu.AddToMainMenu();
            font = new Font(
                Drawing.Direct3DDevice9,
                new FontDescription
                {
                    FaceName = "Arial", Height = 20, OutputPrecision = FontPrecision.Character, Quality = FontQuality.ClearTypeNatural, CharacterSet = FontCharacterSet.Ansi, MipLevels = 3, PitchAndFamily = FontPitchAndFamily.Modern, Weight = FontWeight.Heavy, Width = 12
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
            //  
            try
            {
                foreach (Vector2 position in Pos.ToList())
                {
                    
                    font.DrawText(null, "+", (int)position.X - 13, (int)position.Y - 10, colorselect[Menu.Item("color").GetValue<StringList>().SelectedValue]);
                }
                
            }
            catch
            {
                Console.WriteLine("Error Drawing Text");
            }
        }
        private static async void remover ()
        {
            await Task.Delay(Menu.Item("delay").GetValue<Slider>().Value);
            Pos.RemoveAt(0);
        }

        private static void Skill_entity(EntityEventArgs args)
        {
            try
            {
                if (!args.Entity.Name.Contains("npc_dota_creep") && !args.Entity.Name.Contains("npc_dota_neutral") && !args.Entity.Name.Contains("npc_dota_badguys") && !args.Entity.Name.Contains("npc_dota_goodguys") && !args.Entity.Name.Contains("dota_scene_entity") && !args.Entity.Name.Contains("point_hmd") && !args.Entity.Name.Contains("dota_item") && !args.Entity.Name.Contains("ent_dota_tree") && !args.Entity.Name.Contains("dota_world") && !args.Entity.Name.Contains("ambient_") && args.Entity.NetworkPosition.X != 0 && !args.Entity.Name.Contains("npc_dota_hero_") && !args.Entity.Name.Contains("effigy") && !args.Entity.Name.Contains("prop_dynamic") && !args.Entity.Name.Contains("world_layer") && !args.Entity.NetworkName.Contains("Spawner") && !args.Entity.Name.Contains("dota_shop") && !args.Entity.Name.Contains("fountain") && !args.Entity.Name.Contains("halloffame") && !args.Entity.Name.Contains("info_player") && !args.Entity.Name.Contains("info_particle") && !args.Entity.Name.Contains("Camera") && !args.Entity.Name.Contains("tonemap") && args.Entity.NetworkName != "CDOTAPlayer" && !args.Entity.Owner.IsVisible)
                {
                   // Game.PrintMessage(args.Entity.Name + " " + args.Entity.NetworkName + " " + args.En);
                    minimap_pos2d = HUDInfo.WorldToMinimap(args.Entity.NetworkPosition);
                    Pos.Add(minimap_pos2d);
                    //  Game.PrintMessage(minimap_pos2d.X + " " + minimap_pos2d.Y);
                    remover();
                }
            }
            catch
            {
                minimap_pos2d = HUDInfo.WorldToMinimap(args.Entity.NetworkPosition);
                Pos.Add(minimap_pos2d);
                remover();
            }            
        }
    }
}
