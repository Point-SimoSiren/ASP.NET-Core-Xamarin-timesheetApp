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

            employeeList.ItemsSource = new object[] { "Huoltovarma Oy", "Työn iloa!" };    
        }

        /* Huom. Async metodeissa on käytettävä await sanaa.
        Async mahdollistaa asynkronisen metodin, joka ei keskeytä ohjelman suoritusta vastauksen odottamisen ajaksi, vaan voi tehdä
        muita tehtäviä sillä aikaa.*/


        //-------------Tapahtumankäsittelijä----Lataa työntekijät--------------------------------------

        private async void LoadEmployees(object sender, EventArgs e) 
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://timesheetrestapi.azurewebsites.net/");
            string json = await client.GetStringAsync("/api/employees/");
            object[] employees = JsonConvert.DeserializeObject<object[]>(json);

            employeeList.ItemsSource = employees;

        }

        //-------------tapahtuman käsittelijä-----valittu työntekijä otetaan talteen ja navigoidaan työtehtävät sivulle--------

        private async void LoadAssignmentPage(object sender, EventArgs e) 
        {

            string employee = employeeList.SelectedItem?.ToString(); //kysymysmerkkioperaattori mahdollistaa toString funktion vaikka arvo puuttuisi.
            if (string.IsNullOrEmpty(employee))
            {
               await DisplayAlert("Valinta puuttuu", "Valitse työntekijä.", "OK"); // (otsikko, teksti, kuittausnapin teksti)
            }
            else
            {
                await Navigation.PushAsync(new WorkAssignmentPage()); // Navigoidaan uudelle sivulle
            }
        }        // kts. App.xaml.cs että on navigointi on määritetty kuten tässä projektissa.
    }
}
