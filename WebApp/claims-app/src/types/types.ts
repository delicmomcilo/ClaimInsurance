export enum TypeEnum {
  Collision = 1,
  Grounding = 2,
  BadWeather = 3,
  Fire = 4,
}

export interface ClaimResponse {
  id: string;
  year: number;
  name: string;
  damageCost: number;
  type: TypeEnum;
}

export interface ClaimsRequest {
  year: number;
  name: string;
  damageCost: number;
  type: TypeEnum;
}
