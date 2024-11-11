using NguyenPhuongNam_SE173557;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Product_Repository
{
    public interface IProductReadFileRepository
    {
        public List<Product> GetProducts(string filePath);

        public void StoreToFile(string filePath);

        public List<Product> GetLoadedProducts();

        public bool AddProduct(Product product, string filePath);

        public bool UpdateProduct(Product product, string filePath);

        public bool DeleteProduct(int productId, string filePath);

        public string SearchFile(string filePath, string fileName);

    }
}
