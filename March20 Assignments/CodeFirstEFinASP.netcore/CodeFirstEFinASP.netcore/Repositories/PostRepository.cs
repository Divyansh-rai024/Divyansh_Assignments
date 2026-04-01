using CodeFirstEFinASP.netcore.Models;

namespace CodeFirstEFinASP.netcore.Repositories
{
    public class PostRepository : IPost
    {
        public EventContext context;

        public void postRepository(EventContext cnt)
        {
            this.context = cnt;
        }
        public void DeletePost(int postid)
        {
            Post post = context.posts.Find(postid);
            context.posts.Remove(post);
        }

        public Post GetPostByID(int postid)
        {
            throw new NotImplementedException();
        }

        public List<Post> GetPosts()
        {
            throw new NotImplementedException();
        }

        public void InsertPost(Post post)
        {
            context.posts.Add(post);
        }

        public void save()
        {
            context.SaveChanges();
        }

        public void UpdatePost(Post post)
        {
            context.Entry(post).State =Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
