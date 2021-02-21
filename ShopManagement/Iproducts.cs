using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ShopManagement
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "Iproducts" in both code and config file together.
    [ServiceContract]
    public interface Iproducts
    {
        [OperationContract]
        string AddProduct(Product product);

        List<Product> GetAllProduct();

        [OperationContract]
        void UpdateProductDetails(Product product);

        [OperationContract]
        Product GetProduct(int id);

        [OperationContract]
        int StoreBill(Bill b);

        [OperationContract]
        int GetProductPrice(int id);

        
    }

    [DataContract]
    public class Product
    {
        [DataMember]
        public int ProductId { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public int ProductPrice { get; set; }

        [DataMember]
        public int Quantity { get; set; }

        [DataMember]
        public string Description { get; set; }
    }

    public class Bill
    {
        public int id { get; set; }

        public DateTime date { get; set; }

        public int total { get; set; }

    }

    public class History
    {
        public int BillNo { get; set; }

        public int productid { get; set; }

        public int Price { get; set; }

        public int Quantity { get; set; }

        public int Total { get; set; }
    }

}
