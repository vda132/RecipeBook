using DBLayer.Models;
using Providers.Providers;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserProvider userProvider;
        public UsersController(IUserProvider userProvider) =>
            this.userProvider = userProvider;

        [HttpGet]
        public async Task<JsonResult> GetAll() =>
            new JsonResult((await this.userProvider.GetAllAsyns()).ToList());

        [HttpGet("{id}")]
        public async Task<JsonResult> Get(Guid id) =>
            new JsonResult(await this.userProvider.GetAsync(id));

        [HttpPost("register")]
        public async Task<ResultDTO> Register([FromBody] UserRegisterDTO model)
        {
            if (string.IsNullOrEmpty(model.Login) || 
                string.IsNullOrEmpty(model.Name) || 
                string.IsNullOrEmpty(model.Password))
                return new ResultDTO
                {
                    Status = 500,
                    Message = "Fields should be filled"
                };

            var users = (await this.userProvider.GetAllAsyns()).ToList();
            

            if (users.FirstOrDefault(x => x.Login.Equals(model.Login)) is not null)
                return new ResultDTO
                {
                    Status = 500,
                    Message = "Current user`s already exist"
                };

            var user = new User
            {
                Login = model.Login,
                Name = model.Name,
                Password = model.Password
            };

            await this.userProvider.AddAsync(user);

            return new ResultDTO
            {
                Status = 200,
                Message = "Ok"
            };
        }

        [HttpPut("{id}")]
        public async Task<ResultDTO> Put(Guid id, User model)
        {
            if (string.IsNullOrEmpty(model.Login) ||
                string.IsNullOrEmpty(model.Name) ||
                string.IsNullOrEmpty(model.Password))
                return new ResultDTO
                {
                    Status = 500,
                    Message = "Fields should be filled"
                };

            var users = (await this.userProvider.GetAllAsyns()).ToList();

            if (users.FirstOrDefault(x => x.Login.Equals(model.Login) && x.Id == model.Id) is not null)
                return new ResultDTO
                {
                    Status = 500,
                    Message = "Current user`s already exist"
                };

            if(!(await this.userProvider.UpdateAsync(model, id)))
                return new ResultDTO
                {
                    Status = 500,
                    Message = "Wrong data"
                };

            return new ResultDTO
            {
                Status = 200,
                Message = "Ok"
            };
        }

        [HttpDelete("{id}")]
        public async Task<ResultDTO> Delete(Guid id)
        {
            return await this.userProvider.DeleteAsync(id) ? new ResultDTO
            {
                Status = 200,
                Message = "Ok"
            } : new ResultDTO
            {
                Status = 500,
                Message = "Wrong data"
            };
        }

        [HttpPost("login")]
        public async Task<AuthorizedUserDTO> Login([FromBody] LoginDTO loginDTO)
        {
            var users = (await this.userProvider.GetAllAsyns()).ToList();

            var user = users.FirstOrDefault(x => x.Login.Equals(loginDTO.Login) && x.Password.Equals(loginDTO.Password));

            if (user is not null)
                return new AuthorizedUserDTO
                {
                    Name = user.Name,
                    Login = user.Login,
                    Status = 200
                };

            return new AuthorizedUserDTO
            {
                Name = "",
                Login = "",
                Status = 500
            }; 
        }
    }
}
