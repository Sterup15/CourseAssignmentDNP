using Repositories;

namespace CLI.UI.ManagePosts;

public class ManagePostsView
{
    private IPostRepository PostRepository { get; set; }
    private CreatePostView CreatePostView { get; set; }
    private ListPostsView ListPostsView { get; set; }
    private SinglePostView SinglePostView { get; set; }
    private DeletePostView DeletePostView { get; set; }
    public ManagePostsView(IPostRepository postRepository)
    {
        PostRepository = postRepository;
        CreatePostView = new CreatePostView(postRepository);
        ListPostsView = new ListPostsView(postRepository);
        SinglePostView = new SinglePostView(postRepository);
        DeletePostView = new DeletePostView(postRepository);
    }

    public async Task HandleAsync(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Please provide a post command (create, list, delete).");
            Console.WriteLine("To create a new post, type 'Create' alongside 'ID' 'title' 'body' and 'userID' in that order");
            Console.WriteLine("To see a list of all posts, type 'list'");
            Console.WriteLine("To delete a post, type 'Delete' alongside 'ID'");
            return;
        }

        switch (args[0].ToLower())
        {
            case "create":
                await CreatePostView.CreatePost(args[1..]);
                break;
            case "list":
                await ListPostsView.ListPosts();
                break;
            case "delete":
                if (args.Length > 1 && int.TryParse(args[1], out var postId))
                {
                    await DeletePostView.DeletePost(postId);
                }
                else
                {
                    Console.WriteLine("Please provide a valid post ID to delete.");
                }
                break;
            default:
                Console.WriteLine("Unknown post command.");
                break;
        }
    }
}