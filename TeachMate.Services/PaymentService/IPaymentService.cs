using TeachMate.Domain;


namespace TeachMate.Services;
public interface IPaymentService
{
    Task<OrderUrlResponseDto> CreateOrderUrl(double amount, PaymentProviderType type);

     Task<LearningModulePaymentOrder> CreatePaymentOrder(CreateOrderPaymentDto dto);
    Task<ResponseDto> PayForClass(int ModuleID);
    Task<List<LearningModulePaymentOrder>> GetAllPaymentOrder(Guid LearnerId);
    Task<List<LearningModulePaymentOrder>> GetAllPaymentOrderByModuleID(int moduleID);
    Task<LearningModulePaymentOrder> GetAllPaymentOrderUnpaidByModuleIdByLearner(int moduleID, Guid LearnerId);
    Task<List<LearningModulePaymentOrder>> GetAllPaymentOrderByModuleIdByLearner(int moduleID, Guid LearnerId);
}