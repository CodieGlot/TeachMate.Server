using TeachMate.Domain;

namespace TeachMate.Services;
public interface IPaymentService
{
    Task<ResponseDto> TestMomo();
    Task<ResponseDto> TestZaloPay();
}