using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net.Http;
using Newtonsoft.Json;

namespace XamarinTimesheet
{
    
    [DesignTimeVisible(false)]
    public partial class EmployeePage : ContentPage
    {
        public EmployeePage()
        {
            InitializeComponent();

            employeeList.ItemsSource = new object[] { "Kello Kalle -sovellus", "Androidille", "Työn iloa!" };    
        }

        private async void LoadEmployees(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://timesheetrestapi.azurewebsites.net/");
            string json = await client.GetStringAsync("/api/employees/");
            object[] employees = JsonConvert.DeserializeObject<object[]>(json);

            employeeList.ItemsSource = employees;

        }

        private void LoadAssignmentPage(object sender, EventArgs e)
        {

            string employee = employeeList.SelectedItem.ToString();
            Navigation.PushAsync(new WorkAssignmentPage());
        }
    }
}
