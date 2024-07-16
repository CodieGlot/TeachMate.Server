using TeachMate.Domain;
using TeachMate.Domain.Models.Payment;
using TeachMate.Services.Migrations;


namespace TeachMate.Services;
public interface IPaymentService
{
    Task<OrderUrlResponseDto> CreateOrderUrl(double amount, PaymentProviderType type, string tick);

    Task<LearningModule> SetPriceForLearningModule(SetPriceForLearningModuleDto dto);

     Task<LearningModulePaymentOrder> CreatePaymentOrder(CreateOrderPaymentDto dto);


    Task<List<LearningModulePaymentOrder>> GetAllPaymentOrder(Guid LearnerId);
    Task<List<LearningModulePaymentOrder>> GetAllPaymentOrderByModuleID(int moduleID);
    Task<LearningModulePaymentOrder> GetAllPaymentOrderUnpaidByModuleIdByLearner(int moduleID, Guid LearnerId);
    Task<List<LearningModulePaymentOrder>> GetAllPaymentOrderByModuleIdByLearner(int moduleID, Guid LearnerId);

    Task<ResponseDto> PayForClass(int OrderID);

    Task<Transaction> CreateTransactionAsync(CreateTransactionDto dto);
    Task<Transaction> UpdateTransactionAsync(UpdateTransactionDto dto);

    Task<bool> CheckPermissionToViewLearningModule(Guid learnerId, int learningModuleId);
    Task<AccountInformation> AddAccountInformation(AddAccountInformationDto dto, Guid tutorID);
    Task<AccountInformation> GetAccountInformationByTutorId(Guid tutorId);
    Task<bool> ExistedAccountInformationByTutorId(Guid tutorId);

}