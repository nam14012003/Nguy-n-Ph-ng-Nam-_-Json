using NguyenPhuongNam_SE173557;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Product_DAO
{
    public class ProductManagementDAO
    {

        private List<Product> products = new List<Product>();

        public List<Product> GetProducts(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string jsonData = File.ReadAllText(filePath);
                    products = JsonSerializer.Deserialize<List<Product>>(jsonData) ?? new List<Product>();
                }
                else
                {
                    products = new List<Product>();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Notice: " + ex.Message);
            }

            return products;
        }

        public string SearchFile(string filePath, string fileName)
        {
            try
            {
                foreach (var file in Directory.GetFiles(filePath, fileName))
                {
                    return file;  
                }

                foreach (var directory in Directory.GetDirectories(filePath))
                {
                    string result = SearchFile(directory, fileName); 
                    if (!string.IsNullOrEmpty(result))
                        return result;
                }
            }
            catch (UnauthorizedAccessException) { }
            catch (Exception ex)
            {
                throw new Exception("Notice: " + ex.Message);
            }

            return null;  
        }


        public void StoreToFile(string filePath)
        {
            try
            {
                string jsonData = JsonSerializer.Serialize(products, new JsonSerializerOptions
                {
                    WriteIndented = true,
                });
                File.WriteAllText(filePath, jsonData);
            }
            catch (Exception ex)
            {
                throw new Exception("Notice: " + ex.Message);
            }
        }
        public List<Product> GetLoadedProducts()
        {
            return products;
        }


        public bool AddProduct(Product product, string filePath)
        {
            try
            {
                Product existingProduct = products.SingleOrDefault(x => x.ProductId == product.ProductId);
                if (existingProduct != null)
                {
                    return false;
                }

                products.Add(product);
                StoreToFile(filePath);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool UpdateProduct(Product product, string filePath)
        {
            try
            {
                Product existingProduct = products.SingleOrDefault(p => p.ProductId == product.ProductId);
                if (existingProduct == null)
                {
                    return false;
                }

                existingProduct.ProductName = product.ProductName;
                existingProduct.Quantity = product.Quantity;
                existingProduct.Price = product.Price;
                existingProduct.Description = product.Description;

                StoreToFile(filePath);
                return true;
            }
            catch (Exception ex)
            {
               
                return false;
            }
        }

        public bool DeleteProduct(int productId, string filePath)
        {
            try
            {
                Product existingProduct = products.SingleOrDefault(x => x.ProductId == productId);
                if (existingProduct == null)
                {
                    return false;
                }

                products.Remove(existingProduct);
                StoreToFile(filePath);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
