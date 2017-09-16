using System;
using Zzz.Core.Contracts.Repositories;
using Zzz.Core.Contracts.Services;
using Zzz.Core.Models;

namespace Zzz.Core.Services.Login
{
    public class LoginService : ILoginService
    {
        private readonly IPasswordRepository _passwordRepository;
        const string _cMasterSecretName = "masterkey";
        const int _cMaxNumberOfAttempts = 3;
        const int _cMaxNumberOfAttemptsBeforeLockedOut = 10;

        /// <summary>Initializes a new instance of the <see cref="LoginService"/> class.</summary>
        public LoginService(IPasswordRepository passwordRepository) // e.g. LoginService(IMyApiClient client)
        {
            _passwordRepository = passwordRepository;
        }

        /// <summary>
        /// Gets a value indicating whether the user is authenticated.
        /// </summary>
        public bool IsAuthenticated { get; private set; }

        /// <summary>Gets the error message.</summary>
        /// <value>The error message.</value>
        public string ErrorMessage { get; private set; }

        /// <summary>
        /// Attempts to log the user in using stored credentials if present
        /// </summary>
        /// <returns> <see langword="true"/> if the login is successful, false otherwise </returns>
        public bool Login()
        {
            // get the stored username from previous sessions
            // var username = Settings.UserName;
            // var username = _settingsService.GetValue<string>(Constants.UserNameKey);

            // force return of false just for demo purposes
            IsAuthenticated = false;
            return IsAuthenticated;
        }

        /// <summary>The login method to retrieve OAuth2 access tokens from an API. </summary>
        /// <param name="userName">The user Name (email address) </param>
        /// <param name="password">The users <paramref name="password"/>. </param>
        /// <param name="scope">The required scopes. </param>
        /// <returns>The <see cref="bool"/>. </returns>
        public bool Login(string userName, string password, string scope)
        {
            try
            {
                //IsAuthenticated = _apiClient.ExchangeUserCredentialsForTokens(userName, password, scope);
                return IsAuthenticated;
            }
            catch (ArgumentException argex)
            {
                ErrorMessage = argex.Message;
                IsAuthenticated = false;
                return IsAuthenticated;
            }
        }

        /// <summary>
        /// Logins the specified user name.
        /// </summary>
        /// <param name="password">The users password.</param>
        /// <returns></returns>
        public bool Login(string password)
        {
            bool result = false;

            MasterSecret masterSecret = _passwordRepository.GetMasterSecret(_cMasterSecretName);

            if (password == masterSecret.Password)
            {
                result = true;
            }
            else
            {
                if (masterSecret.NumberOfAttemptsLeft > 0)
                {
                    masterSecret.NumberOfAttemptsLeft -= masterSecret.NumberOfAttemptsLeft;
                }
                else
                {
                    masterSecret.NumberOfAttemptsLeft = _cMaxNumberOfAttempts;
                }
            }

            return result;
        }

        /// <summary>
        /// Set the master password.
        /// </summary>
        /// <param name="password">The master password.</param>
        public void SetMasterPassword(string password)
        {
            MasterSecret masterSecret = new MasterSecret()
            {
                Name = _cMasterSecretName,
                Password = password,
                NumberOfAttemptsLeft = _cMaxNumberOfAttempts,
                LastAttempt = DateTime.Now,
                NumberOfAttemptsLeftBeforeLockout = _cMaxNumberOfAttemptsBeforeLockedOut
            };

            _passwordRepository.SaveMasterSecret(masterSecret);
        }

        public bool IsFirstTime()
        {
            bool result = ! _passwordRepository.HasMasterSecret(_cMasterSecretName);

            return result;
        }
    }
}
