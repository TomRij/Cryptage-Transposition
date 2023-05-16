using System;
using System.Collections.Generic;
using System.Text;

namespace TomRijckaert_CryptTranspo
{
    struct Methodes
    {
        // Retire les espaces d'une chaîne de caractères
        public void RetireEspaces(string chaine, out string chaineSpace)
        {
            chaineSpace = "";
            int lg = chaine.Length; // Récupère la longueur de la chaîne de caractères
            for (int i = 0; i < lg; i++) // Parcourt chaque caractère de la chaîne
            {
                if (chaine[i] != ' ') // Vérifie si le caractère n'est pas un espace
                {
                    chaineSpace = chaineSpace + chaine[i]; // Ajoute le caractère à la chaîne sans espaces
                }
            }
        }

        // Détermine les dimensions de la matrice en fonction de la clé et du texte
        public void DimensionMat(string cle, string texte, out char[,] matrice)
        {
            int d1 = (texte.Length / cle.Length) + 2; // Calcule la dimension 1 de la matrice en fonction de la longueur du texte et de la clé
            int d2 = cle.Length; // Récupère la longueur de la clé
            if (texte.Length % cle.Length != 0) // Vérifie s'il reste des caractères du texte non répartis dans la matrice
            {
                d1 = d1 + 1; // Ajoute une ligne supplémentaire à la matrice
            }
            matrice = new char[d1, d2]; // Crée une nouvelle matrice avec les dimensions calculées
        }

        // Remplit la matrice avec la clé et le texte
        public void RemplirMatrice(string cle, string texte, ref char[,] mat)
        {
            for (int j = 0; j < mat.GetLength(1); j++) // Parcourt chaque colonne de la matrice
            {
                mat[0, j] = cle[j]; // Ajoute les caractères de la clé à la première ligne de la matrice
            }
            int k = 0; // Indice pour parcourir le texte
            for (int i = 2; i < mat.GetLength(0); i++) // Parcourt chaque ligne de la matrice à partir de la troisième ligne
            {
                int j = 0;
                while (j < mat.GetLength(1) && k < texte.Length) // Parcourt chaque cellule de la ligne tant qu'il y a des caractères dans le texte
                {
                    mat[i, j] = texte[k]; // Ajoute un caractère du texte dans la cellule correspondante de la matrice
                    k = k + 1; // Passe au caractère suivant du texte
                    j = j + 1; // Passe à la cellule suivante de la ligne
                }
            }
        }

        // Trie les caractères de la première ligne de la matrice
        public void TriLigne(ref char[,] mat)
        {
            bool permut = true; // Variable pour indiquer si une permutation a eu lieu
            while (permut)
            {
                permut = false;
                for (int i = 0; i < mat.GetLength(1) - 1; i++)
                {
                    if (mat[0, i] > mat[0, i + 1])
                    {
                        (mat[0, i], mat[0, i + 1]) = (mat[0, i + 1], mat[0, i]); // Permute les caractères dans la première ligne de la matrice
                        permut = true; // Indique qu'une permutation a eu lieu
                    }
                }
            }
        }

        // Classe la clé et crée une matrice triée correspondante
        public void ClasseCle(string cle, out char[,] mattri)
        {
            mattri = new char[3, cle.Length]; // Crée une matrice de 3 lignes et la longueur de la clé colonnes
            for (int j = 0; j < mattri.GetLength(1); j++)
            {
                mattri[0, j] = cle[j]; // Ajoute les caractères de la clé à la première ligne de la matrice triée
                mattri[2, j] = '0'; // Initialise la troisième ligne de la matrice triée avec des zéros
                TriLigne(ref mattri); // Trie les caractères de la première ligne de la matrice triée
            }
            for (int j = 1; j < mattri.GetLength(1); j++)
            {
                mattri[1, j - 1] = Char.Parse(j.ToString()); // Remplit la deuxième ligne de la matrice triée avec des nombres séquentiels à partir de 1
            }
        }

        // Attribue le rang à chaque colonne de la matrice en utilisant la matrice triée
        public void AttribueRang(ref char[,] mat, ref char[,] mattri)
        {
            for (int i = 0; i < mat.GetLength(1); i++)
            {
                bool trouve = false; // Variable pour indiquer si une correspondance a été trouvée dans la matrice triée
                int j = 0;
                while (trouve == false && j < mattri.GetLength(1))
                {
                    if (mat[0, i] == mattri[0, j] && mattri[2, j] != '1')
                    {
                        mat[1, j] = mattri[1, j]; // Attribue le rang à la colonne correspondante dans la deuxième ligne de la matrice non triée
                        mattri[2, j] = '1'; // Marque la colonne comme utilisée dans la matrice triée
                        trouve = true; // Indique qu'une correspondance a été trouvée
                        j = j + 1;
                    }
                }
            }
        }

        // Réalise le cryptage en utilisant la matrice attribuée
        public void RealisCrypt(char[,] mat, out string chaineCrypt)
        {
            chaineCrypt = "";
            int i = 1;
            while (i <= mat.GetLength(1))
            {
                bool trouve = false; // Variable pour indiquer si une correspondance a été trouvée dans la matrice attribuée
                int j = 0;
                while (!trouve && j < mat.GetLength(1))
                {
                    if (i == mat[1,j])
                    {
                        for (int k = 2; k < mat.GetLength(0); k++)
                        {
                            chaineCrypt += mat[k, j]; // Ajoute les caractères de la colonne correspondante dans la chaîne de cryptage
                        }
                        chaineCrypt += " "; // Ajoute un espace entre les colonnes cryptées
                        trouve = true; // Indique qu'une correspondance a été trouvée
                        i = i + 1;
                        j = j + 1;
                    }
                }
            }
        }
    }
}
