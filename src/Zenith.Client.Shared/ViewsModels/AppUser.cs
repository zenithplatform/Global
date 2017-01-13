using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenith.Client.Shared.ViewsModels
{
    public class AppUser
    {
        public long UserId { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string AccessToken { get; set; }
    }
}
