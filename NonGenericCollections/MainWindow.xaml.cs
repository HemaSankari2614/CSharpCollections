using System.Collections;
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

namespace NonGenericCollections
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ArrayList employees = new ArrayList();
        public MainWindow()
        {
            InitializeComponent();
            RefreshList();
        }
            private void Add_Click(object sender, RoutedEventArgs e)
            {
                string idText = txtId.Text.Trim();
                string name = txtName.Text.Trim();
                string salaryText = txtSalary.Text.Trim();

                if (string.IsNullOrWhiteSpace(idText) ||
                    string.IsNullOrWhiteSpace(name) ||
                    string.IsNullOrWhiteSpace(salaryText))
                {
                    MessageBox.Show("Please fill in all fields.");
                    return;
                }

                if (!int.TryParse(idText, out int id) || id <= 0)
                {
                    MessageBox.Show("Enter a valid positive Employee ID.");
                    return;
                }

                if (!decimal.TryParse(salaryText, out decimal salary) ||
                    salary < 0)
                {
                    MessageBox.Show("Enter a valid salary.");
                    return;
                }

                foreach (Employee emp in employees)
                {
                    if (emp.Id == id)
                    {
                        MessageBox.Show("Employee ID already exists.");
                        return;
                    }
                }

                Employee employee = new Employee
                {
                    Id = id,
                    Name = name,
                    Salary = salary
                };

                employees.Add(employee);

                RefreshList();

                txtId.Clear();
                txtName.Clear();
                txtSalary.Clear();

                MessageBox.Show("Employee added successfully!");
            }

            private void Remove_Click(object sender, RoutedEventArgs e)
            {
                Employee selectedEmployee =
                    lstEmployees.SelectedItem as Employee;

                if (selectedEmployee == null)
                {
                    MessageBox.Show("Please select an employee to remove.");
                    return;
                }

                employees.Remove(selectedEmployee);

                RefreshList();

                MessageBox.Show("Employee removed successfully!");
            }

            private void Clear_Click(object sender, RoutedEventArgs e)
            {
                employees.Clear();
                RefreshList();

                txtId.Clear();
                txtName.Clear();
                txtSalary.Clear();
            }

            private void RefreshList()
            {
                lstEmployees.Items.Clear();

                foreach (Employee emp in employees)
                {
                    lstEmployees.Items.Add(emp);
                }

                txtCount.Text = "Total Records: " + employees.Count;
            }
        }

        public class Employee
        {
            public int Id { get; set; }

            public string Name { get; set; } = string.Empty;

            public decimal Salary { get; set; }

            public override string ToString()
            {
                return "ID: " + Id +
                       " | Name: " + Name +
                       " | Salary: " + Salary.ToString("N2");
            }
        }
}

