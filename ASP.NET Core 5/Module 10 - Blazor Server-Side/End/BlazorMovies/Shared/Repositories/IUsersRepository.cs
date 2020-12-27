using BlazorMovies.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMovies.Shared.Repositories
{
    public interface IUsersRepository
    {
        Task AssignRole(EditRoleDTO editRole);
        Task<List<RoleDTO>> GetRoles();
        Task<PaginatedResponse<List<UserDTO>>> GetUsers(PaginationDTO paginationDTO);
        Task RemoveRole(EditRoleDTO editRole);
    }
}
