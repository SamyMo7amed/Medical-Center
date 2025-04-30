using Medical_CenterAPI.Data;
using Medical_CenterAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Medical_CenterAPI.Repository
{
    public class AssistantRepository : IRepository<Assistant>
    {
        private readonly AppDbContext appDbContext;

        public AssistantRepository(AppDbContext appDbContext) {
        this.appDbContext = appDbContext;
                }    
        public async Task AddAsync(Assistant entity)
        {
            await appDbContext.Assistants.AddAsync(entity);
           
        }

        public async Task DeleteAsync(Guid id)
        {
            var user= await appDbContext.Assistants.FirstOrDefaultAsync(x => x.Id == id);
            if (user != null)
            {
                appDbContext.Remove(user);
            }
        }

        public async Task<IEnumerable<Assistant>> GetAllAsync()
        {

            var result= await appDbContext.Assistants.ToListAsync();  
            return result;

           
           
        }

        public async Task<Assistant?> GetByIdAsync(Guid id)
        {

            var user = await appDbContext.Assistants.FirstOrDefaultAsync(x => x.Id == id);
          
                return user;
        }

        public async Task SaveChangesAsync()
        {
            await appDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Assistant entity)
        {
            var user = await appDbContext.Assistants.FirstOrDefaultAsync(x => x.Id == entity.Id);
            if (user != null)
            {
                user.UserName = entity.UserName;
                user.Password = entity.Password;
                user.Email = entity.Email;
                user.EmailConfirmed = entity.EmailConfirmed;
                user.ConfirmToken = entity.ConfirmToken;
                user.ConfirmPassword = entity.ConfirmPassword;
                user.PhoneNumber = entity.PhoneNumber;
                user.ImagePath = entity.ImagePath;


            }

        }
    }
}
