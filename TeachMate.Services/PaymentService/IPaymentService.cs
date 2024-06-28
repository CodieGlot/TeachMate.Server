using TeachMate.Domain;


namespace TeachMate.Services;
public interface IPaymentService
{
    Task<OrderUrlResponseDto> CreateOrderUrl(double amount, PaymentProviderType type);

    Task<LearningModule> SetPriceForLearningModule(SetPriceForLearningModuleDto dto);

     Task<LearningModulePaymentOrder> CreatePaymentOrder(CreateOrderPaymentDto dto);
    Task<ResponseDto> PayForClass(int OrderID);


}