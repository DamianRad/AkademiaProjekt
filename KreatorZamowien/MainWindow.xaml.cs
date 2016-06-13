using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using System.Xml.Serialization;

namespace KreatorZamowien
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Order> OrderList { get; set; }
        public Settings Settings;
        public MainWindow()
        {
            //Inicjalizaca okna i jego wszystkich komponentów
            InitializeComponent();
            //DataContext okresla skad chcemy Bindowac
            this.DataContext = this;
            this.Settings = new Settings();
            this.WeightTextBox.DataContext=Settings;
            OrderList = new ObservableCollection<Order>();            
            //Pokazuje dane z listy w SetComboBox
            this.SetComboBox.ItemsSource = Enum.GetValues(typeof(Sets));
            this.SetComboBox.SelectedIndex = 0;
            //Show values form list in DrinksComboBox
            this.DrinksComboBox.ItemsSource = Enum.GetValues(typeof(Drinks));
            this.DrinksComboBox.SelectedIndex = 0;

            
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            string Name = this.NameTextBox.Text;
            string NastName = this.LastNameTextBox.Text;
            int TableNumber = TableNumber = int.Parse(this.TableTextbox.Text); ;
            
            Sets Sets = (Sets)Enum.Parse(typeof(Sets), this.SetComboBox.Text);
            Drinks Drinks = (Drinks)Enum.Parse(typeof(Drinks), this.DrinksComboBox.Text);
            Order Order = new Order(Name, NastName, TableNumber, Sets, Drinks);
            OrderList.Add(Order);
        }
       
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            //Usuwanie zaznaczonego elementu w ListView
            try
            { 
                OrderList.RemoveAt(this.ListView1.SelectedIndex);
            }catch(Exception exception)
            {
                MessageBox.Show("First you have to select character if you want to delete it.");
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Order"; // Domyślna nazwa pliku
            dlg.DefaultExt = ".xml";
            dlg.Filter = "XML documents (.xml)|*.xml";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                string filePath = dlg.FileName;
                ListToXmlFile(filePath);
            }
        }

        private void ListToXmlFile(string filePath)
        {
            //Zapisywanie do pliku xml
            using (var sw = new StreamWriter(filePath))
            {
                //Observable collecion tworzy Xml-a
                var serializer = new XmlSerializer(typeof(ObservableCollection<Order>));
                //Serializer uzywajac stream witer-a wie jak sie zserializowac
                serializer.Serialize(sw, OrderList);
            }
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".xml";
            dlg.Filter = "XML documents (.xml)|*.xml";

            Nullable<bool> result = dlg.ShowDialog();
            string filename = "";
            if (result == true)
            {
                filename = dlg.FileName;
            }
            if (File.Exists(filename))
            {
                XmlFileToList(filename);
            }
            else
            {
                MessageBox.Show("Such file doesn\'t exist");
            }
        }

        private void XmlFileToList(string filename)
        {
            using (var sr = new StreamReader(filename))
            {
                var deserializer = new XmlSerializer(typeof(ObservableCollection<Order>));
                ObservableCollection<Order> tmplist = (ObservableCollection<Order>)deserializer.Deserialize(sr);
                foreach(var item in tmplist)
                {
                    OrderList.Add(item);
                }
            }
        }

        private void MinusAmountButton(object sender, RoutedEventArgs e)
        {
            Settings.Amount--;
            /*int tmp = int.Parse(this.WeightTextBox.Text);
            tmp--;
            this.WeightTextBox.Text = tmp.ToString();*/
        }

        private void PlusAmountButton(object sender, RoutedEventArgs e)
        {
            Settings.Amount++;          
            /*int tmp = int.Parse(this.WeightTextBox.Text);
            tmp++;
            this.WeightTextBox.Text = tmp.ToString();*/
        }
    }
}
