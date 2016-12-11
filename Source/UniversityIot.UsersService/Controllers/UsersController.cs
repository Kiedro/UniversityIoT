using AutoMapper;

namespace UniversityIot.UsersService.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Http;
    using UniversityIot.UsersDataAccess.Models;
    using UniversityIot.UsersDataService;
    using UniversityIot.UsersService.Helpers;
    using UniversityIot.UsersService.Models;

    [RoutePrefix("users")]
    public class UsersController : ApiController
    {
        private readonly IUsersDataService usersDataService;

        public UsersController(IUsersDataService usersDataService)
        {
            this.usersDataService = usersDataService;
        }

        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            return Ok("a");
        }

        [Route("{id:int}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var user = await usersDataService.GetUserAsync(id);
            if (user == null)
            {
                return BadRequest("User does not exist");
            }

            var model = Mapper.Map<User, UserViewModel>(user);
            return Ok(model);
        }

        [Route("")]
        public async Task<IHttpActionResult> Post(AddUserViewModel userVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(modelState: ModelState);
            }

            User createdUser = await usersDataService.AddUserAsync(new User
            {
                CustomerNumber = userVM.CustomerNumber,
                Name = userVM.Name,
                Password = userVM.Password
            });

            UserViewModel createdUserVM = Mapper.Map<User, UserViewModel>(createdUser);
            return Created(createdUserVM.Id.ToString(), createdUserVM);
        }

        [Route("{id:int}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var user = await usersDataService.GetUserAsync(id);
            if (user == null)
            {
                return BadRequest("User does not exist");
            }

            await usersDataService.DeleteUserAsync(id);

            return Ok();
        }

        [Route("{id:int}")]
        public async Task<IHttpActionResult> Put(int id, [FromBody] EditUserViewModel editUserViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updatedUser = await usersDataService.UpdateUserAsync(new User { Id = id, CustomerNumber = editUserViewModel.CustomerNumber });
            var updatedUserVM = Mapper.Map<User, UserViewModel>(updatedUser);

            return Ok(updatedUserVM);
        }
    }
}