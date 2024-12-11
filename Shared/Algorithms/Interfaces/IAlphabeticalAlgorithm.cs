using DAL.Entities;
using Shared.Models.Algorithms;

namespace Shared.Algorithms.Interfaces
{
    public interface IAlphabeticalAlgorithm
    {
        Task<AlgorithmExecution> ExecuteAlgorithm();
        Task<List<AlgorithmHorseRankingModel>> PredictRace(Race race);
    }
}