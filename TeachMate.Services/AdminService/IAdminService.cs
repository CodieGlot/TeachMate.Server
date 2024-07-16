
using TeachMate.Domain;
using TeachMate.Domain.DTOs.SearchDto;

namespace TeachMate.Services;

public interface IAdminService
{
    Task<List<AppUser>> GetAllUser();
    Task<List<AppUser>> SearchUser(SearchUserDto dto);
    Task<AppUser?> UpdateStatus(DisableDto dto);
    Task<Report?> GetReportByID(int id);
    Task<List<Report>> GetAllReportSystem();
    Task<List<Report>> GetAllReportUser();
    Task<List<Report>> SearchReportSystem(SearchReportSystemDto dto);
    Task<List<Report>> SearchReportUser(SearchReportUserDto dto);
    Task<Report?> UpdateStatusReport(UpdateStatusDto dto);
    Task<LearningModulePaymentOrder?> GetPaymentByID(int id);
    Task<List<LearningModulePaymentOrder>> GetAllPaymentOrder();
    Task<List<LearningModulePaymentOrder>> SearchPaymentOrder(SearchPaymentOrderDto dto);
    Task<LearningModulePaymentOrder?> UpdateHasClaimed(HasClaimedDto dto); 
}
