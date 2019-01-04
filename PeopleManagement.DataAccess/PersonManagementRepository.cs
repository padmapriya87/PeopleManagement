using PeopleManagement.DataAccess.Interface;
using PeopleManagement.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleManagement.DataAccess
{
    public class PersonManagementRepository : IPersonManagementRepository
    {
        string cs = ConfigurationManager.ConnectionStrings["PeopleManagementContext"].ToString();

        public List<Person> GetPeople(Person p)
        {
            // Create ADO.NET objects.
            SqlConnection connection = new SqlConnection(cs);
            var command = new SqlCommand("uspPersonSearch", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@first_name", p.FirstName??string.Empty);
            command.Parameters.AddWithValue("@last_name", p.LastName?? string.Empty);
            command.Parameters.AddWithValue("@state_id", p.StateId);
            command.Parameters.AddWithValue("@gender", p.Gender!=0?p.Gender.ToString():string.Empty);
            command.Parameters.AddWithValue("@dob", p.DOB!= DateTime.MinValue?p.DOB.ToString("MM/dd/yyyy"): string.Empty);
            
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            List<Person> personList = new List<Person>();
            Person person = null;

            while (reader.Read())
            {
                person = new Person();
                person.PersonId = int.Parse(reader["person_id"].ToString());
                person.FirstName = reader["first_name"].ToString();
                person.LastName = reader["last_name"].ToString();
                person.StateId =(int) reader["state_id"];
                person.StateCode = reader["state_code"].ToString();
                person.Gender =char.Parse( reader["gender"].ToString());
                person.DOB = DateTime.Parse(reader["dob"].ToString());
                personList.Add(person);
            }
            connection.Close();
            return personList;
        }

        public List<State> GetStates()
        {
            SqlConnection connection = new SqlConnection(cs);
            var command = new SqlCommand("uspStatesList", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            connection.Open();
            var reader = command.ExecuteReader();
            List<State> states = new List<State>();
            State state;
            while (reader.Read())
            {
                state = new State();
                state.stateId = int.Parse(reader["state_id"].ToString());
                state.code = reader["state_code"].ToString();
                states.Add(state);
            }
            connection.Close();
            return states;
        }

        public int UpsertPerson(Person p)
        {
            SqlConnection connection = new SqlConnection(cs);
            var command = new SqlCommand("uspPersonUpsert", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@person_id", p.PersonId);
            command.Parameters.AddWithValue("@first_name", p.FirstName);
            command.Parameters.AddWithValue("@last_name", p.LastName);
            command.Parameters.AddWithValue("@state_id", p.StateId);
            command.Parameters.AddWithValue("@gender", p.Gender);
            command.Parameters.AddWithValue("@dob", p.DOB);
            connection.Open();
            var output = command.ExecuteNonQuery();
            connection.Close();
            return output;

        }
    }
}
