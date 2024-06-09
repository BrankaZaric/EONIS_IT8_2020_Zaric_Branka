using VPDecijeIgracke.Models.PorudzbinaModel;

namespace VPDecijeIgracke.Data
{
    public interface IPaymentService
    {
        Task<PorudzbinaPaymentDTO> CreateOrUpdatePaymentIntent(int porudzbinaId);

        Task<Porudzbina> UpdatePorudzbinaPaymentSucceeded(string paymentIntentId);

        Task<Porudzbina> UpdatePorudzbinaPaymentFailed(string paymentIntentId);
    }
}
