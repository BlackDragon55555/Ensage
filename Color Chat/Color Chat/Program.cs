using System;

using Ensage;
using Ensage.Common.Menu;
using Ensage.Common;


namespace ChatColorAndEmoticon
{
    internal class Program
    {
        private static readonly Menu Menu = new Menu("Color Chat", "chat", true);
        static Random _r = new Random();
        static string Command;
        static string modspace;
        static string modifier;
        public static Boolean team;
        enum color
        {
            Olive,
            Pink,
            Red,
            Orange,
            DarkYellow,
            LightGreen,
            Purple,
            Grey,
            Green,
            Blue,
            White,
            Rainbow,
            HotPink,
            VibrantOrange,
            Violet,
            RedishPink
        }
        private static char[] Cyrillic = { 'A', 'a', 'B', 'b', 'C', 'c', 'D', 'd', 'E', 'e', 'F', 'f', 'G', 'g', 'H', 'h', 'I', 'i', 'J', 'j', 'K', 'k', 'L', 'l', 'M', 'm', 'N', 'n', 'O', 'o', 'P', 'p', 'Q', 'q', 'R', 'r', 'S', 's', 'T', 't', 'U', 'u', 'V', 'v', 'W', 'w', 'X', 'x', 'Y', 'y', 'Z', 'z' };
        private static char[] Latin = { 'А', 'а', 'Б', 'б', 'Ц', 'ц', 'Д', 'д', 'Е', 'е', 'Ф', 'ф', 'Г', 'г', 'Ч', 'ч', 'И', 'и', 'Й', 'й', 'К', 'к', 'Л', 'л', 'М', 'м', 'Н', 'н', 'О', 'о', 'П', 'п', 'Я', 'я', 'Р', 'р', 'С', 'с', 'Т', 'т', 'У', 'у', 'В', 'в', 'Ш', 'ш', 'Х', 'х', 'Ы', 'ы', 'З', 'з' };
        private static string[] emo = { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
        private static string[] code = { ":wink:", ":blush:", ":cheeky:", ":cool:", ":crazy:", ":cry:", ":disapprove:", ":doubledamage:", ":facepalm:", ":happytears:", ":haste:", ":hex:", ":highfive:", ":huh:", ":hush:", ":illusion:", ":invisibility:", ":laugh:", ":rage:", ":regeneration:", ":sad:", ":sick:", ":sleeping:", ":smile:", ":surprise:", ":aaaah:", ":burn:", ":hide:", ":iceburn:", ":tears:", ":techies:", ":fail:", ":goodjob:", ":headshot:", ":heart:", ":horse:", ":grave:", ":ti4copper:", ":ti4bronze:", ":ti4silver:", ":ti4gold:", ":ti4platinum:", ":ti4diamond:", ":dac15_sad:", ":dac15_surprise:", ":dac15_upset:", ":dac15_angry:", ":dac15_embarrass:", ":dac15_fade:", ":dac15_fantastic:", ":dac15_tired:", ":dac15_water:", ":pup:", ":bts_bristle:", ":bts_godz:", ":bts_lina:", ":bts_merlini:", ":bts_rosh:", ":cocky:", ":devil:", ":happy:", ":thinking:", ":tp:", ":salty:", ":angel:", ":blink:", ":snot:", ":stunned:", ":disappear:", ":fire:", ":bountyrune:", ":troll:", ":gross:", ":ggdire:", ":ggradiant:", ":yolo:", ":throwgame:", ":aegis2015:", ":eyeroll:" };

        static void Main(string[] args)
        {

            Game.OnWndProc += Game_OnGameWndProc;
            var menu_utama = new Menu("Options", "opsi");
            menu_utama.AddItem(new MenuItem("EnableAssembly", "EnableAssembly").SetValue(true));
            menu_utama.AddItem(new MenuItem("Emoticon", "Emoticon").SetValue(true));
            menu_utama.AddItem(new MenuItem("Color", "Color").SetValue(new StringList(new[] { "Olive", "Pink", "Red", "Orange", "Dark Yellow", "Light Green", "Purple", "Grey", "Green", "Blue", "White", "Rainbow", "Hot Pink", "Vibrant Orange", "Violet", "Redish Pink" })));
            menu_utama.AddItem(new MenuItem("Russia", "Russia").SetValue(true));
            Menu.AddSubMenu(menu_utama);
            Menu.AddToMainMenu();
            Game.PrintMessage("Colored Chat by <font color='#ff1111'>Spyware293</font> Loaded !!", MessageType.LogMessage);


        }

        static string rand()
        {
            int n = _r.Next(15);

            switch (n)
            {
                case 1:
                    modspace = "10";
                    break;
                case 2:
                    modspace = "11";
                    break;
                case 3:
                    modspace = "12";
                    break;
                case 4:
                    modspace = "13";
                    break;
                case 5:
                    modspace = "14";
                    break;
                case 6:
                    modspace = "15";
                    break;
                case 7:
                    modspace = "16";
                    break;
                case 8:
                    modspace = "17";
                    break;
                case 9:
                    modspace = "18";
                    break;
                case 10:
                    modspace = "19";
                    break;
                case 11:
                    modspace = "0E";
                    break;
                case 12:
                    modspace = "0F";
                    break;
                case 13:
                    modspace = "1A";
                    break;
                case 14:
                    modspace = "1C";
                    break;
                default:
                    modspace = "12";
                    break;
            }
            return modspace;

        }
        public static string Emotikon(string tukaremotikon)
        {
            for (int i = 0; i <= emo.Length - 1; i++)
            {
                tukaremotikon = tukaremotikon.Replace(code[i], emo[i]);
            }
            return tukaremotikon;
        }
        public static string Romanize(string russian)
        {
            bool isnotfound = false;
            foreach (char c in russian)
            {
                try
                {
                    int a = Array.IndexOf(Cyrillic, c);
                    russian = russian.Replace(Cyrillic[a], Latin[a]);
                }
                catch
                {
                    isnotfound = true;
                }
                if (isnotfound) continue;
            }
            return russian;
        }
        public static void Game_OnGameWndProc(WndEventArgs args)
        {
            if (Game.IsChatOpen && Menu.Item("EnableAssembly").GetValue<bool>())
            {
                if (args.Msg == 0x0101 && args.WParam == 0x0D)
                {

                    if (Game.IsKeyDown(0x10)) team = false;
                    else team = true;
                }
                if (Menu.Item("Color").GetValue<StringList>().SelectedIndex == (int)color.Olive)
                {
                    modifier = "10";
                }
                if (Menu.Item("Color").GetValue<StringList>().SelectedIndex == (int)color.Pink)
                {
                    modifier = "11";
                }
                if (Menu.Item("Color").GetValue<StringList>().SelectedIndex == (int)color.Red)
                {
                    modifier = "12";
                }
                if (Menu.Item("Color").GetValue<StringList>().SelectedIndex == (int)color.Orange)
                {
                    modifier = "13";
                }
                if (Menu.Item("Color").GetValue<StringList>().SelectedIndex == (int)color.DarkYellow)
                {
                    modifier = "14";
                }
                if (Menu.Item("Color").GetValue<StringList>().SelectedIndex == (int)color.LightGreen)
                {
                    modifier = "15";
                }
                if (Menu.Item("Color").GetValue<StringList>().SelectedIndex == (int)color.Purple)
                {
                    modifier = "16";
                }
                if (Menu.Item("Color").GetValue<StringList>().SelectedIndex == (int)color.Grey)
                {
                    modifier = "17";
                }
                if (Menu.Item("Color").GetValue<StringList>().SelectedIndex == (int)color.Green)
                {
                    modifier = "18";
                }
                if (Menu.Item("Color").GetValue<StringList>().SelectedIndex == (int)color.Blue)
                {
                    modifier = "19";
                }
                if (Menu.Item("Color").GetValue<StringList>().SelectedIndex == (int)color.White)
                {
                    modifier = "0B";
                }
                if (Menu.Item("Color").GetValue<StringList>().SelectedIndex == (int)color.Rainbow)
                {
                    modifier = rand();
                }
                if (Menu.Item("Color").GetValue<StringList>().SelectedIndex == (int)color.HotPink)
                {
                    modifier = "0E";
                }
                if (Menu.Item("Color").GetValue<StringList>().SelectedIndex == (int)color.VibrantOrange)
                {
                    modifier = "0F";
                }
                if (Menu.Item("Color").GetValue<StringList>().SelectedIndex == (int)color.Violet)
                {
                    modifier = "1A";
                }
                if (Menu.Item("Color").GetValue<StringList>().SelectedIndex == (int)color.RedishPink)
                {
                    modifier = "1C";
                }

                if (args.Msg == 256)
                {
                    if (args.WParam == 13)
                    {

                        int hexnum = Int32.Parse(modifier, System.Globalization.NumberStyles.HexNumber);
                        string stringmodifier = Char.ConvertFromUtf32(hexnum);
                        if (Command == "")
                        {
                            return;
                        }
                        if (Menu.Item("Emoticon").GetValue<bool>())
                        {
                            Command = Emotikon(Command);
                        }
                        if (Menu.Item("Russia").GetValue<bool>())
                        {
                            Command = Romanize(Command);
                        }
                        if (Menu.Item("Color").GetValue<StringList>().SelectedIndex == (int)color.Rainbow)
                        {
                            Command = space(Command);
                        }

                        Game.ExecuteCommand(((team) ? "say_team " : "say ") + stringmodifier + Command);


                        Command = "";

                        return;

                    }
                    if (args.WParam == 8)
                    {
                        Command = Command.Substring(0, Command.Length - 1);
                        return;
                    }
                    if (args.WParam == 32)
                    {
                        Command += " ";
                        return;
                    }
                    if (args.WParam == 186)
                    {
                        if (Game.IsKeyDown(16))
                        {
                            Command += ":";
                            return;
                        }
                        else
                            Command += ";";
                        return;
                    }
                    if (args.WParam == 222)
                    {
                        if (Game.IsKeyDown(16))
                        {
                            Command += "\"";
                            return;
                        }
                        else
                            Command += "'";
                        return;
                    }
                    if (args.WParam == 191 || args.WParam == 193)
                    {
                        if (Game.IsKeyDown(16))
                        {
                            Command += "?";
                            return;
                        }
                        else
                            Command += "/";
                        return;
                    }
                    if (args.WParam == 190)
                    {
                        if (Game.IsKeyDown(16))
                        {
                            Command += ">";
                            return;
                        }
                        else
                            Command += ".";
                        return;
                    }
                    if (args.WParam == 188)
                    {
                        if (Game.IsKeyDown(16))
                        {
                            Command += "<";
                            return;
                        }
                        else
                            Command += ",";
                        return;
                    }
                    if (args.WParam == 49)
                    {
                        if (Game.IsKeyDown(16))
                        {
                            Command += "!";
                            return;
                        }
                        else
                            Command += "1";
                        return;
                    }
                    if (args.WParam == 50)
                    {
                        if (Game.IsKeyDown(16))
                        {
                            Command += "@";
                            return;
                        }
                        else
                            Command += "2";
                        return;
                    }
                    if (args.WParam == 51)
                    {
                        if (Game.IsKeyDown(16))
                        {
                            Command += "#";
                            return;
                        }
                        else
                            Command += "3";
                        return;
                    }
                    if (args.WParam == 52)
                    {
                        if (Game.IsKeyDown(16))
                        {
                            Command += "$";
                            return;
                        }
                        else
                            Command += "4";
                        return;
                    }
                    if (args.WParam == 53)
                    {
                        if (Game.IsKeyDown(16))
                        {
                            Command += "%";
                            return;
                        }
                        else
                            Command += "5";
                        return;
                    }
                    if (args.WParam == 54)
                    {
                        if (Game.IsKeyDown(16))
                        {
                            Command += "^";
                            return;
                        }
                        else
                            Command += "6";
                        return;
                    }
                    if (args.WParam == 55)
                    {
                        if (Game.IsKeyDown(16))
                        {
                            Command += "&";
                            return;
                        }
                        else
                            Command += "7";
                        return;
                    }
                    if (args.WParam == 56)
                    {
                        if (Game.IsKeyDown(16))
                        {
                            Command += "*";
                            return;
                        }
                        else
                            Command += "8";
                        return;
                    }
                    if (args.WParam == 57)
                    {
                        if (Game.IsKeyDown(16))
                        {
                            Command += "(";
                            return;
                        }
                        else
                            Command += "9";
                        return;
                    }
                    if (args.WParam == 48)
                    {
                        if (Game.IsKeyDown(16))
                        {
                            Command += ")";
                            return;
                        }
                        else
                            Command += "0";
                        return;
                    }
                    if (args.WParam == 189)
                    {
                        if (Game.IsKeyDown(16))
                        {
                            Command += "_";
                            return;
                        }
                        else
                            Command += "-";
                        return;
                    }
                    if (args.WParam == 187)
                    {
                        if (Game.IsKeyDown(16))
                        {
                            Command += "+";
                            return;
                        }
                        else
                            Command += "=";
                        return;
                    }
                    if (args.WParam == 192)
                    {
                        if (Game.IsKeyDown(16))
                        {
                            Command += "~";
                            return;
                        }
                        else
                            Command += "`";
                        return;
                    }

                    if (args.WParam == 45 || args.WParam == 46 || args.WParam == 19 || args.WParam == 36 || args.WParam == 35 || args.WParam == 34 || args.WParam == 33 || args.WParam == 93 || args.WParam == 91 || args.WParam == 17 || args.WParam == 9 || args.WParam == 16 || args.WParam == 112 || args.WParam == 113 || args.WParam == 114 || args.WParam == 115 || args.WParam == 116 || args.WParam == 117 || args.WParam == 118 || args.WParam == 119 || args.WParam == 120 || args.WParam == 121 || args.WParam == 122 || args.WParam == 123)
                    {
                        return;
                    }
                    if (args.WParam == 27)
                    {
                        Command = "";
                        return;
                    }

                    if (args.WParam == 107)
                    {
                        Command = "+";
                        return;
                    }
                    if (args.WParam == 109)
                    {
                        Command = "-";
                        return;
                    }
                    if (args.WParam == 106)
                    {
                        Command = "*";
                        return;
                    }
                    if (args.WParam == 219)
                    {
                        Command = "é";
                        return;
                    }
                    if (Game.IsKeyDown(16))
                    {
                        Command += Utils.KeyToText((uint)args.WParam).ToUpper();

                        return;
                    }
                    else
                        Command += Utils.KeyToText((uint)args.WParam).ToLower();



                    return;


                }
            }
        }

        private static string space(string Command)
        {
            char[] array = Command.ToCharArray();
            Command = "";
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == ' ')
                {
                    string modifiers = rand();
                    int hexnum = Int32.Parse(modifiers, System.Globalization.NumberStyles.HexNumber);
                    string stringmodifier = char.ConvertFromUtf32(hexnum);
                    Command += array[i] + stringmodifier;

                }
                else
                {
                    Command += array[i];
                }
            }
            return Command;
        }
    }
}


