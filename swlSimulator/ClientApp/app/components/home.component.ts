import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Configuration } from './app/app.constants'
import { HubConnection } from '@aspnet/signalr-client';
import { ISettingsService } from "./isettings.service";
import { ISettings } from "./isettings";

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})

export class HomeComponent implements OnInit {
    private hubConnection: HubConnection;
    public async: any;
    message = '';
    messages: string[] = [];


    constructor(
        private _settingsservice: ISettingsService,
        private _settings: Configuration) {
    }
    public settings: ISettings[];

    public sendMessage(): void {
        const data = `Sent: ${this.message}`;

        this.hubConnection.invoke('Send', data);
        this.messages.push(data);
    }

    ngOnInit() {
        this.hubConnection = new HubConnection('/loopy');

        this.hubConnection.on('Send', (data: any) => {
            const received = `Received: ${data}`;
            this.messages.push(received);
        });

        this.hubConnection.start()
            .then(() => {
                console.log('Hub connection started')
            })
            .catch(err => {
                console.log('Error while establishing connection')
            });

        this._settingsservice
            .getAll()
            .subscribe((data: ISettings[]) => this.settings = data,
                error => console.log(error),
                () => console.log("getAllItems() complete from init"));
    }

}
