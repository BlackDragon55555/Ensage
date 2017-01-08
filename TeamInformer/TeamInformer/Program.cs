using System;
using Ensage;
using Ensage.Common.Menu;
using SharpDX;

namespace TeamInformer
{
    class Program
    {
        private static readonly Menu teaminformer = new Menu("TeamInformer", "menu", true);
        static void Main(string[] args)
        {
            teaminformer.AddItem(new MenuItem("tell","Inform Team" ).SetValue(true));
            teaminformer.AddItem(new MenuItem("print", "PrintMessage" ).SetValue(true));
            teaminformer.AddToMainMenu();
            Entity.OnParticleEffectAdded += ParticleDetector;
            Hero.OnModifierAdded += ModifierDetector;

        }

        private static void ModifierDetector(Unit sender, ModifierChangedEventArgs args)
        {
            if (args.Modifier.Name.Contains("charge_of_darkness_vision") && args.Modifier.Owner.UnitType == 1 && !args.Modifier.Owner.IsIllusion && args.Modifier.Owner.Team == ObjectManager.LocalHero.Team)
            {
                if (teaminformer.Item("tell").GetValue<bool>())
                {
                    Game.ExecuteCommand("say_team "+ args.Modifier.Owner.Name.Replace("npc_dota_hero_", "").Replace("_", " ") + " Charged");
                }
                if (teaminformer.Item("print").GetValue<bool>())
                {
                    Game.PrintMessage(args.Modifier.Owner.Name.Replace("npc_dota_hero_", "").Replace("_"," ") + " Charged",MessageType.ChatMessage);
                }
            }
        }

        private static async void ParticleDetector (Entity entity, ParticleEffectAddedEventArgs effect)
        {
            if (effect.Name.Contains("smoke_of_deceit"))
            {
                if (teaminformer.Item("tell").GetValue<bool>())
                {
                    Game.ExecuteCommand("say_team Smoke Detected");
                }
                if (teaminformer.Item("print").GetValue<bool>())
                {
                    Game.PrintMessage("Smoke Detected", MessageType.ChatMessage);
                }
            }
            else if (effect.Name.Contains("mirana_moonlight_cast"))
            {
                if (teaminformer.Item("tell").GetValue<bool>())
                {
                    Game.ExecuteCommand("say_team Moonlight Detected");
                }
                if (teaminformer.Item("print").GetValue<bool>())
                {
                    Game.PrintMessage("Moonlight Detected", MessageType.ChatMessage);
                }
            }
            else if (effect.Name.Contains("sandking_epicenter_tell"))
            {
                if (teaminformer.Item("tell").GetValue<bool>())
                {
                    Game.ExecuteCommand("say_team Epicenter Detected");
                }
                if (teaminformer.Item("print").GetValue<bool>())
                {
                    Game.PrintMessage("Epicenter Detected", MessageType.ChatMessage);
                }
            }
        }
    }
}
