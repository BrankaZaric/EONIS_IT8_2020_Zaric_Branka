using VPDecijeIgracke.Models.ProizvodModel;
using VPDecijeIgracke.Models.StavkaPorudzbineModel;

namespace VPDecijeIgracke.Data.StavkaPorudzbineData
{
    public interface IStavkaRepository
    {
        Task<List<StavkaPorudzbine>> GetAllStavke();
        Task<StavkaPorudzbine> GetStavkaById(int stavkaID);
        Task<StavkaPorudzbineConfirmation> CreateStavka(StavkaPorudzbine stavka);
        Task UpdateStavka(StavkaPorudzbine stavka);
        Task DeleteStavka(int stavkaID);
        Task<bool> SaveChanges();

    }
}
