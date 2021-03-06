﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovieRentShop
{
    class Functions
    {
        SqlConnection conn = new SqlConnection("Data Source = DESKTOP-8PLOII5; Initial Catalog = MovieShop; Integrated Security = True");
        SqlCommand cmd = new SqlCommand();

        string query;

        public void best_customer()
        {// find best customer
            int Top = 0, Max = 0, Total = 0;
            string Value = "";
            try
            {
                cmd.Parameters.Clear();
                cmd.Connection = conn;
                string Val = "Select IDENT_CURRENT('Customer')";

                cmd.CommandText = Val;
                conn.Open();
                Total = Convert.ToInt32(cmd.ExecuteScalar());

                for (int i = 1; i <= Total; i++)
                {

                    Value = "select Count(*) from RentedMovies where CustIDFK= '" + i + "'";


                    cmd.CommandText = Value;
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count > Max)
                    {
                        Max = count;
                        Top = i;
                    }
                }
                this.query = "Select FirstName from Customer where CustID ='" + Top + "'";
                this.cmd.CommandText = this.query;
                String FirstName = Convert.ToString(cmd.ExecuteScalar());
                MessageBox.Show("Our Top of List Customer is " + FirstName + "\nRented Movies: " + Max);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Database Exception " + exception.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

        }


        public void best_movie()
        {// find best movie 
            int Top = 0, Max = 0, Total = 0;
            string Value = "";
            try
            {
                cmd.Parameters.Clear();
                cmd.Connection = conn;
                string Val = "Select IDENT_CURRENT('Movies')";

                cmd.CommandText = Val;
                conn.Open();
                Total = Convert.ToInt32(cmd.ExecuteScalar());

                for (int i = 1; i <= Total; i++)
                {

                    Value = "select Count(*) from RentedMovies where MovieIDFK= '" + i + "'";


                    cmd.CommandText = Value;
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count > Max)
                    {
                        Max = count;
                        Top = i;
                    }
                }


                this.query = "Select Title from Movies where MovieID ='" + Top + "'";
                this.cmd.CommandText = this.query;
                String Title = Convert.ToString(cmd.ExecuteScalar());
                MessageBox.Show("Best Movie in store is " + Title + "\nRented: " + Max);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Database Exception " + exception.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

        }
    }
}
