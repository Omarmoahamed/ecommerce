namespace Ecommerce.DAL.models
{
    public class Shoppingcart : Baseentity
    {
        public Guid cartguid { get; set; }
        public IEnumerable<Shopping_cart_item> cartItems { get; set; }

        public int totalquantity { get; set; }
        public decimal Totalcost { get; set; }

        public Shoppingcart() 
        {
            this.cartguid = Guid.NewGuid();
        }
    }
}
