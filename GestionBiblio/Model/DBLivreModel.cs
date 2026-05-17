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
    class DBLivreModel
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
        public static void getAllLivre(DataGrid x)
        {
            string sql;

            sql = "SELECT * FROM livre";
            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            adp.Fill(dt);

            x.ItemsSource = dt.DefaultView;
            con.Close();
        }
        public static bool checkDispo(string num)
        {
            string sql;
            string disponible = "";
            bool check = false;

            sql = "SELECT disponible  FROM livre WHERE numLivre = " + num;
            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            DataTable dt = new DataTable();
            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    disponible = reader.GetString("disponible");
                }
            }
            if (disponible == "oui") check = true;

            con.Close();
            return check;
        }
        public static void setUnDispo(string id)
        {
            string sql = "UPDATE livre SET disponible = 'non' WHERE numLivre = "+ id;

            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, con);
          
                cmd.ExecuteNonQuery();
               
           
            con.Close();
        }
        public static void setDispo (string id)
        {
            string sql = "UPDATE livre SET disponible = 'oui' WHERE numLivre = " + id;

            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            
                cmd.ExecuteNonQuery();
             
         
            con.Close();
        }
        public static void searchLivre(DataGrid x, string name)
        {
            string sql = "SELECT * FROM livre WHERE design LIKE '%"+name+"%' OR auteur LIKE '%"+name+ "%' OR disponible LIKE '%" + name + "%'";
            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            adp.Fill(dt);

            x.ItemsSource = dt.DefaultView;
            con.Close();
        }

        //Ajouter un livre Modele
        public static void addLivre(LivreModel lm)
        {
            string sql = "INSERT INTO livre VALUES (@LivreNumLivre,@LivreDesign,@LivreAuteur,@LivreDateEdition,@LivreDispo)";

            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, con);

            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@LivreNumLivre", MySqlDbType.VarChar).Value = lm.numLivre;
            cmd.Parameters.Add("@LivreDesign", MySqlDbType.VarChar).Value = lm.design;
            cmd.Parameters.Add("@LivreAuteur", MySqlDbType.VarChar).Value = lm.auteur;
            cmd.Parameters.Add("@LivreDateEdition", MySqlDbType.VarChar).Value = lm.dateEdition;
            cmd.Parameters.Add("@LivreDispo", MySqlDbType.VarChar).Value = lm.disponible;
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
        public static void updateLivre(LivreModel lm)
        {
            string sql = "UPDATE livre SET design = @LivreDesign , auteur = @LivreAuteur , dateEdition = @LivreDateEdition , disponible = @LivreDispo WHERE numLivre = @LivreNumLivre";

            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@LivreNumLivre", MySqlDbType.VarChar).Value = lm.numLivre;
            cmd.Parameters.Add("@LivreDesign", MySqlDbType.VarChar).Value = lm.design;
            cmd.Parameters.Add("@LivreAuteur", MySqlDbType.VarChar).Value = lm.auteur;
            cmd.Parameters.Add("@LivreDateEdition", MySqlDbType.VarChar).Value = lm.dateEdition;
            cmd.Parameters.Add("@LivreDispo", MySqlDbType.VarChar).Value = lm.disponible;
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

        //Suppression d'une Livre
        public static void deleteLivre (string id)
        {
            string sql = " DELETE FROM livre WHERE numLivre = @LivreNumLivre";
            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@LivreNumLivre", MySqlDbType.VarChar).Value = id;
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Suppression réussi", "Information", MessageBoxButton.OK);
            }
            catch (MySqlException e)
            {
                MessageBox.Show("Echec du suppression : " + e.Message, "Error", MessageBoxButton.OK);
            }
            con.Close();

        }

    }
}
