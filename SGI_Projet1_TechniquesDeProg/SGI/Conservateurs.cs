using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace SGI
{
    public class Conservateurs : CollectionBase
    {
        //tentative d'indexeur - il va lui falloir des validations/contraintes
        //serait fun de l'avoir avec le string de l'IDConservateur...
        public Conservateur this[int index]
        {
            get
            {
                return (Conservateur)List[index];
            }
            //set
            //{
            //    List[index] = value;
            //}
        }



        //tentative de méthode ajouter
        public void Ajouter(Conservateur conservateur)
        {
            List.Add(conservateur);
        }
    }
}
