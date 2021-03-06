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
            int width = BitmapEcran.GetWidth();
            int height = BitmapEcran.GetHeight()+1;

            Couleur C_ambiant = new Couleur(0.2f, 0.2f, 0.2f);
            V3 camera = new V3(width / 2, -1000, height / 2);

            // LAMPES
            List<Lampe> lampList = new List<Lampe>();
            lampList.Add(new Lumiere(0, new V3(1f, -1f, 1f), new V3(0f, 0f, 0f), new Couleur(0.4f, 0.4f, 0.4f)));
            lampList.Add(new Lumiere(0, new V3(-1f, -1f, 1f), new V3(0f, 0f, 0f), new Couleur(0.4f, 0.4f, 0.4f)));

            // TEXTURES
            Texture T_carreau = new Texture("carreau.jpg");
            Texture T_lead = new Texture("lead.jpg");
            Texture T_Aymeric = new Texture("aymeric.jpg");
            Texture T_stone = new Texture("stone2.jpg");
            Texture T_fibre = new Texture("fibre.jpg");
            Texture T_brick = new Texture("brick01.jpg");
            Texture T_gold = new Texture("gold.jpg");
            // BUMP TEXTURES
            Texture T_bump = new Texture("bump38.jpg");
            Texture T_leadbump = new Texture("lead_bump.jpg");


            // OBJETS
            List<Objet3D> objList = new List<Objet3D>();

            //SPHERES
            objList.Add(new Sphere(new V3(width / 2, 500, height - 50), 150, T_carreau, T_bump, 0.008f));
            objList.Add(new Sphere(new V3(50, 500, height / 2), 150, T_lead, T_leadbump, 0.01f));
            objList.Add(new Sphere(new V3(175, 300, height / 2 + 75), 50, T_gold, T_bump, 0.1f));
            //RECTS
            //fond
            objList.Add(new Rect(new V3(50, 1000, 50), new V3(width - 100, 0, 0), new V3(0, 0, height - 100), T_brick, null));
            //bas
            objList.Add(new Rect(new V3(50, 0, 50), new V3(width - 100, 0, 0), new V3(0, 1000, 0), T_stone, null));
            //haut
            //objList.Add(new Rect(new V3(width-50, 0, height-50), new V3(-(width-100), 0, 0), new V3(0, 1000, 0), T_fibre, null));
            //gauche
            objList.Add(new Rect(new V3(50, 0, 50), new V3(0, 1000, 0), new V3(0, 0, height - 100), T_brick, null));
            //droite
            objList.Add(new Rect(new V3(width - 50, 1000, 50), new V3(0, -1000, 0), new V3(0, 0, height - 100), T_brick, null));
            //milieu
            objList.Add(new Rect(new V3(width / 2 + 100, 750, 50), new V3(0, -500, 0), new V3(0, 0, height / 3), T_brick, null));
            

            // RAY CASTING

            V3 Rd, R;
            float t, tnew;
            bool[] occs = new bool[lampList.Count];

            //parcourir les pixels en x
            for (int pxx = 0; pxx < width; pxx++) {
                //parcourir les pixels en z
                for (int pxz = 0; pxz < height; pxz++) {
                    t = float.MaxValue;
                    // pour chaque pixel générer un rayon Rd
                    Rd = new V3(pxx - camera.x, -camera.y, pxz - camera.z);
                    Rd.Normalize();
                    // parcourir la liste des objets
                    foreach (Objet3D obj in objList) {
                        // appeler l'intersection et récupérer le point
                        tnew = obj.getIntersect(Rd, camera);
                        if (tnew != -1)
                        {
                            if (tnew <= t)
                            {
                                t = tnew;
                                // R est le point d'intersection de Rd et de l'objet
                                R = camera + t * Rd;
                                occs = obj.occultation(R, objList, lampList);
                                //Console.WriteLine(occs[0]+" "+occs[1]);
                                Couleur finalColor = obj.DrawPoint(C_ambiant, lampList, camera, R, occs);
                                BitmapEcran.DrawPixel(pxx, pxz, finalColor);
                            }
                        }                          

                    }
                }
            }




            //s1.Draw(C_ambiant, L, camera);
            //s2.Draw(C_ambiant, L, camera);

            
            //r1.Draw(C_ambiant, L, camera);
            //r2.Draw(C_ambiant, L, camera);

        }
    }
}
