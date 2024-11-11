using NguyenPhuongNam_SE173557;
using Product_DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product_Repository
{
    public class ProductReadFileRepository : IProductReadFileRepository
    {
        private ProductManagementDAO _productReadFileDAO;

        public ProductReadFileRepository()
        {
            _productReadFileDAO = new ProductManagementDAO();
        }

        public bool AddProduct(Product product, string filePath) => _productReadFileDAO.AddProduct(product, filePath);

        public bool DeleteProduct(int productId, string filePath) => _productReadFileDAO.DeleteProduct(productId, filePath);

        public List<Product> GetLoadedProducts() => _productReadFileDAO.GetLoadedProducts();    

        public List<Product> GetProducts(string filePath) => _productReadFileDAO.GetProducts(filePath);

        public string SearchFile(string filePath, string fileName) => _productReadFileDAO.SearchFile(filePath, fileName);

        public void StoreToFile(string filePath) => _productReadFileDAO.StoreToFile(filePath);

        public bool UpdateProduct(Product product, string filePath) => _productReadFileDAO.UpdateProduct(product, filePath);    


    }
}
