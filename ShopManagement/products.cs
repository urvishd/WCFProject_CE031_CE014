using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ShopManagement
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "products" in both code and config file together.
    public class products : Iproducts
    {
        string connetionString;
        SqlConnection cnn;
        public string AddProduct(Product product)
        {
            connetionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ShopDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            if(product.ProductId==-1)
            {
                try
                {
                    var sql = "INSERT INTO Products(Name,Price,Quantity,Description) VALUES(@pname,@pprice,@qun,@des)";
                    using (var cmd = new SqlCommand(sql, cnn))
                    {
                        cmd.Parameters.AddWithValue("@pname", product.ProductName);
                        cmd.Parameters.AddWithValue("@pprice", product.ProductPrice);
                        cmd.Parameters.AddWithValue("@qun", product.Quantity);
                        cmd.Parameters.AddWithValue("@des", product.Description);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    cnn.Close();
                    return "Error in inserting Product!";
                }
            }
            else
            {
                try
                {
                    var sql = "INSERT INTO Products(Id,Name,Price,Quantity,Description) VALUES(@id1,@pname,@pprice,@qun,@des)";
                    using (var cmd = new SqlCommand(sql, cnn))
                    {
                        cmd.Parameters.AddWithValue("@id1", product.ProductId);
                        cmd.Parameters.AddWithValue("@pname", product.ProductName);
                        cmd.Parameters.AddWithValue("@pprice", product.ProductPrice);
                        cmd.Parameters.AddWithValue("@qun", product.Quantity);
                        cmd.Parameters.AddWithValue("@des", product.Description);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    cnn.Close();
                    return "Product already inserted!";
                }
            }



            cnn.Close();
            return "Product added Successfully!!";
        }

        

        public List<Product> GetAllProduct()
        {
            throw new NotImplementedException();
        }

        public Product GetProduct(int id)
        {
            connetionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ShopDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            string command = "select * from Products where Id=" + id;
            SqlCommand cmd = new SqlCommand(command, cnn);
            SqlDataReader rdr = cmd.ExecuteReader();
            Product pd = new Product();
            while (rdr.Read())
            {
                pd.ProductName = rdr["Name"].ToString();
                pd.ProductPrice = (int)rdr["Price"];
                pd.Quantity = (int)rdr["Quantity"];
                pd.Description = rdr["Description"].ToString();
            }
            cnn.Close();
            return pd;
        }

        public int GetProductPrice(int id)
        {
            connetionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ShopDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            string command = "select Price from Products where Id=" + id;
            SqlCommand cmd = new SqlCommand(command, cnn);
            SqlDataReader rdr = cmd.ExecuteReader();
            int ans=0;
            while (rdr.Read())
            {
                ans= (int)rdr["Price"];
            }
            cnn.Close();
            return ans;
        }

        public int StoreBill(Bill b)
        {
            connetionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ShopDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            var sql = "INSERT INTO Bills(Date,Total) output INSERTED.Billid VALUES(@date,@total)";
            using (var cmd = new SqlCommand(sql, cnn))
            {
                cmd.Parameters.AddWithValue("@date", b.date);
                cmd.Parameters.AddWithValue("@total",b.total);
                int modified = (int)cmd.ExecuteScalar();
                cnn.Close();
                return modified;
            }
          
           

        }

        public void UpdateProductDetails(Product product)
        {
            connetionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ShopDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            var sql = "UPDATE Products set Name=@pname,Price=@pprice,Quantity=@qun,Description=@des where Id=@pid";
            using (var cmd = new SqlCommand(sql, cnn))
            {
                cmd.Parameters.AddWithValue("@pid", product.ProductId);
                cmd.Parameters.AddWithValue("@pname", product.ProductName);
                cmd.Parameters.AddWithValue("@pprice", product.ProductPrice);
                cmd.Parameters.AddWithValue("@qun", product.Quantity);
                cmd.Parameters.AddWithValue("@des", product.Description);
                cmd.ExecuteNonQuery();
            }
            cnn.Close();
        }
    }
}
