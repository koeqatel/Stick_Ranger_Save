using System;
using System.Drawing;
using System.Media;
using System.IO;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows.Forms;


namespace Stick_Ranger
{
    public class Program
    {
        public static List<Team> files = new List<Team>();
        public static string Path = Environment.GetFolderPath(Environment.SpecialFolder.System).Substring(0, 1) + @":\Users\" + Environment.UserName + @"\AppData\Roaming\SnSStudio\Stick Ranger\Stick Ranger.txt";
        public static List<string> ClassList = new List<string>();
        public static List<Team> Teams = new List<Team>();

        public static string Input1;
        public static string Input2;
        public static string Input3;
        public static string Input4;
        public static string Input5;


        [STAThread]
        static void Main(string[] args)
        {
            string line;
            System.IO.StreamReader AddFile = new System.IO.StreamReader(Path);
            while ((line = AddFile.ReadLine()) != null)
            {
                string[] parts = line.Split(';');
                if (parts.Length == 5)
                {
                    files.Add(new Team(Convert.ToString(parts[0]),
                        Convert.ToString(parts[1]),
                        Convert.ToString(parts[2]),
                        Convert.ToString(parts[3]),
                        Convert.ToString(parts[4])));
                }
            }
            AddFile.Close();
            Program.Menu();
        }
        public static void Menu()
        {
            Console.Clear();
            Console.WriteLine("Hello, what would you like to do?");
            Console.WriteLine();
            Console.WriteLine("1. Add item.");
            Console.WriteLine("2. Update item.");
            Console.WriteLine("3. Pick item.");
            Console.WriteLine("4. Delete item.");
            Console.WriteLine("5. Exit.");
            ConsoleKeyInfo ans = new ConsoleKeyInfo();
            ans = Console.ReadKey();
            switch (ans.KeyChar.ToString())
            {
                case "1":
                    Program.Add();
                    break;
                case "2":
                    Program.Update();
                    break;
                case "3":
                    Program.Pick();
                    break;
                case "4":
                    Program.Delete();
                    break;
                case "5":
                    Environment.Exit(0);
                    break;
                default:
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n{0} is no correct input!", (ans.KeyChar.ToString()));
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.ReadLine();
                    Console.Clear();
                    Program.Menu();
                    break;
            }
            Console.ReadLine();
        }
        public static void Add()
        {
            #region Classes
            ClassList.Add("boxer");
            ClassList.Add("gladiator");
            ClassList.Add("sniper");
            ClassList.Add("magician");
            ClassList.Add("priest");
            ClassList.Add("gunner");
            ClassList.Add("whipper");
            ClassList.Add("angel");
            #endregion

            Input1 = null;
            Input2 = null;
            Input3 = null;
            Input4 = null;
            Input5 = null;

            while (Input1 == null)
            {
                Console.Clear();
                Console.WriteLine("\bCould you give me the class of your first man?");
                Input1 = Console.ReadLine();
                if (!ClassList.Contains(Input1.ToLower()))
                {
                    Input1 = null;
                    WrongName();
                }
            }

            while (Input2 == null)
            {
                Console.Clear();
                Console.WriteLine("Could you give me the class of your second man?");
                Input2 = Console.ReadLine();
                if (!ClassList.Contains(Input2.ToLower()))
                {
                    Input2 = null;
                    WrongName();
                }
            }

            while (Input3 == null)
            {
                Console.Clear();
                Console.WriteLine("Could you give me the class of your third man?");
                Input3 = Console.ReadLine();
                if (!ClassList.Contains(Input3.ToLower()))
                {
                    Input3 = null;
                    WrongName();
                }
            }

            while (Input4 == null)
            {
                Console.Clear();
                Console.WriteLine("Could you give me the class of your last man?");
                Input4 = Console.ReadLine();
                if (!ClassList.Contains(Input4.ToLower()))
                {
                    Input4 = null;
                    WrongName();
                }
            }
            if (Input5 == null)
            {
                Console.Clear();
                Console.WriteLine("And could you give me your current save code?");
                Input5 = Console.ReadLine();
            }
            System.IO.File.WriteAllText(Path, System.IO.File.ReadAllText(Path) + String.Format("{0};{1};{2};{3};{4}", Input1, Input2, Input3, Input4, Input5) + Environment.NewLine);
            files.Add(new Team(Input1, Input2, Input3, Input4, Input5));
            Menu();
        }
        public static void WrongName()
        {
            int i = 0;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Make sure you picked one of these.\n");
            Console.ForegroundColor = ConsoleColor.Gray;
            foreach (string Class in ClassList)
            {
                i++;
                Console.WriteLine(i + ". " + Class);
            }
            Console.ReadLine();
            Console.Clear();
        }
        public static void Update()
        {
            int i = 0;
            string ans = null;
            int ansInt = 0;

            Console.Clear();
            Console.WriteLine("\bWhich one would you like to update?");
            string line;
            System.IO.StreamReader AddFile = new System.IO.StreamReader(Path);
            while ((line = AddFile.ReadLine()) != null)
            {
                string[] parts = line.Split(';');
                if (parts.Length == 5)
                {
                    Console.WriteLine(i + 1 + ": Class 1: {0, -9}" + " Class 2: {1, -9}" + " Class 3: {2, -9}" + " Class 4: {3, -9}",
                         Convert.ToString(parts[0]),
                         Convert.ToString(parts[1]),
                         Convert.ToString(parts[2]),
                         Convert.ToString(parts[3]));
                    i++;
                }
            }
            AddFile.Close();

            while (ans == null)
            {
                ans = Console.ReadLine();
                if (!int.TryParse(ans, out ansInt))
                {
                    Console.WriteLine("Not a number!");
                    ans = null;
                }
                if (files.Count < ansInt)
                {
                    Console.WriteLine("That one doesn't exist");
                    ans = null;
                }
            }

            Console.Clear();
            Console.WriteLine("Could you give me your current save code for number {0}?", ans);
            string NewCode = Console.ReadLine();
            files[ansInt - 1].saveCode = NewCode;
            System.IO.File.WriteAllText(Path, "");
            foreach (var file in files)
            {
                System.IO.File.WriteAllText(Path, System.IO.File.ReadAllText(Path) + String.Format("{0};{1};{2};{3};{4}", file.firstName, file.secondName, file.thirdName, file.lastName, file.saveCode) + Environment.NewLine);
            }
            Menu();
        }
        public static void Pick()
        {
            Console.Clear();
            Console.WriteLine("\bThese are the current items");

            int i = 0;
            int ansInt = 0;
            string line;
            string ans = null;
            System.IO.StreamReader AddFile = new System.IO.StreamReader(Path);
            while ((line = AddFile.ReadLine()) != null)
            {
                string[] parts = line.Split(';');
                if (parts.Length == 5)
                {
                    Console.WriteLine(i + 1 + ": Class 1: {0, -9}" + " Class 2: {1, -9}" + " Class 3: {2, -9}" + " Class 4: {3, -9}",
                         Convert.ToString(parts[0]),
                         Convert.ToString(parts[1]),
                         Convert.ToString(parts[2]),
                         Convert.ToString(parts[3]));
                    i++;
                }
            }
            AddFile.Close();

            while (ans == null)
            {
                ans = Console.ReadLine();
                if (!int.TryParse(ans, out ansInt))
                {
                    Console.WriteLine("Not a number!");
                    ans = null;
                }
                if (files.Count < ansInt)
                {
                    Console.WriteLine("That one doesn't exist");
                    ans = null;
                }
            }
            Console.Clear();
            Clipboard.SetText(files[ansInt - 1].saveCode);


            Menu();

            Console.ReadLine();
            Program.Menu();
        }
        public static void Delete()
        {
            int i = 0;
            string ans = null;
            int ansInt = 0;

            Console.Clear();
            Console.WriteLine("\bWhich one would you like to delete?");
            string line;
            System.IO.StreamReader AddFile = new System.IO.StreamReader(Path);
            while ((line = AddFile.ReadLine()) != null)
            {
                string[] parts = line.Split(';');
                if (parts.Length == 5)
                {
                    Console.WriteLine(i + 1 + ": Class 1: {0, -9}" + " Class 2: {1, -9}" + " Class 3: {2, -9}" + " Class 4: {3, -9}",
                         Convert.ToString(parts[0]),
                         Convert.ToString(parts[1]),
                         Convert.ToString(parts[2]),
                         Convert.ToString(parts[3]));
                    i++;
                }
            }
            AddFile.Close();

            while (ans == null)
            {
                ans = Console.ReadLine();
                if (!int.TryParse(ans, out ansInt))
                {
                    Console.WriteLine("Not a number!");
                    ans = null;
                }
                if (files.Count < ansInt)
                {
                    Console.WriteLine("That one doesn't exist");
                    ans = null;
                }
            }
            Console.Clear();
            string Answer = null;
            while (Answer == null)
            {
                Console.WriteLine("Are you sure you want to delete {0}", ans);
                Answer = Console.ReadLine();
                if (Answer == "Yes")
                {
                    files.Remove(files[ansInt - 1]);
                    System.IO.File.WriteAllText(Path, "");
                    foreach (var file in files)
                    {
                        System.IO.File.WriteAllText(Path, System.IO.File.ReadAllText(Path) + String.Format("{0};{1};{2};{3};{4}", file.firstName, file.secondName, file.thirdName, file.lastName, file.saveCode) + Environment.NewLine);
                    }
                    Console.WriteLine("Deleted: {0}", ans);
                    Console.ReadLine();
                }
                else if (Answer == "No")
                {
                    Menu();
                }
                else
                {
                    Console.WriteLine("{0} is not an option.", Answer);
                    Answer = null;
                }
            }
            Menu();
        }
    }
}