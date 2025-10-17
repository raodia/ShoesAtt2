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

namespace ShoesAtt2
{
    /// <summary>
    /// Логика взаимодействия для ProductsPage.xaml
    /// </summary>
    public partial class ProductsPage : Page
    {
        public ProductsPage()
        {
            InitializeComponent();

            var currentData = Shavaleev_shoesEntities.GetContext().Products.ToList();
            
            FilterCB.SelectedIndex = 0;
            Listv.ItemsSource = currentData;
            
            
        }

        public void update()
        {

            var currentData = Shavaleev_shoesEntities.GetContext().Products.ToList();

            var textWords = SearchTB.Text.Split(',');


            for (int i = 0; i < textWords.Length; i++)
            {
                while (textWords[i].StartsWith(" ") && textWords[i].Length > 1)
                {
                    textWords[i] = textWords[i].Substring(1);
                }

                while (textWords[i].EndsWith(" ") && textWords[i].Length > 1)
                {
                    textWords[i] = textWords[i].Trim();
                }

               currentData = currentData.Where(a => (
               a.ProductsTitle.ToLower().Contains(textWords[i].ToLower())
               || a.ProductsSupplier.ToLower().Contains(textWords[i].ToLower())
               || a.ProductsProducer.ToLower().Contains(textWords[i].ToLower())
               || a.ProductsDescription.ToLower().Contains(textWords[i].ToLower())
               || a.ProductsCategory.ToLower().Contains(textWords[i].ToLower())
               || a.ProductsArticle.ToLower().Contains(textWords[i].ToLower())
               || a.ProductsName.ToLower().Contains(textWords[i].ToLower())
               )).ToList();

            }

            var startData = currentData;
            
            if (sortDesc.IsChecked == true)
            {
                currentData = currentData.OrderByDescending(a => a.ProductsCountOnInventory).ToList();
                startData = currentData;
            }
            else if (sortAsc.IsChecked == true)
            {
                currentData = currentData.OrderBy(a => a.ProductsCountOnInventory).ToList();
                startData = currentData;
            }

            switch (FilterCB.SelectedIndex)
            {
                case 0:
                    currentData = startData;
                    break;
                case 1:
                    currentData = currentData.Where(a => a.ProductsSupplier == "Kari").ToList();
                    break;
                case 2:
                    currentData = currentData.Where(a => a.ProductsSupplier == "Обувь для вас").ToList();
                    break;
            }

            

            Listv.ItemsSource = currentData;
        }

        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            update();
        }

        private void sortDefault_Checked(object sender, RoutedEventArgs e)
        {
            update();
        }

        private void sortDesc_Checked(object sender, RoutedEventArgs e)
        {
            update();

        }

        private void sortAsc_Checked(object sender, RoutedEventArgs e)
        {
            update();

        }

        private void FilterCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            update();
        }
    }

}
