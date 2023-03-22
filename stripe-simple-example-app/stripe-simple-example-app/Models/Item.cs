namespace stripe_simple_example_app.Models
{
    public class Item
    {
            public string? Id { get; set; }
            public string? Title { get; set; }
            public long Price { get; set; }
            public int Quantity { get; set; }
            public bool IsSelected { get; set; }
    }
}