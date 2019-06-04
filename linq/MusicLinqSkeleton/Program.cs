using System;
using System.Collections.Generic;
using System.Linq;
using JsonData;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Collections to work with
            List<Artist> Artists = MusicStore.GetData().AllArtists;
            List<Group> Groups = MusicStore.GetData().AllGroups;

            //========================================================
            //Solve all of the prompts below using various LINQ queries
            //========================================================

            //There is only one artist in this collection from Mount Vernon, what is their name and age?
						IEnumerable<Artist> vernon = Artists.Where(a => a.Hometown == "Mount Vernon");
						foreach (Artist a in vernon)
						{
							System.Console.WriteLine(a.ArtistName);
						}


            //Who is the youngest artist in our collection of artists?
						Artist youngest = Artists
							.OrderBy(a => a.Age)
							.FirstOrDefault();
						System.Console.WriteLine(youngest.ArtistName);

            //Display all artists with 'William' somewhere in their real name
						IEnumerable<Artist> billy = Artists.Where(a => a.RealName.Contains("William"));
						foreach (Artist a in billy)
							System.Console.WriteLine(a.RealName);

						// Display all groups with names less than 8 characters in length
						IEnumerable<Group> shorties = Groups.Where(g => g.GroupName.Length < 8);
						foreach (Group g in shorties)
							System.Console.WriteLine(g.GroupName);

            //Display the 3 oldest artist from Atlanta
						IEnumerable<Artist> atl = Artists
							.Where(a => a.Hometown == "Atlanta")
							.OrderByDescending(a => a.Age)
							.Take(3);
						foreach (Artist a in atl)
							System.Console.WriteLine(a.ArtistName);

            //(Optional) Display the Group Name of all groups that have members that are not from New York City
						IEnumerable<Group> notNYC = Groups
							.Where(group => group.Members
								.Any(member => member.Hometown != "New York City")
								);
						foreach (Group g in notNYC)
							System.Console.WriteLine(g.GroupName);

            //(Optional) Display the artist names of all members of the group 'Wu-Tang Clan'
						IEnumerable<string> wutang = Artists
							.Join(Groups,
								artist => artist.GroupId,
								group => group.Id,
								(artist, group) => 
									new {ArtistInfo = artist.ArtistName, GroupInfo = group.GroupName} )
							.Where(group => group.GroupInfo == "Wu-Tang Clan")
							.Select(info => info.ArtistInfo);

						foreach (string artist in wutang)
							System.Console.WriteLine(artist);

					Console.WriteLine(Groups.Count);
        }
    }
}
