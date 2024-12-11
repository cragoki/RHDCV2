using DAL.Entities;
using DAL.Enums;

namespace Shared.Managers.Interfaces
{
    public interface IAlgorithmManager
    {
        Task<AlgorithmExecution> ExecuteAlgorithm(AlgorithmType algorithm);
    }
}