namespace WpfClient.Utilities
{
    class VehicleSearch
    {
        public string? SearchTerm { get; set; }
        public string? Color { get; set; }
        public string? Version { get; set; }
        public string? Year { get; set; } 
        public int MaxPrice { get; set; }
        public int? MinPrice { get; set; }

        public VehicleSearch() { }

        public VehicleSearch(string? color, string? version, string? year, int maxPrice, int minPrice)
        {
            Color = color;
            Version = version;
            Year = year;
            MaxPrice = maxPrice;
            MinPrice = minPrice;
        }
    }
}
