using Repositories;

namespace CLI.UI.ManageUsers;

public class ManageUsersView
{
    private IUserRepository UserRepository { get; set; }
    private CreateUserView createUserView { get; set; }
    private DeleteUserView deleteUserView { get; set; }
    private ListUsersView listUsersView { get; set; }

    public ManageUsersView(IUserRepository userRepository)
    {
        UserRepository = userRepository;
        createUserView = new CreateUserView(userRepository);
        deleteUserView = new DeleteUserView(userRepository);
        listUsersView = new ListUsersView(userRepository);
    }
    
    public async Task HandleAsync(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Please provide a user command (create, list, delete).");
            Console.WriteLine("To create a new user, type 'Create' alongside 'ID' 'username' and 'password' in that order");
            Console.WriteLine("To see a list of all users, type 'list'");
            Console.WriteLine("To delete a user, type 'Delete' alongside 'ID'");
            return;
        }

        switch (args[0].ToLower())
        {
            case "create":
                await createUserView.CreateUser(args[1..]);
                break;
            case "list":
                await listUsersView.ListUsers();
                break;
            case "delete":
                if (args.Length > 1 && int.TryParse(args[1], out var userId))
                {
                    await deleteUserView.DeleteUserAsync(userId);
                }
                else
                {
                    Console.WriteLine("Please provide a valid user ID to delete.");
                }
                break;
            default:
                Console.WriteLine("Unknown user command.");
                break;
        }
    }
}