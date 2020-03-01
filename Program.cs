using System;

namespace MaybeMonadDemo
{
    public class Person
    {
        public Address Address { get; set; }

        public Person(Address address)
        {
            Address = address;
        }
    }

    public class Address
    {
        public string PostCode { get; set; }

        public Address(string postCode)
        {
            PostCode = postCode;
        }
    }

    class Program
    {
        static void MyMethod(Person p)
        {
            // we want to achieve PostCode
            // example typical scenario is as follows:

            // string postcode;
            // if (p != null)
            // {
            //   if (HasMedicalRecord(p) && p.Address != null)
            //   {
            //     CheckAddress(p.Address);
            //     if (p.Address.PostCode != null)
            //       postcode = p.Address.PostCode.ToString();
            //     else
            //       postcode = "UNKNOWN";
            //   }
            // }

            // using With extension method
            // checking for nulls is build-in into each function in the chain
            string postcode = p.With(x => x.Address).With(x => x.PostCode);

            //using Maybe monad
            postcode = p
                .If(HasMedicalRecord)
                .With(x => x.Address)
                .Do(CheckAddress)
                .Return(x => x.PostCode, "UNKNOWN");
            
            Console.WriteLine(postcode);
        }

        private static void CheckAddress(Address pAddress)
        {
            // anything
        }

        private static bool HasMedicalRecord(Person person)
        {
            return true;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Maybe monad demo!");

            var address = new Address("123");

            MyMethod(new Person(address));
        }
    }
}