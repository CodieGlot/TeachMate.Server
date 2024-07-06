using Microsoft.AspNetCore.Http;
using TeachMate.Domain;

namespace TeachMate.Services;
public interface IVnPayService
{
    OrderUrlResponseDto CreateVnPayOrder(double amount, string tick);
}