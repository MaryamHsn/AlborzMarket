using Alborz.DataLayer.Context;
using Alborz.DomainLayer.DTO;
using Alborz.ServiceLayer.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Alborz.ServiceLayer.Service
{
    public class PostService : IPostService
    {
        IUnitOfWork _uow;
        DateTime _now;
        public PostService(IUnitOfWork uow)
        {
            _now = DateTime.Now;
            _uow = uow;
        }
        public void AddNewPost(PostTbl Post)
        {
            _uow.PostRepository.Add(Post);
            _uow.SaveAllChanges();
        }
        public IList<PostTbl> GetAllPosts()
        {
            return _uow.PostRepository.GetAll().ToList();
        }
        public PostTbl GetPost(int? id)
        {
            return _uow.PostRepository.GetAll(x => x.Id == id).SingleOrDefault();
        }
        public bool Delete(int id)
        {
            PostTbl Post = _uow.PostRepository.Get(id);
            var t = _uow.PostRepository.SoftDelete(Post);
            _uow.SaveAllChanges();
            return t;
        }
        ////Async 
        public async Task AddNewPostAsync(PostTbl Post, CancellationToken ct = new CancellationToken())
        {
            await _uow.PostRepository.AddAsync(Post, ct);
            _uow.SaveAllChanges();
        }
        public async Task<IList<PostTbl>> GetAllPostsAsync(CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.PostRepository.GetAllAsync(ct);
            //return obj.Select(PropertyKeyMapper.Map).Where(x => x.IsActive == true).ToList();
            return obj.ToList();
        }
        public async Task<PostTbl> GetPostAsync(int? id, CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.PostRepository.GetAllAsync(x => x.Id == id);
            return obj.FirstOrDefault();
        }
        public async Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken())
        {
            var Post = await _uow.PostRepository.GetAsync(id, ct);
            var obj = await _uow.PostRepository.SoftDeleteAsync(Post);
            _uow.SaveAllChanges();
            return obj;
        }
    }
}
