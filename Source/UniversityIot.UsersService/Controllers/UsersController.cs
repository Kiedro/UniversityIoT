namespace UniversityIot.UsersService.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Http;
    using UsersDataAccess.Models;
    using UsersDataService;
    using Models;

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
            return Ok();
        }

        [Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var user = await this.usersDataService.GetUserAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var userVm = MapUser(user);
            return Ok(userVm);
        }

        [Route("")]
        public async Task<IHttpActionResult> Post(AddUserViewModel userVm)
        {
            var user = new User
            {
                CustomerNumber = userVm.CustomerNumber,
                Name = userVm.Name,
                Password = userVm.Password
            };

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var addedUser = await usersDataService.AddUserAsync(user);
            var userFromDb = await usersDataService.GetUserAsync(addedUser.Id);

            return Ok(MapUser(userFromDb));
        }

        [Route("{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var user = await usersDataService.GetUserAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            await usersDataService.DeleteUserAsync(user.Id);

            return Ok();
        }

        [Route("{id}")]
        public async Task<IHttpActionResult> Put(EditUserViewModel userEvm, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await usersDataService.GetUserAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            user.CustomerNumber = userEvm.CustomerNumber;
            var updatedUser = await usersDataService.UpdateUserAsync(user);

            var userVm = MapUser(updatedUser);

            return Ok(userVm);
        }
        
        private static UserViewModel MapUser(User user)
        {
            var userVM = new UserViewModel()
            {
                CustomerNumber = user.CustomerNumber,
                Id = user.Id,
                Name = user.Name
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