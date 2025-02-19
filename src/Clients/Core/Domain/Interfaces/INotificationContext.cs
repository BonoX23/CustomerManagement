using Domain.Notifications;
using FluentValidation.Results;

namespace Domain.Interfaces
{
    public interface INotificationContext
    {
        IReadOnlyCollection<Notification> Notifications { get; }
        bool HasNotifications { get; }
        void CleanNotification();
        void AddNotificationMessage(string erro);
        void AddNotification(Notification notification);
        void AddNotificationFromValidationResult(ValidationResult validationResult);
    }
}
