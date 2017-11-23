using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGI
{
    public abstract class Personne
    {
        //définition des variables de la classe Personne
        protected string prenom;
        protected string nomFamille;

        //accesseur pour prenom
        public string Prenom
        {
            get
            {
                return prenom;
            }
            set
            {
                prenom = value;
            }
        }

        //accesseur pour nomFamille
        public string NomFamille
        {
            get
            {
                return nomFamille;
            }
            set
            {
                nomFamille = value;
            }
        }
        //constructeur par défaut
        public Personne()
        {

        }

        //constructeur avec paramètres
        public Personne(string Prenom, string NomFamille)
        {
            this.prenom = Prenom.Trim();
            this.nomFamille = NomFamille.Trim();
        }
        //méthode ToString qui doit retourner une seule chaîne de caractères contenant le prénom et le nom
        public override string ToString()
        {
            if ((this.prenom == null) || (this.nomFamille == null))
            {
                throw new ApplicationException("Prenom ou NomFamille est null");
            }
            return prenom + " " + nomFamille;
        }
    }
}
