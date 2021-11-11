using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Models
{
    public class ProviderFactory
    {
        private readonly IServiceProvider provider;

        public ProviderFactory(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public IToDoItemProvider GetProvider(string typeOfProvider)
        {
            if (typeOfProvider == "memory")
            {
                return (IToDoItemProvider) provider.GetService(typeof(ToDoItemMemoryProvider));
            }

            return (IToDoItemProvider) provider.GetService(typeof(ToDoItemMsSqlProvider));
        }
    }
}
