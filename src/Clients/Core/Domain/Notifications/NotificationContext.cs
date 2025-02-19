using Domain.Interfaces;
using FluentValidation.Results;

namespace Domain.Notifications
{
    public class NotificationContext : INotificationContext
    {
        private readonly List<Notification> _notifications;
        public bool HasNotifications => _notifications.Any();
        public IReadOnlyCollection<Notification> Notifications => _notifications;

        public NotificationContext()
        {
            _notifications = new List<Notification>();
        }

        public void AddNotificationMessage(string erro)
        {
            _notifications.Add(new Notification(erro));
        }

        public void AddNotification(Notification notification)
        {
            _notifications.Add(notification);
        }

        public void AddNotificationFromValidationResult(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                AddNotificationMessage(error.ErrorMessage);
            }
        }

        public void CleanNotification()
        {
            _notifications.Clear();
        }
    }
}