using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Data;
using MySql.Data.MySqlClient;
using GestionBiblio.Model;

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
     

        public LoginView()
        {
            InitializeComponent();
            DBLivreModel.getAllLivre(dataGridLivre);
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            Panel.SetZIndex(Livre, 1);
            Panel.SetZIndex(Lecteur, 0);
            Panel.SetZIndex(Preter, 0);
        }
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);


        // WINDOWS ACTION BUTTONS

        private void pnlControlBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            SendMessage(helper.Handle, 161, 2, 0);
            DragMove();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        // NAVIGATION BUTTONS

        private void btnLivre_Click(object sender, RoutedEventArgs e)
        {
            
            Panel.SetZIndex(Livre, 1);
            Panel.SetZIndex(Lecteur, 0);
            Panel.SetZIndex(Preter, 0);
            DBLivreModel.getAllLivre(dataGridLivre);
        }

        private void btnPreter_Click(object sender, RoutedEventArgs e)
        {
            Panel.SetZIndex(Livre, 0);
            Panel.SetZIndex(Lecteur, 0);
            Panel.SetZIndex(Preter, 1);
            DBEmpruntModel.getAllEmprunt(dataGridPreter);
        }

        private void btnLecteur_Click(object sender, RoutedEventArgs e)
        {
            
            Panel.SetZIndex(Livre, 0);
            Panel.SetZIndex(Lecteur, 1);
            Panel.SetZIndex(Preter, 0);
            DBLecteurModel.getAllLecteur(dataGridLecteur);
            /**/


        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Êtes-vous sûr de vouloir fermer l'application" , "Inforamtion" , MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }


        // ADD BUTTONS

        private void btnAjoutLivre_Click(object sender, RoutedEventArgs e)
        {

            string disponible = "oui";

            if (dispoCheck.IsChecked.Value)
            {
                disponible = "oui";
            }
            else
            {
                disponible = "non";
            }

            string numero = numLivre.Text.Trim();
            string design = designLivre.Text.Trim();
            string auteur = auteurLivre.Text.Trim();
            string date = dateLivre.Text;

            LivreModel livre = new LivreModel(numero, design, auteur, date, disponible);
            if (checkLivreLabel())
            {
                if (btnAjoutLivre.Content.Equals("Ajouter"))
                {

                    DBLivreModel.addLivre(livre);

                }
                if (btnAjoutLivre.Content.Equals("Modifier"))
                {

                    DBLivreModel.updateLivre(livre);
                    btnAjoutLivre.Content = "Ajouter";
                }
            }
            else MessageBox.Show("Veuiller completer tous les champs !!", "Erreur", MessageBoxButton.OK);

            DBLivreModel.getAllLivre(dataGridLivre);
            clear();
        }

        private void btnAjoutLecteur_Click_1(object sender, RoutedEventArgs e)
        {
            string numero = numLecteur.Text.Trim();
            string nom = nomLecteur.Text.Trim();
            string prenom = prenomLecteur.Text.Trim();

            /*btnAjoutLecteur*/
            LecteurModel lecteur = new LecteurModel(numero, nom, prenom);
            if(checkLecteurLabel())
            {
                if (btnAjoutLecteur.Content.Equals("Ajouter"))
                {
                    DBLecteurModel.addLecteur(lecteur);
                }
                if (btnAjoutLecteur.Content.Equals("Modifier"))
                {
                    DBLecteurModel.updateLecteur(lecteur);
                    btnAjoutLecteur.Content = "Ajouter";
                }
            }
            else MessageBox.Show("Veuiller completer tous les champs !!", "Erreur", MessageBoxButton.OK);

            DBLecteurModel.getAllLecteur(dataGridLecteur);
            clear();
        }

        private void btnAjoutPret_Click(object sender, RoutedEventArgs e)
        {
            string pret = numPret.Text.Trim();
            string lecteur = numLecteurPret.Text.Trim();
            string livre = numLivrePret.Text.Trim();
            string datePret = DateTime.Now.ToString();
            string dateRetour = dateRetourPret.Text.Trim();

            EmpruntModel emprunt = new EmpruntModel(pret, lecteur, livre, datePret, dateRetour);

            if (checkEmpruntLabel())
            {
                if (DBLivreModel.checkDispo(livre))
                {
                    if (btnAjoutPret.Content.Equals("Ajouter"))
                    {
                        DBLivreModel.setUnDispo(livre);
                        DBEmpruntModel.addEmprunt(emprunt);
                    }
                    if (btnAjoutPret.Content.Equals("Modifier"))
                    {
                        DBLivreModel.setUnDispo(livre);
                        DBEmpruntModel.updateEmprunt(emprunt);
                        btnAjoutPret.Content = "Ajouter";
                    }
                }
                else
                {
                    DBLivreModel.setDispo(livre);
                    MessageBox.Show("Ce livre n'est pas encore disponible pour le moments", "Information", MessageBoxButton.OK);
                }
                
            }
            else MessageBox.Show("Veuiller completer tous les champs !!", "Erreur", MessageBoxButton.OK);
            DBEmpruntModel.getAllEmprunt(dataGridPreter);
            clear();
        }


        // DELETE BUTTONS

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selRow = dataGridLivre.SelectedItem as DataRowView;
            if (dataGridLivre.SelectedItem != null)
            {
                if (MessageBox.Show("Êtes-vous sûr de vouloir supprimer cette ligne ?", "Confiramtion de Suppression", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    DBEmpruntModel.deleteByLivre(selRow[0].ToString());
                    DBLivreModel.deleteLivre(selRow[0].ToString());
                }
            }
            DBLivreModel.getAllLivre(dataGridLivre);
        }

        private void btnDeleteLecteur_Click(object sender, RoutedEventArgs e)
        {
            var selRow = dataGridLecteur.SelectedItem as DataRowView;
            if (dataGridLecteur.SelectedItem != null)
            {
                if (MessageBox.Show("Êtes-vous sûr de vouloir supprimer cette ligne ?", "Confiramtion de Suppression", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    DBEmpruntModel.deleteByLecteur(selRow[0].ToString());
                    DBLecteurModel.deleteLecteur(selRow[0].ToString());
                }
            }
            DBLecteurModel.getAllLecteur(dataGridLecteur);
        }

        private void btnDeletePreter_Click(object sender, RoutedEventArgs e)
        {
            var selRow = dataGridPreter.SelectedItem as DataRowView;
            if (dataGridPreter.SelectedItem != null)
            {
                if(MessageBox.Show("Êtes-vous sûr de vouloir supprimer cette ligne ?", "Confiramtion de Suppression", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    DBLivreModel.setDispo(selRow[2].ToString());
                    DBEmpruntModel.deleteEmprunt(selRow[0].ToString());
                }                
            }
            DBEmpruntModel.getAllEmprunt(dataGridPreter);
        }


        //EDIT BUTTONS

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
           
            var selRow = dataGridLivre.SelectedItem as DataRowView;
            if(dataGridLivre.SelectedItem  != null)
            {
                /*numLivre.Text = selRow["Numero"].ToString();*/
                /*Numero*/

                numLivre.Text = selRow[0].ToString(); ;
                designLivre.Text = selRow[1].ToString();
                auteurLivre.Text = selRow[2].ToString();
                dateLivre.Text = selRow[3].ToString();
                if (selRow[4].ToString() == "oui") dispoCheck.IsChecked = true;
                else dispoCheck.IsChecked = false ;
                btnAjoutLivre.Content = "Modifier";
            }
        }

        private void btnEditLecteur_Click(object sender, RoutedEventArgs e)
        {
            var selRow = dataGridLecteur.SelectedItem as DataRowView;
            if (dataGridLecteur.SelectedItem != null)
            {
                numLecteur.Text = selRow[0].ToString(); ;
                nomLecteur.Text = selRow[1].ToString();
                prenomLecteur.Text = selRow[2].ToString();
                btnAjoutLecteur.Content = "Modifier";
            }
        }

        private void btnEditPreter_Click(object sender, RoutedEventArgs e)
        {
            var selRow = dataGridPreter.SelectedItem as DataRowView;
            if (dataGridPreter.SelectedItem != null)
            {
                numPret.Text = selRow[0].ToString(); ;
                numLecteurPret.Text = selRow[1].ToString();
                numLivrePret.Text = selRow[2].ToString();
                dateRetourPret.Text = selRow[7].ToString();
                DBLivreModel.setDispo(selRow[2].ToString());
                btnAjoutPret.Content = "Modifier";
            }
        }


        // REFRESH && RECHERCHER BUTTONS

        private void btnSearchLivre_Click(object sender, RoutedEventArgs e)
        {   
            DBLivreModel.searchLivre(dataGridLivre, labelRecherche.Text.Trim());
        }

        private void btnRefreshLivre_Click(object sender, RoutedEventArgs e)
        {
            DBLivreModel.getAllLivre(dataGridLivre);
            clear();
        }

        private void btnSearchLecteur_Click(object sender, RoutedEventArgs e)
        {
            DBLecteurModel.searchLecteur(dataGridLecteur, labelRechercheLecteur.Text.Trim());
        }

        private void btnRefreshLecteur_Click(object sender, RoutedEventArgs e)
        {
            DBLecteurModel.getAllLecteur(dataGridLecteur);
            clear();
        }

        private void btnSearchPret_Click(object sender, RoutedEventArgs e)
        {
            DBEmpruntModel.searchEmprunt(dataGridPreter, recherchePret.Text.Trim());
        }

        private void btnRefreshPret_Click(object sender, RoutedEventArgs e)
        {
            DBEmpruntModel.getAllEmprunt(dataGridPreter);
            clear();
        }


        private bool checkLivreLabel()
        {
            bool verify = true;
            if (numLivre.Text == string.Empty) verify = false;
            if (designLivre.Text == string.Empty) verify = false;
            if (auteurLivre.Text == string.Empty) verify = false;
            if (dateLivre.Text == string.Empty) verify = false;
            
            return verify;
        }
        private bool checkLecteurLabel()
        {
            bool verify = true;
            if (nomLecteur.Text == string.Empty) verify = false;
            if (prenomLecteur.Text == string.Empty) verify = false;
            if (numLecteur.Text == string.Empty) verify = false;

            return verify;
        }
        private bool checkEmpruntLabel()
        {
            bool verify = true;
            if (numPret.Text == string.Empty) verify = false;
            if (numLecteurPret.Text == string.Empty) verify = false;
            if (numLivrePret.Text == string.Empty) verify = false;
            if (dateRetourPret.Text == string.Empty) verify = false;

            return verify;
        }
        private void clear()
        {
            numLivre.Text = designLivre.Text = auteurLivre.Text = dateLivre.Text = string.Empty;
            nomLecteur.Text = prenomLecteur.Text = numLecteur.Text = string.Empty;
            numPret.Text = numLecteurPret.Text = numLivrePret.Text = dateRetourPret.Text = string.Empty;
            labelRecherche.Text = recherchePret.Text = labelRechercheLecteur.Text = string.Empty;
        }

       
    }
}
