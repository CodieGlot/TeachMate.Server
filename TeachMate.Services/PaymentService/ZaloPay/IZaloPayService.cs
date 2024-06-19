using TeachMate.Domain;

public interface IZaloPayService
{
    Task<OrderUrlResponseDto> CreateZaloPayOrder(double amount);
}