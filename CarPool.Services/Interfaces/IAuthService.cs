using CarPool.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<bool> ValidateUser(string userEmail, string password);
        public Task<UserEntity> GetClaimDataForUserIdentification(string userEmail);
        public string GetUserIdByToken();
    }
}
