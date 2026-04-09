using Domain;

namespace Application.Services;

public interface ICounterAgentService
{
    IList<CounterAgentModel> GetAll();
    IList<StafferModel> GetStaffers();
    void Create(CounterAgentModel model);
    void Update(CounterAgentModel model);
    bool CanDelete(int counterAgentId);
    void Delete(CounterAgentModel model);
}
