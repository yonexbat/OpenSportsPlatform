import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { SaveWorkout } from './model/editworkout/saveWorkout';
import { PolarExchangeToken } from './model/polar/polarExchangeToken';
import { PolarRegister } from './model/polar/polarRegister';

@Injectable({
  providedIn: 'root'
})
export class PolarService {

  private apiPrefix = '';

  constructor(private http: HttpClient) { }

  public exchangeToken(dto: PolarExchangeToken): Promise<any> {
    return firstValueFrom(this.http.post<any>(`${this.apiPrefix}/Polar/ExchangeToken`, dto)) as Promise<any> ;
  }

  public registerData(): Promise<PolarRegister> {
    return firstValueFrom(this.http.get<PolarRegister>(`${this.apiPrefix}/Polar/RegisterData`)) as Promise<PolarRegister> ;
  }

}
