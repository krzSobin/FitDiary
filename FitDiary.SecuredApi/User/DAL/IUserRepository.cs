using FitDiary.Contracts.DTOs.User;
using FitDiary.SecuredApi.User.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitDiary.SecuredApi.User.DAL
{
    public interface IUserRepository : IDisposable
    {
        ApplicationUser GetUserData(int userId);
    }
}