using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SGI
{
    public class Oeuvre
    {
        //définition des variables
        private string idOeuvre;
        private static int nextPieceID;
        private string titre;        
        private string annee;
        private double prixOeuvreVendue = 0;

        
        private double estimation;      
        private Artiste artisteCreateur;
        private char etat = 'E';

        //private string idConservateur;

        //accesseurs

        public string IDOeuvre
        {
            get
            {
                return idOeuvre;
            }
        }

        public Artiste ArtisteCreateur
        {
            get
            {
                return artisteCreateur;
            }
        }

        public char Etat
        {
            get
            {
                return etat;
            }
            set
            {
                this.etat = value;
            }
        }

        public int WantedID
        {
            get;
            private set;
        }

        //accesseur pour artiste créateur
        public Artiste GetArtisteCreateur
        {
            get
            {
                return artisteCreateur;
            }
        }

        public string Titre { get => titre; set => titre = value; }
        public string Annee { get => annee; set => annee = value; }
        public double PrixDeVente1 { get => PrixOeuvreVendue; set => PrixOeuvreVendue = value; }
        public double Estimation { get => estimation; set => estimation = value; }
        public double PrixOeuvreVendue { get => prixOeuvreVendue; set => prixOeuvreVendue = value; }

        //constructeur par défaut pour retourner le bon module
        public Oeuvre()
        {
            
        }


        //constructeur avec paramètres
        public Oeuvre(string Titre, Artiste ArtisteCreateur, double Estimation, string Annee)            
        {
            this.WantedID = Interlocked.Increment(ref nextPieceID);
            this.idOeuvre = "O" + (WantedID + 1000);
            this.Titre = Titre;
            this.artisteCreateur = ArtisteCreateur;
            this.Estimation = Estimation;
            this.PrixDeVente1 = 0;
            this.Annee = Annee;
            this.etat = 'E';
        }


        //méthode ToString qui doit retourner une seule chaîne de caractères contenant le prénom, le nom et le id de l'artiste 
        //polymorphisme
        public override string ToString()
        {
            if (idOeuvre == null)
            {
                throw new ApplicationException("IDOeuvre est null");
            }
            return idOeuvre + ", " + Titre + "\n" 
                + artisteCreateur.IDArtiste + " (" + artisteCreateur.Prenom + " " + artisteCreateur.NomFamille +")" + "\n"
                + "Valeur estimée : " + Estimation + " " + "Prix de vente : " + PrixDeVente1 + "; " + "\n"
                + " Année d'acquisition : " + Annee + "; " + "État : " + etat + "\n\n";
        }

        // méthode ChangerEtat(char) void
        public void ChangerEtat()
        {
            this.etat = 'V';
        }

        // méthode PrixDeVente (double) void
        public void PrixDeVente(double PrixPaye)
        {
            this.PrixDeVente1 = PrixPaye;
        }

        // méthode CalculerCommission (double) double
        public double CalculerCommission(double PrixPaye)
        {
            double commission = (PrixPaye - this.Estimation) * 0.25;
            return commission;
        }
    }
}
