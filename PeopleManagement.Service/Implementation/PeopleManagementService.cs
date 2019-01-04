using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeopleManagement.Service.Interface;
using PeopleManagement.Domain;
using PeopleManagement.DataAccess.Interface;

namespace PeopleManagement.Service.Implementation
{
    public class PeopleManagementService : IPeopleMangementService
    {
        private IPersonManagementRepository repository;
        public PeopleManagementService(IPersonManagementRepository repo)
        {
            repository = repo;
        }

        public List<Person> GetPeople(Person person)
        {
            return repository.GetPeople(person);
        }

        public List<State> GetStates()
        {
            return repository.GetStates();
        }

        public int UpertPerson(Person person)
        {

            return repository.UpsertPerson(person);

        }


    }
}
