using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using TeachMate.Domain;

namespace TeachMate.Services;
public class PaymentService : IPaymentService
{
    private readonly IZaloPayService _zaloPayService;
    private readonly IMomoService _momoService;
    private readonly IVnPayService _vnPayService;
    private readonly DataContext _context;


   

    public PaymentService(IZaloPayService zaloPayService, IMomoService momoService, IVnPayService vnPayService, DataContext context)
    {
        _zaloPayService = zaloPayService;
        _momoService = momoService;
        _vnPayService = vnPayService;
        _context = context;
    }
    public async Task<OrderUrlResponseDto> CreateOrderUrl(double amount, PaymentProviderType type)
    {
        return type switch
        {
            PaymentProviderType.ZaloPay => await _zaloPayService.CreateZaloPayOrder(amount),
            PaymentProviderType.Momo => await _momoService.CreateMomoOrder(amount),
            PaymentProviderType.VnPay => _vnPayService.CreateVnPayOrder(amount),
            _ => throw new NotImplementedException(),
        };
    }
    public async Task<LearningModulePaymentOrder> CreatePaymentOrder(CreateOrderPaymentDto dto)
    {
        var amount = await _context.LearningModules
            .Where(p => p.Id == dto.LearningModuleId)
            .Select(p => p.Price)
            .FirstOrDefaultAsync();
        var learningModule = await _context.LearningModules
            .Where(p => p.Id == dto.LearningModuleId)
            .FirstOrDefaultAsync();
        var learner = await _context.Learners
            .SingleOrDefaultAsync(l => l.Id == dto.LearnerID);

        if (learner == null)
        {
            throw new Exception("Invalid LearnerId");
        }

        var order = new LearningModulePaymentOrder
        {
            Learner = learner,
            LearningModule=learningModule,
            PaymentAmount = amount,
            CreatedAt = DateTime.Now,
            HasClaimed = false,
            PaymentStatus = PaymentStatus.Pending,
        };

        await _context.LearningModulePaymentOrders.AddAsync(order);
        await _context.SaveChangesAsync();
        return order;
    }

    public async Task<ResponseDto> PayForClass(int OrderID) {
        
        var paid = await _context.LearningModulePaymentOrders.Where(p => p.Id == OrderID).FirstOrDefaultAsync();
        paid.PaymentStatus = PaymentStatus.Paid;
        _context.LearningModulePaymentOrders.Update(paid);
       await _context.SaveChangesAsync();
        return new ResponseDto("paid success");
    }
}
