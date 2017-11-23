using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGI
{
    public class Validations
    {
        public void MessageNonValide()
        {
            Console.WriteLine("Votre entrée n’est pas valide, veuillez réessayer");
            return;
        }

        //validation du choix du menu (1 à 9)
        public bool ValiderChoix(string c)
        {
            bool codeValide = true;
            if (!(Char.IsDigit(c[0]) && (c.Length == 1) && (!string.IsNullOrEmpty(c))))
            {
                MessageNonValide();
                codeValide = false;
            }                       
            return codeValide;                             
        }
            
                

        
        //Pour déterminer existence ou inexistence du code saisi par l’usager
        public bool ExistenceCode(string i, string[] target)
        {
            int Cnt = 0;
            bool trouve = false;
            while ((Cnt < target.Length) && (trouve == false))
            {
                if (i == target[Cnt])
                {
                    trouve = true;
                    break;
                }
                Cnt = Cnt + 1;
            }
            return trouve;
        }
        
        //pour trouver l'index cohérent
        public int IndexCoherence(string i, string[] target)
        {
            int cnt = 0;
            int index = -1;
            bool trouve = false;
            while ((cnt < target.Length) && (trouve == false))
            {
                if (i == target[cnt])
                {
                    trouve = true;
                    index = cnt;
                    break;
                }
                cnt = cnt + 1;
            }
            return index;
        }

        public bool ValiderCode(string i)
        {
            string code = i.Trim(); //pour ne pas tenir compte des espaces qui auraient pu s'introduire involotairement avant ou après la saisie
            bool valide = true;

            //validations : nombre de caractères, valeur non nulle, doit commencer par A, C ou O, les 4 derniers caractères sont des chiffres
            //REMARQUE : au lieu de vérifier chacun des caractères, un substring aurait pu être plus efficace...
            if (!((code.Length == 5) && !string.IsNullOrEmpty(code) && ((code[0] == 'A') | (code[0] == 'C') | (code[0] == 'O'))) && Char.IsDigit(code[1]) && Char.IsDigit(code[2]) && Char.IsDigit(code[3]) && Char.IsDigit(code[4]))
            {
                valide = false;
            }
            return valide;
        }

        //Pour valider que l’année est en chiffres
        public bool ValiderAnnee(string annee)
        {
            bool valide = true;
            annee = annee.Trim();   //pour ne pas tenir compte des espaces involontaires

            //si la valeur entrée n'est pas nulle, vérification des caractères (entrée non valide si un caractère n'est pas un chiffre)
            if (!string.IsNullOrEmpty(annee))
            {
                for (int i = 0; i < annee.Length; i++)
                {
                    if (!(Char.IsDigit(annee[i])))
                    {
                        valide = false;
                        break;
                    }
                }
            }
            else
            {
                valide = false;
            }
            return valide;
        }

        //Pour valider le nombre de caractères i d'un string
        public bool ValiderNombreCaracteres(string titre, int i)
        {
            bool valide = true;
            if (!((titre.Trim().Length <= i) && !string.IsNullOrEmpty(titre)))
            {
                valide = false;
            }
            return valide;
        }

        //Pour valider le type de caractères du titre de l’œuvre
        public bool ValiderTypeCaracteresTitre(string titre)
        {
            bool valide = true;
            titre = titre.Trim();
            for (int i = 0; i < titre.Length; i++)
            {
                if (!(Char.IsNumber(titre[i]) || Char.IsLetter(titre[i]) || Char.IsWhiteSpace(titre[i]) || Char.IsPunctuation(titre[i])))
                {
                    valide = false;
                    break;
                }
            }
            return valide;
        }

        //Pour la validation du type de caractères du nom
        public bool ValiderTypeCaracteresNom(string nom)
        {
            bool valide = true;
            nom = nom.Trim();
            for (int i = 0; i < nom.Length; i++)
            {
                if (!(Char.IsLetter(nom[i]) || Char.IsWhiteSpace(nom[i]) || (nom[i] == ('\'' | '-'))))
                {
                    valide = false;
                    break;
                }
            }
            return valide;
        }

        //Pour valider valeur monétaire
        public bool ValiderMontant(string montant)
        {
            bool valide = true;
            if (!string.IsNullOrEmpty(montant))
            {
                for (int i = 0; i < montant.Length; i++)
                {
                    
                //dépendamment de la façon d'écrire les valeurs monétaires, il est possible que l'usager utilise aussi les caractères suivants
                    if (!(Char.IsDigit(montant[i]) || Char.IsWhiteSpace(montant[i]) || (montant[i] == (',' | '.'))))
                    {
                        valide = false;
                        break;
                    }
                }
            }
            return valide;
        }
        

        //Module non prêt fake pour ajouter la routine de gestion d'exceptions
        public void ModuleNonPret()
        {
            throw new NotImplementedException("Module non prêt");
        }

        

        
        // Pour vérifier si un tableau contient des valeurs
        public bool VerifierSiVide(string[] tableau)
        {
            bool tableauNonVide = true;
            string code;
            for (int i = 0; i < 10; i++)
            {
                code = tableau[i];
                if (!string.IsNullOrEmpty(code))
                {
                    tableauNonVide = true;
                    break;
                }
                tableauNonVide = false;
            }
            return tableauNonVide;

        }

        
        
    }
}
       

     
    