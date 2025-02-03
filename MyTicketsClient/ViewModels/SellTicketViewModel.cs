using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyTicketsClient.Views;

namespace MyTicketsClient.ViewModels
{
    public class SellTicketViewModel:ViewModelBase
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

        // Command to trigger file uploading
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

        // Function to upload the selected file
        private async Task UploadFileAsync()
        {
            if (_selectedFile == null)
            {
                StatusMessage = "Please select a file first.";
                return;
            }

            try
            {
                // Use HttpClient to upload the file to a server
                using (var httpClient = new HttpClient())
                {
                    // Read the file stream into a byte array
                    using (var fileStream = await _selectedFile.OpenReadAsync())
                    {
                        var fileBytes = new byte[fileStream.Length];
                        await fileStream.ReadAsync(fileBytes, 0, (int)fileStream.Length);

                        var fileContent = new ByteArrayContent(fileBytes);

                        // Set the content headers
                        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

                        // Create the multipart form data content
                        var formData = new MultipartFormDataContent();
                        formData.Add(fileContent, "file", _selectedFile.FileName);

                        // Replace with your actual API endpoint
                        var apiEndpoint = "https://yourserver.com/upload";

                        var response = await httpClient.PostAsync(apiEndpoint, formData);

                        if (response.IsSuccessStatusCode)
                        {
                            StatusMessage = "File uploaded successfully!";
                        }
                        else
                        {
                            StatusMessage = "File upload failed. Try again.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                StatusMessage = "Error uploading file: " + ex.Message;
            }
        }

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

                        // Set the content headers
                        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

                        // Create the multipart form data content
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

    }
}
