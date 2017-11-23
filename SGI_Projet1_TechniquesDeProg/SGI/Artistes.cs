using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace SGI
{
    public class Artistes : CollectionBase //classe collection
    {
        //tentative d'indexeur
        public Artiste this[int index]
        {
            get 
            {
                return (Artiste)List[index];
            }
            set 
            {
                List[index] = value;
            }
        }
                
        //tentative de méthode ajouter
        public void Ajouter(Artiste artiste)
        {
            List.Add(artiste);
        }

       
    }
}
