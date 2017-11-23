using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SGI
{
    public class Artiste : Personne
    {
        //définition des variables
        private string idArtiste;
        private static int nextArtistID;
        private Conservateur conservateurAttitre;

        //private string idConservateur;

        //accesseurs

        public string IDArtiste
        {
            get
            {
                return idArtiste;
            }                      
        }

        //accesseur pour conservateur attitré
        public Conservateur ConservateurAttitre
        {
            get
            {
                return conservateurAttitre;
            }
        }

        public int WantedID
        {
            get;
            private set;
        }

        
        //public Conservateur GetConservateurAttitre
        //{
        //    get
        //    {
        //        return conservateurAttitre;
        //    }            
        //}                     
        
        //constructeur par défaut pour le choix du module à retourner à partir du menu
        public Artiste()
        {            
                                   
        }
       
        //constructeur avec paramètres
        public Artiste(string Prenom, string NomFamille, Conservateur ConservateurAttitre)
            :base(Prenom, NomFamille)
        {            
            this.WantedID = Interlocked.Increment(ref nextArtistID);
            this.idArtiste = "A" + (WantedID + 1000);
            this.conservateurAttitre = ConservateurAttitre;                       
        }       
        

        //méthode ToString qui doit retourner une seule chaîne de caractères contenant le prénom, le nom et le id de l'artiste 
        //polymorphisme
        public override string ToString()
        {
            if (idArtiste == null)
            {
                throw new ApplicationException("IDArtiste est null");
            }
            return prenom + " " + nomFamille + ", " + idArtiste + "\n\n";
        }

        //méthode SetComm (void double) qui devra être validée et qui permettra d'attribuer la commission
        //recevra le résultat double de la méthode oeuvre.CalculerCommission(PrixPaye)
        public void SetComm(double montant)
        {
            //validation
            
            this.conservateurAttitre.Commissions = this.conservateurAttitre.Commissions + (Conservateur.tauxDeCommission * montant);

        }
    }
}
