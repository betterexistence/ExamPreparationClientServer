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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StoloviePribori.Pages
{
    /// <summary>
    /// Interaction logic for ProductsPage.xaml
    /// </summary>
    public partial class ProductsPage : Page
    {
        private Base.Product selectedItem;
        private Base.User user;
        public ProductsPage(Base.User user)
        {
            InitializeComponent();
            this.user = user;
            if(user != null)
            {
                if (user.Role.RoleID == 1)
                {
                    EditProduct.Visibility = Visibility;
                }
            }
            loadProducts();
        }

        private void loadProducts()
        {
            productsListBox.ItemsSource = SourceCore.entities1.Product.ToList();
        }

        private void EditProduct_Click(object sender, RoutedEventArgs e)
        {
            selectedItem = (Base.Product)productsListBox.SelectedItem;
            if(selectedItem == null)
            {
                MessageBox.Show("Выберите объект");
                return;
            }
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
            Base.Product selectedItem = (Base.Product)productsListBox.SelectedItem;
            var product = new Base.Product();
            product = SourceCore.entities1.Product.First(i => i.ProductArticleNumber == selectedItem.ProductArticleNumber);
            product.ProductName = ProductNameTextBox.Text;
            product.ProductDescription = ProductDescriptionTextBox.Text;
            product.ProductCategory = ProductCategoryTextBox.Text;
            //product.
            product.ProductManufacturer = ProductManufactureTextBox.Text;
            product.ProductCost = Decimal.Parse(ProductCostTextBox.Text);
            product.ProductDiscountAmount = Byte.Parse(ProductDiscountAmountTextBox.Text);
            product.ProductQuantityInStock = int.Parse(ProductQuantityInStockTextBox.Text);
            product.ProductStatus = ProductStatusTextBox.Text;

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
    }
}
