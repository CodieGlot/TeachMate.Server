namespace TeachMate.Domain;
public class UpdateRequestStatusDto
{
    public RequestStatus Status { get; set; } = RequestStatus.Waiting;
    public int LearningModuleId { get; set; }

    public int LearningRequestId { get; set; }
}
