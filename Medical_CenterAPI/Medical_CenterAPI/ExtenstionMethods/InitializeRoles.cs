using Medical_CenterAPI.Data;
using Medical_CenterAPI.UnitOfWork;
using Microsoft.AspNetCore.Identity;

namespace Medical_CenterAPI.ExtenstionMethods
{
    public  class InitializeRoles
    {

        private readonly AppDbContext appDbContext ;
        private readonly IUnitOfWork unitOfWork;    

       
        public  InitializeRoles(AppDbContext appDbContext, IUnitOfWork unitOfWork)
        {
            this.appDbContext = appDbContext;   
            this.unitOfWork = unitOfWork;
         
           
        }

        public  async Task InitRoles()
        {

            string[] roleNames = { "Assistant", "Doctor", "Manager" };
            foreach (var roleName in roleNames)
            {
                
                if (!await unitOfWork.RoleManager.RoleExistsAsync(roleName))
                {
                    var role = new IdentityRole(roleName);
                    await unitOfWork.RoleManager.CreateAsync(new IdentityRole<Guid> { Name = roleName, NormalizedName = roleName.ToUpper() });
                }
            }
        }

    }
}
