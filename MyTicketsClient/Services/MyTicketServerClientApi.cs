//using Android.Telecom;
using MyTicketsClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MyTicketsClient.ViewModels;

namespace MyTicketsClient.Services
{
    public class MyTicketServerClientApi
    {

        //מנהל תכונות מתקדמות של בקשות HTTP
        //cookies כמו תמיכה
        private HttpClient client;

        // JSON משתנה זה יכיל את ההגדרות שייקבעו בהמשך כיצד לעבד ולהמיר נתוני
        // בעת שליחת וקבלת בקשות מהשרת
        private JsonSerializerOptions jsonSerializerOptions;

        // כתובת הבסיס לכתובת השרת מותאמת לפי פלטפורמות ההרצה
        public static string BaseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "https://59zm7fqh-5198.uks1.devtunnels.ms/api/TicketServerApi/" : "http://localhost:5198/api/TicketServerApi/";
        public static string ImageUrl = DeviceInfo.Platform == DevicePlatform.Android ? "https://59zm7fqh-5198.uks1.devtunnels.ms/images/" : "http://localhost:5198/images/";

        // אובייקט של מחלקת השירות שמכיל את כתובת הבסיס לשרת
        private string baseUrl;


        //מאפיין זה מחזיק את פרטי המשתמש לאחר התחברות מוצלחת.
        //ניתן להשתמש בו לצורך בדיקה או שליפה של מידע על המשתמש המחובר
        public User LoggedInUser { get; set; }



        public MyTicketServerClientApi()
        {
            // Set up the HTTP client handler to support cookies
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = new System.Net.CookieContainer(); // Initialize a container to store cookies

            // Create a new HttpClient with the handler and enable automatic disposal of the handler
            this.client = new HttpClient(handler, true);

            // Set the base URL for API requests
            this.baseUrl = BaseAddress;

            // Configure JSON serializer options to make JSON formatting more readable and case insensitive
            jsonSerializerOptions = new JsonSerializerOptions()
            {
                WriteIndented = true, // Makes the JSON output more readable (with indents)
                PropertyNameCaseInsensitive = true // Allows deserialization to ignore property name case differences
            };
        }



        public async Task<User> Login(User info)
        {
            // Set the URL for the specific login API function
            string url = $"{this.baseUrl}login";
            try
            {
                // Serialize the login info into a JSON string using the configured options
                string json = JsonSerializer.Serialize(info, jsonSerializerOptions);

                // Create the content to send in the POST request with proper encoding and content type
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                // Send the POST request to the server API
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();

                    User result = JsonSerializer.Deserialize<User>(resContent, jsonSerializerOptions);

                    // Store the logged-in user details for further use
                    this.LoggedInUser = result;

                    // Show a success message to the user
                    await Application.Current.MainPage.DisplayAlert("Login", "Login Succeced", "ok");
                    // Return the logged-in user details
                    return result;
                }
                else
                {
                    // ניתן להוסיף תנאים שיציגו הודעות שגיאה מתאימות לסוגים השונים
                    // טיפול בתקלות שעלולות להתרחש במהלך התחברות
                    await Application.Current.MainPage.DisplayAlert("Login", "Login Faild!", "ok");
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<int?> Register(User user)
        {
            // Set the URL to point to the specific API endpoint for registering a user
            string url = $"{this.baseUrl}register";

            try
            {
                // Serialize the User object into a JSON string to send it to the API
                string json = JsonSerializer.Serialize(user);

                // Create the content to send in the POST request with proper encoding and content type
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                // Send a POST request to the server API with the user data
                HttpResponseMessage response = await client.PostAsync(url, content);

                // Check if the response status indicates success (HTTP status code 200)
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content as a string
                    //string resContent = await response.Content.ReadAsStringAsync();

                    //// Deserialize the response to get the UserId
                    //int? userId = JsonSerializer.Deserialize<int?>(resContent, jsonSerializerOptions);

                    // Return the UserId if it was received successfully
                    //return userId;
                    return 0;
                }
                else
                {
                    // Return null if the response status was not successful
                    return null;
                }
            }
            catch (Exception ex)
            {
                // If there is an exception, return null (indicating the registration failed)
                return null;
            }
        }

        public async Task<User> LoginAsync(LoginInfo userInfo)
        {

            //Set URI to the specific function API
            string url = $"{this.baseUrl}login";
            try
            {
                //Call the server API
                string json = JsonSerializer.Serialize(userInfo);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    User? result = JsonSerializer.Deserialize<User>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }








    }
}
