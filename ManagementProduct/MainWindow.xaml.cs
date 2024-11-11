using Microsoft.Win32;
using NguyenPhuongNam_SE173557;
using Product_Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace ManagementProduct
{
    public partial class MainWindow : Window
    {
        private IProductReadFileRepository productReadFileRepository;
      

        public MainWindow()
        {
            InitializeComponent();
            productReadFileRepository = new ProductReadFileRepository();
        }

        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtProductId.Clear();
            txtProductName.Clear();
            txtPrice.Clear();
            txtQuantity.Clear();
            txtDescription.Clear();
        }

        private string GetFilePath()
        {
            string fileName = txtFile.Text.Trim();
            if (string.IsNullOrEmpty(fileName))
            {
                MessageBox.Show("Nhập tên file vào đường dẫn.");
                return null;
            }

            if (!File.Exists(fileName))
            {
                MessageBox.Show("File không tồn tại.");
                return null;
            }

            if (Path.GetExtension(fileName)?.ToLower() != ".json")
            {
                MessageBox.Show("Cung cấp đúng dạng JSON file.");
                return null;
            }
            return fileName;
        }


        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            string fileName = GetFilePath();
            if (fileName == null) return;
            try
            {
                List<Product> listData = productReadFileRepository.GetProducts(fileName);
                dtgProducts.ItemsSource = listData;
                Window_Loaded(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading products: " + ex.Message);
            }
        }

        private Product GetProductFromInput()
        {
            if (!int.TryParse(txtProductId.Text, out int productId) ||
                !int.TryParse(txtQuantity.Text, out int quantity) ||
                !decimal.TryParse(txtPrice.Text, out decimal price))
            {
                MessageBox.Show("Nhập đầy đủ thông tin.");
                return null;
            }

            return new Product
            {
                ProductId = productId,
                ProductName = txtProductName.Text,
                Quantity = quantity,
                Price = price,
                Description = txtDescription.Text
            };
        }


        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string filePath = GetFilePath();
            if (filePath == null) return;

            var product = GetProductFromInput();
            if (product == null) return;

            bool isAdded = productReadFileRepository.AddProduct(product, filePath);
            MessageBox.Show(isAdded ? "Thêm sản phẩm thành công." : "Thêm sản phẩm thất bại");
            btnLoad_Click(sender, e);
            Window_Loaded(sender, e);
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            string filePath = GetFilePath();
            if (filePath == null) return;

            var product = GetProductFromInput();
            if (product == null) return;

            bool isUpdated = productReadFileRepository.UpdateProduct(product, filePath);
            MessageBox.Show(isUpdated ? "Sản phẩm cập nhập." : "Sản phẩm cập nhập thất bại.");
            btnLoad_Click(sender, e);
            Window_Loaded(sender, e);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtProductId.Text, out int productId))
            {
                return;
            }

            string filePath = GetFilePath();
            if (filePath == null) return;

            var result = MessageBox.Show("Bạn có muốn xóa sản phẩm này không?",
                                         "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                bool isDeleted = productReadFileRepository.DeleteProduct(productId, filePath);
                MessageBox.Show(isDeleted ? "Sản phẩm đã được xóa." : "Không thể tìm được sản phẩm.");
                btnLoad_Click(sender, e);
                Window_Loaded(sender, e);
            }
        }

        private void dtgProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtgProducts.SelectedItem is Product selectedProduct)
            {
                txtProductId.Text = selectedProduct.ProductId.ToString();
                txtProductName.Text = selectedProduct.ProductName;
                txtPrice.Text = selectedProduct.Price.ToString();
                txtQuantity.Text = selectedProduct.Quantity.ToString();
                txtDescription.Text = selectedProduct.Description;

            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "JSON Files (*.json)|*.json",
                Title = "Chọn đúng định dạng file"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                txtFile.Text = openFileDialog.FileName;
            }
        }

        private void btnFindFile_Click(object sender, RoutedEventArgs e)
        {
            string fileName = txtFile.Text.Trim();

            if (!string.IsNullOrEmpty(fileName))
            {
                string filePath = productReadFileRepository.SearchFile(@"D:\", fileName);

                if (!string.IsNullOrEmpty(filePath))
                {
                    txtFile.Text = filePath;  
                }
                else
                {
                    MessageBox.Show("Không tìm thấy file trong ổ D.");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập tên file.");
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            dtgProducts.ItemsSource = null;
            Window_Loaded(sender, e);
        }
    }
}
