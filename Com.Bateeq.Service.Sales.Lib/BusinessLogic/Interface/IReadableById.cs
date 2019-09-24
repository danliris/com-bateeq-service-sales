using System.Threading.Tasks;

namespace Com.Bateeq.Service.Purchasing.Lib.Interfaces
{
    public interface IReadByIdable<TModel>
    {
        Task<TModel> ReadById(int id);
    }
}
