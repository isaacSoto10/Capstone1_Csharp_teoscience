
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using HobbyCenter.Models;


namespace HobbyCenter.Controllers
{
    public class HomeController : Controller
    {   
        private MyContext DbContext;

        public HomeController (MyContext context)
        {
        DbContext = context;
        }
        
        [HttpGet("")]
        public IActionResult Index()
        {
            int? session = HttpContext.Session.GetInt32("UserId");
            if(session == null)
            {
                return View("Login"); 
                
            }
            return RedirectToAction("Dashboard");   
        }
        [HttpGet("signup")]
        public IActionResult signup()
        {
            int? session = HttpContext.Session.GetInt32("UserId");
            if(session == null)
            {
                return View("Signup"); 
                
            }
            return RedirectToAction("Dashboard");   
        }
        


        [HttpPost("Register")]
        public IActionResult Register(User user) 
        {
            if(ModelState.IsValid)
            {
                if(DbContext.Users.Any(i => i.UserName == user.UserName))
                {
                    ModelState.AddModelError("UserName", "Username is already in Use! Try Another one");
                    return View("Signup");
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                user.Password = Hasher.HashPassword(user, user.Password);
                DbContext.Users.Add(user);
                DbContext.SaveChanges();
                HttpContext.Session.SetInt32("userId", user.UserId);
                return RedirectToAction("Dashboard");
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            return View("Signup");
        }

        [HttpPost("Login")]
        public IActionResult Login(Login person)
        {
            if(ModelState.IsValid)
            {
                User oneUser = DbContext.Users.FirstOrDefault(u => u.UserName == person.UserLogin);
                if (oneUser == null)
                {
                    ModelState.AddModelError("UserName", "User Name has not found been found.");
                    return View("Login");
                }
                var hasher = new PasswordHasher<Login>();
                var result = hasher.VerifyHashedPassword(person, oneUser.Password, person.PasswordLogin);
                if(result == 0)
                {
                    ModelState.AddModelError("Password", "Password typed didnt work. Please try again.");
                    return View("Login");
                }
                HttpContext.Session.SetInt32("UserId", oneUser.UserId);
                return RedirectToAction("Dashboard");
            }
            else
            {
                return View("Login");
            }
        }

        [HttpGet("Dashboard")]
        public IActionResult Dashboard()
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");

            if(UserId == null)
            {
                return RedirectToAction("Index");
            }

            List<Hobby> AllHobbys = DbContext.Hobbys
            .ToList();
            

            User user = DbContext.Users.FirstOrDefault(i => i.UserId == UserId);
            return View(AllHobbys);
        }

        [HttpGet("New")]
        public IActionResult New()
        {
            return View("New");
        }
        [HttpGet("NewTag")]
        public IActionResult NewTag()
        {
            return View("NewTag");
        }
        
        [HttpGet("tag/add/{hobbyId}")]
        public IActionResult NewTagToPost(int hobbyId)
        {
            // check that hobby existing
            List<Tag> AllTags = DbContext.Tags.ToList(); 
            ViewBag.hobbyId = hobbyId;

            return View(AllTags);
        }

        [HttpPost("/tag/add")]
        public IActionResult CreatePostHasTags(int postId, int tagId)
        {
                // check that the postID is valid
                // check that tagID is valid
                //  create a new PostHasTagsId - Set tagid and the postid
                // _context.Add(newPostHasTags);///////////////////////////
                // _context.SaveUpdates();
                    Tag Tag = DbContext.Tags.FirstOrDefault(u => u.TagId == tagId);
                    Hobby post = DbContext.Hobbys.FirstOrDefault(u => u.HobbyId == postId);
                    if (post == null || Tag == null)
                        {
                            ModelState.AddModelError("HobbyId", "ID wasnt found");
                            return RedirectToAction("Index");
                        }
                    DbContext.Add(new PostHasTags(){
                        TagId = tagId,
                        HobbyId = postId
                    });
                    DbContext.SaveChanges();
                    int? UserId = HttpContext.Session.GetInt32("UserId");
                    return RedirectToAction("Dashboard");
        }



        [HttpPost("CreateTag")]
        public IActionResult CreateTag(Tag formData)
        {
            if(ModelState.IsValid){
            DbContext.Add(formData);
            DbContext.SaveChanges();
            return RedirectToAction("Dashboard");
            }
            return View("NewTag", formData);
        }        
        [HttpPost("Create")]
        public IActionResult Create(Hobby formData)
        {
            if(ModelState.IsValid){
            formData.UserId=(int)HttpContext.Session.GetInt32("UserId");
            DbContext.Add(formData);
            DbContext.SaveChanges();
            return RedirectToAction("Dashboard");
            }
            return View("New", formData);
        }
        [HttpGet("tags/{id}")]
        public ActionResult tags(int id)
        {
            Tag RetrievedTag = DbContext.Tags
                .Include(tags => tags.Posts)
                    .ThenInclude(allPosts => allPosts.Hobby)
                .SingleOrDefault(tag => tag.TagId == id);
            return View("tags", RetrievedTag);
        }

[HttpGet("Details/{id}")]
        public IActionResult Detail(int id)
        {   
            Hobby RetrievedHobby = DbContext.Hobbys
                .Include(hobby => hobby.Tags)
                    .ThenInclude(allTags => allTags.Tag)
                .SingleOrDefault(hobby => hobby.HobbyId == id);
            return View("Details", RetrievedHobby);
        }

        public IActionResult Privacy()
        {
            return View();
        }

         
    }
}