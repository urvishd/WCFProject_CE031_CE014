using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace ShopManagementClient
{
    public partial class Homepage : System.Web.UI.Page
    {

        //KeyValuePair<int, int>[] d1;
        public List<KeyValuePair<int, int>> list;
        static List<KeyValuePair<int, int>> lst1 = new List<KeyValuePair<int, int>>();
        static int[] arr1 = new int[10000];
        static int total;

        protected void Page_Load(object sender, EventArgs e)
        {
           

            if (!IsPostBack)
            {
             
                ListBox1.Items.Clear();
                string connetionString;
                SqlConnection cnn;
                DataTable dt;
                SqlDataAdapter da;
                DataSet ds;

                connetionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ShopDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                cnn = new SqlConnection(connetionString);



                cnn.Open();
                SqlCommand command = new SqlCommand("select * from Products", cnn);
                da = new SqlDataAdapter(command);
                ds = new DataSet();
                da.Fill(ds, "ProductTable");
                cnn.Close();

                dt = ds.Tables["ProductTable"];
                int i;

                for (i = 0; i < dt.Rows.Count; i++)
                {
                    string itm="ID:" + dt.Rows[i].ItemArray[0].ToString() + "   " + dt.Rows[i].ItemArray[1].ToString() + "  -₹" + dt.Rows[i].ItemArray[2].ToString() + "  -" + dt.Rows[i].ItemArray[3].ToString() + "Pcs.";
                    ListItem li = new ListItem();
                    li.Text = itm;
                    arr1[(int)dt.Rows[i].ItemArray[0]] = (int)dt.Rows[i].ItemArray[3];
                    li.Value = dt.Rows[i].ItemArray[0].ToString();
                    li.Attributes.Add("id", dt.Rows[i].ItemArray[0].ToString());
                    li.Attributes.Add("name", dt.Rows[i].ItemArray[1].ToString());
                    li.Attributes.Add("qty", dt.Rows[i].ItemArray[3].ToString());
                    ListBox1.Items.Add(li);
                }
            }
            
            
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ProductService.IproductsClient client = new ProductService.IproductsClient();
            Label5.Text = "";
            
            if (TextBox1.Text=="" || !System.Text.RegularExpressions.Regex.IsMatch(TextBox1.Text, "^[0-9]*$"))
            {
                Label1.Text = "Please enter valid Product id!";
                Label1.BackColor = System.Drawing.Color.Red;
            }
            else
            {
                int id = Convert.ToInt32(TextBox1.Text);
                ProductService.Product p = client.GetProduct(id);
                TextBox2.Text = p.ProductName;
                TextBox3.Text = p.ProductPrice.ToString();
                TextBox4.Text = p.Quantity.ToString();
                TextBox5.Text = p.Description;
                Label1.Text = "Product fetched!";
                Label1.BackColor = System.Drawing.Color.Green;
            }
           
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            ProductService.IproductsClient client = new ProductService.IproductsClient();
            ProductService.Product p = new ProductService.Product();

            string t1 = TextBox1.Text;
            string t2 = TextBox2.Text;
            string t3 = TextBox3.Text;
            string t4 = TextBox4.Text;
            string t5 = TextBox5.Text;

            if(t1=="" || (t2=="" ||(t3=="" ||( t4==""|| t5==""))))
            {
                Label1.Text = "Please fill All details!!";
                Label1.BackColor = System.Drawing.Color.Red;
            }
            else
            {
                p.ProductId = Convert.ToInt32(t1);
                p.ProductName = t2;
                p.ProductPrice = Convert.ToInt32(t3);
                p.Quantity = Convert.ToInt32(t4);
                p.Description = t5;

                client.UpdateProductDetails(p);
                Label1.Text = "Product Updated!!";
                Label1.BackColor = System.Drawing.Color.Green;
            }
            
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            ProductService.IproductsClient client = new ProductService.IproductsClient();
            ProductService.Product p = new ProductService.Product();

            string t1 = TextBox1.Text;
            string t2 = TextBox2.Text;
            string t3 = TextBox3.Text;
            string t4 = TextBox4.Text;
            string t5 = TextBox5.Text;

            if (t2 == "" || (t3 == "" || (t4 == "" || t5 == "")))
            {
                Label1.Text = "Please fill all Product details!!";
                Label1.BackColor = System.Drawing.Color.Red;
            }
            else
            {
                if(t1!="")
                {
                    p.ProductId = Convert.ToInt32(t1);
                }
                else{
                    p.ProductId = -1;
                }
               
                
                p.ProductName = t2;
                p.ProductPrice = Convert.ToInt32(t3);
                p.Quantity = Convert.ToInt32(t4);
                p.Description = t5;

                string status=client.AddProduct(p);
                Label1.Text =status;
                Label1.BackColor = System.Drawing.Color.Green;
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            

            if (!IsPostBack)
            {
                //d1 = new KeyValuePair<int, int>[20];
                Label3.Text = "";
                total = 0;
            }
            
            if(!System.Text.RegularExpressions.Regex.IsMatch(TextBox6.Text, "^[0-9]*$") || TextBox6.Text=="")
            {
                Label3.Text = "Please enter valid Quantity!";
                Label3.BackColor = System.Drawing.Color.Red;
            }
           
            else
            {
                int q =Convert.ToInt32(TextBox6.Text);
                int id1 = Convert.ToInt32(ListBox1.SelectedItem.Value);
                
                if (arr1[id1]<q)
                {
                    Label3.Text = "Quantity not available as you want!";
                    Label3.BackColor = System.Drawing.Color.Red;
                    return;
                }
                lst1.Add(new KeyValuePair<int, int>(Convert.ToInt32(ListBox1.SelectedItem.Value), Convert.ToInt32(TextBox6.Text)));
                //list =lst1;
                ListItem itm = ListBox1.SelectedItem;
                
                //Label3.Text = ListBox1.SelectedItem.Value;
                ListBox1.Items.Remove(ListBox1.SelectedItem);

                /*itm.Text = ListBox1.SelectedItem.Text;
                int p = Convert.ToInt32(ListBox1.SelectedItem.Value);
                itm.Value =(p*q).ToString();*/
                ProductService.IproductsClient client = new ProductService.IproductsClient();
                
                ProductService.Product p = client.GetProduct(id1);
                total += q * p.ProductPrice;



                ListBox2.Items.Add(itm);
                ListBox2.ClearSelection();
                Label3.Text = " ";
                Label5.Text = total.ToString();
                foreach (var element in lst1)
                {
                    Label3.Text += element;
                }
               
            }

        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            
            ListItem itm = ListBox2.SelectedItem;
            Label4.Text = " ";
           

            KeyValuePair<int, int> r = lst1.SingleOrDefault(x => x.Key == Convert.ToInt32(itm.Value));

            //Label3.Text = r.Key.ToString();
            ProductService.IproductsClient client = new ProductService.IproductsClient();
            
            ProductService.Product p = client.GetProduct(Convert.ToInt32(itm.Value));
            total-= r.Value * p.ProductPrice;
            Label5.Text = total.ToString();
            lst1.Remove(r);
            Label4.Text = "";
            foreach (var element in lst1)
            {
                Label4.Text += element;
            }
            ListBox2.Items.Remove(ListBox2.SelectedItem);
            ListBox1.Items.Add(itm);
            ListBox1.ClearSelection();
           
            
           

        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            ProductService.IproductsClient client = new ProductService.IproductsClient();
            ProductService.Bill b = new ProductService.Bill();
            b.date = DateTime.Today;
            b.total = total;
            int id2=client.StoreBill(b);

            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ShopDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            cnn = new SqlConnection(connetionString);
            cnn.Open();

            foreach (var itm1 in lst1)
            {
                int p = client.GetProductPrice(itm1.Key);
                ProductService.Product p1 = client.GetProduct(itm1.Key);
                ProductService.Product p2=new ProductService.Product();
                p2.ProductId = p1.ProductId;
                p2.ProductName = p1.ProductName;
                p2.ProductPrice = p2.ProductPrice;
                p2.Description = p1.Description;
                p2.Quantity = itm1.Value;
                client.UpdateProductDetails(p2);
                
                var sql = "INSERT INTO History(BillNo,Productid,Price,Quantity,Total) VALUES(@billno,@pid,@pprice,@qun,@tot)";
                using (var cmd = new SqlCommand(sql, cnn))
                {
                    cmd.Parameters.AddWithValue("@billno", id2);
                    cmd.Parameters.AddWithValue("@pid", itm1.Key);
                    cmd.Parameters.AddWithValue("@pprice", p);
                    cmd.Parameters.AddWithValue("@qun", itm1.Value);
                    cmd.Parameters.AddWithValue("@tot", p*itm1.Value);
                    
                    cmd.ExecuteNonQuery();
                    
                }
                

            }

            
            cnn.Close();




            Label4.Text = "Wohoo! Purchase is successful!!! and Your Invoice number is:";
            Label4.Text += id2.ToString();
            total = 0;
            Label5.Text = total.ToString();
            foreach(ListItem itm3 in ListBox2.Items)
            {
                ListBox1.Items.Add(itm3);
            }
            ListBox2.Items.Clear();
            lst1.Clear();
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id3 =Convert.ToInt32(GridView1.SelectedRow.Cells[0].Text);
            


        }
    }
}