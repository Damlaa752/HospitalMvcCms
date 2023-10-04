using App.Data.Entity;
using App.Web.Mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace App.Web.Mvc.Controllers
{
    public class AuthController : Controller
    {
        private readonly HttpClient _httpClient;

        public AuthController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private readonly string _apiAddress = "http://localhost:5005/api/Users";
        private readonly string _apiRoleAddress = "http://localhost:5005/api/Roles";
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        //public async Task<IActionResult> Register(User newUser, string password2)
        //{
        //    try
        //    {
        //        if (newUser.Password == password2)
        //        {
        //            var users = await _httpClient.GetFromJsonAsync<List<User>>(_apiAddress);
        //            var user = users.FirstOrDefault(u => u.Email == newUser.Email);

        //            if (user is not null)
        //                ModelState.AddModelError("", "This Email Has Already Been Registered!");

        //            var roles = await _httpClient.GetFromJsonAsync<List<Role>>(_apiRoleAddress);

        //            var roleFromDatabase = roles.FirstOrDefault(r => r.Id == newUser.RoleId); // newUser.RoleId Her zaman Null olduğu için roleFromDatabase de null oluyor

        //            //KAAA

        //            if (roleFromDatabase == null)
        //            {
        //                roleFromDatabase = new Role
        //                {
        //                    RoleName = "DefaultRoleName" // roleFromDatabase Null olduğu için her zaman DefaultRoleName adında yeni role ekliyor.
        //                };
        //            }

        //            newUser.Role = roleFromDatabase; 

        //            var add = await _httpClient.PostAsJsonAsync(_apiAddress, newUser);

        //            if (add.IsSuccessStatusCode)
        //                return RedirectToAction("Index", "Home");
        //            else
        //                ModelState.AddModelError("", "An error occurred while registering the user.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ModelState.AddModelError("", ex.Message);
        //    }
        //    return View(newUser);
        //}

        public async Task<IActionResult> Register(User newUser, string password2)
        {
            try
            {
                if (newUser.Password == password2)
                {
                    var users = await _httpClient.GetFromJsonAsync<List<User>>(_apiAddress);
                    var user = users.FirstOrDefault(u => u.Email == newUser.Email);

                    if (user is not null)
                        ModelState.AddModelError("", "This Email Has Already Been Registered!");

                    var roles = await _httpClient.GetFromJsonAsync<List<Role>>(_apiRoleAddress);

                    // Burada, doktor, kullanıcı ve admin rollerinden birini seçelim.
                    Role selectedRole = SelectRoleToAssign(roles);

                    if (selectedRole == null)
                    {
                        ModelState.AddModelError("", "No valid role found to assign to the new user.");
                        return View(newUser);
                    }

                    newUser.RoleId = selectedRole.Id; // Eğer Id kullanıyorsanız RoleId'ye atayın, ya da RoleName'e göre yapıyorsanız RoleName'e atayın.

                    var add = await _httpClient.PostAsJsonAsync(_apiAddress, newUser);

                    if (add.IsSuccessStatusCode)
                        return RedirectToAction("Index", "Home");
                    else
                        ModelState.AddModelError("", "An error occurred while registering the user.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(newUser);
        }

        // Belirtilen üç rol (doktor, kullanıcı ve admin) arasından birini seçen yardımcı fonksiyon.
        private Role SelectRoleToAssign(List<Role> roles)
        {
            // Burada, belirtilen üç rol arasından birini seçmek için kendi kriterlerinize göre bir seçim yapabilirsiniz.
            // Örneğin, rastgele bir rol seçmek için aşağıdaki gibi yapabilirsiniz:
            // Random random = new Random();
            // int randomIndex = random.Next(0, roles.Count);
            // return roles[randomIndex];

            // Varsayılan olarak, doktor rolünü döndürelim:
            return roles.FirstOrDefault(r => r.RoleName == "doktor");
        }







        public IActionResult Login(string redirectUrl)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginModel)
        {
            var users = await _httpClient.GetFromJsonAsync<List<User>>(_apiAddress);
            var user = users.FirstOrDefault(u => u.Email == loginModel.Email && u.Password == loginModel.Password);
            if (user is not null)
                return RedirectToAction("Index", "Home");
            ModelState.AddModelError("", "?");
            return View(loginModel);
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }
    }
}
