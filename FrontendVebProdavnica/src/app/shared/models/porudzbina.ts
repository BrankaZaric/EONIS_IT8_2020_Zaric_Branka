export class PorudzbinaCreationDTO {
    datum: Date;
    status: string;
    iznos: number;
    korisnikID: number;

    constructor(
        datum: Date,
        status: string,
        iznos: number,
        korisnikID: number,
        
      ) {
        this.datum = datum;
        this.status = status;
        this.iznos = iznos;
        this.korisnikID = korisnikID;
      }
  }
  

  export class PorudzbinaDTO {
    porudzbinaID: number;
    datum: Date;
    status: string;
    iznos: number;
    korisnikID: number;
    paymentIntentId: string | null;
    clientSecret: string | null;

    constructor(
        porudzbinaID: number,
        datum: Date,
        status: string,
        iznos: number,
        korisnikID: number,
        paymentIntentId: string | null,
        clientSecret: string | null
      ) {
        this.porudzbinaID = porudzbinaID;
        this.datum = datum;
        this.status = status;
        this.iznos = iznos;
        this.korisnikID = korisnikID;
        this.paymentIntentId = paymentIntentId;
        this.clientSecret = clientSecret;
      }
  }
  

  export interface PorudzbinaPaymentDTO {
    porudzbinaID: number;
    datum: Date;
    status: string;
    iznos: number;
    korisnikID: number;
    paymentIntentId: string;
    clientSecret: string;
  }
  