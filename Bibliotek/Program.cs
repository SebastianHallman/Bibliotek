using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace Bibliotek
{
    class Program
    {
        // Markus E och Sebastian and the parrot

        static void LäggTillBok()
        {
            // tar in bokens data och lägger till boken i boklistan
            StreamWriter skriv = new StreamWriter("data.txt", true);
            string temptitel, tempförfattare, tempstatus;

            Console.Write("Bokens titel: ");
            temptitel = Console.ReadLine();
            Console.Write("Bokens författare: ");
            tempförfattare = Console.ReadLine();
            Console.Write("Bokens status (true/false): ");
            tempstatus = Console.ReadLine();
            skriv.WriteLine(temptitel + "," + tempförfattare + "," + tempstatus);
            skriv.Close();

        }
        static void TaBortBok(List<Bok> BiblioteketsBöcker)
        {
            // söker efter boken och tar bort den
            StreamReader läs = new StreamReader("data.txt");


            string bok, s, temptitel;

            Console.Write("\tvilken bok vill du ta bort? ");
            bok = Console.ReadLine();

            while ((s = läs.ReadLine()) != null)
            {

                string[] data = s.Split(',');

                temptitel = data[0];
                

                // kollar om boken finns, isåfall tar den bort den
                if (String.Compare(temptitel, bok) == 0)
                {
                    // sparar all text i filen och skriver om filen utan den borttagna boken
                    läs.Close();
                    string RensaFil = null;
                    File.WriteAllText("data.txt", RensaFil);
                    StreamWriter skriv = new StreamWriter("data.txt", true);
                    for (int i = 0; BiblioteketsBöcker.Count > i; i++)
                    {
                        if (bok != BiblioteketsBöcker[i].TitelGetOrSet)
                        {
                            skriv.WriteLine(BiblioteketsBöcker[i].TitelGetOrSet + "," + BiblioteketsBöcker[i].FörfattareGetOrSet + "," + BiblioteketsBöcker[i].LånadGetOrSet);
                        }
                    }
                    skriv.Close();



                    goto Klar;
                }
            }
            Klar:
            läs.Close();

        }

        static void RaderaFörfattare(List<Bok> BiblioteketsBöcker)
        {
            // tar bort alla böcker av samma författare
            StreamReader läs = new StreamReader("data.txt");


            string författare, s, tempförfattare;

            Console.Write("\tvilken författare vill du radera? ");
            författare = Console.ReadLine();

            while ((s = läs.ReadLine()) != null)
            {

                string[] data = s.Split(',');


                tempförfattare = data[1];

                // samma procedur som i taBortBok() men med författarna
                if (String.Compare(tempförfattare, författare) == 0)
                {

                    läs.Close();
                    string RensaFil = null;
                    File.WriteAllText("data.txt", RensaFil);
                    StreamWriter skriv = new StreamWriter("data.txt", true);
                    for (int i = 0; BiblioteketsBöcker.Count > i; i++)
                    {
                        if (författare != BiblioteketsBöcker[i].FörfattareGetOrSet)
                        {
                            skriv.WriteLine(BiblioteketsBöcker[i].TitelGetOrSet + "," + BiblioteketsBöcker[i].FörfattareGetOrSet + "," + BiblioteketsBöcker[i].LånadGetOrSet);
                        }
                    }
                    skriv.Close();


                    goto Klar;
                }
            }
            Klar:
            läs.Close();

        }

        static List<Bok> UppdateraBibliotek()
        {
            // uppdaterar våran lista så att den stämmer överräns med bokfilen
            List<Bok> BiblioteksBöcker = new List<Bok>();
            StreamReader sr = new StreamReader("data.txt");
            string s, temptitel, tempförfattare, tempstatus;
            Console.WriteLine();
            while ((s = sr.ReadLine()) != null)
            {
                

                string[] data = s.Split(',');

                try
                {
                    temptitel = data[0];
                    tempförfattare = data[1];
                    tempstatus = data[2];
                }
                catch
                {
                    temptitel = "";
                    tempförfattare = "";
                    tempstatus = "false";
                }
                try { Bok nyBok = new Bok(temptitel, tempförfattare, bool.Parse(tempstatus)); BiblioteksBöcker.Add(nyBok); }
                catch { Bok nyBok = new Bok(temptitel, tempförfattare, false); BiblioteksBöcker.Add(nyBok); }
               

            }
            sr.Close();
            return BiblioteksBöcker;
        }

        static void SökBok()
        {
            // söker efter en bok och visar dem
            Console.Write("Vilken bok söker du? : ");
            string sök = Console.ReadLine();
            StreamReader sr = new StreamReader("data.txt");
            string s, temptitel, tempförfattare, tempstatus;
            int antal = 0;
            Console.WriteLine();

            while ((s = sr.ReadLine()) != null)
            {
                //  Console.Write(s);

                string[] data = s.Split(',');

                try
                {
                    temptitel = data[0];
                    tempförfattare = data[1];
                    tempstatus = data[2];
                }
                catch
                {
                    temptitel = "";
                    tempförfattare = "";
                    tempstatus = "false";
                }
                Bok hittadBok = new Bok(temptitel, tempförfattare, bool.Parse(tempstatus));

                if (hittadBok.TitelGetOrSet == sök)
                {
                    Console.WriteLine("Titel: " + hittadBok.TitelGetOrSet + ",\t\t\tFörfattare: " + hittadBok.FörfattareGetOrSet + ",\t\t\tLånad: " + hittadBok.LånadGetOrSet);
                    antal++;
                }


            }
            sr.Close();

            if (antal == 0)
            {
                Console.WriteLine("Ingen bok hittades");
            }
        }

        static void SökFörfattare()
        {

            // söker efter författare och visar deras böcker

            Console.Write("Vilken författare söker du? : ");
            string sök = Console.ReadLine();
            StreamReader sr = new StreamReader("data.txt");
            string s, temptitel, tempförfattare, tempstatus;
            int antal = 0;
            Console.WriteLine();

            while ((s = sr.ReadLine()) != null)
            {
                

                string[] data = s.Split(',');

                try
                {
                    temptitel = data[0];
                    tempförfattare = data[1];
                    tempstatus = data[2];
                }
                catch
                {
                    temptitel = "";
                    tempförfattare = "";
                    tempstatus = "false";
                }
                Bok hittadBok = new Bok(temptitel, tempförfattare, bool.Parse(tempstatus));

                if (hittadBok.FörfattareGetOrSet == sök)
                {
                    Console.WriteLine("Titel: " + hittadBok.TitelGetOrSet + ",\t\t\tFörfattare: " + hittadBok.FörfattareGetOrSet + ",\t\t\tLånad: " + hittadBok.LånadGetOrSet);
                    antal++;
                }


            }
            sr.Close();

            if (antal == 0)
            {
                Console.WriteLine("Ingen bok hittades");
            }
        }

        static void lånaBok()
        {
            // Om boken finns så kan man låna den, annars visar den att den är lånad
            Console.Write("Vilken bok vill du låna? : ");
            string sök = Console.ReadLine();
            StreamReader sr = new StreamReader("data.txt");

            string s, temptitel, tempförfattare, tempstatus;
            int antal = 0;
            Console.WriteLine();

            while ((s = sr.ReadLine()) != null)
            {
                

                string[] data = s.Split(',');

                try
                {
                    temptitel = data[0];
                    tempförfattare = data[1];
                    tempstatus = data[2];
                }
                catch
                {
                    temptitel = "";
                    tempförfattare = "";
                    tempstatus = "false";
                }
                Bok hittadBok = new Bok(temptitel, tempförfattare, bool.Parse(tempstatus));

                if (hittadBok.TitelGetOrSet == sök)
                {
                    antal++;

                    if (hittadBok.LånadGetOrSet == false)
                    {
                        hittadBok.LånadGetOrSet = true;
                        sr.Close();
                    
                        string[] lines = File.ReadAllLines("data.txt");
                        for (int i = 0; i < lines.Length; i++)
                        {
                            if (lines[i]== hittadBok.TitelGetOrSet + "," + hittadBok.FörfattareGetOrSet + ",false" || lines[i] == hittadBok.TitelGetOrSet + "," + hittadBok.FörfattareGetOrSet + ",False")
                            {
                                lines[i] = hittadBok.TitelGetOrSet + "," + hittadBok.FörfattareGetOrSet + "," + hittadBok.LånadGetOrSet;
                            }
                        }
                        File.WriteAllLines("data.txt", lines);
                    

                        if (hittadBok.LånadGetOrSet == true)
                        {
                            Console.WriteLine("Du har nu lånat " + hittadBok.TitelGetOrSet + " av " + hittadBok.FörfattareGetOrSet + "!");
                        }
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Boken är utlånad!");
                    }
                }
            }
            sr.Close();

            if (antal == 0)
            {
                Console.WriteLine("Ingen bok hittades");
            }
        }

        static void lämnaBok()
        {
            // Om boken är utlämnad kan man återlämna den
            Console.Write("Vilken bok vill du lämna tilbaka? : ");
            string sök = Console.ReadLine();
            StreamReader sr = new StreamReader("data.txt");

            string s, temptitel, tempförfattare, tempstatus;
            int antal = 0;
            Console.WriteLine();

            while ((s = sr.ReadLine()) != null)
            {
                

                string[] data = s.Split(',');

                try
                {
                    temptitel = data[0];
                    tempförfattare = data[1];
                    tempstatus = data[2];
                }
                catch
                {
                    temptitel = "";
                    tempförfattare = "";
                    tempstatus = "false";
                }
                Bok hittadBok = new Bok(temptitel, tempförfattare, bool.Parse(tempstatus));

                if (hittadBok.TitelGetOrSet == sök)
                {
                    antal++;

                    if (hittadBok.LånadGetOrSet == true)
                    {
                        hittadBok.LånadGetOrSet = false;
                        sr.Close();

                        string[] lines = File.ReadAllLines("data.txt");
                        for (int i = 0; i < lines.Length; i++)
                        {
                            if (lines[i] == hittadBok.TitelGetOrSet + "," + hittadBok.FörfattareGetOrSet + ",true" || lines[i] == hittadBok.TitelGetOrSet + "," + hittadBok.FörfattareGetOrSet + ",True")
                            {
                                lines[i] = hittadBok.TitelGetOrSet + "," + hittadBok.FörfattareGetOrSet + "," + hittadBok.LånadGetOrSet;
                            }
                        }
                        File.WriteAllLines("data.txt", lines);
                                              
                        Console.WriteLine("Du har nu lämnat tillbaka " + hittadBok.TitelGetOrSet + " av " + hittadBok.FörfattareGetOrSet + "!");
                        
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Boken är utlånad!");
                    }
                }
            }
            sr.Close();

            if (antal == 0)
            {
                Console.WriteLine("Ingen bok hittades");
            }
        }

        static void SkrivLista(List<Bok> BiblioteksBöcker)
        {
            for (int i = 0; i < BiblioteksBöcker.Count; i++)
            {
                Console.WriteLine("Titel: " + BiblioteksBöcker[i].TitelGetOrSet + ",\t\t\tFörfattare: " + BiblioteksBöcker[i].FörfattareGetOrSet + ",\t\t\tLånad: " + BiblioteksBöcker[i].LånadGetOrSet);
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Välkommen till Författarnas utbytesklubb");

            //Inläsning av alla böcker i systemet
            List<Bok> böcker = new List<Bok>();
            böcker = UppdateraBibliotek();

            Meny:
            Console.WriteLine("\nVad vill du göra hos oss idag?");
            Console.WriteLine("Q Sök titel\t\tF Sök författare\t\tA Lista alla böcker");
            Console.WriteLine("B Låna bok\t\tÅ Återlämna bok");
            Console.WriteLine("Esc för att avsluta\tS för adminåtkomst");


            Admin:
            var MenyVal = Console.ReadKey();
            switch (MenyVal.Key)
            {
                case ConsoleKey.Q:
                    SökBok();
                    goto Meny;
                case ConsoleKey.T:
                    böcker = UppdateraBibliotek();
                    TaBortBok(böcker);
                    goto Meny;
                case ConsoleKey.R:
                    böcker = UppdateraBibliotek();
                    RaderaFörfattare(böcker);
                    goto Meny;
                case ConsoleKey.L:
                    LäggTillBok();
                    böcker = UppdateraBibliotek();
                    goto Meny;
                case ConsoleKey.F:
                    SökFörfattare();
                    goto Meny;
                case ConsoleKey.Oem6: //Å
                    lämnaBok();
                    goto Meny;
                case ConsoleKey.A:
                    böcker = UppdateraBibliotek();
                    SkrivLista(böcker);
                    goto Meny;
                case ConsoleKey.Escape:
                    Console.WriteLine("\nDu går nu vidare");
                    break;
                case ConsoleKey.S:
                    Console.WriteLine("\nT för att ta bort bok\tR radera en författare\tL för att lägga till bok");
                    goto Admin;
                case ConsoleKey.B:
                    lånaBok();
                    böcker = UppdateraBibliotek();
                    goto Meny;
                default:
                    Console.WriteLine("\nVar vänlig och mata in ett av de angivna alternativen");
                    goto Meny;
            }
            //  }
            //  catch
            //{
            //     Console.WriteLine("Debug 3 Sec");  Thread.Sleep(3000);
            //   Console.WriteLine("\nVar vänlig och mata in ett av de angivna alternativen");
            // goto Meny;
            //  }
        }


    }
}
