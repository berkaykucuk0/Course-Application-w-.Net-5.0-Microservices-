using Course.Services.Order.Domain.Core;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Course.Services.Order.Domain.OrderAggregate
{
    [Owned] // LOOK HERE FOR OWNED TYPES https://docs.microsoft.com/en-us/ef/core/modeling/owned-entities

    //ValueObject classes is that dont have their own id. They are not entity classes.So we will do all operations in Order Class for Address class (add,delete,update...)
    public class Address : ValueObject
    {

        //This properties private set because  we don't want anyone else change it in other places.Only we can set these values from function in this class
        public string Province { get; private set; }
        public string District { get; private set; }
        public string Street { get; private set; }
        public string ZipCode { get; private set; }
        public string Line { get; private set; }

        public Address(string province, string district, string street, string zipCode, string line)
        {
            Province = province;
            District = district;
            Street = street;
            ZipCode = zipCode;
            Line = line;
        }



        //Override function 
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Province;
            yield return District;
            yield return Street;
            yield return ZipCode;
            yield return Line;
        }
    }
}
