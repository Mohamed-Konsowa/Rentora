using Rentora.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentora.Application.IRepositories
{
    public interface IEmailRepository
    {
        Task<bool> AddOtp(OTP otp);

    }
}
