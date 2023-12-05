using Microsoft.Identity.Web;
using UNUMSelfPwdReset.Models;

namespace UNUMSelfPwdReset
{
    public class LoginsManager
    {
        private readonly IConfiguration _config;
        public LoginsManager(IConfiguration config)
        {
            _config = config;
        }
        public async Task<List<UserLoginClient>> GetUserLogins(string userId, string Username, DateTime? pwdChangedOn,string lanID)
        {
            string days = _config.GetValue<string>("Localinstants:days");
            List<UserLoginClient> loginClients = new List<UserLoginClient>() {
                new UserLoginClient() {
                    LoginType = LoginClientType.LAN
                    , UserLoginId= userId
                    ,Username= Username
                    ,LastSignInAt=DateTime.Now
                    , HasAccess= true
                    , OnPremisesSamAccountName= lanID,
                    Description="ID used to login to the UNUM network.",
                     ExpireInDays= pwdChangedOn.HasValue ? Convert.ToInt16((pwdChangedOn.Value.AddDays(Convert.ToInt32(days)) - DateTime.Now).TotalDays ): null,
            }

            };

            return loginClients;
        }
    }
}
