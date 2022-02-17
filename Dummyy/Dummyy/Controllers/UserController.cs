using Dummyy.Models.DTOs;
using Dummyy.Models.UserViewModels;
using Dummyy.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Dummyy.Controllers
{
    public class UserController: Controller
    {
        private readonly IConfiguration _configuration;
        private UserService userService;
        public UserController(IConfiguration configuration)
        {
            this._configuration = configuration;
            userService = new UserService(configuration);
        }
        #region Get
        [HttpGet("/users/getAll")]
        public IActionResult AllUsers()
        {
            return View(userService.GetAll());
        }
        #endregion Get
        #region Edit 
        [HttpGet("/users/EditUser")]
        public IActionResult EditUser(string id)
        {
            return View(userService.GetOne(id));
        }
        [HttpPost("/users/EditUser")]
        public IActionResult EditUser(UserViewModel user)
        {
            return View(userService.Update(user));
        }
        #endregion Edit 
        #region Create
        [HttpGet("/users/Create")]
        public IActionResult CreateUser()
        {
            return View();
        }
        [HttpPost("/users/Create")]
        public IActionResult CreateUser(CreateUserViewModel model)
        {
            userService.Add(model);
            return RedirectToAction("AllUsers");
        }
        #endregion Create
        #region Delete
        [HttpGet("/users/DeleteUser")]
        public IActionResult DeleteUser(string id)
        {
            userService.Delete(id);
            return RedirectToAction("AllUSers");
        }
        #endregion Delete
    }
}
