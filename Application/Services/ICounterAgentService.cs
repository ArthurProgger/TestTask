using Application.DTOs;
using Domain;

namespace Application.Services;

public interface ICounterAgentService
{
    IList<CounterAgentDto> GetAll();
    IList<StafferDto> GetStaffers();
    void Create(CounterAgentModel model);
    void Update(CounterAgentModel model);
    bool CanDelete(int counterAgentId);
    void Delete(int counterAgentId);
}
