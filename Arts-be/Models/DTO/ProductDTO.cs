namespace Arts_be.Models.DTO
{
    public class ProductDTO
    {

        public int productId { get; set; }

        public string? Product_code { get; set; }

        public  int Product_category_id { get; set; }

        public string? Description { get; set; }

        public decimal? Price { get; set; }

        public int Quantity { get; set; }

        public string Title { get; set; }

        public decimal? Weight { get; set; }

        public string? Tag { get; set;}

        public string? Availability { get; set; }

        public decimal? Sale { get; set; }

        public string? SKU    { get; set;}

        public string? Brands { get; set; }

        public string? CommentsBrands { get; set; }
        public string? path { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}
