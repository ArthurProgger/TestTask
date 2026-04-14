using Application.DTOs;

namespace Application.Services;

public interface ICounterAgentService
{
    IList<CounterAgentDto> GetAll();
    IList<StafferDto> GetStaffers();
    void Create(CounterAgentDto dto);
    void Update(CounterAgentDto dto);
    bool CanDelete(int counterAgentId);
    void Delete(int counterAgentId);
}
