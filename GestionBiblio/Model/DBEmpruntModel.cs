using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;

namespace GestionBiblio.Model
{
    class DBEmpruntModel
    {   //Connection à la base de donnée Mysql
        public static MySqlConnection GetConnection()
        {
            string sql = "datasource = localhost;port=3306;username=root;password=;database=bibliotheque";

            MySqlConnection con = new MySqlConnection(sql);
            try
            {
                con.Open();
            }
            catch(MySqlException ex)
            {
                MessageBox.Show("Erreur de connection : " + ex.Message);
            }
            return con;
        }
        public static void getAllEmprunt(DataGrid x)
        {
            string sql;
            sql = "SELECT emprunt.numPret,emprunt.numLecteur,emprunt.numLivre, lecteur.nom, livre.design, livre.auteur, emprunt.datePret, emprunt.dateRetour FROM (( emprunt INNER JOIN lecteur ON emprunt.numLecteur = lecteur.numLecteur ) INNER JOIN livre ON emprunt.numLivre = livre.numLivre)";
            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            adp.Fill(dt);

            x.ItemsSource = dt.DefaultView;
            con.Close();
        }
        public static void searchEmprunt(DataGrid x, string name)
        {
            string sql = "SELECT emprunt.numPret,emprunt.numLecteur,emprunt.numLivre , lecteur.nom, livre.design, livre.auteur, emprunt.datePret, emprunt.dateRetour FROM (( emprunt INNER JOIN lecteur ON emprunt.numLecteur = lecteur.numLecteur ) INNER JOIN livre ON emprunt.numLivre = livre.numLivre ) WHERE lecteur.nom LIKE '%" + name+ "%' OR livre.design LIKE '%" + name + "%' OR livre.auteur LIKE '%" + name + "%'";
            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            adp.Fill(dt);

            x.ItemsSource = dt.DefaultView;
            con.Close();
        }

        
        public static void addEmprunt(EmpruntModel lm)
        {
            string sql = "INSERT INTO Emprunt VALUES (@numPret,@numLecteur,@numLivre,@datePret,@dateRetour)";

            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, con);

            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@numPret", MySqlDbType.VarChar).Value = lm.numPret;
            cmd.Parameters.Add("@numLecteur", MySqlDbType.VarChar).Value = lm.numLecteur;
            cmd.Parameters.Add("@numLivre", MySqlDbType.VarChar).Value = lm.numLivre;
            cmd.Parameters.Add("@datePret", MySqlDbType.VarChar).Value = lm.DatePret;
            cmd.Parameters.Add("@dateRetour", MySqlDbType.VarChar).Value = lm.DateRetour;
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Enregistrement réussi", "Information",MessageBoxButton.OK);
            }
            catch(MySqlException e)
            {
                MessageBox.Show("Echec de l'enregistrement : " + e.Message, "Erreur", MessageBoxButton.OK);
            }
            con.Close();

        }

        //Mise à Jour Affichage Livre
        public static void updateEmprunt(EmpruntModel lm)
        {
            string sql = "UPDATE emprunt SET numLecteur = @numLecteur , numLivre = @numLivre , dateRetour = @dateRetour  WHERE numPret= @numPret  ";

            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@numPret", MySqlDbType.VarChar).Value = lm.numPret;
            cmd.Parameters.Add("@numLecteur", MySqlDbType.VarChar).Value = lm.numLecteur;
            cmd.Parameters.Add("@numLivre", MySqlDbType.VarChar).Value = lm.numLivre;
            cmd.Parameters.Add("@dateRetour", MySqlDbType.VarChar).Value = lm.DateRetour;
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Enregistrement réussi", "Information", MessageBoxButton.OK);
            }
            catch (MySqlException e)
            {
                MessageBox.Show("Echec de l'enregistrement : " + e.Message, "Erreur", MessageBoxButton.OK);
            }
            con.Close();
        }
 
        public static void deleteEmprunt (string id)
        {
            string sql = " DELETE FROM Emprunt WHERE numPret = @numPret";
            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@numPret", MySqlDbType.VarChar).Value = id;
            
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Suppression réussi", "Information", MessageBoxButton.OK);
            }
            catch (MySqlException e)
            {
                MessageBox.Show("Echec du suppression : " + e.Message, "Erreur", MessageBoxButton.OK);
            }
            con.Close();

        }
        public static void deleteByLecteur(string numero)
        {
            string sql = " DELETE FROM Emprunt WHERE numLecteur = @numLecteur";
            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@numLecteur", MySqlDbType.VarChar).Value = numero;
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public static void deleteByLivre(string numero)
        {
            string sql = " DELETE FROM Emprunt WHERE numLivre = @numLivre";
            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@numLivre", MySqlDbType.VarChar).Value = numero;
            cmd.ExecuteNonQuery();
            con.Close();
        }

    }
}
