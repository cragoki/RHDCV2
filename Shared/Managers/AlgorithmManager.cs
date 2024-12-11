using DAL.DbRHDCV2Context;
using DAL.Entities;
using DAL.Enums;
using Shared.Algorithms.Interfaces;
using Shared.Managers.Interfaces;

namespace Shared.Managers
{
    public class AlgorithmManager : IAlgorithmManager
    {
        private readonly RHDCV2Context _context;
        private readonly IAlphabeticalAlgorithm _alphabeticalAlgorithm;

        public AlgorithmManager(RHDCV2Context context, IAlphabeticalAlgorithm alphabeticalAlgorithm)
        {
            _context = context;
            _alphabeticalAlgorithm = alphabeticalAlgorithm;
        }

        public async Task<AlgorithmExecution> ExecuteAlgorithm(AlgorithmType algorithm)
        {
            var result = new AlgorithmExecution();

            try
            {
                switch (algorithm)
                {
                    case AlgorithmType.Alphabetical:
                            result = await _alphabeticalAlgorithm.ExecuteAlgorithm();
                        break;
                    default:
                        throw new NotImplementedException();
                }

                _context.tb_algorithm_execution.Add(result);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return result;
        }
    }
}
