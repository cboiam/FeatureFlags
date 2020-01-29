import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { environment } from '../../environments/environment';
import Feature from './models/Feature';

const featureBaseUrl = `${environment.featureBaseUrl}/features`;
const environmentBaseUrl = `${environment.featureBaseUrl}/environments`;

@Injectable({
  providedIn: 'root'
})

export class FeatureListService {
  private client: HttpClient;

  constructor(client: HttpClient) {
    this.client = client;
  }

  public getFeatures = () => {
    return this.client.get<Array<Feature>>(featureBaseUrl);
  }

  public addFeature = (feature: Feature) => {
    return this.client.post<Feature>(featureBaseUrl, feature, { observe: 'response' });
  }

  public toggle = (environmentId: number) => {
    return this.client.patch(`${environmentBaseUrl}/${environmentId}/toggle`, { observe: 'response' });
  }
}
