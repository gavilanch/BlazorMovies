using AutoMapper;
using BlazorMovies.Shared.DTOs;
using BlazorMovies.Shared.Entities;
using BlazorMovies.Shared.Repositories;
using BlazorMovies.SharedBackend.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorMovies.SharedBackend.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IFileStorageService fileStorageService;
        private readonly IMapper mapper;
        private readonly string containerName = "people";

        public PersonRepository(ApplicationDbContext context,
            IFileStorageService fileStorageService,
            IMapper mapper)
        {
            this.context = context;
            this.fileStorageService = fileStorageService;
            this.mapper = mapper;
        }

        public Task<PaginatedResponse<List<Person>>> GetPeople(PaginationDTO paginationDTO)
        {
            var queryable = context.People.AsQueryable();
            return queryable.GetPaginatedResponse(paginationDTO);
        }

        public async Task<List<Person>> GetPeopleByName(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText)) { return new List<Person>(); }
            return await context.People.Where(x => x.Name.Contains(searchText))
                .Take(5)
                .ToListAsync();
        }

        public async Task<Person> GetPersonById(int id)
        {
            return await context.People.FindAsync(id);
        }

        public async Task CreatePerson(Person person)
        {
            if (!string.IsNullOrWhiteSpace(person.Picture))
            {
                var personPicture = Convert.FromBase64String(person.Picture);
                person.Picture = await fileStorageService.SaveFile(personPicture, "jpg", containerName);
            }

            context.Add(person);
            await context.SaveChangesAsync();
        }

        public async Task UpdatePerson(Person person)
        {
            context.Entry(person).State = EntityState.Detached;

            var personDB = await GetPersonById(person.Id);

            personDB = mapper.Map(person, personDB);

            if (!string.IsNullOrWhiteSpace(person.Picture))
            {
                var personPicture = Convert.FromBase64String(person.Picture);
                personDB.Picture = await fileStorageService.EditFile(personPicture,
                    "jpg", containerName, personDB.Picture);
            }

            await context.SaveChangesAsync();
        }

        public async Task DeletePerson(int Id)
        {
            var person = await GetPersonById(Id);
            context.Remove(person);
            await context.SaveChangesAsync();
        }
    }
}
