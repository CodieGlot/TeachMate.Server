using TeachMate.Domain;


namespace TeachMate.Services;
public interface IPaymentService
{
    Task<OrderUrlResponseDto> CreateOrderUrl(double amount, PaymentProviderType type);

     Task<LearningModulePaymentOrder> CreatePaymentOrder(CreateOrderPaymentDto dto);
    Task<ResponseDto> PayForClass(int OrderID);

}