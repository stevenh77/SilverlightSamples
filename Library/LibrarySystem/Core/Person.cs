using System;
using LibrarySystem.Interfaces;

namespace LibrarySystem.Core
{
    public class Person : IPerson
    {
        public string Address { get; private set; }
        public string Name { get; private set; }

        private Person()
        {
            
        }

        public static IPerson Create(string address, string name)
        {
            if (address == null || name == null)
                throw new ArgumentNullException();

            return new Person() { Address = address, Name = name};
        }
    }
}
