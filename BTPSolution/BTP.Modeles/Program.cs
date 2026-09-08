using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTP.Modeles
{
    public class Program
    {
        static void Main(string[] args)
        {
            Test();
        }


        static void Test()
        {
            using (var context = new Db.BTPSolutionDbContext())
            {
                var projets = context.Projets.ToList();
                foreach (var projet in projets)
                {
                    Console.WriteLine($"Projet: {projet.NomProjet}, Description: {projet.Description}");
                }
            }
        }



    }
}
