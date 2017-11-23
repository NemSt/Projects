using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SGI
{
    public class Conservateur : Personne
    {
        //définition des variables
        private string idConservateur;
        private static int nextCuratorID;
        private double commissions;
        internal const double tauxDeCommission = 0.25;
        private string commissionsTotales;
        

        //accesseurs
        public string IDConservateur
        {
            get
            {
                return idConservateur;
            }
        }

        public double Commissions
        {
            get
            {
                return commissions;
            }
            set
            {
                commissions = value;
            }
        }
        public int WantedID
        {
            get;
            private set;
        }

        
        //constructeur par défaut pour définition du type d'objet du switch case
        public Conservateur()
        {

        }           

        

        //constructeur avec paramètres
        public Conservateur(string Prenom, string NomFamille)
            :base(Prenom, NomFamille)
        {
            this.WantedID = Interlocked.Increment(ref nextCuratorID);
            this.idConservateur = "C" + (WantedID + 1000);
            this.commissions = 0;
            this.commissionsTotales = commissions.ToString();
        }




        //méthode ToString qui doit retourner une seule chaîne de caractères contenant le prénom, le nom et le id de l'artiste 
        //polymorphisme
        public override string ToString()
        {
            if (idConservateur == null)
            {
                throw new ApplicationException("IDConservateur est null");
            }
            return prenom + " " + nomFamille  + "(" + idConservateur + ")" + ", " + commissionsTotales + "\n\n";
        }
        
        //méthode GetID() pour l'accès en lecture à idConservateur
        public string GetID()
        {            
            return idConservateur;
        }
        
    }
}
