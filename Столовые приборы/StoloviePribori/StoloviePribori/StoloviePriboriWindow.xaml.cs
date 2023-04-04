using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace StoloviePribori
{
    /// <summary>
    /// Interaction logic for StoloviePriboriWindow.xaml
    /// </summary>
    public partial class StoloviePriboriWindow : Window
    {
        private Base.Product selectedItem;
        private Base.User user;
        private bool justChange = true;
        public StoloviePriboriWindow(Base.User user)
        {
            InitializeComponent();
            this.user = user;
            if(user != null)
            {
                if(user.UserRole == 1)
                {
                    EditProduct.Visibility = Visibility.Visible;
                    AddProduct.Visibility = Visibility.Visible;
                }
                userNameLabel.Content = user.UserSurname + " " + user.UserName + " " + user.UserPatronymic;
            }
            else
            {
                userNameLabel.Content = "Вы вошли как Гость!";
            }
            loadProducts();
        }

        private void BackToAuth_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }

        private void loadProducts()
        {
            List<Base.Product> products = SourceCore.entities1.Product.ToList();
            List<ProductForList> productForLists = new List<ProductForList>();
            for (int i = 0; i < products.Count; i++)
            {
                ImageSource source = new BitmapImage(new Uri("/Images/picture.png", UriKind.RelativeOrAbsolute));
                productForLists.Add(new ProductForList()
                {
                    Background = products[i].ProductQuantityInStock == 0 ? (Brush)FindResource("Color9") : (Brush)FindResource("Color2"),
                    ProductPhoto = source,
                    ProductId = products[i].ProductArticleNumber,
                    ProductDescription = products[i].ProductDescription,
                    ProductManufacturer = products[i].ProductManufacturer,
                    ProductCost = products[i].ProductCost.ToString(),
                    ProductQuantityInStock = products[i].ProductQuantityInStock.ToString()
                });
            }
            productsListBox.ItemsSource = productForLists;
        }

        private void EditProduct_Click(object sender, RoutedEventArgs e)
        {
            ProductForList selectedItemTest = (ProductForList)productsListBox.SelectedItem;
            if (selectedItemTest == null)
            {
                MessageBox.Show("Выберите объект");
                return;
            }
            selectedItem = SourceCore.entities1.Product.First(i => i.ProductArticleNumber == selectedItemTest.ProductId);
            ProductNameTextBox.Text = selectedItem.ProductName;
            ProductDescriptionTextBox.Text = selectedItem.ProductDescription;
            ProductCategoryTextBox.Text = selectedItem.ProductCategory;
            ProductManufactureTextBox.Text = selectedItem.ProductManufacturer;
            ProductCostTextBox.Text = selectedItem.ProductCost.ToString();
            ProductDiscountAmountTextBox.Text = selectedItem.ProductDiscountAmount.ToString();
            ProductQuantityInStockTextBox.Text = selectedItem.ProductQuantityInStock.ToString();
            ProductStatusTextBox.Text = selectedItem.ProductStatus;
            actionName.Content = "Редактирование товара";
            ActionColumn.Width = new GridLength(400);
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            if (justChange)
            {
                var product = new Base.Product();
                product = SourceCore.entities1.Product.First(i => i.ProductArticleNumber == selectedItem.ProductArticleNumber);
                product.ProductName = ProductNameTextBox.Text;
                product.ProductDescription = ProductDescriptionTextBox.Text;
                product.ProductCategory = ProductCategoryTextBox.Text;
                product.ProductManufacturer = ProductManufactureTextBox.Text;
                product.ProductCost = Decimal.Parse(ProductCostTextBox.Text);
                product.ProductDiscountAmount = Byte.Parse(ProductDiscountAmountTextBox.Text);
                product.ProductQuantityInStock = int.Parse(ProductQuantityInStockTextBox.Text);
                product.ProductStatus = ProductStatusTextBox.Text;
            }
            else
            {
                var product = new Base.Product();
                product.ProductName = ProductNameTextBox.Text;
                product.ProductDescription = ProductDescriptionTextBox.Text;
                product.ProductCategory = ProductCategoryTextBox.Text;
                product.ProductManufacturer = ProductManufactureTextBox.Text;
                product.ProductCost = Decimal.Parse(ProductCostTextBox.Text);
                product.ProductDiscountAmount = Byte.Parse(ProductDiscountAmountTextBox.Text);
                product.ProductQuantityInStock = int.Parse(ProductQuantityInStockTextBox.Text);
                product.ProductStatus = ProductStatusTextBox.Text;
                SourceCore.entities1.Product.Add(product);
            }


            try
            {
                SourceCore.entities1.SaveChanges();
                ActionColumn.Width = new GridLength(0);
                loadProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Cance_Click(object sender, RoutedEventArgs e)
        {
            ActionColumn.Width = new GridLength(0);
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            actionName.Content = "Добавление товара";
            ActionColumn.Width = new GridLength(400);
            justChange = false;
        }

    }
}
