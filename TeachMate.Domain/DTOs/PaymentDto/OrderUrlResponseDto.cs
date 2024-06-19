namespace TeachMate.Domain;
public class OrderUrlResponseDto
{
    public string OrderUrl { get; set; }
    public OrderUrlResponseDto(string orderUrl)
    {
        this.OrderUrl = orderUrl;
    }
}
