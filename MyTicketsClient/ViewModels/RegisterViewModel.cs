using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Input;
using MyTicketsClient.ViewModels;
using MyTicketsClient.Services;


namespace MyTicketsClient.ViewModels
{
    class RegisterViewModel : ViewModelBase
    {
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



        public ICommand RegistrationCommand
        {
            get; private set;
        }



    }












}

