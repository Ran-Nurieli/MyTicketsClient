using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Input;
using MyTicketsClient.ViewModels;
using MyTicketsClient.Services;
//using AndroidX.Annotations;
//using Java.Security;
using MyTicketsClient.Models;


namespace MyTicketsClient.ViewModels
{
    class RegisterViewModel : ViewModelBase
    {
        private MyTicketServerClientApi proxy;


        public RegisterViewModel(MyTicketServerClientApi proxy)
        {
            this.proxy = proxy;
            RegisterCommand = new Command(OnRegister);
            CancelCommand = new Command(OnCancel);
            ShowPasswordCommand = new Command(OnShowPassword);
            IsPassword = true;
            PasswordError = "Password must be at least 4 characters long and contain letters and numbers";
            NameError = "Name is required";
            EmailError = "Email is required";
        }

        #region UsernameValidation
        private bool showNameError;

        public bool ShowNameError
        {
            get => showNameError;
            set
            {
                showNameError = value;
                OnPropertyChanged("ShowNameError");
            }
        }

        private string name;

        public string Name
        {
            get => name;
            set
            {
                name = value;
                ValidateName();
                OnPropertyChanged("Name");
            }
        }

        private string nameError;

        public string NameError
        {
            get => nameError;
            set
            {
                nameError = value;
                OnPropertyChanged("NameError");
            }
        }


        private string usererror;
        private string username;

        private string password;
        private string passworderror;
        


        public string Username
        {
            get
            {
                return Username;
            }
            set
            {
                Username = value;
                usererror = ""; // איפוס שגיאת שם המשתמש
                OnPropertyChanged(nameof(Username));
               

                if (!string.IsNullOrEmpty(Username))

                {
                    if (char.IsDigit(Username[0]))
                    {
                        usererror = "!!שם המשתמש לא יכול להתחיל בספרה!!";
                        OnPropertyChanged(nameof(Username));
                    }

                }
            }
        }

        public string UserError
        {
            get
            {
                return UserError;
            }
            set
            {
                UserError = value;
                OnPropertyChanged(nameof(UserError));
            }
        }

        private void ValidateName()
        {
            this.showNameError =  string.IsNullOrEmpty(Username);
        }
        #endregion

        #region PasswordValidation
        public string? Password
        {
            get { return password; }
            set
            {
                password = value;
                PasswordError = "";
                OnPropertyChanged(nameof(Password));
                OnPropertyChanged(nameof(UserError));
                if (string.IsNullOrEmpty(password))
                {
                    PasswordError = ""; // איפוס השגיאה אם השדה ריק
                }
                else
                {
                    if (password != null)
                    {
                        bool IsPasswordOk = IsValidPassword(password);
                        if (!IsPasswordOk)
                        {
                            PasswordError = "!!סיסמה חייבת להכיל לפחות אות גדולה אחת ומספר!!";
                        }
                    }
                }
            }


        }

        public string PasswordError
        {
            get { return passworderror; }
            set
            {
                passworderror = value;
                OnPropertyChanged(nameof(PasswordError));
            }
        }

        private bool showPasswordError;
        public bool ShowPasswordError
        {
            get => showPasswordError;
            set => showPasswordError = value;
            

        }

        private bool IsValidPassword(string password)
        {
            bool hasUpperCase = false;
            bool hasDigit = false;

            foreach (char c in password)
            {
                if (char.IsUpper(c))
                {
                    hasUpperCase = true;
                }
                if (char.IsDigit(c))
                {
                    hasDigit = true;
                }

                if (hasUpperCase && hasDigit)
                {
                    break; // אם מצאנו כבר גם אות גדולה וגם ספרה, אפשר לעצור את הלולאה
                }
            }
            return hasUpperCase && hasDigit;

        }

        public void ValidatePassword()
        {
            if(string.IsNullOrEmpty(password) || password.Length < 4 || !password.Any(char.IsDigit) || !password.Any(char.IsLetter))
            {
                this.ShowPasswordError = true;
            }
            else
            {
                this.showPasswordError = false;
            }
        }
        private bool isPassword = true;
        public bool IsPassword
        {
            get => isPassword;
            set
            {
                isPassword = value;
                OnPropertyChanged("IsPassword");
            }
        }
        //This command will trigger on pressing the password eye icon
        public Command ShowPasswordCommand { get; }
        //This method will be called when the password eye icon is pressed
        public void OnShowPassword()
        {
            //Toggle the password visibility
            IsPassword = !IsPassword;
        }
        #endregion

        #region EmailValidation

        private bool showEmailError;
        public bool ShowEmailError
        {
            get => showEmailError;
            set
            {
                showEmailError = value;
                OnPropertyChanged("ShowEmailError");
            }
        }

        private string email;
        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }


        }

        private string emailError;

        public string EmailError
        {
            get => emailError;
            set
            {
                emailError = value;
                OnPropertyChanged("EmailError");
            }
        }

        private void ValidateEmail()
        {
            this.ShowEmailError = string.IsNullOrEmpty(Email);
            if (!ShowEmailError)
            {
                //check if email is in the correct format using regular expression
                if (!System.Text.RegularExpressions.Regex.IsMatch(Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
                {
                    EmailError = "Email is not valid";
                    ShowEmailError = true;
                }
                else
                {
                    EmailError = "";
                    ShowEmailError = false;
                }
            }
            else
            {
                EmailError = "Email is required";
            }
        }
        #endregion








        public Command CancelCommand { get; private set; }
        public Command RegisterCommand
        {
            get; private set;
        }


        private string gender;

        private int age;


        public async void OnRegister()
        {
           ValidatePassword();
            ValidateName();
            ValidateEmail();    

            if(!ShowNameError && !ShowEmailError && !ShowPasswordError)
            {
  
                //var User = new User { Username = Name, Password = Password, Email = Email, IsAdmin = false, Age = age, Gender = gender };

                

            }




        }

        public void OnCancel()
        {
            //Navigate back to the login page
            ((App)(Application.Current)).MainPage.Navigation.PopAsync();
        }



    }












}

