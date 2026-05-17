using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace GestionBiblio.Model
{
    class LivreModel
    {
        public string numLivre { get; set; }
        public string design { get; set; }
        public string auteur { get; set; }
        public string dateEdition { get; set; }
        public string disponible { get; set; }

        public LivreModel(string numLivre, string design, string auteur, string dateEdition, string disponible)
        {
            this.numLivre = numLivre;
            this.design = design;
            this.auteur = auteur;
            this.dateEdition = dateEdition;
            this.disponible = disponible;

        }
    }
}
