using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EKE.Service.Services.Admin
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
