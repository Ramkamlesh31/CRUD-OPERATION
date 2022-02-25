namespace Crud_operation.Models
{
    internal class Product
    {
        internal int CatId;

        public int ProductId { get; internal set; }
        public string ProductName { get; internal set; }
        public int ReorderLevel { get; internal set; }
        public decimal Price { get; internal set; }
    }
}