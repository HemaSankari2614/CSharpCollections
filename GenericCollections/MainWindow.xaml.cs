using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace GenericCollections
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Generic Collection
        private List<Employee> employees = new List<Employee>();
        public MainWindow()
        {
            InitializeComponent();
            RefreshList();
        }
        
            private void Add_Click(object sender, RoutedEventArgs e)
            {
                string idText = txtId.Text.Trim();
                string name = txtName.Text.Trim();

                if (string.IsNullOrWhiteSpace(idText) ||
                    string.IsNullOrWhiteSpace(name))
                {
                    MessageBox.Show("Please enter Employee ID and Name.");
                    return;
                }

                if (!int.TryParse(idText, out int id) || id <= 0)
                {
                    MessageBox.Show("Enter a valid positive Employee ID.");
                    return;
                }

                if (employees.Any(emp => emp.Id == id))
                {
                    MessageBox.Show("Employee ID already exists.");
                    return;
                }

                Employee employee = new Employee
                {
                    Id = id,
                    Name = name
                };

                employees.Add(employee);

                RefreshList();

                txtId.Clear();
                txtName.Clear();

                MessageBox.Show("Employee added successfully!");
            }

            private void Remove_Click(object sender, RoutedEventArgs e)
            {
                if (!int.TryParse(txtId.Text.Trim(), out int id))
                {
                    MessageBox.Show("Enter the Employee ID to remove.");
                    return;
                }

                Employee employee = employees.FirstOrDefault(
                    emp => emp.Id == id);

                if (employee == null)
                {
                    MessageBox.Show("Employee not found.");
                    return;
                }

                employees.Remove(employee);

                RefreshList();

                txtId.Clear();
                txtName.Clear();

                MessageBox.Show("Employee removed successfully!");
            }

            private void Clear_Click(object sender, RoutedEventArgs e)
            {
                employees.Clear();
                RefreshList();

                txtId.Clear();
                txtName.Clear();
            }

            private void RefreshList()
            {
                lvEmployees.ItemsSource = null;
                lvEmployees.ItemsSource = employees;

                txtCount.Text = "Total Employees: " + employees.Count;
            }
        }

        public class Employee
        {
            public int Id { get; set; }

            public string Name { get; set; } = string.Empty;
        }
}


