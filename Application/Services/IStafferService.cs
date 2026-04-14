using Application.DTOs;

namespace Application.Services;

public interface IStafferService
{
    IList<StafferDto> GetAll();
    void Create(StafferDto dto);
    void Update(StafferDto dto);
    bool CanDelete(int stafferId);
    void Delete(int stafferId);
}
