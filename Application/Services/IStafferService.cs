using Domain;

namespace Application.Services;

public interface IStafferService
{
    IList<StafferModel> GetAll();
    void Create(StafferModel model);
    void Update(StafferModel model);
    bool CanDelete(int stafferId);
    void Delete(StafferModel model);
}
