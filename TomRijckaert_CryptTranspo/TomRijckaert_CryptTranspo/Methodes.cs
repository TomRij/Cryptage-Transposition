using System;
using System.Collections.Generic;
using System.Text;

namespace TomRijckaert_CryptTranspo
{
    struct Methodes
    {
        public void RetireEspaces(string chaine, out string chaineSpace)
        {
            chaineSpace = "";
            int lg = chaine.Length;
            for (int i = 0; i < lg; i++)
            {
                if (chaine[i]!=' ')
                {
                    chaineSpace = chaineSpace + chaine[i];
                }
            }
        }
        public void DimensionMat(string cle, string texte, out char[,] matrice)
        {
            int d1 = (texte.Length / cle.Length) + 2;
            int d2 = cle.Length;
            if (texte.Length % cle.Length != 0)
            {
                d1 = d1 + 1;
            }
            matrice = new char[d1, d2];
        }
        public void RemplirMatrice(string cle, string texte, ref char[,] mat)
        {
            for (int j = 0; j < mat.GetLength(1); j++)
            {
                mat[0, j] = cle[j];
            }
            int k = 0;
            for (int i = 2; i < mat.GetLength(0); i++)
            {
                int j = 0;
                while (j < mat.GetLength(1) && k < texte.Length)
                {
                    mat[i, j] = texte[k];
                    k = k + 1;
                    j = j + 1;
                }
            }
        }
        public void TriLigne(ref char[,] mat)
        {
            bool permut = false;
            for (int i = 0; i < mat.GetLength(1); i++)
            {
                if (mat[0,i]>mat[0, i+1])
                {
                    (mat[0, i], mat[0, i + 1]) = (mat[0, i+1], mat[0, i]);
                    permut = true;
                }
            }
        }
        public void ClasseCle(string cle, out char[,] mattri)
        {
            mattri = new char[3, cle.Length];
            for (int j = 0; j < mattri.GetLength(1); j++)
            {
                mattri[0, j] = cle[j];
                mattri[2, j] = '0';
                TriLigne(ref mattri);
            }
            for (int j = 1; j < mattri.GetLength(1); j++)
            {
                mattri[1, j - 1] = Char.Parse(j.ToString());
            }
        }
        public void AttribueRang(ref char[,] mat, ref char[,] mattri)
        {
            for (int i = 0; i < mat.GetLength(1); i++)
            {
                bool trouve = false;
                int j = 0;
                while (trouve == false && j < mattri.GetLength(1))
                {
                    if (mat[0, i] == mattri[0, j] && mattri[2, j] != '1')
                    {
                        mat[1, j] = mattri[1, j];
                        mattri[2, j] = '1';
                        trouve = true;
                        j = j + 1;
                    }
                }
            }
        }
        public void RealisCrypt(char[,] mat, out string chaineCrypt)
        {
            chaineCrypt = "";
            int i = 1;
            while (i <= mat.GetLength(1))
            {
                bool trouve = false;
                int j = 0;
                while (!trouve && j < mat.GetLength(1))
                {
                    if (i == mat[1,j])
                    {
                        for (int k = 2; k < mat.GetLength(0); k++)
                        {
                            chaineCrypt += mat[k, j];
                        }
                        chaineCrypt += " ";
                        trouve = true;
                        i = i + 1;
                        j = j + 1;
                    }
                }
            }
        }
    }
}
