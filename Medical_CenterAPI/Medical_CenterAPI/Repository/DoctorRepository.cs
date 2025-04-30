using Medical_CenterAPI.Data;
using Medical_CenterAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Runtime.CompilerServices;

namespace Medical_CenterAPI.Repository
{
    public class DoctorRepository : IRepository<Doctor>
    {

        private readonly AppDbContext appDbContext;
        public DoctorRepository(AppDbContext appDbContext) {
        this.appDbContext = appDbContext;   
        }  
        public async Task AddAsync(Doctor entity)
        {


           
           
                await appDbContext.Doctors.AddAsync(entity);
            
           


        }
        

        public async Task DeleteAsync(Guid id)
        {
              var result=await appDbContext.Doctors.FirstOrDefaultAsync(x => x.Id == id);
            if (result != null)
            {
                appDbContext.Doctors.Remove(result);
            }
            
        }

        public async Task<IEnumerable<Doctor>> GetAllAsync()
        {
           var Doctors= await appDbContext.Doctors.ToListAsync();

            return Doctors; 
        }

        public async Task<Doctor?> GetByIdAsync(Guid id)
        {
            var user = await appDbContext.Doctors.FirstOrDefaultAsync(x => x.Id == id);

            return user;
        }

        public async Task SaveChangesAsync()
        {
          await appDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Doctor entity)
        {
            var user = await appDbContext.Doctors.FirstOrDefaultAsync(x => x.Id == entity.Id);
            if (user != null) {
                user.UserName = entity.UserName;
                user.Password = entity.Password;
                user.Email = entity.Email;
                user.EmailConfirmed = entity.EmailConfirmed;        
                user.ConfirmToken = entity.ConfirmToken;
                user.Appointments = entity.Appointments;
                user.ConfirmPassword = entity.ConfirmPassword;
                user.PhoneNumber = entity.PhoneNumber;  
                user.Specialization = entity.Specialization;    
                user.ImagePath = entity.ImagePath;
                
            
            }

        }
    }
}
