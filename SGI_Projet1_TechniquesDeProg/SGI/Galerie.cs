using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGI
{
    public class Galerie //classe boss
    {        
        private Artistes listeArtistes = new Artistes();
        private Conservateurs listeConservateurs = new Conservateurs();
        private Oeuvres listeOeuvres = new Oeuvres();
        private Validations validations = new Validations();
        private bool oeuvreNonVendue = true;

        //accesseurs... je ne suis pas certaine de la nécessité de l'accès en écriture par Galerie...
        public Conservateurs ListeConservateurs 
        { 
            get
            { 
                return listeConservateurs;
            }
            set
            {
                listeConservateurs = value;
            }
        }
        public Artistes ListeArtistes 
        { 
            get
            {
                return listeArtistes;
            }
            set
            {
                listeArtistes = value;
            }
        }

        public Oeuvres ListeOeuvres
        {
            get
            {
                return listeOeuvres;
            }
            set
            {
                listeOeuvres = value;
            }
        }

        

        //méthode pour lier l'objet conservateur correspondant à l'idConservateur entré par l'usager
        public Conservateur GetConservateur(string idConservateur)
        {
            Conservateur conservateurTrouve = null;
            foreach (Conservateur conservateur in ListeConservateurs)
            {
                
                if (conservateur.IDConservateur == idConservateur)
                {                    
                    conservateurTrouve = conservateur;                    
                }                               
            }
            return conservateurTrouve;
        }
        
        //méthode pour lier l'objet artiste correspondant à l'idArtiste entré par l'usager
        public Artiste GetArtiste(string idArtiste)
        {
            Artiste artisteTrouve = null;
            foreach (Artiste artiste in ListeArtistes)
            {

                if (artiste.IDArtiste == idArtiste)
                {
                    artisteTrouve = artiste;
                }
            }
            return artisteTrouve;
        }

        
        public void AjouterArtiste(string Prenom, string NomFamille, Conservateur ConservateurAttitre)
        {            
            Artiste artiste = new Artiste(Prenom, NomFamille, ConservateurAttitre);
            ListeArtistes.Ajouter(artiste); 
        }

                
        public void AjouterConservateur(string Prenom, string NomFamille)
        {
            Conservateur conservateur = new Conservateur(Prenom, NomFamille);
            ListeConservateurs.Ajouter(conservateur);
        }
               
        public void AjouterOeuvre(string Titre, Artiste ArtisteCreateur, double Estimation, string Annee)
        {
            Oeuvre oeuvre = new Oeuvre(Titre, ArtisteCreateur, Estimation, Annee);
            ListeOeuvres.Ajouter(oeuvre);
        }

        
        public bool VendreOeuvre(string idOeuvre, double PrixPaye)
        {
            Oeuvre oeuvreTrouve = null;
            try
            {
                foreach (Oeuvre oeuvre in ListeOeuvres)
                {                
                    if (oeuvre.IDOeuvre == idOeuvre)
                    {
                        oeuvreTrouve = oeuvre;
                        {
                            if (oeuvreTrouve.Etat == 'V')
                            {
                                oeuvreNonVendue = false;
                                validations.MessageNonValide();
                            }
                            else
                            {
                                oeuvreNonVendue = true;
                            }
                        }
                    }
                }                               
            }
            catch (Exception fail)
            {
                oeuvreNonVendue = false;
                Console.WriteLine(fail.Message);                
            }
            finally
            {
                if ((oeuvreNonVendue) && (oeuvreTrouve != null))
                {
                    if (PrixPaye <= oeuvreTrouve.Estimation)
                    {
                        validations.MessageNonValide();                        
                        oeuvreNonVendue = false;
                    }

                    string idArtiste = oeuvreTrouve.ArtisteCreateur.IDArtiste;
                    Artiste artisteCreateur = GetArtiste(idArtiste);
                    oeuvreTrouve.ChangerEtat();
                    oeuvreTrouve.PrixDeVente(PrixPaye);
                    double montant = oeuvreTrouve.CalculerCommission(PrixPaye);
                    oeuvreTrouve.ArtisteCreateur.SetComm(montant);
                }
            }
            
            return oeuvreNonVendue;
        }

        
        public string ProduireRapportVentes()
        {
            int cnt = 0;
            double ventesTotales = 0;
            double ventesTotalesCurator = 0;
            double profitTotalCurator = 0;
            double commissionsTotalesCurator = 0;
            string s = "Ligne qui ne devrait pas s'afficher.";
            string sGeneral;
            string sCurator;
            string sPiece = null;            
            string sSummary;
            string sEachPiecesOfCurator = null;
            string sEveryCurator = null;
                        
            try
            {
                int i = ListeOeuvres.Count;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            foreach (Oeuvre oeuvre in ListeOeuvres)
            {
                if (oeuvre.Etat == 'V')
                {
                    cnt++;
                    ventesTotales = ventesTotales + oeuvre.PrixOeuvreVendue;
                } 
            }

            sGeneral = "\nOeuvres vendues \t\t" + "Ventes totales\n" +
                "---------------------------------------------------------\n" +
                cnt.ToString() + "\t\t" + ventesTotales.ToString("0.00") + "\n" +
                "---------------------------------------------------------\n\n";

            foreach (Conservateur conservateur in ListeConservateurs)
            {
                string idComparison = conservateur.IDConservateur;
                sCurator = "**Conservateur**\n" +
                    "Code d'identification\t" + "Nom de famille\t" + "Prénom\n" +
                    conservateur.IDConservateur + "\t" + conservateur.NomFamille + "\t" + conservateur.Prenom + "\n\n" +
                    "---------------------------------------------------------\n\n" +
                    "*Oeuvres vendues*\n\n";

                foreach (Oeuvre oeuvre in ListeOeuvres)
                {
                    if ((oeuvre.Etat == 'V') && (oeuvre.ArtisteCreateur.ConservateurAttitre.IDConservateur == idComparison))
                    {
                        sPiece += "Code d'identification :\t\t" + oeuvre.IDOeuvre + "\n" +
                            "Titre :\t\t" + oeuvre.Titre + "\n" +
                            "Artiste :\t\t" + oeuvre.ArtisteCreateur.Prenom + " " + oeuvre.ArtisteCreateur.NomFamille + "\n" +
                            "Valeur estimée :\t\t" + oeuvre.Estimation.ToString("0.00") + " $\n" +
                            "Prix de vente :\t\t" + oeuvre.PrixOeuvreVendue.ToString("0.00") + " $\n\n";
                        ventesTotalesCurator = ventesTotalesCurator + oeuvre.PrixOeuvreVendue;
                        profitTotalCurator = profitTotalCurator + (oeuvre.PrixOeuvreVendue - oeuvre.Estimation);
                        commissionsTotalesCurator = 0.25 * profitTotalCurator;
                    }                    
                }

                sSummary = "*Sommaire*\n" + "---------------------------------------------------------\n\n" +
                    "Ventes totales :\t\t" + ventesTotalesCurator.ToString("0.00") + " $\n" +
                    "Profit :\t\t" + profitTotalCurator.ToString("0.00") + " $\n" +
                    "Commission :\t\t" + commissionsTotalesCurator.ToString("0.00") + " $\n";

                sEachPiecesOfCurator = sCurator + sPiece + sSummary;
            }

            sEveryCurator += sEachPiecesOfCurator;

            s = sGeneral + sEveryCurator;

            return s;
        }


        public string AfficherMenuPrincipal()
        {            
            string menu = @"***  -  Bienvenue dans le menu principal!  -  ***\n"  +
                        @"*** - 1 - ***" + "\t" + @"Ajouter conservateur\t" + @"***\n" +
                        @"*** - 2 - ***" + "\t" + @"Ajouter artiste\t" + @"***\n" +
                        @"*** - 3 - ***" + "\t" + @"Ajouter oeuvre\t" + @"***\n" +
                        @"*** - 4 - ***" + "\t" + @"Afficher conservateur\t" + @"***\n" +
                        @"*** - 5 - ***" + "\t" + @"Afficher artiste\t" + @"***\n" +
                        @"*** - 6 - ***" + "\t" + @"Afficher oeuvre\t" + @"***\n" +
                        @"*** - 7 - ***" + "\t" + @"Vendre oeuvre\t" + @"***\n" +                        
                        @"*** - 8 - ***" + "\t" + @"Rapport des ventes\t" + @"***\n" + 
                        @"*** - 9 - ***" + "\t" + @"Quitter\t" +@"***\n\n";

            return menu;
        }

        public object AppelerModule(string choix)
        {
            string s = "Cette ligne ne devrait pas s'afficher";
            List<string> questions;

            //Structure switch case pour rediriger l'usager vers le module de son choix
            if (choix == "1")
            {
                questions = ObtenirInfoConservateur();
                return questions;
            }
            else if (choix == "2")
            {
                questions = ObtenirInfoArtiste();
                return questions;
            }
            else if (choix == "3")
            {
                questions = ObtenirInfoOeuvre();
                return questions;
            }
            
            else if (choix == "7")
            {
                questions = ObtenirInfoVente();                
                return questions;                
            }
            else if (choix == "8")
            {                
                return s = "Rapport des ventes";
            }
            else if (choix == "9")
            {
                return s = "Au revoir!";
            }  
            else
            {
                switch (choix)
                {                    
                    case "4":
                        s = "Ligne qui ne devrait pas s'afficher.";
                        try
                        {
                            int i = ListeConservateurs.Count;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }

                        foreach (Conservateur conservateur in ListeConservateurs)
                        {
                            s += conservateur.ToString();
                        }                        
                        break;
                    
                    case "5":                        
                        s = "Ligne qui ne devrait pas s'afficher.";
                        try
                        {           
                            int i = ListeArtistes.Count;        
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e.Message);                
                        }

                        foreach (Artiste artiste in ListeArtistes)
                        {
                            s += artiste.ToString();
                        }            
                        break;
                    
                    case "6":                        
                        s = "Ligne qui ne devrait pas s'afficher.";
                        try
                        {
                            int i = ListeOeuvres.Count;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }

                        foreach (Oeuvre oeuvre in ListeOeuvres)
                        {
                            s += oeuvre.ToString();
                        }                              
                        break;
                }
                return s;        
            }
        }
        

        public string ReponsesObtenues(List<string> reponses, string choix)
        {
            string s = " ";
            string codeValide;
            string sFailCurator = "Le conservateur n'a pu être ajouté au système.\n Veuillez vérifier les informations et réessayer.\n";
            string sNom = "Le nom complet doit compter 40 caractères ou moins, sans caractères spéciaux.\n";
            string sSuccessCurator = "Le conservateur est maintenant au système.\n Un code d'identification unique lui a été attribué automatiquement.\n";
            string sFailArtist = "L'artiste n'a pu être ajouté au système.\n Veuillez vérifier les informations et réessayer.\n";
            string sSuccessArtist = "L'artiste est maintenant au système.\n Un code d'identification unique lui a été attribué automatiquement.\n";
            string sCode = "Le code entré doit commencer par une lettre majuscule (C, A ou O) suivie de quatre chiffres.\n";
            string sNoCode = "Le code entré n'existe pas. Assurez-vous que le conservateur attitré a bel et bien été entré au système.\n";
            string sTitre = "Le titre doit compter 40 caractères ou moins, sans caractères spéciaux.\n";
            string sNum = "Tous les caractères de la valeur entrée doivent être des chiffres.\n";
            string sAnnee = "L'année doit compter quatre caractères numériques.\n";
            string sFailPiece = "L'oeuvre n'a pu être ajoutée au système.\n Veuillez vérifier les informations et réessayer.\n";
            string sSuccessPiece = "L'oeuvre est maintenant au sytème.\n Un code d'identification unique lui a été attribué automatiquement.\n";
            string sFailSale = "L'oeuvre n'a pas pu être vendue.\n Veuillez vérifier les informations et réessayer.\n";
            string sSuccessSale = "L'oeuvre a été vendue. La commission a été attribuée au conservateur associé.\n";
            bool valide;
            
            switch (choix)
            {    
                case "1":
                    string nomCompletC = reponses[0].Trim() + " " + reponses[1].Trim();
                    valide = validations.ValiderNombreCaracteres(nomCompletC, 40);
                    if (!valide)
                    {
                        validations.MessageNonValide();
                        s = sFailCurator + sNom;
                        break;
                    }
                    valide = validations.ValiderTypeCaracteresNom(nomCompletC);
                    if (!valide)
                    {
                        validations.MessageNonValide();
                        s = sFailCurator + sNom;
                        break;
                    }
                    AjouterConservateur(reponses[0].Trim(), reponses[1].Trim());
                    s = sSuccessCurator;
                    break;

                case "2":         
                    string nomCompletA = reponses[0].Trim() + " " + reponses[1].Trim();
                    valide = validations.ValiderNombreCaracteres(nomCompletA, 40);
                    if (!valide)
                    {
                        validations.MessageNonValide();
                        s = sFailArtist + sNom;
                        break;
                    }
                    valide = validations.ValiderTypeCaracteresNom(nomCompletA);
                    if (!valide)
                    {
                        validations.MessageNonValide();
                        s = sFailArtist + sNom;
                        break;
                    }
                    codeValide = reponses[2];
                    valide = validations.ValiderCode(codeValide);
                    if (!valide)
                    {
                        validations.MessageNonValide();
                        s = sFailArtist + sCode;
                        break;
                    }

                    Conservateur conservateurAttitre = GetConservateur(codeValide);

                    if (conservateurAttitre != null)
                    {
                        AjouterArtiste(reponses[0], reponses[1], conservateurAttitre);
                        s = sSuccessArtist;
                        break;
                    }
                    else
                    {
                        s = sFailArtist + sNoCode;
                    }                    
                    break;
                    
                case "3":
                    string titre = reponses[0].Trim();
                    valide = validations.ValiderNombreCaracteres(titre, 40);
                    if (!valide)
                    {
                        validations.MessageNonValide();
                        s = sFailPiece + sTitre;
                        break;
                    }
                    codeValide = reponses[1];
                    valide = validations.ValiderCode(codeValide);
                    if (!valide)
                    {
                        validations.MessageNonValide();
                        s = sFailPiece + sCode;
                        break;
                    }

                    double estimation;
                    bool estimationTemp = double.TryParse(reponses[2], out estimation);
                    if (!(estimationTemp))
                    {
                        validations.MessageNonValide();
                        s = sFailPiece + sNum;
                        break;
                    }
                    estimation = Convert.ToDouble(reponses[2]);
                    string annee = reponses[3].Trim();
                    valide = validations.ValiderAnnee(annee);
                    if (!valide)
                    {
                        validations.MessageNonValide();
                        s = sFailPiece + sAnnee;
                        break;
                    }

                    Artiste artisteCreateur = GetArtiste(codeValide);

                    if (artisteCreateur != null)
                    {
                        AjouterOeuvre(titre, artisteCreateur, estimation, annee);
                        s = sSuccessPiece;
                    }                        
                    break;
                    
                case "7": 
                    codeValide = reponses[0];
                    valide = validations.ValiderCode(codeValide);
                    if (!valide)
                    {
                        validations.MessageNonValide();
                        s = sFailSale + sCode;
                        break;
                    } 
                    
                    double prixVente;
                    bool temp = double.TryParse(reponses[1], out prixVente);
                    if (!temp)
                    {
                        validations.MessageNonValide();
                        s = sFailSale + sNum;
                        break;
                    }
                    prixVente = Convert.ToDouble(reponses[1]);
                    bool vente = VendreOeuvre(codeValide, prixVente);
                    if (!vente)
                    {
                        validations.MessageNonValide();
                        s = sFailSale;
                        break;
                    }                
                    
                    Conservateur conservateurAssocie = GetConservateur(reponses[2]);

                    if (conservateurAssocie != null)
                    {
                        VendreOeuvre(codeValide, prixVente);
                        s = sSuccessSale;
                        break;                        
                    }
                    else 
                    {
                        validations.MessageNonValide();
                        s = sFailSale + sNoCode;
                    }                    
                    break;               
            }
            return s;            
        }
                    
      
        public List<string> ObtenirInfoConservateur()
        {
            List<string> questionsConservateur = new List<string>{"Veuillez entrer le prénom du conservateur :  ", "Veuillez entrer le nom de famille du conservateur : "};
            return questionsConservateur;
        }

        public List<string> ObtenirInfoArtiste()
        {
            List<string> questionsArtiste = new List<string>{"Veuillez entrer le prénom de l'artiste :  ",
                        "Veuillez entrer le nom de famille de l'artiste : ",
                        "Veuillez entrer le code d'identification du conservateur attitré à l'artiste :  "};
            return questionsArtiste;
        }

        public List<string> ObtenirInfoOeuvre()
        {
            List<string> questionsOeuvre = new List<string>{"Veuillez entrer le titre de l'oeuvre :  ",
                "Veuillez entrer le code d'identification de l'artiste qui a créé l'oeuvre : ",
                "Veuillez entrer la valeur estimée de l'oeuvre à l'acquisition :  ",
                "Veuillez entrer l'année de l'acquisition de l'oeuvre :  "};
            return questionsOeuvre;
        }

        public List<string> ObtenirInfoVente()
        {
            List<string> questionsVente = new List<string>
                    {
                        "Veuillez entrer le code d'identification de l'oeuvre vendue :  ",
                        "Veuillez entrer le prix de vente de l'oeuvre : "                        
                    };
            return questionsVente;
        }
    }
}