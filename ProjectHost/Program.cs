using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using(ServiceHost host=new ServiceHost(typeof(ProductServiceReference.Product)))
            {
                host.Open();
                Console.WriteLine("Host has been started at:" + DateTime.Now.ToString());
                Console.ReadLine();
            }
        }
    }
}
