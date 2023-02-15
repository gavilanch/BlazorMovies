using BlazorMovies.Shared.DTOs;
using BlazorMovies.Shared.Repositories;
using BlazorMovies.SharedBackend.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlazorMovies.SharedBackend.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<IdentityUser> userManager;

        public UsersRepository(ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<List<RoleDTO>> GetRoles()
        {
            return await context.Roles
               .Select(x => new RoleDTO { RoleName = x.Name }).ToListAsync();
        }

        public async Task<PaginatedResponse<List<UserDTO>>> GetUsers(PaginationDTO paginationDTO)
        {
            var queryable = context.Users.AsQueryable();
            var paginatedResponse = await queryable.GetPaginatedResponse(paginationDTO);
                
            var usersDTO = paginatedResponse.Response
                .Select(x => new UserDTO { Email = x.Email, UserId = x.Id }).ToList();

            var paginatedResponseDTO = new PaginatedResponse<List<UserDTO>>()
            {
                Response = usersDTO,
                TotalAmountPages = paginatedResponse.TotalAmountPages
            };

            return paginatedResponseDTO;
        }

        public async Task AssignRole(EditRoleDTO editRoleDTO)
        {
            var user = await userManager.FindByIdAsync(editRoleDTO.UserId);
            await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, editRoleDTO.RoleName));
        }

        public async Task RemoveRole(EditRoleDTO editRoleDTO)
        {
            var user = await userManager.FindByIdAsync(editRoleDTO.UserId);
            await userManager.RemoveClaimAsync(user, new Claim(ClaimTypes.Role, editRoleDTO.RoleName));
        }
    }
}
