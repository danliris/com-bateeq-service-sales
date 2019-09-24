using System.Threading.Tasks;

namespace Com.Bateeq.Service.Purchasing.Lib.Interfaces
{
    public interface ICreateable
    {
        Task<int> Create(object model);
    }
}
