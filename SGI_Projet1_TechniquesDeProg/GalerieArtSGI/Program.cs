using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGI;

namespace GalerieArtSGI
{
    class Program
    {    
        //déclarations    
        static bool quitter = false;
        static bool codeValide;
        static string choix;        
        static List<string> result;
        static string menu;
       
        static void Main(string[] args)
        {            
            Galerie galerie = new Galerie();
            Validations validations = new Validations();        
            do
            {
                choix = " ";
                do
                {
                    menu = galerie.AfficherMenuPrincipal();
                    Console.WriteLine(menu);
                    Console.WriteLine("Veuillez saisir le chiffre correspondant à votre choix :  ");
                    choix = Console.ReadLine();
                    codeValide = validations.ValiderChoix(choix);                              
                }
                while (!codeValide);

                try
                {
                    if ((choix == "1") || (choix == "2") || (choix == "3") || (choix == "7"))
                    {
                        List<string> reponse = new List<string>();
                        result = (List<string>)galerie.AppelerModule(choix);
                        foreach (string question in result)
                        {
                            Console.WriteLine(question);
                            string temp = Console.ReadLine();              
                            reponse.Add(temp);
                        }                        
                        string message = galerie.ReponsesObtenues(reponse, choix);
                        Console.WriteLine(message);
                        quitter = false; 
                    }
                    else
                    {
                        string reponse = (string)galerie.AppelerModule(choix);
                        Console.WriteLine(reponse);
                        if ((choix == "4") || (choix == "5") || (choix == "6") || (choix == "8"))
                        {                            
                            quitter = false;
                        }
                        if (choix == "9")
                        {                            
                            quitter = true;
                        }
                        else
                        {
                            validations.MessageNonValide();
                            quitter = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    validations.MessageNonValide();
                    quitter = false;
                }
            }
            while (!quitter);
        }
    }
}