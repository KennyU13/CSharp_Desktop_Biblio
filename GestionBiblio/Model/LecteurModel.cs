using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionBiblio.Model
{
    class LecteurModel
    {
        public string numLecteur { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }

        public LecteurModel(string numLecteur, string nom, string prenom)
        {
            this.numLecteur = numLecteur;
            this.nom = nom;
            this.prenom = prenom;
        }
    }

}
