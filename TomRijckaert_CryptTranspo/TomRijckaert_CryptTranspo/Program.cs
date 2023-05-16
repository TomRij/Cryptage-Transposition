using System;

namespace TomRijckaert_CryptTranspo
{
    class Program
    {
        static void Main(string[] args)
        {
            bool recommencer = true;

            while (recommencer)
            {
                Console.WriteLine("Cryptage par Transposition");
                Console.WriteLine("--------------------------");

                // Demande de la clé
                Console.Write("Entrez la clé : ");
                string cle = Console.ReadLine();

                // Demande du texte
                Console.Write("Entrez le texte : ");
                string texte = Console.ReadLine();

                Methodes methodes = new Methodes();

                // Retire les espaces du texte
                methodes.RetireEspaces(texte, out string texteSansEspaces);

                // Crée la matrice
                methodes.DimensionMat(cle, texteSansEspaces, out char[,] matrice);

                // Remplit la matrice
                methodes.RemplirMatrice(cle, texteSansEspaces, ref matrice);

                // Classe la clé
                methodes.ClasseCle(cle, out char[,] matriceTriee);

                // Attribue le rang à chaque colonne de la matrice
                methodes.AttribueRang(ref matrice, ref matriceTriee);

                // Réalise le cryptage
                methodes.RealisCrypt(matrice, out string chaineCryptee);

                Console.WriteLine("Le texte crypté est : " + chaineCryptee);

                // Demande si l'utilisateur veut recommencer
                Console.Write("Voulez-vous recommencer ? (O/N) : ");
                string reponse = Console.ReadLine();

                recommencer = (reponse.ToLower() == "o");
            }
        }
    }
}
