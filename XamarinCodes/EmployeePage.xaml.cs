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
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class EmployeePage : ContentPage
    {
        public EmployeePage()
        {
            InitializeComponent();

            employeeList.ItemsSource = new string[] { "Janne", "Hanna", "Raimo" };
            
        }


        private async void LoadEmployees(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://timesheetrestapi.azurewebsites.net/");
            string json = await client.GetStringAsync("/api/employees/");
            string[] employees = JsonConvert.DeserializeObject<string[]>(json);

            employeeList.ItemsSource = employees;

        }
    }
}
