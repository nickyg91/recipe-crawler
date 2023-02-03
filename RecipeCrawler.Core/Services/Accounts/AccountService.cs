using AutoMapper;
using RecipeCrawler.Core.Exceptions;
using RecipeCrawler.Core.Services.Email;
using RecipeCrawler.Data.Repositories;
using RecipeCrawler.Entities;
using RecipeCrawler.Entities.Models;

namespace RecipeCrawler.Core.Services.Accounts
{
    public class AccountService : IAccountService
    {
        private readonly IChefRepository _chefRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        public AccountService(IChefRepository chefRepository, IMapper mapper, IEmailService emailService) 
        {
            _chefRepository = chefRepository;
            _mapper = mapper;
            _emailService = emailService;
        }

        public async Task VerifyAccount(Guid verificationGuid)
        {
            var chef = await _chefRepository.FindChefByVerificationGuid(verificationGuid);
            if (chef == null)
            {
                throw new AccountNotFoundFromVerificationEmailException(
                    "Account was not found via verification email.");
            }
            var isVerified = await _chefRepository.VerifyChef(chef);

            if (!isVerified)
            {
                throw new AccountVerificationException(
                    "Unable to verify account. Please request a new verification email.");
            }
        }

        public async Task<Chef?> Authenticate(string username, string password)
        {
            var chef = await _chefRepository.FindChefByUsername(username);

            if (chef != null && !chef.IsEmailVerified)
            {
                return null;
            }

            return BCrypt.Net.BCrypt.Verify(password, chef?.PasswordHash) ? chef : null;
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

            await _emailService.SendVerificationEmail(createdChef.Email, createdChef.EmailVerificationGuid!.Value);
            
            return createdChef;
        }

        public Task ResetPassword(int chefId, string newPassword)
        {
            throw new NotImplementedException();
        }
    }
}
