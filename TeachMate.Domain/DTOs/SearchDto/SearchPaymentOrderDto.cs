namespace TeachMate.Domain;

public class SearchPaymentOrderDto
{
    public bool? HasClaimed { get; set; } = null;
    public PaymentStatus? PaymentStatus { get; set; } = null;
}
