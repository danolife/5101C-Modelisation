﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    static class ProjetEleve
    {
        public static void Go()
        {
            /*
            Texture T1 = new Texture("brick01.jpg");
           
            int larg = 600;
            int haut = 300;
            float r_x = 1.5f;   // repetition de la texture en x
            float r_y = 1.0f;   // repetition de la texture en y
            float pas = 0.001f;

            for (float u = 0 ; u < 1 ; u+=pas)  // echantillonage fnt paramétrique
            for (float v = 0 ; v < 1 ; v+=pas)
                {
                    int x = (int) (u * larg + 10); // calcul des coordonnées planes
                    int y = (int) (v * haut + 15);

                    Couleur c = T1.LireCouleur(u * r_x, v * r_y);
                    
                    BitmapEcran.DrawPixel(x,y,c );
                   
                }

            // dessin sur l'image pour comprendre l'orientation axe et origine du Bitmap
            
            Couleur Red = new Couleur(1.0f, 0.0f, 0.0f);
            for (int i = 0; i < 1000; i++)
                BitmapEcran.DrawPixel(i, i, Red);

            Couleur Green = new Couleur(0.0f, 1.0f, 0.0f);
            for (int i = 0; i < 1000; i++)
                BitmapEcran.DrawPixel(i, 1000-i, Green);

            // test des opérations sur les vecteurs

            V3 t = new V3(1, 0, 0);
            V3 r = new V3(0, 1, 0);
            V3 k = t + r;
            float p = k * t * 2;
            V3 n = t ^ r;
            V3 m = -t;

            */

            Couleur Green = new Couleur(0.0f, 1.0f, 0.0f);
            Couleur Red = new Couleur(1.0f, 0.0f, 0.0f);

            V3 L = new V3(1.0f, -1.0f, 1.0f);
            Couleur C_ambiant = new Couleur(0.2f, 0.2f, 0.2f);
            Couleur C_lampe = new Couleur(0.8f, 0.8f, 0.8f);

            // zbuffer initialization
            double[,] zbuffer = new double[957,569];
            for (int i = 0; i < zbuffer.GetLength(0); i++)
            {
                for (int j = 0; j < zbuffer.GetLength(1); j++)
                {
                    zbuffer[i, j] = double.MaxValue;
                }
            }

            Draw.DrawSphere(200, 0, 200, 100, Red, C_ambiant, C_lampe, ref zbuffer, L);
            Draw.DrawSphere(350, 0, 200, 100, Green, C_ambiant, C_lampe, ref zbuffer, L);
            
        }
    }
}