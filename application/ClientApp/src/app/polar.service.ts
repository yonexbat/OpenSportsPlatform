import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { PolarExchangeToken } from './model/polar/polarExchangeToken';
import { PolarRegister } from './model/polar/polarRegister';

@Injectable({
  providedIn: 'root'
})
export class PolarService {

  private apiPrefix = '';

  constructor(private http: HttpClient) { }

  public exchangeToken(dto: PolarExchangeToken): Promise<void> {
    return firstValueFrom(this.http.post<void>(`${this.apiPrefix}/Polar/ExchangeToken`, dto)) as Promise<void> ;
  }

  public registerData(): Promise<PolarRegister> {
    return firstValueFrom(this.http.get<PolarRegister>(`${this.apiPrefix}/Polar/RegisterData`)) as Promise<PolarRegister> ;
  }

}
