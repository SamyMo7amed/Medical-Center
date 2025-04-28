using Medical_CenterAPI.Data;
using Medical_CenterAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Medical_CenterAPI.Repository
{
    public class PatientRepository : IRepository<Patient>
    {
        private readonly AppDbContext appDbContext;

        public PatientRepository(AppDbContext appDbContext) {
        this.appDbContext = appDbContext;
        }  
        public async Task AddAsync(Patient entity)
        {
            await appDbContext.AddAsync(entity);
            
        }

        public void DeleteAsync(Guid id)
        {
            var user= appDbContext.Patients.FirstOrDefault(x => x.Id == id);
            if (user != null) { 
            
                appDbContext.Patients.Remove(user); 
            }
            
        }

        public async Task<IEnumerable<Patient>> GetAllAsync()
        {
            var users= await appDbContext.Patients.ToListAsync();
            return users;   
        }

        public async Task<Patient?> GetByIdAsync(Guid id)
        {
            return await appDbContext.Patients.FirstOrDefaultAsync(x=>x.Id == id);
         
        }

        public void SaveChangesAsync()
        {
            appDbContext.SaveChanges();
        }

        public void UpdateAsync(Patient entity)
        {
            var user = appDbContext.Patients.FirstOrDefault(x => x.Id == entity.Id);
            if (user != null)
            {
                user.UserName = entity.UserName;
                user.Password = entity.Password;
                user.Email = entity.Email;
                user.EmailConfirmed = entity.EmailConfirmed;
                user.ConfirmToken = entity.ConfirmToken;
                user.Appointments = entity.Appointments;
                user.ConfirmPassword = entity.ConfirmPassword;
                user.PhoneNumber = entity.PhoneNumber;
                user.Age=entity.Age;
                user.ImagePath = entity.ImagePath;


            }

        }
    }
}
