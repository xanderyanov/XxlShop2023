using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using XxlStore.Models;
using XxlStore.Infrastructure;

namespace XxlStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : XxlController
    {
        Domain domain = Data.MainDomain;
        public IActionResult Index()
        {
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
            try
            {
                Id = new ObjectId(id);
            }
            catch
            {
                return NotFound();
            }

            Post post = domain.ExistingPosts.SingleOrDefault(x => x.Id == Id);

            return View("Edit", post);
        }

        [HttpPost]
        public IActionResult CreateOrUpdatePost(Post post)
        {

            if (post.Id == default)
            {
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


            if (!domain.ExistingPosts.Any(x => x.Id == post.Id))
            {
                domain.ExistingPosts.Add(post);
            }
            else
            {
                var mPosts = domain.ExistingPosts;

                int index = mPosts.IndexOf(mPosts.Where(x => x.Id == post.Id).FirstOrDefault());
                mPosts[index] = post;
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeletePost(string Id)
        {
            //передавать на удаление надо Id того типа, который хранится в базе. Мы передаем string, а в базе ObjectId. По этому делаем проверку ObjectId.TryParse(Id, out var postId)

            if (ObjectId.TryParse(Id, out var postId))
            {

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

        public IActionResult UsersList()
        {
            var users = domain.ExistingUsers.OrderBy(x => x.Id).ToList();

            return View("UsersList", users);
        }

        public IActionResult AddUser()
        {
            TUser user = new TUser();

            return View("UserEdit", user);
        }

        public IActionResult UpdateUser(string id)
        {
            ObjectId Id = default;
            try
            {
                Id = new ObjectId(id);
            }
            catch
            {
                return NotFound();
            }

            TUser user = domain.ExistingUsers.SingleOrDefault(x => x.Id == Id);

            return View("UserEdit", user);
        }

        [HttpPost]
        public IActionResult CreateOrUpdateUser(TUser user)
        {

            if (user.Id == default)
            {
                user.Id = ObjectId.GenerateNewId();
            }

            BsonDocument filter = new BsonDocument() {
                { "_id", user.Id }
            };

            if (user.Password != null)
            {
                user.Password = HashPasswordHelper.HashPassword(user.Password);
                Data.usersCollection.ReplaceOne(filter, user, new ReplaceOptions()
                {
                    IsUpsert = true
                });
            }
            else
            {

                var updateSettings = new BsonDocument("$set", new BsonDocument { { "Name", user.Name.ToLower() }, { "Email", user.Email } });
                Data.usersCollection.UpdateOne(filter, updateSettings);
            }


            if (!domain.ExistingUsers.Any(x => x.Id == user.Id))
            {
                domain.ExistingUsers.Add(user);
            }
            else
            {
                var mUsers = domain.ExistingUsers;

                int index = mUsers.IndexOf(mUsers.Where(x => x.Id == user.Id).FirstOrDefault());
                mUsers[index] = user;
            }

            return RedirectToAction("UsersList");
        }

        public IActionResult DeleteUser(string Id)
        {
            if (ObjectId.TryParse(Id, out var userId))
            {

                BsonDocument filter = new BsonDocument() {
                    {
                        "_id", userId
                    }
                };

                Data.usersCollection.DeleteOne(filter);

                domain.ExistingUsers.RemoveAll(x => x.Id == userId);

            }
            return RedirectToAction("UsersList");
        }

    }
}
