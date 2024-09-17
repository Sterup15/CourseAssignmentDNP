using DomainEntities;
using Repositories;

namespace CLI.UI.ManageUsers;

public class ListUsersView
{
    private IUserRepository UserRepository { get; set; }

    public ListUsersView(IUserRepository userRepository)
    {
        UserRepository = userRepository;
    }

    public async Task<Task<IQueryable<User>>> ListUsers()
    {
        foreach (User user in UserRepository.GetMany())
        {
            Console.WriteLine($"{user.Id}: {user.Username}");
        }

        return Task.FromResult<IQueryable<User>>(null!);
    }
}