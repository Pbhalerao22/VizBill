namespace VizBill.Models
{
    public class BillViewModel
    {
        public decimal Amt { get; set; }
        public string Mode { get; set; }
        public List<CartItem> Cart { get; set; }
    }

    public class CartItem
    {
 
        public string name { get; set; }
        public decimal price { get; set; }
        public int qty { get; set; }
        public string itemid { get; set; }
    }
}
