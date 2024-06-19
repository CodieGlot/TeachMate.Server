
using TeachMate.Domain;

namespace TeachMate.Services;

public interface IMomoService
{
    Task<OrderUrlResponseDto> CreateMomoOrder(double amount);
}