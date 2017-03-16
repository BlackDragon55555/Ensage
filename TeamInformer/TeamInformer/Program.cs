using Ensage;
using Ensage.Common.Menu;
using System.Collections.Generic;

namespace TeamInformer
{
    class Program
    {
        private static readonly Menu teaminformer = new Menu("TeamInformer", "menu", true);
       
        static void Main(string[] args)
        {
            
            teaminformer.AddItem(new MenuItem("tell","Inform Team" ).SetValue(false));
            teaminformer.AddItem(new MenuItem("print", "PrintMessage" ).SetValue(true));
            teaminformer.AddItem(new MenuItem("humanizer", "humanizer").SetValue(true));
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
                    if (teaminformer.Item("humanizer").GetValue<bool>())
                    {
                        humanizer("Charge",args.Modifier.Owner.Name.Replace("npc_dota_hero_", "").Replace("_", " "));
                    }
                    else
                    {
                        Game.ExecuteCommand("say_team " + args.Modifier.Owner.Name.Replace("npc_dota_hero_", "").Replace("_", " ") + " Charged");
                    }
                }
                if (teaminformer.Item("print").GetValue<bool>())
                {
                    Game.PrintMessage(args.Modifier.Owner.Name.Replace("npc_dota_hero_", "").Replace("_"," ") + " Charged",MessageType.ChatMessage);
                }
            }
        }
        private static void humanizer (string say, string target)
        {
            string[] humanwords = {
            "Enemy is using",
            "Careful,",
            "",
            "Care",
            "I Think they use"
            };
            System.Random humanizer = new System.Random();
            int words = humanizer.Next(0,humanwords.Length);
            string targetcharge = (target.Length <= 0 ? " to" + target : "");
            Game.ExecuteCommand("say_team " + humanwords[words] + " " + say + targetcharge);
        }
        private static void ParticleDetector (Entity entity, ParticleEffectAddedEventArgs effect)
        {

            //Game.PrintMessage(effect.Name, MessageType.ChatMessage);
            if (effect.Name.Contains("smoke_of_deceit"))
            {
                if (teaminformer.Item("tell").GetValue<bool>())
                {
                    if (teaminformer.Item("humanizer").GetValue<bool>())
                    {
                        humanizer("Smoke","");
                    }
                    else
                    {
                        Game.ExecuteCommand("say_team Smoke Detected");
                    }
                }
                if (teaminformer.Item("print").GetValue<bool>())
                {
                    Game.PrintMessage("Smoke Detected", MessageType.ChatMessage);
                }
            }
            else if (effect.Name.Contains("mirana_moonlight_cast") && effect.ParticleEffect.Owner.Team != ObjectManager.LocalHero.Team)
            {
                if (teaminformer.Item("tell").GetValue<bool>())
                {
                    if (teaminformer.Item("humanizer").GetValue<bool>())
                    {
                        humanizer("Moonlight","");
                    }
                    else
                    {
                        Game.ExecuteCommand("say_team Moonlight Detected");
                    }
                }
                if (teaminformer.Item("print").GetValue<bool>())
                {
                    Game.PrintMessage("Moonlight Detected", MessageType.ChatMessage);
                }
            }
            else if (effect.Name.Contains("sandking_epicenter_tell") && effect.ParticleEffect.Owner.Team != ObjectManager.LocalHero.Team)
            {
                if (teaminformer.Item("tell").GetValue<bool>())
                {
                    if (teaminformer.Item("humanizer").GetValue<bool>())
                    {
                        humanizer("Epicenter","");
                    }
                    else
                    {
                        Game.ExecuteCommand("say_team Epicenter Detected");
                    }
                }
                if (teaminformer.Item("print").GetValue<bool>())
                {
                    Game.PrintMessage("Epicenter Detected", MessageType.ChatMessage);
                }
            }
        }
    }
}
