using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VehicleProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private DataTable loadTable()
        {
            DataTable carData = new DataTable();

            try
            {
                string connectionString = @"Data Source=radproject.database.windows.net;Initial Catalog=RADProject;User id=User;Password=asdFGH123;Integrated Security=True;Trusted_Connection=False;Encrypt=True;";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                string query = "SELECT (SELECT modelName from VehicleType WHERE VehicleType.VehicleTypeId = (SELECT VehicleTypeID FROM Model WHERE Model.ModelId = Vehicle.ModelId))AS Model, Make.makeName,modelYear,Price,SoldDate FROM Vehicle JOIN Model ON Model.ModelId = Vehicle.ModelId JOIN Make ON Make.MakeId = Vehicle.MakeId;";
                               
                SqlCommand command = new SqlCommand(query, connection);

                SqlDataReader dr = command.ExecuteReader();
                carData.Load(dr);

                connection.Close();
            }

            catch (Exception e)
            {
                Console.Write(e.GetBaseException());
            }
            return carData;
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            panel1.Visible = true;
            dataGridView1.DataSource = loadTable();
        }

        private void available_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = showAvailable();
        }

        private DataTable showAvailable()
        {
            DataTable carData = new DataTable();

            try
            {
                string connectionString = @"Data Source=radproject.database.windows.net;Initial Catalog=RADProject;User id=User;Password=asdFGH123;Integrated Security=True;Trusted_Connection=False;Encrypt=True;";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                string query = "SELECT (SELECT modelName from VehicleType WHERE VehicleType.VehicleTypeId = (SELECT VehicleTypeID FROM Model WHERE Model.ModelId = Vehicle.ModelId))AS Model, Make.makeName,modelYear,Price,SoldDate FROM Vehicle JOIN Model ON Model.ModelId = Vehicle.ModelId JOIN Make ON Make.MakeId = Vehicle.MakeId WHERE SoldDate IS NULL;";

                SqlCommand command = new SqlCommand(query, connection);

                SqlDataReader dr = command.ExecuteReader();
                carData.Load(dr);

                connection.Close();
            }

            catch (Exception e)
            {
                Console.Write(e.GetBaseException());
            }
            return carData;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = showSold();
        }
        private DataTable showSold()
        {
            DataTable carData = new DataTable();

            try
            {
                string connectionString = @"Data Source=radproject.database.windows.net;Initial Catalog=RADProject;User id=User;Password=asdFGH123;Integrated Security=True;Trusted_Connection=False;Encrypt=True;";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                string query = "SELECT (SELECT modelName from VehicleType WHERE VehicleType.VehicleTypeId = (SELECT VehicleTypeID FROM Model WHERE Model.ModelId = Vehicle.ModelId))AS Model, Make.makeName,modelYear,Price,SoldDate FROM Vehicle JOIN Model ON Model.ModelId = Vehicle.ModelId JOIN Make ON Make.MakeId = Vehicle.MakeId WHERE SoldDate IS NOT NULL;";

                SqlCommand command = new SqlCommand(query, connection);

                SqlDataReader dr = command.ExecuteReader();
                carData.Load(dr);

                connection.Close();
            }

            catch (Exception e)
            {
                Console.Write(e.GetBaseException());
            }
            return carData;
        }
    }
}
