using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyTicketsClient.Views;
using MyTicketsClient.Services;
using MyTicketsClient.ViewModels;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
//using Java.Security;

namespace MyTicketsClient.ViewModels
{
    public class SellTicketViewModel : ViewModelBase
    {

        private string _statusMessage;
        private FileResult _selectedFile;

        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                _statusMessage = value;
                OnPropertyChanged();
            }
        }

        private string maxTicketPrice;
        public string MaxTicketPrice
        {
            get => maxTicketPrice;
            set
            {
                maxTicketPrice = value;
                OnPropertyChanged();
            }
        }

        // Command to trigger file picking
        public Command PickFileCommand => new Command(async () => await PickFileAsync());

        // Command to trigger file uploading (now saves to local storage)
        public Command UploadFileCommand => new Command(async () => await UploadFileAsync());

        public Command PublishFileCommand => new Command(async () => await PublishFileAsync());

        public SellTicketViewModel()
        {
            StatusMessage = "Select a file to upload.";
        }

        // Function to pick a file using the file picker
        private async Task PickFileAsync()
        {
            try
            {
                // Use the FilePicker to allow the user to pick a file
                _selectedFile = await FilePicker.PickAsync();

                if (_selectedFile != null)
                {
                    StatusMessage = $"File selected: {_selectedFile.FileName}";
                }
                else
                {
                    StatusMessage = "No file selected.";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = "Error selecting file: " + ex.Message;
            }
        }

        // Function to upload the selected file and save it to a local folder
        private async Task UploadFileAsync()
        {
            if (_selectedFile == null)
            {
                StatusMessage = "Please select a file first.";
                return;
            }

            try
            {
                // Set the path where you want to save the file within your project (adjust path as necessary)
                string localDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UploadedFiles");
                Directory.CreateDirectory(localDirectory); // Create directory if it doesn't exist

                // Define the full file path to save the file
                string filePath = Path.Combine(localDirectory, _selectedFile.FileName);

                // Open the file stream and save it to the local directory
                using (var fileStream = await _selectedFile.OpenReadAsync())
                {
                    using (var localFileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    {
                        await fileStream.CopyToAsync(localFileStream); // Copy the content to the new file
                    }
                }

                StatusMessage = $"File saved successfully at {filePath}";
            }
            catch (Exception ex)
            {
                StatusMessage = "Error saving file: " + ex.Message;
            }
        }

        // Function to publish the selected file (same as before, but you can adjust it to local publishing if needed)
        private async Task PublishFileAsync()
        {
            if (_selectedFile == null)
            {
                StatusMessage = "Please select a file";
                return;
            }

            try
            {
                // Use HttpClient to publish the file to a server
                using (var httpClient = new HttpClient())
                {
                    // Read the file stream into a byte array
                    using (var fileStream = await _selectedFile.OpenReadAsync())
                    {
                        var fileBytes = new byte[fileStream.Length];
                        await fileStream.ReadAsync(fileBytes, 0, (int)fileStream.Length);

                        var fileContent = new ByteArrayContent(fileBytes);
                        fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                        var formData = new MultipartFormDataContent();
                        formData.Add(fileContent, "file", _selectedFile.FileName);

                        // Replace with your actual API endpoint for publishing
                        var apiEndpoint = "https://yourserver.com/publish";

                        var response = await httpClient.PostAsync(apiEndpoint, formData);

                        if (response.IsSuccessStatusCode)
                        {
                            StatusMessage = "File published successfully!";
                        }
                        else
                        {
                            StatusMessage = "File publishing failed. Please try again.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                StatusMessage = "Error publishing file: " + ex.Message;
                Console.WriteLine(ex);  // Log the exception to see full stack trace
            }
        }


        private int ticketPrice {  get; set; }
        public int TicketPrice { get => ticketPrice; set {  ticketPrice = value; OnPropertyChanged("Price"); } }
         


        private int row {  get; set; }
        public int Row
        {
            get => row; set { row = value; OnPropertyChanged("Row"); }



        }

        private int gate {  get; set; }
        public int Gate { get => gate;set { gate = value; OnPropertyChanged("Gate"); } }









    }
}