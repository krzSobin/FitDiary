using FitDiary.SecuredApi.User.Models;
using System;

namespace FitDiary.SecuredApi.User.DAL
{
    public interface IUserRepository : IDisposable
    {
        ApplicationUser GetUserData(int userId);
    }
}