﻿using RecipeCrawler.Entities;

namespace RecipeCrawler.Data.Repositories
{
    public interface IChefRepository
    {
        Task<Chef> InsertChef(Chef chefToCreate);
        Task<Chef?> CheckIfUsernameOrEmailAlreadyExists(string username, string email);
        Task<Chef?> FindChefByUsername(string username);
        Task<Chef?> FindChefByVerificationGuid(Guid verificationGuid);
        Task<bool> VerifyChef(Chef chef);
        Task<Chef?> ResetVerificationGuid(Guid guid, string email);
    }
}
