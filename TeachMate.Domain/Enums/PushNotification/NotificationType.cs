using System.ComponentModel;

namespace TeachMate.Domain;
public enum NotificationType
{
    Custom = 0,
    [Description("NotificationMessage_NewLearnerJoined")]
    NewLearnerJoined = 1,
    [Description("NotificationMessage_NewLearningRequest")]
    NewLearningRequest = 2,
    [Description("NotificationMessage_LearningRequestAccepted")]
    LearningRequestAccepted = 3,
    [Description("NotificationMessage_LearningRequestDeclined")]
    LearningRequestBeenDeclined = 4,
}
