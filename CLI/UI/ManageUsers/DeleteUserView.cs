using DomainEntities;
using Repositories;

namespace CLI.UI.ManageUsers;

public class DeleteUserView
{
    private IUserRepository UserRepository { get; set; }

    public DeleteUserView(IUserRepository userRepository)
    {
        UserRepository = userRepository;
    }

    public async Task DeleteUserAsync(int userId)
    {
        UserRepository.DeleteAsync(userId);
    }
}