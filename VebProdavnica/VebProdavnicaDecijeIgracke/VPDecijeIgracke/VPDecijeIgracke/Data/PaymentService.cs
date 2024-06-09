using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Stripe;
using VPDecijeIgracke.Context;
using VPDecijeIgracke.Data.PorudzbinaData;
using VPDecijeIgracke.Models.PorudzbinaModel;
using VPDecijeIgracke.Models.StavkaPorudzbineModel;

namespace VPDecijeIgracke.Data
{
    public class PaymentService : IPaymentService
    {
        private readonly IPorudzbinaRepository porudzbinaRepository;
        private readonly IConfiguration configuration;
        private readonly DatabaseContext context;
        private readonly IMapper mapper;

        public PaymentService(IPorudzbinaRepository porudzbinaRepository, IConfiguration configuration, DatabaseContext context, IMapper mapper)
        {
            this.porudzbinaRepository = porudzbinaRepository;
            this.configuration = configuration;
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<PorudzbinaPaymentDTO> CreateOrUpdatePaymentIntent(int porudzbinaId)
        {
            StripeConfiguration.ApiKey = configuration["StripeSettings:SecretKey"];

            var porudzbina = await context.Porudzbina
                .Include(s => s.StavkePorudzbine)
                .Where(s => s.PorudzbinaID == porudzbinaId)
                .Select(s => new Porudzbina
                {
                    PorudzbinaID = s.PorudzbinaID,
                    Datum = s.Datum,
                    Status = s.Status,
                    Iznos = s.Iznos,
                    PaymentIntentId = s.PaymentIntentId,
                    ClientSecret = s.ClientSecret,
                    KorisnikID = s.KorisnikID,
                    StavkePorudzbine = s.StavkePorudzbine.Select(sp => new StavkaPorudzbine
                    {
                        StavkaID = sp.StavkaID,
                        CenaStavka = sp.CenaStavka,
                        KolicinaStavka = sp.KolicinaStavka,
                        ProizvodID = sp.ProizvodID,
                        PorudzbinaID = sp.PorudzbinaID
                    }).ToList()
                }).FirstOrDefaultAsync();

            if (porudzbina == null)
            {
                return null;
            }

            var service = new PaymentIntentService();
            PaymentIntent intent;
            if(string.IsNullOrEmpty(porudzbina.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (int)(porudzbina.Iznos * 100),
                    Currency = "RSD",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                intent = await service.CreateAsync(options);
                porudzbina.PaymentIntentId = intent.Id;
                porudzbina.ClientSecret = intent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (int)(porudzbina.Iznos * 100)
                };

                await service.UpdateAsync(porudzbina.PaymentIntentId, options);
            }

            context.Set<Porudzbina>().Attach(porudzbina);
            context.Entry(porudzbina).State = EntityState.Modified;
            context.SaveChanges();

            return  mapper.Map<Porudzbina, PorudzbinaPaymentDTO>(porudzbina);
            
        }


        public async Task<Porudzbina> UpdatePorudzbinaPaymentFailed(string paymentIntentId)
        {
            var porudzbina = context.Porudzbina.Include(k => k.Korisnik).Include(sp => sp.StavkePorudzbine).
                Where(p => p.PaymentIntentId == paymentIntentId).Select(p => new Porudzbina
                {
                    PorudzbinaID = p.PorudzbinaID,
                    Datum = p.Datum,
                    Status = p.Status,
                    Iznos = p.Iznos,
                    PaymentIntentId = p.PaymentIntentId,
                    ClientSecret = p.ClientSecret,
                    KorisnikID = p.KorisnikID,
                    StavkePorudzbine = p.StavkePorudzbine.Select(sp => new StavkaPorudzbine
                    {
                        StavkaID = sp.StavkaID,
                        CenaStavka = sp.CenaStavka,
                        KolicinaStavka = sp.KolicinaStavka,
                        ProizvodID = sp.ProizvodID,
                        PorudzbinaID = sp.PorudzbinaID
                    }).ToList()

                }).FirstOrDefault();

            if (porudzbina != null)
            {
                porudzbina.PorudzbinaID = porudzbina.PorudzbinaID;
                porudzbina.Datum = porudzbina.Datum;
                porudzbina.Status = "Nije placena";
                porudzbina.Iznos = porudzbina.Iznos;
                porudzbina.KorisnikID = porudzbina.KorisnikID;

                context.Set<Porudzbina>().Attach(porudzbina);
                context.Entry(porudzbina).State = EntityState.Modified;
                context.SaveChanges();
                return porudzbina;
            }
            else
            {
                return null;
            }
        }

        public async Task<Porudzbina> UpdatePorudzbinaPaymentSucceeded(string paymentIntentId)
        {
            var porudzbina = context.Porudzbina.Include(k => k.Korisnik).Include(sp => sp.StavkePorudzbine).
                Where(p => p.PaymentIntentId == paymentIntentId).Select(p => new Porudzbina
                {
                    PorudzbinaID = p.PorudzbinaID,
                    Datum = p.Datum,
                    Status = p.Status,
                    Iznos = p.Iznos,
                    PaymentIntentId = p.PaymentIntentId,
                    ClientSecret = p.ClientSecret,
                    KorisnikID = p.KorisnikID,
                    StavkePorudzbine = p.StavkePorudzbine.Select(sp => new StavkaPorudzbine
                    {
                        StavkaID = sp.StavkaID,
                        CenaStavka = sp.CenaStavka,
                        KolicinaStavka = sp.KolicinaStavka,
                        ProizvodID = sp.ProizvodID,
                        PorudzbinaID = sp.PorudzbinaID
                    }).ToList()

                }).FirstOrDefault();

            if (porudzbina != null)
            {
                porudzbina.PorudzbinaID = porudzbina.PorudzbinaID;
                porudzbina.Datum = porudzbina.Datum;
                porudzbina.Status = "Placena";
                porudzbina.Iznos = porudzbina.Iznos;
                porudzbina.KorisnikID = porudzbina.KorisnikID;

                context.Set<Porudzbina>().Attach(porudzbina);
                context.Entry(porudzbina).State = EntityState.Modified;
                context.SaveChanges();
                return porudzbina;
            }
            else
            {
                return null;
            }
        }

    }
}
