using AutoMapper;
using RecipeCrawler.Core.Exceptions;
using RecipeCrawler.Data.Repositories;
using RecipeCrawler.Entities;
using RecipeCrawler.Entities.Models;

namespace RecipeCrawler.Core.Services.Accounts
{
    public class AccountService : IAccountService
    {
        private IChefRepository _chefRepository;
        private IMapper _mapper;
        public AccountService(IChefRepository chefRepository, IMapper mapper) 
        {
            _chefRepository = chefRepository;
            _mapper = mapper;
        }

        public async Task<Chef> Authenticate(string username, string password)
        {
            var chef = await _chefRepository.FindChefByUsername(username);

            if (BCrypt.Net.BCrypt.Verify(password, chef.PasswordHash))

            BCrypt.Net.BCrypt.Verify(password, chef.Password);

            return chef;
        }

        public Task ChangePassword(int chefId, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public async Task<Chef> Create(CreateAccountModel model)
        {
            var doesAccountExist = await _chefRepository.CheckIfUsernameOrEmailAlreadyExists(model.Username, model.Email);

            if (doesAccountExist != null)
            {
                throw new AccountAlreadyExistsException("An account already exists with this user name or email.");
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password, BCrypt.Net.BCrypt.GenerateSalt());
            model.Password = hashedPassword;
            var chef = _mapper.Map<Chef>(model);
            //need logic to check for duplicate
            var createdChef = await _chefRepository.InsertChef(chef);
            return createdChef;
        }

        public Task ResetPassword(int chefId, string newPassword)
        {
            throw new NotImplementedException();
        }
    }
}
