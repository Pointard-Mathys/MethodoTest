using Notification.Contracts;
using Notification.Model;

namespace Notification.Services;

public class UserService
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;

    public UserService(IUserRepository userRepository, IEmailService emailService)
    {
        _userRepository = userRepository;
        _emailService = emailService;
    }

    public User CreateUser(string name, string email)
    {
        if (_userRepository.Exists(email))
        {
            throw new InvalidOperationException("User already exists");
        }

        var user = new User { Name = name, Email = email };
        _userRepository.Save(user);
        _emailService.SendWelcomeEmail(email, name);

        return user;
    }

    public User GetUser(int id)
    {
        return _userRepository.GetById(id);
    }
   
}