import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { environment } from "../../environments/environment";
import Feature from "./models/Feature";
import Environment from "./models/Environment";

const featureBaseUrl = `${environment.featureBaseUrl}/features`;
const environmentBaseUrl = `${environment.featureBaseUrl}/environments`;

@Injectable({
  providedIn: "root"
})
export class FeatureService {
  private client: HttpClient;

  constructor(client: HttpClient) {
    this.client = client;
  }

  public getFeatures = () => {
    return this.client.get<Array<Feature>>(featureBaseUrl);
  };

  public addFeature = (feature: Feature) => {
    return this.client.post<Feature>(featureBaseUrl, feature, {
      observe: "response"
    });
  };

  public editFeature = (feature: Feature) => {
    return this.client.put(featureBaseUrl, feature).toPromise();
  };

  public removeFeature = (featureId: number) => {
    return this.client.delete(`${featureBaseUrl}/${featureId}`).toPromise();
  };

  public addEnvironment = (featureId: number, environment: Environment) => {
    const addUrl = `${featureBaseUrl}/${featureId}/environments`;

    return this.client.post<Environment>(addUrl, environment, {
      observe: "response"
    });
  };

  public editEnvironment = (featureId: number, environment: Environment) => {
    const updateUrl = `${featureBaseUrl}/${featureId}/environments`;

    return this.client.put(updateUrl, environment).toPromise();
  };

  public removeEnvironment = (environmentId: number) => {
    return this.client
      .delete(`${environmentBaseUrl}/${environmentId}`)
      .toPromise();
  };

  public toggle = (environmentId: number) => {
    return this.client
      .patch(`${environmentBaseUrl}/${environmentId}/toggle`, null)
      .toPromise();
  };
}
