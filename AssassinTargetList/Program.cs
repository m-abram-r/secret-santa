using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace AssassinTargetList
{
    public class Program
    {
        public class Participant : IEquatable<Participant>
        {
            public string MyName { get; set; }

            public string MyFavoriteCandy { get; set; }

            public Participant Target { get; set; }

            public bool Equals(Participant otherPerson)
            {
                return (MyName, Target, MyFavoriteCandy) == (otherPerson.MyName, otherPerson.Target, otherPerson.MyFavoriteCandy);
                //return (MyName, Target) == (otherPerson.MyName, otherPerson.Target);
            }
        }

        static Random rnd = new Random();


        static void Main()
        {
            List<Participant> participants = new List<Participant>();

            var listFull = false;

            var gifts = false;

            Console.WriteLine(@"
SECRET SANTA GENERATOR
ALSO WORKS FOR GAMES OF ASSASSIN
HO HO HO

NOTHING IS TRUE, EVERYTHING IS PERMITTED."
                );

            Console.WriteLine("Do you want each participant to receive candy? (Enter Y or N)\n\t");

            var yeaornah = Console.ReadLine();

            if (yeaornah.Contains("Y") || yeaornah.Contains("y"))
            {
                gifts = true;
                Console.WriteLine("\nOkay, each person will get candy!\n");
            }                

            else if (yeaornah.Contains("N") || yeaornah.Contains("n"))

                Console.WriteLine("\nOkay, no candy this time. \n");

            while (!listFull)
            {
                var tPerson = new Participant();

                while (tPerson.MyName == null)
                {
                    Console.WriteLine("\nPlease enter a participant's name to be included.\n\t");

                    var tName = Console.ReadLine();

                    Console.WriteLine($"\nYou entered: {tName}. Is this correct? (Enter Y or N)\n\t");

                    var yesorno = Console.ReadLine();

                    if (yesorno.Contains("Y") || yesorno.Contains("y"))

                        tPerson.MyName = tName;

                    else if (yesorno.Contains("N") || yesorno.Contains("n"))

                        Console.WriteLine("\nOkay, we'll try again. \n");
                }

                if (gifts)
                {
                    Console.WriteLine("\nGreat! Now for candy.\n");

                    while (tPerson.MyFavoriteCandy == null)
                    {
                        Console.WriteLine($"\nPlease enter {tPerson.MyName}'s favorite candy.\n\t");

                        var tCandy = Console.ReadLine();

                        Console.WriteLine($"\nYou entered: {tCandy}. Is this correct? (Enter Y or N)\n\t");

                        var yn = Console.ReadLine();

                        if (yn.Contains("Y") || yn.Contains("y"))

                            tPerson.MyFavoriteCandy = tCandy;

                        else if (yn.Contains("N") || yn.Contains("n"))

                            Console.WriteLine("\nOkay, we'll try again. ");
                    }

                    Console.WriteLine($"\nGreat! so we have {tPerson.MyName}, whose favorite candy is {tPerson.MyFavoriteCandy}.\n\nIs this right?(Enter Y or N)\n\t");
                }
                else
                {
                    Console.WriteLine($"\nGreat! so we have {tPerson.MyName}.\n\nIs this right?(Enter Y or N)\n\t");
                }


                var yayornay = Console.ReadLine();

                if (yayornay.Contains("Y") || yayornay.Contains("y"))
                {
                    participants.Add(tPerson);

                    Console.WriteLine("\nDo you want to add more participants? (Enter Y or N)\n\t");

                    var yn = Console.ReadLine();

                    if (yn.Contains("Y") || yn.Contains("y"))

                        Console.WriteLine("\nOkay, on to the next!\n");

                    else if (yn.Contains("N") || yn.Contains("n"))

                        listFull = true;
                }
                else if (yayornay.Contains("N") || yayornay.Contains("n"))

                    Console.WriteLine("\nOkay, we'll try again. \n");

                if (participants.Count < 4)

                    continue;
            }

            var fullList = participants.ToArray();

            var reshuffle = true;

            Console.WriteLine("\nOkay great!");

            while (reshuffle)
            {
                Console.WriteLine("Assigning targets to everyone...\n");

                var remainingTargets = fullList;

                for (int i = 0; i < fullList.Length; i++)
                {
                    var tSubject = fullList[i];

                    var tRemainingTargets = remainingTargets.Where(val => val != tSubject).ToArray();

                    if (tRemainingTargets.Contains(tSubject))
                    {
                        Console.WriteLine("whoops! couldnt remove element (you cant be assigned to yourself silly)");

                        break;
                    }

                    int r = rnd.Next(tRemainingTargets.Length);

                    var cTarget = tRemainingTargets[r];

                    fullList[i].Target = cTarget;

                    remainingTargets = remainingTargets.Where(val => val != cTarget).ToArray();

                    if (remainingTargets.Contains(cTarget))
                    {
                        Console.WriteLine("whoops! couldnt remove element (no getting two gifts!)");

                        break;
                    }
                }

                Console.WriteLine("Done! Here are the participants, their targets, and their targets' favorite candies:\n");

                foreach (Participant person in fullList)
                {
                    if (gifts)
                        Console.WriteLine($"{person.MyName} is getting something for {person.Target.MyName}, whose favorite candy is {person.Target.MyFavoriteCandy}.\n");
                    else
                        Console.WriteLine($"{person.MyName} is assigned to {person.Target.MyName}\n");
                }

                Console.WriteLine("Are you happy with these results? (Enter Y or N)\n");

                var yesorno = Console.ReadLine();

                if (yesorno.Contains("Y") || yesorno.Contains("y"))

                    reshuffle = false;

                else if (yesorno.Contains("N") || yesorno.Contains("n"))

                    Console.WriteLine("\nOkay, we'll re-shuffle. \n");

            }
        }
    }
}
