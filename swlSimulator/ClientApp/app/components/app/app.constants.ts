import { Injectable } from "@angular/core";

@Injectable()
export class Configuration {
  public ApiServer: string = "";
  public ApiUrl: string = "api/settings.json";
  public ServerWithApiUrl: string = this.ApiServer + this.ApiUrl;
}
