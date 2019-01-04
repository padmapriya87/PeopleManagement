using PeopleManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleManagement.Service.Interface
{
    public interface IStatesService
    {
        List<State> GetStates();
    }
}
