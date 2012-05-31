using System;
using LibrarySystem.Interfaces;

namespace LibrarySystem.Core
{
    public class Publisher : IPublisher
    {
        public string Name { get; private set; }

        private Publisher()
        {
            
        }

        public static IPublisher Create(string name)
        {
            if (name == null)
                throw new ArgumentNullException();

            return new Publisher() { Name = name };
        }
    }
}
