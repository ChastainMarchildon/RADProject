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

        private void loadTable()
        {
            string textToWrite = "";
            string[] headers = { "Make", "Model", "Year", "Price", "Sold Date" };
            TableLayoutPanel carTable = new TableLayoutPanel();
            carTable.Location = new Point(12, 12);
            carTable.Size = new Size(260, 200);
            carTable.ColumnCount = 5;
            carTable.RowCount = 4;
            panel1.Controls.Add(carTable);

            try
            {
                string connectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = 'c:\users\owner\source\repos\VehicleProject\VehicleProject\Database1.mdf'; Integrated Security = True";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                string query = "SELECT * FROM Vehicle";
                SqlCommand command = new SqlCommand(query, connection);

                SqlDataReader dr = command.ExecuteReader();

                while (dr.HasRows)
                {
                    ArrayList data = new ArrayList();

                    string make = "";
                    int Make = dr.GetOrdinal("MakeId");
                    make = getMake(Make);
                    data.Add(make);

                    string model = "";
                    int Model = dr.GetOrdinal("ModelId");
                    model = getModel(Model);
                    data.Add(model);

                    string year = "";
                    if (dr.Read())
                        year = String.Format("%d", dr.GetInt32(dr.GetOrdinal("Year")));
                    data.Add(year);

                    string price = "";
                    if (dr.Read())
                        price = String.Format("{0:0.##}", dr.GetDecimal(dr.GetOrdinal("Price")));
                    data.Add(price);

                    string sold = "";
                    if (dr.Read())
                        sold = String.Format("%d", dr.GetDateTime(dr.GetOrdinal("SoldDate")));
                    data.Add(sold);

                    carTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));

                    for (int colCount = 0; colCount < 5; colCount++)
                    {
                        for (int i = 0; i < data.Count; i++)
                        {
                            Label label = new Label();
                            if (i == 0)
                            {
                                textToWrite = headers[colCount];
                            }
                            else
                            {
                                textToWrite = String.Format("%d", data[i]);
                            }
                            label.Text = textToWrite;
                            carTable.Controls.Add(label, colCount, i);
                        }
                    }
                }

                connection.Close();
            }
            catch (Exception e)
            {
                Console.Write(e.GetBaseException());
            }
        }
    

        private string getMake(int MakeId)
        {
            string makeName = "";
            string connectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = 'c:\users\owner\source\repos\VehicleProject\VehicleProject\Database1.mdf'; Integrated Security = True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string query = "SELECT Name FROM Make WHERE MakeId = "+MakeId;
            SqlCommand command = new SqlCommand(query, connection);

            SqlDataReader dr = command.ExecuteReader();
            if(dr.Read())
            {
                makeName = dr.GetString(dr.GetOrdinal("Name"));
            }
            connection.Close();
            return makeName;
        
        }
        private string getModel(int ModelId)
        {
            string modelName = "";
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=c:\users\owner\source\repos\VehicleProject\VehicleProject\Database1.mdf;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string query = "SELECT Name FROM VehicleType WHERE VehicleTypeId = (SELECT VehicleTypeId FROM Model WHERE ModelId = " + ModelId +")";
            SqlCommand command = new SqlCommand(query, connection);

            SqlDataReader dr = command.ExecuteReader();
            if (dr.Read())
            {
                modelName = dr.GetString(1);
            }
            connection.Close();
            return modelName;
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            panel1.Visible = true;
            loadTable();

        }
    }
}
