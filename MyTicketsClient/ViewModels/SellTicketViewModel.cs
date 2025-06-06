﻿using System;
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
using Microsoft.Maui.Graphics;
using ZXing;
using ZXing.Common;
using System.Diagnostics.Contracts;
//using Javax.Xml.Transform;
using MyTicketsClient.Models;
using System.Collections.ObjectModel;
//using Java.Security;

namespace MyTicketsClient.ViewModels
{
    public class SellTicketViewModel : ViewModelBase
    {

        private MyTicketServerClientApi proxy;
        private string _statusMessage;
        private FileResult _selectedFile;

        private Team homeTeam;
        public Team HomeTeam
        {
            get => homeTeam;
            set
            {
                homeTeam = value;
                OnPropertyChanged();
            }
        }

        private Team awayTeam;
        public Team AwayTeam
        {
            get => awayTeam;
            set
            {
                awayTeam = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Team> teams;
        public ObservableCollection<Team> Teams { get => teams; set { teams = value; OnPropertyChanged(); } }

        private Team selectedHomeTeam;
        public Team SelectedHomeTeam { get => selectedHomeTeam; set { selectedHomeTeam = value; OnPropertyChanged(); } }

        private Team selectedAwayTeam;
        public Team SelectedAwayTeam { get => selectedAwayTeam; set { selectedAwayTeam = value; OnPropertyChanged(); } }



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
        //public Command PickFileCommand => new Command(async () => await PickFileAsync());

        //// Command to trigger file uploading (now saves to local storage)
        //public Command UploadFileCommand => new Command(async () => await UploadFileAsync());

        //public Command PublishFileCommand => new Command(async () => await PublishFileAsync());

       // public Command SellCommand => new Command(async () => await SellTicketAsync());







        //// Function to pick a file using the file picker
        //private async Task PickFileAsync()
        //{
        //    try
        //    {
        //        // Use the FilePicker to allow the user to pick a file
        //        _selectedFile = await FilePicker.PickAsync();

        //        if (_selectedFile != null)
        //        {
        //            StatusMessage = $"File selected: {_selectedFile.FileName}";
        //            var skbitmap = SkiaSharp.SKBitmap.Decode(_selectedFile.FullPath);
        //            var reader = new ZXing.SkiaSharp.BarcodeReader();
        //            // detect and decode the barcode inside the bitmap
        //            var result = reader.Decode(skbitmap);
        //            // do something with the result
        //            if(result != null)
        //            {
        //                var ticketValue = result.Text;
        //            }
        //        }
        //        else
        //        {
        //            StatusMessage = "No file selected.";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        StatusMessage = "Error selecting file: " + ex.Message;
        //    }
        //}



        //// Function to upload the selected file and save it to a local folder
        //private async Task UploadFileAsync()
        //{
        //    if (_selectedFile == null)
        //    {
        //        StatusMessage = "Please select a file first.";
        //        return;
        //    }

        //    try
        //    {
        //        // Read and encode the file to Base64
        //        string base64File = await EncodeFileToBase64Async();

        //        if (base64File != null)
        //        {
        //            // Send the Base64 encoded file to the server
        //            await SendToServer(base64File);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        StatusMessage = "Error uploading file: " + ex.Message;
        //    }
        //}

        //// Function to read the selected file and encode it to Base64
        //private async Task<string> EncodeFileToBase64Async()
        //{
        //    if (_selectedFile == null)
        //    {
        //        StatusMessage = "Please select a file first.";
        //        return null;
        //    }

        //    try
        //    {
        //        // Open the file stream to read the file content
        //        using (var fileStream = await _selectedFile.OpenReadAsync())
        //        {
        //            // Create byte array to store file content
        //            byte[] fileBytes = new byte[fileStream.Length];

        //            // Read the file content into the byte array
        //            await fileStream.ReadAsync(fileBytes, 0, (int)fileStream.Length);

        //            // Encode the byte array to Base64
        //            string base64Encoded = Convert.ToBase64String(fileBytes);

        //            StatusMessage = "File encoded successfully to Base64.";
        //            return base64Encoded;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        StatusMessage = "Error encoding file to Base64: " + ex.Message;
        //        return null;
        //    }
        //}

        //// Function to send the Base64 encoded file to the server
        //private async Task SendToServer(string base64File)
        //{
        //    var client = new HttpClient();
        //    var content = new FormUrlEncodedContent(new[]
        //    {
        //        new KeyValuePair<string, string>("file", base64File)
        //    });

        //    var response = await client.PostAsync("https://yourserver.com/upload", content);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        StatusMessage = "File uploaded successfully!";
        //    }
        //    else
        //    {
        //        StatusMessage = "Error uploading file to server.";
        //    }
        //}

        //// Function to publish the selected file (same as before, but you can adjust it to local publishing if needed)
        //private async Task PublishFileAsync()
        //{
        //    if (_selectedFile == null)
        //    {
        //        StatusMessage = "Please select a file";
        //        return;
        //    }

        //    try
        //    {
        //        // Use HttpClient to publish the file to a server
        //        using (var httpClient = new HttpClient())
        //        {
        //            // Read the file stream into a byte array
        //            using (var fileStream = await _selectedFile.OpenReadAsync())
        //            {
        //                var fileBytes = new byte[fileStream.Length];
        //                await fileStream.ReadAsync(fileBytes, 0, (int)fileStream.Length);

        //                var fileContent = new ByteArrayContent(fileBytes);
        //                fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

        //                var formData = new MultipartFormDataContent();
        //                formData.Add(fileContent, "file", _selectedFile.FileName);

        //                // Replace with your actual API endpoint for publishing
        //                var apiEndpoint = "https://yourserver.com/publish";

        //                var response = await httpClient.PostAsync(apiEndpoint, formData);

        //                if (response.IsSuccessStatusCode)
        //                {
        //                    StatusMessage = "File published successfully!";
        //                }
        //                else
        //                {
        //                    StatusMessage = "File publishing failed. Please try again.";
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        StatusMessage = "Error publishing file: " + ex.Message;
        //        Console.WriteLine(ex);  // Log the exception to see full stack trace
        //    }
        //}

 









        private int ticketPrice { get; set; }
        public int TicketPrice { get => ticketPrice; set { ticketPrice = value;

                OnPropertyChanged("Price"); } }



        private int row { get; set; }
        public int Row
        {
            get => row; set { row = value; OnPropertyChanged("Row"); }



        }

        public Command SellTicketCommand { get; private set; }

        private int seats {  get; set; }
        public int Seats { get => seats; set { seats = value;OnPropertyChanged("Seats"); } }



        public SellTicketViewModel(MyTicketServerClientApi proxy, IServiceProvider serviceProvider)
        {
            this.proxy = proxy;
            this.teams = new ObservableCollection<Team>();
            StatusMessage = "Select a file to upload.";
            PriceError = "you cant sell a ticket for more than its original price";
            GateError = "this gate does not exist";
            PublishTicket = new Command(OnPublish);
            SellTicketCommand = new Command(OnPublish);
            Task.Run(async () => await LoadTeams());
        }

       public async Task LoadTeams()
        {
            var teamsList = await proxy.GetTeams();
            this.Teams = new ObservableCollection<Team>(teamsList);
        }

        public Command PublishTicket {  get; private set; }
        public async void OnPublish()
        {
            ValidateGate();
            ValidatePrice();
            ValidateRow();
            ValidateSeats();    
            if (!ShowGateError && !ShowPriceError && !ShowRowError && !showSeatsError)
            {
                var Ticket = new Ticket(ticketPrice, gateNum, Row, Seats, SelectedHomeTeam.TeamId,SelectedAwayTeam.TeamId);
                var result = await proxy.SellTicket(Ticket);
                await App.Current.MainPage.DisplayAlert("Success", $"Ticket submitted succesfully", "OK");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", $"Error in submitting ticket", "OK");
            }

                //look at onregister + userDTO 

        }

        //ticket validation
        #region ticketvalidation
        private bool showPriceError;
        public bool ShowPriceError { get => showPriceError; set { showPriceError = value; OnPropertyChanged("ShowPriceError"); } }

        private string priceError;
        public string PriceError { get => priceError; set { priceError = value; OnPropertyChanged($"{nameof(PriceError)}"); } }


        private bool showGateError;
        public bool ShowGateError { get => showGateError; set { showGateError = value;OnPropertyChanged("ShowGateError"); } }

        private string gateError;
        public string GateError { get => gateError; set { gateError = value;OnPropertyChanged($"{nameof(GateError)}"); } }

        private bool showSeatsError;
        public bool ShowSeatsError { get => showSeatsError; set { showSeatsError = value; OnPropertyChanged("ShowSeatsError"); } }

        private bool showRowError;
        private bool ShowRowError { get => showRowError; set { showRowError = value; OnPropertyChanged("ShowRowError"); } }

        

        public void ValidatePrice()
        {
            if(price > 50 ||  price < 0)
            {
                this.showPriceError = true;
            }
            else
            {
                this.showPriceError= false;
            }
        }
        public void ValidateGate()
        {
            if(IsGateValid)
            {
                this.ShowGateError = true;
            }
            else
            {
                this.ShowGateError = false;
            }
        }


        private int price;
        public int Price
        {
            get => price;
            set
            {
                price = value;
                priceError = "";
                OnPropertyChanged(nameof(Price));
                if (price < 0 || price > 70)
                {
                    priceError = "price is not valid";
                }
            }
        }
        public void ValidateRow()
        {
            if (row < 1 || row > 16)
            {
                this.ShowRowError = true;
            }
            else
            {
                this.ShowRowError = false;
            }
        }

        public void ValidateSeats()
        {
            if (seats < 1 || seats > 40)
            {
                this.ShowSeatsError = true;
            }
            else
            {
                this.ShowSeatsError = false;
            }
        }

        private int gateNum;
        private bool IsGateValid;


        private string gate { get; set; }
        public string Gate { get => gate; set { gate = value; GateError = "";OnPropertyChanged(nameof(Gate));
 
                if(!int.TryParse(gate,out gateNum))
                {
                    GateError = "Gate is not valid";
                    IsGateValid = false;
                }

            }
           
        }


        private int teamId {  get; set; }
        public int TeamId { get => teamId; set { teamId = value; OnPropertyChanged("TeamId"); } }

        //public async void OnPublish()
        //{
        //    ValidateGate();
        //    ValidatePrice();

        //    if(!ShowGateError && !ShowPriceError)
        //    {
        //        //var ticket = new Ticket(TicketId = ticketId,Price = price, Gate = gate,Row = row,Seats = seats,TeamId = 0);
        //    }
        //}

        #endregion




        //is price valid?
        //showPriceError

        //is gate valid?
        //show gate error

        //put in constructor-example on register vm
















    }
}