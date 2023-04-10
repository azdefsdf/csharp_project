using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Mediat
{

    public partial class Form1 : MetroFramework.Forms.MetroForm
    {

        // Chemin d'accès à la base de données Access
        public static string dbPath = @"C:\Users\DELL\Documents\mediat.accdb";

        // Chaîne de connexion à la base de données
        public static string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + dbPath;

        // Création d'une connexion à la base de données
        OleDbConnection connection = new OleDbConnection(connectionString);


        public Form1()
        {
            InitializeComponent();
        }





        private void Form1_Load(object sender, EventArgs e)
        {
           

        }

        private void button1_Click(object sender, EventArgs e)
        {


            try
            {
                // Ouvre la connexion à la base de données
                connection.Open();

                // Requête SQL pour insérer un nouvel oeuvre
                string sql = "INSERT INTO oeuvre (Id, titre, auteur, categorie, disponible, date_entree) " +
                             "VALUES (@Id, @titre, @auteur, @categorie, @disponible, @date_entree)";

                // Création d'une commande SQL avec la requête et la connexion
                OleDbCommand command = new OleDbCommand(sql, connection);

                // Ajout des paramètres à la commande
                command.Parameters.AddWithValue("@Id", txtId.Text);
                command.Parameters.AddWithValue("@titre", txtTitre.Text);
                command.Parameters.AddWithValue("@auteur", txtAuteur.Text);
                command.Parameters.AddWithValue("@categorie", txtCategorie.Text);
                command.Parameters.AddWithValue("@disponible", txtQuantite.Text);
                command.Parameters.AddWithValue("@date_entree", dtpDateEntree.Value.ToShortDateString());

                // Exécution de la commande SQL
                int rowsAffected = command.ExecuteNonQuery();

                // Affiche un message d'alerte si l'oeuvre est ajouté avec succès
                if (rowsAffected > 0)
                {
                    MessageBox.Show("L'oeuvre a été ajouté avec succès à la base de données !");
                }
                else
                {
                    MessageBox.Show("Erreur lors de l'ajout de l'oeuvre à la base de données.");
                }
            }
            catch (Exception ex)
            {
                // Affiche un message d'erreur si l'ajout de l'oeuvre échoue
                MessageBox.Show("Erreur lors de l'ajout de l'oeuvre à la base de données : " + ex.Message);
            }
            finally
            {
                // Ferme la connexion à la base de données
                connection.Close();
            }


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {

            try
            {
                // Ouvre la connexion à la base de données
                connection.Open();

                // Affiche un message d'alerte si la connexion est établie
                MessageBox.Show("La connexion à la base de données a été établie avec succès !");
            }
            catch (Exception ex)
            {
                // Affiche un message d'erreur si la connexion échoue
                MessageBox.Show("Erreur lors de la connexion à la base de données : " + ex.Message);
            }
            finally
            {
                // Ferme la connexion à la base de données
                connection.Close();

            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0) // Check if a row was actually clicked and not the header row
            {
                // Get the selected row
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Set the values of the text boxes to the values of the selected row
                txtId.Text = row.Cells[0].Value.ToString();
                txtTitre.Text = row.Cells[1].Value.ToString();
                txtAuteur.Text = row.Cells[2].Value.ToString();
                txtCategorie.Text = row.Cells[3].Value.ToString();
                txtQuantite.Text = row.Cells[4].Value.ToString();
                dtpDateEntree.Value = (DateTime)row.Cells[5].Value;
            }

        }

        private void tabpage1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Set up the query string to retrieve data from the "books" table
            string queryString = "SELECT * FROM oeuvre";

            try
            {
                // Open the connection to the Access database
                connection.Open();

                // Create a new OleDbDataAdapter object using the query string and connection object
                OleDbDataAdapter adapter = new OleDbDataAdapter(queryString, connection);

                // Create a new DataTable object to store the data
                DataTable dt = new DataTable();

                // Fill the DataTable object with the data retrieved by the OleDbDataAdapter
                adapter.Fill(dt);

                // Set the DataSource property of the DataGridView to the DataTable object
                dataGridView1.DataSource = dt;

            }
            catch (Exception ex)
            {
                // Handle any errors that may occur when connecting to the database or retrieving data
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Close the connection to the database when done
                connection.Close();
            }

        }

        
        public static int executeUpdate(String query)
        {
            // Création d'une connexion à la base de données
            OleDbConnection connection = new OleDbConnection(connectionString);

            OleDbCommand cmd = new OleDbCommand(query, connection);
            try
            {
                connection.Open();
                //Since return type is System.Object, a typecast is must
                cmd.ExecuteNonQuery();
                //  count = (Int32)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return 1;
        }

        private DataTable GetDataFromDatabase()
        {
            string query = "SELECT * FROM oeuvre";
            DataTable dataTable = new DataTable();
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, connection))
                {
                    connection.Open();
                    adapter.Fill(dataTable);
                    connection.Close();
                }
            }
            return dataTable;
        }
        private DataTable GetDataFromDatabase1()
        {
            string query = "SELECT * FROM adherent";
            DataTable dataTable = new DataTable();
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, connection))
                {
                    connection.Open();
                    adapter.Fill(dataTable);
                    connection.Close();
                }
            }
            return dataTable;
        }
        private DataTable GetDataFromDatabase2()
        {
            string query = "SELECT * FROM prets";
            DataTable dataTable = new DataTable();
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, connection))
                {
                    connection.Open();
                    adapter.Fill(dataTable);
                    connection.Close();
                }
            }
            return dataTable;
        }

        private void RefreshDataGridView()
        {
            // Clear the existing data bindings
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();

            // Rebind the DataGridView to the data source
            // For example:
            dataGridView1.DataSource = GetDataFromDatabase();
        }
        private void RefreshDataGridView2()
        {
            // Clear the existing data bindings
            dataGridView2.DataSource = null;
            dataGridView2.Rows.Clear();

            // Rebind the DataGridView to the data source
            // For example:
            dataGridView2.DataSource = GetDataFromDatabase1();
        }
        private void RefreshDataGridView3()
        {
            // Clear the existing data bindings
            dataGridView3.DataSource = null;
            dataGridView3.Rows.Clear();

            // Rebind the DataGridView to the data source
            // For example:
            dataGridView3.DataSource = GetDataFromDatabase2();
        }
        private void button2_Click(object sender, EventArgs e)
        {
         
                // Get the selected row index
                int selectedRowIndex = dataGridView1.SelectedCells[0].RowIndex;

                // Get the ID of the selected row
                int id = (int)dataGridView1.Rows[selectedRowIndex].Cells[0].Value;

                // Update the record in the database
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    // Construct the SQL query
                    string query = "UPDATE oeuvre SET titre = @titre, auteur = @auteur, categorie = @categorie, disponible = @disponible, date_entree = @date_entree WHERE Id = @id";

                    // Create the command object with the SQL query and the connection object
                    using (OleDbCommand cmd = new OleDbCommand(query, connection))
                    {
                        // Set the parameter values
                        cmd.Parameters.AddWithValue("@titre", txtTitre.Text);
                        cmd.Parameters.AddWithValue("@auteur", txtAuteur.Text);
                        cmd.Parameters.AddWithValue("@categorie", txtCategorie.Text);
                        cmd.Parameters.AddWithValue("@disponible", txtQuantite.Text);
                        cmd.Parameters.AddWithValue("@date_entree", dtpDateEntree.Value);
                        cmd.Parameters.AddWithValue("@Id", id);

                        // Open the connection
                        connection.Open();

                        // Execute the query
                        cmd.ExecuteNonQuery();
                    }
                }
            // Refresh the data in the DataGridView
            RefreshDataGridView();




        }

        private void button3_Click(object sender, EventArgs e)
        {

                // Get the selected row index
                int selectedRowIndex = dataGridView1.SelectedCells[0].RowIndex;

                // Get the ID of the selected row
                int Id = (int)dataGridView1.Rows[selectedRowIndex].Cells[0].Value;

                // Delete the record from the database
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    // Construct the SQL query
                    string query = "DELETE FROM oeuvre WHERE Id = @Id";

                    // Create the command object with the SQL query and the connection object
                    using (OleDbCommand cmd = new OleDbCommand(query, connection))
                    {
                        // Set the parameter values
                        cmd.Parameters.AddWithValue("@id", Id);

                        // Open the connection
                        connection.Open();

                        // Execute the query
                        cmd.ExecuteNonQuery();
                    }
                }

                // Remove the selected row from the DataGridView
                dataGridView1.Rows.RemoveAt(selectedRowIndex);
            

        }

        private void searchbtn_Click(object sender, EventArgs e)
        {
          // Construct the SQL query
                string query = "SELECT * FROM oeuvre WHERE Id LIKE @Id OR titre LIKE @titre OR auteur LIKE @auteur OR categorie LIKE @categorie OR date_entree LIKE @date_entree";

                // Create the connection object
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    // Create the command object with the SQL query and the connection object
                    using (OleDbCommand cmd = new OleDbCommand(query, connection))
                    {
                    // Set the parameter values
                        cmd.Parameters.AddWithValue("@Id", "%" + txtSearch.Text + "%");
                        cmd.Parameters.AddWithValue("@titre", "%" + txtSearch.Text + "%");
                        cmd.Parameters.AddWithValue("@auteur", "%" + txtSearch.Text + "%");
                        cmd.Parameters.AddWithValue("@categorie", "%" + txtSearch.Text + "%");
                        cmd.Parameters.AddWithValue("@date_entree", "%" + txtSearch.Text + "%");

                        // Open the connection
                        connection.Open();

                        // Create a data adapter and fill a data table with the query results
                        OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        // Bind the data table to the DataGridView
                        dataGridView1.DataSource = table;
                    }
                }
            

        }

        private void addbtn_Click(object sender, EventArgs e)
        {
              using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    string query = "INSERT INTO adherent (nom, prenom, num_tele, email, adresse) VALUES (@nom, @prenom, @numTele, @email, @adresse)";

                    using (OleDbCommand cmd = new OleDbCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@nom", nomTextBox.Text);
                        cmd.Parameters.AddWithValue("@prenom", prenomTextBox.Text);
                        cmd.Parameters.AddWithValue("@numTele", numTeleTextBox.Text);
                        cmd.Parameters.AddWithValue("@email", emailTextBox.Text);
                        cmd.Parameters.AddWithValue("@adresse", adresseTextBox.Text);

                        connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("New adherent added successfully.");
            RefreshDataGridView2();
            

        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Set up the query string to retrieve data from the "books" table
            string queryString = "SELECT * FROM adherent";

            try
            {
                // Open the connection to the Access database
                connection.Open();

                // Create a new OleDbDataAdapter object using the query string and connection object
                OleDbDataAdapter adapter = new OleDbDataAdapter(queryString, connection);

                // Create a new DataTable object to store the data
                DataTable dt = new DataTable();

                // Fill the DataTable object with the data retrieved by the OleDbDataAdapter
                adapter.Fill(dt);

                // Set the DataSource property of the DataGridView to the DataTable object
                dataGridView2.DataSource = dt;

            }
            catch (Exception ex)
            {
                // Handle any errors that may occur when connecting to the database or retrieving data
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Close the connection to the database when done
                connection.Close();
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
          // Get the selected row index
                int selectedRowIndex = dataGridView2.SelectedCells[0].RowIndex;

                // Get the ID of the selected row
                int Id = (int)dataGridView2.Rows[selectedRowIndex].Cells[0].Value;

                // Construct the SQL query
                string query = "DELETE FROM adherent WHERE Id = @Id";

                // Create the command object with the SQL query and the connection object
                using (OleDbCommand cmd = new OleDbCommand(query, connection))
                {
                    // Set the parameter value
                    cmd.Parameters.AddWithValue("@Id", Id);

                    // Open the connection
                    connection.Open();

                    // Execute the query
                    cmd.ExecuteNonQuery();
                }

            // Refresh the data in the DataGridView
            RefreshDataGridView2();
            

        }

        private void button7_Click(object sender, EventArgs e)
        {
           
                // Get the selected row index
                int selectedRowIndex = dataGridView2.SelectedCells[0].RowIndex;

                // Get the ID of the selected row
                int id = (int)dataGridView2.Rows[selectedRowIndex].Cells[0].Value;

                // Update the record in the database
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    // Construct the SQL query
                    string query = "UPDATE adherent SET nom = @nom, prenom = @prenom, Num_tele = @Num_tele, email = @email, adresse = @adresse WHERE Id = @Id";

                    // Create the command object with the SQL query and the connection object
                    using (OleDbCommand cmd = new OleDbCommand(query, connection))
                    {
                        // Set the parameter values
                        cmd.Parameters.AddWithValue("@nom", nomTextBox.Text);
                        cmd.Parameters.AddWithValue("@prenom", prenomTextBox.Text);
                        cmd.Parameters.AddWithValue("@Num_tele", numTeleTextBox.Text);
                        cmd.Parameters.AddWithValue("@email", emailTextBox.Text);
                        cmd.Parameters.AddWithValue("@adresse", adresseTextBox.Text);
                        cmd.Parameters.AddWithValue("@Id", id);

               
                    // Open the connection
                    connection.Open();

                        // Execute the query
                        cmd.ExecuteNonQuery();
                    }
                }

            // Refresh the data in the DataGridView
            RefreshDataGridView2();
            

        }

        private void searchbtnn_Click(object sender, EventArgs e)
        {
            // Get the search query from the search box
                string searchQuery = searchl.Text.Trim();

                // Construct the SQL query
                string query = "SELECT * FROM adherent WHERE Id = @Id";

                // Create a new connection object
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    // Create a new command object with the SQL query and the connection object
                    using (OleDbCommand cmd = new OleDbCommand(query, connection))
                    {
                        // Add the parameter for the search query
                        cmd.Parameters.AddWithValue("@Id", searchQuery);

                        // Open the connection
                        connection.Open();

                        // Create a new data adapter object with the command object
                        using (OleDbDataAdapter adapter = new OleDbDataAdapter(cmd))
                        {
                            // Create a new data table object
                            DataTable dataTable = new DataTable();

                            // Fill the data table with the data from the database
                            adapter.Fill(dataTable);

                            // Bind the data table to the DataGridView
                            dataGridView2.DataSource = dataTable;
                        }
                    }
                
            }

        }

        private void button12_Click(object sender, EventArgs e)
        {

            int idAdherent = Convert.ToInt32(txtIdAdherent.Text);
            int idOeuvre = Convert.ToInt32(txtIdOeuvre.Text);
            DateTime datePret = dtpDatePret.Value;

            // Check if the adherent exists
            if (!AdherentExists(idAdherent))
            {
                MessageBox.Show("Adherent with ID " + idAdherent + " does not exist.");
                return;
            }

            // Check if the oeuvre exists
            if (!OeuvreExists(idOeuvre))
            {
                MessageBox.Show("Oeuvre with ID " + idOeuvre + " does not exist.");
                return;
            }

            // Add the record to the database
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                // Construct the SQL query
                string query = "INSERT INTO prets (id_adherent, id_oeuvre, date_pret)"+ " VALUES (@idAdherent, @idOeuvre, @datePret)";

                // Create the command object with the SQL query and the connection object
                using (OleDbCommand cmd = new OleDbCommand(query, connection))
                {
                    /*    // Set the parameter values
                        cmd.Parameters.AddWithValue("@idAdherent", idAdherent);
                        cmd.Parameters.AddWithValue("@idOeuvre", idOeuvre);
                        cmd.Parameters.AddWithValue("@datePret", datePret);*/
                

                    cmd.Parameters.AddWithValue("@idAdherent", txtIdAdherent.Text);
                    cmd.Parameters.AddWithValue("@idOeuvre", txtIdOeuvre.Text);
                    cmd.Parameters.AddWithValue("@datePret", dtpDatePret.Value.ToShortDateString());
                    
                 


                    // Open the connection
                    connection.Open();

                    // Execute the query
                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Record added successfully.");
        }

        private bool AdherentExists(int idAdherent)
        {
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                // Construct the SQL query
                string query = "SELECT COUNT(*) FROM adherent WHERE Id = @idAdherent";

                // Create the command object with the SQL query and the connection object
                using (OleDbCommand cmd = new OleDbCommand(query, connection))
                {
                    // Set the parameter values
                    cmd.Parameters.AddWithValue("@idAdherent", idAdherent);

                    // Open the connection
                    connection.Open();

                    // Execute the query and get the count of rows
                    int count = (int)cmd.ExecuteScalar();

                    // Return true if there is at least one row with the specified id
                    return count > 0;
                }
            }
        }

        private bool OeuvreExists(int idOeuvre)
        {
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                // Construct the SQL query
                string query = "SELECT COUNT(*) FROM oeuvre WHERE Id = @idOeuvre";

                // Create the command object with the SQL query and the connection object
                using (OleDbCommand cmd = new OleDbCommand(query, connection))
                {
                    // Set the parameter values
                    cmd.Parameters.AddWithValue("@idOeuvre", idOeuvre);

                    // Open the connection
                    connection.Open();

                    // Execute the query and get the count of rows
                    int count = (int)cmd.ExecuteScalar();

                    // Return true if there is at least one row with the specified id
                    return count > 0;
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            // Set up the query string to retrieve data from the "books" table
            string queryString = "SELECT * FROM prets";

            try
            {
                // Open the connection to the Access database
                connection.Open();

                // Create a new OleDbDataAdapter object using the query string and connection object
                OleDbDataAdapter adapter = new OleDbDataAdapter(queryString, connection);

                // Create a new DataTable object to store the data
                DataTable dt = new DataTable();

                // Fill the DataTable object with the data retrieved by the OleDbDataAdapter
                adapter.Fill(dt);

                // Set the DataSource property of the DataGridView to the DataTable object
                dataGridView3.DataSource = dt;

            }
            catch (Exception ex)
            {
                // Handle any errors that may occur when connecting to the database or retrieving data
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Close the connection to the database when done
                connection.Close();
            }


        }

        private void button10_Click(object sender, EventArgs e)
        {
            // Get the selected row index
            int selectedRowIndex = dataGridView3.SelectedCells[0].RowIndex;

            // Get the ID of the selected row
            int id_adherent = (int)dataGridView3.Rows[selectedRowIndex].Cells[0].Value;

            // Construct the SQL query
            string query = "DELETE FROM prets WHERE  id_adherent= @id_adherent";

            // Create the command object with the SQL query and the connection object
            using (OleDbCommand cmd = new OleDbCommand(query, connection))
            {
                // Set the parameter value
                cmd.Parameters.AddWithValue("@id_adherent", id_adherent);

                // Open the connection
                connection.Open();

                // Execute the query
                cmd.ExecuteNonQuery();
            }

            // Refresh the data in the DataGridView
            RefreshDataGridView3();


        }

        private void button11_Click(object sender, EventArgs e)
        {
           
            // Get the selected row index
            int selectedRowIndex = dataGridView3.SelectedCells[0].RowIndex;

            // Get the ID of the selected row
            int id_adherent = (int)dataGridView3.Rows[selectedRowIndex].Cells[0].Value;

            // Update the record in the database
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                // Construct the SQL query
                string query = "UPDATE prets SET id_oeuvre = @id_oeuvre, date_pret = @date_pret WHERE id_adherent = @id_adherent";

                // Create the command object with the SQL query and the connection object
                using (OleDbCommand cmd = new OleDbCommand(query, connection))
                {
                    // Set the parameter values
             
                    cmd.Parameters.AddWithValue("@id_oeuvre", txtIdOeuvre.Text);
                    cmd.Parameters.AddWithValue("@date_pret", dtpDatePret.Value);
                    cmd.Parameters.AddWithValue("@id_adherent", id_adherent);

                    // Open the connection
                    connection.Open();

                    // Execute the query
                    cmd.ExecuteNonQuery();
                }
            }
            // Refresh the data in the DataGridView
            RefreshDataGridView3();

        }

        private void tabpage2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Check if a row was actually clicked and not the header row
            {
                // Get the selected row
                DataGridViewRow row = dataGridView2.Rows[e.RowIndex];

                // Set the values of the text boxes to the values of the selected row
                id.Text = row.Cells[0].Value.ToString();
                nomTextBox.Text = row.Cells[1].Value.ToString();
                prenomTextBox.Text = row.Cells[2].Value.ToString();
                numTeleTextBox.Text = row.Cells[3].Value.ToString();
                emailTextBox.Text = row.Cells[4].Value.ToString();
                adresseTextBox.Text = row.Cells[5].Value.ToString();
               

            }
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Check if a row was actually clicked and not the header row
            {
                // Get the selected row
                DataGridViewRow row = dataGridView3.Rows[e.RowIndex];

                // Set the values of the text boxes to the values of the selected row
                txtIdAdherent.Text = row.Cells[0].Value.ToString();
                txtIdOeuvre.Text = row.Cells[1].Value.ToString();
                dtpDatePret.Value = (DateTime)row.Cells[2].Value;


            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            // Get the search query from the search box
            string searchQuery = serchpr.Text.Trim();

            // Construct the SQL query
            string query = "SELECT * FROM prets WHERE id_adherent = @id_adherent";

            // Create a new connection object
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                // Create a new command object with the SQL query and the connection object
                using (OleDbCommand cmd = new OleDbCommand(query, connection))
                {
                    // Add the parameter for the search query
                    cmd.Parameters.AddWithValue("@id_adherent", searchQuery);

                    // Open the connection
                    connection.Open();

                    // Create a new data adapter object with the command object
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(cmd))
                    {
                        // Create a new data table object
                        DataTable dataTable = new DataTable();

                        // Fill the data table with the data from the database
                        adapter.Fill(dataTable);

                        // Bind the data table to the DataGridView
                        dataGridView3.DataSource = dataTable;
                    }
                }

            }

        }
    }
}
    





