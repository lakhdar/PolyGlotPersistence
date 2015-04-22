namespace ASP.NET.MVC5.Client.Models
{
    using Microsoft.AspNet.Identity;
    using System.Threading.Tasks;
    public class EmailManagementServices : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            return  Task.FromResult<int>(0);
        }
    }
}
