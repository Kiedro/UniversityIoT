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
            var model = MapUser(user);

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

            UserViewModel createdUserVM = MapUser(createdUser);
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
            var updatedUserVM = MapUser(updatedUser);

            return Ok(updatedUserVM);
        }


        private static UserViewModel MapUser(User user)
        {
            var userVM = new UserViewModel()
            {
                CustomerNumber = user.CustomerNumber,
                Id = user.Id,
                Name = user.Name,
                Password = user.Password
            };

            foreach (var userGateway in user.UserGateways)
            {
                userVM.UserGateways.Add(new UserGatewayViewModel()
                {
                    GatewaySerial = userGateway.GatewaySerial,
                    Id = userGateway.Id,
                    AccessType = userGateway.AccessType.ToString()
                });
            }

            return userVM;
        }
    }
}