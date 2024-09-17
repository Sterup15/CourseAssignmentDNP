using DomainEntities;
using Repositories;

namespace CLI.UI.ManageUsers;

public class CreateUserView
{
    private IUserRepository UserRepository { get; set; }

    public CreateUserView(IUserRepository userRepository)
    {
        UserRepository = userRepository;
    }

    public async Task CreateUser(string[] args)
    {
        UserRepository.AddAsync(new User(Convert.ToInt32(args[0]), args[1], args[2]));
    }
}