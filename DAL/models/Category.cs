namespace Ecommerce.DAL.models
{
    public class Category : Baseentity
    {
        public string name { get; set; }

        public IEnumerable<Product> products { get; set; }



    }
}
