using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using Spectre.Console;
namespace Product_Management
{ public 
    class Product
    { 
    
        public static void Add_New_Product(SqlConnection con)
        {
            SqlDataAdapter adp = new SqlDataAdapter("select * from ProductManagement", con);
            DataSet ds = new DataSet();
            Console.WriteLine("ADD NEW PRODUCT");
            adp.Fill(ds, "product");
            var row = ds.Tables["product"].NewRow();
            Console.WriteLine("Enter Product Name");
            row["Product_Name"] = Console.ReadLine();
            Console.WriteLine("Enter Product Description");
            row["Product_description"] = Console.ReadLine();
            Console.WriteLine("Enter Quantity");
            row["Quantity"] = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Price");
            row["Price"] = Convert.ToInt32(Console.ReadLine());
            ds.Tables[0].Rows.Add(row);
            SqlCommandBuilder builder = new SqlCommandBuilder(adp);
            adp.Update(ds, "product");
            AnsiConsole.Write(new Markup("[green] Product Added Successfully [/]"));
                    
        }
        public static void view_all_Products(SqlConnection con)
        {
            Console.WriteLine("VIEW ALL PRODUCTS");
            SqlDataAdapter adp = new SqlDataAdapter("select * from ProductManagement", con);
            DataSet ds = new DataSet();
            adp.Fill(ds, "product");
            var row = new Table();
            row.AddColumn("Product_ID");
            row.AddColumn("Product_Name");
            row.AddColumn("Product_description");
            row.AddColumn("Quantity");
            row.AddColumn("Price");
            for (int i = 0; i < ds.Tables["product"].Rows.Count; i++)
            {
                row.AddRow(ds.Tables["product"].Rows[i][0].ToString(), ds.Tables["product"].Rows[i][1].ToString(), ds.Tables["product"].Rows[i][2].ToString(), ds.Tables["product"].Rows[i][3].ToString(), ds.Tables["product"].Rows[i][4].ToString());

            }
            AnsiConsole.Write(row);
        }
        public static void view_Product(SqlConnection con)
        {
            Console.WriteLine("VIEW PRODUCTS");
            Console.WriteLine("enter ID");
            int id = Convert.ToInt16(Console.ReadLine());
            SqlDataAdapter adp = new SqlDataAdapter($"select * from ProductManagement where Product_ID={id}", con);
            DataSet ds = new DataSet();
            adp.Fill(ds, "product");
            var row = new Table();
            row.AddColumn("Product_ID");
            row.AddColumn("Product_Name");
            row.AddColumn("Product_description");
            row.AddColumn("Quantity");
            row.AddColumn("Price");            
            try
            {
                for (int i = 0; i < ds.Tables["product"].Rows.Count; i++)
                {


                    row.AddRow(ds.Tables["product"].Rows[i][0].ToString(), ds.Tables["product"].Rows[i][1].ToString(), ds.Tables["product"].Rows[i][2].ToString(), ds.Tables["product"].Rows[i][3].ToString(), ds.Tables["product"].Rows[i][4].ToString());
                }
                AnsiConsole.Write(row);
            }
            catch (Exception)
            {
                AnsiConsole.Write(new Markup($"[red] ID not exist with number{id} [/]")); return;
            }
            
            
        }
        public static void Delete_Product(SqlConnection con)
        {
            Console.WriteLine("enter ID");
            int id = Convert.ToInt16(Console.ReadLine());
            SqlDataAdapter adp = new SqlDataAdapter($"Select * from keepNote where Id={id}", con);

            DataSet ds = new DataSet();
            Console.WriteLine("DELETE PRODUCTS");
           
            adp.Fill(ds, "product");
            
           
            try
            {
                ds.Tables["product"].Rows[0].Delete();

                SqlCommandBuilder builder = new SqlCommandBuilder(adp);
                adp.Update(ds, "product");
                AnsiConsole.Write(new Markup("[green] Product deleted Successfully [/]"));
                
            }
            catch (Exception)
            {
                AnsiConsole.Write(new Markup($"[red] ID not exist with number{id} [/]"));
                return;
            }
        }
        public static void Update_Product(SqlConnection con)
        {
            Console.WriteLine("UPDATE PRODUCTS");
            Console.WriteLine("enter updated id");
            int id = Convert.ToInt16(Console.ReadLine());
            SqlDataAdapter adp = new SqlDataAdapter($"Select * from keepNote where Id={id}", con);
            DataSet ds = new DataSet();
            adp.Fill(ds, "product");


            try
            {
                ds.Tables["product"].Rows[0][1] = Console.ReadLine();
                Console.WriteLine("Enter Product Name");
                
                Console.WriteLine("Enter Product Description");
                ds.Tables["product"].Rows[0][2] = Console.ReadLine();
                Console.WriteLine("Enter Quantity");
                ds.Tables["product"].Rows[0][3] = Console.ReadLine();
                Console.WriteLine("Enter Price");
                ds.Tables["product"].Rows[0][4] = Console.ReadLine();
                SqlCommandBuilder builder = new SqlCommandBuilder(adp);
                adp.Update(ds, "product");
                AnsiConsole.Write(new Markup("[bold green]Product Updated succesfully[/]"));
            }
            catch
            {
                AnsiConsole.Write(new Markup($"[bold red]ID not exist with number {id}[/]"));
            }

        }
    }
    
    internal class Program
    {
        static void Main(string[] args)
        {
            SqlConnection con = new SqlConnection("Server=IN-5YC79S3; database=TestDB; Integrated Security=true");
            string res;
            AnsiConsole.Write(new FigletText("Product Management App").Centered().Color(Color.Red));
            do
            {
                var choice = AnsiConsole.Prompt(new SelectionPrompt<string>().Title("What's your [yellow]choice[/]")
                    .AddChoices(new[] { "Add New Product", "Get Product", "Get All Products", "Update Product", "Delete Product" }));
                switch (choice)
                {
                    case "Add New Product":
                        {
                            Product.Add_New_Product(con);
                            break;
                        }
                    case "Get Product":
                        {
                            Product.view_Product(con);

                            break;
                        }
                    case "Get All Products":
                        {
                            Product.view_all_Products(con);

                            break;
                        }
                    case "Update Product":
                        {
                            Product.Update_Product(con);

                            break;
                        }
                    case "Delete Product":
                        {
                            Product.Delete_Product(con);

                            break;
                        }
                }
                res = AnsiConsole.Ask<string>("Do you wish to [pink1] continue y/n? [/] ");
            } while (res.ToLower() == "y");
        }
    }
}