using Alborz.DomainLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Alborz.ServiceLayer.IService
{
    public interface IPostService
    {
        void AddNewPost(PostTbl Post);
        IList<PostTbl> GetAllPosts();
        PostTbl GetPost(int? id);
        //  int Delete(int id);
        bool Delete(int id);
        Task AddNewPostAsync(PostTbl Post, CancellationToken ct = new CancellationToken());
        Task<IList<PostTbl>> GetAllPostsAsync(CancellationToken ct = new CancellationToken());
        Task<PostTbl> GetPostAsync(int? id, CancellationToken ct = new CancellationToken());
        Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken());
    }
}
