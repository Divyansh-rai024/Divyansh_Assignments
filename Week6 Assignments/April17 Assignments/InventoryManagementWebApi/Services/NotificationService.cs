using System.Collections.Generic;
using System.Linq;

public class NotificationService
{
    private readonly ApplicationDbContext _context;

    public NotificationService(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Notification> GetAll()
    {
        return _context.Notifications.ToList();
    }

    public string Add(Notification notification)
    {
        notification.DateCreated = DateTime.Now;

        _context.Notifications.Add(notification);
        _context.SaveChanges();

        return "Notification Added";
    }
}