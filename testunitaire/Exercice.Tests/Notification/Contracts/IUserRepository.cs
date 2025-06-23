using Notification.Model;

namespace Notification.Contracts;

public interface IUserRepository
{
    /// <summary>
    /// Retrieves a user by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user to retrieve.</param>
    /// <returns>The user associated with the specified identifier, or null if no user is found.</returns>
    User GetById(int id);

    /// <summary>
    /// Saves the specified user to the repository.
    /// </summary>
    /// <param name="user">The user object to be saved.</param>
    void Save(User user);

    /// <summary>
    /// Checks if a user with the specified email exists in the repository.
    /// </summary>
    /// <param name="email">The email address of the user to check.</param>
    /// <returns>True if a user with the specified email exists; otherwise, false.</returns>
    bool Exists(string email);
}