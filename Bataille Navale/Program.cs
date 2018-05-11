using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;  //Pour pouvoir utiliser le "StreamWriter" lors de la sauvegarde, et "StreamReader" lors du chargement

//Dernière version

namespace Bataille_Navale
{
    class Program
    {
        //Pour tout le programme, i est le nombre de ligne et j est le nombre de colonne

        // ----------------- FONCTIONS PERMETTANT D'AFFICHER LES GRILLES --------------------//
        
            //Affiche la grille du joueur avec la position des bateaux
        public static void AfficherGrilleJoueur(string[,] tab)
        {
            string[] lettre = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" }; // On fait un tableau avec les lettres qui seront écrites sur le côté pour facilité l'affichage

            Console.WriteLine("\n \n --------------- GRILLE JOUEUR--------------- \n \n");

            Console.WriteLine("     1   2   3   4   5   6   7   8   9   10");


            for (int i = 0; i < tab.GetLength(0); i++) 
            {
                Console.WriteLine("   +---+---+---+---+---+---+---+---+---+---+");
                Console.Write(" {0} |", lettre[i]); // Pour écrire les lettres sur le coté de la grille

                for (int j = 0; j < tab.GetLength(0); j++)
                {
                    if (j == (tab.GetLength(0)- 1)) // Permet de sauter une ligne quand on arrive à la dernière colonne
                    {
                        switch (tab[i, j]) 
                        {

                            case ("2"): //Pour le  bateau de 2
                                Console.WriteLine(" 2 |");
                                break;

                            case ("3s"): //Pour le sous-marin
                                Console.WriteLine(" 3s|");
                                break;

                            case ("3c"): //Pour le croiseur
                                Console.WriteLine(" 3c|");
                                break;

                            case ("4"): //Pour le bateau de 4
                                Console.WriteLine(" 4 |");
                                break;

                            case ("5"): //Pour le bateau de 5
                                Console.WriteLine(" 5 |");
                                break;

                            case ("t"): //Cas d'un bateau touché
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write(" *");
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Console.WriteLine(" |");
                                
                                break;

                            case ("plouf"): //Cas d'un bateau touché
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.Write(" X ");
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Console.WriteLine("|");
                                break;

                            default:
                                Console.WriteLine("   |");
                                break;
                                                             
                        }

                    }

                    else
                    {
                        switch (tab[i, j]) 
                        {

                            case ("2"): //Pour le bateau de 2
                                Console.Write(" 2 |");
                                break;

                            case ("3s"): //Pour le sous-marin
                                Console.Write(" 3s|");
                                break;

                            case ("3c"): //Pour le croiseur
                                Console.Write(" 3c|");
                                break;

                            case ("4"): //Pour le bateau de 4
                                Console.Write(" 4 |");
                                break;

                            case ("5"): //Pour le bateau de 5
                                Console.Write(" 5 |");
                                break;

                            case ("t"): //Cas d'un bateau touché
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write(" * ");
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Console.Write("|");
                                break;

                            case ("plouf"): //Cas d'un bateau touché
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.Write(" X ");
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Console.Write("|");
                                break;

                            default: 
                                Console.Write("   |");
                                break;

                        }

                    }
                }

            }
            Console.WriteLine("   +---+---+---+---+---+---+---+---+---+---+");

        }

            //Affiche la grille de l'adversaire, avec les coups du joueur, mais ne montre pas l'emplacement des bateaux.
        
        public static void AfficherGrilleAdversaire(string[,] tab)
        {
            string[] lettre = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" }; // On fait un tableau avec les lettres qui seront écrites sur le côté pour facilité l'affichage

            Console.WriteLine("\n \n ------------ GRILLE ADVERSAIRE ------------- \n \n");

            Console.WriteLine("     1   2   3   4   5   6   7   8   9   10");

            for (int i = 0; i < tab.GetLength(0); i++) 
            {
                Console.WriteLine("   +---+---+---+---+---+---+---+---+---+---+");
                Console.Write(" {0} |", lettre[i]); //Permet d'écrire les lettres à gauche de la grille

                for (int j = 0; j < tab.GetLength(0); j++) 
                {
                    if (j == (tab.GetLength(0)-1)) //On spécifie ce cas car il est permet de revenir à la ligne à la fin d'une ligne.
                    {
                        switch (tab[i, j])
                        {
                            case ("t"):
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write(" O "); //Quand un bateau est touché
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Console.WriteLine("|");
                                break;

                            case ("plouf"):
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.Write(" X "); //Quand on tire dans l'eau
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Console.WriteLine("|");
                                break;

                            default:
                                Console.WriteLine("   |");
                                break;

                        }

                    }

                    else 
                    {
                        switch (tab[i, j])
                        {
                            case ("t"):
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write(" O "); //Quand un bateau est touché
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Console.Write("|");
                                break;

                            case ("plouf"):
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.Write(" X "); //Quand on tire dans l'eau
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Console.Write("|");
                                break;

                            default:
                                Console.Write("   |"); //case vide
                                break;

                        }
                    }
                }

            }
            Console.WriteLine("   +---+---+---+---+---+---+---+---+---+---+");

        }

        //------------------ FONCTIONS PERMETTANT DE PLACER LES BATEAUX ---------------//

            //Placement des bateaux en général, cette fonction est appelée dans les deux suivantes
        public static bool PlacerBateau(Random random, string[,] tab, string[] bateau)
        {
            
            int choixLigne;
            int choixColonne;

            
            /*La boucle do-while va permettre de réaliser une première fois le tirage au hasard d'une ligne et d'une colonne
              Ce tirage est réalisé tant que la case correspondante n'est pas vide (dans le cas échéant cela voudrait dire qu'il y a déjà un  bateau sur cette case)*/
            do
            {
                //On choisit au hasard une case de départ, de la ligne puis, de la colonne.
                choixLigne = random.Next(0, tab.GetLength(0)); 
                choixColonne = random.Next(0, tab.GetLength(0)); 
            }

            while (tab[choixLigne, choixColonne] != " ");//On vérifie s'il y a deja un bateau sur cette case.
            //S'il y a déjà un bateau sur la case, alors ça ne sera plus un espace, mais un chiffre, donc on boucle tant que c'est différent de " ".

            //On met un caractère dans la case initiale. Ce caractère correspond à la première case du tableau "bateau".
            tab[choixLigne, choixColonne] = bateau[0];


            //On définit la direction du bateau au hasard

            int direction = random.Next(0, 4); //On a 4 directions possibles. Choix aléatoire d'un chiffre entre 0 et 3 inclu.


            //Bateau dirigé vers la gauche. Pour cela on soustrait "k" à la coolonne correspondante à la celle de la case initiale
            if (direction == 0)
            {
                try //On éxecute le code, et si on repère une exception (ici de type IndexOutOfRange) on retourne vrai.
                {
                    for (int k = 1; k < bateau.Length; k++) //On boucle autant de fois qu'il y a de cases dans un bateau
                    {
                        if (tab[choixLigne, (choixColonne - k)] == " ") //S'il n'y a rien dans la case, alors on execute ce code, et on place une partie du bateau
                        {
                            tab[choixLigne, (choixColonne - k)] = bateau[k]; //On écrit ce qu'il y a dans la case d'indice k du tableau "bateau", sachant que celle d'indice 0 correspond à la case initiale (k commençant à 1).

                            //Si la totalité du bateau a été placé, on retourne "true". Si tous les bateaux sont placés correctement, on ne rééxcute pas le "try" (cf "PlacerBateauJoueur" et " PlacerBateauAdversaire").
                            if (k == (bateau.Length - 1))
                            {
                                return true; 
                            }

                        }
                        else
                        {
                            return false; //On retourne false, car ça veut dire qu'on est tombé sur une case où il y a déjà un bateau
                        }
                    }
                    //A la fin du "for", soit on a parcouru la totalité de la boucle, et le bateau a été placé entièrement; soit il y a eu une erreur, et le bateau n'a pas été placé dans sa totalité.
                }
                catch (IndexOutOfRangeException) //Si on a une exceptionde type IndexOutofRange, on recommence.--> Boucle while dans le Main
                {
                    return false;
                }
            }

            //Les directions suivantes fonctionnent de manière similaire à celle ci-dessus

            //Bateau dirigé vers le bas. Pour cela on ajoute "k" à la ligne correspondante à la celle de la case initiale
            else if (direction == 1)
            {
                try
                {
                    for (int k = 1; k < bateau.Length; k++)
                    {
                        if (tab[(choixLigne + k), choixColonne] == " ")
                        {
                            tab[(choixLigne + k), choixColonne] = bateau[k];

                            //Si la totalité du bateau a été placé, on retourne "true". Si tous les bateaux sont placés correctement, on ne rééxcute pas le "try" (cf "PlacerBateauJoueur" et " PlacerBateauAdversaire").
                            if (k == (bateau.Length - 1))
                            {
                                return true;
                            }
                        }

                        else
                        {
                            return false; //On retourne false, car ça veut dire qu'on est tombé sur une case où il y a déjà un bateau
                        }
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    return false;
                }
            }


            //Bateau dirigé vers la droite. Pour cela on ajoute "k" à la colonne correspondante à la celle de la case initiale
            else if (direction == 2)
            {
                try
                {
                    for (int k = 1; k < bateau.Length; k++)
                    {
                        if (tab[choixLigne, (choixColonne + k)] == " ")
                        {
                            tab[choixLigne, (choixColonne + k)] = bateau[k];

                            //Si la totalité du bateau a été placé, on retourne "true". Si tous les bateaux sont placés correctement, on ne rééxcute pas le "try" (cf "PlacerBateauJoueur" et " PlacerBateauAdversaire").
                            if (k == (bateau.Length - 1))
                            {
                                return true;
                            }
                        }

                        else
                        {
                            return false; //On retourne false, car ça veut dire qu'on est tombé sur une case où il y a déjà un bateau
                        }
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    return false;
                }
            }

            //Bateau dirigé vers le haut. Pour cela on ajoute "k" à la ligne correspondante à la celle de la case initiale
            else if (direction == 3)
            {
                try
                {
                    for (int k = 1; k < bateau.Length; k++)
                    {
                        if (tab[(choixLigne - k), choixColonne] == " ")
                        {
                            tab[(choixLigne - k), choixColonne] = bateau[k];

                            //Si la totalité du bateau a été placé, on retourne "true". Si tous les bateaux sont placés correctement, on ne rééxcute pas le "try" (cf "PlacerBateauJoueur" et " PlacerBateauAdversaire").
                            if (k == (bateau.Length - 1))
                            {
                                return true;
                            }
                        }

                        else
                        {
                            return false; //On retourne false, car ça veut dire qu'on est tombé sur une case où il y a déjà un bateau
                        }
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    return false;
                }
            }

            return false;


        }

           //Permet de placer les bateaux du joueur (sur la grille joueur)
        public static void PlacerBateauJoueur(Random random, string[,] grilleJoueur, string[] bateau5, string[] bateau4, string[] bateau3Croiseur, string[] bateau3Sousmarin, string[] bateau2)
        {

            /*On initialise des booléens. Ils vont permettre de faire tourner la boucle while. Boucle qui va permettre de répèter l'opération du placement
              tant que tous les bateaux ne sont pas placés sur la grille.*/

            bool bat5 = false;
            bool bat4 = false;
            bool bat3Croiseur = false;
            bool bat3Sousmarin = false;
            bool bat2 = false;
                        
           
            //Tant qu'une des variables booléennes est fausse, on rentre dans la boucle.
            //Autrement dit, dès que l'on détecte une erreur du type IndexOutOfRange, puisque la fonction "PlacerBateau" renvoit "false" lorsqu'une exception de type IndexOutOfRange est captée.
            while ((bat5 == false) || (bat4 == false) || (bat3Croiseur == false) || (bat3Sousmarin == false) || (bat2 == false))
            {
                /*On réinitialise la grille du joueur en remettant toutes les valeurs à 0, c'est à dire avec des espaces.
                  En effet, si tous les bateaux n'ont pas pu être placés, la boucle est exécutée de nouveau. 
                  Cependant, ce n'est pas parce qu'on reboucle qu'aucun bateau n'a été placé correctement précèdemment, c'est au moins l'un d'eux qui a généré l'erreur.
                  Ainsi il est nécessaire de réinitialiser la grille pour ne pas avoir trop de bateau.*/


                for (int i = 0; i < grilleJoueur.GetLength(0) ; i++)
                {
                    for (int j = 0; j < grilleJoueur.GetLength(0); j++)
                    {
                        grilleJoueur[i, j] = " ";
                        
                    }
                }

                //Placement du porte-Avion (bateau de 5) sur la grille du joueur
                bat5 = PlacerBateau(random, grilleJoueur, bateau5);

                //Placement du cuirassé (bateau de 4) sur la grille du joueur
                bat4 = PlacerBateau( random, grilleJoueur, bateau4);

                //Placement du croiseur (bateau de 3) sur la grille du joueur
                bat3Croiseur = PlacerBateau(random, grilleJoueur, bateau3Croiseur);

                //Placement du sous-marin(bateau de 3) sur la grille du joueur
                bat3Sousmarin = PlacerBateau(random, grilleJoueur, bateau3Sousmarin);

                //Placement du contre-torpilleur (bateau de 2) sur la grille du joueur
                bat2 = PlacerBateau(random, grilleJoueur, bateau2);

            }

            AfficherGrilleJoueur(grilleJoueur);
        }
            
            //Permet de placer les bateaux de l'adversaire (sur la grille adversaire)
        public static void PlacerBateauAdversaire(Random random, string[,] grilleAdversaire, string[] bateau5, string[] bateau4, string[] bateau3Croiseur, string[] bateau3Sousmarin, string[] bateau2)
        {
            /*On initialise des booléens. Ils vont permettre de faire tourner la boucle while. Boucle qui va permettre de répèter l'opération du placement
              tant que tous les bateaux ne sont pas placés sur la grille.*/

            bool bat5 = false;
            bool bat4 = false;
            bool bat3Croiseur = false;
            bool bat3Sousmarin = false;
            bool bat2 = false;

            //Tant qu'une des variables booléennes est fausse, on rentre dans la boucle.
            //Autrement dit, dès que l'on détecte une erreur du type IndexOutOfRange, puisque la fonction "PlacerBateau" renvoit "false" lorsqu'une exception de type IndexOutOfRange est captée.
            while ((bat5 == false) || (bat4 == false) || (bat3Croiseur == false) || (bat3Sousmarin == false) || (bat2 == false))
            {
                /*On réinitialise la grille du joueur en remettant toutes les valeurs à 0, c'est à dire avec des espaces.
                  En effet, si tous les bateaux n'ont pas pu être placés, la boucle est exécutée de nouveau. 
                  Cependant, ce n'est pas parce qu'on reboucle qu'aucun bateau n'a été placé correctement précèdemment, c'est au moins l'un d'eux qui a généré l'erreur.
                  Ainsi il est nécessaire de réinitialiser la grille pour ne pas avoir trop de bateau.*/

                for (int i = 0; i < grilleAdversaire.GetLength(0); i++)
                {
                    for (int j = 0; j < grilleAdversaire.GetLength(0); j++)
                    {
                        grilleAdversaire[i, j] = " ";
                    }
                }

                //Placement du porte-Avion (bateau de 5) sur la grille de l'adversaire
                bat5 = PlacerBateau(random, grilleAdversaire, bateau5);

                //Placement du cuirassé (bateau de 4) sur la grille de l'adversaire
                bat4 = PlacerBateau(random, grilleAdversaire, bateau4);

                //Placement du croiseur (bateau de 3) sur la grille de l'adversaire
                bat3Croiseur = PlacerBateau(random, grilleAdversaire, bateau3Croiseur);

                //Placement du sous-marin (bateau de 3) sur la grille de l'adversaire
                bat3Sousmarin = PlacerBateau(random, grilleAdversaire, bateau3Sousmarin);

                //Placement du contre-torpilleur (bateau de 2) sur la grille de l'adversaire
                bat2 = PlacerBateau(random, grilleAdversaire, bateau2);
                
            }
        }


        //------------------ FONCTIONS PERMETTANT DE TRADUIRE LES CIBLES ENTREES PAR LE JOUEUR ---------------//

        //On rentre une case, on traduit pour ressortir le numéro de la ligne
        public static int LigneCible(string casevisee) 
        {

            int i; //Correspond à l'indice de la colonne dont le joueur a entré le numéro. Exemple : s'il rentre la colonne 1, c'est en réalité l'indice 0.

            //On traite le cas de la lettre, pour connaitre la ligne
            switch (casevisee[0])
            {
                
                //Pour chaque lettre, on traite le cas où l'utilisateur écrit en majuscule et celui où il écrit en minuscule.
                case ('A'):
                case ('a'): 
                    i = 0;
                    return i;

                case ('B'):
                case ('b'):
                    i = 1;
                    return i;

                case ('C'):
                case ('c'):
                    i = 2;
                    return i;

                case ('D'):
                case ('d'):
                    i = 3;
                    return i;

                case ('E'):
                case ('e'):
                    i = 4;
                    return i;

                case ('F'):
                case ('f'):
                    i = 5;
                    return i;

                case ('G'):
                case ('g'):
                    i = 6;
                    return i;

                case ('H'):
                case ('h'):
                    i = 7;
                    return i;

                case ('I'):
                case ('i'):
                    i = 8;
                    return i;

                case ('J'):
                case ('j'):
                    i = 9;
                    return i;

                case ('S'): 
                case ('s'):
                    return -2;                       

                default:
                    return -1;
            }
        }

        //On rentre une case, on traduit pour ressortir le numéro de la colonne
        public static int ColonneCible(string casevisee)
        {
            //On traite le cas du n° de colonne

            int j; //Correspond à l'indice de la colonne dont le joueur a entré le numéro. Exemple : s'il rentre la colonne 1, c'est en réalité l'indice 0.

            string numeroCase = casevisee.Substring(1); //On récuère le numéro entré par le joueur en prenant la commençant au caractère d'indice 1 dans la chaine de caractère "caseVisee".

            switch (numeroCase)
            {
                case ("1"):
                    j = 0;
                    return j;

                case ("2"):
                    j = 1;
                    return j;

                case ("3"):
                    j = 2;
                    return j;

                case ("4"):
                    j = 3;
                    return j;

                case ("5"):
                    j = 4;
                    return j;

                case ("6"):
                    j = 5;
                    return j;

                case ("7"):
                    j = 6;
                    return j;

                case ("8"):
                    j = 7;
                    return j;

                case ("9"):
                    j = 8;
                    return j;

                case ("10"):
                    j = 9;
                    return j;
                                   

                default:
                    return -1; //Ce cas nous permet de traiter le cas où la saisie est mauvaise. On peut donc demander à l'utilisateur de saisir une nouvelle valeur.
            }
        }

        //------------------ FONCTIONS PERMETTANT D'INTERPRETER LES TIRS DU JOUEUR ET DE L'ADVERSAIRE -------------------//

        public static int ResultatMissileJoueur(string[,] tab, int i, int j, int [] compteurJoueur, ref int compteurNbBat)
        {
            //On va d'abord traiter le cas où il y a un bateau

            // -------------------------- CAS AVEC UN BATEAU ------------------------------------//

            switch (tab[i, j])
            {
                case ("2"):
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("\nTouché ! ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("Bravo capitaine !\n");
                    

                    //On met un t pour indiquer qu'un bateau a été touché 
                    tab[i, j] = "t";

                    compteurJoueur[5]--; //On décrémente le compteur à chaque fois. Permet de savoir combien de case(s) le joueur doit encore toucher avant de la couler

                    if (compteurJoueur[5] == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nContre-torpilleur coulé capitaine !\n");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        return compteurNbBat - 1;
                    }
                    return compteurNbBat;

                case ("3c"):
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("\nTouché ! ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("Bravo capitaine !\n");
                    

                    tab[i, j] = "t";
                    compteurJoueur[3]--; //On décrémente le compteur à chaque fois. Permet de savoir combien de case(s) le joueur doit encore toucher avant de la couler
                    if (compteurJoueur[3] == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nCroiseur coulé capitaine !\n");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        return compteurNbBat - 1;
                    }
                    return compteurNbBat;

                case ("3s"):
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("\nTouché ! ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("Bravo capitaine !\n");
                    

                    tab[i, j] = "t";
                    compteurJoueur[4]--; //On décrémente le compteur à chaque fois. Permet de savoir combien de case(s) le joueur doit encore toucher avant de la couler
                    if (compteurJoueur[4] == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nSous-marin coulé capitaine !\n");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        return compteurNbBat - 1;
                    }
                    return compteurNbBat;

                case ("4"):
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("\nTouché ! ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("Bravo capitaine !\n");
                    

                    tab[i, j] = "t";

                    compteurJoueur[2]--; //On décrémente le compteur à chaque fois. Permet de savoir combien de case(s) le joueur doit encore toucher avant de la couler

                    if (compteurJoueur[2] == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nCuirassé coulé capitaine !\n");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        return compteurNbBat - 1;
                    }
                    return compteurNbBat;

                case ("5"):
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("\nTouché ! ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("Bravo capitaine !\n");
                    

                    tab[i, j] = "t";
                    compteurJoueur[1]--; //On décrémente le compteur à chaque fois. Permet de savoir combien de case(s) le joueur doit encore toucher avant de la couler
                    if (compteurJoueur[1] == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nPorte-avion coulé capitaine !\n");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        return compteurNbBat - 1;
                    }
                    return compteurNbBat;

                case ("t"): //Cas où il retire une case avec un bateau
                    
                    Console.WriteLine("\nCapitaine, vous avez perdu un coup, cette partie du bateau était déjà touchée !\n");
                    
                    return compteurNbBat;

                case ("plouf"): //Cas où il retire dans l'eau
                    
                    Console.WriteLine("\nCapitaine, vous avez perdu un coup, on avait déjà tiré ici !\n");
                    
                    return compteurNbBat;

                default: //Cas où c'est dans l'eau
                   
                    Console.WriteLine("\nDommage capitaine, c'est à côté !\n");
                    
                    tab[i, j] = "plouf";
                    return compteurNbBat;
            }
                                
        }

        public static int ResultatMissileAdversaire(string[,] tab, int i, int j, int [] compteurAdversaire)
        {
            //En parmètre on retrouve les indices des lignes et des colonnes tirées aléatoirement. Ce sont i et j.
            
            switch (tab[i, j])
            {
                 
                case ("2"): //Cas où le contre-torpilleur est touché
                    
                    Console.Write("Capitaine ! ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Notre contre-torpilleur a été touché ! ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("Soyez sans pitié !\n");
                   

                    //On met un t pour indiquer qu'un bateau a été touché 
                    tab[i, j] = "t";

                    
                    compteurAdversaire[5]--; //On décrémente le compteur à chaque fois. Permet de savoir combien de case(s) l'adversaire doit encore toucher avant de la couler

                    if (compteurAdversaire[5] == 0) //Cas où le bateau est coulé.
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Contre-torpilleur coulé capitaine !\n");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        return compteurAdversaire[0] - 1;
                    }
                    return compteurAdversaire[0]; //On retourne le nombre de bateau que l'adversaire doit encore couler.

                //Pour les bateaux suivants le fonctionnement est similaire.

                case ("3s"): //Cas où le sous-marin est touché.
                    tab[i, j] = "t";

                    
                    Console.Write("Capitaine ! ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Notre sous-marin a été touché ! ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("Soyez sans pitié !\n");
                    


                    compteurAdversaire[4]--;
                    if (compteurAdversaire[4] == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Sous-marin coulé capitaine ! \n");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        return compteurAdversaire[0] - 1;
                    }
                    return compteurAdversaire[0];

                case ("3c"): //Cas où le croiseur est touché
                    tab[i, j] = "t";
                                       
                    Console.Write("Capitaine ! ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Notre croiseur a été touché ! ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("Soyez sans pitié !\n");
                    


                    compteurAdversaire[3]--;
                    if (compteurAdversaire[3] == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Croiseur coulé capitaine ! \n");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        return compteurAdversaire[0] - 1;
                    }
                    return compteurAdversaire[0];

                case ("4"): //Cas où le cuirassé est touché
                    tab[i, j] = "t";
                    
                    Console.Write("Capitaine ! ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Notre cuirassé a été touché ! ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("Soyez sans pitié !\n");
                    

                    compteurAdversaire[2]--;
                    if (compteurAdversaire[2] == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Cuirassé coulé capitaine ! \n");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        return compteurAdversaire[0] - 1;
                    }
                    return compteurAdversaire[0];

                case ("5"): //Cas où le porte-avion est touché
                    tab[i, j] = "t";

                    
                    Console.Write("Capitaine ! ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Notre porte-avion a été touché ! ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("Soyez sans pitié !\n");
                    


                    compteurAdversaire[1]--;
                    if (compteurAdversaire[1] == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Porte-avion coulé capitaine ! \n");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        return compteurAdversaire[0] - 1;
                    }
                    return compteurAdversaire[0]; 

                default: // Cas où la case contient juste un espace, c'est à dire qu'il n'y a pas de bateau.
                    tab[i, j] = "plouf";
                    
                    Console.WriteLine("Capitaine ! L'adversaire a tiré dans l'eau ! Il faut en profiter !\n");
                    
                    return compteurAdversaire[0]; 
            }

        }

        
        //------------------ SAUVEGARDE ---------------//


        //Cette fonction va permettre de sauvegarder les grilles
        public static void SauvegarderPartie(string[,] tabJoueur, string[,] tabAdv, int [] tabCompteurJoueur, int [] tabCompteurAdversaire)
        {

            Console.WriteLine("Quel nom souhaitez-vous donner à la sauvegarde ?");
            string nomSauvegarde = Console.ReadLine();

            //On sauvegarde la grille du joueur
            using (StreamWriter writer = new StreamWriter(nomSauvegarde + "GrilleJoueur.txt"))
            {

                // ------------ SAUVEGARDE GRILLE JOUEUR ----------------------//

                string partieJoueur = string.Empty;         //On écrira la matrice du joueur dans cette variable.


                //On parcourt la matrice du joueur, afin de sauvegarder la valeur chaque case de la matrice
                for (int i = 0; i < tabJoueur.GetLength(0); i++)
                {
                    for (int j = 0; j < tabJoueur.GetLength(0); j++)
                    {
                        
                        partieJoueur += tabJoueur[i, j] + "/";      //On met un séparateur, pour pouvoir récupérer les données facilement par la suite
                                                
                    }
                    partieJoueur += Environment.NewLine;  //On revient à la ligne

                }
                writer.WriteLine(partieJoueur);         // On écrit tout ce qui a été enregistré sur le fichier texte
                
            }

            
            using (StreamWriter writer = new StreamWriter(nomSauvegarde + "GrilleAdversaire.txt"))
            {

                string partieAdversaire = string.Empty;     //On écrira la matrice de l'adversaire dans cette variable.

                // ------------ SAUVEGARDE GRILLE ADVERSAIRE ----------------------
                //On parcourt la matrice de L'ADVERSAIRE, afin de sauvegarder la valeur de chaque case de la matrice

                for (int i = 0; i < tabAdv.GetLength(0); i++)
                {
                    for (int j = 0; j < tabAdv.GetLength(0); j++)
                    {
                        
                        partieAdversaire += tabAdv[i, j] + "/";      //On met un séparateur, pour pouvoir récupérer les données facilement par la suite
                                                
                    }
                    partieAdversaire += Environment.NewLine;  //On revient à la ligne

                }
                writer.Write(partieAdversaire);         // On écrit tout ce qui a été enregistré sur le fichier texte

            }

            // ------------ SAUVEGARDE DES COMPTEURS ----------------------

            SauvergarderCompteur(tabCompteurJoueur, tabCompteurAdversaire, nomSauvegarde);
        }

        //Cette fonction va permettre de sauvegarder les compteurs concernant une grille (nb de bateau restant, avancement dans la Bataille)
        //Cette fonction est appelée dans la première.
        public static void SauvergarderCompteur(int [] tabCompteurJoueur, int [] tabCompteurAdversaire, string nomSauvegarde)
        {
            //SAUVEGARDE DES COMPTEURS EN RELATION AVEC LA GRILLE DU JOUEUR

            using (StreamWriter writer = new StreamWriter(nomSauvegarde + "CompteurJoueur.txt"))
            {
                string compteurJoueur = string.Empty;

                for (int i = 0; i < tabCompteurJoueur.Length; i++)
                {                    
                    if (i == 0)
                    {
                        compteurJoueur += tabCompteurJoueur[i];
                    }
                    else
                    {
                        compteurJoueur += "/" + tabCompteurJoueur[i];      //On met un séparateur ou délimiteur, pour pouvoir récupérer les données facilement par la suite

                    }
                    
                    
                }
                writer.WriteLine(compteurJoueur);
                
            }

            //SAUVEGARDE DES COMPTEURS EN RELATION AVEC LA GRILLE DE L'ADVERSAIRE

            using (StreamWriter writer = new StreamWriter(nomSauvegarde + "CompteurAdversaire.txt"))
            {
                string compteurAdversaire = string.Empty;

                for (int i = 0; i < tabCompteurAdversaire.Length; i++)
                {
                    if (i == 0)
                    {
                        compteurAdversaire += tabCompteurAdversaire[i];
                    }
                    else
                    {
                        compteurAdversaire += "/" + tabCompteurAdversaire[i];      //On met un séparateur, pour pouvoir récupérer les données facilement par la suite

                    }
                    
                }
                writer.WriteLine(compteurAdversaire);

            }

        }

        //------------------ CHARGEMENT ---------------//

        public static void ChargerPartie(string[,] sauvegardeJoueur, string[,] sauvegardeAdversaire, int[] tabCompteurJoueur, int[] tabCompteurAdversaire)
        {
            Console.WriteLine("Quel est le nom de la partie que vous souhaitez charger ?");
            string nomPartie = Console.ReadLine();


            using (StreamReader reader = new StreamReader(nomPartie + "GrilleJoueur.txt"))
            {

                //------------- CHARGEMENT DU TABLEAU JOUEUR -------------------
                int indexTableauJoueur = 0;
                string txt_j;
                string[] tabGrilleJoueur;

                while (((txt_j = reader.ReadLine()) != null) && indexTableauJoueur < 10)
                {
                    tabGrilleJoueur = txt_j.Split('/');
                    for (int k = 0; k < tabGrilleJoueur.Length - 1; k++) //Y a un pb là
                    {
                        sauvegardeJoueur[indexTableauJoueur, k] = tabGrilleJoueur[k];
                    }
                    indexTableauJoueur++;
                }

                AfficherGrilleJoueur(sauvegardeJoueur);
            }

            using (StreamReader reader = new StreamReader(nomPartie + "GrilleAdversaire.txt"))
            {

                //------------- CHARGEMENT DU TABLEAU ADVERSAIRE -------------------

                int indexTableauAdversaire = 0;
                string txt_a;
                string[] tabGrilleAdversaire;

                while (((txt_a = reader.ReadLine()) != null) && indexTableauAdversaire < 10)
                {

                    tabGrilleAdversaire = txt_a.Split('/');                                       

                    for (int k = 0; k < tabGrilleAdversaire.Length - 1; k++)
                    {
                        sauvegardeAdversaire[indexTableauAdversaire, k] = tabGrilleAdversaire[k];
                    }
                    indexTableauAdversaire++;
                }
                AfficherGrilleAdversaire(sauvegardeAdversaire);
            }

            using (StreamReader reader = new StreamReader(nomPartie + "CompteurJoueur.txt"))
            {
                string txt_j;

                while ((txt_j = reader.ReadLine()) != null)
                {
                    string[] tab = txt_j.Split('/');
                    for (int k = 0; k < tab.Length - 1; k++)
                    {
                        tabCompteurJoueur[k] = Convert.ToInt32(tab[k]);
                    }
                }


            }

            using (StreamReader reader = new StreamReader(nomPartie + "CompteurAdversaire.txt"))
            {
                string txt_a;

                while ((txt_a = reader.ReadLine()) != null)
                {
                    string[] tab = txt_a.Split('/');
                    for (int k = 0; k < tab.Length - 1; k++)
                    {
                        tabCompteurAdversaire[k] = Convert.ToInt32(tab[k]);
                    }
                }

            }

        }


        //------------------ FONCTIONS PERMETTANT DE GERER LE TOUR DU JOUEUR ---------------//

        public static int TourJoueur(int[] compteurJoueur, int[] compteurAdversaire, string[,] grilleJoueur, string[,] grilleAdversaire)
        {
            Console.WriteLine("\n\n\n------------ C'EST AU TOUR DU JOUEUR ---------------");

            //On définit les tableaux dans lesquels on stocke les différentes cibles du joueur
            int[] ligne = new int[compteurAdversaire[0]]; //Stockage des indices des lignes
            int[] colonne = new int[compteurAdversaire[0]]; //Stockage des indices des colonnes

            
            Console.WriteLine("\nCapitaine, vous avez {0} coup(s), ne les ratez pas !\n\nAnalysez bien la situation, voici la grille adverse ! \n\n", compteurJoueur[0]);
            AfficherGrilleAdversaire(grilleAdversaire);
            
            //On appelle la procédure permettant au joueur de saisir les cibles qu'il souhaite.
            ModeSalvo(compteurJoueur, compteurAdversaire, ligne, colonne, grilleJoueur, grilleAdversaire);

            /* Pour gérer le traitement des cibles mises en mémoire dans les tableaux "ligne" et "colonne" nous utilisons la fonction "resultatMissileJoueur"
            Cette fonction renvoit une valeur correspondant au nombre de bateau que le joueur doit encore toucher. 
            Cette valeur, est également en paramètre de la boucle for. Il est donc nécéssaire de créer une variable temporaire, 
            pour éviter de modifier le nombre de tir si un bateau est coulé au cours de la salve.*/


            int temp = compteurJoueur[0]; //temp prends la valeur correspondant au nombre de bateau que le joueur doit encore toucher.

            //Traitement des différentes cibles visées dans la procédure "ModeSalvo"
            for (int k = 0; k < compteurJoueur[0]; k++)
            {

                //Permet de dire quel bateau est touché ou coulé 
                temp = ResultatMissileJoueur(grilleAdversaire, ligne[k], colonne[k], compteurJoueur, ref temp);
                //Si un bateau est coulé, la valeur temp diminue de 1, mais il faut que les coups suivants s'affichent quand même.

            }

            compteurJoueur[0] = temp; //Le compteur change de valeur et prend la valeur décrémentée ou pas.

            return compteurJoueur[0];

        }

        //Cette fonction permet de traiter le mode salvo d'une partie. Elle est appelée dans la fonction précédente.
        public static void ModeSalvo(int[] compteurJoueur, int[] compteurAdversaire, int[] ligne, int[] colonne, string[,] grilleJoueur, string[,] grilleAdversaire)
        {

            //La boucle for va permettre de gérer le nombre de tir que le joueur peut faire(en fonction du nombre de bateau qu'il a déjà coulé.
            for (int m = 0; m < compteurJoueur[0]; m++)
            {
                string caseVisee; //Prend la valeur saisie par le joueur et correspondant à la cible à viser.
                int continuer = 1; 

                Console.WriteLine("\n\n------------------------- TIR {0} -------------------- ", (m + 1));


                //Ce do-while va permettre de demander à l'utilisateur de saisir une nouvelle case lorsqu'il l'a mal saisie, ou bien qu'elle n'existe pas.
                do
                {
                    //On demande une cible au joueur
                    Console.WriteLine("\n\nCapitaine, où voulez-vous tirer ?! (Tapez S pour sauvegarder)"); //Pour sauvegarder le joueur doit écrire "S". Il est maitre de quand il sauvegarde.
                    caseVisee = Console.ReadLine();

                    if (LigneCible(caseVisee) == -2) //Cas où l'utilisateur demande une sauvegarde
                    {
                        /*Pour rentrer dans ce cas il faut que le joueur ait tapé "s" ou "S" ET qu'il n'y a rien derrière (ça ne sauvegardera pas si le joueur tape S1302)*/
                        
                        //--------------------- DEMANDE DE SAUVEGARDE ---------------------------//

                        SauvegarderPartie(grilleJoueur, grilleAdversaire, compteurJoueur, compteurAdversaire);

                        //Le joueur peut décider de sauvegarder à n'importe quel moment, même s'il veut continuer de jouer. 
                        Console.WriteLine("Voulez-vous poursuivre la partie ? 0 pour non, 1 pour oui. ");
                        continuer = Convert.ToInt32(Console.ReadLine());
                        break;                                    
                        
                    }

                    else if ((LigneCible(caseVisee) == -1) || (ColonneCible(caseVisee) == -1)) //Cas où il y a une erreur de saisie au niveau de la ligne OU de la colonne.
                    {
                        Console.WriteLine("Erreur de saisie, capitaine concentrez-vous !");
                    }
                    
                }
                while ((LigneCible(caseVisee) == -1) || (ColonneCible(caseVisee) == -1));
                //On reboucle tant que le joueur n'a pas saisie correctement une cible.

                //Permet de traiter le cas de si le joueur veut continuer ou non à jouer après une sauvegarde.
                if (continuer == 0) // Il ne veut pas continuer
                {                    
                    Environment.Exit(0); //On quitte tout, et on ferme la console.

                }
                else
                {
                    //On traduit la lettre en indice, et on la stocke dans le tableau ligne. Ce stockage permet "d'enregistrer" momentanément plusieurs cibles saisies par le joueur 
                    ligne[m] = LigneCible(caseVisee);

                    //On traduit le numéro en indice, et on la stocke dans le tableau colonne. Ce stockage permet "d'enregistrer" momentanément plusieurs cibles saisies par le joueur
                    colonne[m] = ColonneCible(caseVisee); 

                }


            }
        }

        //------------------ FONCTIONS PERMETTANT DE GERER LE TOUR DE L'ADVERSAIRE ---------------//

        public static void TourAdversaire(Random random, int [] compteurAdversaire, string [,] grilleJoueur)
        {
            Console.WriteLine("\n\n\n------------ C'EST AU TOUR DE l'IA ---------------\n\n");

            for (int m = 0; m < compteurAdversaire[0]; m++)
            {
               //Permet de vérifier si la case choisie n'avait pas été déjà choisie auparavant, cela permet d'éviter que l'adversaire tirer deux fois au même endroit.
                bool verif = false;

                do
                {
                    //La première case où va tirer l'adversaire est choisie aléatoirement
                    int ligneCibleInitiale = random.Next(0, 10); 
                    int colonneCibleInitiale = random.Next(0, 10); 

                    if (grilleJoueur[ligneCibleInitiale, colonneCibleInitiale] == "t") //Cas où la case a déjà été touchée (tir sur un bateau)
                    {
                        verif = false; //On met un false pour reboucler, et tirer une nouvelle case.
                    }

                    else if (grilleJoueur[ligneCibleInitiale, colonneCibleInitiale] == "plouf") //Cas où la case a déjà été touchée (tir dans l'eau)
                    {
                        verif = false; //On met un false pour reboucler, et tirer une nouvelle case.
                    }

                    else //La case n'a jamais été touchée. 
                    {
                        //On traduit le tir de l'adversaire sur la cible, et on regarde ce qu'il a touché. 

                        compteurAdversaire[0] = ResultatMissileAdversaire(grilleJoueur, ligneCibleInitiale, colonneCibleInitiale, compteurAdversaire);

                        verif = true; //Permet de sortir du do-while et de continuer.
                        
                    }
                }

                while (verif == false);

            }
            AfficherGrilleJoueur(grilleJoueur);


            //Permet au joueur de prendre son temps pour analyser la situation, et les différents coups.

            Console.WriteLine("\n\nCapitaine, avez-vous eu le temps d'analyser l'état de votre flotte ? Si oui, appuyez sur entrée...");
            Console.ReadLine();
            Console.Clear(); //nettoie la console avant l'affichage de la nouvelle grille de jeu
        }


        //------------------ FONCTIONS PERMETTANT DE FAIRE LA BATAILLE ---------------//

        public static void Bataille(Random random, int [] compteurJoueur, int [] compteurAdversaire, string [,] grilleJoueur, string [,] grilleAdversaire)
        {

            /*On fait une boucle while. La partie doit continuer tant qu'il reste des bateaux sur les deux grilles.
             S'il n'y a plus de bateau sur une des deux, la partie est terminée !*/

            while ((compteurAdversaire[0] != 0) && (compteurJoueur[0] != 0)) 
            {
                //------------------- TOUR DU JOUEUR ------------------------

                //Le joueur joue : sélection de la ou des case(s) à viser (en fonction de l'avancement de la partie).

                TourJoueur(compteurJoueur, compteurAdversaire, grilleJoueur, grilleAdversaire);

                //Permet de sortir directement de la boucle dans le cas où c'est le joueur qui gagne. Sinon l'adversaire jouerait son tour avant que l'on sorte de la boucle. 
                if (compteurJoueur[0] == 0)
                {
                    break;
                }

                AfficherGrilleAdversaire(grilleAdversaire);

                //Permet au joueur de prendre son temps pour analyser la situation, et les différents coups.
                Console.WriteLine("\n\nCapitaine, avez-vous eu le temps d'analyser vos cibles ? Si oui, appuyez sur entrée...");
                Console.ReadLine();
                Console.Clear(); //nettoie la console avant l'affichage de la nouvelle grille de jeu

                //------------------- TOUR DE L'ADVERSAIRE ------------------------

                //L'adversaire joue : sélection d'une ou plusieurs case(s) (en fonction de l'avancement de la partie) de manière aléatoire.
                TourAdversaire(random, compteurAdversaire, grilleJoueur);

            }
        }


        //------------------ FONCTION PERMETTANT DE GERER LE GAIN DE LA PARTIE ---------------//

        public static void GagnerPartie(int [] compteurJoueur, string[,] grilleAdversaire, int pays)
        {
            //Va permettre d'indiquer au joueur s'il a gagné ou perdu la partie.
            
            if(pays == 1)
            {
                if (compteurJoueur[0] == 0) //Ce compteur compte le nombre de bateau restant à toucher pour le joueur, s'il vaut 0, c'est qu'il a gagné.
                {
                    Console.WriteLine("CONGRATULATIONS Capitaine ! Vous avez gagné ! Vous êtes le meilleur ! \n");

                }
                else //Cas où le compteur qui compte le nombre de bateau restant à toucher pour l'adversaire vaut 0.
                {
                    Console.WriteLine("Capitaine, toute la flotte a été détruite... Ces nord coréens sont redoutables ! Ne vous laissez pas faire ! Retentez votre chance !");
                    AfficherGrilleJoueur(grilleAdversaire);

                }
            }
            else
            {
                if (compteurJoueur[0] == 0) //Ce compteur compte le nombre de bateau restant à toucher pour le joueur, s'il vaut 0, c'est qu'il a gagné.
                {
                    Console.WriteLine("chugha haeyo Capitaine ! Vous avez gagné ! Vous êtes le meilleur ! \n");

                }
                else //Cas où le compteur qui compte le nombre de bateau restant à toucher pour l'adversaire vaut 0.
                {
                    Console.WriteLine("Capitaine, toute la flotte a été détruite... Ces américains sont redoutables ! Ne vous laissez pas faire ! Retentez votre chance !");
                    AfficherGrilleJoueur(grilleAdversaire);

                }
            }

            



        }


        public static void JouerPartie(Random random)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            

            //On définit tous les bateaux. Ces tableaux nous seront utiles pour le placement des bateaux.

            string[] porteAvion = { "5", "5", "5", "5", "5" };
            string[] bateau4 = { "4", "4", "4", "4" };
            string[] bateau3Croiseur = { "3c", "3c", "3c" };
            string[] bateau3Sousmarin = { "3s", "3s", "3s" };
            string[] bateau2 = { "2", "2" };


            //On définit les grilles, correspondant à deux matrices de 10 par 10.
            string[,] grilleJoueur = new string[10, 10];
            string[,] grilleAdversaire = new string[10, 10];

            /*On définit des compteurs pour savoir au fur et à mesure de la partie : 
                    - le nombre de bateau encore à flots sur une grille donnée
                    - le nombre de case touchée par bateau (on aura donc un compteur par bateau)
              
            Ces comteurs sont présents pour la grille du joueur et de l'adversaire.            
            Ils sont stockés dans deux tableaux dstincts : un pour le joueur et un pour l'adversaire. 
            */

            // °°°°°°°°° ADVERSAIRE °°°°°°°°°°//
            //Compte les bateaux que l'adversaire doit encore toucher (sur la grille du joueur)

            int[] compteurAdversaire = new int[6]; //On définit le tableau.

            //On remplit le tableau des compteurs gérant la flotte de l'adversaire.

            compteurAdversaire[0] = 5; //Correspond au nombre de bateau que l'adversaire doit encore toucher avant de gagner. (Autrement dit le nombre de bateau encore à flot dans la flotte du Joueur).
            compteurAdversaire[1] = 5; //Correspond au nombre de cibles restantes à toucher (pour l'adversaire) sur le porte-avion (du joueur).
            compteurAdversaire[2] = 4; //Correspond au nombre de cibles restantes à toucher (pour l'adversaire) sur le cuirassé (du joueur).
            compteurAdversaire[3] = 3; //Correspond au nombre de cibles restantes à toucher (pour l'adversaire) sur le croiseur (du joueur).
            compteurAdversaire[4] = 3; //Correspond au nombre de cibles restantes à toucher (pour l'adversaire) sur le sous-marin (du joueur).
            compteurAdversaire[5] = 2; //Correspond au nombre de cibles restantes à toucher (pour l'adversaire) sur le contre-torpilleur (du joueur).


            // °°°°°°°°° JOUEUR °°°°°°°°°°
            //Compte les bateaux que le joueur doit encore toucher (sur la grille de l'adversaire)

            int[] compteurJoueur = new int[6];

            //On remplit le tableau des compteurs gérant la flotte du joueur.

            compteurJoueur[0] = 5; //Correspond au nombre de bateau que le joueur doit encore toucher avant de gagner. (Autrement dit le nombre de bateau encore à flot dans la flotte de l'adversaire).
            compteurJoueur[1] = 5; //Correspond au nombre de cibles restantes à toucher (pour le joueur) sur le porte-avion (de l'adversaire).
            compteurJoueur[2] = 4; //Correspond au nombre de cibles restantes à toucher (pour le joueur) sur le cuirassé (de l'adversaire).
            compteurJoueur[3] = 3; //Correspond au nombre de cibles restantes à toucher (pour le joueur) sur le croiseur (de l'adversaire).
            compteurJoueur[4] = 3; //Correspond au nombre de cibles restantes à toucher (pour le joueur) sur le sous-marin (de l'adversaire).
            compteurJoueur[5] = 2; //Correspond au nombre de cibles restantes à toucher (pour le joueur) sur le contre-torpilleur (de l'adversaire).


            //On fait une condition, en fonction de si le joueur veut charger une partie existante ou non
                        
            Console.WriteLine("Voulez-vous charger une partie déjà existante ? 0 pour non, 1 pour oui");
            int chargement = Convert.ToInt32(Console.ReadLine());

            int pays = 0; //Le joueur peut choisir le pays qu'il souhaite incarner

            if (chargement == 0) //Cas où le joueur ne veut pas charger une partie. 
            {
                Console.Clear(); //On nettoie la console 
                
                //Mise en place du contexte

                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(" \n  /!\\ ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(" Vous vous apprétez à jouer à la Bataille navale. STOP.");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(" /!\\  \n \n /!\\");
                Console.ForegroundColor = ConsoleColor.Gray;

                Console.Write(" Soyez prêt à donner corps et âme pour obtenir la victoire. STOP");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" /!\\ \n\n ");
                Console.ForegroundColor = ConsoleColor.Gray;


                Console.WriteLine("===========================================================================");
                Console.WriteLine("                         BUT DE LA MISSION                                 ");
                Console.WriteLine("===========================================================================");

                
                Console.WriteLine("\n Vous êtes en 2048. \n\n Au fil des années, les relations entre la Corée du Nord et les Etats-Unis \n d’Amérique se sont complexifiées, et la tension entre ces deux pays a atteint \n son paroxysme. Suite à des échanges mouvementés, le président des Etats-Unis \n Donald Trump, a tweeté l'envoi de sa flotte en Corée du Nord pour montrer \n à Kim Jong-un qui est le patron. \n\n\n Quel rôle déciderez vous d’endosser ? \n\n\n Celui d’un soldat américain fièr de défendre sa patrie ? \n Ou bien, celui d’un jeune nord coréen prêt à résister à la menace américaine ?");

                Console.WriteLine("\n\n\n===========================================================================");
                Console.WriteLine("                         CHOIX DE LA FLOTTE                                 ");
                Console.WriteLine("===========================================================================");


                Console.WriteLine("\n\n Quel pays voulez vous jouer ? \n               (1) Etats-Unis d'Amérique \n               (2) Corée du Nord");
                pays = Convert.ToInt32(Console.ReadLine());

                do
                {
                    if (pays == 1)
                    {
                        Console.WriteLine("\n\n\n===========================================================================");
                        Console.WriteLine("                      Welcome on board soldier !                                 ");
                        Console.WriteLine("===========================================================================");
                    }
                    else if (pays == 2)
                    {
                        Console.WriteLine("\n\n\n===========================================================================");
                        Console.WriteLine("                      Welcome hwan-yeong jojongsa !                             ");
                        Console.WriteLine("===========================================================================");
                    }
                }
                while (pays != 1 && pays != 2);

                //Le joueur démarre une nouvelle partie, il faut donc placr les bateaux du joueur et de l'adversaire.

                //  ---------------------------- PLACEMENT DES BATEAUX DU JOUEUR ----------------------------------------- //

                int satisfait = 0;

                while (satisfait == 0)
                {
                    
                    //On place les bateaux du joueur
                    PlacerBateauJoueur(random, grilleJoueur, porteAvion, bateau4, bateau3Croiseur, bateau3Sousmarin, bateau2);

                    //On vérifie si la configuration lui convient.
                    Console.WriteLine("\nEtes-vous satisfait de cette configuration ? 0 si non, 1 si oui. ");
                    satisfait = Convert.ToInt32(Console.ReadLine());

                    Console.Clear(); //On nettoie la console pour plus de clareté. 

                } //Si il est satisfait, il sort de la boucle


                //  ---------------------------- PLACEMENT DES BATEAUX DE L'ADVERSAIRE ----------------------------------------- //

                PlacerBateauAdversaire(random, grilleAdversaire, porteAvion, bateau4, bateau3Croiseur, bateau3Sousmarin, bateau2);

            }

            else //Cas où le joueur décide de charger une partie.
            {
                //--------------------- CHARGE DE LA PARTIE ---------------------------

                ChargerPartie(grilleJoueur, grilleAdversaire, compteurJoueur, compteurAdversaire);

            }


            // ------------------------------------- BATAILLE ------------------------------------------- //


            Bataille(random, compteurJoueur, compteurAdversaire, grilleJoueur, grilleAdversaire);


          
            // ------------------------------------- GAIN DE LA PARTIE ------------------------------------------- //
            
            GagnerPartie(compteurJoueur, grilleAdversaire, pays);
        }
        
        static void Main(string[] args)
        {

            Random random = new Random(); //Ne créer qu'un Random par programme et le prendre en entrée la ou on s'en sert

            //On souhaite que le joueur puisse rejouer en fin de partie. On fait donc une boucle do-while. 

            string reponse;
            
            do
            {
                //On lance une première partie, avant de lui demander s'il veut rejouer. 
                JouerPartie(random); 

                //A la fin de la partie, on demande au joueur s'il veut rejouer ou non 
                Console.WriteLine("\n\nVoulez-vous rejouez ? 0 pour non, 1 pour oui\n\n");
                reponse = Console.ReadLine();

                if (reponse == "0") //il ne veut pas rejouer.
                {
                    Environment.Exit(0); //On quitte tout et on ferme la console.
                }
                else if(reponse == "1")
                {
                    Console.Clear();        //On nettoie la console.
                    JouerPartie(random);    //On relance une partie.
                     
                }
            } while (reponse != "0" || reponse != "1");

            
            Console.ReadKey();        
        }
    }
}
