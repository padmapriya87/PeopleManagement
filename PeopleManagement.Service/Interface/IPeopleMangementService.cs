using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeopleManagement.Domain;

namespace PeopleManagement.Service.Interface
{
    public interface IPeopleMangementService
    {
        List<Person> GetPeople(Person person);
        int UpertPerson(Person person);
        List<State> GetStates();
        
    }
}
