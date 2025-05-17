namespace InventryOrderManagementSystem.BLL.Helpers
{
    public static class SKUGenerator
    {
        public static string GenerateSKU(string categoryName, string productName)
        {
            if (string.IsNullOrWhiteSpace(categoryName) || string.IsNullOrWhiteSpace(productName))
                throw new ArgumentException("Category name and product name cannot be empty.");

            // Take first 3 letters of category and product (only letters)
            string categoryPart = new string(categoryName.Where(char.IsLetter).Take(3).ToArray()).ToUpper();
            string productPart = new string(productName.Where(char.IsLetter).Take(3).ToArray()).ToUpper();

            // Generate a unique numeric part (e.g., using timestamp or random)
            string uniquePart = DateTime.UtcNow.ToString("yyMMddHHmmss"); // e.g., 250510145622

            return $"{categoryPart}-{productPart}-{uniquePart}";
        }
    }
}
