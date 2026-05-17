using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GestionBiblio.Model
{
    class DBLecteurModel
    {
        public static MySqlConnection GetConnection()
        {
            string sql = "datasource = localhost;port=3306;username=root;password=;database=bibliotheque";

            MySqlConnection con = new MySqlConnection(sql);
            try
            {
                con.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Erreur de connection : " + ex.Message);
            }
            return con;
        }
        public static void getAllLecteur(DataGrid x)
        {
            string sql;

            sql = "SELECT * FROM Lecteur";
            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            adp.Fill(dt);

            x.ItemsSource = dt.DefaultView;
            con.Close();
        }
        public static void searchLecteur(DataGrid x, string name)
        {
            string sql = "SELECT * FROM lecteur WHERE nom LIKE '%"+name+"%' OR prenom LIKE '%"+name+"%'";
            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            adp.Fill(dt);

            x.ItemsSource = dt.DefaultView;
            con.Close();
        }

        //Ajouter un Lecteur Modele
        public static void addLecteur(LecteurModel lm)
        {
            string sql = "INSERT INTO Lecteur VALUES (@LecteurNumLecteur, @LecteurNom, @LecteurPrenom)";

            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, con);

            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@LecteurNumLecteur", MySqlDbType.VarChar).Value = lm.numLecteur;
            cmd.Parameters.Add("@LecteurNom", MySqlDbType.VarChar).Value = lm.nom;
            cmd.Parameters.Add("@LecteurPrenom", MySqlDbType.VarChar).Value = lm.prenom;
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

        //Mise à Jour Affichage Lecteur
        public static void updateLecteur(LecteurModel lm)
        {
            string sql = "UPDATE Lecteur SET nom = @LecteurNom , prenom = @LecteurPrenom WHERE numLecteur = @LecteurNumLecteur";

            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@LecteurNumLecteur", MySqlDbType.VarChar).Value = lm.numLecteur;
            cmd.Parameters.Add("@LecteurNom", MySqlDbType.VarChar).Value = lm.nom;
            cmd.Parameters.Add("@LecteurPrenom", MySqlDbType.VarChar).Value = lm.prenom;
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Modification réussi", "Information", MessageBoxButton.OK);
            }
            catch (MySqlException e)
            {
                MessageBox.Show("Echec du modification : " + e.Message, "Erreur", MessageBoxButton.OK);
            }
            con.Close();
        }

        //Suppression d'une Lecteur
        public static void deleteLecteur(string id)
        {
            string sql = " DELETE FROM Lecteur WHERE numLecteur = @LecteurNumLecteur";
            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@LecteurNumLecteur", MySqlDbType.VarChar).Value = id;
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
    }
}
