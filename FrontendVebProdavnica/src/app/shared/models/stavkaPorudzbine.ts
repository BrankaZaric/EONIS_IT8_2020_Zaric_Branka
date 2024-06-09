export class StavkaPorudzbineCreationDTO {
  cenaStavka: number;
  kolicinaStavka: number;
  proizvodID: number;
  porudzbinaID: number;

  constructor(
    cenaStavka: number,
    kolicinaStavka: number,
    proizvodID: number,
    porudzbinaID: number
  ) {
    this.cenaStavka = cenaStavka;
    this.kolicinaStavka = kolicinaStavka;
    this.proizvodID = proizvodID;
    this.porudzbinaID = porudzbinaID;
  }
}
