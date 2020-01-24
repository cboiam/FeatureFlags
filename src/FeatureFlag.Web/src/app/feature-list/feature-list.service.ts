import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import Feature from './models/Feature';

@Injectable({
  providedIn: 'root'
})
export class FeatureListService {

  private client: HttpClient;

  constructor(client: HttpClient) {
    this.client = client;
  }

  public getFeatures = () => {
    return this.client.get<Array<Feature>>(`${environment.featureBaseUrl}/features`);
  }
}
