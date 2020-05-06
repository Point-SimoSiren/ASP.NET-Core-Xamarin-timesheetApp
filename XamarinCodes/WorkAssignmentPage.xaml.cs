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
using XamarinTimesheet.Models;
using Xamarin.Essentials; // Tämä tarvitaan koordinaatteja varten

namespace XamarinTimesheet
{
    [DesignTimeVisible(false)]
    public partial class WorkAssignmentPage : ContentPage
    {

        public WorkAssignmentPage()
        {
            InitializeComponent();

            assignmentList.ItemsSource = new string[] { "" };

        }


        //------------------------------------------EVENT----LOAD-WORKS----------------------------------------------

        private async void LoadWorkAssignments(object sender, EventArgs e)
        {

            //Alustus, ennen palvelimen vastauksen saapumista näytetään ilmoitus lataamisesta.
            assignmentList.ItemsSource = new string[] { "Ladataan..." };


            // -------------------Sijainnin haku ja näyttäminen--------
            try
            {
                
                var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                var location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                {
                   
                    longitudeLabel.Text = location.Longitude.ToString();

                    latitudeLabel.Text = location.Latitude.ToString();
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                longitudeLabel.Text = "error";
            }
            catch (FeatureNotEnabledException fneEx)
            {
                longitudeLabel.Text = "error";
            }
            catch (PermissionException pEx)
            {
                longitudeLabel.Text = "error";
            }
            catch (Exception ex)
            {
                longitudeLabel.Text = "error";
            }

            try //----------HTTP pyyntö--------------------
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

        //--------------------------------------EVENT----START-WORK---------------------------------------------

        private async void StartWork(object sender, EventArgs e)
        {
            string assignmentName = assignmentList.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(assignmentName))
            {
                await DisplayAlert("Työ puuttuu", "Valitse ensin aloitettava työtehtävä!", "OK");
            }
            else
            {
                try
                {

                    // Käytetään Xamarin sovellukseen luotua model luokkaa ja perustetaan objekti palvelimelle lähetettäväksi.

                    WorkAssignmentOperationModel data = new WorkAssignmentOperationModel()
                    {
                        Operation = "Start",
                        AssignmentTitle = assignmentName
                    };

                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri("https://timesheetrestapi.azurewebsites.net/");


                    // Muutetaan em. data objekti Jsoniksi
                    string input = JsonConvert.SerializeObject(data);
                    StringContent content = new StringContent(input, Encoding.UTF8, "application/json");


                    // Lähetetään serialisoitu objekti back-endiin Post pyyntönä
                    HttpResponseMessage message = await client.PostAsync("/api/workassignments", content);


                    // Otetaan vastaan palvelimen vastaus
                    string reply = await message.Content.ReadAsStringAsync();


                    //Asetetaan vastaus serialisoituna success muuttujaan
                    bool success = JsonConvert.DeserializeObject<bool>(reply);


                    if (success)  // Näytetään ehdollisesti alert viesti
                    {

                        await DisplayAlert("Työn aloitus", "Työ aloitettu.", "Sulje"); // (otsikko, teksti, kuittausnapin teksti)
                    }
                    else
                    {
                        await DisplayAlert("Aloitus ei onnistu", "Työ on jo käynnissä!", "Sulje"); // Muutettu 4.5.
                    }
                }

                catch (Exception ex) // Otetaan poikkeus ex muuttujaan ja sijoitetaan errorMessageen
                {

                    string errorMessage = ex.GetType().Name + ": " + ex.Message; // Poikkeuksen customoitu selvittäminen ja...

                    assignmentList.ItemsSource = new string[] { errorMessage }; // ..näyttäminen list viewissä
                }
            }
        }

        //-----------------------------EVENT-----STOP-WORK------------------------------------------------------

        private async void StopWork(object sender, EventArgs e)
        {
            {
                string assignmentName = assignmentList.SelectedItem?.ToString();
                if (string.IsNullOrEmpty(assignmentName))
                {
                    await DisplayAlert("Työ puuttuu", "Valitse ensin lopetettava työtehtävä!", "OK");
                }
                else
                {
                    try
                    {

                        // Käytetään Xamarin sovellukseen luotua model luokkaa ja perustetaan objekti.

                        WorkAssignmentOperationModel data = new WorkAssignmentOperationModel()
                        {
                            Operation = "Stop",
                            AssignmentTitle = assignmentName
                        };

                        HttpClient client = new HttpClient();
                        client.BaseAddress = new Uri("https://timesheetrestapi.azurewebsites.net/");

                        // Muutetaan em. objekti Jsoniksi

                        string input = JsonConvert.SerializeObject(data);
                        StringContent content = new StringContent(input, Encoding.UTF8, "application/json");

                        // Lähetetään serialisoitu objekti back-endiin Post pyyntönä
                        HttpResponseMessage message = await client.PostAsync("/api/workassignments", content);

                        // Otetaan vastaan palvelimen vastaus
                        string reply = await message.Content.ReadAsStringAsync();

                        //Asetetaan vastaus serialisoituna success muuttujaan
                        bool success = JsonConvert.DeserializeObject<bool>(reply);


                        if (success)  // Näytetään ehdollisesti alert viesti
                        {

                            await DisplayAlert("Työn lopetus", "Työ lopetettu.", "Sulje"); // (otsikko, teksti, kuittausnapin teksti)

                            assignmentList.ItemsSource = ""; // Muutettu 4.5.
                        }
                        else
                        {
                            await DisplayAlert("Työn lopetus", "Työtä ei voitu lopettaa koska sitä ei ole aloitettu.", "Sulje"); // Muutettu 4.5.
                        }
                    }

                    catch (Exception ex) // Otetaan poikkeus ex muuttujaan ja sijoitetaan errorMessageen
                    {

                        string errorMessage = ex.GetType().Name + ": " + ex.Message; // Poikkeuksen customoitu selvittäminen ja...

                        assignmentList.ItemsSource = new string[] { errorMessage }; // ..näyttäminen list viewissä
                    }
                }
            }
        }
    }
}