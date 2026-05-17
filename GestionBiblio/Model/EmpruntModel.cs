using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace GestionBiblio.Model
{
    class EmpruntModel
    {
        public string numPret { get; set; }
        public string numLecteur { get; set; }
        public string numLivre { get; set; }
        public string DatePret { get; set; }
        public string DateRetour { get; set; }

        public EmpruntModel(string numPret, string numLecteur, string numLivre, string datePret, string dateRetour)
        {
            this.numPret = numPret;
            this.numLecteur = numLecteur;
            this.numLivre = numLivre;
            this.DatePret = datePret;
            this.DateRetour = dateRetour;
        }
    }
}
