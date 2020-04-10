using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Forms.Xaml;

namespace XamarinTimesheet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkAssignmentPage : ContentPage
    {
        public WorkAssignmentPage()
        {
            InitializeComponent();
            assignmentList.ItemsSource = new string[] { " " };
        }

        private async void LoadWorkAssignments(object sender, EventArgs e)
        {

            assignmentList.ItemsSource = new string[] { "Ladataan...", "Loading... "};

            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://timesheetrestapi.azurewebsites.net/");
                string json = await client.GetStringAsync("/api/workassignments/");
                string[] assignments = JsonConvert.DeserializeObject<string[]>(json);

                assignmentList.ItemsSource = assignments;
            }
            catch (Exception ex) 
            {
                string errorMessage = ex.GetType().Name + ": " + ex.Message;
                assignmentList.ItemsSource = new string[] { errorMessage };
            }
        }
    }
}