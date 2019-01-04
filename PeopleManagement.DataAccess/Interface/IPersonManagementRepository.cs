using PeopleManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleManagement.DataAccess.Interface
{
    public interface IPersonManagementRepository
    {
        List<Person> GetPeople(Person person);
        int UpsertPerson(Person person);
        List<State> GetStates();
    }
}
