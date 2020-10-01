using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;

namespace MovieRentShop
{
    public partial class frmMain : Form
    {
        // Create objects of all classes

        MovieShop movieShop = new MovieShop();
  
        public frmMain()
        {
            InitializeComponent();
  
        }
        private void populate_customer_view(DataTable d)
        {
            lvCustomers.Items.Clear();

            for (int i = 0; i < d.Rows.Count; i++)
            {
            
               
                lvCustomers.Items.Add(d.Rows[i].ItemArray[0].ToString());
                lvCustomers.Items[i].SubItems.Add(d.Rows[i].ItemArray[1].ToString());
                lvCustomers.Items[i].SubItems.Add(d.Rows[i].ItemArray[2].ToString());
                lvCustomers.Items[i].SubItems.Add(d.Rows[i].ItemArray[3].ToString());
                lvCustomers.Items[i].SubItems.Add(d.Rows[i].ItemArray[4].ToString());
            }
            
        
        }
        private void populate_movies_view(DataTable d)
        {
            lvMovies.Items.Clear();

            foreach (DataRow row in d.Rows)
            {
                string[] items = { row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(),row[5].ToString(),row[6].ToString(),row[7].ToString() };
                lvMovies.Items.Add(new ListViewItem(items));
            }

        }
        private void populate_rents_view(DataTable d)
        {
            lvRentedMovies.Items.Clear();
            foreach (DataRow row in d.Rows)
            {
                string[] items = { row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(),row[5].ToString() };
                lvRentedMovies.Items.Add(new ListViewItem(items));
            }

        }
        private void BtnUpdateCust_Click(object sender, EventArgs e)
        {
            if (tbCustFirstName.Text != "" && tbCustLastName.Text != "" && tbCustAddress.Text != "" && tbCustPhone.Text != "")
            {
                string firstname = tbCustFirstName.Text;
                string lastname = tbCustLastName.Text;
                string address = tbCustAddress.Text;
                string phone = tbCustPhone.Text;
                int custId = Convert.ToInt32(tbCustID.Text);
                movieShop.update_customer(custId, firstname, lastname, address, phone);
                populate_customer_view(movieShop.get_customers());
                clear_customer_boxes();    
            } else
            {
                MessageBox.Show("Customer Fields are empty");
            }
        }
        // this func clear text boxes for customer
        private void clear_customer_boxes()
        {
            tbCustID.Text = "";
            tbCustFirstName.Text = "";
            tbCustAddress.Text = "";
            tbCustLastName.Text = "";
            tbCustPhone.Text = "";
        }
        private void BtnAddCust_Click(object sender, EventArgs e)
        {
            if (tbCustFirstName.Text != "" && tbCustLastName.Text != "" && tbCustAddress.Text != "" && tbCustPhone.Text != "")
            {
        
                movieShop.add_customer(tbCustFirstName.Text, tbCustLastName.Text, tbCustAddress.Text, tbCustPhone.Text);
                populate_customer_view(movieShop.get_customers()); 
                clear_customer_boxes();
            }
            else
            {
                MessageBox.Show("Customer Fields are empty");
            }
        }

        private void BtnDeleteCust_Click(object sender, EventArgs e)
        {
            if(tbCustID.Text!="")
            {
                // confirm to delete customer
                int custId = Convert.ToInt32(tbCustID.Text);
                DialogResult mbresult = MessageBox.Show("Do you want to delete?", "customer", MessageBoxButtons.YesNo);
                if(mbresult.ToString()=="Yes")
                {
                    movieShop.delete_customer(custId);
                    MessageBox.Show("Deleted!");
                    populate_customer_view(movieShop.get_customers());
                    clear_customer_boxes();
                }
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            // fetch all data into data grid view for customers, movies and rented movies on loading
            // and set it into view
         
            populate_customer_view(movieShop.get_customers());
            populate_movies_view(movieShop.get_movies());
            populate_rents_view(movieShop.get_rented_movies()); // update list view


        }

    
        // this func clears textboxes for movie
        private void clear_movies_boxes()
        {
            tbMovieId.Text = ""; tbMovieName.Text = ""; tbMovieGenre.Text = ""; tbMovieRating.Text = ""; tbMovieYear.Text = ""; tbMovieCopies.Text = ""; tbMoviePlot.Text = "";
        }
        private void BtnAddMovie_Click(object sender, EventArgs e)
        {
            // input validate
            if(tbMovieName.Text!="" && tbMovieGenre.Text!="" && tbMovieRating.Text!="" && tbMovieYear.Text!="" && tbMovieCopies.Text!="" && tbMoviePlot.Text!="")
            {
                int movieYear = Convert.ToInt32(tbMovieYear.Text);
                int copies = Convert.ToInt32(tbMovieCopies.Text);
                string rent;
               // if movie is older than 5 years then rent is 2 else 5
               
                if(DateTime.Now.Date.Year-movieYear > 5) 
                {
                    rent = "2";
                }else
                {
                    rent = "5";
                }
                movieShop.add_movie(tbMovieRating.Text, tbMovieName.Text, tbMovieYear.Text, rent, tbMoviePlot.Text, tbMovieGenre.Text, copies);
                populate_movies_view(movieShop.get_movies());
                clear_movies_boxes();
            }else
            {
                MessageBox.Show("Movie Fields are empty");
            }
        }

        private void BtnUpdateMovie_Click(object sender, EventArgs e)
        {
            // perform check
            if (tbMovieId.Text != "" && tbMovieName.Text != "" && tbMovieGenre.Text != "" && tbMovieRating.Text != "" && tbMovieYear.Text != "" && tbMovieCopies.Text != "" && tbMoviePlot.Text != "")
            {
                // get data from text boxes
                int movieId = Convert.ToInt32(tbMovieId.Text);
                int copies = Convert.ToInt32(tbMovieCopies.Text);
                int year = Convert.ToInt32(tbMovieYear.Text);

                string title = tbMovieName.Text;
                string rating = tbMovieRating.Text;
                string genre = tbMovieGenre.Text;
                string plot = tbMoviePlot.Text;
                //updates movie in db table
                movieShop.update_movie(movieId, rating, title, year, plot, genre, copies);
                MessageBox.Show("Updated!", "Success");
                populate_movies_view(movieShop.get_movies());
                clear_movies_boxes();
            }else
            {
                MessageBox.Show("Movie Fileds are Empty");
            }

        }

        private void BtnDeleteMovie_Click(object sender, EventArgs e)
        {
            if(tbMovieId.Text!="")
            {
                // confirm to delete
                DialogResult result = MessageBox.Show("do you want to delete?", "Confirm", MessageBoxButtons.YesNo);
                if(result.ToString()=="Yes")
                {
                    int movieId = Convert.ToInt32(tbMovieId.Text);
                    movieShop.delete_movie(movieId); // delete a movie by id
                    populate_movies_view(movieShop.get_movies());
                    clear_movies_boxes();
                }
            }
            else
            {
                MessageBox.Show("Select a movie first");
            }
        }


        private void BtnIssueMovie_Click(object sender, EventArgs e)
        {
            if(tbMovieId.Text !="" && tbCustID.Text!="")
            {
                if(tbMovieCopies.Text!="0")
                {
                    int movieId = Convert.ToInt32(tbMovieId.Text);
                    int custId = Convert.ToInt32(tbCustID.Text);
                    int copies = Convert.ToInt32(tbMovieCopies.Text);
                    // sets rented value to 1 on current date
                    movieShop.add_rented_movie(movieId, custId, DateTime.Now, copies, 1); // adds rented movie
                    populate_rents_view(movieShop.get_rented_movies()); // update list view
                    // clear all textboxes
                    clear_customer_boxes();
                    clear_movies_boxes();

                }
                else
                {
                    MessageBox.Show("All copies are not. nothing left", "Problem");
                }
            }else
            {
                MessageBox.Show("Select a customer and movie to rent");
            }
        }

        private void BtnReturnMovie_Click(object sender, EventArgs e)
        {
            // perform check
            if(tbMovieId.Text!="" && tbDateRented.Text!="" && tbRMID.Text!="")
            {
                int rmid = Convert.ToInt32(tbRMID.Text);
                int movieId = Convert.ToInt32(tbMovieId.Text);
                String date = tbDateRented.Text;
                // return movie by updating rented movies table, var are passed via method created in Class MovieRentals
                movieShop.update_rented_movie(rmid, movieId, Convert.ToDateTime(date), DateTime.Now);
                dgvRentals.DataSource = movieShop.get_rented_movies().DefaultView;
                populate_customer_view(movieShop.get_customers());
                populate_movies_view(movieShop.get_movies());
                populate_rents_view(movieShop.get_rented_movies());
                clear_customer_boxes(); // clears customer inputs
                clear_movies_boxes(); // clears movie inputs
                tbRMID.Text = ""; //clear rented movie inputs
                tbDateRented.Text = "";
            }else
            {
                MessageBox.Show("first select a Movie to return");
            }
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit(); // closes the main application on form closing
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        
        Functions f = new Functions();

        private void BtnBestCustomer_Click(object sender, EventArgs e)
        {
            f.best_customer();
        }

        private void BtnBestMoie_Click(object sender, EventArgs e)
        {
            f.best_movie();
        }

        private void LvRentedMovies_ItemActivate(object sender, EventArgs e)
        {
            ListViewItem item= lvRentedMovies.SelectedItems[0];
            tbRMID.Text = item.SubItems[0].Text;
            tbDateRented.Text = item.SubItems[3].Text;
            tbMovieId.Text = item.SubItems[1].Text;
        }

        private void LvMovies_ItemActivate(object sender, EventArgs e)
        {

            ListViewItem item = lvMovies.SelectedItems[0];
            tbMovieId.Text = item.SubItems[0].Text;
            tbMovieRating.Text = item.SubItems[1].Text;
            tbMovieName.Text = item.SubItems[2].Text;
            tbMovieYear.Text = item.SubItems[3].Text;
            tbMovieCopies.Text = item.SubItems[5].Text;
            tbMoviePlot.Text =  item.SubItems[6].Text;
            tbMovieGenre.Text = item.SubItems[7].Text;

        }

        private void LvCustomers_ItemActivate(object sender, EventArgs e)
        {
            ListViewItem item = lvCustomers.SelectedItems[0];
            tbCustID.Text = item.SubItems[0].Text;
            tbCustFirstName.Text = item.SubItems[1].Text;
            tbCustLastName.Text = item.SubItems[2].Text;
            tbCustAddress.Text = item.SubItems[3].Text;
            tbCustPhone.Text = item.SubItems[4].Text;
        }
    }
}
