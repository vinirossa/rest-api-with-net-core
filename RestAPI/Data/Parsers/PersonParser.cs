using RestAPI.Data.VOs;
using RestAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace RestAPI.Data.Parsers
{
    public class PersonParser : IParser<PersonVO, Person>, IParser<Person, PersonVO>
    {
        public Person Parse(PersonVO origin)
        {
            if (origin == null)
                return null;

            return new Person
            {
                Id = origin.Id,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                Address = origin.Address,
                Gender = origin.Gender
            };
        }

        public List<Person> Parse(List<PersonVO> origin)
        {
            if (origin == null)
                return null;

            return origin.Select(i => Parse(i)).ToList();
        }

        public PersonVO Parse(Person origin)
        {
            if (origin == null)
                return null;

            return new PersonVO
            {
                Id = origin.Id,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                Address = origin.Address,
                Gender = origin.Gender
            };
        }

        public List<PersonVO> Parse(List<Person> origin)
        {
            if (origin == null)
                return null;

            return origin.Select(i => Parse(i)).ToList();
        }
    }
}
