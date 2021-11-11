using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Models.ViewModels;

namespace ToDoList.Models
{
    public class ProviderFactory
    {
        private readonly IServiceProvider _provider;

        public ProviderFactory(IServiceProvider provider)
        {
            this._provider = provider;
        }

        public IToDoItemProvider GetProvider(TypeOfToDoItemProvider typeOfProvider)
        {
            switch (typeOfProvider)
            {
                case TypeOfToDoItemProvider.Memory:
                    return (IToDoItemProvider)_provider.GetService(typeof(ToDoItemMemoryProvider));

                case TypeOfToDoItemProvider.MsSql:
                    return (IToDoItemProvider) _provider.GetService(typeof(ToDoItemMsSqlProvider));

                default:
                    return null;
            }
        }
    }
}
