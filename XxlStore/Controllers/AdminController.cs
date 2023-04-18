using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using XxlStore.Models;

namespace XxlStore.Controllers
{
    public class AdminController : BaseController
    {
        public IActionResult Index()
        {
            Domain domain = Data.MainDomain;

            var posts = domain.ExistingPosts.OrderByDescending(x => x.CreatedDate).ToList();

            return View("BlogPostsList", posts);
        }

        public IActionResult Create()
        {
            Post model = new Post();
            model.CreatedDate = DateTime.Now;
            
            return View("Edit", model);
        }

        public IActionResult Update(string id)
        {
            ObjectId Id = default;
            try {
                Id = new ObjectId(id);
            }
            catch {
                return NotFound();
            }

            Domain domain = Data.MainDomain;


            Post post = domain.ExistingPosts.SingleOrDefault(x => x.Id == Id);

            return View("Edit", post);
        }

        [HttpPost]
        public IActionResult CreateOrUpdatePost(Post post)
        {

            if (post.Id == default) {
                post.Id = ObjectId.GenerateNewId();
            }
            BsonDocument filter = new BsonDocument() {
                {
                    "_id", post.Id
                }
            };

            Data.blogCollection.ReplaceOne(filter, post, new ReplaceOptions()
            {
                IsUpsert = true
            });


            if (!Data.MainDomain.ExistingPosts.Any(x => x.Id == post.Id)) {
                Data.MainDomain.ExistingPosts.Add(post);
            } else {
                var mPosts = Data.MainDomain.ExistingPosts;

                int index = mPosts.IndexOf(mPosts.Where(x => x.Id == post.Id).FirstOrDefault());
                mPosts[index] = post;
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeletePost(string Id)
        {
            //передавать на удаление надо Id того типа, который хранится в базе. Мы передаем string, а в базе ObjectId. По этому делаем проверку ObjectId.TryParse(Id, out var postId)

            if (ObjectId.TryParse(Id, out var postId)) {

                BsonDocument filter = new BsonDocument() {
                    {
                        "_id", postId
                    }
                };

                Data.blogCollection.DeleteOne(filter);

                //Data.LoadObjects();      //метод удаления поста - переинициализация всех объектов из базы (в потенциале - затратно)

                Domain domain = Data.MainDomain;

                domain.ExistingPosts.RemoveAll(x => x.Id == postId);    // удаление всех постов с Id == postId // как-то сомнительно

                //var itemToRemove = domain.ExistingPosts.SingleOrDefault(x => x.Id == postId);   //  больно код большой, по сравнению с предыдущим примером
                //if (itemToRemove != null)
                //    domain.ExistingPosts.Remove(itemToRemove);
            }
            return RedirectToAction("Index");
        }
    }
}
