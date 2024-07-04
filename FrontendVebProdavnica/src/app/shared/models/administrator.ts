export interface Administrator {
    adminID: number
    imeAdmin: string
    prezimeAdmin: string
    emailAdmin: string
    korisnickoImeAdmin: string
    lozinkaAdmin: string
    adresaAdmin: string
    telefonAdmin: string
  }

  export class AdministratorLozinka {
    adminID: number;
    imeAdmin: string;
    prezimeAdmin: string;
    emailAdmin: string;
    korisnickoImeAdmin: string;
    lozinkaAdmin: string;
    adresaAdmin: string;
    telefonAdmin: string;
    
    constructor(
      adminID: number,
      imeAdmin: string,
      prezimeAdmin: string,
      emailAdmin: string,
      korisnickoImeAdmin: string,
      lozinkaAdmin: string,
      adresaAdmin: string,
      telefonAdmin: string
    ){
      this.adminID = adminID;
      this.imeAdmin = imeAdmin;
      this.prezimeAdmin = prezimeAdmin;
      this.emailAdmin = emailAdmin;
      this.korisnickoImeAdmin = korisnickoImeAdmin;
      this.lozinkaAdmin = lozinkaAdmin;
      this.adresaAdmin = adresaAdmin;
      this.telefonAdmin = telefonAdmin;
    }
}