using Application.DTOs;
using Domain;

namespace Application.Services;

public interface IStafferService
{
    IList<StafferDto> GetAll();
    void Create(StafferModel model);
    void Update(StafferModel model);
    bool CanDelete(int stafferId);
    void Delete(int stafferId);
}
